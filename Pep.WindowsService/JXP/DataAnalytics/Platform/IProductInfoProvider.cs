using System;

namespace JXP.DataAnalytics.Platform
{
	// Token: 0x0200003A RID: 58
	public interface IProductInfoProvider
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600019E RID: 414
		// (set) Token: 0x0600019F RID: 415
		string ProductId { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A0 RID: 416
		// (set) Token: 0x060001A1 RID: 417
		string ActiveUser { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A2 RID: 418
		// (set) Token: 0x060001A3 RID: 419
		string Organization { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A4 RID: 420
		// (set) Token: 0x060001A5 RID: 421
		string GroupId { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A6 RID: 422
		// (set) Token: 0x060001A7 RID: 423
		string ProductVersion { get; set; }
	}
}
