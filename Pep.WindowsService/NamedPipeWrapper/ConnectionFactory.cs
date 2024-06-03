using System;
using System.IO.Pipes;

namespace NamedPipeWrapper
{
	// Token: 0x02000006 RID: 6
	internal static class ConnectionFactory
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002790 File Offset: 0x00000990
		public static NamedPipeConnection<TRead, TWrite> CreateConnection<TRead, TWrite>(PipeStream pipeStream) where TRead : class where TWrite : class
		{
			return new NamedPipeConnection<TRead, TWrite>(++ConnectionFactory._lastId, "Client " + ConnectionFactory._lastId.ToString(), pipeStream);
		}

		// Token: 0x04000015 RID: 21
		private static int _lastId;
	}
}
