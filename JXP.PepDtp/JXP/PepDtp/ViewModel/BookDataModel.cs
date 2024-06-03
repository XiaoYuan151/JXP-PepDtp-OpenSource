using System;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000063 RID: 99
	public class BookDataModel : BaseModel
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00024160 File Offset: 0x00022360
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00024168 File Offset: 0x00022368
		public string Tbid
		{
			get
			{
				return this.mTbid;
			}
			set
			{
				this.mTbid = value;
				base.OnPropertyChanged<string>(() => this.Tbid);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000241A6 File Offset: 0x000223A6
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x000241AE File Offset: 0x000223AE
		public string ThumbUrl
		{
			get
			{
				return this.mThumbUrl;
			}
			set
			{
				this.mThumbUrl = value;
				base.OnPropertyChanged<string>(() => this.ThumbUrl);
			}
		}

		// Token: 0x04000398 RID: 920
		private string mTbid;

		// Token: 0x04000399 RID: 921
		private string mThumbUrl;
	}
}
