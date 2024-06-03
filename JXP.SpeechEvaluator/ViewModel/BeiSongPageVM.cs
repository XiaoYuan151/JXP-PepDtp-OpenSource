using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.View.CustomControl;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002C RID: 44
	public class BeiSongPageVM : NotificationObject
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007488 File Offset: 0x00005688
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000074AD File Offset: 0x000056AD
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000074EB File Offset: 0x000056EB
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000074F3 File Offset: 0x000056F3
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007531 File Offset: 0x00005731
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00007539 File Offset: 0x00005739
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007577 File Offset: 0x00005777
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000757F File Offset: 0x0000577F
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000075BD File Offset: 0x000057BD
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000075C5 File Offset: 0x000057C5
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007603 File Offset: 0x00005803
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000760B File Offset: 0x0000580B
		public EvaluatorButtonState BtnBeiSong
		{
			get
			{
				return this.mBtnBeiSong;
			}
			set
			{
				this.mBtnBeiSong = value;
				base.RaisePropertyChanged<EvaluatorButtonState>(() => this.BtnBeiSong);
			}
		}

		// Token: 0x040000A5 RID: 165
		private ObservableCollection<Sentence> mSentences;

		// Token: 0x040000A6 RID: 166
		private bool mContentWithImgVisible;

		// Token: 0x040000A7 RID: 167
		private bool mContentNoImgVisible;

		// Token: 0x040000A8 RID: 168
		private bool mDisableSentenceUi;

		// Token: 0x040000A9 RID: 169
		private string mSideImg;

		// Token: 0x040000AA RID: 170
		private EvaluatorButtonState mBtnBeiSong;
	}
}
