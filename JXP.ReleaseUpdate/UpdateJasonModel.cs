using System;
using Newtonsoft.Json;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000008 RID: 8
	public class UpdateJasonModel
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C3D File Offset: 0x00000E3D
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002C45 File Offset: 0x00000E45
		[JsonProperty("statusCode")]
		public int Result { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C4E File Offset: 0x00000E4E
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002C56 File Offset: 0x00000E56
		[JsonProperty("msg")]
		public string RequestMessage { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C5F File Offset: 0x00000E5F
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002C67 File Offset: 0x00000E67
		[JsonProperty("dataMap")]
		public UpdateModel UpdateInfo { get; set; }
	}
}
