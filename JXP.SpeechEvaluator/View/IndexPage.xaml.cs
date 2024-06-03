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
	// Token: 0x02000009 RID: 9
	public partial class IndexPage : UserControl, INavigationPage
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002B65 File Offset: 0x00000D65
		public IndexPage()
		{
			this.InitializeComponent();
			this.mPageVM = new IndexPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B8A File Offset: 0x00000D8A
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as IndexPageParas);
			this.InitData();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BA4 File Offset: 0x00000DA4
		private void InitData()
		{
			this.mPageVM.Speeches.Clear();
			foreach (ChapterData chapterData in this.mNaviParas.Speeches.chapterList)
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

		// Token: 0x06000041 RID: 65 RVA: 0x00002D40 File Offset: 0x00000F40
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.Index);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D4D File Offset: 0x00000F4D
		private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D50 File Offset: 0x00000F50
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

		// Token: 0x06000044 RID: 68 RVA: 0x00002E20 File Offset: 0x00001020
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

		// Token: 0x06000045 RID: 69 RVA: 0x00002E3C File Offset: 0x0000103C
		private TreeViewItem TryGetClickedItem(TreeView treeView, MouseButtonEventArgs e)
		{
			DependencyObject dependencyObject = e.OriginalSource as DependencyObject;
			while (dependencyObject != null && !(dependencyObject is TreeViewItem))
			{
				dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
			}
			return dependencyObject as TreeViewItem;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E6F File Offset: 0x0000106F
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E78 File Offset: 0x00001078
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E81 File Offset: 0x00001081
		public void Close(bool dispose = false)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E83 File Offset: 0x00001083
		private void btnDH_Gendu_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E85 File Offset: 0x00001085
		private void btnDH_BeiSong_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E87 File Offset: 0x00001087
		private void btnDH_Duihua_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E89 File Offset: 0x00001089
		private void btnXS_Zidu_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E8B File Offset: 0x0000108B
		private void btnXS_Gendu_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8D File Offset: 0x0000108D
		private void btnXS_BeiSong_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0400001A RID: 26
		private IndexPageParas mNaviParas;

		// Token: 0x0400001B RID: 27
		private IndexPageVM mPageVM;
	}
}
