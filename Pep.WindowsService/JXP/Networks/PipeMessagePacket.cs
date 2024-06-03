using System;

namespace JXP.Networks
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class PipeMessagePacket
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003B47 File Offset: 0x00001D47
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003B4F File Offset: 0x00001D4F
		public string Command { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003B58 File Offset: 0x00001D58
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003B60 File Offset: 0x00001D60
		public string ModuleId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003B69 File Offset: 0x00001D69
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003B71 File Offset: 0x00001D71
		public string JsonData { get; set; }
	}
}
