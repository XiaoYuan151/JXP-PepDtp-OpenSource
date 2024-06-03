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
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using JXP.SpeechEvaluator.View.CustomControl;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x02000013 RID: 19
	public partial class ZiDuPage : UserControl, INavigationPage
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005D1C File Offset: 0x00003F1C
		public ZiDuPage()
		{
			this.InitializeComponent();
			this.mPageVM = new GenDuZiDuPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005D41 File Offset: 0x00003F41
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as ZiDuPageParas);
			this.mPageVM.Sentences = new ObservableCollection<Sentence>(this.mNaviParas.Sentences);
			this.InitButtonState(0);
			this.InitUiState();
			this.InitData();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005D84 File Offset: 0x00003F84
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

		// Token: 0x060000EA RID: 234 RVA: 0x00005E27 File Offset: 0x00004027
		private void RemoveEvent()
		{
			this.listWithImg.SelectionChanged -= this.Sentence_SelectionChanged;
			this.listNoImg.SelectionChanged -= this.Sentence_SelectionChanged;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005E57 File Offset: 0x00004057
		private void InitButtonState(int state)
		{
			if (state == 0)
			{
				this.mPageVM.BtnZidu = EvaluatorButtonState.Animated;
				return;
			}
			if (state != 1)
			{
				return;
			}
			this.mPageVM.BtnZidu = EvaluatorButtonState.Disabled;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005E7C File Offset: 0x0000407C
		private void InitData()
		{
			this.mRecordingFolder = EvaluatorContext.CreateRecordingDir("");
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.Hidden = string.Empty;
				sentence.Voice = System.IO.Path.Combine(this.mRecordingFolder, sentence.Id + ".wav");
			}
			if (this.mPageVM.Sentences.Count > 0)
			{
				this.mPageVM.Sentences[0].IsSelected = true;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005F30 File Offset: 0x00004130
		private Task DoXfSpeechTest()
		{
			ZiDuPage.<DoXfSpeechTest>d__11 <DoXfSpeechTest>d__;
			<DoXfSpeechTest>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<DoXfSpeechTest>d__.<>4__this = this;
			<DoXfSpeechTest>d__.<>1__state = -1;
			<DoXfSpeechTest>d__.<>t__builder.Start<ZiDuPage.<DoXfSpeechTest>d__11>(ref <DoXfSpeechTest>d__);
			return <DoXfSpeechTest>d__.<>t__builder.Task;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005F73 File Offset: 0x00004173
		private void StopXfSpeechTest(XfEngineHelper engine, bool fireCompletedEvent = false)
		{
			if (engine == null)
			{
				return;
			}
			engine.StopRecording(fireCompletedEvent);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005F80 File Offset: 0x00004180
		private void XfSpeechTestCompleted(Sentence sentence, bool isSuccess, string xfXml)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mXfEngine.WhenResultReturnHandler = null;
				if (isSuccess)
				{
					sentence.XfResult = EvaluatorContext.ConvertXmlToObject(xfXml);
					sentence.ShowScore = true;
				}
				this.PlayNex();
			}), new object[0]);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005FCD File Offset: 0x000041CD
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
					return item;
				}).ToList<Sentence>();
				resultPageParas.RecordingFolder = this.mRecordingFolder;
				ResultPageParas paras = resultPageParas;
				PageNavigation.Instance.NavigateTo(PageType.Result, paras, NavigationDirection.Forward);
			}), new object[0]);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005FF0 File Offset: 0x000041F0
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

		// Token: 0x060000F2 RID: 242 RVA: 0x000060A0 File Offset: 0x000042A0
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			this.InitButtonState(1);
			this.StopXfSpeechTest(this.mXfEngine, true);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000060B8 File Offset: 0x000042B8
		private void Sentence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ZiDuPage.<Sentence_SelectionChanged>d__17 <Sentence_SelectionChanged>d__;
			<Sentence_SelectionChanged>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Sentence_SelectionChanged>d__.<>4__this = this;
			<Sentence_SelectionChanged>d__.e = e;
			<Sentence_SelectionChanged>d__.<>1__state = -1;
			<Sentence_SelectionChanged>d__.<>t__builder.Start<ZiDuPage.<Sentence_SelectionChanged>d__17>(ref <Sentence_SelectionChanged>d__);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000060F7 File Offset: 0x000042F7
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			this.Close(false);
			PageNavigation.Instance.NavigateTo(PageType.Selection, null, NavigationDirection.Backward);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000610D File Offset: 0x0000430D
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.ZiDu);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000611A File Offset: 0x0000431A
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006123 File Offset: 0x00004323
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000612C File Offset: 0x0000432C
		public void Close(bool dispose = false)
		{
			this.RemoveEvent();
			this.StopXfSpeechTest(this.mXfEngine, false);
		}

		// Token: 0x04000060 RID: 96
		private GenDuZiDuPageVM mPageVM;

		// Token: 0x04000061 RID: 97
		private string mRecordingFolder;

		// Token: 0x04000062 RID: 98
		private Sentence mCurSentence;

		// Token: 0x04000063 RID: 99
		private XfEngineHelper mXfEngine;

		// Token: 0x04000064 RID: 100
		private ZiDuPageParas mNaviParas;
	}
}
