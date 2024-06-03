using System;
using System.Runtime.Serialization;

namespace JXP.DataAnalytics.Network.Model
{
	// Token: 0x0200003F RID: 63
	[DataContract]
	internal class BaseModel
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000070F0 File Offset: 0x000052F0
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000070F8 File Offset: 0x000052F8
		[DataMember(Name = "errmsg")]
		internal string ErrMsg { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007101 File Offset: 0x00005301
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00007109 File Offset: 0x00005309
		[DataMember(Name = "errcode")]
		internal string ErrCode { get; set; }
	}
}
