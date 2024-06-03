using System;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000079 RID: 121
	public class XDViewModel : BaseModel
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0002A975 File Offset: 0x00028B75
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0002A97D File Offset: 0x00028B7D
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002A9BB File Offset: 0x00028BBB
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0002A9C3 File Offset: 0x00028BC3
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

		// Token: 0x04000463 RID: 1123
		private string mId;

		// Token: 0x04000464 RID: 1124
		private string mValue;
	}
}
