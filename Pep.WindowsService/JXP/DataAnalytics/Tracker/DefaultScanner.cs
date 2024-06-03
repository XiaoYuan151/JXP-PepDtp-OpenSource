using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Exceptions;
using JXP.DataAnalytics.Network;
using JXP.DataAnalytics.Utility;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x0200002D RID: 45
	internal class DefaultScanner : IScanner
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004DFA File Offset: 0x00002FFA
		public string Name { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004E02 File Offset: 0x00003002
		public ITrackerConfig TrackerConfig { get; }

		// Token: 0x0600010D RID: 269 RVA: 0x00004E0A File Offset: 0x0000300A
		public DefaultScanner(string name, ITrackerConfig config)
		{
			if (config == null)
			{
				throw new TrackerException("参数[config]不能为空.", null);
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new TrackerException("参数[name]不能为空.", null);
			}
			this.Name = name;
			this.TrackerConfig = config;
			this.InternalInit();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004E49 File Offset: 0x00003049
		public void Start()
		{
			ThreadEx.Run(delegate()
			{
				InterProcessLocker.Execute(this.mLockString, new Action(this.DeleteOldData));
				try
				{
					Timer timer = this.mScanTimer;
					if (timer != null)
					{
						timer.Change(TimeSpan.Zero, this.TrackerConfig.ScanPeriod);
					}
				}
				catch (Exception ex)
				{
					DebugTracer.Write(ex);
				}
			});
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004E60 File Offset: 0x00003060
		public void Stop()
		{
			try
			{
				Timer timer = this.mScanTimer;
				if (timer != null)
				{
					timer.Change(-1, -1);
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004E9C File Offset: 0x0000309C
		private void InternalInit()
		{
			try
			{
				TrackerHelper.SyncConfig(DefaultNetProvider.Current, this.TrackerConfig);
				this.mLockString = string.Format("Global\\{0}{1}", this.TrackerConfig.AppFolder.Replace("\\", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty).ToLowerInvariant(), "scan");
				this.mScanDir = Path.Combine(this.TrackerConfig.AppFolder, "scan");
				this.mScanTimer = new Timer(new TimerCallback(this.ScanData), null, -1, -1);
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004F5C File Offset: 0x0000315C
		private void ScanData(object state)
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(DefaultScanner.mLockObj, TimeSpan.FromSeconds(3.0), ref flag);
				if (flag)
				{
					InterProcessLocker.Execute(this.mLockString, new Action(this.PostData));
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(DefaultScanner.mLockObj);
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004FD4 File Offset: 0x000031D4
		private void DeleteOldData()
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.mScanDir);
				if (directoryInfo.Exists)
				{
					long num = 0L;
					foreach (DirectoryInfo directoryInfo2 in from item in directoryInfo.EnumerateDirectories()
					orderby item.CreationTime
					select item)
					{
						long num2 = 0L;
						foreach (FileInfo fileInfo in directoryInfo2.EnumerateFiles(string.Format("*.{0}", "padat")))
						{
							num2 += fileInfo.Length;
							if (num2 >= this.TrackerConfig.MaxDataSize)
							{
								break;
							}
						}
						if (num2 == 0L && DateTime.Now.Date.CompareTo(directoryInfo2.CreationTime.Date) != 0)
						{
							DefaultScanner.DeleteDir(directoryInfo2);
						}
						num += num2;
						if (num >= this.TrackerConfig.MaxDataSize)
						{
							break;
						}
					}
					if (num >= this.TrackerConfig.MaxDataSize)
					{
						DateTime now = DateTime.Now;
						foreach (DirectoryInfo directoryInfo3 in from item in directoryInfo.EnumerateDirectories()
						orderby item.CreationTime
						select item)
						{
							if (DateTime.Now.Date.CompareTo(directoryInfo3.CreationTime.Date) != 0)
							{
								DefaultScanner.DeleteDir(directoryInfo3);
							}
							else
							{
								foreach (FileInfo fileInfo2 in directoryInfo3.EnumerateFiles(string.Format("*.{0}", "padat")))
								{
									if (fileInfo2.CreationTime.CompareTo(now) < 0)
									{
										DefaultScanner.DeleteFile(fileInfo2);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005274 File Offset: 0x00003474
		private void PostData()
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.mScanDir);
				if (directoryInfo.Exists)
				{
					int num = 0;
					foreach (DirectoryInfo directoryInfo2 in from item in directoryInfo.EnumerateDirectories()
					orderby item.CreationTime
					select item)
					{
						if (num >= 3)
						{
							break;
						}
						foreach (FileInfo fileInfo in directoryInfo2.EnumerateFiles(string.Format("*.{0}", "padat")))
						{
							if (num >= 3)
							{
								break;
							}
							if (fileInfo.Length == 0L || fileInfo.Length >= 512000L)
							{
								DefaultScanner.DeleteFile(fileInfo);
							}
							else
							{
								StringBuilder stringBuilder = new StringBuilder();
								try
								{
									foreach (string text in File.ReadAllLines(fileInfo.FullName))
									{
										if (!string.IsNullOrWhiteSpace(text))
										{
											if (text.IndexOf("|||", StringComparison.InvariantCultureIgnoreCase) == 0)
											{
												stringBuilder.Append(Base64StringHelper.StringDecrypt(text.Substring(3)));
											}
											else
											{
												stringBuilder.Append(text);
											}
										}
									}
								}
								catch (Exception ex)
								{
									DebugTracer.Write(ex);
								}
								if (stringBuilder.Length != 0)
								{
									if (!DefaultNetProvider.Current.SendTrackerData(stringBuilder.ToString()))
									{
										if (this.TrackerConfig.AlwaysDeleteFile)
										{
											DefaultScanner.DeleteFile(fileInfo);
										}
										num++;
									}
									else
									{
										DefaultScanner.DeleteFile(fileInfo);
										ThreadEx.Delay(TimeSpan.FromSeconds(1.0));
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				DebugTracer.Write(ex2);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005494 File Offset: 0x00003694
		private static void DeleteDir(DirectoryInfo dir)
		{
			try
			{
				if (dir != null && dir.Exists)
				{
					dir.Delete(true);
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000054D0 File Offset: 0x000036D0
		private static void DeleteFile(FileInfo file)
		{
			try
			{
				if (file != null && file.Exists)
				{
					file.Delete();
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x04000072 RID: 114
		private string mScanDir;

		// Token: 0x04000073 RID: 115
		private Timer mScanTimer;

		// Token: 0x04000074 RID: 116
		private static readonly object mLockObj = new object();

		// Token: 0x04000075 RID: 117
		private string mLockString;
	}
}
