using System;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000078 RID: 120
	public class VideoUploadResult
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002A964 File Offset: 0x00028B64
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0002A96C File Offset: 0x00028B6C
		[JsonProperty("s_state")]
		public int State { get; set; }
	}
}
