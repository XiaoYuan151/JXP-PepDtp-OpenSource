using System;

namespace JXP.SpeechEvaluator.View.Navigation
{
	// Token: 0x02000018 RID: 24
	public interface INavigationPage
	{
		// Token: 0x06000115 RID: 277
		void Init(NavigationParas paras);

		// Token: 0x06000116 RID: 278
		void BringFront();

		// Token: 0x06000117 RID: 279
		void BringBack();

		// Token: 0x06000118 RID: 280
		void Close(bool dispose);
	}
}
