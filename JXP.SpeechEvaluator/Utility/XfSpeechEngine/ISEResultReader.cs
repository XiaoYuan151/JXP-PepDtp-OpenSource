using System;
using System.Text;
using System.Xml;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x02000043 RID: 67
	public class ISEResultReader
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A647 File Offset: 0x00008847
		public string Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A64F File Offset: 0x0000884F
		public string OriContent
		{
			get
			{
				return this.oriContent;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A657 File Offset: 0x00008857
		public int Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A660 File Offset: 0x00008860
		public ISEResultReader(string resultxml, string category)
		{
			this.oriContent = resultxml;
			if (resultxml.StartsWith("error:"))
			{
				this._content = resultxml;
				this.code = 0;
				return;
			}
			this._content = this.PasswordBase64Encode(resultxml);
			this.code = 1;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A6C0 File Offset: 0x000088C0
		private string PasswordBase64Encode(string text)
		{
			string result = string.Empty;
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			try
			{
				result = Convert.ToBase64String(bytes);
			}
			catch
			{
				result = text;
			}
			return result;
		}

		// Token: 0x04000195 RID: 405
		private XmlElement xeSentence;

		// Token: 0x04000196 RID: 406
		private string oriContent = string.Empty;

		// Token: 0x04000197 RID: 407
		public int code;

		// Token: 0x04000198 RID: 408
		private string _content = "";
	}
}
