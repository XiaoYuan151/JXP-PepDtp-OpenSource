using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using JXP.Cef;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.OsInfo;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.View.TextBookReader;
using JXP.SpeechEvaluator.Utility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using pep.Course.Views;
using pep.sdk.reader.View.Textbook;
using pep.sdk.utility;
using pep.sdk.utility.Common;

namespace JXP.PepDtp
{
	// Token: 0x02000009 RID: 9
	public partial class App : Application
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000089B0 File Offset: 0x00006BB0
		public App()
		{
			this.InitializeComponent();
			ClientUpdateHelper.AutoUpdate(AppConsts.JXP_PRODUCTID);
			this.RegisterEvent();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000089D0 File Offset: 0x00006BD0
		private void RegisterEvent()
		{
			base.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			base.Startup += this.App_Startup;
			base.Exit += this.App_Exit;
			base.Activated += this.App_Activated;
			base.Deactivated += this.App_Deactivated;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008A38 File Offset: 0x00006C38
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				LogHelper.Instance.Error(ex);
			}
			try
			{
				if (e.ExceptionObject is OutOfMemoryException)
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "sys_400005",
						FromPos = App.mClassPath + ".CurrentDomain_UnhandledException",
						Params = string.Format("Reason:{0}", ex)
					});
				}
				else
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "sys_400001",
						FromPos = App.mClassPath + ".CurrentDomain_UnhandledException",
						Params = string.Format("Reason:{0}", ex)
					});
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008B04 File Offset: 0x00006D04
		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			LogHelper.Instance.Error(e.Exception);
			e.Handled = true;
			try
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "sys_400001",
					FromPos = App.mClassPath + ".App_DispatcherUnhandledException",
					Params = string.Format("Reason:{0}", e.Exception)
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008B84 File Offset: 0x00006D84
		private void App_Startup(object sender, StartupEventArgs e)
		{
			try
			{
				BaseBookReader.Instance = new TextBookReader();
				BaseBookReader.Instance.InitPdfSdkOnStartup();
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "sys_100001",
					FromPos = App.mClassPath + ".App_Startup"
				});
				TrackerManager.Tracker.NotifySync();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008BF4 File Offset: 0x00006DF4
		private void App_Exit(object sender, ExitEventArgs e)
		{
			try
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "sys_100005",
					FromPos = App.mClassPath + ".App_Exit"
				});
				TrackerManager.Tracker.NotifySync();
				ProcessHelper.DeleteProcessByName(Process.GetCurrentProcess().ProcessName);
			}
			catch
			{
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008C60 File Offset: 0x00006E60
		private void App_Activated(object sender, EventArgs e)
		{
			try
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "sys_100002",
					FromPos = App.mClassPath + ".App_Activated"
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008CB4 File Offset: 0x00006EB4
		private void App_Deactivated(object sender, EventArgs e)
		{
			try
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "sys_100003",
					FromPos = App.mClassPath + ".App_Deactivated"
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008E8C File Offset: 0x0000708C
		private static void FixPopupBug()
		{
			try
			{
				if (SystemParameters.MenuDropAlignment)
				{
					typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, false);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008ED8 File Offset: 0x000070D8
		private static void ActiveFirstInstance(IList<string> args)
		{
			try
			{
				if (Application.Current != null)
				{
					bool flag = false;
					string pepkcFilePath = (args != null && args.Count > 0) ? args[0] : string.Empty;
					if (!string.IsNullOrEmpty(pepkcFilePath) && pepkcFilePath.EndsWith(CommonParamter.Instance.CoursePackageExtension, StringComparison.InvariantCultureIgnoreCase) && File.Exists(pepkcFilePath))
					{
						flag = true;
					}
					if (flag)
					{
						Application.Current.Dispatcher.Invoke(new Action(delegate()
						{
							if (CommonParamter.Instance.CourseIsClassing)
							{
								CustomMessageBox.Info("已经上课中!", "确定", "", ToolBarWindow.Instance, WindowStartupLocation.CenterScreen, false);
								return;
							}
							if (CommonParamter.Instance.CoursePackageIsParsing)
							{
								LogHelper.Instance.Error("前一个课程包:[" + CommonParamter.Instance.PackageFilePath + "]正在打开中。");
								return;
							}
							CommonParamter.Instance.PackageFilePath = pepkcFilePath;
							MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
							CommonParamter.Instance.CourseIsOnline = false;
							ImportWindow importWindow = new ImportWindow();
							importWindow.Topmost = true;
							if (mainWindow != null)
							{
								mainWindow.Visibility = Visibility.Hidden;
							}
							importWindow.Show();
						}), new object[0]);
					}
					else
					{
						Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
						{
							if (BaseBookReader.Instance.Visibility != Visibility.Visible)
							{
								Application.Current.MainWindow.Visibility = Visibility.Visible;
								Application.Current.MainWindow.WindowState = WindowState.Maximized;
								Application.Current.MainWindow.ShowInTaskbar = true;
								Application.Current.MainWindow.Activate();
							}
						}), new object[0]);
					}
				}
			}
			catch (Exception)
			{
				LogHelper.Instance.Error("激活主窗口的发生了错误.");
			}
		}

		// Token: 0x0400003F RID: 63
		private static Mutex mutexSingleton = null;

		// Token: 0x04000040 RID: 64
		private const string MUTEX_NAME = "886B84D7-276F-4AC8-899B-4FAD01C0C32F";

		// Token: 0x04000041 RID: 65
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"App"
		});
	}
}
