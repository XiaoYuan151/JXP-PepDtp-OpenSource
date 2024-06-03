using System;
using JXP.SpeechEvaluator.Model.Xunfei;
using JXP.SpeechEvaluator.Utility;
using Microsoft.Practices.Prism.ViewModel;
using Newtonsoft.Json;

namespace JXP.SpeechEvaluator.Model
{
	// Token: 0x02000050 RID: 80
	public class Sentence : NotificationObject
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000ACAA File Offset: 0x00008EAA
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		public string Id { get; set; } = Guid.NewGuid().ToString("N");

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000ACBB File Offset: 0x00008EBB
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		[JsonProperty("index")]
		public string Index
		{
			get
			{
				return this.mIndex;
			}
			set
			{
				this.mIndex = value;
				base.RaisePropertyChanged<string>(() => this.Index);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000AD01 File Offset: 0x00008F01
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000AD09 File Offset: 0x00008F09
		[JsonProperty("role")]
		public string Role
		{
			get
			{
				return this.mRole;
			}
			set
			{
				this.mRole = value;
				base.RaisePropertyChanged<string>(() => this.Role);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AD47 File Offset: 0x00008F47
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AD4F File Offset: 0x00008F4F
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AD8D File Offset: 0x00008F8D
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AD98 File Offset: 0x00008F98
		[JsonProperty("content")]
		public string Content
		{
			get
			{
				return this.mContent;
			}
			set
			{
				if (value != null)
				{
					value = value.Replace("’", "'").Replace("，", ",").Replace("“", "\"").Replace("”", "\"").Replace("—", "-");
				}
				this.mContent = value;
				base.RaisePropertyChanged<string>(() => this.Content);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AE32 File Offset: 0x00009032
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000AE3A File Offset: 0x0000903A
		[JsonProperty("number_replace")]
		public string NumReplace
		{
			get
			{
				return this.mNumReplace;
			}
			set
			{
				this.mNumReplace = value;
				base.RaisePropertyChanged<string>(() => this.NumReplace);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000AE78 File Offset: 0x00009078
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000AE80 File Offset: 0x00009080
		[JsonProperty("hidden")]
		public string Hidden
		{
			get
			{
				return this.mHidden;
			}
			set
			{
				this.mHidden = value;
				base.RaisePropertyChanged<string>(() => this.Hidden);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000AEBE File Offset: 0x000090BE
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000AEC6 File Offset: 0x000090C6
		[JsonProperty("italic")]
		public string Italic
		{
			get
			{
				return this.mItalic;
			}
			set
			{
				this.mItalic = value;
				base.RaisePropertyChanged<string>(() => this.Italic);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000AF04 File Offset: 0x00009104
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000AF0C File Offset: 0x0000910C
		[JsonProperty("image")]
		public string Image
		{
			get
			{
				return this.mImage;
			}
			set
			{
				this.mImage = value;
				base.RaisePropertyChanged<string>(() => this.Image);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000AF4A File Offset: 0x0000914A
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000AF52 File Offset: 0x00009152
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000AF90 File Offset: 0x00009190
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000AF98 File Offset: 0x00009198
		public ReadChapter XfResult
		{
			get
			{
				return this.mReadChapter;
			}
			set
			{
				this.mReadChapter = value;
				if (value == null || value.IsRejected)
				{
					this.Score = 0;
				}
				else
				{
					this.Score = EvaluatorContext.GetScore(value.TotalScore);
				}
				base.RaisePropertyChanged<ReadChapter>(() => this.XfResult);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B006 File Offset: 0x00009206
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000B00E File Offset: 0x0000920E
		public bool IsSelected
		{
			get
			{
				return this.mIsSelected;
			}
			set
			{
				this.mIsSelected = value;
				base.RaisePropertyChanged<bool>(() => this.IsSelected);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B04C File Offset: 0x0000924C
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B054 File Offset: 0x00009254
		public int Score
		{
			get
			{
				return this.mScore;
			}
			set
			{
				this.mScore = value;
				base.RaisePropertyChanged<int>(() => this.Score);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B092 File Offset: 0x00009292
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000B09A File Offset: 0x0000929A
		public bool ShowScore
		{
			get
			{
				return this.mShowScore;
			}
			set
			{
				this.mShowScore = value;
				base.RaisePropertyChanged<bool>(() => this.ShowScore);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B0D8 File Offset: 0x000092D8
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public string Voice { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B0E9 File Offset: 0x000092E9
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B0F1 File Offset: 0x000092F1
		public RoleAlignment RoleAlignment { get; set; }

		// Token: 0x060002BD RID: 701 RVA: 0x0000B0FA File Offset: 0x000092FA
		public Sentence Clone()
		{
			return base.MemberwiseClone() as Sentence;
		}

		// Token: 0x040001CD RID: 461
		private string mIndex;

		// Token: 0x040001CE RID: 462
		private string mRole;

		// Token: 0x040001CF RID: 463
		private string mRoleImg;

		// Token: 0x040001D0 RID: 464
		private string mContent;

		// Token: 0x040001D1 RID: 465
		private string mNumReplace;

		// Token: 0x040001D2 RID: 466
		private string mHidden;

		// Token: 0x040001D3 RID: 467
		private string mItalic;

		// Token: 0x040001D4 RID: 468
		private string mImage;

		// Token: 0x040001D5 RID: 469
		private string mAudio;

		// Token: 0x040001D6 RID: 470
		private bool mIsSelected;

		// Token: 0x040001D7 RID: 471
		private int mScore;

		// Token: 0x040001D8 RID: 472
		private bool mShowScore;

		// Token: 0x040001D9 RID: 473
		private ReadChapter mReadChapter;
	}
}
