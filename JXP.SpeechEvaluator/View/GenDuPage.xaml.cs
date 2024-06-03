using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using JXP.Logs;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using JXP.SpeechEvaluator.View.CustomControl;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;
using JXP.Threading;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x0200000E RID: 14
	public partial class GenDuPage : UserControl, INavigationPage
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public GenDuPage()
		{
			this.InitializeComponent();
			this.mPageVM = new GenDuZiDuPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004C08 File Offset: 0x00002E08
		public void Init(NavigationParas paras)
		{
			GenDuPage.<Init>d__6 <Init>d__;
			<Init>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Init>d__.<>4__this = this;
			<Init>d__.paras = paras;
			<Init>d__.<>1__state = -1;
			<Init>d__.<>t__builder.Start<GenDuPage.<Init>d__6>(ref <Init>d__);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004C47 File Offset: 0x00002E47
		public void Init(List<Sentence> sentenceList)
		{
			this.mPageVM.Sentences = new ObservableCollection<Sentence>(sentenceList);
			this.InitButtonState(-1);
			this.InitUiState();
			this.InitData();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004C70 File Offset: 0x00002E70
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

		// Token: 0x060000A3 RID: 163 RVA: 0x00004D13 File Offset: 0x00002F13
		private void RemoveEvent()
		{
			this.listWithImg.SelectionChanged -= this.Sentence_SelectionChanged;
			this.listNoImg.SelectionChanged -= this.Sentence_SelectionChanged;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004D44 File Offset: 0x00002F44
		private void InitButtonState(int state)
		{
			switch (state)
			{
			case 0:
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Animated;
				this.mPageVM.BtnGendu = EvaluatorButtonState.Disabled;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Disabled;
				return;
			case 1:
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Default;
				this.mPageVM.BtnGendu = EvaluatorButtonState.Animated;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Disabled;
				return;
			case 2:
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Disabled;
				this.mPageVM.BtnGendu = EvaluatorButtonState.Disabled;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Animated;
				return;
			case 3:
				this.mPageVM.BtnGendu = EvaluatorButtonState.Disabled;
				return;
			default:
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Disabled;
				this.mPageVM.BtnGendu = EvaluatorButtonState.Disabled;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Disabled;
				return;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004E0C File Offset: 0x0000300C
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

		// Token: 0x060000A6 RID: 166 RVA: 0x00004EC0 File Offset: 0x000030C0
		private void DoXfSpeechTest()
		{
			try
			{
				Sentence sentence = this.mCurSentence;
				if (sentence != null)
				{
					XfEngineHelper tmpEngine = this.mXfEngine;
					this.StopXfSpeechTest(this.mXfEngine, false);
					this.mXfEngine = new XfEngineHelper(sentence);
					this.mXfEngine.WhenResultReturnHandler = new Action<Sentence, bool, string>(this.XfSpeechTestCompleted);
					this.InitButtonState(1);
					TaskEx.Run(delegate()
					{
						while (tmpEngine != null && !tmpEngine.isSessionEnd)
						{
							Thread.Sleep(100);
						}
						this.mXfEngine.StartRecording("ise", "read_sentence", "en_vip", false);
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004F5C File Offset: 0x0000315C
		private void StopXfSpeechTest(XfEngineHelper engine, bool fireCompletedEvent = false)
		{
			if (engine == null)
			{
				return;
			}
			engine.StopRecording(fireCompletedEvent);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004F6C File Offset: 0x0000316C
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
				this.InitButtonState(2);
				AudioPlayer.Instance.Play(sentence.Voice, new Action(this.PlayNex));
			}), new object[0]);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004FB9 File Offset: 0x000031B9
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

		// Token: 0x060000AA RID: 170 RVA: 0x00004FDC File Offset: 0x000031DC
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

		// Token: 0x060000AB RID: 171 RVA: 0x0000508C File Offset: 0x0000328C
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			object tag = (sender as Button).Tag;
			string a = (tag != null) ? tag.ToString() : null;
			if (a == "0")
			{
				foreach (Sentence sentence in this.mPageVM.Sentences)
				{
					sentence.IsSelected = false;
				}
				this.mCurSentence.IsSelected = true;
				return;
			}
			if (!(a == "1"))
			{
				return;
			}
			this.InitButtonState(3);
			this.StopXfSpeechTest(this.mXfEngine, true);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005130 File Offset: 0x00003330
		private void Sentence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			GenDuPage.<Sentence_SelectionChanged>d__18 <Sentence_SelectionChanged>d__;
			<Sentence_SelectionChanged>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Sentence_SelectionChanged>d__.<>4__this = this;
			<Sentence_SelectionChanged>d__.e = e;
			<Sentence_SelectionChanged>d__.<>1__state = -1;
			<Sentence_SelectionChanged>d__.<>t__builder.Start<GenDuPage.<Sentence_SelectionChanged>d__18>(ref <Sentence_SelectionChanged>d__);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000516F File Offset: 0x0000336F
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			this.Close(false);
			PageNavigation.Instance.NavigateTo(PageType.Index, null, NavigationDirection.Backward);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005185 File Offset: 0x00003385
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.GenDu);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005192 File Offset: 0x00003392
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000519B File Offset: 0x0000339B
		public void BringBack()
		{
			this.RemoveEvent();
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000051AA File Offset: 0x000033AA
		public void Close(bool dispose = false)
		{
			this.RemoveEvent();
			AudioPlayer.Instance.StopPlay();
			this.StopXfSpeechTest(this.mXfEngine, false);
		}

		// Token: 0x04000040 RID: 64
		private GenDuZiDuPageVM mPageVM;

		// Token: 0x04000041 RID: 65
		private string mRecordingFolder;

		// Token: 0x04000042 RID: 66
		private Sentence mCurSentence;

		// Token: 0x04000043 RID: 67
		private XfEngineHelper mXfEngine;

		// Token: 0x04000044 RID: 68
		private GenDuPageParas mNaviParas;
	}
}
