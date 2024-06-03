using System;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200007A RID: 122
	public class XKViewModel : BaseModel
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002AA01 File Offset: 0x00028C01
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0002AA09 File Offset: 0x00028C09
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002AA47 File Offset: 0x00028C47
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0002AA4F File Offset: 0x00028C4F
		public string Id
		{
			get
			{
				return this.mId;
			}
			set
			{
				this.mId = value;
				base.OnPropertyChanged<string>(() => this.Id);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002AA8D File Offset: 0x00028C8D
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0002AA95 File Offset: 0x00028C95
		public string Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
				base.OnPropertyChanged<string>(() => this.Value);
			}
		}

		// Token: 0x04000465 RID: 1125
		private bool mSelected;

		// Token: 0x04000466 RID: 1126
		private string mId;

		// Token: 0x04000467 RID: 1127
		private string mValue;
	}
}
