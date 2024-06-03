using System;
using JXP.Models;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000068 RID: 104
	public class SelectUserRes : UserResourceModel
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0002586C File Offset: 0x00023A6C
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x00025874 File Offset: 0x00023A74
		[JsonIgnore]
		public bool ShowAdd
		{
			get
			{
				return this.mShowAdd;
			}
			set
			{
				this.mShowAdd = value;
				base.OnPropertyChanged<bool>(() => this.ShowAdd);
			}
		}

		// Token: 0x040003C9 RID: 969
		private bool mShowAdd = true;
	}
}
