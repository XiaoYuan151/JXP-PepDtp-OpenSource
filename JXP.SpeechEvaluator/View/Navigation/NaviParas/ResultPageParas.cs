using System;
using System.Collections.Generic;
using JXP.SpeechEvaluator.Model;

namespace JXP.SpeechEvaluator.View.Navigation.NaviParas
{
	// Token: 0x02000021 RID: 33
	public class ResultPageParas : NavigationParas
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006D51 File Offset: 0x00004F51
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006D59 File Offset: 0x00004F59
		public List<Sentence> Sentences { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006D62 File Offset: 0x00004F62
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006D6A File Offset: 0x00004F6A
		public string SelectedRole { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006D73 File Offset: 0x00004F73
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006D7B File Offset: 0x00004F7B
		public string SelectedRoleImg { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006D84 File Offset: 0x00004F84
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006D8C File Offset: 0x00004F8C
		public string RecordingFolder { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006D95 File Offset: 0x00004F95
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006D9D File Offset: 0x00004F9D
		public bool NoDetailScore { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006DA6 File Offset: 0x00004FA6
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006DAE File Offset: 0x00004FAE
		public double TotalScore { get; set; }
	}
}
