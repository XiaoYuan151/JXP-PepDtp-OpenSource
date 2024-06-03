using System;
using System.Collections.Generic;
using JXP.SpeechEvaluator.Model;

namespace JXP.SpeechEvaluator.View.Navigation.NaviParas
{
	// Token: 0x0200001E RID: 30
	public class DuiHuaPageParas : NavigationParas
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006CC8 File Offset: 0x00004EC8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public string SelectedRole { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006CD9 File Offset: 0x00004ED9
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public string SelectedRoleImg { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006CEA File Offset: 0x00004EEA
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006CF2 File Offset: 0x00004EF2
		public List<Sentence> Sentences { get; set; }
	}
}
