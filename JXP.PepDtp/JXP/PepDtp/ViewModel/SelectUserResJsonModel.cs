using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000069 RID: 105
	public class SelectUserResJsonModel
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000258C1 File Offset: 0x00023AC1
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x000258C9 File Offset: 0x00023AC9
		[JsonProperty("_APP_RESULT_OPT_CODE")]
		public string Result { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000258D2 File Offset: 0x00023AD2
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x000258DA File Offset: 0x00023ADA
		[JsonProperty("resList")]
		public List<SelectUserRes> SelectUserResList { get; set; }
	}
}
