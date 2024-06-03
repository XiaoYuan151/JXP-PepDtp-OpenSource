using System;
using System.Runtime.Serialization;

namespace Pep.WindowsService.Data
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	public class AnalyticsConnectData : AnalyticsBasicData
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003A74 File Offset: 0x00001C74
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003A7C File Offset: 0x00001C7C
		[DataMember]
		public States States { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003A85 File Offset: 0x00001C85
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003A8D File Offset: 0x00001C8D
		[DataMember]
		public long WinHandle { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003A96 File Offset: 0x00001C96
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003A9E File Offset: 0x00001C9E
		[DataMember]
		public string ProcessName { get; set; }
	}
}
