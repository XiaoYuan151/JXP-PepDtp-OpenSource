using System;
using System.IO.Pipes;

namespace NamedPipeWrapper.IO
{
	// Token: 0x02000013 RID: 19
	public class PipeStreamWrapper<TReadWrite> : PipeStreamWrapper<TReadWrite, TReadWrite> where TReadWrite : class
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003180 File Offset: 0x00001380
		public PipeStreamWrapper(PipeStream stream) : base(stream)
		{
		}
	}
}
