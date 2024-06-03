using System;
using System.Runtime.Serialization;

namespace JXP.DataAnalytics.Network.Model
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	internal class TokenModel : BaseModel
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000714C File Offset: 0x0000534C
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007154 File Offset: 0x00005354
		[DataMember(Name = "result")]
		internal TokenInfo Result { get; set; }
	}
}
