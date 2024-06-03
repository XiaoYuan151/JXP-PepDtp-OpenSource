using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using Newtonsoft.Json;

namespace JXP.SpeechEvaluator.Model
{
	// Token: 0x0200004E RID: 78
	public class Paragraph : NotificationObject
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000AB31 File Offset: 0x00008D31
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000AB39 File Offset: 0x00008D39
		[JsonProperty("index")]
		public int Index
		{
			get
			{
				return this.mIndex;
			}
			set
			{
				this.mIndex = value;
				base.RaisePropertyChanged<int>(() => this.Index);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AB77 File Offset: 0x00008D77
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000AB7F File Offset: 0x00008D7F
		[JsonProperty("audio")]
		public string Audio
		{
			get
			{
				return this.mAudio;
			}
			set
			{
				this.mAudio = value;
				base.RaisePropertyChanged<string>(() => this.Audio);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000ABBD File Offset: 0x00008DBD
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		[JsonProperty("sentences")]
		public ObservableCollection<Sentence> Sentences
		{
			get
			{
				return this.mSentences;
			}
			set
			{
				this.mSentences = value;
				base.RaisePropertyChanged<ObservableCollection<Sentence>>(() => this.Sentences);
			}
		}

		// Token: 0x040001C8 RID: 456
		private int mIndex;

		// Token: 0x040001C9 RID: 457
		private string mAudio;

		// Token: 0x040001CA RID: 458
		private ObservableCollection<Sentence> mSentences = new ObservableCollection<Sentence>();
	}
}
