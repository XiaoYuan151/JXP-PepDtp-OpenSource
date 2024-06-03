using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000056 RID: 86
	[XmlRoot(ElementName = "read_chapter")]
	public class ReadChapter
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000B31C File Offset: 0x0000951C
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000B324 File Offset: 0x00009524
		[XmlElement(ElementName = "sentence")]
		public List<XSentence> Sentences { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B32D File Offset: 0x0000952D
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000B335 File Offset: 0x00009535
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000B33E File Offset: 0x0000953E
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000B346 File Offset: 0x00009546
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000B34F File Offset: 0x0000954F
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000B357 File Offset: 0x00009557
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000B360 File Offset: 0x00009560
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000B368 File Offset: 0x00009568
		[XmlAttribute(AttributeName = "except_info")]
		public int ExceptInfo { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000B371 File Offset: 0x00009571
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000B379 File Offset: 0x00009579
		[XmlAttribute(AttributeName = "is_rejected")]
		public bool IsRejected { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B382 File Offset: 0x00009582
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000B38A File Offset: 0x0000958A
		[XmlAttribute(AttributeName = "reject_type")]
		public int RejectType { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B393 File Offset: 0x00009593
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000B39B File Offset: 0x0000959B
		[XmlAttribute(AttributeName = "score_pattern")]
		public string ScorePattern { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B3A4 File Offset: 0x000095A4
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000B3AC File Offset: 0x000095AC
		[XmlAttribute(AttributeName = "total_score")]
		public double TotalScore { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B3B5 File Offset: 0x000095B5
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000B3BD File Offset: 0x000095BD
		[XmlAttribute(AttributeName = "word_count")]
		public int WordCount { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B3C6 File Offset: 0x000095C6
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000B3CE File Offset: 0x000095CE
		[XmlElement(ElementName = "rec_paper")]
		public RecPaper RecPaper { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B3D7 File Offset: 0x000095D7
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000B3DF File Offset: 0x000095DF
		[XmlAttribute(AttributeName = "lan")]
		public string Lan { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000B3E8 File Offset: 0x000095E8
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000B3F0 File Offset: 0x000095F0
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B3F9 File Offset: 0x000095F9
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000B401 File Offset: 0x00009601
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B40A File Offset: 0x0000960A
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000B412 File Offset: 0x00009612
		public bool NeedChangeColor { get; set; } = true;
	}
}
