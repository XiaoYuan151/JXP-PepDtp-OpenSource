using System;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000058 RID: 88
	[XmlRoot(ElementName = "rec_paper")]
	public class RecPaper
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B527 File Offset: 0x00009727
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000B52F File Offset: 0x0000972F
		[XmlElement(ElementName = "read_chapter")]
		public ReadChapter ReadChapter { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B538 File Offset: 0x00009738
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000B540 File Offset: 0x00009740
		[XmlElement(ElementName = "read_sentence")]
		public ReadSentence ReadSentence { get; set; }
	}
}
