using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000057 RID: 87
	[XmlRoot(ElementName = "read_sentence")]
	public class ReadSentence
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B42A File Offset: 0x0000962A
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000B432 File Offset: 0x00009632
		[XmlElement(ElementName = "sentence")]
		public List<XSentence> Sentences { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B43B File Offset: 0x0000963B
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000B443 File Offset: 0x00009643
		[XmlAttribute(AttributeName = "beg_pos")]
		public int BegPos { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B44C File Offset: 0x0000964C
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000B454 File Offset: 0x00009654
		[XmlAttribute(AttributeName = "content")]
		public string Content { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B45D File Offset: 0x0000965D
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000B465 File Offset: 0x00009665
		[XmlAttribute(AttributeName = "end_pos")]
		public int EndPos { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B46E File Offset: 0x0000966E
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000B476 File Offset: 0x00009676
		[XmlAttribute(AttributeName = "except_info")]
		public int ExceptInfo { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B47F File Offset: 0x0000967F
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000B487 File Offset: 0x00009687
		[XmlAttribute(AttributeName = "is_rejected")]
		public bool IsRejected { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B490 File Offset: 0x00009690
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B498 File Offset: 0x00009698
		[XmlAttribute(AttributeName = "reject_type")]
		public int RejectType { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B4A1 File Offset: 0x000096A1
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B4A9 File Offset: 0x000096A9
		[XmlAttribute(AttributeName = "score_pattern")]
		public string ScorePattern { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B4B2 File Offset: 0x000096B2
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000B4BA File Offset: 0x000096BA
		[XmlAttribute(AttributeName = "total_score")]
		public double TotalScore { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B4C3 File Offset: 0x000096C3
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B4CB File Offset: 0x000096CB
		[XmlAttribute(AttributeName = "word_count")]
		public int WordCount { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B4D4 File Offset: 0x000096D4
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B4DC File Offset: 0x000096DC
		[XmlAttribute(AttributeName = "lan")]
		public string Lan { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B4E5 File Offset: 0x000096E5
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000B4ED File Offset: 0x000096ED
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B4F6 File Offset: 0x000096F6
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000B4FE File Offset: 0x000096FE
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B507 File Offset: 0x00009707
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000B50F File Offset: 0x0000970F
		public bool NeedChangeColor { get; set; } = true;
	}
}
