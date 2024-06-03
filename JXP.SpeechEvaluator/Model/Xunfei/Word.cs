using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000054 RID: 84
	[XmlRoot(ElementName = "word")]
	public class Word
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B1DC File Offset: 0x000093DC
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000B1E4 File Offset: 0x000093E4
		[XmlElement(ElementName = "syll")]
		public List<Syll> Syll { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B1ED File Offset: 0x000093ED
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B1F5 File Offset: 0x000093F5
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B1FE File Offset: 0x000093FE
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B206 File Offset: 0x00009406
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B20F File Offset: 0x0000940F
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B217 File Offset: 0x00009417
		[XmlAttribute(AttributeName = "dp_message")]
		public int DpMessage { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B220 File Offset: 0x00009420
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B228 File Offset: 0x00009428
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B231 File Offset: 0x00009431
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000B239 File Offset: 0x00009439
		[XmlAttribute(AttributeName = "global_index")]
		public int GlobalIndex { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B242 File Offset: 0x00009442
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000B24A File Offset: 0x0000944A
		[XmlAttribute(AttributeName = "index")]
		public int Index { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B253 File Offset: 0x00009453
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000B25B File Offset: 0x0000945B
		[XmlAttribute(AttributeName = "total_score")]
		public double TotalScore { get; set; } = -1.0;

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B264 File Offset: 0x00009464
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000B26C File Offset: 0x0000946C
		public bool IsUsed { get; set; }
	}
}
