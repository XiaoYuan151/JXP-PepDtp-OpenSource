using System;
using JXP.SpeechEvaluator.Model.Http;

namespace JXP.SpeechEvaluator.View.Navigation.NaviParas
{
	// Token: 0x02000020 RID: 32
	public class IndexPageParas : NavigationParas
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006D1C File Offset: 0x00004F1C
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006D24 File Offset: 0x00004F24
		public IndexList Speeches { get; set; } = new IndexList();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006D2D File Offset: 0x00004F2D
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006D35 File Offset: 0x00004F35
		public string BookId { get; set; }
	}
}
