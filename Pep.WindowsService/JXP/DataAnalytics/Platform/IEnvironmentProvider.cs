using System;

namespace JXP.DataAnalytics.Platform
{
	// Token: 0x02000039 RID: 57
	public interface IEnvironmentProvider
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000196 RID: 406
		// (set) Token: 0x06000197 RID: 407
		string Os { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000198 RID: 408
		// (set) Token: 0x06000199 RID: 409
		string Hardware { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019A RID: 410
		// (set) Token: 0x0600019B RID: 411
		string IpAddr { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600019C RID: 412
		// (set) Token: 0x0600019D RID: 413
		string SoftwareDependency { get; set; }
	}
}
