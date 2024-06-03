using System;
using Newtonsoft.Json;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000040 RID: 64
	public class userInfo
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000138AA File Offset: 0x00011AAA
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000138B2 File Offset: 0x00011AB2
		[JsonProperty("name")]
		public string Name { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000138BB File Offset: 0x00011ABB
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000138C3 File Offset: 0x00011AC3
		[JsonProperty("org_id")]
		public string OrgId { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000138CC File Offset: 0x00011ACC
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000138D4 File Offset: 0x00011AD4
		[JsonProperty("s_modify_time")]
		public string ModifyTime { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000138DD File Offset: 0x00011ADD
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000138E5 File Offset: 0x00011AE5
		[JsonProperty("mobile")]
		public string Mobile { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000138EE File Offset: 0x00011AEE
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000138F6 File Offset: 0x00011AF6
		[JsonProperty("reg_name")]
		public string RegName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000138FF File Offset: 0x00011AFF
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00013907 File Offset: 0x00011B07
		[JsonProperty("a_region_code")]
		public string RegionCode { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00013910 File Offset: 0x00011B10
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00013918 File Offset: 0x00011B18
		[JsonProperty("rkxd")]
		public string Xd { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00013921 File Offset: 0x00011B21
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00013929 File Offset: 0x00011B29
		[JsonProperty("time")]
		public string Time { get; set; }
	}
}
