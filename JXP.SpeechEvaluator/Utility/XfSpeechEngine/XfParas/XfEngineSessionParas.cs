using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas
{
	// Token: 0x02000049 RID: 73
	public class XfEngineSessionParas
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A858 File Offset: 0x00008A58
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000A860 File Offset: 0x00008A60
		public string Id { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A869 File Offset: 0x00008A69
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000A871 File Offset: 0x00008A71
		public string Text { get; set; } = string.Empty;

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A87A File Offset: 0x00008A7A
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000A882 File Offset: 0x00008A82
		public string VoiceFile { get; set; }
	}
}
