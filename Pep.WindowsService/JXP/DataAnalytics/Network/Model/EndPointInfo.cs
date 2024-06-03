using System;
using System.Runtime.Serialization;

namespace JXP.DataAnalytics.Network.Model
{
	// Token: 0x02000041 RID: 65
	[DataContract]
	internal class EndPointInfo
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007133 File Offset: 0x00005333
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000713B File Offset: 0x0000533B
		[DataMember(Name = "url")]
		internal string Url { get; set; }
	}
}
