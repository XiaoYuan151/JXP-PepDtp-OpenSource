using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using JXP.ReleaseUpdate.ViewModel;

namespace JXP.ReleaseUpdate
{
	// Token: 0x0200000A RID: 10
	public partial class MainWindow : Window
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002D88 File Offset: 0x00000F88
		public MainWindow(string[] args)
		{
			RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
			this.InitializeComponent();
			try
			{
				if (args == null || args.Length < 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
				{
					MessageBox.Show("更新程序启动参数错误！");
					Application.Current.Shutdown();
				}
				else
				{
					this.mStrMainProcessPath = args[0];
					this.mMainVM = new MainWindowViewModel(args[0], args[1]);
					base.DataContext = this.mMainVM;
					if (!this.mMainVM.CheckUpdateInfo())
					{
						Application.Current.Shutdown();
					}
					else
					{
						base.Visibility = Visibility.Visible;
						base.ShowDialog();
						this.SetUpdateStatus();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("更新程序启动失败！" + ex.ToString());
				Application.Current.Shutdown();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E70 File Offset: 0x00001070
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.mMainVM.CheckNewReleaseUpdate())
				{
					this.KillMainProcess();
					this.StartUpdate();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("更新程序启动失败！" + ex.ToString());
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EC4 File Offset: 0x000010C4
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (e.ButtonState == MouseButtonState.Pressed)
				{
					base.DragMove();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F00 File Offset: 0x00001100
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			if (!this.mIsUpdateCompleted)
			{
				this.CancelUPdate();
				return;
			}
			this.StartMainProcess();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F17 File Offset: 0x00001117
		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			this.KillMainProcess();
			this.StartUpdate();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F25 File Offset: 0x00001125
		private void btnSilentUpdate_Click(object sender, RoutedEventArgs e)
		{
			base.Visibility = Visibility.Collapsed;
			this.StartUpdate();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F34 File Offset: 0x00001134
		private void btnAbandon_Click(object sender, RoutedEventArgs e)
		{
			this.CancelUPdate();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F34 File Offset: 0x00001134
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.CancelUPdate();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F3C File Offset: 0x0000113C
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			this.StartMainProcess();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F44 File Offset: 0x00001144
		private void btnDownload_Click(object sender, RoutedEventArgs e)
		{
			string text = this.mMainVM.StartDownloadPack();
			if (!string.IsNullOrEmpty(text))
			{
				MessageBox.Show(text);
				return;
			}
			this.btnUpdate.Visibility = Visibility.Collapsed;
			this.btnOK.Visibility = Visibility.Collapsed;
			this.btnCancel.Visibility = Visibility.Visible;
			this.btnAbandon.IsEnabled = false;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void chBoxDonotNotify_Checked(object sender, RoutedEventArgs e)
		{
			if (this.mMainVM == null)
			{
				return;
			}
			MainWindowViewModel mainWindowViewModel = this.mMainVM;
			bool? isChecked = this.chBoxDonotNotify.IsChecked;
			bool flag = false;
			mainWindowViewModel.SetAutoUpdate(isChecked.GetValueOrDefault() == flag & isChecked != null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FE1 File Offset: 0x000011E1
		private void SetUpdateStatus()
		{
			if (this.mMainVM.IsCoerceUpdate)
			{
				this.btnUpdate.Focus();
				this.btnUpdate.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000301C File Offset: 0x0000121C
		private void StartUpdate()
		{
			if (this.mMainVM == null)
			{
				return;
			}
			this.btnDownload.Visibility = Visibility.Collapsed;
			this.btnUpdate.Visibility = Visibility.Collapsed;
			this.btnOK.Visibility = Visibility.Collapsed;
			this.btnCancel.Visibility = Visibility.Visible;
			this.btnAbandon.IsEnabled = false;
			this.mMainVM.UpdateCompletedCallBack = new UpdateCompeleted(this.OnUpdateCompleted);
			this.mMainVM.DownloadCompeletedCallBack = new DownloadCompeleted(this.OnDownloadCompleted);
			this.mMainVM.StartDownload();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030A8 File Offset: 0x000012A8
		private void CancelUPdate()
		{
			if (this.mMainVM == null)
			{
				base.Close();
			}
			this.mMainVM.StartCancel(true);
			if (this.mMainVM.IsCoerceUpdate)
			{
				if (MessageBox.Show("本次更新非常重要，取消更新将会导致客户端不能使用！" + Environment.NewLine + Environment.NewLine + "确定要取消更新吗？", "取消更新", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
				{
					this.mMainVM.StartCancel(false);
					return;
				}
				this.KillMainProcess();
				base.Close();
			}
			else if (MessageBox.Show("确定放弃本次更新吗？", "取消更新", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
			{
				this.mMainVM.StartCancel(false);
				return;
			}
			if (!this.mIsUpdateCompleted)
			{
				this.mMainVM.Cancel();
			}
			base.Close();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003164 File Offset: 0x00001364
		private void KillMainProcess()
		{
			if (string.IsNullOrEmpty(this.mStrMainProcessPath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.mStrMainProcessPath);
			this.KillProcessByName(fileNameWithoutExtension);
			this.KillProcessByName("JXP.Subprocess");
			this.KillProcessByName("pepserver");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031A8 File Offset: 0x000013A8
		private void KillProcessByName(string strProcName)
		{
			if (string.IsNullOrEmpty(strProcName))
			{
				return;
			}
			new Process();
			try
			{
				Process[] processesByName = Process.GetProcessesByName(strProcName);
				for (int i = 0; i < processesByName.Length; i++)
				{
					processesByName[i].Kill();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("程序退出失败！" + Environment.NewLine + ex.Message.ToString());
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003218 File Offset: 0x00001418
		private void StartMainProcess()
		{
			if (string.IsNullOrEmpty(this.mStrMainProcessPath))
			{
				return;
			}
			if (File.Exists(this.mStrMainProcessPath))
			{
				Process.Start(this.mStrMainProcessPath);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003244 File Offset: 0x00001444
		private void OnUpdateCompleted(bool isSuccess, bool isReStart = false)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.Visibility = Visibility.Visible;
				this.btnCancel.Visibility = Visibility.Collapsed;
				this.mIsUpdateCompleted = isSuccess;
				if (isSuccess)
				{
					if (isReStart)
					{
						this.StartMainProcess();
					}
					this.Close();
					return;
				}
				this.btnDownload.Visibility = Visibility.Visible;
				this.btnUpdate.Visibility = Visibility.Visible;
				this.btnOK.Visibility = Visibility.Collapsed;
				MessageBox.Show("更新失败，请重试更新或下载最新安装程序重新安装！");
			}), new object[0]);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000328A File Offset: 0x0000148A
		private void OnDownloadCompleted()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (base.Visibility == Visibility.Collapsed && MessageBox.Show("更新包已经下载完成，是否立即安装？" + Environment.NewLine + Environment.NewLine + "客户端升级安装，需要重启客户端，请保存当前操作!", "客户端更新安装提示", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
				{
					base.Close();
					return;
				}
				this.KillMainProcess();
				this.mMainVM.StartUpdate();
			}), new object[0]);
		}

		// Token: 0x0400001C RID: 28
		private MainWindowViewModel mMainVM;

		// Token: 0x0400001D RID: 29
		private string mStrMainProcessPath = string.Empty;

		// Token: 0x0400001E RID: 30
		private bool mIsUpdateCompleted;
	}
}
