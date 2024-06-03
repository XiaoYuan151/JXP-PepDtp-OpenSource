using System;
using System.Xml.Serialization;

namespace JXP.SpeechEvaluator.Model.Xunfei
{
	// Token: 0x02000059 RID: 89
	[XmlRoot(ElementName = "xml_result")]
	public class XmlResult
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000B551 File Offset: 0x00009751
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000B559 File Offset: 0x00009759
		[XmlElement(ElementName = "read_chapter")]
		public ReadChapter ReadChapter { get; set; }
	}
}
