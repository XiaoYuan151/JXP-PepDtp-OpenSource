using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using JXP.Logs;
using JXP.SpeechEvaluator.Model;
using JXP.Threading;
using NAudio.Utils;
using NAudio.Wave;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x02000041 RID: 65
	public class XfEngineHelper
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000099C8 File Offset: 0x00007BC8
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000099D0 File Offset: 0x00007BD0
		public string vad_timeout { get; set; } = "4000";

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000099D9 File Offset: 0x00007BD9
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000099E1 File Offset: 0x00007BE1
		public string vad_speech_tail { get; set; } = "5000";

		// Token: 0x0600022A RID: 554 RVA: 0x000099EC File Offset: 0x00007BEC
		public XfEngineHelper(Sentence sentence)
		{
			this.mCurSentence = sentence;
			Console.WriteLine(string.Format("GetHashCode:{0}", this.GetHashCode()));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009A53 File Offset: 0x00007C53
		private void ExecCompleted(object sender, EventArgs e)
		{
			Console.WriteLine("ExecCompleted");
			if (this.isStoped)
			{
				Console.WriteLine("ExecCompleted return");
				return;
			}
			this.isStoped = true;
			this.StopRecording(this.filePath);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009A85 File Offset: 0x00007C85
		private string CheckText(string originStr)
		{
			if (string.IsNullOrEmpty(originStr))
			{
				return originStr;
			}
			return originStr.Replace("(", " ").Replace(")", " ");
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public void StartRecording(string sub = "ise", string category = "read_sentence", string ent = "en_vip", bool isSimpleResult = false)
		{
			Console.WriteLine("StartRecording");
			if (this.isRecording)
			{
				return;
			}
			this.isStoping = false;
			this.isStoped = false;
			this.isRecording = true;
			ISEServerAgent.Instance.Login();
			this.QISESessionBegin(sub, category, ent, isSimpleResult);
			if (ent == "en_vip")
			{
				if (!string.IsNullOrEmpty(this.mCurSentence.NumReplace))
				{
					this.Text = "[content]\n" + this.mCurSentence.Content + "\n[number_replace]\n" + this.mCurSentence.NumReplace;
				}
				else
				{
					this.Text = "[content]\n" + this.mCurSentence.Content;
				}
			}
			else
			{
				this.Text = this.mCurSentence.Content;
			}
			this.filePath = this.mCurSentence.Voice;
			LogHelper.Instance.Debug(string.Concat(new string[]
			{
				"[",
				this.Text,
				"]\r\n[",
				this.filePath,
				"]"
			}));
			if (!Directory.Exists(Path.GetDirectoryName(this.filePath)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(this.filePath));
			}
			DateTime now = DateTime.Now;
			for (;;)
			{
				try
				{
					this.fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				}
				catch
				{
					Thread.Sleep(100);
					if (DateTime.Now.Subtract(now).TotalSeconds >= 2.0)
					{
						throw new Exception("文件" + this.filePath + "被占用");
					}
					continue;
				}
				break;
			}
			Application.Current.Dispatcher.Invoke(new Action(delegate()
			{
				this.waveIn = new WaveIn
				{
					WaveFormat = new WaveFormat(16000, 1)
				};
				this.waveIn.DataAvailable += this.waveIn_DataAvailable;
				this.writer = new WaveFileWriter(new IgnoreDisposeStream(this.fileStream), this.waveIn.WaveFormat);
				try
				{
					this.waveIn.StartRecording();
					this.TextPut(this.Text);
					Console.WriteLine(string.Format("StartRecording GetHashCode:{0}", this.GetHashCode()));
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error("没有检测到音频输入设备" + ex.ToString());
				}
			}), new object[0]);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009C7C File Offset: 0x00007E7C
		public void AudioWrite(Stream stream)
		{
			int num = (int)stream.Length;
			IntPtr pointer = UnmanagedManager.UnmanagedReader(stream, num);
			int num2 = 0;
			int audioStatus = 2;
			for (;;)
			{
				uint num3 = 6400U;
				if ((long)num <= (long)((ulong)(2U * num3)))
				{
					num3 = (uint)num;
				}
				if (num3 <= 0U)
				{
					break;
				}
				if (num2 == 0)
				{
					audioStatus = 1;
				}
				this.errorCode = ISEDLL.QISEAudioWrite(this.SessionID, pointer + num2, num3, audioStatus, ref this._epstatus, ref this._recstatus);
				num2 += (int)num3;
				num -= (int)num3;
			}
			this.errorCode = ISEDLL.QISEAudioWrite(this.SessionID, pointer + num2, 0U, 4, ref this._epstatus, ref this._recstatus);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009D14 File Offset: 0x00007F14
		public void StopRecording(bool fireCompletedEvent = false)
		{
			try
			{
				LogHelper.Instance.Debug(string.Format("StopRecording:{0}", fireCompletedEvent));
				if (this.isStoped)
				{
					LogHelper.Instance.Debug("StopRecording return");
				}
				else
				{
					this.isStoping = true;
					if (!fireCompletedEvent)
					{
						this.isRecording = false;
					}
					WaveIn waveIn = this.waveIn;
					if (waveIn != null)
					{
						waveIn.StopRecording();
					}
					WaveIn waveIn2 = this.waveIn;
					if (waveIn2 != null)
					{
						waveIn2.Dispose();
					}
					this.waveIn = null;
					TaskEx.Run(delegate()
					{
						try
						{
							int num = ISEDLL.QISEAudioWrite(this.SessionID, IntPtr.Zero, 0U, 4, ref this._epstatus, ref this._recstatus);
							if (num != 0)
							{
								LogHelper.Instance.Error(string.Format("[StopRecording]写入音频时出错，错误代码：{0}", num));
								this.isReadCompleted = true;
							}
							if (this._epstatus >= 3)
							{
								LogHelper.Instance.Error("评测结束，原因epStatus大于等于3时,用户应当停止写入音频的操作,否则写入MSC的音频会被忽略");
								this.isReadCompleted = true;
							}
							if (this._recstatus == 0 || this._recstatus == 5 || this._recstatus == 1)
							{
								this.isReadCompleted = true;
								if (this._recstatus == 1)
								{
									LogHelper.Instance.Error("评测结束，没有评测结果");
								}
								this.isHasResultReturn = true;
							}
							if (!fireCompletedEvent)
							{
								this.QISESessionEnd(this.SessionID);
							}
							else
							{
								num = ISEDLL.QISEAudioWrite(this.SessionID, IntPtr.Zero, 0U, 4, ref this._epstatus, ref this._recstatus);
								this.ExecCompleted(null, null);
							}
						}
						catch (Exception arg2)
						{
							LogHelper.Instance.Error(string.Format("停止录音出错：{0}", arg2));
						}
					});
					LogHelper.Instance.Debug("StopRecording ok");
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("出错{0}", arg));
			}
			finally
			{
				ActionUtility.IgnoreExp(new Action[]
				{
					delegate()
					{
						Console.WriteLine(string.Format("StopRecording GetHashCode:{0}", this.GetHashCode()));
						FileStream fileStream = this.fileStream;
						if (fileStream != null)
						{
							fileStream.Close();
						}
						FileStream fileStream2 = this.fileStream;
						if (fileStream2 != null)
						{
							fileStream2.Dispose();
						}
						this.fileStream = null;
						this.writer = null;
					}
				});
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009E24 File Offset: 0x00008024
		private void StopRecording(string filePath)
		{
			Console.WriteLine("StopRecording(string filePath)");
			object obj = XfEngineHelper.waveObjectLocker;
			lock (obj)
			{
				bool arg = false;
				string arg2 = string.Empty;
				try
				{
					if (this.isRecording)
					{
						LogHelper.Instance.Debug("StopRecording(string filePath) if");
						this.isRecording = false;
						Application.Current.Dispatcher.Invoke(new Action(delegate()
						{
							try
							{
								WaveIn waveIn = this.waveIn;
								if (waveIn != null)
								{
									waveIn.StopRecording();
								}
								WaveIn waveIn2 = this.waveIn;
								if (waveIn2 != null)
								{
									waveIn2.Dispose();
								}
								this.waveIn = null;
							}
							catch (Exception arg4)
							{
								LogHelper.Instance.Error(string.Format("停止录音出错：{0}", arg4));
							}
						}), new object[0]);
						ISEResultReader iseresultReader = new ISEResultReader(this.GetAnswer(), "");
						LogHelper.Instance.Debug("返回结果" + iseresultReader.OriContent);
						this.QISESessionEnd(this.SessionID);
						LogHelper.Instance.Debug("接收到停止录音指令，已停止");
						arg = (iseresultReader.Code == 1);
						arg2 = iseresultReader.OriContent;
					}
				}
				catch (Exception arg3)
				{
					LogHelper.Instance.Error(string.Format("出错{0}", arg3));
				}
				finally
				{
					ActionUtility.IgnoreExp(new Action[]
					{
						delegate()
						{
							Console.WriteLine(string.Format("StopRecording(string filePath) GetHashCode:{0}", this.GetHashCode()));
							FileStream fileStream = this.fileStream;
							if (fileStream != null)
							{
								fileStream.Close();
							}
							FileStream fileStream2 = this.fileStream;
							if (fileStream2 != null)
							{
								fileStream2.Dispose();
							}
							this.fileStream = null;
							this.writer = null;
						}
					});
					Action<Sentence, bool, string> whenResultReturnHandler = this.WhenResultReturnHandler;
					if (whenResultReturnHandler != null)
					{
						whenResultReturnHandler(this.mCurSentence, arg, arg2);
					}
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009F9C File Offset: 0x0000819C
		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			try
			{
				if (this.isRecording && !this.isStoping && !this.isStoped)
				{
					if (this.writer != null && this.writer.CanWrite)
					{
						this.writer.Write(e.Buffer, 0, e.BytesRecorded);
						this.writer.Flush();
						this.WriteAudioData(e.Buffer, (uint)e.BytesRecorded);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A034 File Offset: 0x00008234
		public int epStatus
		{
			get
			{
				return this._epstatus;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A03C File Offset: 0x0000823C
		public int recStatus
		{
			get
			{
				return this._recstatus;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A044 File Offset: 0x00008244
		public void QISESessionBegin(string sub = "ise", string category = "read_chapter", string ent = "en_vip", bool isSimpleResult = false)
		{
			if (!ISEServerAgent.Instance.IsLoginSucess)
			{
				throw new Exception("请先登录");
			}
			IntPtr h = ISEDLL.QISESessionBegin(isSimpleResult ? string.Concat(new string[]
			{
				"sub=",
				sub,
				",category=",
				category,
				",ent=",
				ent,
				",aue=speex-wb;7,auf=audio/L16;rate=16000,plev=1,rse=utf8,rst=plain,ise_unite=0,vad_enable=1,vad_timeout=",
				this.vad_timeout,
				",vad_speech_tail=",
				this.vad_speech_tail
			}) : string.Concat(new string[]
			{
				"sub=",
				sub,
				",category=",
				category,
				",ent=",
				ent,
				",aue=speex-wb;7,auf=audio/L16;rate=16000,plev=0,rse=utf8,vad_enable=1,vad_timeout=",
				this.vad_timeout,
				",vad_speech_tail=",
				this.vad_speech_tail
			}), null, ref this.errorCode);
			if (this.errorCode == 0)
			{
				this.SessionID = UnmanagedManager.GetStringFromUnmanagedMemory(h);
				this.isSessionEnd = false;
				LogHelper.Instance.Debug("QISESessionBegin SessionID:" + this.SessionID);
				return;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A15C File Offset: 0x0000835C
		public void TextPut(string text)
		{
			this.pcmCount = 0;
			this.isReadCompleted = false;
			this.isHasResultReturn = false;
			this.Text = (text ?? string.Empty);
			Console.WriteLine("TextPut SessionID:" + this.SessionID);
			this.errorCode = ISEDLL.QISETextPut(this.SessionID, this.CheckText(this.Text), (uint)Encoding.UTF8.GetByteCount(this.Text), null);
			if (this.errorCode != 0)
			{
				LogHelper.Instance.Error(string.Format("TextPut写入文本失败：{0}", this.errorCode));
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public void WriteAudioData(byte[] audioBuffer, uint len)
		{
			if (this.isStoping || this.isStoped)
			{
				return;
			}
			int audioStatus = 2;
			if (this.pcmCount == 0)
			{
				audioStatus = 1;
			}
			int num = ISEDLL.QISEAudioWrite(this.SessionID, audioBuffer, (uint)audioBuffer.Length, audioStatus, ref this._epstatus, ref this._recstatus);
			this.pcmCount += audioBuffer.Length;
			if (num != 0)
			{
				LogHelper.Instance.Error(string.Format("写入音频时出错，错误代码：{0}", num));
				this.isReadCompleted = true;
			}
			if (this._epstatus >= 3)
			{
				LogHelper.Instance.Error("评测结束，原因epStatus大于等于3时,用户应当停止写入音频的操作,否则写入MSC的音频会被忽略");
				this.isReadCompleted = true;
			}
			if (this._recstatus == 0 || this._recstatus == 5 || this._recstatus == 1)
			{
				this.isReadCompleted = true;
				if (this._recstatus == 1)
				{
					LogHelper.Instance.Error("评测结束，没有评测结果");
				}
				this.isHasResultReturn = true;
			}
			if (this.isReadCompleted)
			{
				num = ISEDLL.QISEAudioWrite(this.SessionID, IntPtr.Zero, 0U, 4, ref this._epstatus, ref this._recstatus);
				this.ExecCompleted(null, null);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A301 File Offset: 0x00008501
		public void EndAudioWrite()
		{
			if (this.isStoped)
			{
				return;
			}
			this.isReadCompleted = true;
			this.errorCode = ISEDLL.QISEAudioWrite(this.SessionID, IntPtr.Zero, 0U, 4, ref this._epstatus, ref this._recstatus);
			this.ExecCompleted(null, null);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A340 File Offset: 0x00008540
		public string GetAnswer()
		{
			int num = 0;
			uint num2 = 0U;
			while (num != 5)
			{
				Thread.Sleep(500);
				IntPtr h = ISEDLL.QISEGetResult(this.SessionID, ref num2, ref num, ref this.errorCode);
				if (num == 5)
				{
					return UnmanagedManager.GetStringFromUnmanagedMemory(h);
				}
				if (this.errorCode != 0)
				{
					return "error:" + this.errorCode.ToString();
				}
			}
			return null;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A3A1 File Offset: 0x000085A1
		public void QISESessionEnd(string sessionId)
		{
			if (!string.IsNullOrEmpty(sessionId))
			{
				LogHelper.Instance.Debug("注销session");
				this.errorCode = ISEDLL.QISESessionEnd(sessionId, null);
				this.isSessionEnd = true;
				Console.WriteLine("QISESessionEnd SessionID:" + sessionId);
			}
		}

		// Token: 0x04000180 RID: 384
		private bool isRecording;

		// Token: 0x04000181 RID: 385
		private WaveFileWriter writer;

		// Token: 0x04000182 RID: 386
		private FileStream fileStream;

		// Token: 0x04000183 RID: 387
		private WaveIn waveIn;

		// Token: 0x04000184 RID: 388
		private string Text = string.Empty;

		// Token: 0x04000185 RID: 389
		private string filePath = string.Empty;

		// Token: 0x04000186 RID: 390
		private Sentence mCurSentence;

		// Token: 0x04000187 RID: 391
		internal bool isStoping;

		// Token: 0x04000188 RID: 392
		private bool isStoped;

		// Token: 0x0400018B RID: 395
		public Action<Sentence, bool, string> WhenResultReturnHandler;

		// Token: 0x0400018C RID: 396
		private static object waveObjectLocker = new object();

		// Token: 0x0400018D RID: 397
		private int _epstatus;

		// Token: 0x0400018E RID: 398
		private int _recstatus;

		// Token: 0x0400018F RID: 399
		public string SessionID;

		// Token: 0x04000190 RID: 400
		public int errorCode;

		// Token: 0x04000191 RID: 401
		public bool isSessionEnd = true;

		// Token: 0x04000192 RID: 402
		private int pcmCount;

		// Token: 0x04000193 RID: 403
		private bool isReadCompleted;

		// Token: 0x04000194 RID: 404
		private bool isHasResultReturn;
	}
}
