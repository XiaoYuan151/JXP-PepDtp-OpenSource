using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas
{
	// Token: 0x0200004A RID: 74
	public class XfEngineSettings
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A89E File Offset: 0x00008A9E
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public string Vad_timeout { get; set; } = "3000";

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A8AF File Offset: 0x00008AAF
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public string Vad_speech_tail { get; set; } = "3000";

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public string Category { get; set; } = "read_sentence";

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000A8D9 File Offset: 0x00008AD9
		public string Ent { get; set; } = "en_vip";
	}
}
