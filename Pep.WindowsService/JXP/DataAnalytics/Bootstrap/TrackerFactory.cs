using System;
using System.IO;
using JXP.DataAnalytics.Network;
using JXP.DataAnalytics.Platform;
using JXP.DataAnalytics.Tracker;

namespace JXP.DataAnalytics.Bootstrap
{
	// Token: 0x02000046 RID: 70
	public sealed class TrackerFactory
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00007265 File Offset: 0x00005465
		public TrackerFactory SetConfig(ITrackerConfig config)
		{
			this.mTrackerConfig = config;
			return this;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000726F File Offset: 0x0000546F
		public TrackerFactory SetEnvProvider(IEnvironmentProvider envProvider)
		{
			this.mEnvironmentProvider = envProvider;
			return this;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007279 File Offset: 0x00005479
		public TrackerFactory SetProductInfoProvider(IProductInfoProvider productInfoProvider)
		{
			this.mProductInfoProvider = productInfoProvider;
			return this;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007284 File Offset: 0x00005484
		public TrackerFactory Build()
		{
			if (this.mTrackerConfig == null)
			{
				this.mTrackerConfig = new DefaultTrackerConfig();
			}
			if (!Directory.Exists(this.mTrackerConfig.AppFolder))
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(this.mTrackerConfig.AppFolder);
				if (!directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
				{
					directoryInfo.Attributes |= FileAttributes.Hidden;
				}
			}
			return this;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000072F0 File Offset: 0x000054F0
		internal ITracker NewRealTimeTracker(string trackerName = "")
		{
			if (this.mProductInfoProvider == null)
			{
				this.mProductInfoProvider = new DefaultProductInfoProvider();
			}
			if (this.mEnvironmentProvider == null)
			{
				this.mEnvironmentProvider = new DefaultEnvironmentProvider();
			}
			return new RealtimeTracker(string.IsNullOrEmpty(trackerName) ? Guid.NewGuid().ToString("N") : trackerName, this.mTrackerConfig, this.mEnvironmentProvider, this.mProductInfoProvider);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007357 File Offset: 0x00005557
		internal INetProvider NewNetProvider()
		{
			return new DefaultNetProvider(this.mTrackerConfig);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007364 File Offset: 0x00005564
		internal ITracker NewEmptyTracker(string trackerName = "")
		{
			if (this.mProductInfoProvider == null)
			{
				this.mProductInfoProvider = new DefaultProductInfoProvider();
			}
			if (this.mEnvironmentProvider == null)
			{
				this.mEnvironmentProvider = new DefaultEnvironmentProvider();
			}
			return new EmptyTracker(string.IsNullOrEmpty(trackerName) ? Guid.NewGuid().ToString("N") : trackerName, this.mTrackerConfig, this.mEnvironmentProvider, this.mProductInfoProvider);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000073CC File Offset: 0x000055CC
		internal IScanner NewEmptyScanner(string scannerName = "")
		{
			return new EmptyScanner(string.IsNullOrEmpty(scannerName) ? Guid.NewGuid().ToString("N") : scannerName, this.mTrackerConfig);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007401 File Offset: 0x00005601
		internal INetProvider NewEmptyNetProvider()
		{
			return new EmptyNetProvider();
		}

		// Token: 0x040000B1 RID: 177
		private ITrackerConfig mTrackerConfig;

		// Token: 0x040000B2 RID: 178
		private IEnvironmentProvider mEnvironmentProvider;

		// Token: 0x040000B3 RID: 179
		private IProductInfoProvider mProductInfoProvider;
	}
}
