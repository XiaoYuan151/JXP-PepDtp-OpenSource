using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.View.CustomControl;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002E RID: 46
	public class GenDuZiDuPageVM : NotificationObject
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007844 File Offset: 0x00005A44
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00007869 File Offset: 0x00005A69
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000078A7 File Offset: 0x00005AA7
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000078AF File Offset: 0x00005AAF
		public bool ContentWithImgVisible
		{
			get
			{
				return this.mContentWithImgVisible;
			}
			set
			{
				this.mContentWithImgVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ContentWithImgVisible);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000078ED File Offset: 0x00005AED
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000078F5 File Offset: 0x00005AF5
		public bool ContentNoImgVisible
		{
			get
			{
				return this.mContentNoImgVisible;
			}
			set
			{
				this.mContentNoImgVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ContentNoImgVisible);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007933 File Offset: 0x00005B33
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000793B File Offset: 0x00005B3B
		public string SideImg
		{
			get
			{
				return this.mSideImg;
			}
			set
			{
				this.mSideImg = value;
				base.RaisePropertyChanged<string>(() => this.SideImg);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007979 File Offset: 0x00005B79
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00007981 File Offset: 0x00005B81
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000079BF File Offset: 0x00005BBF
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000079C7 File Offset: 0x00005BC7
		public EvaluatorButtonState BtnGendu
		{
			get
			{
				return this.mBtnGendu;
			}
			set
			{
				this.mBtnGendu = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnGendu);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007A05 File Offset: 0x00005C05
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007A0D File Offset: 0x00005C0D
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007A4B File Offset: 0x00005C4B
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00007A53 File Offset: 0x00005C53
		public EvaluatorButtonState BtnZidu
		{
			get
			{
				return this.mBtnZidu;
			}
			set
			{
				this.mBtnZidu = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnZidu);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007A91 File Offset: 0x00005C91
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007A99 File Offset: 0x00005C99
		public bool DisableSentenceUi
		{
			get
			{
				return this.mDisableSentenceUi;
			}
			set
			{
				this.mDisableSentenceUi = value;
				base.RaisePropertyChanged<bool>(() => this.DisableSentenceUi);
			}
		}

		// Token: 0x040000AE RID: 174
		private ObservableCollection<Sentence> mSentences;

		// Token: 0x040000AF RID: 175
		private bool mContentWithImgVisible;

		// Token: 0x040000B0 RID: 176
		private bool mContentNoImgVisible;

		// Token: 0x040000B1 RID: 177
		private bool mDisableSentenceUi;

		// Token: 0x040000B2 RID: 178
		private string mSideImg;

		// Token: 0x040000B3 RID: 179
		private EvaluatorButtonState mBtnShifanyin;

		// Token: 0x040000B4 RID: 180
		private EvaluatorButtonState mBtnGendu;

		// Token: 0x040000B5 RID: 181
		private EvaluatorButtonState mBtnLuyin;

		// Token: 0x040000B6 RID: 182
		private EvaluatorButtonState mBtnZidu;
	}
}
