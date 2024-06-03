using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000073 RID: 115
	public class ShareResViewModel : BaseModel
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0002841E File Offset: 0x0002661E
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00028428 File Offset: 0x00026628
		public bool AllStationChecked
		{
			get
			{
				return this.mAllStationChecked;
			}
			set
			{
				this.mAllStationChecked = value;
				if (this.mAllStationChecked)
				{
					this.SchoolChecked = false;
					this.GroupChecked = false;
				}
				base.OnPropertyChanged<bool>(() => this.AllStationChecked);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00028487 File Offset: 0x00026687
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00028490 File Offset: 0x00026690
		public bool SchoolChecked
		{
			get
			{
				return this.mSchoolChecked;
			}
			set
			{
				this.mSchoolChecked = value;
				if (this.SchoolChecked)
				{
					this.AllStationChecked = false;
				}
				base.OnPropertyChanged<bool>(() => this.SchoolChecked);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000284E8 File Offset: 0x000266E8
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000284F0 File Offset: 0x000266F0
		public bool GroupChecked
		{
			get
			{
				return this.mGroupChecked;
			}
			set
			{
				this.mGroupChecked = value;
				if (this.mGroupChecked)
				{
					this.AllStationChecked = false;
				}
				base.OnPropertyChanged<bool>(() => this.GroupChecked);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00028548 File Offset: 0x00026748
		public ObservableCollection<ShareResGroupModel> ShareResGroupList
		{
			get
			{
				return this.mShareResGroupList;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00028550 File Offset: 0x00026750
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x00028558 File Offset: 0x00026758
		public DoWorkEventArgs DoWorkAgrs { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00028561 File Offset: 0x00026761
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00028569 File Offset: 0x00026769
		public string UserId { get; set; }

		// Token: 0x0600084C RID: 2124 RVA: 0x00028572 File Offset: 0x00026772
		public void ResetData()
		{
			this.AllStationChecked = false;
			this.SchoolChecked = false;
			this.GroupChecked = false;
			this.ShareResGroupList.Clear();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00028594 File Offset: 0x00026794
		public Task<bool> UpdateShareResData(string userid, UserResourceModel userRes)
		{
			ShareResViewModel.<UpdateShareResData>d__29 <UpdateShareResData>d__;
			<UpdateShareResData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<UpdateShareResData>d__.<>4__this = this;
			<UpdateShareResData>d__.userid = userid;
			<UpdateShareResData>d__.userRes = userRes;
			<UpdateShareResData>d__.<>1__state = -1;
			<UpdateShareResData>d__.<>t__builder.Start<ShareResViewModel.<UpdateShareResData>d__29>(ref <UpdateShareResData>d__);
			return <UpdateShareResData>d__.<>t__builder.Task;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000285E8 File Offset: 0x000267E8
		public Task<ShareGroupResModel> GetShareResInfo(string userId)
		{
			ShareResViewModel.<GetShareResInfo>d__30 <GetShareResInfo>d__;
			<GetShareResInfo>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<ShareGroupResModel>.Create();
			<GetShareResInfo>d__.<>4__this = this;
			<GetShareResInfo>d__.userId = userId;
			<GetShareResInfo>d__.<>1__state = -1;
			<GetShareResInfo>d__.<>t__builder.Start<ShareResViewModel.<GetShareResInfo>d__30>(ref <GetShareResInfo>d__);
			return <GetShareResInfo>d__.<>t__builder.Task;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00028634 File Offset: 0x00026834
		public void SubmitShareResData(string userId, UserResourceModel userRes)
		{
			if (userRes == null || string.IsNullOrEmpty(userId))
			{
				return;
			}
			this.UserId = userId;
			if (this.SchoolChecked)
			{
				Md5ResultModel md5ResultModel = JsonConvert.DeserializeObject<Md5ResultModel>(HttpHelper.HttpGet(string.Concat(new string[]
				{
					ConfigHelper.WebServerUrl,
					"resuser/queryResMd5.json?res_md5=",
					userRes.FileMd5,
					"&res_id=",
					userRes.ID
				}), null, true, ""));
				if (md5ResultModel == null || md5ResultModel.State != 110)
				{
					if (this.DoWorkAgrs != null)
					{
						this.DoWorkAgrs.Result = "分享失败，本校内已有该资源";
					}
					return;
				}
			}
			if (!File.Exists(PathHelper.GetConvertPath(userRes.FullPath)))
			{
				this.PostShareData(userRes);
				return;
			}
			bool flag = UtilityHelper.IsUserResChanged(userRes);
			UserResourceModel cloudIUserRes = UserResourcesManager.Instance.GetCloudIUserRes(userRes.ID);
			if ((cloudIUserRes != null && userRes.ModifyTime.CompareTo(cloudIUserRes.ModifyTime) > 0) || flag)
			{
				this.UploadUserRes(userRes);
				return;
			}
			this.PostShareData(userRes);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00028738 File Offset: 0x00026938
		private void UploadUserRes(UserResourceModel userRes)
		{
			if (userRes == null)
			{
				return;
			}
			string serverSaveDir = PathHelper.GetServerSaveDir(userRes.FilePath);
			string convertPath = PathHelper.GetConvertPath(userRes.FullPath);
			if (!this.mUpload.UploadFile2(convertPath, serverSaveDir, "", null))
			{
				if (this.DoWorkAgrs != null)
				{
					this.DoWorkAgrs.Result = "Error";
				}
				return;
			}
			UserResourceModel userResourceModel = userRes.Clone();
			userResourceModel.State = 100;
			userResourceModel.H5Url = string.Empty;
			userResourceModel.H5PageNum = 0;
			userResourceModel.OffineH5Staus = 0;
			List<UserResourceModel> t = new List<UserResourceModel>
			{
				userResourceModel
			};
			string value = this.mJsonOperate.ListToJson<UserResourceModel>(t);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("resList", value);
			string text = HttpHelper.HttpPost(ConfigHelper.WebServerUrl + "resuser/save.json", dictionary, new int?(300000), "", true);
			ReturnJsonUserResourcesModel returnJsonUserResourcesModel = this.mJsonOperate.JsonsDeserialize<ReturnJsonUserResourcesModel>(text);
			if (((returnJsonUserResourcesModel != null) ? returnJsonUserResourcesModel.Result : null) == "110")
			{
				userRes.State = 100;
				userRes.PosType = 3;
				this.mUserResDA.UpdateUserRes(userRes);
				this.PostShareData(userRes);
				return;
			}
			LogHelper.Instance.Error("资源分享提交分数据失败:[" + text + "]。");
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00028878 File Offset: 0x00026A78
		public UserResourceJsonModel GetUserResesJsonModelByResIDs(string strUserResIDs)
		{
			UserResourceJsonModel result = null;
			if (string.IsNullOrEmpty(strUserResIDs))
			{
				return result;
			}
			string jsonStr = WebRequestHelper.HttpGetRequest(string.Format("resuser/list.json?resIds={0}", strUserResIDs), new int?(30000), 0, false);
			return this.mJsonOperate.JsonsDeserialize<UserResourceJsonModel>(jsonStr);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000288BC File Offset: 0x00026ABC
		private void ResUploadFailed(TransferFileModel transFile)
		{
			if (this.DoWorkAgrs != null)
			{
				this.DoWorkAgrs.Result = "Error";
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000288D8 File Offset: 0x00026AD8
		private void PostShareData(UserResourceModel userRes)
		{
			string text = "0";
			string text2 = string.Empty;
			if (this.AllStationChecked)
			{
				text = "2";
			}
			if (this.GroupChecked)
			{
				text = "3";
			}
			if (this.SchoolChecked)
			{
				text = "4";
			}
			IEnumerable<ShareResGroupModel> enumerable = from o in this.ShareResGroupList
			where o.IsChecked
			select o;
			if (enumerable != null && enumerable.Count<ShareResGroupModel>() > 0)
			{
				foreach (ShareResGroupModel shareResGroupModel in enumerable)
				{
					if (shareResGroupModel.IsChecked)
					{
						text2 = text2 + shareResGroupModel.GroupID + ",";
					}
				}
				text2 = text2.Remove(text2.Length - 1, 1);
			}
			string curTime = CommonHandle.GetCurTime();
			bool flag = this.ShareResRequest(userRes.ID, this.UserId, text, text2, curTime);
			if (flag)
			{
				if (text != "4")
				{
					this.CancelSelect(userRes.ID);
				}
				if (userRes != null)
				{
					userRes.GroupIds = text2;
					userRes.RangeType = text;
					userRes.ModifyTime = curTime;
				}
				this.mUserResDA.UpdateResourcesShareInfo(text, text2, curTime, userRes.ID);
			}
			else if (this.DoWorkAgrs != null)
			{
				this.DoWorkAgrs.Result = "Error";
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200134",
				Passive = userRes.ID,
				FromPos = ShareResViewModel.mClassPath + ".PostShareData",
				Params = "分享范围:" + text + ",分享群组:" + text2,
				Request = Path.Combine(ConfigHelper.WebServerUrl, string.Concat(new string[]
				{
					"resuser/resUserShair.json?id=",
					userRes.ID,
					"&userId=",
					this.UserId,
					"&range_type=",
					text,
					"&groupIds=",
					text2
				})),
				RetInfo = flag.ToString()
			});
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00028AF8 File Offset: 0x00026CF8
		private bool ShareResRequest(string resId, string userId, string rangeFlag, string groupIds, string modifyTime)
		{
			return WebRequestHelper.WebSubmitDataRequest(string.Format("resuser/resUserShair.json?id={0}&userId={1}&range_type={2}&groupIds={3}&s_modify_time={4}", new object[]
			{
				resId,
				userId,
				rangeFlag,
				groupIds,
				modifyTime
			}));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00028B25 File Offset: 0x00026D25
		private void CancelSelect(string resId)
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "tbuser/cancelSelect.json?res_id=" + resId, new int?(10000), true, "");
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("取消推优资:[{0}]源调用失败:[{1}]。", resId, arg));
				}
			});
		}

		// Token: 0x0400041E RID: 1054
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"ViewModel",
			"ShareResViewModel"
		});

		// Token: 0x0400041F RID: 1055
		private UserResourceDataAccess mUserResDA = new UserResourceDataAccess();

		// Token: 0x04000420 RID: 1056
		private Md5Helper mMd5Oper = new Md5Helper();

		// Token: 0x04000421 RID: 1057
		private JsonHelper mJsonOperate = new JsonHelper();

		// Token: 0x04000422 RID: 1058
		private UpLoadHelper mUpload = new UpLoadHelper();

		// Token: 0x04000423 RID: 1059
		private bool mAllStationChecked;

		// Token: 0x04000424 RID: 1060
		private bool mSchoolChecked;

		// Token: 0x04000425 RID: 1061
		private bool mGroupChecked;

		// Token: 0x04000426 RID: 1062
		private ObservableCollection<ShareResGroupModel> mShareResGroupList = new ObservableCollection<ShareResGroupModel>();
	}
}
