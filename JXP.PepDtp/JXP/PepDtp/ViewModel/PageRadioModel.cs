using System;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000062 RID: 98
	public class PageRadioModel : BaseModel
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00024074 File Offset: 0x00022274
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0002407C File Offset: 0x0002227C
		public string Text
		{
			get
			{
				return this.mText;
			}
			set
			{
				this.mText = value;
				base.OnPropertyChanged<string>(() => this.Text);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000240BA File Offset: 0x000222BA
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x000240C2 File Offset: 0x000222C2
		public bool Selected
		{
			get
			{
				return this.mSelected;
			}
			set
			{
				this.mSelected = value;
				base.OnPropertyChanged<bool>(() => this.Selected);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00024100 File Offset: 0x00022300
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00024108 File Offset: 0x00022308
		public int Index
		{
			get
			{
				return this.mIndex;
			}
			set
			{
				this.mIndex = value;
				base.OnPropertyChanged<int>(() => this.Index);
			}
		}

		// Token: 0x04000395 RID: 917
		private bool mSelected;

		// Token: 0x04000396 RID: 918
		private int mIndex = -1;

		// Token: 0x04000397 RID: 919
		private string mText = string.Empty;
	}
}
