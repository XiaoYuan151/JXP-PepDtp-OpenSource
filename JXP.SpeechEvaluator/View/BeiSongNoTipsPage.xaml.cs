using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Xunfei;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x0200000B RID: 11
	public partial class BeiSongNoTipsPage : UserControl, INavigationPage
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003CB6 File Offset: 0x00001EB6
		public BeiSongNoTipsPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public void Init(NavigationParas paras)
		{
			BeiSongNoTipsPage.<Init>d__5 <Init>d__;
			<Init>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Init>d__.<>4__this = this;
			<Init>d__.paras = paras;
			<Init>d__.<>1__state = -1;
			<Init>d__.<>t__builder.Start<BeiSongNoTipsPage.<Init>d__5>(ref <Init>d__);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D04 File Offset: 0x00001F04
		private void InitData()
		{
			this.mRecordingFolder = EvaluatorContext.CreateRecordingDir("");
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.mNaviParas.Sentences.Count; i++)
			{
				stringBuilder.Append(this.mNaviParas.Sentences[i].Content);
				if (i == 0)
				{
					stringBuilder.Append(".");
				}
			}
			this.mCurSentence = new Sentence();
			this.mCurSentence.Content = stringBuilder.ToString();
			this.mNaviParas.Sentences[0].Voice = Path.Combine(this.mRecordingFolder, this.mNaviParas.Sentences[0].Id + ".wav");
			this.mCurSentence.Voice = this.mNaviParas.Sentences[0].Voice;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003DEC File Offset: 0x00001FEC
		private Task DoXfSpeechTest()
		{
			BeiSongNoTipsPage.<DoXfSpeechTest>d__7 <DoXfSpeechTest>d__;
			<DoXfSpeechTest>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<DoXfSpeechTest>d__.<>4__this = this;
			<DoXfSpeechTest>d__.<>1__state = -1;
			<DoXfSpeechTest>d__.<>t__builder.Start<BeiSongNoTipsPage.<DoXfSpeechTest>d__7>(ref <DoXfSpeechTest>d__);
			return <DoXfSpeechTest>d__.<>t__builder.Task;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E2F File Offset: 0x0000202F
		private void StopXfSpeechTest(XfEngineHelper engine, bool fireCompletedEvent = false)
		{
			if (engine == null)
			{
				return;
			}
			engine.StopRecording(fireCompletedEvent);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E3C File Offset: 0x0000203C
		private void XfSpeechTestCompleted(Sentence sentence, bool isSuccess, string xfXml)
		{
			ReadChapter xfResult = null;
			if (isSuccess)
			{
				xfResult = EvaluatorContext.ConvertXmlToObject(xfXml);
			}
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mXfEngine.WhenResultReturnHandler = null;
				double totalScore = 0.0;
				if (xfResult != null)
				{
					totalScore = xfResult.TotalScore;
					this.AnalysisScore(xfResult);
				}
				ResultPageParas resultPageParas = new ResultPageParas();
				resultPageParas.Sentences = this.mNaviParas.Sentences.Select(delegate(Sentence item)
				{
					item = item.Clone();
					item.IsSelected = false;
					item.ShowScore = false;
					return item;
				}).ToList<Sentence>();
				resultPageParas.NoDetailScore = true;
				resultPageParas.TotalScore = totalScore;
				resultPageParas.RecordingFolder = this.mRecordingFolder;
				ResultPageParas paras = resultPageParas;
				PageNavigation.Instance.NavigateTo(PageType.Result, paras, NavigationDirection.Forward);
			}), new object[0]);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E8A File Offset: 0x0000208A
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			this.Close(false);
			PageNavigation.Instance.NavigateTo(PageType.Selection, null, NavigationDirection.Backward);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.BeiSongNoTips);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003EAD File Offset: 0x000020AD
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			this.StopXfSpeechTest(this.mXfEngine, true);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003EBC File Offset: 0x000020BC
		private void AnalysisScore(ReadChapter xfResult)
		{
			if (xfResult.IsRejected)
			{
				using (List<Sentence>.Enumerator enumerator = this.mNaviParas.Sentences.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Sentence sentence = enumerator.Current;
						ReadChapter xfResult2 = new ReadChapter
						{
							Sentences = new List<XSentence>(),
							IsRejected = true,
							TotalScore = 0.0
						};
						sentence.XfResult = xfResult2;
					}
					return;
				}
			}
			List<Word> list = new List<Word>();
			foreach (XSentence xsentence in xfResult.Sentences)
			{
				foreach (Word word in xsentence.Words)
				{
					if (word.TotalScore >= 0.0)
					{
						list.Add(word);
					}
				}
			}
			foreach (Sentence sentence2 in this.mNaviParas.Sentences)
			{
				ReadChapter readChapter = new ReadChapter
				{
					Sentences = new List<XSentence>()
				};
				readChapter.Sentences.Add(new XSentence
				{
					Words = new List<Word>()
				});
				int startIndex = 0;
				foreach (Word word2 in list)
				{
					if (!word2.IsUsed)
					{
						int num = sentence2.Content.IndexOf(word2.Content, startIndex, StringComparison.CurrentCultureIgnoreCase);
						if (num < 0)
						{
							break;
						}
						readChapter.Sentences[0].Words.Add(word2);
						startIndex = num + word2.Content.Length;
						word2.IsUsed = true;
					}
				}
				sentence2.XfResult = readChapter;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000040F0 File Offset: 0x000022F0
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000040F9 File Offset: 0x000022F9
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004102 File Offset: 0x00002302
		public void Close(bool dispose = false)
		{
			this.StopXfSpeechTest(this.mXfEngine, false);
		}

		// Token: 0x0400002A RID: 42
		private string mRecordingFolder;

		// Token: 0x0400002B RID: 43
		private XfEngineHelper mXfEngine;

		// Token: 0x0400002C RID: 44
		private Sentence mCurSentence;

		// Token: 0x0400002D RID: 45
		private BeiSongPageParas mNaviParas;
	}
}
