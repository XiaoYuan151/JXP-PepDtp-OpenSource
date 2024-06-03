using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace NamedPipeWrapper.IO
{
	// Token: 0x02000012 RID: 18
	public class PipeStreamReader<T> where T : class
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000304D File Offset: 0x0000124D
		public PipeStream BaseStream { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003056 File Offset: 0x00001256
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000305E File Offset: 0x0000125E
		public bool IsConnected { get; private set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00003067 File Offset: 0x00001267
		public PipeStreamReader(PipeStream stream)
		{
			this.BaseStream = stream;
			this.IsConnected = stream.IsConnected;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003098 File Offset: 0x00001298
		private int ReadLength()
		{
			byte[] array = new byte[4];
			int num = this.BaseStream.Read(array, 0, 4);
			if (num == 0)
			{
				this.IsConnected = false;
				return 0;
			}
			if (num != 4)
			{
				throw new IOException(string.Format("Expected {0} bytes but read {1}", 4, num));
			}
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030F4 File Offset: 0x000012F4
		private T ReadObject(int len)
		{
			byte[] buffer = new byte[len];
			this.BaseStream.Read(buffer, 0, len);
			T result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = (T)((object)this._binaryFormatter.Deserialize(memoryStream));
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003158 File Offset: 0x00001358
		public T ReadObject()
		{
			int num = this.ReadLength();
			if (num != 0)
			{
				return this.ReadObject(num);
			}
			return default(T);
		}

		// Token: 0x04000026 RID: 38
		private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter
		{
			Binder = new PreMergeToMergedDeserializationBinder()
		};
	}
}
