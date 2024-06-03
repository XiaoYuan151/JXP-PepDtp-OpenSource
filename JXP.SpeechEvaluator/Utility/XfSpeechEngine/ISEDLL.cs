using System;
using System.Runtime.InteropServices;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x02000042 RID: 66
	public static class ISEDLL
	{
		// Token: 0x0600023E RID: 574
		[DllImport("msc.dll", EntryPoint = "MSPLogin")]
		private static extern int MSPLogin_32(string usr, string pwd, string param);

		// Token: 0x0600023F RID: 575
		[DllImport("msc.dll", EntryPoint = "QISESessionBegin")]
		private static extern IntPtr QISESessionBegin_32(string param, string userModelId, ref int errorCode);

		// Token: 0x06000240 RID: 576
		[DllImport("msc.dll", EntryPoint = "QISETextPut")]
		private static extern int QISETextPut_32(string sessionID, string textString, uint textLen, string param);

		// Token: 0x06000241 RID: 577
		[DllImport("msc.dll", EntryPoint = "QISEAudioWrite")]
		private static extern int QISEAudioWrite_32(string sessionID, IntPtr waveDate, uint waveLen, int audioStatus, ref int epStatus, ref int Status);

		// Token: 0x06000242 RID: 578
		[DllImport("msc.dll", EntryPoint = "QISEAudioWrite")]
		private static extern int QISEAudioWrite_32(string sessionID, byte[] waveData, uint waveLen, int audioStatus, ref int epStatus, ref int Status);

		// Token: 0x06000243 RID: 579
		[DllImport("msc.dll", EntryPoint = "QISEGetResult")]
		private static extern IntPtr QISEGetResult_32(string sessionID, ref uint rsltLen, ref int rsltStatus, ref int errorCode);

		// Token: 0x06000244 RID: 580
		[DllImport("msc.dll", EntryPoint = "QISESessionEnd")]
		private static extern int QISESessionEnd_32(string sessionID, string hints);

		// Token: 0x06000245 RID: 581
		[DllImport("msc.dll", EntryPoint = "MSPLogout")]
		private static extern int MSPLogout_32();

		// Token: 0x06000246 RID: 582
		[DllImport("msc_x64.dll", EntryPoint = "MSPLogin")]
		private static extern int MSPLogin_64(string usr, string pwd, string param);

		// Token: 0x06000247 RID: 583
		[DllImport("msc_x64.dll", EntryPoint = "QISESessionBegin")]
		private static extern IntPtr QISESessionBegin_64(string param, string userModelId, ref int errorCode);

		// Token: 0x06000248 RID: 584
		[DllImport("msc_x64.dll", EntryPoint = "QISETextPut")]
		private static extern int QISETextPut_64(string sessionID, string textString, uint textLen, string param);

		// Token: 0x06000249 RID: 585
		[DllImport("msc_x64.dll", EntryPoint = "QISEAudioWrite")]
		private static extern int QISEAudioWrite_64(string sessionID, IntPtr waveDate, uint waveLen, int audioStatus, ref int epStatus, ref int Status);

		// Token: 0x0600024A RID: 586
		[DllImport("msc_x64.dll", EntryPoint = "QISEAudioWrite")]
		private static extern int QISEAudioWrite_64(string sessionID, byte[] waveData, uint waveLen, int audioStatus, ref int epStatus, ref int Status);

		// Token: 0x0600024B RID: 587
		[DllImport("msc_x64.dll", EntryPoint = "QISEGetResult")]
		private static extern IntPtr QISEGetResult_64(string sessionID, ref uint rsltLen, ref int rsltStatus, ref int errorCode);

		// Token: 0x0600024C RID: 588
		[DllImport("msc_x64.dll", EntryPoint = "QISESessionEnd")]
		private static extern int QISESessionEnd_64(string sessionID, string hints);

		// Token: 0x0600024D RID: 589
		[DllImport("msc_x64.dll", EntryPoint = "MSPLogout")]
		private static extern int MSPLogout_64();

		// Token: 0x0600024E RID: 590 RVA: 0x0000A567 File Offset: 0x00008767
		public static int MSPLogin(string usr, string pwd, string param)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.MSPLogin_64(usr, pwd, param);
			}
			return ISEDLL.MSPLogin_32(usr, pwd, param);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A581 File Offset: 0x00008781
		public static IntPtr QISESessionBegin(string param, string userModelId, ref int errorCode)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISESessionBegin_64(param, userModelId, ref errorCode);
			}
			return ISEDLL.QISESessionBegin_32(param, userModelId, ref errorCode);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A59B File Offset: 0x0000879B
		public static int QISETextPut(string sessionID, string textString, uint textLen, string param)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISETextPut_64(sessionID, textString, textLen, param);
			}
			return ISEDLL.QISETextPut_32(sessionID, textString, textLen, param);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A5B7 File Offset: 0x000087B7
		public static int QISEAudioWrite(string sessionID, IntPtr waveDate, uint waveLen, int audioStatus, ref int epStatus, ref int Status)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISEAudioWrite_64(sessionID, waveDate, waveLen, audioStatus, ref epStatus, ref Status);
			}
			return ISEDLL.QISEAudioWrite_32(sessionID, waveDate, waveLen, audioStatus, ref epStatus, ref Status);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A5DB File Offset: 0x000087DB
		public static int QISEAudioWrite(string sessionID, byte[] waveData, uint waveLen, int audioStatus, ref int epStatus, ref int Status)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISEAudioWrite_64(sessionID, waveData, waveLen, audioStatus, ref epStatus, ref Status);
			}
			return ISEDLL.QISEAudioWrite_32(sessionID, waveData, waveLen, audioStatus, ref epStatus, ref Status);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A5FF File Offset: 0x000087FF
		public static IntPtr QISEGetResult(string sessionID, ref uint rsltLen, ref int rsltStatus, ref int errorCode)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISEGetResult_64(sessionID, ref rsltLen, ref rsltStatus, ref errorCode);
			}
			return ISEDLL.QISEGetResult_32(sessionID, ref rsltLen, ref rsltStatus, ref errorCode);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A61B File Offset: 0x0000881B
		public static int QISESessionEnd(string sessionID, string hints)
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.QISESessionEnd_64(sessionID, hints);
			}
			return ISEDLL.QISESessionEnd_32(sessionID, hints);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A633 File Offset: 0x00008833
		public static int MSPLogout()
		{
			if (Environment.Is64BitProcess)
			{
				return ISEDLL.MSPLogout_64();
			}
			return ISEDLL.MSPLogout_32();
		}
	}
}
