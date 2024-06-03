using System;

namespace NamedPipeWrapper
{
	// Token: 0x02000002 RID: 2
	public class NamedPipeClient<TReadWrite> : NamedPipeClient<TReadWrite, TReadWrite> where TReadWrite : class
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public NamedPipeClient(string pipeName, string serverName = ".") : base(pipeName, serverName)
		{
		}
	}
}
