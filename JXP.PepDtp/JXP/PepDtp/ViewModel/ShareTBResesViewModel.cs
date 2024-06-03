using System;
using System.IO;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.Utilities;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000074 RID: 116
	public class ShareTBResesViewModel : BaseModel
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00028BB5 File Offset: 0x00026DB5
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00028BBD File Offset: 0x00026DBD
		public bool SchoolChecked
		{
			get
			{
				return this.mSchoolChecked;
			}
			set
			{
				this.mSchoolChecked = value;
				base.OnPropertyChanged<bool>(() => this.SchoolChecked);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00028BFB File Offset: 0x00026DFB
		public void ResetData()
		{
			this.SchoolChecked = false;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00028C04 File Offset: 0x00026E04
		public bool UpdateShareTbData(string userId, string tbId)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(tbId))
			{
				this.ResetData();
				ShareGroupByTextbookJsonModel shareTbResesInfo = this.GetShareTbResesInfo(userId, tbId);
				if (shareTbResesInfo != null && shareTbResesInfo.Result == "110")
				{
					string rangeType = shareTbResesInfo.RangeType;
					if (!(rangeType == "0"))
					{
						if (!(rangeType == "1") && !(rangeType == "2") && !(rangeType == "3"))
						{
							if (rangeType == "4")
							{
								this.SchoolChecked = true;
							}
						}
					}
					else
					{
						this.SchoolChecked = false;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00028CAC File Offset: 0x00026EAC
		public ShareGroupByTextbookJsonModel GetShareTbResesInfo(string userId, string tbId)
		{
			ShareGroupByTextbookJsonModel result = null;
			try
			{
				string jsonStr = WebRequestHelper.HttpGetRequest(string.Format("group/getGroupsByTextbook.json?userId={0}&textbookId={1}", userId, tbId), new int?(30000), 0, false);
				result = this.mJsonOperate.JsonsDeserialize<ShareGroupByTextbookJsonModel>(jsonStr);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "获取分享教材信息出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			return result;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00028D20 File Offset: 0x00026F20
		public bool SubmitShareTbData(string shareTbId)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(shareTbId))
				{
					string text = "0";
					string empty = string.Empty;
					if (this.SchoolChecked)
					{
						text = "4";
					}
					result = this.ShareTextBookRequest(shareTbId, text, empty);
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200043",
						Passive = ((shareTbId.Length > 13) ? shareTbId.Substring(0, 13) : shareTbId),
						FromPos = ShareTBResesViewModel.mClassPath + ".SubmitShareTbData",
						Params = string.Format(string.Concat(new string[]
						{
							"分享教材id+用户id:",
							shareTbId,
							",分享范围:",
							text,
							",分享群组:",
							empty
						}), new object[0]),
						Request = Path.Combine(ConfigHelper.WebServerUrl, "tbuser/shareTextbook.json?userCtreeId={0}&groupIds={1}&rangeType={2}"),
						RetInfo = result.ToString()
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "分享教材出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			return result;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00028E48 File Offset: 0x00027048
		private bool ShareTextBookRequest(string shareTbId, string rangeFlag, string groupIds)
		{
			bool result = false;
			try
			{
				result = WebRequestHelper.WebSubmitDataRequest(string.Format("tbuser/shareTextbook.json?userCtreeId={0}&groupIds={1}&rangeType={2}", shareTbId, groupIds, rangeFlag));
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "请求WEB提交分享教材数据出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			return result;
		}

		// Token: 0x04000429 RID: 1065
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"ViewModel",
			"ShareTBResesViewModel"
		});

		// Token: 0x0400042A RID: 1066
		private HttpHelper mHttpOperate = new HttpHelper();

		// Token: 0x0400042B RID: 1067
		private JsonHelper mJsonOperate = new JsonHelper();

		// Token: 0x0400042C RID: 1068
		private bool mSchoolChecked;
	}
}
