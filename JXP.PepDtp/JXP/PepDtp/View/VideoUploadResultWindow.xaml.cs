using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;

namespace JXP.PepDtp.View
{
	// Token: 0x02000035 RID: 53
	public partial class VideoUploadResultWindow : Window
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00010F1A File Offset: 0x0000F11A
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00010F22 File Offset: 0x0000F122
		public Action CloseCallback { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00010F2B File Offset: 0x0000F12B
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00010F33 File Offset: 0x0000F133
		public Action ContinueUploadCallback { get; set; }

		// Token: 0x060002D8 RID: 728 RVA: 0x00010F3C File Offset: 0x0000F13C
		public VideoUploadResultWindow(string info)
		{
			this.InitializeComponent();
			this.lblInfo.Text = info;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010F58 File Offset: 0x0000F158
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
				Action closeCallback = this.CloseCallback;
				if (closeCallback != null)
				{
					closeCallback();
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭视频结果画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		private void btnUpload_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Action continueUploadCallback = this.ContinueUploadCallback;
			if (continueUploadCallback == null)
			{
				return;
			}
			continueUploadCallback();
		}
	}
}
