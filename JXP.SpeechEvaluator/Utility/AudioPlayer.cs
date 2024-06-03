using System;
using System.IO;
using System.Windows.Media;
using JXP.Logs;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000034 RID: 52
	internal class AudioPlayer
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008368 File Offset: 0x00006568
		public static AudioPlayer Instance
		{
			get
			{
				return AudioPlayer.singleton.Value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008374 File Offset: 0x00006574
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000837C File Offset: 0x0000657C
		public bool IsEnabled { get; set; } = true;

		// Token: 0x060001DA RID: 474 RVA: 0x00008388 File Offset: 0x00006588
		public void Play(string fileName, Action completed)
		{
			this.StopPlay();
			if (!File.Exists(fileName))
			{
				return;
			}
			if (!this.IsEnabled)
			{
				return;
			}
			try
			{
				this.mediaPlayer.Open(new Uri(fileName));
				this.mediaPlayer.MediaEnded -= this.MediaPlayer_MediaEnded;
				this.mediaPlayer.MediaFailed -= new EventHandler<ExceptionEventArgs>(this.MediaPlayer_MediaEnded);
				this.mediaPlayer.MediaEnded += this.MediaPlayer_MediaEnded;
				this.mediaPlayer.MediaFailed += new EventHandler<ExceptionEventArgs>(this.MediaPlayer_MediaEnded);
				this.playCompleted = completed;
				this.mediaPlayer.Play();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				this.MediaPlayer_MediaEnded(null, null);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008458 File Offset: 0x00006658
		public void PlaySync(string fileName)
		{
			this.StopPlay();
			if (!File.Exists(fileName))
			{
				return;
			}
			if (!this.IsEnabled)
			{
				return;
			}
			try
			{
				this.mediaPlayer.Open(new Uri(fileName));
				this.mediaPlayer.Play();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000084BC File Offset: 0x000066BC
		public void StopPlay()
		{
			this.playCompleted = null;
			if (this.mediaPlayer != null)
			{
				this.mediaPlayer.MediaEnded -= this.MediaPlayer_MediaEnded;
				this.mediaPlayer.MediaFailed -= new EventHandler<ExceptionEventArgs>(this.MediaPlayer_MediaEnded);
				this.mediaPlayer.Stop();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008511 File Offset: 0x00006711
		public void ClosePlayer()
		{
			this.IsEnabled = false;
			this.StopPlay();
			MediaPlayer mediaPlayer = this.mediaPlayer;
			if (mediaPlayer != null)
			{
				mediaPlayer.Close();
			}
			this.mediaPlayer = null;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008538 File Offset: 0x00006738
		public void InitPlayer()
		{
			if (this.mediaPlayer == null)
			{
				this.mediaPlayer = new MediaPlayer();
			}
			this.IsEnabled = true;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008554 File Offset: 0x00006754
		private void MediaPlayer_MediaEnded(object sender, EventArgs e)
		{
			if (e is ExceptionEventArgs)
			{
				LogHelper.Instance.Error(((ExceptionEventArgs)e).ErrorException);
			}
			if (!this.IsEnabled)
			{
				return;
			}
			Action action = this.playCompleted;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x040000D1 RID: 209
		private static readonly Lazy<AudioPlayer> singleton = new Lazy<AudioPlayer>();

		// Token: 0x040000D2 RID: 210
		private MediaPlayer mediaPlayer = new MediaPlayer();

		// Token: 0x040000D3 RID: 211
		private Action playCompleted;
	}
}
