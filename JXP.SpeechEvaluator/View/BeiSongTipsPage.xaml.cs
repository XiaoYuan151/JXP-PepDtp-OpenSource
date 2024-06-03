using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Xunfei;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using JXP.SpeechEvaluator.View.CustomControl;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x0200000C RID: 12
	public partial class BeiSongTipsPage : UserControl, INavigationPage
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004168 File Offset: 0x00002368
		public BeiSongTipsPage()
		{
			this.InitializeComponent();
			this.mPageVM = new BeiSongPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000418D File Offset: 0x0000238D
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as BeiSongPageParas);
			this.mPageVM.Sentences = new ObservableCollection<Sentence>(this.mNaviParas.Sentences);
			this.InitButtonState(0);
			this.InitUiState();
			this.InitData();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000041D0 File Offset: 0x000023D0
		private void InitUiState()
		{
			this.RemoveEvent();
			this.mPageVM.ContentNoImgVisible = false;
			this.mPageVM.ContentWithImgVisible = false;
			if (this.mPageVM.Sentences.Any((Sentence item) => !string.IsNullOrEmpty(item.Image)))
			{
				this.mPageVM.ContentWithImgVisible = true;
				this.listWithImg.SelectionChanged += this.Sentence_SelectionChanged;
				return;
			}
			this.mPageVM.ContentNoImgVisible = true;
			this.listNoImg.SelectionChanged += this.Sentence_SelectionChanged;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004273 File Offset: 0x00002473
		private void RemoveEvent()
		{
			this.listWithImg.SelectionChanged -= this.Sentence_SelectionChanged;
			this.listNoImg.SelectionChanged -= this.Sentence_SelectionChanged;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000042A3 File Offset: 0x000024A3
		private void InitButtonState(int state = 0)
		{
			if (state == -1)
			{
				this.mPageVM.BtnBeiSong = EvaluatorButtonState.Disabled;
				return;
			}
			this.mPageVM.BtnBeiSong = EvaluatorButtonState.Animated;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000042C4 File Offset: 0x000024C4
		private void InitData()
		{
			this.mRecordingFolder = EvaluatorContext.CreateRecordingDir("");
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.Voice = System.IO.Path.Combine(this.mRecordingFolder, sentence.Id + ".wav");
				sentence.Hidden = "1";
			}
			if (this.mPageVM.Sentences.Count > 0)
			{
				this.mPageVM.Sentences[0].IsSelected = true;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004378 File Offset: 0x00002578
		private Task DoXfSpeechTest()
		{
			BeiSongTipsPage.<DoXfSpeechTest>d__12 <DoXfSpeechTest>d__;
			<DoXfSpeechTest>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<DoXfSpeechTest>d__.<>4__this = this;
			<DoXfSpeechTest>d__.<>1__state = -1;
			<DoXfSpeechTest>d__.<>t__builder.Start<BeiSongTipsPage.<DoXfSpeechTest>d__12>(ref <DoXfSpeechTest>d__);
			return <DoXfSpeechTest>d__.<>t__builder.Task;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000043BB File Offset: 0x000025BB
		private void StopXfSpeechTest(XfEngineHelper engine, bool fireCompletedEvent = false)
		{
			if (engine == null)
			{
				return;
			}
			engine.StopRecording(fireCompletedEvent);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000043C8 File Offset: 0x000025C8
		private void XfSpeechTestCompleted(Sentence sentence, bool isSuccess, string xfXml)
		{
			ReadChapter xfResult = null;
			if (isSuccess)
			{
				xfResult = EvaluatorContext.ConvertXmlToObject(xfXml);
				xfResult.NeedChangeColor = false;
			}
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mXfEngine.WhenResultReturnHandler = null;
				sentence.XfResult = xfResult;
				this.PlayNex();
			}), new object[0]);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000442C File Offset: 0x0000262C
		private void PlayNex()
		{
			if (this.mCurSentence == null)
			{
				return;
			}
			int num = this.mPageVM.Sentences.IndexOf(this.mCurSentence) + 1;
			if (this.mPageVM.Sentences.Count <= num)
			{
				this.AllSentencePlayCompleted();
				return;
			}
			this.mCurSentence = this.mPageVM.Sentences[num];
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.IsSelected = false;
			}
			this.mCurSentence.IsSelected = true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000044DC File Offset: 0x000026DC
		private void AllSentencePlayCompleted()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.RemoveEvent();
				ResultPageParas resultPageParas = new ResultPageParas();
				resultPageParas.Sentences = this.mPageVM.Sentences.Select(delegate(Sentence item)
				{
					item = item.Clone();
					item.IsSelected = false;
					if (item.XfResult != null)
					{
						item.XfResult.NeedChangeColor = true;
					}
					item.Hidden = string.Empty;
					return item;
				}).ToList<Sentence>();
				resultPageParas.RecordingFolder = this.mRecordingFolder;
				ResultPageParas paras = resultPageParas;
				PageNavigation.Instance.NavigateTo(PageType.Result, paras, NavigationDirection.Forward);
			}), new object[0]);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000044FC File Offset: 0x000026FC
		private void Sentence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BeiSongTipsPage.<Sentence_SelectionChanged>d__17 <Sentence_SelectionChanged>d__;
			<Sentence_SelectionChanged>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Sentence_SelectionChanged>d__.<>4__this = this;
			<Sentence_SelectionChanged>d__.e = e;
			<Sentence_SelectionChanged>d__.<>1__state = -1;
			<Sentence_SelectionChanged>d__.<>t__builder.Start<BeiSongTipsPage.<Sentence_SelectionChanged>d__17>(ref <Sentence_SelectionChanged>d__);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000453B File Offset: 0x0000273B
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			this.InitButtonState(-1);
			this.StopXfSpeechTest(this.mXfEngine, true);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004551 File Offset: 0x00002751
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			this.Close(false);
			PageNavigation.Instance.NavigateTo(PageType.Selection, null, NavigationDirection.Backward);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004567 File Offset: 0x00002767
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.BeiSongTips);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004574 File Offset: 0x00002774
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000457D File Offset: 0x0000277D
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004586 File Offset: 0x00002786
		public void Close(bool dispose = false)
		{
			this.RemoveEvent();
			this.StopXfSpeechTest(this.mXfEngine, false);
		}

		// Token: 0x04000030 RID: 48
		private BeiSongPageVM mPageVM;

		// Token: 0x04000031 RID: 49
		private string mRecordingFolder;

		// Token: 0x04000032 RID: 50
		private Sentence mCurSentence;

		// Token: 0x04000033 RID: 51
		private static readonly ReadChapter EmptyXfResult = new ReadChapter();

		// Token: 0x04000034 RID: 52
		private XfEngineHelper mXfEngine;

		// Token: 0x04000035 RID: 53
		private BeiSongPageParas mNaviParas;
	}
}
