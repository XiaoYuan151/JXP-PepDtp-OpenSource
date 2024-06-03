using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000055 RID: 85
	[XmlRoot(ElementName = "sentence")]
	public class XSentence
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B28C File Offset: 0x0000948C
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000B294 File Offset: 0x00009494
		[XmlElement(ElementName = "word")]
		public List<Word> Words { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B29D File Offset: 0x0000949D
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000B2A5 File Offset: 0x000094A5
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B2AE File Offset: 0x000094AE
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000B2B6 File Offset: 0x000094B6
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B2BF File Offset: 0x000094BF
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000B2C7 File Offset: 0x000094C7
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B2D0 File Offset: 0x000094D0
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000B2D8 File Offset: 0x000094D8
		[XmlAttribute(AttributeName = "index")]
		public int Index { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B2E1 File Offset: 0x000094E1
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000B2E9 File Offset: 0x000094E9
		[XmlAttribute(AttributeName = "total_score")]
		public double TotalScore { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B2F2 File Offset: 0x000094F2
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000B2FA File Offset: 0x000094FA
		[XmlAttribute(AttributeName = "word_count")]
		public int WordCount { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B303 File Offset: 0x00009503
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000B30B File Offset: 0x0000950B
		public bool IsUsed { get; set; }
	}
}
