using System;

namespace JXP.DataAnalytics.Activity
{
	// Token: 0x0200004B RID: 75
	public class FullActivity : EventActivity
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00007619 File Offset: 0x00005819
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007621 File Offset: 0x00005821
		public string ActionType { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000762A File Offset: 0x0000582A
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00007632 File Offset: 0x00005832
		public string PassiveType { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000763B File Offset: 0x0000583B
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007643 File Offset: 0x00005843
		public string ProductId { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000764C File Offset: 0x0000584C
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00007654 File Offset: 0x00005854
		public string ActiveUser { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000765D File Offset: 0x0000585D
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00007665 File Offset: 0x00005865
		public string ActiveType { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000766E File Offset: 0x0000586E
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00007676 File Offset: 0x00005876
		public string Organization { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000767F File Offset: 0x0000587F
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00007687 File Offset: 0x00005887
		public string GroupId { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007690 File Offset: 0x00005890
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00007698 File Offset: 0x00005898
		public string GroupType { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000076A1 File Offset: 0x000058A1
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000076A9 File Offset: 0x000058A9
		public string FromProduct { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000076B2 File Offset: 0x000058B2
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000076BA File Offset: 0x000058BA
		public string IpAddr { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000076C3 File Offset: 0x000058C3
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000076CB File Offset: 0x000058CB
		public string SoftwareDependency { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000076D4 File Offset: 0x000058D4
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000076DC File Offset: 0x000058DC
		public string Os { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000076E5 File Offset: 0x000058E5
		// (set) Token: 0x06000238 RID: 568 RVA: 0x000076ED File Offset: 0x000058ED
		public string Hardware { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000076F6 File Offset: 0x000058F6
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000076FE File Offset: 0x000058FE
		public string AppKey { get; set; }

		// Token: 0x0600023B RID: 571 RVA: 0x00007707 File Offset: 0x00005907
		public FullActivity()
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007710 File Offset: 0x00005910
		public FullActivity(EventActivity act)
		{
			base.Id = act.Id;
			base.Version = act.Version;
			base.StartTime = act.StartTime;
			base.EndTime = act.EndTime;
			base.ActionId = act.ActionId;
			base.Passive = act.Passive;
			base.FromPos = act.FromPos;
			base.Request = act.Request;
			base.Params = act.Params;
			base.RetCode = act.RetCode;
			base.RetInfo = act.RetInfo;
		}
	}
}
