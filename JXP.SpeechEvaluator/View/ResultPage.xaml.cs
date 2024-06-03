using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.Utility.Download;
using JXP.SpeechEvaluator.View.CustomControl;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;
using JXP.Threading;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x0200000F RID: 15
	public partial class ResultPage : UserControl, INavigationPage
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000052D8 File Offset: 0x000034D8
		public ResultPage()
		{
			this.InitializeComponent();
			this.mPageVM = new ResultPageVM();
			base.DataContext = this.mPageVM;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000530C File Offset: 0x0000350C
		public void Init(NavigationParas paras)
		{
			if (paras == null)
			{
				return;
			}
			this.mNaviParas = (paras as ResultPageParas);
			this.mPageVM.Sentences = new ObservableCollection<Sentence>(this.mNaviParas.Sentences);
			this.mPageVM.RoleImg = this.mNaviParas.SelectedRoleImg;
			this.InitButtonState(-1);
			this.InitUiState();
			this.InitData();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005370 File Offset: 0x00003570
		private void InitButtonState(int state)
		{
			if (state == 0)
			{
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Animated;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Default;
				return;
			}
			if (state != 1)
			{
				this.mPageVM.BtnShifanyin = EvaluatorButtonState.Default;
				this.mPageVM.BtnLuyin = EvaluatorButtonState.Default;
				return;
			}
			this.mPageVM.BtnShifanyin = EvaluatorButtonState.Default;
			this.mPageVM.BtnLuyin = EvaluatorButtonState.Animated;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000053D0 File Offset: 0x000035D0
		private void InitUiState()
		{
			this.mPageVM.ResultNoRoleVisible = false;
			this.mPageVM.ResultWithRoleVisible = false;
			if (!string.IsNullOrEmpty(this.mPageVM.RoleImg))
			{
				this.mPageVM.ResultWithRoleVisible = true;
				return;
			}
			this.mPageVM.ResultNoRoleVisible = true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005420 File Offset: 0x00003620
		private void InitData()
		{
			if (this.mNaviParas.NoDetailScore)
			{
				this.mPageVM.ScoreBtnVisible = false;
				this.mPageVM.Score = EvaluatorContext.GetScore2(this.mNaviParas.TotalScore).ToString();
				return;
			}
			List<Sentence> list = new List<Sentence>();
			foreach (Sentence sentence2 in this.mPageVM.Sentences)
			{
				list.Add(sentence2);
				sentence2.ShowScore = false;
			}
			this.mPageVM.ScoreBtnVisible = true;
			if (list.Count == 0)
			{
				this.mPageVM.Score = "0";
				return;
			}
			double realScore = (double)list.Sum((Sentence sentence) => sentence.Score) * 1.0 / (double)list.Count;
			this.mPageVM.Score = EvaluatorContext.GetScore3(realScore).ToString();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005538 File Offset: 0x00003738
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			ResultPage.<NavigationBar_Return>d__12 <NavigationBar_Return>d__;
			<NavigationBar_Return>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<NavigationBar_Return>d__.<>4__this = this;
			<NavigationBar_Return>d__.<>1__state = -1;
			<NavigationBar_Return>d__.<>t__builder.Start<ResultPage.<NavigationBar_Return>d__12>(ref <NavigationBar_Return>d__);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000556F File Offset: 0x0000376F
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.Result);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000557C File Offset: 0x0000377C
		private void AudioPlayer_PlayCompleted(object sender, EventArgs e)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000557E File Offset: 0x0000377E
		private void Sentence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005580 File Offset: 0x00003780
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			ResultPage.<EvaluatorButton_Click>d__16 <EvaluatorButton_Click>d__;
			<EvaluatorButton_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<EvaluatorButton_Click>d__.<>4__this = this;
			<EvaluatorButton_Click>d__.sender = sender;
			<EvaluatorButton_Click>d__.<>1__state = -1;
			<EvaluatorButton_Click>d__.<>t__builder.Start<ResultPage.<EvaluatorButton_Click>d__16>(ref <EvaluatorButton_Click>d__);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000055C0 File Offset: 0x000037C0
		private void PlayShifanyin(CancellationToken ct = default(CancellationToken))
		{
			Action <>9__3;
			Action <>9__4;
			this.mPlayShifanyinTask = TaskEx.Run(delegate()
			{
				using (IEnumerator<Sentence> enumerator = this.mPageVM.Sentences.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Sentence sentence = enumerator.Current;
						this.mResetEvent.WaitOne();
						if (ct.IsCancellationRequested)
						{
							ct.ThrowIfCancellationRequested();
						}
						string url = DownloadManager.Download(sentence.Audio, null, false);
						if (ct.IsCancellationRequested)
						{
							ct.ThrowIfCancellationRequested();
						}
						this.Dispatcher.BeginInvoke(new Action(delegate()
						{
							if (ct.IsCancellationRequested)
							{
								return;
							}
							this.UnSelectedOthers();
							sentence.IsSelected = true;
							AudioPlayer instance = AudioPlayer.Instance;
							string url = url;
							Action completed;
							if ((completed = <>9__3) == null)
							{
								completed = (<>9__3 = delegate()
								{
									this.mResetEvent.Set();
								});
							}
							instance.Play(url, completed);
						}), new object[0]);
						this.mResetEvent.Reset();
					}
				}
			}, ct).ContinueWith(delegate(Task preTask)
			{
				if (!preTask.IsFaulted && !preTask.IsCanceled)
				{
					Dispatcher dispatcher = this.Dispatcher;
					Action method;
					if ((method = <>9__4) == null)
					{
						method = (<>9__4 = delegate()
						{
							this.InitButtonState(-1);
						});
					}
					dispatcher.Invoke(method, new object[0]);
				}
			});
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005610 File Offset: 0x00003810
		private void PlayRecording(CancellationToken ct = default(CancellationToken))
		{
			Action <>9__2;
			Action <>9__4;
			Action <>9__5;
			this.mPlayRecordingTask = TaskEx.Run(delegate()
			{
				if (this.mNaviParas.NoDetailScore)
				{
					if (ct.IsCancellationRequested)
					{
						ct.ThrowIfCancellationRequested();
					}
					Dispatcher dispatcher = this.Dispatcher;
					Action method;
					if ((method = <>9__2) == null)
					{
						method = (<>9__2 = delegate()
						{
							this.UnSelectedOthers();
							AudioPlayer.Instance.PlaySync(this.mPageVM.Sentences[0].Voice);
						});
					}
					dispatcher.BeginInvoke(method, new object[0]);
					return;
				}
				using (IEnumerator<Sentence> enumerator = this.mPageVM.Sentences.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Sentence sentence = enumerator.Current;
						this.mResetEvent.WaitOne();
						if (ct.IsCancellationRequested)
						{
							ct.ThrowIfCancellationRequested();
						}
						this.Dispatcher.BeginInvoke(new Action(delegate()
						{
							this.UnSelectedOthers();
							sentence.IsSelected = true;
							AudioPlayer instance = AudioPlayer.Instance;
							string voice = sentence.Voice;
							Action completed;
							if ((completed = <>9__4) == null)
							{
								completed = (<>9__4 = delegate()
								{
									this.mResetEvent.Set();
								});
							}
							instance.Play(voice, completed);
						}), new object[0]);
						this.mResetEvent.Reset();
					}
				}
			}, ct).ContinueWith(delegate(Task preTask)
			{
				if (!preTask.IsFaulted && !preTask.IsCanceled)
				{
					Dispatcher dispatcher = this.Dispatcher;
					Action method;
					if ((method = <>9__5) == null)
					{
						method = (<>9__5 = delegate()
						{
							this.InitButtonState(-1);
						});
					}
					dispatcher.Invoke(method, new object[0]);
				}
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005660 File Offset: 0x00003860
		private void ShowScore_Click(object sender, RoutedEventArgs e)
		{
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.ShowScore = !sentence.ShowScore;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000056B8 File Offset: 0x000038B8
		private Task CancelPlay()
		{
			ResultPage.<CancelPlay>d__20 <CancelPlay>d__;
			<CancelPlay>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<CancelPlay>d__.<>4__this = this;
			<CancelPlay>d__.<>1__state = -1;
			<CancelPlay>d__.<>t__builder.Start<ResultPage.<CancelPlay>d__20>(ref <CancelPlay>d__);
			return <CancelPlay>d__.<>t__builder.Task;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000056FC File Offset: 0x000038FC
		private void UnSelectedOthers()
		{
			foreach (Sentence sentence in this.mPageVM.Sentences)
			{
				sentence.IsSelected = false;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000574C File Offset: 0x0000394C
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005755 File Offset: 0x00003955
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000575E File Offset: 0x0000395E
		public void Close(bool dispose = false)
		{
			this.CancelPlay().Wait();
		}

		// Token: 0x0400004A RID: 74
		private ResultPageVM mPageVM;

		// Token: 0x0400004B RID: 75
		private Task mPlayShifanyinTask;

		// Token: 0x0400004C RID: 76
		private CancellationTokenSource mPlayShifanyinCts;

		// Token: 0x0400004D RID: 77
		private Task mPlayRecordingTask;

		// Token: 0x0400004E RID: 78
		private CancellationTokenSource mPlayRecordingCts;

		// Token: 0x0400004F RID: 79
		private ResultPageParas mNaviParas;

		// Token: 0x04000050 RID: 80
		private ManualResetEvent mResetEvent = new ManualResetEvent(true);
	}
}
