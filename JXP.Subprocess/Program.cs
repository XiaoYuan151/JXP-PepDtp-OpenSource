using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Windows.Forms;
using JXP.Cef;
using JXP.Logs;

namespace JXP.Subprocess
{
	// Token: 0x02000002 RID: 2
	internal static class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[STAThread]
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private static int Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				ServicePointManager.DefaultConnectionLimit = 2048;
				RenderProcessStartHelper.SingleStart(null);
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				return 1;
			}
			return 0;
		}
	}
}
