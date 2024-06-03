using System;
using System.Runtime.Serialization;

namespace JXP.DataAnalytics.Network.Model
{
	// Token: 0x02000040 RID: 64
	[DataContract]
	internal class EndPointModel : BaseModel
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000711A File Offset: 0x0000531A
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00007122 File Offset: 0x00005322
		[DataMember(Name = "result")]
		internal EndPointInfo Result { get; set; }
	}
}
