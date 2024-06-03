using System;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005C RID: 92
	public class GradeViewModel : BaseModel
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00020E72 File Offset: 0x0001F072
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00020E7A File Offset: 0x0001F07A
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		public string Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
				base.OnPropertyChanged<string>(() => this.Id);
			}
		}

		// Token: 0x04000322 RID: 802
		private string mId;

		// Token: 0x04000323 RID: 803
		private string mValue;
	}
}
