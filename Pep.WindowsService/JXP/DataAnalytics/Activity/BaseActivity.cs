using System;
using JXP.DataAnalytics.Utility;

namespace JXP.DataAnalytics.Activity
{
	// Token: 0x02000049 RID: 73
	public class BaseActivity
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007538 File Offset: 0x00005738
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00007540 File Offset: 0x00005740
		public long? Id { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007549 File Offset: 0x00005749
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00007551 File Offset: 0x00005751
		public string Version { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000755A File Offset: 0x0000575A
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00007562 File Offset: 0x00005762
		public long StartTime { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000756B File Offset: 0x0000576B
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00007573 File Offset: 0x00005773
		public long EndTime { get; set; }

		// Token: 0x0600020F RID: 527 RVA: 0x0000757C File Offset: 0x0000577C
		public BaseActivity()
		{
			this.Version = "2";
			this.StartTime = UnixTimeHelper.Now;
		}
	}
}
