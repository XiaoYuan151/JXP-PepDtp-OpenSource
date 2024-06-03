using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace NamedPipeWrapper.IO
{
	// Token: 0x02000015 RID: 21
	public class PipeStreamWriter<T> where T : class
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003236 File Offset: 0x00001436
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000323E File Offset: 0x0000143E
		public PipeStream BaseStream { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x00003247 File Offset: 0x00001447
		public PipeStreamWriter(PipeStream stream)
		{
			this.BaseStream = stream;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003264 File Offset: 0x00001464
		private byte[] Serialize(T obj)
		{
			byte[] result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._binaryFormatter.Serialize(memoryStream, obj);
					result = memoryStream.ToArray();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000032C0 File Offset: 0x000014C0
		private void WriteLength(int len)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(len));
			this.BaseStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032E9 File Offset: 0x000014E9
		private void WriteObject(byte[] data)
		{
			this.BaseStream.Write(data, 0, data.Length);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032FB File Offset: 0x000014FB
		private void Flush()
		{
			this.BaseStream.Flush();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003308 File Offset: 0x00001508
		public void WriteObject(T obj)
		{
			byte[] array = this.Serialize(obj);
			this.WriteLength(array.Length);
			this.WriteObject(array);
			this.Flush();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003333 File Offset: 0x00001533
		public void WaitForPipeDrain()
		{
			this.BaseStream.WaitForPipeDrain();
		}

		// Token: 0x0400002B RID: 43
		private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
	}
}
