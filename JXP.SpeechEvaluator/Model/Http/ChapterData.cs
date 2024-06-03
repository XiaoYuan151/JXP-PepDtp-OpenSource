using System;
using System.Collections.Generic;

namespace JXP.SpeechEvaluator.Model.Http
{
	// Token: 0x0200005B RID: 91
	public class ChapterData
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000B5C7 File Offset: 0x000097C7
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000B5CF File Offset: 0x000097CF
		public string id { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000B5D8 File Offset: 0x000097D8
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public string name { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000B5E9 File Offset: 0x000097E9
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000B5F1 File Offset: 0x000097F1
		public int level { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000B5FA File Offset: 0x000097FA
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000B602 File Offset: 0x00009802
		public List<VoiceRes> voice_res_list { get; set; }
	}
}
