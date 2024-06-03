using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x0200003E RID: 62
	[Flags]
	public enum DataSample
	{
		// Token: 0x040000F0 RID: 240
		MSP_DATA_SAMPLE_INIT = 0,
		// Token: 0x040000F1 RID: 241
		MSP_DATA_SAMPLE_FIRST = 1,
		// Token: 0x040000F2 RID: 242
		MSP_DATA_SAMPLE_CONTINUE = 2,
		// Token: 0x040000F3 RID: 243
		MSP_DATA_SAMPLE_LAST = 4
	}
}
