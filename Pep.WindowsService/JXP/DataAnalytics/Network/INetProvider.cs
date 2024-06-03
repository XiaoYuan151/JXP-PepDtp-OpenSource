using System;
using JXP.DataAnalytics.Network.Model;

namespace JXP.DataAnalytics.Network
{
	// Token: 0x0200003E RID: 62
	internal interface INetProvider
	{
		// Token: 0x060001C7 RID: 455
		bool SendTrackerData(string data);

		// Token: 0x060001C8 RID: 456
		void SendTrackerDataAsync(string data);

		// Token: 0x060001C9 RID: 457
		TokenInfo GetTokenData();
	}
}
