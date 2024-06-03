using System;
using JXP.Models;

namespace JXP.PepDtp.View
{
	// Token: 0x02000019 RID: 25
	public class DownloadResParamter
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000B5E7 File Offset: 0x000097E7
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000B5EF File Offset: 0x000097EF
		public string ResId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000B5F8 File Offset: 0x000097F8
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000B600 File Offset: 0x00009800
		public DeviceFlags DeviceFlag { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B609 File Offset: 0x00009809
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000B611 File Offset: 0x00009811
		public string SaveFilePath { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000B61A File Offset: 0x0000981A
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000B622 File Offset: 0x00009822
		public string DownloadUrl { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B62B File Offset: 0x0000982B
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000B633 File Offset: 0x00009833
		public bool IsNqRes { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000B63C File Offset: 0x0000983C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000B644 File Offset: 0x00009844
		public string ResUserId { get; set; }
	}
}
