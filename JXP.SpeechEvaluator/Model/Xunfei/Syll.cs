using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000053 RID: 83
	[XmlRoot(ElementName = "syll")]
	public class Syll
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B17F File Offset: 0x0000937F
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000B187 File Offset: 0x00009387
		[XmlElement(ElementName = "phone")]
		public List<Phone> Phone { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B190 File Offset: 0x00009390
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000B198 File Offset: 0x00009398
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B1A1 File Offset: 0x000093A1
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B1A9 File Offset: 0x000093A9
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B1B2 File Offset: 0x000093B2
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000B1BA File Offset: 0x000093BA
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B1C3 File Offset: 0x000093C3
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000B1CB File Offset: 0x000093CB
		[XmlAttribute(AttributeName = "syll_score")]
		public double SyllScore { get; set; }
	}
}
