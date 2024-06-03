using System;
using System.CodeDom.Compiler;
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
	// Token: 0x0200000D RID: 13
	public partial class DuiHuaPage : UserControl, INavigationPage
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000046A4 File Offset: 0x000028A4
		public DuiHuaPage()
		{
			this.InitializeComponent();
			this.mPageVM = new DuiHuaPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000046C9 File Offset: 0x000028C9
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as DuiHuaPageParas);
			this.mPageVM.Sentences = new ObservableCollection<Sentence>(this.mNaviParas.Sentences);
			this.InitButtonState(-1);
			this.InitData();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004703 File Offset: 0x00002903
		private void InitButtonState(int state = -1)
		{
			if (state == 0)
			{
				this.mPageVM.BtnDuihua = EvaluatorButtonState.Animated;
				return;
			}
			if (state != 1)
			{
				this.mPageVM.BtnDuihua = EvaluatorButtonState.Default;
				return;
			}
			this.mPageVM.BtnDuihua = EvaluatorButtonState.Disabled;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004734 File Offset: 0x00002934
		private void InitData()
		{
			this.mRecordingFolder = EvaluatorContext.CreateRecordingDir("");
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.Voice = Path.Combine(this.mRecordingFolder, sentence.Id + ".wav");
				sentence.Hidden = string.Empty;
			}
			if (this.mPageVM.Sentences.Count > 0)
			{
				this.mPageVM.Sentences[0].IsSelected = true;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047E8 File Offset: 0x000029E8
		private void DoXfSpeechTest()
		{
			try
			{
				Sentence sentence = this.mCurSentence;
				if (sentence != null)
				{
					if (sentence.RoleAlignment != RoleAlignment.Right)
					{
						this.PlayNex();
					}
					else
					{
						XfEngineHelper tmpEngine = this.mXfEngine;
						this.StopXfSpeechTest(this.mXfEngine, false);
						this.mXfEngine = new XfEngineHelper(sentence);
						this.mXfEngine.WhenResultReturnHandler = new Action<Sentence, bool, string>(this.XfSpeechTestCompleted);
						this.InitButtonState(0);
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
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004894 File Offset: 0x00002A94
		private void StopXfSpeechTest(XfEngineHelper engine, bool fireCompletedEvent = false)
		{
			if (engine == null)
			{
				return;
			}
			engine.StopRecording(fireCompletedEvent);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000048A4 File Offset: 0x00002AA4
		private void XfSpeechTestCompleted(Sentence sentence, bool isSuccess, string xfXml)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mXfEngine.WhenResultReturnHandler = null;
				if (isSuccess)
				{
					sentence.XfResult = EvaluatorContext.ConvertXmlToObject(xfXml);
				}
				this.InitButtonState(-1);
				this.PlayNex();
			}), new object[0]);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000048F1 File Offset: 0x00002AF1
		private void AllSentencePlayCompleted()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				ResultPageParas resultPageParas = new ResultPageParas();
				resultPageParas.Sentences = (from item in this.mPageVM.Sentences
				where item.RoleAlignment == RoleAlignment.Right
				select item).Select(delegate(Sentence item)
				{
					item = item.Clone();
					item.IsSelected = false;
					return item;
				}).ToList<Sentence>();
				resultPageParas.RecordingFolder = this.mRecordingFolder;
				resultPageParas.SelectedRoleImg = this.mNaviParas.SelectedRoleImg;
				resultPageParas.SelectedRole = this.mNaviParas.SelectedRole;
				ResultPageParas paras = resultPageParas;
				PageNavigation.Instance.NavigateTo(PageType.Result, paras, NavigationDirection.Forward);
			}), new object[0]);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004914 File Offset: 0x00002B14
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

		// Token: 0x06000094 RID: 148 RVA: 0x000049C4 File Offset: 0x00002BC4
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.mPageVM.BtnDuihua == EvaluatorButtonState.Animated)
			{
				this.StopXfSpeechTest(this.mXfEngine, true);
				this.InitButtonState(1);
				return;
			}
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.IsSelected = false;
			}
			this.mCurSentence.IsSelected = true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A44 File Offset: 0x00002C44
		private void Sentence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DuiHuaPage.<Sentence_SelectionChanged>d__15 <Sentence_SelectionChanged>d__;
			<Sentence_SelectionChanged>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Sentence_SelectionChanged>d__.<>4__this = this;
			<Sentence_SelectionChanged>d__.e = e;
			<Sentence_SelectionChanged>d__.<>1__state = -1;
			<Sentence_SelectionChanged>d__.<>t__builder.Start<DuiHuaPage.<Sentence_SelectionChanged>d__15>(ref <Sentence_SelectionChanged>d__);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004A83 File Offset: 0x00002C83
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			this.Close(false);
			PageNavigation.Instance.NavigateTo(PageType.Selection, null, NavigationDirection.Backward);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004A99 File Offset: 0x00002C99
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.DuiHua);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004AAF File Offset: 0x00002CAF
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public void Close(bool dispose = false)
		{
			AudioPlayer.Instance.StopPlay();
			this.StopXfSpeechTest(this.mXfEngine, false);
		}

		// Token: 0x0400003A RID: 58
		private DuiHuaPageVM mPageVM;

		// Token: 0x0400003B RID: 59
		private string mRecordingFolder;

		// Token: 0x0400003C RID: 60
		private Sentence mCurSentence;

		// Token: 0x0400003D RID: 61
		private XfEngineHelper mXfEngine;

		// Token: 0x0400003E RID: 62
		private DuiHuaPageParas mNaviParas;
	}
}
