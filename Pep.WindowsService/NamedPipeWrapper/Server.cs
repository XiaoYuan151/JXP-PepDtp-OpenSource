using System;
using System.Collections.Generic;
using System.IO.Pipes;
using NamedPipeWrapper.IO;
using NamedPipeWrapper.Threading;

namespace NamedPipeWrapper
{
	// Token: 0x0200000B RID: 11
	public class Server<TRead, TWrite> where TRead : class where TWrite : class
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000027D0 File Offset: 0x000009D0
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002808 File Offset: 0x00000A08
		public event ConnectionEventHandler<TRead, TWrite> ClientConnected;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002840 File Offset: 0x00000A40
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002878 File Offset: 0x00000A78
		public event ConnectionEventHandler<TRead, TWrite> ClientDisconnected;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000044 RID: 68 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x000028E8 File Offset: 0x00000AE8
		public event ConnectionMessageEventHandler<TRead, TWrite> ClientMessage;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002920 File Offset: 0x00000B20
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002958 File Offset: 0x00000B58
		public event PipeExceptionEventHandler Error;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000298D File Offset: 0x00000B8D
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002995 File Offset: 0x00000B95
		public bool IsActive { get; protected set; }

		// Token: 0x0600004A RID: 74 RVA: 0x0000299E File Offset: 0x00000B9E
		public Server(string pipeName, PipeSecurity pipeSecurity)
		{
			this._pipeName = pipeName;
			this._pipeSecurity = pipeSecurity;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029C0 File Offset: 0x00000BC0
		public void Start()
		{
			if (this.IsActive)
			{
				return;
			}
			this.IsActive = true;
			this._shouldKeepRunning = true;
			Worker worker = new Worker();
			worker.Error += this.OnError;
			worker.DoWork(new Action(this.ListenSync));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A10 File Offset: 0x00000C10
		public void PushMessage(TWrite message)
		{
			List<NamedPipeConnection<TRead, TWrite>> connections = this._connections;
			lock (connections)
			{
				foreach (NamedPipeConnection<TRead, TWrite> namedPipeConnection in this._connections)
				{
					namedPipeConnection.PushMessage(message);
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A88 File Offset: 0x00000C88
		public void PushMessage(TWrite message, string clientName)
		{
			List<NamedPipeConnection<TRead, TWrite>> connections = this._connections;
			lock (connections)
			{
				foreach (NamedPipeConnection<TRead, TWrite> namedPipeConnection in this._connections)
				{
					if (namedPipeConnection.Name == clientName)
					{
						namedPipeConnection.PushMessage(message);
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B10 File Offset: 0x00000D10
		public void Stop()
		{
			this.IsActive = false;
			this._shouldKeepRunning = false;
			List<NamedPipeConnection<TRead, TWrite>> connections = this._connections;
			lock (connections)
			{
				NamedPipeConnection<TRead, TWrite>[] array = this._connections.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Close();
				}
			}
			NamedPipeClient<TRead, TWrite> namedPipeClient = new NamedPipeClient<TRead, TWrite>(this._pipeName, ".");
			namedPipeClient.Start();
			namedPipeClient.WaitForConnection(TimeSpan.FromSeconds(2.0));
			namedPipeClient.Stop();
			namedPipeClient.WaitForDisconnection(TimeSpan.FromSeconds(2.0));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private void ListenSync()
		{
			this._isRunning = true;
			while (this._shouldKeepRunning)
			{
				this.WaitForConnection(this._pipeName, this._pipeSecurity);
			}
			this._isRunning = false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private void WaitForConnection(string pipeName, PipeSecurity pipeSecurity)
		{
			NamedPipeServerStream namedPipeServerStream = null;
			NamedPipeServerStream namedPipeServerStream2 = null;
			NamedPipeConnection<TRead, TWrite> namedPipeConnection = null;
			string nextConnectionPipeName = this.GetNextConnectionPipeName(pipeName);
			try
			{
				namedPipeServerStream = PipeServerFactory.CreateAndConnectPipe(pipeName, pipeSecurity);
				PipeStreamWrapper<string, string> pipeStreamWrapper = new PipeStreamWrapper<string, string>(namedPipeServerStream);
				pipeStreamWrapper.WriteObject(nextConnectionPipeName);
				pipeStreamWrapper.WaitForPipeDrain();
				pipeStreamWrapper.Close();
				namedPipeServerStream2 = PipeServerFactory.CreatePipe(nextConnectionPipeName, pipeSecurity);
				namedPipeServerStream2.WaitForConnection();
				namedPipeConnection = ConnectionFactory.CreateConnection<TRead, TWrite>(namedPipeServerStream2);
				namedPipeConnection.ReceiveMessage += this.ClientOnReceiveMessage;
				namedPipeConnection.Disconnected += this.ClientOnDisconnected;
				namedPipeConnection.Error += this.ConnectionOnError;
				namedPipeConnection.Open();
				List<NamedPipeConnection<TRead, TWrite>> connections = this._connections;
				lock (connections)
				{
					this._connections.Add(namedPipeConnection);
				}
				this.ClientOnConnected(namedPipeConnection);
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("Named pipe is broken or disconnected: {0}", arg);
				Server<TRead, TWrite>.Cleanup(namedPipeServerStream);
				Server<TRead, TWrite>.Cleanup(namedPipeServerStream2);
				this.ClientOnDisconnected(namedPipeConnection);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CFC File Offset: 0x00000EFC
		private void ClientOnConnected(NamedPipeConnection<TRead, TWrite> connection)
		{
			if (this.ClientConnected != null)
			{
				this.ClientConnected(connection);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D12 File Offset: 0x00000F12
		private void ClientOnReceiveMessage(NamedPipeConnection<TRead, TWrite> connection, TRead message)
		{
			if (this.ClientMessage != null)
			{
				this.ClientMessage(connection, message);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D2C File Offset: 0x00000F2C
		private void ClientOnDisconnected(NamedPipeConnection<TRead, TWrite> connection)
		{
			if (connection == null)
			{
				return;
			}
			List<NamedPipeConnection<TRead, TWrite>> connections = this._connections;
			lock (connections)
			{
				this._connections.Remove(connection);
			}
			if (this.ClientDisconnected != null)
			{
				this.ClientDisconnected(connection);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D8C File Offset: 0x00000F8C
		private void ConnectionOnError(NamedPipeConnection<TRead, TWrite> connection, Exception exception)
		{
			this.OnError(exception);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D95 File Offset: 0x00000F95
		private void OnError(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(exception);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DAC File Offset: 0x00000FAC
		private string GetNextConnectionPipeName(string pipeName)
		{
			string format = "{0}_{1}";
			int num = this._nextPipeId + 1;
			this._nextPipeId = num;
			return string.Format(format, pipeName, num);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DDC File Offset: 0x00000FDC
		private static void Cleanup(NamedPipeServerStream pipe)
		{
			if (pipe == null)
			{
				return;
			}
			try
			{
				pipe.Close();
			}
			finally
			{
				if (pipe != null)
				{
					((IDisposable)pipe).Dispose();
				}
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly string _pipeName;

		// Token: 0x0400001B RID: 27
		private readonly PipeSecurity _pipeSecurity;

		// Token: 0x0400001C RID: 28
		private readonly List<NamedPipeConnection<TRead, TWrite>> _connections = new List<NamedPipeConnection<TRead, TWrite>>();

		// Token: 0x0400001D RID: 29
		private int _nextPipeId;

		// Token: 0x0400001E RID: 30
		private volatile bool _shouldKeepRunning;

		// Token: 0x0400001F RID: 31
		private volatile bool _isRunning;
	}
}
