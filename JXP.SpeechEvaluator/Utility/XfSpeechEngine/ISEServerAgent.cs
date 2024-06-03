using System;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x02000044 RID: 68
	public class ISEServerAgent : IDisposable
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A700 File Offset: 0x00008900
		public static ISEServerAgent Instance
		{
			get
			{
				return ISEServerAgent.context;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A707 File Offset: 0x00008907
		public bool IsLoginSucess
		{
			get
			{
				return this.isLoginSuccess;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A710 File Offset: 0x00008910
		public void Login()
		{
			if (this.isLoginSuccess)
			{
				return;
			}
			string text = "appid = {0}";
			text = string.Format(text, ISEServerAgent.AppId);
			this.errorCode = ISEDLL.MSPLogin(null, null, text);
			if (this.errorCode == 0)
			{
				this.isLoginSuccess = true;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A755 File Offset: 0x00008955
		protected virtual void Dispose(bool disposing)
		{
			this.errorCode = ISEDLL.MSPLogout();
			this.isLoginSuccess = false;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A769 File Offset: 0x00008969
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000199 RID: 409
		private static readonly ISEServerAgent context = new ISEServerAgent();

		// Token: 0x0400019A RID: 410
		private static readonly string AppId = "5f87ec12";

		// Token: 0x0400019B RID: 411
		public int errorCode;

		// Token: 0x0400019C RID: 412
		private bool isLoginSuccess;
	}
}
