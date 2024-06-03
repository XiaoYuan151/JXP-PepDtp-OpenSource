using System;
using System.Collections.Generic;

namespace JXP.SpeechEvaluator.Model.Http
{
	// Token: 0x0200005D RID: 93
	public class IndexList
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000B64E File Offset: 0x0000984E
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000B656 File Offset: 0x00009856
		public List<ChapterData> chapterList { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000B65F File Offset: 0x0000985F
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000B667 File Offset: 0x00009867
		public int _APP_RESULT_OPT_CODE { get; set; }
	}
}
