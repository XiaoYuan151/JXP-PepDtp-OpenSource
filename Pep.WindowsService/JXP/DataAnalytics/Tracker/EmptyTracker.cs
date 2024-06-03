using System;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Platform;
using Pep.WindowsService.Data;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000031 RID: 49
	internal class EmptyTracker : ITracker
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000060E6 File Offset: 0x000042E6
		public string Name { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000060EE File Offset: 0x000042EE
		public IProductInfoProvider ProductInfoProvider { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000060F6 File Offset: 0x000042F6
		public ITrackerConfig TrackerConfig { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000060FE File Offset: 0x000042FE
		public IEnvironmentProvider EnvironmentProvider { get; }

		// Token: 0x06000147 RID: 327 RVA: 0x00006106 File Offset: 0x00004306
		public EmptyTracker(string name, ITrackerConfig config, IEnvironmentProvider env, IProductInfoProvider product)
		{
			this.Name = name;
			this.TrackerConfig = config;
			this.EnvironmentProvider = env;
			this.ProductInfoProvider = product;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000612B File Offset: 0x0000432B
		public void Close()
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000612D File Offset: 0x0000432D
		public void OnEvent(FullActivity activity)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000612F File Offset: 0x0000432F
		public void OnEvent(EventActivity activity)
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006131 File Offset: 0x00004331
		public void NotifySync()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006133 File Offset: 0x00004333
		public void OnEventWithConfig(EventActivity activity, AnalyticsBasicData config)
		{
			this.OnEvent(activity);
		}
	}
}
