using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.PepDtp.Events;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.Windows;
using pep.sdk.reader.View;

namespace JXP.PepDtp.PepClass.View
{
	// Token: 0x0200007F RID: 127
	public partial class SelectGroup7ChapterWin : Window
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060008F3 RID: 2291 RVA: 0x0002B008 File Offset: 0x00029208
		// (remove) Token: 0x060008F4 RID: 2292 RVA: 0x0002B040 File Offset: 0x00029240
		public event EventHandler<PepClassEventArgs> OnStartPepClass;

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002B075 File Offset: 0x00029275
		public SelectGroup7ChapterWin()
		{
			this.InitializeComponent();
			this.mSelectGroupVM = new SelectGroupViewModel();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002B0B0 File Offset: 0x000292B0
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SelectGroup7ChapterWin.<Window_Loaded>d__10 <Window_Loaded>d__;
			<Window_Loaded>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Window_Loaded>d__.<>4__this = this;
			<Window_Loaded>d__.<>1__state = -1;
			<Window_Loaded>d__.<>t__builder.Start<SelectGroup7ChapterWin.<Window_Loaded>d__10>(ref <Window_Loaded>d__);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00004541 File Offset: 0x00002741
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002B0E8 File Offset: 0x000292E8
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			SelectGroup7ChapterWin.<btnOK_Click>d__13 <btnOK_Click>d__;
			<btnOK_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<btnOK_Click>d__.<>4__this = this;
			<btnOK_Click>d__.<>1__state = -1;
			<btnOK_Click>d__.<>t__builder.Start<SelectGroup7ChapterWin.<btnOK_Click>d__13>(ref <btnOK_Click>d__);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002B120 File Offset: 0x00029320
		private void btn_selectChapter_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.mSelectChapterWin != null)
				{
					this.mSelectChapterWin.Close();
					this.mSelectChapterWin = null;
				}
				else
				{
					string empty = string.Empty;
					string empty2 = string.Empty;
					this.mSelectChapterWin = new TextbookSelectChapter(empty, empty2, false, true);
					this.mSelectChapterWin.IsShowActivedBooks = true;
					this.mSelectChapterWin.ActivedBooksId = CommonParamter.Instance.lstAuthorizeBookIds;
					this.mSelectChapterWin.Owner = this;
					this.mSelectChapterWin.Closed += this.mSelectChapterWin_Closed;
					this.mSelectChapterWin.SetBookChapterInfoCallBack = new SetBookChapterInfo(this.SetSelectChapterInfo);
					this.mSelectChapterWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
					this.mSelectChapterWin.ShowDialog();
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200129",
						Passive = empty,
						FromPos = SelectGroup7ChapterWin.mClassPath + ".btn_selectChapter_Click"
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
				CustomMessageBox.Info("教材章节选择启动失败！", "确定", "", this, WindowStartupLocation.CenterOwner, false);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002B250 File Offset: 0x00029450
		private void SetSelectChapterInfo(Tuple<string, string, string, string, string> tupleInfo)
		{
			Tuple.Create<string, string>(tupleInfo.Item1, tupleInfo.Item3);
			this.mChapterId = tupleInfo.Item3;
			this.mBookId = tupleInfo.Item1;
			if (string.IsNullOrEmpty(tupleInfo.Item4))
			{
				this.tb_chapterInfo.Text = string.Format("  {0}  ", tupleInfo.Item2);
				return;
			}
			this.mChapterName = tupleInfo.Item4;
			this.tb_chapterInfo.Text = string.Format("  {0}--{1}  ", tupleInfo.Item2, tupleInfo.Item4);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002B2DD File Offset: 0x000294DD
		private void mSelectChapterWin_Closed(object sender, EventArgs e)
		{
			this.mSelectChapterWin = null;
		}

		// Token: 0x0400046E RID: 1134
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"PepClass",
			"View",
			"SelectGroup7ChapterWin"
		});

		// Token: 0x0400046F RID: 1135
		private SelectGroupViewModel mSelectGroupVM;

		// Token: 0x04000470 RID: 1136
		private TextbookSelectChapter mSelectChapterWin;

		// Token: 0x04000471 RID: 1137
		private string mChapterId = string.Empty;

		// Token: 0x04000472 RID: 1138
		private string mChapterName = string.Empty;

		// Token: 0x04000473 RID: 1139
		private string mBookId = string.Empty;
	}
}
