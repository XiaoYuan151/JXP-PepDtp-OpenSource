using System;
using System.Runtime.Serialization;
using JXP.DataAnalytics.Platform;

namespace Pep.WindowsService.Data
{
	// Token: 0x02000020 RID: 32
	[DataContract]
	public class AnalyticsBasicData : IProductInfoProvider
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003AAF File Offset: 0x00001CAF
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003AB7 File Offset: 0x00001CB7
		[DataMember]
		public string ProductId { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003AC0 File Offset: 0x00001CC0
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003AC8 File Offset: 0x00001CC8
		[DataMember]
		public string AppKey { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003AD1 File Offset: 0x00001CD1
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003AD9 File Offset: 0x00001CD9
		[DataMember]
		public string ProductVersion { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003AE2 File Offset: 0x00001CE2
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003AEA File Offset: 0x00001CEA
		[DataMember]
		public string ActiveUser { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003AF3 File Offset: 0x00001CF3
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003AFB File Offset: 0x00001CFB
		[DataMember]
		public string Organization { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B04 File Offset: 0x00001D04
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003B0C File Offset: 0x00001D0C
		[DataMember]
		public string GroupId { get; set; }
	}
}
