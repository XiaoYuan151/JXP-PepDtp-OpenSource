using System;
using System.IO;
using System.Text;
using System.Threading;
using JXP.Logs;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine.XfParas;
using NAudio.Wave;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x0200003C RID: 60
	internal class XfEngineSession
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000091BD File Offset: 0x000073BD
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000091C5 File Offset: 0x000073C5
		public XfEngineSettings EngineSettings { get; set; } = new XfEngineSettings();

		// Token: 0x06000214 RID: 532 RVA: 0x000091CE File Offset: 0x000073CE
		public XfEngineSession(XfEngineSessionParas paras)
		{
			this.mSessionParas = paras;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000091E8 File Offset: 0x000073E8
		public void Start()
		{
			LogHelper.Instance.Info("StartRecording");
			try
			{
				this.InitData();
				if (this.BeginQISESession())
				{
					this.CreateRecorder();
					if (!this.TextPut())
					{
						throw new ApplicationException("文本设置错误.");
					}
					this.mRecorder.StartRecording();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				this.HandleErrorEvent();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009260 File Offset: 0x00007460
		public void Stop()
		{
			try
			{
				if (!this.mIsStoped)
				{
					this.mIsStoped = true;
					LogHelper.Instance.Info("StopRecording");
					this.DisposeRecorder();
					this.HandleCompletedEvent(0);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				this.HandleErrorEvent();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000092C0 File Offset: 0x000074C0
		public void FastStop()
		{
			try
			{
				if (!this.mIsStoped)
				{
					this.mIsStoped = true;
					LogHelper.Instance.Info("FastStop");
					this.DisposeRecorder();
					this.EndQISESession();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000931C File Offset: 0x0000751C
		private void InitData()
		{
			this.mPcmCount = 0;
			this.mIsStoped = false;
			this.mSessionID = string.Empty;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009338 File Offset: 0x00007538
		private void HandleCompletedEvent(int status)
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(XfEngineSession.lockObj, ref flag);
				if (flag)
				{
					LogHelper.Instance.Info(string.Format("HandleCompletedEvent.Status:{0}", status));
					this.EndWriteAudioDataToXunfei();
					string empty = string.Empty;
					XfEngineCompletedEventArgs xfEngineCompletedEventArgs = new XfEngineCompletedEventArgs
					{
						SessionParas = this.mSessionParas
					};
					if (this.GetAnswer(out empty))
					{
						xfEngineCompletedEventArgs.ResultFlag = XfEngineResultFlag.Normal;
						xfEngineCompletedEventArgs.XfResult = empty;
					}
					else
					{
						xfEngineCompletedEventArgs.ResultFlag = XfEngineResultFlag.NoData;
					}
					Action<XfEngineCompletedEventArgs> onCompleted = this.OnCompleted;
					if (onCompleted != null)
					{
						onCompleted(xfEngineCompletedEventArgs);
					}
					this.OnCompleted = null;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
			finally
			{
				if (flag)
				{
					this.EndQISESession();
					Monitor.Exit(XfEngineSession.lockObj);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009410 File Offset: 0x00007610
		private void HandleErrorEvent()
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(XfEngineSession.lockObj, ref flag);
				if (flag)
				{
					LogHelper.Instance.Info("HandleErrorEvent");
					this.DisposeRecorder();
					Action<XfEngineCompletedEventArgs> onCompleted = this.OnCompleted;
					if (onCompleted != null)
					{
						onCompleted(new XfEngineCompletedEventArgs
						{
							ResultFlag = XfEngineResultFlag.Exception,
							SessionParas = this.mSessionParas
						});
					}
					this.OnCompleted = null;
				}
			}
			finally
			{
				if (flag)
				{
					this.EndQISESession();
					Monitor.Exit(XfEngineSession.lockObj);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000949C File Offset: 0x0000769C
		private void Recorder_DataAvailable(object sender, WaveInEventArgs e)
		{
			try
			{
				if (this.mRecorder.VoiceWriter != null && this.mRecorder.VoiceWriter.CanWrite)
				{
					this.mRecorder.VoiceWriter.Write(e.Buffer, 0, e.BytesRecorded);
					this.mRecorder.VoiceWriter.Flush();
				}
				switch (this.WriteAudioDataToXunfei(e.Buffer, (uint)e.BytesRecorded))
				{
				case -1:
					this.DisposeRecorder();
					this.HandleCompletedEvent(-1);
					break;
				case 1:
					this.DisposeRecorder();
					this.HandleCompletedEvent(1);
					break;
				case 2:
					this.DisposeRecorder();
					this.HandleCompletedEvent(2);
					break;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				this.HandleErrorEvent();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009574 File Offset: 0x00007774
		private void CreateRecorder()
		{
			if (!Directory.Exists(Path.GetDirectoryName(this.mSessionParas.VoiceFile)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(this.mSessionParas.VoiceFile));
			}
			DateTime now = DateTime.Now;
			FileStream outStream;
			for (;;)
			{
				try
				{
					outStream = new FileStream(this.mSessionParas.VoiceFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				}
				catch
				{
					Thread.Sleep(100);
					if (DateTime.Now.Subtract(now).TotalSeconds >= 2.0)
					{
						throw new Exception("文件" + this.mSessionParas.VoiceFile + "被占用");
					}
					continue;
				}
				break;
			}
			this.mRecorder = new CustomWaveInEvent();
			this.mRecorder.Id = this.mSessionID;
			this.mRecorder.WaveFormat = new WaveFormat(16000, 1);
			this.mRecorder.DataAvailable += this.Recorder_DataAvailable;
			this.mRecorder.VoiceWriter = new WaveFileWriter(outStream, this.mRecorder.WaveFormat);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000968C File Offset: 0x0000788C
		private void DisposeRecorder()
		{
			ActionUtility.IgnoreExp(new Action[]
			{
				delegate()
				{
					if (this.mRecorder != null)
					{
						this.mRecorder.DataAvailable -= this.Recorder_DataAvailable;
						this.mRecorder.StopRecording();
						this.mRecorder.Dispose();
					}
					this.mRecorder = null;
				}
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000096A8 File Offset: 0x000078A8
		private bool BeginQISESession()
		{
			LogHelper.Instance.Info("QISESessionBegin");
			string param = string.Concat(new string[]
			{
				"sub=ise,category=",
				this.EngineSettings.Category,
				",ent=",
				this.EngineSettings.Ent,
				",aue=speex-wb;7,auf=audio/L16;rate=16000,plev=0,rse=utf8,vad_enable=1,vad_timeout=",
				this.EngineSettings.Vad_timeout,
				",vad_speech_tail=",
				this.EngineSettings.Vad_speech_tail
			});
			int num = 0;
			IntPtr h = ISEDLL.QISESessionBegin(param, null, ref num);
			if (num == 0)
			{
				this.mSessionID = UnmanagedManager.GetStringFromUnmanagedMemory(h);
				LogHelper.Instance.Info("创建Session成功.SessionId:[" + this.mSessionID + "]");
				return true;
			}
			LogHelper.Instance.Error(string.Format("创建Session失败.Code:[{0}]", num));
			return false;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009780 File Offset: 0x00007980
		private void EndQISESession()
		{
			LogHelper.Instance.Info("QISESessionEnd");
			if (!string.IsNullOrEmpty(this.mSessionID))
			{
				int num = ISEDLL.QISESessionEnd(this.mSessionID, null);
				if (num != 0)
				{
					LogHelper.Instance.Error(string.Format("结束Session失败.Code:[{0}]", num));
				}
			}
			this.mSessionID = string.Empty;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000097E0 File Offset: 0x000079E0
		private bool TextPut()
		{
			LogHelper.Instance.Info("TextPut");
			this.mPcmCount = 0;
			int num = ISEDLL.QISETextPut(this.mSessionID, this.mSessionParas.Text, (uint)Encoding.Default.GetByteCount(this.mSessionParas.Text), null);
			if (num != 0)
			{
				LogHelper.Instance.Error(string.Format("写入文本失败：{0}", num));
				return false;
			}
			return true;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009850 File Offset: 0x00007A50
		private int WriteAudioDataToXunfei(byte[] audioBuffer, uint len)
		{
			int num = 0;
			int num2 = 0;
			int audioStatus = 2;
			if (this.mPcmCount == 0)
			{
				audioStatus = 1;
			}
			int num3 = ISEDLL.QISEAudioWrite(this.mSessionID, audioBuffer, (uint)audioBuffer.Length, audioStatus, ref num, ref num2);
			if (num3 != 0)
			{
				LogHelper.Instance.Error(string.Format("写入音频时出错 Code:{0}", num3));
				return -1;
			}
			this.mPcmCount += audioBuffer.Length;
			if (num2 == 0)
			{
				return 2;
			}
			if (num >= 3)
			{
				LogHelper.Instance.Error("评测结束，原因epStatus大于等于3时,用户应当停止写入音频的操作,否则写入MSC的音频会被忽略");
				return 1;
			}
			return 0;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000098CC File Offset: 0x00007ACC
		private bool EndWriteAudioDataToXunfei()
		{
			int num = 0;
			int num2 = 0;
			int num3 = ISEDLL.QISEAudioWrite(this.mSessionID, IntPtr.Zero, 0U, 4, ref num, ref num2);
			if (num3 != 0)
			{
				LogHelper.Instance.Error(string.Format("结束音频时出错.Code:{0}", num3));
				return false;
			}
			return true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009914 File Offset: 0x00007B14
		private bool GetAnswer(out string answer)
		{
			uint num = 0U;
			int num2 = 0;
			int num3 = 0;
			answer = string.Empty;
			while (num3 != 5)
			{
				IntPtr h = ISEDLL.QISEGetResult(this.mSessionID, ref num, ref num3, ref num2);
				if (num2 != 0)
				{
					LogHelper.Instance.Error(string.Format("获取答案出错.Code:{0}", num2));
					return false;
				}
				if (num3 == 5)
				{
					answer = UnmanagedManager.GetStringFromUnmanagedMemory(h);
					return true;
				}
				Thread.Sleep(200);
			}
			return false;
		}

		// Token: 0x040000E2 RID: 226
		private static readonly object lockObj = new object();

		// Token: 0x040000E3 RID: 227
		private CustomWaveInEvent mRecorder;

		// Token: 0x040000E4 RID: 228
		private XfEngineSessionParas mSessionParas;

		// Token: 0x040000E5 RID: 229
		private string mSessionID;

		// Token: 0x040000E6 RID: 230
		private int mPcmCount;

		// Token: 0x040000E7 RID: 231
		private bool mIsStoped;

		// Token: 0x040000E9 RID: 233
		public Action<XfEngineCompletedEventArgs> OnCompleted;
	}
}
