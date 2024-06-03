using System;
using System.ServiceProcess;
using Pep.WindowsService.Log;

namespace Pep.WindowsService
{
	// Token: 0x02000019 RID: 25
	internal static class Program
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000037E4 File Offset: 0x000019E4
		private static void Main(string[] args)
		{
			if (!Environment.UserInteractive)
			{
				ServiceBase.Run(new ServiceBase[]
				{
					new DaemonService()
				});
				return;
			}
			Console.WriteLine("Console");
			((IServiceDebug)new DaemonService()).TestStartupAndStop(args);
			Console.ReadLine();
		}
	}
}
