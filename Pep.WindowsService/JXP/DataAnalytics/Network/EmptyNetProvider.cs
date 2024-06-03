using System;
using JXP.DataAnalytics.Network.Model;

namespace JXP.DataAnalytics.Network
{
	// Token: 0x0200003C RID: 60
	internal class EmptyNetProvider : INetProvider
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00006EE8 File Offset: 0x000050E8
		public bool SendTrackerData(string data)
		{
			return true;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006EEB File Offset: 0x000050EB
		public void SendTrackerDataAsync(string data)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006EED File Offset: 0x000050ED
		public TokenInfo GetTokenData()
		{
			return null;
		}
	}
}
