using System;
using System.Collections.Generic;
using JXP.Models;

namespace JXP.PepDtp.Events
{
	// Token: 0x02000081 RID: 129
	public class PepClassEventArgs : EventArgs
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0002B66C File Offset: 0x0002986C
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0002B674 File Offset: 0x00029874
		public GroupInfoModel GroupInfo { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0002B67D File Offset: 0x0002987D
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0002B685 File Offset: 0x00029885
		public List<GroupUserInfoModel> GroupUserInfoList { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0002B68E File Offset: 0x0002988E
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0002B696 File Offset: 0x00029896
		public string BookId { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002B69F File Offset: 0x0002989F
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0002B6A7 File Offset: 0x000298A7
		public string ChapterId { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002B6B0 File Offset: 0x000298B0
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0002B6B8 File Offset: 0x000298B8
		public string ChapterName { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002B6C1 File Offset: 0x000298C1
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0002B6C9 File Offset: 0x000298C9
		public bool IsSharedScreen { get; set; }
	}
}
