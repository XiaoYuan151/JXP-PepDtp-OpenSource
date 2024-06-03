using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model.Http;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.Model
{
	// Token: 0x0200004C RID: 76
	public class Speech : NotificationObject
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A9BB File Offset: 0x00008BBB
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000A9C3 File Offset: 0x00008BC3
		public string Caption
		{
			get
			{
				return this.mCaption;
			}
			set
			{
				this.mCaption = value;
				base.RaisePropertyChanged<string>(() => this.Caption);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AA01 File Offset: 0x00008C01
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000AA09 File Offset: 0x00008C09
		public bool IsExpanded
		{
			get
			{
				return this.mIsExpanded;
			}
			set
			{
				this.mIsExpanded = value;
				base.RaisePropertyChanged<bool>(() => this.IsExpanded);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000AA47 File Offset: 0x00008C47
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000AA4F File Offset: 0x00008C4F
		public int Level
		{
			get
			{
				return this.mLevel;
			}
			set
			{
				this.mLevel = value;
				base.RaisePropertyChanged<int>(() => this.Level);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000AA90 File Offset: 0x00008C90
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		public ObservableCollection<Speech> Speeches
		{
			get
			{
				return this.mSpeeches = (this.mSpeeches ?? new ObservableCollection<Speech>());
			}
			set
			{
				this.mSpeeches = value;
				base.RaisePropertyChanged<ObservableCollection<Speech>>(() => this.Speeches);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AAF3 File Offset: 0x00008CF3
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000AAFB File Offset: 0x00008CFB
		public VoiceRes VoiceRes { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000AB04 File Offset: 0x00008D04
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public SpeechTestType Type { get; set; }

		// Token: 0x0600028F RID: 655 RVA: 0x0000AB15 File Offset: 0x00008D15
		public Speech Clone()
		{
			return base.MemberwiseClone() as Speech;
		}

		// Token: 0x040001BB RID: 443
		private string mCaption;

		// Token: 0x040001BC RID: 444
		private int mLevel = -1;

		// Token: 0x040001BD RID: 445
		private bool mIsExpanded;

		// Token: 0x040001BE RID: 446
		private ObservableCollection<Speech> mSpeeches;
	}
}
