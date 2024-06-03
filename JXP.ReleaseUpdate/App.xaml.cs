using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000009 RID: 9
	public partial class App : Application
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C70 File Offset: 0x00000E70
		public App()
		{
			this.InitializeComponent();
			this.RegisterEvent();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CB2 File Offset: 0x00000EB2
		private void RegisterEvent()
		{
			base.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CDC File Offset: 0x00000EDC
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D09 File Offset: 0x00000F09
		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message.ToString());
			e.Handled = true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D28 File Offset: 0x00000F28
		private static bool HasRunningApp()
		{
			bool flag;
			App.mutexSingleton = new Mutex(true, "{F89C12F6-FF77-4D94-A14C-2C321792CC13}", ref flag);
			return !flag;
		}

		// Token: 0x04000019 RID: 25
		private static Mutex mutexSingleton;

		// Token: 0x0400001A RID: 26
		private const string MUTEX_NAME = "{F89C12F6-FF77-4D94-A14C-2C321792CC13}";
	}
}
