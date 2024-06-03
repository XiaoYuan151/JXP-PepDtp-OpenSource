using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using JXP.SpeechEvaluator.Utility;

namespace JXP.SpeechEvaluator.View.Navigation
{
	// Token: 0x0200001B RID: 27
	internal class PageNavigation
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006A40 File Offset: 0x00004C40
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00006A48 File Offset: 0x00004C48
		public Stack<PageType> Routings { get; private set; } = new Stack<PageType>();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006A51 File Offset: 0x00004C51
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006A59 File Offset: 0x00004C59
		public Dictionary<PageType, INavigationPage> PageMapping { get; private set; } = new Dictionary<PageType, INavigationPage>();

		// Token: 0x0600011E RID: 286 RVA: 0x00006A64 File Offset: 0x00004C64
		public void Init(Dictionary<PageType, INavigationPage> mapping)
		{
			SpeechEvaluatorWin speechEvaluatorWin = Application.Current.Windows.OfType<SpeechEvaluatorWin>().FirstOrDefault<SpeechEvaluatorWin>();
			speechEvaluatorWin.Closing -= this.Win_Closing;
			speechEvaluatorWin.Closing += this.Win_Closing;
			this.Clear();
			this.PageMapping = mapping;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public void NavigateTo(PageType pageType, NavigationParas paras = null, NavigationDirection direction = NavigationDirection.Forward)
		{
			if (direction != NavigationDirection.Backward)
			{
				if (direction == NavigationDirection.Forward)
				{
					this.Routings.Push(pageType);
				}
			}
			else
			{
				while (this.Routings.Count > 0 && this.Routings.Peek() != pageType)
				{
					this.Routings.Pop();
				}
				if (this.Routings.Count <= 0)
				{
					AudioPlayer.Instance.ClosePlayer();
					this.Close(pageType);
					return;
				}
			}
			AudioPlayer.Instance.ClosePlayer();
			foreach (KeyValuePair<PageType, INavigationPage> keyValuePair in this.PageMapping)
			{
				if (keyValuePair.Key == pageType)
				{
					AudioPlayer.Instance.InitPlayer();
					keyValuePair.Value.BringFront();
					keyValuePair.Value.Init(paras);
				}
				else
				{
					keyValuePair.Value.BringBack();
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public void Close(PageType pageType)
		{
			SpeechEvaluatorWin speechEvaluatorWin = Application.Current.Windows.OfType<SpeechEvaluatorWin>().FirstOrDefault<SpeechEvaluatorWin>();
			if (speechEvaluatorWin == null)
			{
				return;
			}
			speechEvaluatorWin.Close();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006BC8 File Offset: 0x00004DC8
		private void Clear()
		{
			this.PageMapping.Clear();
			this.Routings.Clear();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006BE0 File Offset: 0x00004DE0
		private void CloseCurrentView()
		{
			if (this.Routings.Count <= 0)
			{
				return;
			}
			PageType pageType = this.Routings.Peek();
			foreach (KeyValuePair<PageType, INavigationPage> keyValuePair in this.PageMapping)
			{
				if (keyValuePair.Key == pageType)
				{
					keyValuePair.Value.Close(true);
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006C60 File Offset: 0x00004E60
		private void Win_Closing(object sender, CancelEventArgs e)
		{
			(sender as Window).Closing -= this.Win_Closing;
			this.CloseCurrentView();
			this.Clear();
		}

		// Token: 0x0400007E RID: 126
		public static readonly PageNavigation Instance = new PageNavigation();
	}
}
