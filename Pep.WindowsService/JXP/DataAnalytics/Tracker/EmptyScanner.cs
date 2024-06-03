using System;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000030 RID: 48
	internal class EmptyScanner : IScanner
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000060BC File Offset: 0x000042BC
		public string Name { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000060C4 File Offset: 0x000042C4
		public ITrackerConfig TrackerConfig { get; }

		// Token: 0x06000140 RID: 320 RVA: 0x000060CC File Offset: 0x000042CC
		public EmptyScanner(string name, ITrackerConfig config)
		{
			this.Name = name;
			this.TrackerConfig = config;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000060E2 File Offset: 0x000042E2
		public void Start()
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000060E4 File Offset: 0x000042E4
		public void Stop()
		{
		}
	}
}
