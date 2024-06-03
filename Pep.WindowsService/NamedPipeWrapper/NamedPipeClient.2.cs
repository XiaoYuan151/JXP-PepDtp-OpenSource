using System;
using System.IO.Pipes;
using System.Threading;
using NamedPipeWrapper.IO;
using NamedPipeWrapper.Threading;

namespace NamedPipeWrapper
{
	// Token: 0x02000003 RID: 3
	public class NamedPipeClient<TRead, TWrite> where TRead : class where TWrite : class
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002062 File Offset: 0x00000262
		public bool AutoReconnect { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
		public event ConnectionMessageEventHandler<TRead, TWrite> ServerMessage;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public event ConnectionEventHandler<TRead, TWrite> Disconnected;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000008 RID: 8 RVA: 0x0000214C File Offset: 0x0000034C
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public event PipeExceptionEventHandler Error;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021C1 File Offset: 0x000003C1
		private string _serverName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021CA File Offset: 0x000003CA
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021D2 File Offset: 0x000003D2
		public bool IsActive { get; protected set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021DB File Offset: 0x000003DB
		public NamedPipeClient(string pipeName, string serverName)
		{
			this._pipeName = pipeName;
			this._serverName = serverName;
			this.AutoReconnect = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002210 File Offset: 0x00000410
		public void Start()
		{
			if (this.IsActive)
			{
				return;
			}
			this.IsActive = true;
			this._closedExplicitly = false;
			Worker worker = new Worker();
			worker.Error += this.OnError;
			worker.DoWork(new Action(this.ListenSync));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000225E File Offset: 0x0000045E
		public void PushMessage(TWrite message)
		{
			if (this._connection != null)
			{
				this._connection.PushMessage(message);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		public void Stop()
		{
			this.IsActive = false;
			this._closedExplicitly = true;
			if (this._connection != null)
			{
				this._connection.Close();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002299 File Offset: 0x00000499
		public void WaitForConnection()
		{
			this._connected.WaitOne();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A7 File Offset: 0x000004A7
		public void WaitForConnection(int millisecondsTimeout)
		{
			this._connected.WaitOne(millisecondsTimeout);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B6 File Offset: 0x000004B6
		public void WaitForConnection(TimeSpan timeout)
		{
			this._connected.WaitOne(timeout);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C5 File Offset: 0x000004C5
		public void WaitForDisconnection()
		{
			this._disconnected.WaitOne();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022D3 File Offset: 0x000004D3
		public void WaitForDisconnection(int millisecondsTimeout)
		{
			this._disconnected.WaitOne(millisecondsTimeout);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E2 File Offset: 0x000004E2
		public void WaitForDisconnection(TimeSpan timeout)
		{
			this._disconnected.WaitOne(timeout);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F4 File Offset: 0x000004F4
		private void ListenSync()
		{
			PipeStreamWrapper<string, string> pipeStreamWrapper = PipeClientFactory.Connect<string, string>(this._pipeName, this._serverName);
			string pipeName = pipeStreamWrapper.ReadObject();
			pipeStreamWrapper.Close();
			NamedPipeClientStream pipeStream = PipeClientFactory.CreateAndConnectPipe(pipeName, this._serverName);
			this._connection = ConnectionFactory.CreateConnection<TRead, TWrite>(pipeStream);
			this._connection.Disconnected += this.OnDisconnected;
			this._connection.ReceiveMessage += this.OnReceiveMessage;
			this._connection.Error += this.ConnectionOnError;
			this._connection.Open();
			this._connected.Set();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002393 File Offset: 0x00000593
		private void OnDisconnected(NamedPipeConnection<TRead, TWrite> connection)
		{
			if (this.Disconnected != null)
			{
				this.Disconnected(connection);
			}
			this._disconnected.Set();
			if (this.AutoReconnect && !this._closedExplicitly)
			{
				this.Start();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023CD File Offset: 0x000005CD
		private void OnReceiveMessage(NamedPipeConnection<TRead, TWrite> connection, TRead message)
		{
			if (this.ServerMessage != null)
			{
				this.ServerMessage(connection, message);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E4 File Offset: 0x000005E4
		private void ConnectionOnError(NamedPipeConnection<TRead, TWrite> connection, Exception exception)
		{
			this.OnError(exception);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023ED File Offset: 0x000005ED
		private void OnError(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(exception);
			}
		}

		// Token: 0x04000005 RID: 5
		private readonly string _pipeName;

		// Token: 0x04000006 RID: 6
		private NamedPipeConnection<TRead, TWrite> _connection;

		// Token: 0x04000007 RID: 7
		private readonly AutoResetEvent _connected = new AutoResetEvent(false);

		// Token: 0x04000008 RID: 8
		private readonly AutoResetEvent _disconnected = new AutoResetEvent(false);

		// Token: 0x04000009 RID: 9
		private volatile bool _closedExplicitly;
	}
}
