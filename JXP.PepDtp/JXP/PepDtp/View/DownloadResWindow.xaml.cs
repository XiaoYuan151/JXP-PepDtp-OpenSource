using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;

namespace JXP.PepDtp.View
{
	// Token: 0x02000018 RID: 24
	public partial class DownloadResWindow : Window
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000B13C File Offset: 0x0000933C
		public DownloadResWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.DownloadResWindow_Loaded;
			base.Closed += this.DownloadResWindow_Closed;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B18F File Offset: 0x0000938F
		private void DownloadResWindow_Closed(object sender, EventArgs e)
		{
			this.mIsClosed = true;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B198 File Offset: 0x00009398
		private void DownloadResWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.DownloadResPara == null)
			{
				base.Close();
				return;
			}
			ThreadEx.Run(delegate()
			{
				this.DownloadRes();
			});
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B1BC File Offset: 0x000093BC
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭下载资源画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000B208 File Offset: 0x00009408
		private void DownloadRes()
		{
			try
			{
				bool flag = false;
				string type = "1320";
				if (this.DownloadResPara.IsNqRes)
				{
					type = "1330";
					flag = true;
				}
				string directoryName = Path.GetDirectoryName(this.DownloadResPara.SaveFilePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				string fileName = Path.GetFileName(this.DownloadResPara.SaveFilePath);
				string msg = "资源下载失败!";
				bool flag2;
				if (flag)
				{
					string downloadUrl = this.DownloadResPara.DownloadUrl;
					string dataTempFolder = PepPathHelper.GetDataTempFolder();
					string text = Guid.NewGuid().ToString("N") + Path.GetExtension(this.DownloadResPara.SaveFilePath);
					string tempFile = Path.Combine(dataTempFolder, text);
					flag2 = this.mDownlaod.DownLoadFile(downloadUrl, dataTempFolder, true, false, this.DownloadResPara.DeviceFlag, text);
					if (flag2)
					{
						UtilityHelper.DelFile(this.DownloadResPara.SaveFilePath);
						this.mEncry.FileDecrypt(tempFile, this.DownloadResPara.SaveFilePath);
						msg = "资源下载完成!";
					}
					ThreadEx.Run(delegate()
					{
						try
						{
							UtilityHelper.DelFile(tempFile);
						}
						catch
						{
						}
					});
				}
				else
				{
					string downloadUrl2 = this.DownloadResPara.DownloadUrl;
					flag2 = this.mDownlaod.DownLoadFile(downloadUrl2, directoryName, true, false, this.DownloadResPara.DeviceFlag, fileName);
					if (flag2)
					{
						msg = "资源下载完成!";
					}
				}
				if (flag2 && this.DownloadResPara.ResUserId != CommonParamter.Instance.LoginUserId)
				{
					this.NotifyDownloadCount(type, this.DownloadResPara.ResId, CommonParamter.Instance.LoginUserId, 1);
				}
				base.Dispatcher.Invoke(new Action(delegate()
				{
					if (!this.mIsClosed)
					{
						ToastWin.GetToaster(true, 350.0, 40.0).ShowInfo(new ToastInfo
						{
							Content = msg,
							IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
						});
						this.Close();
					}
				}), new object[0]);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("资源库资源下载失败，资源id：{0}，失败原因:{1}。", this.DownloadResPara.ResId, arg));
				base.Dispatcher.Invoke(new Action(delegate()
				{
					if (!this.mIsClosed)
					{
						CustomMessageBox.Info("下载失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
						base.Close();
					}
				}), new object[0]);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000B444 File Offset: 0x00009644
		public void NotifyDownloadCount(string type, string id, string userid, int num)
		{
			try
			{
				HttpHelper.HttpGet(ConfigHelper.WebServerUrl + string.Format("statistic/countDownload.json?type={0}&id={1}&num={2}&userid={3}", new object[]
				{
					type,
					id,
					num,
					userid
				}), null, true, "");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "调用平台下载次数接口失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x04000078 RID: 120
		public DownloadResParamter DownloadResPara;

		// Token: 0x04000079 RID: 121
		private DownloadHelper mDownlaod = new DownloadHelper();

		// Token: 0x0400007A RID: 122
		private EncryptorHelper mEncry = new EncryptorHelper();

		// Token: 0x0400007B RID: 123
		private bool mIsClosed;
	}
}
