using System;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005E RID: 94
	public class ModifyPwdResultModel
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00023227 File Offset: 0x00021427
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0002322F File Offset: 0x0002142F
		[JsonProperty("s_state")]
		public string State { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00023238 File Offset: 0x00021438
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00023240 File Offset: 0x00021440
		[JsonProperty("msg")]
		public string Msg { get; set; }
	}
}
