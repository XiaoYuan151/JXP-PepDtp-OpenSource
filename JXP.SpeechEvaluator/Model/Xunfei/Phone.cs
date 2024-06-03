using System;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000052 RID: 82
	[XmlRoot(ElementName = "phone")]
	public class Phone
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B133 File Offset: 0x00009333
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000B13B File Offset: 0x0000933B
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B144 File Offset: 0x00009344
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000B14C File Offset: 0x0000934C
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B155 File Offset: 0x00009355
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000B15D File Offset: 0x0000935D
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B166 File Offset: 0x00009366
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000B16E File Offset: 0x0000936E
		[XmlAttribute(AttributeName = "gwpp")]
		public double Gwpp { get; set; }
	}
}
