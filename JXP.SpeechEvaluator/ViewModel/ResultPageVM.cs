using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.View.CustomControl;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002F RID: 47
	public class ResultPageVM : NotificationObject
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007ADF File Offset: 0x00005CDF
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007AE7 File Offset: 0x00005CE7
		public string RoleImg
		{
			get
			{
				return this.mRoleImg;
			}
			set
			{
				this.mRoleImg = value;
				base.RaisePropertyChanged<string>(() => this.RoleImg);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007B28 File Offset: 0x00005D28
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007B4D File Offset: 0x00005D4D
		public ObservableCollection<Sentence> Sentences
		{
			get
			{
				return this.mSentences = (this.mSentences ?? new ObservableCollection<Sentence>());
			}
			set
			{
				this.mSentences = value;
				base.RaisePropertyChanged<ObservableCollection<Sentence>>(() => this.Sentences);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007B8B File Offset: 0x00005D8B
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007B93 File Offset: 0x00005D93
		public EvaluatorButtonState BtnShifanyin
		{
			get
			{
				return this.mBtnShifanyin;
			}
			set
			{
				this.mBtnShifanyin = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnShifanyin);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007BD1 File Offset: 0x00005DD1
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00007BD9 File Offset: 0x00005DD9
		public EvaluatorButtonState BtnLuyin
		{
			get
			{
				return this.mBtnLuyin;
			}
			set
			{
				this.mBtnLuyin = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnLuyin);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007C17 File Offset: 0x00005E17
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00007C1F File Offset: 0x00005E1F
		public string Score
		{
			get
			{
				return this.mScore;
			}
			set
			{
				this.mScore = value;
				base.RaisePropertyChanged<string>(() => this.Score);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007C5D File Offset: 0x00005E5D
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007C65 File Offset: 0x00005E65
		public bool ResultWithRoleVisible
		{
			get
			{
				return this.mResultWithRoleVisible;
			}
			set
			{
				this.mResultWithRoleVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ResultWithRoleVisible);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007CA3 File Offset: 0x00005EA3
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007CAB File Offset: 0x00005EAB
		public bool ResultNoRoleVisible
		{
			get
			{
				return this.mResultNoRoleVisible;
			}
			set
			{
				this.mResultNoRoleVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ResultNoRoleVisible);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007CE9 File Offset: 0x00005EE9
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007CF1 File Offset: 0x00005EF1
		public bool ScoreBtnVisible
		{
			get
			{
				return this.mScoreBtnVisible;
			}
			set
			{
				this.mScoreBtnVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ScoreBtnVisible);
			}
		}

		// Token: 0x040000B7 RID: 183
		private string mRoleImg;

		// Token: 0x040000B8 RID: 184
		private ObservableCollection<Sentence> mSentences;

		// Token: 0x040000B9 RID: 185
		private EvaluatorButtonState mBtnShifanyin;

		// Token: 0x040000BA RID: 186
		private EvaluatorButtonState mBtnLuyin;

		// Token: 0x040000BB RID: 187
		private string mScore;

		// Token: 0x040000BC RID: 188
		private bool mResultWithRoleVisible;

		// Token: 0x040000BD RID: 189
		private bool mResultNoRoleVisible;

		// Token: 0x040000BE RID: 190
		private bool mScoreBtnVisible;
	}
}
