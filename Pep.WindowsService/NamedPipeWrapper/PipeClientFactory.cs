using System;
using System.IO.Pipes;
using NamedPipeWrapper.IO;

namespace NamedPipeWrapper
{
	// Token: 0x02000004 RID: 4
	internal static class PipeClientFactory
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002403 File Offset: 0x00000603
		public static PipeStreamWrapper<TRead, TWrite> Connect<TRead, TWrite>(string pipeName, string serverName) where TRead : class where TWrite : class
		{
			return new PipeStreamWrapper<TRead, TWrite>(PipeClientFactory.CreateAndConnectPipe(pipeName, serverName));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002411 File Offset: 0x00000611
		public static NamedPipeClientStream CreateAndConnectPipe(string pipeName, string serverName)
		{
			NamedPipeClientStream namedPipeClientStream = PipeClientFactory.CreatePipe(pipeName, serverName);
			namedPipeClientStream.Connect();
			return namedPipeClientStream;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002420 File Offset: 0x00000620
		private static NamedPipeClientStream CreatePipe(string pipeName, string serverName)
		{
			return new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
		}
	}
}
