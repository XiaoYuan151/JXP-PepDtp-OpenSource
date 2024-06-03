using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Logs;
using JXP.Models;
using JXP.Networks;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.PepClass;
using JXP.PepDtp.Transfer;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows.View;
using pep.Nobook.Windows;

namespace JXP.PepDtp.View
{
	// Token: 0x0200001B RID: 27
	public partial class ExitTip : Window
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000B64D File Offset: 0x0000984D
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000B655 File Offset: 0x00009855
		public ExitHandle ExitHandleCallBack { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000B65E File Offset: 0x0000985E
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000B666 File Offset: 0x00009866
		public bool IsOnline { get; set; }

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B670 File Offset: 0x00009870
		public ExitTip()
		{
			this.InitializeComponent();
			base.Closed += this.ExitTip_Closed;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000AEEF File Offset: 0x000090EF
		private void ExitTip_Closed(object sender, EventArgs e)
		{
			MaskLayerWindow.SingleInstance().CloseMaskWindow();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000B6DF File Offset: 0x000098DF
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000B6F1 File Offset: 0x000098F1
		public string CancelContent
		{
			get
			{
				return (string)base.GetValue(ExitTip.CancelContentProperty);
			}
			set
			{
				base.SetValue(ExitTip.CancelContentProperty, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000B6FF File Offset: 0x000098FF
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000B711 File Offset: 0x00009911
		public string OkContent
		{
			get
			{
				return (string)base.GetValue(ExitTip.OkContentProperty);
			}
			set
			{
				base.SetValue(ExitTip.OkContentProperty, value);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B720 File Offset: 0x00009920
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.CancelContent == " 下载完自动退出 ")
				{
					base.Close();
					Application.Current.MainWindow.Activate();
					ThreadEx.Run(delegate()
					{
						ExitTip.<<btnCancel_Click>b__26_0>d <<btnCancel_Click>b__26_0>d;
						<<btnCancel_Click>b__26_0>d.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
						<<btnCancel_Click>b__26_0>d.<>4__this = this;
						<<btnCancel_Click>b__26_0>d.<>1__state = -1;
						<<btnCancel_Click>b__26_0>d.<>t__builder.Start<ExitTip.<<btnCancel_Click>b__26_0>d>(ref <<btnCancel_Click>b__26_0>d);
					});
				}
				else
				{
					base.Close();
					Application.Current.MainWindow.Activate();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("退出登录出错[btnCancel_Click]。" + ex.ToString());
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B7B0 File Offset: 0x000099B0
		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			ExitTip.<btnOk_Click>d__27 <btnOk_Click>d__;
			<btnOk_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<btnOk_Click>d__.<>4__this = this;
			<btnOk_Click>d__.<>1__state = -1;
			<btnOk_Click>d__.<>t__builder.Start<ExitTip.<btnOk_Click>d__27>(ref <btnOk_Click>d__);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B7E8 File Offset: 0x000099E8
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			IEnumerable<KeyValuePair<string, TBDownloadOperate>> enumerable = from o in this.mTransTBRecords.DownloadTBsCollection
			where o.Value.TransState == TransferState.Downloading
			select o;
			IEnumerable<TransferFileModel> enumerable2 = from t in UserResourcesManager.Instance.CenterDownloadUserRes.TransferOperList.WaitTransList
			where t.TransState == TransferState.Downloading
			select t;
			IEnumerable<TransferFileModel> enumerable3 = from t in SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.WaitTransList
			where t.TransState == TransferState.Downloading
			select t;
			IEnumerable<TransferFileModel> enumerable4 = from t in SyncResourcesManager.Instance.LstUploadUserRes.TransferList.WaitTransList
			where t.TransState == TransferState.Uploading
			select t;
			if ((enumerable != null && enumerable.Count<KeyValuePair<string, TBDownloadOperate>>() > 0) || (enumerable2 != null && enumerable2.Count<TransferFileModel>() > 0) || (enumerable3 != null && enumerable3.Count<TransferFileModel>() > 0) || (enumerable4 != null && enumerable4.Count<TransferFileModel>() > 0))
			{
				this.lblInfo.Visibility = Visibility.Visible;
				this.OkContent = "立即退出";
				this.CancelContent = " 下载完自动退出 ";
				return;
			}
			this.lblInfo.Visibility = Visibility.Collapsed;
			this.OkContent = "退出";
			this.CancelContent = "再看看";
			if (CommonParamter.Instance.IsInClass)
			{
				this.lblMsg.Text = this._pepClassExitMsg;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B968 File Offset: 0x00009B68
		private void AborTransfer()
		{
			try
			{
				foreach (KeyValuePair<string, TBDownloadOperate> keyValuePair in from o in this.mTransTBRecords.DownloadTBsCollection
				where o.Value.TransState != TransferState.Completed
				select o)
				{
					keyValuePair.Value.Abort();
				}
				foreach (TransferFileModel transFile in (from t in UserResourcesManager.Instance.CenterDownloadUserRes.TransferOperList.WaitTransList
				where t.TransState != TransferState.Completed
				select t).ToList<TransferFileModel>())
				{
					UserResourcesManager.Instance.CenterDownloadUserRes.TransferOperList.AbortTrans(transFile);
				}
				foreach (TransferFileModel transFile2 in (from t in SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.WaitTransList
				where t.TransState != TransferState.Completed
				select t).ToList<TransferFileModel>())
				{
					SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.AbortTrans(transFile2);
				}
				foreach (TransferFileModel transFile3 in (from t in SyncResourcesManager.Instance.LstUploadUserRes.TransferList.WaitTransList
				where t.TransState != TransferState.Completed
				select t).ToList<TransferFileModel>())
				{
					SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.AbortTrans(transFile3);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("程序退出终止下载线程出错：" + ex.ToString());
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		private static void ExitLogin()
		{
			try
			{
				HttpHelper.HttpGet(ExplorerHelper.LogoutUrl, null, true, "");
				string code = "var frame = document.getElementById('bottomframe'); frame.contentWindow.logout();";
				MainWindow.MainBrowser.ExecuteJavaScript(code, "", 0);
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("退出登录出错：" + ex.ToString());
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BC58 File Offset: 0x00009E58
		public void ExitFunc()
		{
			if (CommonParamter.Instance.IsInClass)
			{
				NetManager.Instance.SendPipeDataToClient(new PipeMessagePacket
				{
					Command = "exit"
				}, "");
			}
			CefWindow instance = CefWindow.Instance;
			if (instance != null)
			{
				instance.Close();
			}
			PepClassService.StopPepServer();
			ProcessHelper.DeleteProcessByName("JXP.Subprocess.exe");
			ProcessHelper.DeleteProcessByName("JXP.PepDtp.exe");
			Application.Current.Shutdown();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.MainWindow.Activate();
		}

		// Token: 0x04000088 RID: 136
		private const string mExitTip = "您确定要退出八桂教学通吗?";

		// Token: 0x04000089 RID: 137
		private const string mExitOk = "退出";

		// Token: 0x0400008A RID: 138
		private const string mExitCancel = "再看看";

		// Token: 0x0400008B RID: 139
		private const string mDownLoadingTip = "您还有正在下载的资源,退出会中断下载进程,您确定要退出吗？";

		// Token: 0x0400008C RID: 140
		private const string mExitQuik = "立即退出";

		// Token: 0x0400008D RID: 141
		private const string mExitWait = " 下载完自动退出 ";

		// Token: 0x0400008E RID: 142
		private string _pepClassExitMsg = string.Concat(new string[]
		{
			"您有正在进行的课堂,",
			Environment.NewLine,
			"退出客户端将结束课堂,",
			Environment.NewLine,
			"确定要退出客户端吗?"
		});

		// Token: 0x0400008F RID: 143
		private TransTextBooksRecords mTransTBRecords = TransTextBooksRecords.SingleInstance;

		// Token: 0x04000092 RID: 146
		public static readonly DependencyProperty CancelContentProperty = DependencyProperty.Register("CancelContent", typeof(string), typeof(ExitTip), new PropertyMetadata("再看看"));

		// Token: 0x04000093 RID: 147
		public static readonly DependencyProperty OkContentProperty = DependencyProperty.Register("OkContent", typeof(string), typeof(ExitTip), new PropertyMetadata("退出"));
	}
}
