using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x02000012 RID: 18
	public partial class XfEngineTestView : UserControl
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00005B78 File Offset: 0x00003D78
		public XfEngineTestView()
		{
			this.InitializeComponent();
			this.xfEngineHelper2.OnCompleted = new Action<XfEngineCompletedEventArgs>(this.XfEngineHelper2_OnCompleted);
			this.xfEngineHelper2.Login();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005BB4 File Offset: 0x00003DB4
		private void XfEngineHelper2_OnCompleted(XfEngineCompletedEventArgs e)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("e.ResultFlag\r\n:{0}", e.ResultFlag));
				StringBuilder stringBuilder2 = stringBuilder;
				string str = "e.FilePath\r\n:";
				XfEngineSessionParas sessionParas = e.SessionParas;
				stringBuilder2.AppendLine(str + ((sessionParas != null) ? sessionParas.VoiceFile : null));
				stringBuilder.AppendLine("e.XfResult\r\n:" + e.XfResult);
				this.txtXf.Text = stringBuilder.ToString();
			}), new object[0]);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005BF3 File Offset: 0x00003DF3
		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			this.xfEngineHelper2.StopRecording();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005C00 File Offset: 0x00003E00
		private void Start_Click(object sender, RoutedEventArgs e)
		{
			this.xfEngineHelper2.FastStop();
			this.txtXf.Text = string.Empty;
			string voiceFile = Path.Combine(new string[]
			{
				AppDomain.CurrentDomain.BaseDirectory + "q.wav"
			});
			string text = "[content]" + this.txtReserveText.Text;
			this.xfEngineHelper2.StartRecording(text, voiceFile, string.Empty);
		}

		// Token: 0x0400005C RID: 92
		private XfEngineHelper2 xfEngineHelper2 = new XfEngineHelper2();
	}
}
