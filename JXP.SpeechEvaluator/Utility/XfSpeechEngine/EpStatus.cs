using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x0200003F RID: 63
	public enum EpStatus
	{
		// Token: 0x040000F5 RID: 245
		MSP_EP_LOOKING_FOR_SPEECH,
		// Token: 0x040000F6 RID: 246
		MSP_EP_IN_SPEECH,
		// Token: 0x040000F7 RID: 247
		MSP_EP_AFTER_SPEECH = 3,
		// Token: 0x040000F8 RID: 248
		MSP_EP_TIMEOUT,
		// Token: 0x040000F9 RID: 249
		MSP_EP_ERROR,
		// Token: 0x040000FA RID: 250
		MSP_EP_MAX_SPEECH,
		// Token: 0x040000FB RID: 251
		MSP_EP_IDLE
	}
}
