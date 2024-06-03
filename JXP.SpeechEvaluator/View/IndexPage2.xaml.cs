using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Http;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;
using Microsoft.Practices.Prism;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x02000008 RID: 8
	public partial class IndexPage2 : UserControl, INavigationPage
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000025D8 File Offset: 0x000007D8
		public IndexPage2()
		{
			this.InitializeComponent();
			this.mPageVM = new IndexPageVM2();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002600 File Offset: 0x00000800
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as IndexPageParas);
			if (!(this.mNaviParas.Speeches is IndexList2))
			{
				return;
			}
			this.mIndexList = (this.mNaviParas.Speeches as IndexList2);
			if (this.mIndexList.textbookList == null || this.mIndexList.textbookList.Count == 0)
			{
				return;
			}
			this.InitData();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000266C File Offset: 0x0000086C
		private void InitData()
		{
			this.mPageVM.Speeches.Clear();
			this.mPageVM.BookList.Clear();
			int num = this.mIndexList.textbookList.FindIndex((BookData item) => item.tb_id == this.mNaviParas.BookId);
			if (num < 0)
			{
				num = 0;
			}
			for (int i = 0; i < this.mIndexList.textbookList.Count; i++)
			{
				BookData bookData = this.mIndexList.textbookList[i];
				this.mPageVM.BookList.Add(new BookItem
				{
					BookName = bookData.tb_name,
					IsSelected = (i == num),
					BookData = bookData
				});
			}
			this.InitChapter(this.mIndexList.textbookList[0]);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002734 File Offset: 0x00000934
		private void InitChapter(BookData book)
		{
			this.mPageVM.Speeches.Clear();
			if (book == null)
			{
				return;
			}
			foreach (ChapterData chapterData in book.chapterList)
			{
				Speech speech = new Speech();
				speech.Caption = chapterData.name;
				speech.Level = 0;
				this.mPageVM.Speeches.Add(speech);
				using (List<VoiceRes>.Enumerator enumerator2 = chapterData.voice_res_list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						VoiceRes voice = enumerator2.Current;
						Speech speech2 = new Speech();
						speech2.Caption = voice.title;
						speech2.Level = 1;
						speech.Speeches.Add(speech2);
						if (voice.type == "1")
						{
							speech2.Speeches.AddRange(this.mPageVM.XsSpeeches.Select(delegate(Speech item)
							{
								item = item.Clone();
								item.VoiceRes = voice;
								item.Level = 2;
								return item;
							}));
						}
						else
						{
							speech2.Speeches.AddRange(this.mPageVM.DhSpeeches.Select(delegate(Speech item)
							{
								item = item.Clone();
								item.VoiceRes = voice;
								item.Level = 2;
								return item;
							}));
						}
					}
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028C8 File Offset: 0x00000AC8
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.Index);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028D8 File Offset: 0x00000AD8
		private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.mPageVM.Title = string.Empty;
			if (e.AddedItems.Count < 1)
			{
				return;
			}
			BookItem bookItem = this.mPageVM.BookList.FirstOrDefault((BookItem item) => item.IsSelected);
			if (bookItem == null)
			{
				return;
			}
			this.mPageVM.Title = bookItem.BookName;
			this.InitChapter(bookItem.BookData);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002955 File Offset: 0x00000B55
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.Index);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002962 File Offset: 0x00000B62
		private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002964 File Offset: 0x00000B64
		private void TreeView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			TreeViewItem treeViewItem = this.TryGetClickedItem(this.treeList, e);
			if (treeViewItem == null || !(treeViewItem.DataContext is Speech))
			{
				return;
			}
			Speech speech = (Speech)treeViewItem.DataContext;
			if (speech.VoiceRes == null)
			{
				treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
				e.Handled = true;
				return;
			}
			SpeechEvaluatorWin speechEvaluatorWin = Window.GetWindow(this) as SpeechEvaluatorWin;
			switch (speech.Type)
			{
			case SpeechTestType.DH_GenDu:
			case SpeechTestType.XS_GenDu:
				speechEvaluatorWin.OpenGenDuPage(speech.VoiceRes.file_path);
				return;
			case SpeechTestType.DH_DuiHua:
				speechEvaluatorWin.OpenDuiHuaPage(speech.VoiceRes.file_path);
				return;
			case SpeechTestType.DH_BeiSong:
			case SpeechTestType.XS_BeiSong:
				speechEvaluatorWin.OpenBeiSongPage(speech.VoiceRes.file_path);
				return;
			case SpeechTestType.XS_ZiDu:
				speechEvaluatorWin.OpenZiDuPage(speech.VoiceRes.file_path);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A34 File Offset: 0x00000C34
		private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				Window window = Window.GetWindow(this);
				if (window == null)
				{
					return;
				}
				window.DragMove();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A50 File Offset: 0x00000C50
		private TreeViewItem TryGetClickedItem(TreeView treeView, MouseButtonEventArgs e)
		{
			DependencyObject dependencyObject = e.OriginalSource as DependencyObject;
			while (dependencyObject != null && dependencyObject is Visual && !(dependencyObject is TreeViewItem))
			{
				dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
			}
			return dependencyObject as TreeViewItem;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A8B File Offset: 0x00000C8B
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A94 File Offset: 0x00000C94
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A9D File Offset: 0x00000C9D
		public void Close(bool dispose = false)
		{
		}

		// Token: 0x04000015 RID: 21
		private IndexPageParas mNaviParas;

		// Token: 0x04000016 RID: 22
		private IndexPageVM2 mPageVM;

		// Token: 0x04000017 RID: 23
		private IndexList2 mIndexList;
	}
}
