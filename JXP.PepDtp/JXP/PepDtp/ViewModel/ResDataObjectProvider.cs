using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows;
using JXP.DragHelper;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Windows;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200006B RID: 107
	public class ResDataObjectProvider : IDataObjectProvider
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x00025E48 File Offset: 0x00024048
		public virtual object GetDataObject(object paras)
		{
			return new object();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00025E50 File Offset: 0x00024050
		internal UserResourceJsonModel GetCenterResData(string resId, string resFlg, bool isCreateNewId = true)
		{
			UserResourceJsonModel result;
			try
			{
				if (resFlg == SdkConstants.RJ_CLOUD_RES_TYPE || resFlg == SdkConstants.NQ_RES_TYPE)
				{
					result = this.GetUserResourceJsonModelByYunResId(resId, isCreateNewId);
				}
				else
				{
					result = this.GetUserResourceJsonModel(resId);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
				CustomMessageBox.Info("数据取得失败，请重试!", "确定", "", null, WindowStartupLocation.CenterOwner, false);
				result = null;
			}
			return result;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00025EC8 File Offset: 0x000240C8
		private UserResourceJsonModel GetUserResourceJsonModel(string resId)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["resId"] = resId;
			new FormUrlEncodedContent(dictionary);
			string value = HttpHelper.HttpPost(ConfigHelper.WebServerUrl + "resuser/list.json", dictionary, new int?(30000), "", true);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<UserResourceJsonModel>(value);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00025F24 File Offset: 0x00024124
		private UserResourceJsonModel GetUserResourceJsonModelByYunResId(string resId, bool isCreateNewId = true)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["id"] = resId;
			new FormUrlEncodedContent(dictionary);
			string value = HttpHelper.HttpPost(ConfigHelper.WebServerUrl + "resource/yunRes.json", dictionary, new int?(30000), "", true);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			RJCloudResourceJsonModel rjcloudResourceJsonModel = JsonConvert.DeserializeObject<RJCloudResourceJsonModel>(value);
			if (rjcloudResourceJsonModel == null || rjcloudResourceJsonModel.Result != "110")
			{
				return null;
			}
			UserResourceModel userResourceModel = this.CloudResConvertUserRes(rjcloudResourceJsonModel.ResInfo, isCreateNewId);
			if (userResourceModel == null)
			{
				return null;
			}
			return new UserResourceJsonModel
			{
				Result = "110",
				UserResourcesList = new List<UserResourceModel>
				{
					userResourceModel
				}
			};
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00025FD0 File Offset: 0x000241D0
		public UserResourceModel CloudResConvertUserRes(RJCloudResourceModel cloudRes, bool isCreateNewId = true)
		{
			if (cloudRes == null)
			{
				return null;
			}
			string loginUserId = CommonParamter.Instance.LoginUserId;
			string userShowName = CommonParamter.Instance.UserShowName;
			string curTime = CommonHandle.GetCurTime();
			string filePath = cloudRes.FilePath;
			if (string.IsNullOrEmpty(filePath))
			{
				LogHelper.Instance.Error("人教云资源的云端路径为空，人教云资源id：" + cloudRes.Id);
				return null;
			}
			UserResourceModel userResourceModel = new UserResourceModel();
			userResourceModel.ResSourceType = SdkConstants.RJ_CLOUD_RES_TYPE;
			if (isCreateNewId)
			{
				userResourceModel.ID = cloudRes.OriTreeCode + SdkConstants.ConvertDateTimeToInt(DateTime.Now).ToString();
			}
			else
			{
				userResourceModel.ID = cloudRes.Id;
			}
			userResourceModel.Tbid = cloudRes.TbId;
			userResourceModel.TbName = TextBookInfoDataHelper.Instance.GetTextBookName(userResourceModel.Tbid);
			userResourceModel.Keywords = cloudRes.Keywords;
			userResourceModel.Resume = cloudRes.Resume;
			userResourceModel.Title = cloudRes.Title;
			userResourceModel.Year = new int?(cloudRes.Year);
			userResourceModel.Dzwjlx = cloudRes.Dzwjlx;
			userResourceModel.DzwjlxName = cloudRes.DzwjlxName;
			userResourceModel.Zylx = cloudRes.Zylx;
			userResourceModel.ZylxName = cloudRes.ZylxName;
			userResourceModel.Yhlx = cloudRes.Yhlx;
			userResourceModel.Mtgslx = cloudRes.Mtgslx;
			userResourceModel.SourceApp = cloudRes.SourceApp;
			userResourceModel.OriTreeCode = cloudRes.OriTreeCode;
			userResourceModel.OriTreeName = cloudRes.OriTreeName;
			userResourceModel.SEduCode = cloudRes.SEduCode;
			if (filePath.EndsWith("html", StringComparison.CurrentCultureIgnoreCase))
			{
				string fileName = Path.GetFileName(Path.GetDirectoryName(filePath));
				string fileName2 = Path.GetFileName(filePath);
				userResourceModel.FilePath = string.Concat(new string[]
				{
					"/my_cloud/",
					CommonParamter.Instance.OrgsPath,
					"/",
					loginUserId,
					"/sys/resuser/",
					fileName,
					"/",
					fileName2
				});
			}
			else if (filePath.EndsWith("images", StringComparison.CurrentCultureIgnoreCase))
			{
				string fileName3 = Path.GetFileName(filePath);
				userResourceModel.FilePath = string.Concat(new string[]
				{
					"/my_cloud/",
					CommonParamter.Instance.OrgsPath,
					"/",
					loginUserId,
					"/sys/resuser/",
					fileName3
				});
			}
			else
			{
				string text = userResourceModel.OriTreeCode + DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1) + Path.GetExtension(filePath);
				userResourceModel.FilePath = string.Concat(new string[]
				{
					"/my_cloud/",
					CommonParamter.Instance.OrgsPath,
					"/",
					loginUserId,
					"/sys/resuser/",
					text
				});
			}
			userResourceModel.SourcePath = filePath;
			userResourceModel.FileFormat = cloudRes.FileFormat;
			userResourceModel.FileSize = cloudRes.FileSize;
			userResourceModel.FileMd5 = cloudRes.FileMd5;
			userResourceModel.State = 100;
			userResourceModel.Creator = loginUserId;
			userResourceModel.CreatorName = userShowName;
			userResourceModel.CreateTime = curTime;
			userResourceModel.Modifier = loginUserId;
			userResourceModel.ModifierName = userShowName;
			userResourceModel.ModifyTime = curTime;
			if (userResourceModel.Tbid.Length == 13)
			{
				userResourceModel.Rkxd = userResourceModel.Tbid.Substring(1, 1);
				userResourceModel.Zxxkc = userResourceModel.Tbid.Substring(2, 2);
				userResourceModel.Nj = userResourceModel.Rkxd + userResourceModel.Tbid.Substring(7, 1);
				userResourceModel.Fascicule = userResourceModel.Tbid.Substring(8, 2);
			}
			userResourceModel.ExZynrlID = cloudRes.ExZynrlID;
			userResourceModel.ExZynrlName = cloudRes.ExZynrlName;
			userResourceModel.PosType = 1;
			userResourceModel.EcryType = cloudRes.EcryType;
			return userResourceModel;
		}
	}
}
