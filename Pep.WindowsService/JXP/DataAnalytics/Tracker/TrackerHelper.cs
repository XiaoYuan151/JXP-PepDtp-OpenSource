using System;
using System.Diagnostics;
using JXP.DataAnalytics.Network;
using JXP.DataAnalytics.Network.Model;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x02000036 RID: 54
	internal class TrackerHelper
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00006754 File Offset: 0x00004954
		public static void SyncConfig(INetProvider provider, ITrackerConfig config)
		{
			if (config == null || provider == null)
			{
				return;
			}
			try
			{
				TokenInfo tokenData = provider.GetTokenData();
				if (tokenData != null)
				{
					if (tokenData.Acks == "0")
					{
						config.AlwaysDeleteFile = true;
					}
					int num;
					if (int.TryParse(tokenData.Timeout, out num) && num > 0)
					{
						config.PostTimeout = num * 1000;
					}
					if (int.TryParse(tokenData.SingleFileSize, out num) && num > 0)
					{
						config.SingleFileSize = num * 1024;
					}
					if (int.TryParse(tokenData.UploadInterval, out num) && num > 0)
					{
						config.ScanPeriod = TimeSpan.FromSeconds((double)num);
					}
					if (int.TryParse(tokenData.MaxRecordCountWhenPost, out num) && num > 0)
					{
						config.MaxRecordCountWhenPost = num;
					}
				}
			}
			catch (Exception value)
			{
				Trace.WriteLine(value);
			}
		}
	}
}
