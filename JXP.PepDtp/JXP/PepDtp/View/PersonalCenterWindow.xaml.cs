using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using JXP.Data;
using JXP.Enums;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.Utilities;
using JXP.WinAPI;
using JXP.Windows;
using JXP.Windows.View;
using pep.sdk.utility;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x0200002B RID: 43
	public partial class PersonalCenterWindow : Window
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E1A3 File Offset: 0x0000C3A3
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000E1AB File Offset: 0x0000C3AB
		public LoginHelper mLoginOper { get; set; }

		// Token: 0x06000249 RID: 585 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public PersonalCenterWindow(MainMenuEnums menuEnum)
		{
			this.InitializeComponent();
			string startUrl = ConfigHelper.JCServerUrl + "rj/info?terminal=5&theme=009999&subTheme=33adad&action=1&token=" + CommonParamter.Instance.JcToken;
			this.cefBrowser.StartUrl = startUrl;
			base.Loaded += this.PersonalCenterWindow_Loaded;
			base.Closed += this.PersonalCenterWindow_Closed;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000E238 File Offset: 0x0000C438
		private void PersonalCenterWindow_Closed(object sender, EventArgs e)
		{
			try
			{
				this.cefBrowser.Dispose();
				WndProcHelper.Instance.OnMessageReceived -= this.Instance_OnMessageReceived;
				MaskLayerWindow.SingleInstance().CloseMaskWindow();
			}
			catch
			{
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000E288 File Offset: 0x0000C488
		private void PersonalCenterWindow_Loaded(object sender, RoutedEventArgs e)
		{
			WndProcHelper.Instance.OnMessageReceived -= this.Instance_OnMessageReceived;
			WndProcHelper.Instance.OnMessageReceived += this.Instance_OnMessageReceived;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭个人中心画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E304 File Offset: 0x0000C504
		private void radioPersion_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string url = ExplorerHelper.CreateWebUrlByIndex(59);
				this.cefBrowser.NavigateTo(url);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("跳转个人信息失败:{0}。", arg));
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E350 File Offset: 0x0000C550
		private void radioGroup_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string url = ExplorerHelper.CreateWebUrlByIndex(56);
				this.cefBrowser.NavigateTo(url);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("跳转群组信息失败:{0}。", arg));
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E39C File Offset: 0x0000C59C
		private void radioAccount_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string url = ExplorerHelper.CreateWebUrlByIndex(60);
				this.cefBrowser.NavigateTo(url);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("跳转账号信息失败:{0}。", arg));
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		private void Instance_OnMessageReceived(object sender, WinMessageArgs e)
		{
			if (e.Message.Msg == 74)
			{
				string lpData = ((WinApi.CopyDataStruct)Marshal.PtrToStructure(e.Message.LParam, typeof(WinApi.CopyDataStruct))).lpData;
				this.SwitchMessages(lpData);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000E438 File Offset: 0x0000C638
		private void SwitchMessages(string msg)
		{
			if (string.IsNullOrEmpty(msg))
			{
				return;
			}
			string[] separator = new string[]
			{
				"??<*>()??"
			};
			string[] separator2 = new string[]
			{
				"&&<*>&&"
			};
			string[] array = msg.Split(separator, StringSplitOptions.None);
			string a = array[0];
			string[] funcParams = null;
			if (array.Length > 1)
			{
				funcParams = array[1].Split(separator2, StringSplitOptions.None);
			}
			if (a == "UploadUserPhoto")
			{
				base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(delegate()
				{
					this.UploadUserPhoto(funcParams[0]);
				}));
				return;
			}
			if (a == "SaveLoginResultInfo")
			{
				LoginHelper mLoginOper = this.mLoginOper;
				if (mLoginOper != null)
				{
					mLoginOper.SaveLoginResultInfo(array[1]);
				}
				this.radioPersion_Click(null, null);
				return;
			}
			if (!(a == "notifyInfo"))
			{
				return;
			}
			base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(delegate()
			{
				ToastWin.GetToaster(this.txtTargetTip, true).ShowInfo(new ToastInfo
				{
					Content = funcParams[0],
					IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
				});
			}));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000E524 File Offset: 0x0000C724
		public string UploadUserPhoto(string uploadPath)
		{
			string text = string.Empty;
			try
			{
				string reducedImageFullName = this.mReduceImageOper.GetReducedImageFullName();
				if (string.IsNullOrEmpty(reducedImageFullName))
				{
					return text;
				}
				FileInfo fileInfo = new FileInfo(reducedImageFullName);
				if (!fileInfo.Exists)
				{
					CustomMessageBox.Info("头像上传失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
					return text;
				}
				if ((double)fileInfo.Length > 524288.0)
				{
					CustomMessageBox.Info("选择的头像文件压缩后仍过大!（建议选择小于2M的头像图片）", "确定", "", this, WindowStartupLocation.CenterOwner, false);
					return text;
				}
				string text2 = Guid.NewGuid().ToString() + Path.GetExtension(reducedImageFullName);
				if (uploadPath.StartsWith("/") || uploadPath.StartsWith("\\"))
				{
					uploadPath = uploadPath.Substring(1);
				}
				bool flag = this.mUploadOper.UploadUserPhoto(reducedImageFullName, uploadPath, text2, null);
				if (!flag)
				{
					IList<string> deviceInfo = new DevicesDataAccess().GetDeviceInfo(4);
					if (deviceInfo == null || deviceInfo.Count < 3)
					{
						return string.Empty;
					}
					string destUrl = string.Concat(new string[]
					{
						"http://",
						deviceInfo[0],
						":",
						deviceInfo[1],
						deviceInfo[2]
					});
					flag = this.mHttpOper.HttpPostFile(destUrl, reducedImageFullName, uploadPath, text2, 1024);
				}
				if (flag)
				{
					text = Path.Combine(uploadPath, text2);
				}
				if (!text.StartsWith("/") && text.StartsWith("\\"))
				{
					text = "/" + text;
				}
				string code = "var frame = document.getElementById('framright'); frame.contentWindow.photo_url('" + text + "');";
				this.cefBrowser.ExecuteJavaScript(code, "", 0);
				code = "photo_url('" + text + "');";
				this.cefBrowser.ExecuteJavaScript(code, "", 0);
				try
				{
					fileInfo.Delete();
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error(ex);
				}
			}
			catch (Exception ex2)
			{
				CustomMessageBox.Info("头像上传失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
				LogHelper.Instance.Error("上传用户头像(UploadUserPhoto)出错：" + ex2.ToString());
			}
			return text;
		}

		// Token: 0x040000F7 RID: 247
		private const int WM_COPYDATA = 74;

		// Token: 0x040000F8 RID: 248
		private const string mSepFunc = "??<*>()??";

		// Token: 0x040000F9 RID: 249
		private const string mSepParam = "&&<*>&&";

		// Token: 0x040000FA RID: 250
		private bool InitFlg;

		// Token: 0x040000FB RID: 251
		private ReducedImage mReduceImageOper = new ReducedImage();

		// Token: 0x040000FC RID: 252
		private UpLoadHelper mUploadOper = new UpLoadHelper();

		// Token: 0x040000FD RID: 253
		private HttpHelper mHttpOper = new HttpHelper();
	}
}
