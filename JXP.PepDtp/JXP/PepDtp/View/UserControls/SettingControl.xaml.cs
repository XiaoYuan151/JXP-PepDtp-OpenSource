using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Configuration;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.Utilities;
using JXP.Windows;
using Microsoft.Win32;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000046 RID: 70
	public partial class SettingControl : UserControl
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00019670 File Offset: 0x00017870
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00019682 File Offset: 0x00017882
		public bool AutoStartUp
		{
			get
			{
				return (bool)base.GetValue(SettingControl.AutoUpdateProperty);
			}
			set
			{
				base.SetValue(SettingControl.AutoUpdateProperty, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00019695 File Offset: 0x00017895
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000196A7 File Offset: 0x000178A7
		public bool MinisizeToPallet
		{
			get
			{
				return (bool)base.GetValue(SettingControl.MinisizeToPalletProperty);
			}
			set
			{
				base.SetValue(SettingControl.MinisizeToPalletProperty, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000196BA File Offset: 0x000178BA
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000196CC File Offset: 0x000178CC
		public bool AutoUpdateClient
		{
			get
			{
				return (bool)base.GetValue(SettingControl.AutoUpdateClientProperty);
			}
			set
			{
				base.SetValue(SettingControl.AutoUpdateClientProperty, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000196DF File Offset: 0x000178DF
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000196F1 File Offset: 0x000178F1
		public bool IsRemindSyncRes
		{
			get
			{
				return (bool)base.GetValue(SettingControl.IsRemindSyncResProperty);
			}
			set
			{
				base.SetValue(SettingControl.IsRemindSyncResProperty, value);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00019704 File Offset: 0x00017904
		public SettingControl()
		{
			this.InitializeComponent();
			this.InitAutoStartupSetting();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00019734 File Offset: 0x00017934
		private void CheckBox_Click(object sender, RoutedEventArgs e)
		{
			bool? isChecked = this.startcheckbox.IsChecked;
			bool flag = true;
			if (isChecked.GetValueOrDefault() == flag & isChecked != null)
			{
				if (!ShortcutTool.Create(Environment.GetFolderPath(Environment.SpecialFolder.Startup), this.appName, this.appFile, null, null))
				{
					CustomMessageBox.Info("配置保存失败！", "确定", "", null, WindowStartupLocation.CenterOwner, false);
					this.startcheckbox.IsChecked = new bool?(false);
					return;
				}
			}
			else if (!ShortcutTool.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup), this.appName))
			{
				CustomMessageBox.Info("配置保存失败！", "确定", "", null, WindowStartupLocation.CenterOwner, false);
				this.startcheckbox.IsChecked = new bool?(true);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000197E4 File Offset: 0x000179E4
		private void minisize_Click(object sender, RoutedEventArgs e)
		{
			bool? isChecked = this.minisize.IsChecked;
			bool flag = true;
			if (isChecked.GetValueOrDefault() == flag & isChecked != null)
			{
				ConfigManager.AppSettings["MiniSizeToPallet"] = "true";
				return;
			}
			ConfigManager.AppSettings["MiniSizeToPallet"] = "false";
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001983C File Offset: 0x00017A3C
		private void autoupdate_Click(object sender, RoutedEventArgs e)
		{
			this.SaveClientAutoUpdateConfig(this.autoupdate.IsChecked.Value);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00019864 File Offset: 0x00017A64
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
				registryKey.SetValue(name, Path.Combine(Environment.CurrentDirectory, path));
				registryKey.Close();
				localMachine.Close();
				RegistryKey currentUser = Registry.CurrentUser;
				RegistryKey registryKey2 = currentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
				registryKey2.SetValue(name, Path.Combine(Environment.CurrentDirectory, path));
				registryKey2.Close();
				currentUser.Close();
			}
			catch
			{
				MessageBox.Show("配置保存失败！");
				return false;
			}
			return true;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001990C File Offset: 0x00017B0C
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
				MessageBox.Show("配置保存失败！");
				return false;
			}
			return true;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00019998 File Offset: 0x00017B98
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
					this.AutoStartUp = false;
				}
				else
				{
					this.AutoStartUp = true;
				}
				registryKey.Close();
				localMachine.Close();
				if (!this.AutoStartUp)
				{
					RegistryKey currentUser = Registry.CurrentUser;
					RegistryKey registryKey2 = currentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
					if (registryKey2.GetValue(name) == null)
					{
						this.AutoStartUp = false;
					}
					else
					{
						this.AutoStartUp = true;
					}
					registryKey2.Close();
					currentUser.Close();
				}
			}
			catch
			{
				this.AutoStartUp = false;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00019A48 File Offset: 0x00017C48
		private void InitAutoStartupSetting()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
			this.AutoStartUp = ShortcutTool.IsExists(folderPath, this.appName);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00019A70 File Offset: 0x00017C70
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

		// Token: 0x06000474 RID: 1140 RVA: 0x00019AE0 File Offset: 0x00017CE0
		private void InitData()
		{
			this.InitConfigData();
			this.InitClientAutoUpdateConfig();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00019AF0 File Offset: 0x00017CF0
		private void InitConfigData()
		{
			try
			{
				string keyValue = ConfigHelper.GetKeyValue(ConfigHelper.RemindSyncResKeyName);
				if (!string.IsNullOrEmpty(keyValue))
				{
					this.IsRemindSyncRes = Convert.ToBoolean(keyValue);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("初始化配置信息出错：" + ex.Message);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00019B4C File Offset: 0x00017D4C
		private UpdateModel LoadClientAutoUpdateConfig()
		{
			UpdateModel result = new UpdateModel();
			string path = Path.Combine(Environment.CurrentDirectory, "update_list", "lastver.json");
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

		// Token: 0x06000477 RID: 1143 RVA: 0x00019BC0 File Offset: 0x00017DC0
		private void InitClientAutoUpdateConfig()
		{
			UpdateModel updateModel = this.LoadClientAutoUpdateConfig();
			this.AutoUpdateClient = updateModel.AutoUpdate;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00019BE0 File Offset: 0x00017DE0
		private void SaveClientAutoUpdateConfig(bool isAutoUpdate)
		{
			this.AutoUpdateClient = isAutoUpdate;
			UpdateModel updateModel = this.LoadClientAutoUpdateConfig();
			updateModel.AutoUpdate = isAutoUpdate;
			string text = Path.Combine(Environment.CurrentDirectory, "update_list");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.Combine(text, "lastver.json");
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			try
			{
				string value = new JsonHelper().SerializeString<UpdateModel>(updateModel);
				StreamWriter streamWriter = new StreamWriter(path);
				streamWriter.Write(value);
				streamWriter.Close();
				streamWriter.Dispose();
			}
			catch
			{
				LogHelper.Instance.Error("客户端升级信息保存失败！");
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00019C84 File Offset: 0x00017E84
		private void root_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitData();
		}

		// Token: 0x04000251 RID: 593
		private string appName = "八桂教学通客户端";

		// Token: 0x04000252 RID: 594
		private string appFile = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000253 RID: 595
		public static readonly DependencyProperty AutoUpdateProperty = DependencyProperty.Register("AutoStartUp", typeof(bool), typeof(SettingControl), new PropertyMetadata(false));

		// Token: 0x04000254 RID: 596
		public static readonly DependencyProperty MinisizeToPalletProperty = DependencyProperty.Register("MinisizeToPallet", typeof(bool), typeof(SettingControl), new PropertyMetadata(false));

		// Token: 0x04000255 RID: 597
		public static readonly DependencyProperty AutoUpdateClientProperty = DependencyProperty.Register("AutoUpdateClient", typeof(bool), typeof(SettingControl), new PropertyMetadata(false));

		// Token: 0x04000256 RID: 598
		public static readonly DependencyProperty IsRemindSyncResProperty = DependencyProperty.Register("IsRemindSyncRes", typeof(bool), typeof(SettingControl), new PropertyMetadata(false));
	}
}
