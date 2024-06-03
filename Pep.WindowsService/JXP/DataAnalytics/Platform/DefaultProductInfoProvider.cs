using System;

namespace JXP.DataAnalytics.Platform
{
	// Token: 0x02000038 RID: 56
	public class DefaultProductInfoProvider : IProductInfoProvider
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000069DC File Offset: 0x00004BDC
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000069E4 File Offset: 0x00004BE4
		public string ProductId { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000069ED File Offset: 0x00004BED
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000069F5 File Offset: 0x00004BF5
		public string ActiveUser { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000069FE File Offset: 0x00004BFE
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00006A06 File Offset: 0x00004C06
		public string Organization { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006A0F File Offset: 0x00004C0F
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006A17 File Offset: 0x00004C17
		public string GroupId { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006A20 File Offset: 0x00004C20
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006A28 File Offset: 0x00004C28
		public string ProductVersion { get; set; }

		// Token: 0x06000195 RID: 405 RVA: 0x00006A34 File Offset: 0x00004C34
		public DefaultProductInfoProvider()
		{
			this.GroupId = Guid.NewGuid().ToString("N");
		}
	}
}
