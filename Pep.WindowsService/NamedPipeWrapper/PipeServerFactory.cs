using System;
using System.IO.Pipes;

namespace NamedPipeWrapper
{
	// Token: 0x0200000C RID: 12
	internal static class PipeServerFactory
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002E14 File Offset: 0x00001014
		public static NamedPipeServerStream CreateAndConnectPipe(string pipeName, PipeSecurity pipeSecurity)
		{
			NamedPipeServerStream namedPipeServerStream = PipeServerFactory.CreatePipe(pipeName, pipeSecurity);
			namedPipeServerStream.WaitForConnection();
			return namedPipeServerStream;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E23 File Offset: 0x00001023
		public static NamedPipeServerStream CreatePipe(string pipeName, PipeSecurity pipeSecurity)
		{
			return new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.WriteThrough | PipeOptions.Asynchronous, 0, 0, pipeSecurity);
		}
	}
}
