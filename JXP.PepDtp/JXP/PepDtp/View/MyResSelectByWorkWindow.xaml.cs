using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.PepDtp.View.InnerReaderControl;
using JXP.PepDtp.ViewModel;
using JXP.Utilities;
using JXP.Windows;
using JXP.Windows.MsgBox;
using pep.sdk.reader.TextbookUtils;
using pep.sdk.reader.View;
using pep.sdk.utility.Paramter;

namespace JXP.PepDtp.View
{
	// Token: 0x0200002A RID: 42
	public partial class MyResSelectByWorkWindow : Window, IStyleConnector
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000D8A6 File Offset: 0x0000BAA6
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public NotifyWebHomeWorkAddRes NotifyWebHomeWorkAddResCallBack { get; set; }

		// Token: 0x06000231 RID: 561 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public MyResSelectByWorkWindow(SelectMyResParamter paramter)
		{
			this.InitializeComponent();
			this.mMyResSelectByWorkVM = new MyResSelectByWorkViewModel();
			base.DataContext = this.mMyResSelectByWorkVM;
			this.mInitParamter = paramter;
			this.mMyResSelectByWorkVM.InitParamter = this.mInitParamter;
			this.mMyResSelectByWorkVM.SetPagingControlCallBack = new SetPagingControl(this.SetPagingData);
			this.ucPagingControl.PageIndexChanngedCallBack = new PageIndexChannged(this.ChangePage);
			base.Loaded += this.MyResSelectByWorkWindow_Loaded;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D940 File Offset: 0x0000BB40
		private void MyResSelectByWorkWindow_Loaded(object sender, RoutedEventArgs e)
		{
			MyResSelectByWorkWindow.<MyResSelectByWorkWindow_Loaded>d__8 <MyResSelectByWorkWindow_Loaded>d__;
			<MyResSelectByWorkWindow_Loaded>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<MyResSelectByWorkWindow_Loaded>d__.<>4__this = this;
			<MyResSelectByWorkWindow_Loaded>d__.<>1__state = -1;
			<MyResSelectByWorkWindow_Loaded>d__.<>t__builder.Start<MyResSelectByWorkWindow.<MyResSelectByWorkWindow_Loaded>d__8>(ref <MyResSelectByWorkWindow_Loaded>d__);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D978 File Offset: 0x0000BB78
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				List<SelectUserRes> list = (from a in this.mMyResSelectByWorkVM.mAllRes
				where !a.ShowAdd
				select a).ToList<SelectUserRes>();
				if (list != null)
				{
					string data = JsonHelper.Instance.ListToJson<SelectUserRes>(list);
					NotifyWebHomeWorkAddRes notifyWebHomeWorkAddResCallBack = this.NotifyWebHomeWorkAddResCallBack;
					if (notifyWebHomeWorkAddResCallBack != null)
					{
						notifyWebHomeWorkAddResCallBack.BeginInvoke(data, null, null);
					}
				}
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭选择画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		private void comBoxType_DropDownOpened(object sender, EventArgs e)
		{
			base.Topmost = true;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DA25 File Offset: 0x0000BC25
		private void comBoxType_DropDownClosed(object sender, EventArgs e)
		{
			base.Topmost = false;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DA2E File Offset: 0x0000BC2E
		private void SetPagingData()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.ucPagingControl.TotalPages = this.mMyResSelectByWorkVM.TotlePage;
				this.ucPagingControl.CurrentPage = this.mMyResSelectByWorkVM.CurPageNum;
				this.ucPagingControl.SetbuttonColor();
				if (this.mMyResSelectByWorkVM.TotlePage > 0)
				{
					this.ucPagingControl.Visibility = Visibility.Visible;
					this.mMyResSelectByWorkVM.ShowNoDataMessage = false;
					return;
				}
				this.ucPagingControl.Visibility = Visibility.Hidden;
				this.mMyResSelectByWorkVM.ShowNoDataMessage = true;
			}), new object[0]);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DA4E File Offset: 0x0000BC4E
		private void ChangePage(int nPagNum)
		{
			this.mMyResSelectByWorkVM.CurPageNum = nPagNum;
			this.mMyResSelectByWorkVM.FilterData();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DA67 File Offset: 0x0000BC67
		private void SearchCurConditionData()
		{
			this.mMyResSelectByWorkVM.CurPageNum = 1;
			this.mMyResSelectByWorkVM.TotlePage = 0;
			this.mMyResSelectByWorkVM.FilterData();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		private void BtnSelectChapter_Click(object sender, RoutedEventArgs e)
		{
			SelectMyResParamter selectMyResParamter = this.mInitParamter;
			string strTbid = (selectMyResParamter != null) ? selectMyResParamter.BookId : null;
			SelectMyResParamter selectMyResParamter2 = this.mInitParamter;
			new TextbookSelectChapter(strTbid, (selectMyResParamter2 != null) ? selectMyResParamter2.ChapterId : null, false, false)
			{
				SetBookChapterInfoCallBack = new SetBookChapterInfo(this.SetSelectChapterInfo),
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				Owner = this
			}.ShowDialog();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		private void SetSelectChapterInfo(Tuple<string, string, string, string, string> tupleInfo)
		{
			try
			{
				if (this.mInitParamter == null)
				{
					this.mMyResSelectByWorkVM.InitParamter = this.mInitParamter;
					this.mInitParamter = new SelectMyResParamter();
				}
				this.mInitParamter.BookId = tupleInfo.Item1;
				this.mInitParamter.BookName = tupleInfo.Item2;
				this.mInitParamter.ChapterId = tupleInfo.Item3;
				this.mInitParamter.ChapterName = tupleInfo.Item4;
				this.SetChapterBtnContent();
				this.SearchCurConditionData();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "章节选择完之后筛选数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		private void SetChapterBtnContent()
		{
			if (this.mInitParamter == null || (string.IsNullOrEmpty(this.mInitParamter.ChapterName) && string.IsNullOrEmpty(this.mInitParamter.BookName)))
			{
				this.btnSelectChapter.Content = " 选择章节  ";
				return;
			}
			if (string.IsNullOrEmpty(this.mInitParamter.ChapterName))
			{
				this.btnSelectChapter.Content = string.Format("  {0}  ", this.mInitParamter.BookName);
				return;
			}
			this.btnSelectChapter.Content = string.Concat(new string[]
			{
				"  ",
				this.mInitParamter.BookName,
				"--",
				this.mInitParamter.ChapterName,
				"  "
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		private void ComBoxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				this.SearchCurConditionData();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "资源格式选择变更。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		private void ComBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				this.SearchCurConditionData();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "资源内容类型选择变更失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DD04 File Offset: 0x0000BF04
		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if ((from a in this.mMyResSelectByWorkVM.mAllRes
				where !a.ShowAdd
				select a).Count<SelectUserRes>() >= 9)
				{
					CustomMessageBox.Info("资源已经达到最大限制数量9.", "确定", "", this, WindowStartupLocation.CenterOwner, false);
				}
				else
				{
					SelectUserRes selectUserRes = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SelectUserRes;
					if (selectUserRes != null)
					{
						selectUserRes.ShowAdd = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "添加资源失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		private void BtnRemove_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SelectUserRes selectUserRes = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SelectUserRes;
				if (selectUserRes != null)
				{
					if (CustomMessageBox.Question("确定移除资源吗？", "确定", "取消", this, WindowStartupLocation.CenterOwner, false, MessageIconType.None, ""))
					{
						selectUserRes.ShowAdd = true;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "移除资源失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000DE58 File Offset: 0x0000C058
		private void BtnPreview_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ListBoxItem listBoxItem = VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
				SelectUserRes res = listBoxItem.DataContext as SelectUserRes;
				if (res != null)
				{
					base.Dispatcher.BeginInvoke(new Action(delegate()
					{
						BookResHelper bookResHelper = new BookResHelper(this);
						string resPreviewPath = ExplorerHelper.ResPreviewPath;
						string str2 = "id=";
						string id = res.ID;
						string str3 = "&res_type=00&format=";
						string fileFormat = res.FileFormat;
						bookResHelper.OpenOnlineRes(resPreviewPath, str2 + id + str3 + ((fileFormat != null) ? fileFormat.TrimStart(new char[]
						{
							'.'
						}) : null), this.maingrid, null);
					}), new object[0]);
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "预览资源失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 9:
				((Button)target).Click += this.BtnPreview_Click;
				return;
			case 10:
				((Button)target).Click += this.BtnAdd_Click;
				return;
			case 11:
				((Button)target).Click += this.BtnRemove_Click;
				return;
			default:
				return;
			}
		}

		// Token: 0x040000E7 RID: 231
		private MyResSelectByWorkViewModel mMyResSelectByWorkVM;

		// Token: 0x040000E8 RID: 232
		private SelectMyResParamter mInitParamter;

		// Token: 0x040000E9 RID: 233
		private bool InitFlg;
	}
}
