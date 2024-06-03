using System;
using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Threading;
using NamedPipeWrapper.IO;
using NamedPipeWrapper.Threading;

namespace NamedPipeWrapper
{
	// Token: 0x02000005 RID: 5
	public class NamedPipeConnection<TRead, TWrite> where TRead : class where TWrite : class
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000242F File Offset: 0x0000062F
		public string Name { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002437 File Offset: 0x00000637
		public bool IsConnected
		{
			get
			{
				return this._streamWrapper.IsConnected;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000022 RID: 34 RVA: 0x00002444 File Offset: 0x00000644
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x0000247C File Offset: 0x0000067C
		public event ConnectionEventHandler<TRead, TWrite> Disconnected;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000024 RID: 36 RVA: 0x000024B4 File Offset: 0x000006B4
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x000024EC File Offset: 0x000006EC
		public event ConnectionMessageEventHandler<TRead, TWrite> ReceiveMessage;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000026 RID: 38 RVA: 0x00002524 File Offset: 0x00000724
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x0000255C File Offset: 0x0000075C
		public event ConnectionExceptionEventHandler<TRead, TWrite> Error;

		// Token: 0x06000028 RID: 40 RVA: 0x00002591 File Offset: 0x00000791
		internal NamedPipeConnection(int id, string name, PipeStream serverStream)
		{
			this.Id = id;
			this.Name = name;
			this._streamWrapper = new PipeStreamWrapper<TRead, TWrite>(serverStream);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025CC File Offset: 0x000007CC
		public void Open()
		{
			Worker worker = new Worker();
			worker.Succeeded += this.OnSucceeded;
			worker.Error += this.OnError;
			worker.DoWork(new Action(this.ReadPipe));
			Worker worker2 = new Worker();
			worker2.Succeeded += this.OnSucceeded;
			worker2.Error += this.OnError;
			worker2.DoWork(new Action(this.WritePipe));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000264D File Offset: 0x0000084D
		public void PushMessage(TWrite message)
		{
			this._writeQueue.Add(message);
			this._writeSignal.Set();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002667 File Offset: 0x00000867
		public void Close()
		{
			this.CloseImpl();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000266F File Offset: 0x0000086F
		private void CloseImpl()
		{
			this._streamWrapper.Close();
			this._writeSignal.Set();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002688 File Offset: 0x00000888
		private void OnSucceeded()
		{
			if (this._notifiedSucceeded)
			{
				return;
			}
			this._notifiedSucceeded = true;
			if (this.Disconnected != null)
			{
				this.Disconnected(this);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026AE File Offset: 0x000008AE
		private void OnError(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(this, exception);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026C8 File Offset: 0x000008C8
		private void ReadPipe()
		{
			while (this.IsConnected && this._streamWrapper.CanRead)
			{
				try
				{
					TRead tread = this._streamWrapper.ReadObject();
					if (tread == null)
					{
						this.CloseImpl();
						break;
					}
					if (this.ReceiveMessage != null)
					{
						this.ReceiveMessage(this, tread);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002734 File Offset: 0x00000934
		private void WritePipe()
		{
			while (this.IsConnected && this._streamWrapper.CanWrite)
			{
				try
				{
					this._streamWrapper.WriteObject(this._writeQueue.Take());
					this._streamWrapper.WaitForPipeDrain();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly int Id;

		// Token: 0x04000011 RID: 17
		private readonly PipeStreamWrapper<TRead, TWrite> _streamWrapper;

		// Token: 0x04000012 RID: 18
		private readonly AutoResetEvent _writeSignal = new AutoResetEvent(false);

		// Token: 0x04000013 RID: 19
		private readonly BlockingCollection<TWrite> _writeQueue = new BlockingCollection<TWrite>();

		// Token: 0x04000014 RID: 20
		private bool _notifiedSucceeded;
	}
}
