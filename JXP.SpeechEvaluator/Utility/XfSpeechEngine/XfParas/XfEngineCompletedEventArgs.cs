using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas
{
	// Token: 0x02000048 RID: 72
	public class XfEngineCompletedEventArgs : EventArgs
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A81D File Offset: 0x00008A1D
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000A825 File Offset: 0x00008A25
		public XfEngineResultFlag ResultFlag { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A82E File Offset: 0x00008A2E
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000A836 File Offset: 0x00008A36
		public XfEngineSessionParas SessionParas { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A83F File Offset: 0x00008A3F
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000A847 File Offset: 0x00008A47
		public string XfResult { get; set; }
	}
}
