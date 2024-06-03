using System;
using System.Collections.Generic;
using System.Linq;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.Threading;
using pep.sdk.utility;
using pep.sdk.utility.Interfaces;

namespace JXP.PepDtp.DataStatistics
{
	// Token: 0x02000082 RID: 130
	public class StatisticsHelper : IDataStatistic
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002B6DA File Offset: 0x000298DA
		public static StatisticsHelper Instance
		{
			get
			{
				return StatisticsHelper.singleton.Value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002B6E6 File Offset: 0x000298E6
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0002B6EE File Offset: 0x000298EE
		public DateTime? OpenBooktime { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002B6F7 File Offset: 0x000298F7
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002B6FF File Offset: 0x000298FF
		public Dictionary<string, DateTime> OpenTimeDics { get; set; } = new Dictionary<string, DateTime>();

		// Token: 0x0600091B RID: 2331 RVA: 0x0002B708 File Offset: 0x00029908
		public void CloseBook(string bookid)
		{
			long num = 0L;
			try
			{
				if (this.OpenBooktime == null)
				{
					return;
				}
				DateTime now = DateTime.Now;
				num = ((now.Subtract(this.OpenBooktime.Value).TotalSeconds < 1.0) ? 1L : ((long)now.Subtract(this.OpenBooktime.Value).TotalSeconds));
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭教材统计阅读时长报错。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			try
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx600002",
					Passive = bookid,
					FromPos = "DataStatisticHelper.CloseBook",
					RetInfo = string.Format("ut:{0}", num)
				});
				TrackerManager.Tracker.NotifySync();
			}
			catch
			{
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002B814 File Offset: 0x00029A14
		public void OpenBook(string bookid)
		{
			this.UploadStatisticsInfo(bookid, "jx200125", "", "");
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002B82C File Offset: 0x00029A2C
		public void OpenAnnotRes(string resId, string resType)
		{
			this.OpenCommonRes(resId, "");
			if (resType == "5" || resType == "6")
			{
				return;
			}
			ResourcesModel nqResInfo = ResHelper.GetNqResInfo(resId);
			if (nqResInfo == null)
			{
				return;
			}
			this.UploadStatisticsInfo(resId, "jx200307", "", nqResInfo.Zynrlx);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002B882 File Offset: 0x00029A82
		public void UploadCreateResData(string resId, string zynelx)
		{
			this.UploadStatisticsInfo(resId, "jx200067", "", zynelx);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002B896 File Offset: 0x00029A96
		public void OpenCenterRes(string resId, string zynelx)
		{
			ThreadEx.Run(delegate()
			{
				this.UploadStatisticsInfo(resId, "jx200197", "", zynelx);
			});
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002B8C3 File Offset: 0x00029AC3
		public void OpenMyRes(string resId, string zynelx)
		{
			ThreadEx.Run(delegate()
			{
				this.UploadStatisticsInfo(resId, "jx200308", "", zynelx);
			});
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002B8F0 File Offset: 0x00029AF0
		public void DownloadRes(string resId)
		{
			ThreadEx.Run(delegate()
			{
				this.UploadStatisticsInfo(resId, "jx200301", "", "");
			});
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002B916 File Offset: 0x00029B16
		public void UploadCourseData(string courseId, string actionTitle)
		{
			ThreadEx.Run(delegate()
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = actionTitle,
					Passive = courseId,
					FromPos = "JXP.PepDtp.DataStatistics.StatisticsHelper.UploadCourseData"
				});
				this.UploadStatisticsInfo(courseId, actionTitle, "", "");
			});
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002B943 File Offset: 0x00029B43
		public void UpLoadCloseBookInfoAsync(string bookid, int nPageNum)
		{
			ThreadEx.Run(delegate()
			{
				this.UploadStatisticsInfo(string.Format("{0},{1}", bookid, nPageNum), "jx200701", "", "");
			});
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002B970 File Offset: 0x00029B70
		public void LoginCountStart()
		{
			this.SetOpenTime("LoginCountStart", "");
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002B984 File Offset: 0x00029B84
		public void LoginCountEnd()
		{
			string key = "LoginCountStart";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime value = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			long times = (long)DateTime.Now.Subtract(value).TotalSeconds;
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(ConfigHelper.WebServerUrl + string.Format("statistic/addOnlineTime.json?time={0}", times), null, true, "");
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error("用户登录时长统计：" + ex.ToString());
				}
			});
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		private void UploadStatisticsInfo(string passiveObj, string actiocTitle, string activeType = "", string passiveType = "")
		{
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("active_user", CommonParamter.Instance.LoginUserId);
				dictionary.Add("active_org", CommonParamter.Instance.OrgID);
				if (!string.IsNullOrEmpty(activeType))
				{
					dictionary.Add("active_type", activeType);
				}
				dictionary.Add("passive_obj", passiveObj);
				dictionary.Add("action_title", actiocTitle);
				if (!string.IsNullOrEmpty(passiveType))
				{
					dictionary.Add("passive_type", passiveType);
				}
				WebRequestHelper.HttpPostRequest("statistic/addActiveDetail.json", dictionary, new int?(30000), 0, false, true);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "上报统计数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		private void UploadReadingTime(string bookid, double dTime)
		{
			try
			{
				WebRequestHelper.HttpPostRequest("statistic/readingTime.json", new Dictionary<string, string>
				{
					{
						"userId",
						CommonParamter.Instance.LoginUserId
					},
					{
						"tb_id",
						bookid
					},
					{
						"time",
						dTime.ToString()
					}
				}, new int?(30000), 0, false, true);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "数据统计上报阅读时长失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002BB60 File Offset: 0x00029D60
		public void ExitLogin()
		{
			try
			{
				HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "p/user/Mlogout", null, true, "");
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("退出登录出错：" + ex.ToString());
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		public void SetOpenTime(string objId, string objType)
		{
			string text = objId + "_" + objType;
			DateTime now = DateTime.Now;
			if (this.OpenTimeDics == null)
			{
				this.OpenTimeDics = new Dictionary<string, DateTime>();
			}
			if (this.OpenTimeDics.Keys.Contains(text))
			{
				this.OpenTimeDics[text] = now;
				return;
			}
			this.OpenTimeDics.Add(text, now);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void CloseRes(string resId, string resType)
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002BC21 File Offset: 0x00029E21
		public void OpenTool(string toolId)
		{
			this.SetOpenTime(toolId, "tool");
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002BC30 File Offset: 0x00029E30
		public void CloseTool(string toolId)
		{
			string key = toolId + "_tool";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CloseTool", "jx510025", toolId, openTime);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002BC84 File Offset: 0x00029E84
		public void OpenKeJian(string resId, string resType)
		{
			this.SetOpenTime(resId, "kejian");
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002BC94 File Offset: 0x00029E94
		public void CloseKeJian(string resId, string resType)
		{
			string key = resId + "_kejian";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CloseKeJian", "jx400012", resId, openTime);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002BCE8 File Offset: 0x00029EE8
		public void EditeKeJianStart(string resId, string resType)
		{
			this.SetOpenTime(resId, "editekejian");
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002BCF8 File Offset: 0x00029EF8
		public void EditeKeJianEnd(string resId, string resType)
		{
			string key = resId + "_editekejian";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CloseKeJian", "jx400100", resId, openTime);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public void OpenUserRes(string resId, string resType)
		{
			this.OpenCommonRes(resId, resType);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void CloseUserRes(string resId, string resType)
		{
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public void OpenCloudRes(string resId, string resType)
		{
			this.OpenCommonRes(resId, resType);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void CloseCloudRes(string resId, string resType)
		{
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002BD56 File Offset: 0x00029F56
		public void OpenCommonRes(string resId, string resType)
		{
			this.SetOpenTime(resId, "commonres");
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002BD64 File Offset: 0x00029F64
		public void CloseCommonRes(string resId, string resType)
		{
			string key = resId + "_commonres";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CloseCommonRes", "jx600001", resId, openTime);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002BDB8 File Offset: 0x00029FB8
		private void AddTrackerData(string fromPos, string actionId, string passive, DateTime openTime)
		{
			TaskEx.Run(delegate()
			{
				try
				{
					long num = (long)DateTime.Now.Subtract(openTime).TotalSeconds;
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = actionId,
						Passive = passive,
						FromPos = fromPos,
						RetInfo = string.Format("ut:{0}", num)
					});
				}
				catch
				{
				}
			});
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		public void CrouseEditeStart(string resId, string resType)
		{
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx510010",
				Passive = resId,
				FromPos = "DataStatisticHelper.CrouseEditeStart",
				Params = string.Format("课程ID:" + resId, new object[0])
			});
			this.SetOpenTime(resId, "CrouseEdite");
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002BE54 File Offset: 0x0002A054
		public void CrouseEditeEnd(string resId, string resType)
		{
			string key = resId + "_CrouseEdite";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CrouseClassEnd", "jx510036", resId, openTime);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		public void CrouseClassStart(string resId, string resType)
		{
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx510008",
				Passive = resId,
				Params = string.Format("课程ID:" + resId, new object[0])
			});
			this.SetOpenTime(resId, "CrouseClass");
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002BF00 File Offset: 0x0002A100
		public void CrouseClassEnd(string resId, string resType)
		{
			string key = resId + "_CrouseClass";
			if (!this.OpenTimeDics.ContainsKey(key))
			{
				return;
			}
			DateTime openTime = this.OpenTimeDics[key];
			this.OpenTimeDics.Remove(key);
			this.AddTrackerData("DataStatisticHelper.CrouseClassEnd", "jx510009", resId, openTime);
		}

		// Token: 0x0400048B RID: 1163
		private static readonly Lazy<StatisticsHelper> singleton = new Lazy<StatisticsHelper>();
	}
}
