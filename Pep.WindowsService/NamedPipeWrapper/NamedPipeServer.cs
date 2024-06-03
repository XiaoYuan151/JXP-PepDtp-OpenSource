using System;
using System.IO.Pipes;

namespace NamedPipeWrapper
{
	// Token: 0x0200000A RID: 10
	public class NamedPipeServer<TReadWrite> : Server<TReadWrite, TReadWrite> where TReadWrite : class
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000027B9 File Offset: 0x000009B9
		public NamedPipeServer(string pipeName) : base(pipeName, null)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027C3 File Offset: 0x000009C3
		public NamedPipeServer(string pipeName, PipeSecurity pipeSecurity) : base(pipeName, pipeSecurity)
		{
		}
	}
}
