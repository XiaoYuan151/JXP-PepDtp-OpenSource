using System;
using Newtonsoft.Json;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x0200003F RID: 63
	public class UserInfoResult
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00013888 File Offset: 0x00011A88
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00013890 File Offset: 0x00011A90
		[JsonProperty("_APP_RESULT_OPT_CODE")]
		public string Result { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00013899 File Offset: 0x00011A99
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000138A1 File Offset: 0x00011AA1
		[JsonProperty("data")]
		public userInfo Data { get; set; }
	}
}
