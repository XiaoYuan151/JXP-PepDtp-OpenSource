using System;
using System.Collections.Concurrent;
using System.Text;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Exceptions;
using JXP.DataAnalytics.Network;
using JXP.DataAnalytics.Platform;
using JXP.DataAnalytics.Utility;
using Pep.WindowsService.Data;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000035 RID: 53
	internal class RealtimeTracker : ITracker
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000613C File Offset: 0x0000433C
		public string Name { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006144 File Offset: 0x00004344
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000614C File Offset: 0x0000434C
		public IProductInfoProvider ProductInfoProvider { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006155 File Offset: 0x00004355
		public ITrackerConfig TrackerConfig { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000615D File Offset: 0x0000435D
		public IEnvironmentProvider EnvironmentProvider { get; }

		// Token: 0x06000172 RID: 370 RVA: 0x00006168 File Offset: 0x00004368
		public RealtimeTracker(string name, ITrackerConfig config, IEnvironmentProvider env, IProductInfoProvider product)
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

		// Token: 0x06000173 RID: 371 RVA: 0x000061C4 File Offset: 0x000043C4
		public void Close()
		{
			try
			{
				BlockingCollection<BaseActivity> blockingCollection = this.mActivityCollection;
				if (blockingCollection != null)
				{
					blockingCollection.CompleteAdding();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000061F8 File Offset: 0x000043F8
		public void OnEvent(EventActivity activity)
		{
			this.InternalOnEvent(activity);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006201 File Offset: 0x00004401
		public void OnEvent(FullActivity activity)
		{
			this.InternalOnEvent(activity);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000620A File Offset: 0x0000440A
		public void NotifySync()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000620C File Offset: 0x0000440C
		public void OnEventWithConfig(EventActivity activity, AnalyticsBasicData config)
		{
			this.ProductInfoProvider = config;
			this.TrackerConfig.AppKey = config.AppKey;
			this.OnEvent(activity);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000622D File Offset: 0x0000442D
		private void InternalInit()
		{
			this.mActivityCollection = new BlockingCollection<BaseActivity>(int.MaxValue);
			ThreadEx.Run(delegate()
			{
				this.ConsumeActivities();
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006254 File Offset: 0x00004454
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

		// Token: 0x0600017A RID: 378 RVA: 0x00006298 File Offset: 0x00004498
		private void ConsumeActivities()
		{
			try
			{
				FullActivity activity = null;
				foreach (BaseActivity baseActivity in this.mActivityCollection.GetConsumingEnumerable())
				{
					if (baseActivity is FullActivity)
					{
						activity = (FullActivity)baseActivity;
					}
					else if (baseActivity is EventActivity)
					{
						activity = this.FillActivity(new FullActivity((EventActivity)baseActivity));
					}
					this.PostData(activity);
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000632C File Offset: 0x0000452C
		private void PostData(FullActivity activity)
		{
			try
			{
				if (activity != null)
				{
					DefaultNetProvider.Current.SendTrackerData(this.GetFullActivityString(activity));
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000636C File Offset: 0x0000456C
		private FullActivity FillActivity(FullActivity activity)
		{
			if (activity == null)
			{
				return activity;
			}
			IEnvironmentProvider environmentProvider = this.EnvironmentProvider;
			activity.IpAddr = RealtimeTracker.ReplaceInvalidString((environmentProvider != null) ? environmentProvider.IpAddr : null);
			IProductInfoProvider productInfoProvider = this.ProductInfoProvider;
			activity.ProductId = RealtimeTracker.ReplaceInvalidString((productInfoProvider != null) ? productInfoProvider.ProductId : null);
			IEnvironmentProvider environmentProvider2 = this.EnvironmentProvider;
			activity.Hardware = RealtimeTracker.ReplaceInvalidString((environmentProvider2 != null) ? environmentProvider2.Hardware : null);
			IEnvironmentProvider environmentProvider3 = this.EnvironmentProvider;
			activity.Os = RealtimeTracker.ReplaceInvalidString((environmentProvider3 != null) ? environmentProvider3.Os : null);
			IEnvironmentProvider environmentProvider4 = this.EnvironmentProvider;
			activity.SoftwareDependency = RealtimeTracker.ReplaceInvalidString((environmentProvider4 != null) ? environmentProvider4.SoftwareDependency : null);
			IProductInfoProvider productInfoProvider2 = this.ProductInfoProvider;
			activity.ActiveUser = RealtimeTracker.ReplaceInvalidString((productInfoProvider2 != null) ? productInfoProvider2.ActiveUser : null);
			IProductInfoProvider productInfoProvider3 = this.ProductInfoProvider;
			activity.Organization = RealtimeTracker.ReplaceInvalidString((productInfoProvider3 != null) ? productInfoProvider3.Organization : null);
			ITrackerConfig trackerConfig = this.TrackerConfig;
			activity.AppKey = RealtimeTracker.ReplaceInvalidString((trackerConfig != null) ? trackerConfig.AppKey : null);
			IProductInfoProvider productInfoProvider4 = this.ProductInfoProvider;
			activity.GroupId = RealtimeTracker.ReplaceInvalidString((productInfoProvider4 != null) ? productInfoProvider4.GroupId : null);
			IProductInfoProvider productInfoProvider5 = this.ProductInfoProvider;
			activity.FromProduct = RealtimeTracker.ReplaceInvalidString((productInfoProvider5 != null) ? productInfoProvider5.ProductVersion : null);
			return activity;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000064A4 File Offset: 0x000046A4
		private string GetFullActivityString(FullActivity activity)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Version));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.StartTime.ToString()));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.EndTime.ToString()));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.IpAddr));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.ProductId));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Hardware));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Os));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.SoftwareDependency));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.ActiveUser));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Organization));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.ActiveType));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Passive));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.PassiveType));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.FromProduct));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.FromPos));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.AppKey));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.ActionId));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.ActionType));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Request));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.Params));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.GroupType));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.GroupId));
			stringBuilder.AppendFormat("{0}~", RealtimeTracker.ReplaceInvalidString(activity.RetCode));
			stringBuilder.AppendFormat("{0}", RealtimeTracker.ReplaceInvalidString(activity.RetInfo));
			stringBuilder.Append("\\r\\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006704 File Offset: 0x00004904
		private static string ReplaceInvalidString(string data)
		{
			if (string.IsNullOrWhiteSpace(data))
			{
				return string.Empty;
			}
			return data.Replace("~", " ").Replace(Environment.NewLine, " ").Replace("\\r\\n", " ");
		}

		// Token: 0x04000090 RID: 144
		private BlockingCollection<BaseActivity> mActivityCollection;
	}
}
