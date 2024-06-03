using System;
using System.IO.Pipes;

namespace NamedPipeWrapper.IO
{
	// Token: 0x02000014 RID: 20
	public class PipeStreamWrapper<TRead, TWrite> where TRead : class where TWrite : class
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003189 File Offset: 0x00001389
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003191 File Offset: 0x00001391
		public PipeStream BaseStream { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000319A File Offset: 0x0000139A
		public bool IsConnected
		{
			get
			{
				return this.BaseStream.IsConnected && this._reader.IsConnected;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000031B6 File Offset: 0x000013B6
		public bool CanRead
		{
			get
			{
				return this.BaseStream.CanRead;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000031C3 File Offset: 0x000013C3
		public bool CanWrite
		{
			get
			{
				return this.BaseStream.CanWrite;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031D0 File Offset: 0x000013D0
		public PipeStreamWrapper(PipeStream stream)
		{
			this.BaseStream = stream;
			this._reader = new PipeStreamReader<TRead>(this.BaseStream);
			this._writer = new PipeStreamWriter<TWrite>(this.BaseStream);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003201 File Offset: 0x00001401
		public TRead ReadObject()
		{
			return this._reader.ReadObject();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000320E File Offset: 0x0000140E
		public void WriteObject(TWrite obj)
		{
			this._writer.WriteObject(obj);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000321C File Offset: 0x0000141C
		public void WaitForPipeDrain()
		{
			this._writer.WaitForPipeDrain();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003229 File Offset: 0x00001429
		public void Close()
		{
			this.BaseStream.Close();
		}

		// Token: 0x04000028 RID: 40
		private readonly PipeStreamReader<TRead> _reader;

		// Token: 0x04000029 RID: 41
		private readonly PipeStreamWriter<TWrite> _writer;
	}
}
