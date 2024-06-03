using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JXP.PepDtp.View
{
	// Token: 0x02000025 RID: 37
	public class MessageInfoResult
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		[JsonProperty("result")]
		public List<MessageInfoModel> LstMessageInfo { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000D300 File Offset: 0x0000B500
		[JsonProperty("errmsg")]
		public string Msg { get; set; }
	}
}
