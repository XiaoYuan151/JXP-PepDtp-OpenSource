using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.SpeechEvaluator.Model.Http;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.View;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.Windows;
using JXP.Windows.MsgBox;

namespace JXP.SpeechEvaluator
{
	// Token: 0x02000005 RID: 5
	public partial class SpeechEvaluatorWin : Window
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021C4 File Offset: 0x000003C4
		public string ObjectId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021D5 File Offset: 0x000003D5
		public int ObjectType { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021DE File Offset: 0x000003DE
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021E6 File Offset: 0x000003E6
		public EvaluatorStartParam StartParam { get; set; }

		// Token: 0x06000017 RID: 23 RVA: 0x000021F0 File Offset: 0x000003F0
		public SpeechEvaluatorWin()
		{
			this.InitializeComponent();
			this.InitPageNavigation();
			base.Loaded += this.SpeechEvaluatorWin_Loaded;
			base.Closing += this.SpeechEvaluatorWin_Closing;
			base.PreviewKeyDown += this.MainWindow_PreviewKeyDown;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002245 File Offset: 0x00000445
		public void OpenIndexPage(IndexList indexList)
		{
			PageNavigation.Instance.PageMapping[PageType.Index] = this.indexPage;
			PageNavigation.Instance.NavigateTo(PageType.Index, new IndexPageParas
			{
				Speeches = indexList
			}, NavigationDirection.Forward);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002275 File Offset: 0x00000475
		public void OpenIndexPage(IndexList2 indexList, string bookId = "")
		{
			PageNavigation.Instance.PageMapping[PageType.Index] = this.indexPage2;
			PageNavigation.Instance.NavigateTo(PageType.Index, new IndexPageParas
			{
				Speeches = indexList,
				BookId = bookId
			}, NavigationDirection.Forward);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022AC File Offset: 0x000004AC
		public void OpenBeiSongPage(string srcUrl)
		{
			SelectionPageParas paras = new SelectionPageParas
			{
				SelPageType = SelectionPageType.BeiSong,
				SrcUrl = srcUrl
			};
			PageNavigation.Instance.NavigateTo(PageType.Selection, paras, NavigationDirection.Forward);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022DC File Offset: 0x000004DC
		public void OpenGenDuPage(string srcUrl)
		{
			GenDuPageParas paras = new GenDuPageParas
			{
				SrcUrl = srcUrl
			};
			PageNavigation.Instance.NavigateTo(PageType.GenDu, paras, NavigationDirection.Forward);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002304 File Offset: 0x00000504
		public void OpenDuiHuaPage(string srcUrl)
		{
			SelectionPageParas paras = new SelectionPageParas
			{
				SelPageType = SelectionPageType.DuiHua,
				SrcUrl = srcUrl
			};
			PageNavigation.Instance.NavigateTo(PageType.Selection, paras, NavigationDirection.Forward);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002334 File Offset: 0x00000534
		public void OpenZiDuPage(string srcUrl)
		{
			SelectionPageParas paras = new SelectionPageParas
			{
				SelPageType = SelectionPageType.ZiDu,
				SrcUrl = srcUrl
			};
			PageNavigation.Instance.NavigateTo(PageType.Selection, paras, NavigationDirection.Forward);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002362 File Offset: 0x00000562
		private void SpeechEvaluatorWin_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002364 File Offset: 0x00000564
		private void SpeechEvaluatorWin_Closing(object sender, CancelEventArgs e)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002368 File Offset: 0x00000568
		private void InitPageNavigation()
		{
			Dictionary<PageType, INavigationPage> dictionary = new Dictionary<PageType, INavigationPage>();
			dictionary.Add(PageType.Index, this.indexPage);
			dictionary.Add(PageType.Selection, this.selectionPage);
			dictionary.Add(PageType.Result, this.resultPage);
			dictionary.Add(PageType.ZiDu, this.ziduPage);
			dictionary.Add(PageType.BeiSongNoTips, this.beisongNotipsPage);
			dictionary.Add(PageType.BeiSongTips, this.beisongtipsPage);
			dictionary.Add(PageType.DuiHua, this.duihuaPage);
			dictionary.Add(PageType.GenDu, this.genduPage);
			PageNavigation.Instance.Init(dictionary);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023F0 File Offset: 0x000005F0
		private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) && Keyboard.IsKeyDown(Key.Delete) && CustomMessageBox.Question("清除所有缓存吗", "清除", "取消", this, WindowStartupLocation.CenterOwner, false, MessageIconType.None, ""))
			{
				EvaluatorContext.ClearCache();
			}
		}
	}
}
