using System;

namespace JXP.DataAnalytics.Activity
{
	// Token: 0x0200004A RID: 74
	public class EventActivity : BaseActivity
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000759A File Offset: 0x0000579A
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000075A2 File Offset: 0x000057A2
		public string ActionId { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000075AB File Offset: 0x000057AB
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000075B3 File Offset: 0x000057B3
		public string Passive { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000075BC File Offset: 0x000057BC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000075C4 File Offset: 0x000057C4
		public string FromPos { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000075CD File Offset: 0x000057CD
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000075D5 File Offset: 0x000057D5
		public string Request { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000075DE File Offset: 0x000057DE
		// (set) Token: 0x06000219 RID: 537 RVA: 0x000075E6 File Offset: 0x000057E6
		public string Params { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000075EF File Offset: 0x000057EF
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000075F7 File Offset: 0x000057F7
		public string RetCode { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007600 File Offset: 0x00005800
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00007608 File Offset: 0x00005808
		public string RetInfo { get; set; }
	}
}
