using System;
using System.Collections.Generic;

namespace JXP.SpeechEvaluator.Model.Http
{
	// Token: 0x0200005C RID: 92
	public class BookData
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000B613 File Offset: 0x00009813
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000B61B File Offset: 0x0000981B
		public string tb_id { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B624 File Offset: 0x00009824
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000B62C File Offset: 0x0000982C
		public string tb_name { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B635 File Offset: 0x00009835
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000B63D File Offset: 0x0000983D
		public List<ChapterData> chapterList { get; set; }
	}
}
