using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Exceptions;
using JXP.DataAnalytics.Platform;
using JXP.DataAnalytics.Utility;
using Pep.WindowsService.Data;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x0200002E RID: 46
	public class DefaultTracker : ITracker
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005578 File Offset: 0x00003778
		public string Name { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005580 File Offset: 0x00003780
		public IProductInfoProvider ProductInfoProvider { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005588 File Offset: 0x00003788
		public ITrackerConfig TrackerConfig { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005590 File Offset: 0x00003790
		public IEnvironmentProvider EnvironmentProvider { get; }

		// Token: 0x0600011C RID: 284 RVA: 0x00005598 File Offset: 0x00003798
		public DefaultTracker(string name, ITrackerConfig config, IEnvironmentProvider env, IProductInfoProvider product)
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
			this.EnvironmentProvider = env;
			this.ProductInfoProvider = product;
			this.InternalInit();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000055F4 File Offset: 0x000037F4
		public void Close()
		{
			try
			{
				if (this.mActivityCollection != null)
				{
					this.mActivityCollection.CompleteAdding();
				}
				this.mCurWriter = null;
				if (this.mCurTraceListener != null)
				{
					this.mCurTraceListener.Close();
					this.mCurTraceListener = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000564C File Offset: 0x0000384C
		public void OnEvent(EventActivity activity)
		{
			this.InternalOnEvent(activity);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005655 File Offset: 0x00003855
		public void OnEvent(FullActivity activity)
		{
			this.InternalOnEvent(activity);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000565E File Offset: 0x0000385E
		public void NotifySync()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005660 File Offset: 0x00003860
		public void OnEventWithConfig(EventActivity activity, AnalyticsBasicData config)
		{
			this.OnEvent(activity);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000566C File Offset: 0x0000386C
		private void InternalInit()
		{
			try
			{
				if (this.TrackerConfig.TrackerQueueCapacity <= 0)
				{
					this.mActivityCollection = new BlockingCollection<BaseActivity>();
				}
				else
				{
					this.mActivityCollection = new BlockingCollection<BaseActivity>(this.TrackerConfig.TrackerQueueCapacity);
				}
				this.mTrackDir = Path.Combine(this.TrackerConfig.AppFolder, "track");
				if (!Directory.Exists(this.mTrackDir))
				{
					Directory.CreateDirectory(this.mTrackDir);
				}
				this.mScanDir = Path.Combine(this.TrackerConfig.AppFolder, "scan");
				if (!Directory.Exists(this.mScanDir))
				{
					Directory.CreateDirectory(this.mScanDir);
				}
				foreach (FileInfo file in new DirectoryInfo(this.mTrackDir).EnumerateFiles())
				{
					this.MoveFileToDataDir(file);
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			ThreadEx.Run(new ThreadStart(this.ConsumeActivities));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005784 File Offset: 0x00003984
		private void InternalOnEvent(BaseActivity activity)
		{
			try
			{
				if (!this.mActivityCollection.IsAddingCompleted)
				{
					this.mActivityCollection.TryAdd(activity);
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000057C8 File Offset: 0x000039C8
		private void ConsumeActivities()
		{
			try
			{
				foreach (BaseActivity baseActivity in this.mActivityCollection.GetConsumingEnumerable())
				{
					string text = string.Empty;
					if (baseActivity is FullActivity)
					{
						text = this.GetFullActivityString((FullActivity)baseActivity);
					}
					else if (baseActivity is EventActivity)
					{
						text = this.GetEventActivityString((EventActivity)baseActivity);
					}
					if (!string.IsNullOrWhiteSpace(text))
					{
						this.ConsumeSingleActivity(text);
					}
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000586C File Offset: 0x00003A6C
		private void ConsumeSingleActivity(string activityString)
		{
			if (!this.InitWriter())
			{
				return;
			}
			try
			{
				string str = Base64StringHelper.StringEncrypt(activityString + "\\r\\n");
				this.mCurTraceListener.WriteLine("|||" + str);
				if (this.mCurWriter != null && this.mCurWriter.BaseStream.Length >= (long)this.TrackerConfig.SingleFileSize)
				{
					this.mCurWriter = null;
					this.mCurTraceListener.Close();
					this.mCurTraceListener = null;
					foreach (FileInfo file in new DirectoryInfo(this.mTrackDir).EnumerateFiles())
					{
						this.MoveFileToDataDir(file);
					}
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000594C File Offset: 0x00003B4C
		private string GetEventActivityString(EventActivity activity)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			stringBuilder.AppendFormat("{0}~", activity.Version);
			stringBuilder.AppendFormat("{0}~", activity.StartTime);
			stringBuilder.AppendFormat("{0}~", activity.EndTime);
			string format = "{0}~";
			IEnvironmentProvider environmentProvider = this.EnvironmentProvider;
			stringBuilder.AppendFormat(format, DefaultTracker.ReplaceInvalidString((environmentProvider != null) ? environmentProvider.IpAddr : null));
			string format2 = "{0}~";
			IProductInfoProvider productInfoProvider = this.ProductInfoProvider;
			stringBuilder.AppendFormat(format2, DefaultTracker.ReplaceInvalidString((productInfoProvider != null) ? productInfoProvider.ProductId : null));
			string format3 = "{0}~";
			IEnvironmentProvider environmentProvider2 = this.EnvironmentProvider;
			stringBuilder.AppendFormat(format3, DefaultTracker.ReplaceInvalidString((environmentProvider2 != null) ? environmentProvider2.Hardware : null));
			string format4 = "{0}~";
			IEnvironmentProvider environmentProvider3 = this.EnvironmentProvider;
			stringBuilder.AppendFormat(format4, DefaultTracker.ReplaceInvalidString((environmentProvider3 != null) ? environmentProvider3.Os : null));
			string format5 = "{0}~";
			IEnvironmentProvider environmentProvider4 = this.EnvironmentProvider;
			stringBuilder.AppendFormat(format5, DefaultTracker.ReplaceInvalidString((environmentProvider4 != null) ? environmentProvider4.SoftwareDependency : null));
			string format6 = "{0}~";
			IProductInfoProvider productInfoProvider2 = this.ProductInfoProvider;
			stringBuilder.AppendFormat(format6, DefaultTracker.ReplaceInvalidString((productInfoProvider2 != null) ? productInfoProvider2.ActiveUser : null));
			string format7 = "{0}~";
			IProductInfoProvider productInfoProvider3 = this.ProductInfoProvider;
			stringBuilder.AppendFormat(format7, DefaultTracker.ReplaceInvalidString((productInfoProvider3 != null) ? productInfoProvider3.Organization : null));
			stringBuilder.AppendFormat("{0}~", string.Empty);
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Passive));
			stringBuilder.AppendFormat("{0}~", string.Empty);
			stringBuilder.AppendFormat("{0}~", string.Empty);
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.FromPos));
			string format8 = "{0}~";
			ITrackerConfig trackerConfig = this.TrackerConfig;
			stringBuilder.AppendFormat(format8, DefaultTracker.ReplaceInvalidString((trackerConfig != null) ? trackerConfig.AppKey : null));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ActionId));
			stringBuilder.AppendFormat("{0}~", string.Empty);
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Request));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Params));
			stringBuilder.AppendFormat("{0}~", string.Empty);
			string format9 = "{0}~";
			IProductInfoProvider productInfoProvider4 = this.ProductInfoProvider;
			stringBuilder.AppendFormat(format9, DefaultTracker.ReplaceInvalidString((productInfoProvider4 != null) ? productInfoProvider4.GroupId : null));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.RetCode));
			stringBuilder.AppendFormat("{0}", DefaultTracker.ReplaceInvalidString(activity.RetInfo));
			return stringBuilder.ToString();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005BDC File Offset: 0x00003DDC
		private string GetFullActivityString(FullActivity activity)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Version));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.StartTime.ToString()));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.EndTime.ToString()));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.IpAddr));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ProductId));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Hardware));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Os));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.SoftwareDependency));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ActiveUser));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Organization));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ActiveType));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Passive));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.PassiveType));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.FromProduct));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.FromPos));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.AppKey));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ActionId));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.ActionType));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Request));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.Params));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.GroupType));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.GroupId));
			stringBuilder.AppendFormat("{0}~", DefaultTracker.ReplaceInvalidString(activity.RetCode));
			stringBuilder.AppendFormat("{0}", DefaultTracker.ReplaceInvalidString(activity.RetInfo));
			return stringBuilder.ToString();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005E30 File Offset: 0x00004030
		private bool InitWriter()
		{
			if (this.mCurTraceListener != null)
			{
				return true;
			}
			string arg = "proc";
			try
			{
				arg = Process.GetCurrentProcess().Id.ToString();
			}
			catch
			{
			}
			try
			{
				string path = string.Format("{0}_{1}.{2}", arg, UnixTimeHelper.Now, "padat");
				this.mCurTraceListener = new TextWriterTraceListener(Path.Combine(this.mTrackDir, path));
				this.mCurWriter = (this.mCurTraceListener.Writer as StreamWriter);
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
				return false;
			}
			return true;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005ED8 File Offset: 0x000040D8
		private bool MoveFileToDataDir(FileInfo file)
		{
			try
			{
				if (file == null)
				{
					return false;
				}
				string text = Path.Combine(this.mScanDir, DateTime.Now.ToString("yyyyMMdd"));
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				File.Move(file.FullName, Path.Combine(text, file.Name));
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
				return false;
			}
			return true;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005F50 File Offset: 0x00004150
		private static string ReplaceInvalidString(string data)
		{
			if (string.IsNullOrWhiteSpace(data))
			{
				return string.Empty;
			}
			return data.Replace("~", " ").Replace(Environment.NewLine, " ").Replace("\\r\\n", " ");
		}

		// Token: 0x04000078 RID: 120
		private BlockingCollection<BaseActivity> mActivityCollection;

		// Token: 0x04000079 RID: 121
		private TextWriterTraceListener mCurTraceListener;

		// Token: 0x0400007A RID: 122
		private StreamWriter mCurWriter;

		// Token: 0x0400007B RID: 123
		private string mTrackDir;

		// Token: 0x0400007C RID: 124
		private string mScanDir;
	}
}
