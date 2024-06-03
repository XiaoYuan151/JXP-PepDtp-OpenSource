using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.ViewModel;
using JXP.Threading;
using JXP.Windows;

namespace JXP.PepDtp.View
{
	// Token: 0x02000036 RID: 54
	public partial class VideoUploadWindow : Window
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000110CE File Offset: 0x0000F2CE
		// (set) Token: 0x060002DF RID: 735 RVA: 0x000110D6 File Offset: 0x0000F2D6
		public Action GoBackCallBack { get; set; }

		// Token: 0x060002E0 RID: 736 RVA: 0x000110DF File Offset: 0x0000F2DF
		public VideoUploadWindow()
		{
			this.InitializeComponent();
			this.mVideoUploadVM = new VideoUploadViewModel();
			base.DataContext = this.mVideoUploadVM;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011104 File Offset: 0x0000F304
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭视频上传画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011150 File Offset: 0x0000F350
		private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.BtnClose_Click(null, null);
			Action goBackCallBack = this.GoBackCallBack;
			if (goBackCallBack == null)
			{
				return;
			}
			goBackCallBack();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001116C File Offset: 0x0000F36C
		private void btnIntroduce_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<string> list = new List<string>();
				list.Add(".jpg");
				list.Add(".jpeg");
				string introduceFile;
				string text;
				if (!this.mVideoUploadVM.SelectFile(list, 50L, out introduceFile, out text))
				{
					if (!string.IsNullOrEmpty(text))
					{
						CustomMessageBox.Info(text, "确定", "", this, WindowStartupLocation.CenterOwner, false);
					}
				}
				else
				{
					this.mVideoUploadVM.IntroduceFile = introduceFile;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("添加作品封面文件失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011200 File Offset: 0x0000F400
		private void btnWorks_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<string> list = new List<string>();
				list.Add(".mp4");
				string text;
				string text2;
				if (!this.mVideoUploadVM.SelectFile(list, 400L, out text, out text2))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						CustomMessageBox.Info(text2, "确定", "", this, WindowStartupLocation.CenterOwner, false);
					}
				}
				else
				{
					this.mVideoUploadVM.WorksFile = text;
					this.mVideoUploadVM.WorkTitle = Path.GetFileNameWithoutExtension(text);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("添加作品文件失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001129C File Offset: 0x0000F49C
		private void btnSubtitles_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<string> list = new List<string>();
				list.Add(".srt");
				string subtitlesFile;
				string text;
				if (!this.mVideoUploadVM.SelectFile(list, 50L, out subtitlesFile, out text))
				{
					if (!string.IsNullOrEmpty(text))
					{
						CustomMessageBox.Info(text, "确定", "", this, WindowStartupLocation.CenterOwner, false);
					}
				}
				else
				{
					this.mVideoUploadVM.SubtitlesFile = subtitlesFile;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("添加字幕文件失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011324 File Offset: 0x0000F524
		private void btnIntroduceWorks_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<string> list = new List<string>();
				list.Add(".doc");
				list.Add(".docx");
				list.Add(".wps");
				string introduceWorksFile;
				string text;
				if (!this.mVideoUploadVM.SelectFile(list, 50L, out introduceWorksFile, out text))
				{
					if (!string.IsNullOrEmpty(text))
					{
						CustomMessageBox.Info(text, "确定", "", this, WindowStartupLocation.CenterOwner, false);
					}
				}
				else
				{
					this.mVideoUploadVM.IntroduceWorksFile = introduceWorksFile;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("添加作品介绍文件失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000113C4 File Offset: 0x0000F5C4
		private void btnSubmit_Click(object sender, RoutedEventArgs e)
		{
			string text = this.mVideoUploadVM.DataCheck();
			if (!string.IsNullOrEmpty(text))
			{
				CustomMessageBox.Info(text, "确定", "", this, WindowStartupLocation.CenterOwner, false);
				return;
			}
			ThreadEx.Run(delegate()
			{
				string empty = string.Empty;
				int fileCount;
				if (!this.mVideoUploadVM.UploadFile(out empty, out fileCount))
				{
					CustomMessageBox.Info(empty, "确定", "", this, WindowStartupLocation.CenterOwner, false);
					return;
				}
				base.Dispatcher.Invoke(new Action(delegate()
				{
					new VideoUploadResultWindow(string.Format("你上传的\"{0}\"等{1}个文件上传成功，类别:{2}，你可以，继续上传其他类别或关闭该窗口。", this.mVideoUploadVM.WorkTitle, fileCount, this.mVideoUploadVM.GetWorksTypeTitle()))
					{
						Owner = this,
						CloseCallback = new Action(this.Close),
						ContinueUploadCallback = new Action(this.ContinueUpload)
					}.ShowDialog();
				}), new object[0]);
			});
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001140C File Offset: 0x0000F60C
		private void ContinueUpload()
		{
			this.mVideoUploadVM.ClearData();
			base.Activate();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011420 File Offset: 0x0000F620
		private void lblDownload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://rjzhpt-gx-100.oss-cn-chengdu.aliyuncs.com/gx_21_activity_video/%E7%B4%A0%E6%9D%90%E5%8C%85.rar");
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("活动视频下载模板失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011464 File Offset: 0x0000F664
		private void lbl1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://rjzhpt-gx-100.oss-cn-chengdu.aliyuncs.com/gx_21_activity_video/%E4%BD%9C%E5%93%81%E5%B0%81%E9%9D%A2%E7%BC%96%E8%BE%91%E7%A4%BA%E4%BE%8B.rar");
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("活动视频下载模板失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000114A8 File Offset: 0x0000F6A8
		private void lbl2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://rjzhpt-gx-100.oss-cn-chengdu.aliyuncs.com/gx_21_activity_video/%E5%BE%AE%E8%A7%86%E9%A2%91%E5%90%88%E6%88%90%E7%A4%BA%E4%BE%8B.rar");
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("活动视频下载模板失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000114EC File Offset: 0x0000F6EC
		private void lbl3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://rjzhpt-gx-100.oss-cn-chengdu.aliyuncs.com/gx_21_activity_video/%E6%88%90%E5%93%81%E7%A4%BA%E4%BE%8B.rar");
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("活动视频下载模板失败:[{0}]。", arg));
			}
		}

		// Token: 0x0400016A RID: 362
		private VideoUploadViewModel mVideoUploadVM;
	}
}
