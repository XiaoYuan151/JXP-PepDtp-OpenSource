using System;
using JXP.Models;
using JXP.PepDtp.Common;
using Newtonsoft.Json;

namespace JXP.PepDtp.PepClass
{
	// Token: 0x0200007D RID: 125
	internal class PepClassParameter
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002AC57 File Offset: 0x00028E57
		[JsonProperty]
		public static PepClassSharedData SharedData { get; } = new PepClassSharedData();

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0002AC5E File Offset: 0x00028E5E
		[JsonProperty]
		public static RequestUrls RequestUrl { get; } = new RequestUrls();
	}
}
