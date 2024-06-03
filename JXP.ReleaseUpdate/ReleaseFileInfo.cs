using System;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000006 RID: 6
	public class ReleaseFileInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002B0D File Offset: 0x00000D0D
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002B15 File Offset: 0x00000D15
		public string FilePath { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B1E File Offset: 0x00000D1E
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002B26 File Offset: 0x00000D26
		public string LastVersion { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002B2F File Offset: 0x00000D2F
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002B37 File Offset: 0x00000D37
		public long FileSize { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B40 File Offset: 0x00000D40
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002B48 File Offset: 0x00000D48
		public string Identity { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002B51 File Offset: 0x00000D51
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002B59 File Offset: 0x00000D59
		public int NeedInst { get; set; }
	}
}
