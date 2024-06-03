using System;
using NAudio.Wave;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x0200003A RID: 58
	public class CustomWaveInEvent : WaveInEvent
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008FEC File Offset: 0x000071EC
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00008FF4 File Offset: 0x000071F4
		public string Id { get; set; } = Guid.NewGuid().ToString("N");

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008FFD File Offset: 0x000071FD
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00009005 File Offset: 0x00007205
		public WaveFileWriter VoiceWriter { get; set; }

		// Token: 0x06000207 RID: 519 RVA: 0x0000900E File Offset: 0x0000720E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.VoiceWriter != null)
			{
				this.VoiceWriter.Dispose();
			}
			this.VoiceWriter = null;
		}
	}
}
