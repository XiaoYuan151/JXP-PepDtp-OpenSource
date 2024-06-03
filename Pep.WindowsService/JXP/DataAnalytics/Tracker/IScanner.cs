using System;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000032 RID: 50
	internal interface IScanner
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014D RID: 333
		string Name { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014E RID: 334
		ITrackerConfig TrackerConfig { get; }

		// Token: 0x0600014F RID: 335
		void Start();

		// Token: 0x06000150 RID: 336
		void Stop();
	}
}
