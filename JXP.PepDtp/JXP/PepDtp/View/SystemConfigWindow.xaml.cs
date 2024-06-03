using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using JXP.Controls;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.ViewModel;
using JXP.Utilities;
using JXP.Windows;
using JXP.Windows.View;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;

namespace JXP.PepDtp.View
{
	// Token: 0x02000030 RID: 48
	public partial class SystemConfigWindow : Window
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public SystemConfigWindow()
		{
			this.InitializeComponent();
			base.DataContext = this.systemConfigViewModel;
			this.InitAutoStartupSetting();
			base.Closed += this.SystemConfigWindow_Closed;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AEEF File Offset: 0x000090EF
		private void SystemConfigWindow_Closed(object sender, EventArgs e)
		{
			MaskLayerWindow.SingleInstance().CloseMaskWindow();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000F317 File Offset: 0x0000D517
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitData();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00004541 File Offset: 0x00002741
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000F320 File Offset: 0x0000D520
		private void btn_commit_Click(object sender, RoutedEventArgs e)
		{
			string text = this.tb_report.Text;
			if (string.IsNullOrEmpty(text))
			{
				ToastWin.GetToaster(this.toastTarget, true).ShowInfo(new ToastInfo
				{
					Content = "反馈或意见不能为空",
					IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
				});
				return;
			}
			if (this.mCommitWorker.IsBusy)
			{
				return;
			}
			string text2 = "pc_" + this.tb_version.Text;
			this.systemConfigViewModel.WaitingVisibility = Visibility.Visible;
			this.mCommitWorker.RunWorkerAsync(new string[]
			{
				text,
				text2
			});
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		private void MCommitWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string[] array = e.Argument as string[];
			if (array == null || array.Length < 2)
			{
				return;
			}
			e.Result = this.systemConfigViewModel.CommitReport(array[0], array[1]);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		private void MCommitWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.systemConfigViewModel.WaitingVisibility = Visibility.Collapsed;
			string content = string.Empty;
			if (e.Error != null)
			{
				LogHelper.Instance.Error(string.Format("提交反馈信息失败:[{0}]。", e.Error));
				return;
			}
			bool flag;
			if (e.Result == null || !bool.TryParse(e.Result.ToString(), out flag) || !flag)
			{
				content = "提交失败，请稍候重试";
			}
			else
			{
				content = "您的建议已提交，谢谢您的建议";
			}
			ToastWin.GetToaster(this.toastTarget, true).ShowInfo(new ToastInfo
			{
				Content = content,
				IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
			});
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000F490 File Offset: 0x0000D690
		private void grid_sys_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.systemConfigViewModel.IsSystemSelected = true;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F49E File Offset: 0x0000D69E
		private void grid_about_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.systemConfigViewModel.IsAboutSelected = true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		private void grid_report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.systemConfigViewModel.IsReportSelected = true;
			this.tb_report.Text = string.Empty;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		private void CheckBox_Click(object sender, RoutedEventArgs e)
		{
			bool? isChecked = this.startcheckbox.IsChecked;
			bool flag = true;
			if (isChecked.GetValueOrDefault() == flag & isChecked != null)
			{
				if (!ShortcutTool.Create(Environment.GetFolderPath(Environment.SpecialFolder.Startup), this.appName, this.appFile, null, null))
				{
					CustomMessageBox.Info("配置保存失败！", "确定", "", this, System.Windows.WindowStartupLocation.CenterOwner, false);
					this.startcheckbox.IsChecked = new bool?(false);
					return;
				}
			}
			else if (!ShortcutTool.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup), this.appName))
			{
				CustomMessageBox.Info("配置保存失败！", "确定", "", this, System.Windows.WindowStartupLocation.CenterOwner, false);
				this.startcheckbox.IsChecked = new bool?(true);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F57C File Offset: 0x0000D77C
		private void setClose_Click(object sender, RoutedEventArgs e)
		{
			ConfigHelper.UpdateKeyValue("CloseToPallet", this.systemConfigViewModel.ColseToPallet.ToString(), false);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F5A7 File Offset: 0x0000D7A7
		private void autoupdate_Click(object sender, RoutedEventArgs e)
		{
			ClientUpdateHelper.AutoUpdate(AppConsts.JXP_PRODUCTID, this.systemConfigViewModel.AutoUpdateClient);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		private bool SetAutoStartupToRegister()
		{
			string path = "JXP.PepDtp.exe";
			string name = "JXPTeacher";
			string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			if (Environment.Is64BitOperatingSystem)
			{
				subkey = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
			}
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				RegistryKey registryKey = localMachine.CreateSubKey(subkey);
				registryKey.SetValue(name, System.IO.Path.Combine(Environment.CurrentDirectory, path));
				registryKey.Close();
				localMachine.Close();
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey2 = currentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
				registryKey2.SetValue(name, System.IO.Path.Combine(Environment.CurrentDirectory, path));
				registryKey2.Close();
				currentUser.Close();
			}
			catch
			{
				CustomMessageBox.Info("配置保存失败！", "确定", "", this, System.Windows.WindowStartupLocation.CenterOwner, false);
				return false;
			}
			return true;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F674 File Offset: 0x0000D874
		private bool RemoveAutoStartupFromRegister()
		{
			string name = "JXPTeacher";
			string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			if (Environment.Is64BitOperatingSystem)
			{
				subkey = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
			}
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				RegistryKey registryKey = localMachine.CreateSubKey(subkey);
				registryKey.DeleteValue(name, false);
				registryKey.Close();
				localMachine.Close();
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey2 = currentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
				registryKey2.DeleteValue(name, false);
				registryKey2.Close();
				currentUser.Close();
			}
			catch
			{
				CustomMessageBox.Info("配置保存失败！", "确定", "", this, System.Windows.WindowStartupLocation.CenterOwner, false);
				return false;
			}
			return true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F710 File Offset: 0x0000D910
		private void ReadAutoStartupFromRegister()
		{
			string name = "JXPTeacher";
			string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			if (Environment.Is64BitOperatingSystem)
			{
				subkey = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
			}
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				RegistryKey registryKey = localMachine.CreateSubKey(subkey);
				if (registryKey.GetValue(name) == null)
				{
					this.systemConfigViewModel.AutoStartUp = false;
				}
				else
				{
					this.systemConfigViewModel.AutoStartUp = true;
				}
				registryKey.Close();
				localMachine.Close();
				if (!this.systemConfigViewModel.AutoStartUp)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey2 = currentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
					if (registryKey2.GetValue(name) == null)
					{
						this.systemConfigViewModel.AutoStartUp = false;
					}
					else
					{
						this.systemConfigViewModel.AutoStartUp = true;
					}
					registryKey2.Close();
					currentUser.Close();
				}
			}
			catch
			{
				this.systemConfigViewModel.AutoStartUp = false;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		private void InitAutoStartupSetting()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
			this.systemConfigViewModel.AutoStartUp = ShortcutTool.IsExists(folderPath, this.appName);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F80C File Offset: 0x0000DA0C
		private void mRemindSyncRes_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool? flag;
				ConfigHelper.UpdateKeyValue(ConfigHelper.RemindSyncResKeyName, (this.mRemindSyncRes.IsChecked != null) ? flag.GetValueOrDefault().ToString() : null, false);
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("设置提醒同步资源出错：" + ex.ToString());
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F87C File Offset: 0x0000DA7C
		private void InitData()
		{
			this.InitConfigData();
			this.tb_version.Text = this.GetVersion();
			this.mCommitWorker = new BackgroundWorker();
			this.mCommitWorker.DoWork += this.MCommitWorker_DoWork;
			this.mCommitWorker.RunWorkerCompleted += this.MCommitWorker_RunWorkerCompleted;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F8DC File Offset: 0x0000DADC
		private void InitConfigData()
		{
			try
			{
				string keyValue = ConfigHelper.GetKeyValue(ConfigHelper.RemindSyncResKeyName);
				if (!string.IsNullOrEmpty(keyValue))
				{
					this.systemConfigViewModel.IsRemindSyncRes = Convert.ToBoolean(keyValue);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("初始化配置信息出错：" + ex.Message);
			}
			try
			{
				string keyValue2 = ConfigHelper.GetKeyValue("IsAutoLock");
				if (!string.IsNullOrEmpty(keyValue2))
				{
					this.systemConfigViewModel.AutoLockClient = Convert.ToBoolean(keyValue2);
				}
			}
			catch (Exception ex2)
			{
				LogHelper.Instance.Error("初始化配置信息出错：" + ex2.Message);
			}
			try
			{
				string keyValue3 = ConfigHelper.GetKeyValue("CloseToPallet");
				if (!string.IsNullOrEmpty(keyValue3))
				{
					this.systemConfigViewModel.ColseToPallet = Convert.ToBoolean(keyValue3);
				}
			}
			catch (Exception ex3)
			{
				this.systemConfigViewModel.ColseToPallet = false;
				LogHelper.Instance.Error("初始化配置信息出错：" + ex3.Message);
			}
			try
			{
				string keyValue4 = ConfigHelper.GetKeyValue("AutoUpdate");
				if (!string.IsNullOrEmpty(keyValue4))
				{
					this.systemConfigViewModel.AutoUpdateClient = Convert.ToBoolean(keyValue4);
				}
			}
			catch (Exception ex4)
			{
				LogHelper.Instance.Error("初始化配置信息出错：" + ex4.Message);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000FA44 File Offset: 0x0000DC44
		private string GetVersion()
		{
			UpdateModel updateModel = this.LoadClientAutoUpdateConfig();
			if (updateModel == null)
			{
				Version version = System.Windows.Application.ResourceAssembly.GetName().Version;
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					(version != null) ? new int?(version.Major) : null,
					(version != null) ? new int?(version.Minor) : null,
					(version != null) ? new int?(version.Build) : null,
					(version != null) ? new int?(version.Revision) : null
				});
			}
			if (updateModel.State == 0)
			{
				this.systemConfigViewModel.UpdateVisibility = Visibility.Visible;
			}
			return updateModel.Version;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000FB20 File Offset: 0x0000DD20
		private UpdateModel LoadClientAutoUpdateConfig()
		{
			UpdateModel result = null;
			string path = System.IO.Path.Combine(Environment.CurrentDirectory, "update_list", "lastver.json");
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string jsonStr = streamReader.ReadToEnd();
				streamReader.Close();
				streamReader.Dispose();
				result = new JsonHelper().JsonsDeserialize<UpdateModel>(jsonStr);
			}
			catch
			{
				LogHelper.Instance.Error("客户端升级信息读取失败！");
			}
			return result;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000FB90 File Offset: 0x0000DD90
		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			this.systemConfigViewModel.AutoUpdateClient = true;
			ClientUpdateHelper.AutoUpdate(AppConsts.JXP_PRODUCTID, true);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000FBA9 File Offset: 0x0000DDA9
		private void cbx_autoLock_Checked(object sender, RoutedEventArgs e)
		{
			this.SetAutoLock(true);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		private void SetAutoLock(bool isLock)
		{
			ConfigHelper.UpdateKeyValue("IsAutoLock", isLock.ToString(), false);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		private void cbx_autoLock_Unchecked(object sender, RoutedEventArgs e)
		{
			this.SetAutoLock(false);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void btn_ok_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		private void lblSearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://gxadmin.mypep.cn/gx-web-manager/");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "启动后台管理失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000FC20 File Offset: 0x0000DE20
		private void btnUploadImg_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
				openFileDialog.CheckFileExists = true;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(*.png)|*.png|");
				stringBuilder.Append("(*.jpg)|*.jpg|");
				stringBuilder.Append("(*.bmp)|*.bmp|");
				stringBuilder.Append("(*.jpeg)|*.jpeg|");
				stringBuilder.Append("所有文件(*.*)|*.*");
				openFileDialog.Filter = stringBuilder.ToString();
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.systemConfigViewModel.ImagePath = openFileDialog.FileName;
					this.lblImageName.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
					this.systemConfigViewModel.UploadImgeVisibility = Visibility.Collapsed;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("上传图片失败：[{0}]。", arg));
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		private void btnCancelImage_Click(object sender, RoutedEventArgs e)
		{
			this.systemConfigViewModel.ImagePath = string.Empty;
			this.lblImageName.Text = string.Empty;
			this.systemConfigViewModel.UploadImgeVisibility = Visibility.Visible;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000FD20 File Offset: 0x0000DF20
		private void btnQ1_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Process.Start("https://rj.gxjyzy.com/answer.html");
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000FD60 File Offset: 0x0000DF60
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start(AppConsts.Online_Customer_service_Url);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("在线客服失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Image_MouseLeftButtonDown(sender, null);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
		private void lblConcealAgreement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://szgs-100.oss-cn-beijing.aliyuncs.com/ysxy/policy8gt.html");
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		private void lblUserAgreement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://szgs-100.oss-cn-beijing.aliyuncs.com/ysxy/userprotocolGX.html");
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x04000122 RID: 290
		public System.EventHandler<ValueChangedEventArgs> MinisizeToPalletValueChangedHandled;

		// Token: 0x04000123 RID: 291
		private string appName = "八桂教学通客户端";

		// Token: 0x04000124 RID: 292
		private string appFile = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000125 RID: 293
		private SystemConfigViewModel systemConfigViewModel = new SystemConfigViewModel();

		// Token: 0x04000126 RID: 294
		private BackgroundWorker mCommitWorker;
	}
}
