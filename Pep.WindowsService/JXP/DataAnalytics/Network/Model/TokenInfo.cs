using System;
using System.Runtime.Serialization;

namespace JXP.DataAnalytics.Network.Model
{
	// Token: 0x02000043 RID: 67
	[DataContract]
	internal class TokenInfo
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007165 File Offset: 0x00005365
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000716D File Offset: 0x0000536D
		[DataMember(Name = "active_time")]
		internal string ActiveTime { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007176 File Offset: 0x00005376
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000717E File Offset: 0x0000537E
		[DataMember(Name = "access_token")]
		internal string AccessToken { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007187 File Offset: 0x00005387
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000718F File Offset: 0x0000538F
		[DataMember(Name = "url")]
		internal string Url { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007198 File Offset: 0x00005398
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000071A0 File Offset: 0x000053A0
		[DataMember(Name = "uploadInterval")]
		internal string UploadInterval { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000071A9 File Offset: 0x000053A9
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000071B1 File Offset: 0x000053B1
		[DataMember(Name = "acks")]
		internal string Acks { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000071BA File Offset: 0x000053BA
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000071C2 File Offset: 0x000053C2
		[DataMember(Name = "batchSize")]
		internal string SingleFileSize { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000071CB File Offset: 0x000053CB
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000071D3 File Offset: 0x000053D3
		[DataMember(Name = "timeout")]
		internal string Timeout { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000071DC File Offset: 0x000053DC
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000071E4 File Offset: 0x000053E4
		[DataMember(Name = "batchCount")]
		internal string MaxRecordCountWhenPost { get; set; }
	}
}
