using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using JXP.Logs;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;

namespace JXP.SpeechEvaluator
{
	// Token: 0x02000004 RID: 4
	public partial class App : Application
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020DF File Offset: 0x000002DF
		protected override void OnExit(ExitEventArgs e)
		{
			ISEServerAgent.Instance.Dispose();
			base.OnExit(e);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F2 File Offset: 0x000002F2
		protected override void OnStartup(StartupEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
			Application.Current.DispatcherUnhandledException += this.Current_DispatcherUnhandledException;
			base.OnStartup(e);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002127 File Offset: 0x00000327
		private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			LogHelper.Instance.Error(e.Exception);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002140 File Offset: 0x00000340
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogHelper instance = LogHelper.Instance;
			object exceptionObject = e.ExceptionObject;
			instance.Error((exceptionObject != null) ? exceptionObject.ToString() : null);
		}
	}
}
