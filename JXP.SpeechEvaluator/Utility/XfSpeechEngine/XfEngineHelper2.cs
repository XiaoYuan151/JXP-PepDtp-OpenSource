using System;
using JXP.Logs;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x0200003B RID: 59
	public class XfEngineHelper2
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000905F File Offset: 0x0000725F
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00009067 File Offset: 0x00007267
		public XfEngineSettings EngineSettings { get; set; } = new XfEngineSettings();

		// Token: 0x0600020C RID: 524 RVA: 0x00009084 File Offset: 0x00007284
		public bool Login()
		{
			LogHelper.Instance.Info("Login");
			string text = "appid = {0}";
			text = string.Format(text, XfEngineHelper2.AppId);
			int num = ISEDLL.MSPLogin(null, null, text);
			if (num == 0)
			{
				return true;
			}
			LogHelper.Instance.Error(string.Format("登录测评引擎失败.Code:[{0}]", num));
			return false;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000090DC File Offset: 0x000072DC
		public void Logout()
		{
			LogHelper.Instance.Info("Logout");
			int num = ISEDLL.MSPLogout();
			if (num != 0)
			{
				LogHelper.Instance.Info(string.Format("退出测评引擎失败.Code:[{0}]", num));
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000911C File Offset: 0x0000731C
		public void StartRecording(string text, string voiceFile, string id)
		{
			XfEngineSessionParas paras = new XfEngineSessionParas
			{
				Id = id,
				VoiceFile = voiceFile,
				Text = text
			};
			this.mEngineSession = new XfEngineSession(paras);
			this.mEngineSession.EngineSettings = this.EngineSettings;
			this.mEngineSession.OnCompleted = this.OnCompleted;
			this.mEngineSession.Start();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000917D File Offset: 0x0000737D
		public void StopRecording()
		{
			XfEngineSession xfEngineSession = this.mEngineSession;
			if (xfEngineSession != null)
			{
				xfEngineSession.Stop();
			}
			this.mEngineSession = null;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009197 File Offset: 0x00007397
		public void FastStop()
		{
			XfEngineSession xfEngineSession = this.mEngineSession;
			if (xfEngineSession != null)
			{
				xfEngineSession.FastStop();
			}
			this.mEngineSession = null;
		}

		// Token: 0x040000DD RID: 221
		private static readonly string AppId = "5f87ec12";

		// Token: 0x040000DE RID: 222
		private bool mIsStoped;

		// Token: 0x040000DF RID: 223
		private XfEngineSession mEngineSession;

		// Token: 0x040000E1 RID: 225
		public Action<XfEngineCompletedEventArgs> OnCompleted;
	}
}
