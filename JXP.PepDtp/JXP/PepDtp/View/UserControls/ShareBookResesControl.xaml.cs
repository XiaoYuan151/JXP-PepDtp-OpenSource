using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.Windows;
using JXP.Windows.MsgBox;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x0200004A RID: 74
	public partial class ShareBookResesControl : UserControl
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0001AB5C File Offset: 0x00018D5C
		public ShareBookResesControl()
		{
			this.InitializeComponent();
			base.DataContext = this.mShareTBResesVM;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001AB81 File Offset: 0x00018D81
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0001AB89 File Offset: 0x00018D89
		public string TextBookID { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001AB92 File Offset: 0x00018D92
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0001AB9A File Offset: 0x00018D9A
		public ShareBookResesControl.CloseShareBook CloseShareBookCallBack { get; set; }

		// Token: 0x060004AF RID: 1199 RVA: 0x0001ABA3 File Offset: 0x00018DA3
		public bool GetShareTbData()
		{
			return this.mShareTBResesVM.UpdateShareTbData(CommonParamter.Instance.LoginUserId, this.TextBookID);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		private void ckbSchool_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!this.mShareTBResesVM.SchoolChecked && !CustomMessageBox.Question("确认要取消共享吗？", "确定", "取消", Window.GetWindow(this), WindowStartupLocation.CenterOwner, false, MessageIconType.None, ""))
				{
					this.mShareTBResesVM.SchoolChecked = true;
				}
				else
				{
					string shareTbId = this.TextBookID + CommonParamter.Instance.LoginUserId;
					if (this.mShareTBResesVM.SubmitShareTbData(shareTbId))
					{
						CustomMessageBox.Info("操作成功!", "确定", "", Window.GetWindow(this), WindowStartupLocation.CenterOwner, false);
					}
					else
					{
						CustomMessageBox.Info("操作失败!", "确定", "", Window.GetWindow(this), WindowStartupLocation.CenterOwner, false);
					}
				}
			}
			catch (Exception arg)
			{
				CustomMessageBox.Info("操作失败!", "确定", "", Window.GetWindow(this), WindowStartupLocation.CenterOwner, false);
				LogHelper.Instance.Error(string.Format("分享教材失败，失败原因：{0}。", arg));
			}
			finally
			{
				ShareBookResesControl.CloseShareBook closeShareBookCallBack = this.CloseShareBookCallBack;
				if (closeShareBookCallBack != null)
				{
					closeShareBookCallBack();
				}
			}
		}

		// Token: 0x04000280 RID: 640
		private ShareTBResesViewModel mShareTBResesVM = new ShareTBResesViewModel();

		// Token: 0x02000107 RID: 263
		// (Invoke) Token: 0x06000AD8 RID: 2776
		public delegate void CloseShareBook();
	}
}
