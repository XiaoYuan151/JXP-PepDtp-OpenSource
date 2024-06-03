using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using JXP.Cef.Utils;
using JXP.Data;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.Utilities;
using JXP.WinAPI;
using JXP.Windows;
using JXP.Windows.View;
using pep.sdk.utility;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x0200001D RID: 29
	public partial class GroupInfoWindow : Window
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public bool CloseMaskWin { get; set; } = true;

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C100 File Offset: 0x0000A300
		public GroupInfoWindow()
		{
			this.InitializeComponent();
			base.Closed += this.GroupInfoWindow_Closed;
			this.cefBrowser.LoadEnd += this.CefBrowser_LoadEnd;
			this.cefBrowser.LoadError += this.CefBrowser_LoadError;
			this.cefBrowser.StartUrl = ExplorerHelper.CreateWebUrlByIndex(56);
			Dictionary<string, Cookie> dicWebCookies = CookieHelper.GetDicWebCookies();
			if (dicWebCookies.ContainsKey("JSESSIONID"))
			{
				CookieHelper.SetCefCookies(dicWebCookies["JSESSIONID"]);
			}
			WndProcHelper.Instance.OnMessageReceived -= this.Instance_OnMessageReceived;
			WndProcHelper.Instance.OnMessageReceived += this.Instance_OnMessageReceived;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭群组信息画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C230 File Offset: 0x0000A430
		private void GroupInfoWindow_Closed(object sender, EventArgs e)
		{
			try
			{
				this.cefBrowser.Dispose();
				WndProcHelper.Instance.OnMessageReceived -= this.Instance_OnMessageReceived;
				if (this.CloseMaskWin)
				{
					MaskLayerWindow.SingleInstance().CloseMaskWindow();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void CefBrowser_LoadError(object sender, LoadErrorEventArgs e)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void CefBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C288 File Offset: 0x0000A488
		private void Instance_OnMessageReceived(object sender, WinMessageArgs e)
		{
			if (e.Message.Msg == 74)
			{
				string lpData = ((WinApi.CopyDataStruct)Marshal.PtrToStructure(e.Message.LParam, typeof(WinApi.CopyDataStruct))).lpData;
				this.SwitchMessages(lpData);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
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
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C368 File Offset: 0x0000A568
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

		// Token: 0x040000A3 RID: 163
		private const int WM_COPYDATA = 74;

		// Token: 0x040000A4 RID: 164
		private const string mSepFunc = "??<*>()??";

		// Token: 0x040000A5 RID: 165
		private const string mSepParam = "&&<*>&&";

		// Token: 0x040000A6 RID: 166
		private ReducedImage mReduceImageOper = new ReducedImage();

		// Token: 0x040000A7 RID: 167
		private UpLoadHelper mUploadOper = new UpLoadHelper();

		// Token: 0x040000A8 RID: 168
		private HttpHelper mHttpOper = new HttpHelper();
	}
}
