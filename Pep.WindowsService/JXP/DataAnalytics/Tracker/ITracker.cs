using System;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Platform;
using Pep.WindowsService.Data;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000033 RID: 51
	public interface ITracker
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000151 RID: 337
		string Name { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000152 RID: 338
		IProductInfoProvider ProductInfoProvider { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000153 RID: 339
		ITrackerConfig TrackerConfig { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000154 RID: 340
		IEnvironmentProvider EnvironmentProvider { get; }

		// Token: 0x06000155 RID: 341
		void Close();

		// Token: 0x06000156 RID: 342
		void OnEvent(EventActivity activity);

		// Token: 0x06000157 RID: 343
		void OnEvent(FullActivity activity);

		// Token: 0x06000158 RID: 344
		void NotifySync();

		// Token: 0x06000159 RID: 345
		void OnEventWithConfig(EventActivity activity, AnalyticsBasicData config);
	}
}
