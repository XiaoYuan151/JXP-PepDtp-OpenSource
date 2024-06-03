using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.View.CustomControl;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002A RID: 42
	public class DuiHuaPageVM : NotificationObject
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000072DC File Offset: 0x000054DC
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007301 File Offset: 0x00005501
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000733F File Offset: 0x0000553F
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007347 File Offset: 0x00005547
		public EvaluatorButtonState BtnDuihua
		{
			get
			{
				return this.mBtnDuihua;
			}
			set
			{
				this.mBtnDuihua = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnDuihua);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007385 File Offset: 0x00005585
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000738D File Offset: 0x0000558D
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

		// Token: 0x040000A0 RID: 160
		private ObservableCollection<Sentence> mSentences;

		// Token: 0x040000A1 RID: 161
		private EvaluatorButtonState mBtnDuihua;

		// Token: 0x040000A2 RID: 162
		private bool mDisableSentenceUi;
	}
}
