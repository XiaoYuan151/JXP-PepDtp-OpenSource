using System;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000034 RID: 52
	public interface ITrackerConfig
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015A RID: 346
		// (set) Token: 0x0600015B RID: 347
		string AppKey { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015C RID: 348
		// (set) Token: 0x0600015D RID: 349
		string AppFolder { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015E RID: 350
		// (set) Token: 0x0600015F RID: 351
		long MaxDataSize { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000160 RID: 352
		// (set) Token: 0x06000161 RID: 353
		TimeSpan ScanPeriod { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000162 RID: 354
		// (set) Token: 0x06000163 RID: 355
		int TrackerQueueCapacity { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000164 RID: 356
		// (set) Token: 0x06000165 RID: 357
		bool AlwaysDeleteFile { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000166 RID: 358
		// (set) Token: 0x06000167 RID: 359
		int SingleFileSize { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000168 RID: 360
		// (set) Token: 0x06000169 RID: 361
		int MaxRecordCountWhenPost { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016A RID: 362
		// (set) Token: 0x0600016B RID: 363
		int PostTimeout { get; set; }
	}
}
