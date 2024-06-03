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
	// Token: 0x02000034 RID: 52
	public partial class VideoIntroduceWindow : Window
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00010D8F File Offset: 0x0000EF8F
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00010D97 File Offset: 0x0000EF97
		public Action OpenActivityVideoCallBack { get; set; }

		// Token: 0x060002CD RID: 717 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		public VideoIntroduceWindow()
		{
			this.InitializeComponent();
			base.Closing -= this.VideoIntroduceWindow_Closing;
			base.Closing += this.VideoIntroduceWindow_Closing;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭视频介绍画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void VideoIntroduceWindow_Closing(object sender, CancelEventArgs e)
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00010E20 File Offset: 0x0000F020
		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			VideoUploadWindow videoUploadWindow = new VideoUploadWindow();
			videoUploadWindow.Owner = ((this != null) ? base.Owner : null);
			videoUploadWindow.GoBackCallBack = this.OpenActivityVideoCallBack;
			this.BtnClose_Click(null, null);
			videoUploadWindow.ShowDialog();
		}
	}
}
