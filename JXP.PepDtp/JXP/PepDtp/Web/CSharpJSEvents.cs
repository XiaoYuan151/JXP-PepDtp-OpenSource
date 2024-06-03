using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JXP.Data;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.DataAccess;
using JXP.PepDtp.Model;
using JXP.PepUtility;
using JXP.Utilities;

namespace JXP.PepDtp.Web
{
	// Token: 0x0200000D RID: 13
	[ComVisible(true)]
	public class CSharpJSEvents
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00009184 File Offset: 0x00007384
		public void SaveLoginInfo(string username, string pwd, bool isAutoLogin)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"SaveLoginInfo??<*>()??",
				username,
				"&&<*>&&",
				pwd,
				"&&<*>&&",
				isAutoLogin.ToString()
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000091D0 File Offset: 0x000073D0
		public void SaveLoginResultInfo(string retJson)
		{
			WindowMessages.SendMessage2WinByName("SaveLoginResultInfo??<*>()??" + retJson, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000091E8 File Offset: 0x000073E8
		public void SchoolSelected(string orgIds, string schoolId, string schoolName, string exit)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"SchoolSelected??<*>()??",
				orgIds,
				"&&<*>&&",
				schoolId,
				"&&<*>&&",
				schoolName,
				"&&<*>&&",
				exit
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000923B File Offset: 0x0000743B
		public void PcLogin(string menuType)
		{
			WindowMessages.SendMessage2WinByName("PcLogin??<*>()??" + menuType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009252 File Offset: 0x00007452
		public void ExitLogin()
		{
			WindowMessages.SendMessage2WinByName("ExitLogin", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009263 File Offset: 0x00007463
		public void ReBindingPhoneNumber(string phonenumber)
		{
			WindowMessages.SendMessage2WinByName("ReBindingPhoneNumber??<*>()??" + phonenumber, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000927A File Offset: 0x0000747A
		public void UploadUserPhoto(string uploadPath)
		{
			WindowMessages.SendMessage2WinByName("UploadUserPhoto??<*>()??" + uploadPath, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009291 File Offset: 0x00007491
		public void TextBookDownload(string tbId, string downType)
		{
			WindowMessages.SendMessage2WinByName("TextBookDownload??<*>()??" + tbId + "&&<*>&&" + downType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000092B0 File Offset: 0x000074B0
		public string GetDownloadTextBooks()
		{
			string text = string.Empty;
			try
			{
				LogHelper.Instance.Info("GetDownloadTextBooks Start");
				UserModel firstUser = new UserDataAccess().GetFirstUser();
				TransferTextbookModelList userDownloadedFinishTextbooks = this.mTextBookDataOper.GetUserDownloadedFinishTextbooks(firstUser.UserId);
				if (userDownloadedFinishTextbooks != null)
				{
					text = this.mJsonOper.ObservableCollectionToJson<TransferTextbookModel>(userDownloadedFinishTextbooks);
				}
				LogHelper.Instance.Info("GetDownloadTextBooks End.[" + text + "]");
			}
			catch (Exception ex)
			{
				text = string.Empty;
				LogHelper.Instance.Error("获取用户下载的教材列表(GetDownloadTextBooks)出错：" + ex.Message);
			}
			return text;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009350 File Offset: 0x00007550
		public void CancelDownloadTextBookFiles(string tbId)
		{
			WindowMessages.SendMessage2WinByName("CancelDownloadTextBookFiles??<*>()??" + tbId, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009367 File Offset: 0x00007567
		public bool DeleteDownloadedTextBook(string tbId)
		{
			bool result = true;
			WindowMessages.SendMessage2WinByName("DeleteDownloadedTextBook??<*>()??" + tbId, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000937F File Offset: 0x0000757F
		public void MyResourceDownload(string resId, string strType)
		{
			WindowMessages.SendMessage2WinByName("MyResourceDownload??<*>()??" + resId + "&&<*>&&" + strType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000939C File Offset: 0x0000759C
		public void MyResourceDownloadExt(string resId, string strType)
		{
			WindowMessages.SendMessage2WinByName("MyResourceDownloadExt??<*>()??" + resId + "&&<*>&&" + strType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000093B9 File Offset: 0x000075B9
		public void ResourceListDownload(string resId, string posType)
		{
			WindowMessages.SendMessage2WinByName("ResourceListDownload??<*>()??" + resId + "&&<*>&&" + posType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000093D6 File Offset: 0x000075D6
		public void ShareResourceDrag(string resJson)
		{
			WindowMessages.SendMessage2WinByName("ShareResourceDrag??<*>()??" + resJson, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000093ED File Offset: 0x000075ED
		public void MyResourceDrag(string resJson, string posType)
		{
			WindowMessages.SendMessage2WinByName("MyResourceDrag??<*>()??" + resJson + "&&<*>&&" + posType, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000940A File Offset: 0x0000760A
		public void OpenTextBook(string tbId)
		{
			WindowMessages.SendMessage2WinByName("OpenTextBook??<*>()??" + tbId, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009424 File Offset: 0x00007624
		public string SelectMyResources(string resourcesList, string conditionsList)
		{
			string text = string.Empty;
			try
			{
				LogHelper.Instance.Info("SelectMyResources Start:[" + resourcesList + "]");
				text = this.SelectMyResourcesMethod(resourcesList, conditionsList);
				LogHelper.Instance.Info("SelectMyResources End:[" + text + "]");
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("联网状态点击个人中心我的资源获取交互数据(SelectMyResources)出错：" + ex.Message);
			}
			return text;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000094A4 File Offset: 0x000076A4
		private string SelectMyResourcesMethod(string resourcesList, string conditionsList)
		{
			string result = string.Empty;
			try
			{
				BothSideUserResourceJsonModel bothSideUserResourceJsonModel = new BothSideUserResourceJsonModel();
				InteractiveUserResourcesDataModel interactiveUserResourcesDataModel = this.mJsonOper.JsonsDeserialize<InteractiveUserResourcesDataModel>(conditionsList);
				if (interactiveUserResourcesDataModel.Type == 1)
				{
					this.TotalUserResourcesList.Clear();
					if (!string.IsNullOrEmpty(resourcesList))
					{
						List<UserResourceModel> list = this.mJsonOper.JSONStringToList<UserResourceModel>(resourcesList);
						List<UserResourceModel> list2 = this.mUserResourceDA.GetUserResourcesByUserID(interactiveUserResourcesDataModel.UserId);
						List<UserResourceModel> list3;
						if (list2 == null)
						{
							list3 = null;
						}
						else
						{
							IEnumerable<UserResourceModel> enumerable = from a in list2
							where a.PvtbizType != "1"
							select a;
							list3 = ((enumerable != null) ? enumerable.ToList<UserResourceModel>() : null);
						}
						list2 = list3;
						if (list != null)
						{
							using (List<UserResourceModel>.Enumerator enumerator = list.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									UserResourceModel item = enumerator.Current;
									if (((list2 != null) ? (from t in list2
									where t.ID == item.ID
									select t).FirstOrDefault<UserResourceModel>() : null) == null)
									{
										item.Ex1 = "云端";
										this.TotalUserResourcesList.Add(item);
									}
								}
							}
						}
						if (list2 == null)
						{
							goto IL_292;
						}
						using (List<UserResourceModel>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								UserResourceModel item = enumerator.Current;
								UserResourceModel userResourceModel = (list != null) ? (from t in list
								where t.ID == item.ID
								select t).FirstOrDefault<UserResourceModel>() : null;
								if (userResourceModel == null)
								{
									item.Ex1 = "本地";
								}
								else if (item.PosType == 2)
								{
									item.State = userResourceModel.State;
									item.Ex1 = "云端";
								}
								else
								{
									item.State = userResourceModel.State;
									item.Ex1 = "两端";
								}
								this.TotalUserResourcesList.Add(item);
							}
							goto IL_292;
						}
					}
					List<UserResourceModel> userResourcesByUserID = this.mUserResourceDA.GetUserResourcesByUserID(interactiveUserResourcesDataModel.UserId);
					List<UserResourceModel> list4;
					if (userResourcesByUserID == null)
					{
						list4 = null;
					}
					else
					{
						IEnumerable<UserResourceModel> enumerable2 = from a in userResourcesByUserID
						where a.PvtbizType != "1"
						select a;
						list4 = ((enumerable2 != null) ? enumerable2.ToList<UserResourceModel>() : null);
					}
					foreach (UserResourceModel userResourceModel2 in list4)
					{
						userResourceModel2.Ex1 = "本地";
						this.TotalUserResourcesList.Add(userResourceModel2);
					}
					IL_292:
					List<UserResourceModel> list5 = this.QueryMyResources(this.TotalUserResourcesList, interactiveUserResourcesDataModel);
					bothSideUserResourceJsonModel.UserResourcesList = (from t in list5
					orderby t.ModifyTime descending
					select t).Skip((interactiveUserResourcesDataModel.PageNumber - 1) * interactiveUserResourcesDataModel.PageSize).Take(interactiveUserResourcesDataModel.PageSize).ToList<UserResourceModel>();
					bothSideUserResourceJsonModel.TotalPage = this.ComputePages(list5.Count, interactiveUserResourcesDataModel.PageSize).ToString();
					result = this.mJsonOper.ScriptSerialize<BothSideUserResourceJsonModel>(bothSideUserResourceJsonModel);
				}
				else
				{
					List<UserResourceModel> list6 = this.QueryMyResources(this.TotalUserResourcesList, interactiveUserResourcesDataModel);
					bothSideUserResourceJsonModel.UserResourcesList = (from t in list6
					orderby t.ModifyTime descending
					select t).Skip((interactiveUserResourcesDataModel.PageNumber - 1) * interactiveUserResourcesDataModel.PageSize).Take(interactiveUserResourcesDataModel.PageSize).ToList<UserResourceModel>();
					bothSideUserResourceJsonModel.TotalPage = this.ComputePages(list6.Count, interactiveUserResourcesDataModel.PageSize).ToString();
					result = this.mJsonOper.ScriptSerialize<BothSideUserResourceJsonModel>(bothSideUserResourceJsonModel);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("联网状态点击个人中心我的资源获取交互数据(SelectMyResources)出错：" + ex.Message);
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000098E8 File Offset: 0x00007AE8
		private List<UserResourceModel> QueryMyResources(List<UserResourceModel> list, InteractiveUserResourcesDataModel item)
		{
			List<UserResourceModel> list2 = new List<UserResourceModel>(list);
			if (!string.IsNullOrEmpty(item.Dzwjlx))
			{
				list2 = (from t in list2
				where t.Dzwjlx == item.Dzwjlx
				select t).ToList<UserResourceModel>();
			}
			if (!string.IsNullOrEmpty(item.Ex_zynrlx))
			{
				list2 = (from t in list2
				where t.ExZynrlID == item.Ex_zynrlx
				select t).ToList<UserResourceModel>();
			}
			if (!string.IsNullOrEmpty(item.Fascicule))
			{
				list2 = (from t in list2
				where t.Fascicule == item.Fascicule
				select t).ToList<UserResourceModel>();
			}
			if (!string.IsNullOrEmpty(item.Zxxkc))
			{
				list2 = (from t in list2
				where t.Zxxkc == item.Zxxkc
				select t).ToList<UserResourceModel>();
			}
			if (!string.IsNullOrEmpty(item.Rkxd))
			{
				list2 = (from t in list2
				where t.Rkxd == item.Rkxd
				select t).ToList<UserResourceModel>();
			}
			if (!string.IsNullOrEmpty(item.RangeType))
			{
				if (item.RangeType == "0")
				{
					list2 = (from t in list2
					where t.RangeType == item.RangeType
					select t).ToList<UserResourceModel>();
				}
				else
				{
					list2 = (from t in list2
					where t.RangeType != "0"
					select t).ToList<UserResourceModel>();
				}
			}
			return list2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009A4C File Offset: 0x00007C4C
		private int ComputePages(int listcount, int pagesize)
		{
			int num = 0;
			try
			{
				num = listcount / pagesize;
				if (listcount % pagesize > 0)
				{
					num++;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("计算总页码数(ComputePages)出错：" + ex.Message);
			}
			return num;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009A9C File Offset: 0x00007C9C
		public void SelectReadPageMyResources(string resourcesList, string conditionsList)
		{
			WindowMessages.SendMessage2WinByName("SelectReadPageMyResources??<*>()??" + resourcesList + "&&<*>&&" + conditionsList, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009AB9 File Offset: 0x00007CB9
		public void userResOption(string resId, string userId, string type)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"HandleResOption??<*>()??",
				resId,
				"&&<*>&&",
				userId,
				"&&<*>&&",
				type
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public void tbResOption(string resId, string userId, string type, string ex1)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"SyncResOption??<*>()??",
				resId,
				"&&<*>&&",
				userId,
				"&&<*>&&",
				type,
				"&&<*>&&",
				ex1
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009B47 File Offset: 0x00007D47
		public void ShareBothRes(string id, string rangetype, string groupids)
		{
			LogHelper.Instance.Info("ShareBothRes Start");
			this.mUserResourceDA.UpdateResourcesShareInfo2(rangetype, groupids, id);
			LogHelper.Instance.Info("ShareBothRes End");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009B75 File Offset: 0x00007D75
		public void xinJRes()
		{
			WindowMessages.SendMessage2WinByName("NewResource", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009B86 File Offset: 0x00007D86
		public void GetShareBook()
		{
			WindowMessages.SendMessage2WinByName("GetShareBook", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009B97 File Offset: 0x00007D97
		public void GetMySubscribe()
		{
			WindowMessages.SendMessage2WinByName("GetMySubscribe", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009BA8 File Offset: 0x00007DA8
		public void OpenMySubscribe(string userid, string path)
		{
			WindowMessages.SendMessage2WinByName("OpenMySubscribe??<*>()??" + userid + "&&<*>&&" + path, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009BC5 File Offset: 0x00007DC5
		public void GetMessageCount(string count)
		{
			WindowMessages.SendMessage2WinByName("GetMessageCount??<*>()??" + count, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009BDC File Offset: 0x00007DDC
		public void NavigateTo(string nPageIndex)
		{
			WindowMessages.SendMessage2WinByName("NavigateTo??<*>()??" + nPageIndex.ToString(), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public void resPreview(string strUrl, string strparamter)
		{
			WindowMessages.SendMessage2WinByName("resPreview??<*>()??" + strUrl + "&&<*>&&" + strparamter, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009C15 File Offset: 0x00007E15
		public void UserResourcePreview(string reskeyId)
		{
			WindowMessages.SendMessage2WinByName("UserResourcePreview??<*>()??" + reskeyId, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009C2C File Offset: 0x00007E2C
		public void UserOffLine()
		{
			WindowMessages.SendMessage2WinByName("UserOffLine", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009C40 File Offset: 0x00007E40
		public string GetTBDownloadState(string tbId)
		{
			LogHelper.Instance.Info("GetTBDownloadState Start");
			string result = "0";
			try
			{
				UserModel firstUser = new UserDataAccess().GetFirstUser();
				if (firstUser != null && new TransferTextBookDataAccess().GetTransTextBook(firstUser.UserId, tbId) != null)
				{
					result = "1";
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("获取书籍是否下载(GetTBDownloadState)出错：" + ex.Message);
			}
			LogHelper.Instance.Info("GetTBDownloadState End");
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009CC8 File Offset: 0x00007EC8
		public void buriedFun(string actionId, string passive, string fromPos, string request, string strparams)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"buriedFun??<*>()??",
				actionId,
				"&&<*>&&",
				passive,
				"&&<*>&&",
				fromPos,
				"&&<*>&&",
				request,
				"&&<*>&&",
				strparams
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009D2A File Offset: 0x00007F2A
		public void downloadApplyTool(string applyCenterJson)
		{
			WindowMessages.SendMessage2WinByName("downloadApplyTool??<*>()??" + applyCenterJson, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009D41 File Offset: 0x00007F41
		public void DelApplyTool(string applyId)
		{
			WindowMessages.SendMessage2WinByName("DelApplyTool??<*>()??" + applyId, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009D58 File Offset: 0x00007F58
		public string GetInstallApplyTools()
		{
			LogHelper.Instance.Info("GetInstallApplyTools Start");
			string result = string.Empty;
			try
			{
				UserModel firstUser = new UserDataAccess().GetFirstUser();
				if (firstUser == null)
				{
					return result;
				}
				List<AppToolsModel> applyTools = this.mApplyToolsDA.GetApplyTools(firstUser.UserId);
				if (applyTools != null)
				{
					result = this.mJsonOper.ListToJson<AppToolsModel>(applyTools);
				}
			}
			catch (Exception ex)
			{
				result = string.Empty;
				LogHelper.Instance.Error("获取用户已经安装的应用工具(GetInstallApplyTools)出错：" + ex.ToString());
			}
			LogHelper.Instance.Info("GetInstallApplyTools End");
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public void SaveText(string strContent, string title, bool isClose)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"SaveText??<*>()??",
				strContent,
				"&&<*>&&",
				title,
				"&&<*>&&",
				isClose.ToString()
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009E44 File Offset: 0x00008044
		public void DeleteText()
		{
			WindowMessages.SendMessage2WinByName("DeleteText", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009E55 File Offset: 0x00008055
		public void CloseText()
		{
			WindowMessages.SendMessage2WinByName("CloseText", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009E66 File Offset: 0x00008066
		public void IsEdit(bool isEdit)
		{
			WindowMessages.SendMessage2WinByName("IsEdit??<*>()??" + isEdit.ToString(), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009E83 File Offset: 0x00008083
		public void ExpandText(int flg)
		{
			WindowMessages.SendMessage2WinByName("ExpandText??<*>()??" + flg.ToString(), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00009EA0 File Offset: 0x000080A0
		public void EditText()
		{
			WindowMessages.SendMessage2WinByName("EditText", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009EB1 File Offset: 0x000080B1
		public void OnTextClick()
		{
			WindowMessages.SendMessage2WinByName("OnTextClick", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009EC2 File Offset: 0x000080C2
		public void StartPepClass(string data)
		{
			WindowMessages.SendMessage2WinByName("StartPepClass", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009ED3 File Offset: 0x000080D3
		public void openPepClassFile(string classId, string downloadUrl)
		{
			WindowMessages.SendMessage2WinByName("openPepClassFile??<*>()??" + classId + "&&<*>&&" + downloadUrl, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009EF0 File Offset: 0x000080F0
		public void goback()
		{
			WindowMessages.SendMessage2WinByName("goback", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009F01 File Offset: 0x00008101
		public void ShowDebugWindow()
		{
			WindowMessages.SendMessage2WinByName("ShowDebugWindow", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009F12 File Offset: 0x00008112
		public void ShowWebDebugWindow(int flag)
		{
			WindowMessages.SendMessage2WinByName("ShowWebDebugWindow??<*>()??" + flag.ToString(), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009F2F File Offset: 0x0000812F
		public void OpenCharacter(string character)
		{
			WindowMessages.SendMessage2WinByName("OpenCharacter??<*>()??" + character, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009F48 File Offset: 0x00008148
		public void addAppendixResByhomework(string tbid, string tbName, string ctreeId, string ctreeName)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"addAppendixResByhomework??<*>()??",
				tbid,
				"&&<*>&&",
				tbName,
				"&&<*>&&",
				ctreeId,
				"&&<*>&&",
				ctreeName
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009F9C File Offset: 0x0000819C
		public void addmyResByhomework(string tbid, string tbName, string ctreeId, string ctreeName, string resLst)
		{
			WindowMessages.SendMessage2WinByName(string.Concat(new string[]
			{
				"addmyResByhomework??<*>()??",
				tbid,
				"&&<*>&&",
				tbName,
				"&&<*>&&",
				ctreeId,
				"&&<*>&&",
				ctreeName,
				"&&<*>&&",
				resLst
			}), SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009FFE File Offset: 0x000081FE
		public void GetloginType()
		{
			WindowMessages.SendMessage2WinByName("GetloginType", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A00F File Offset: 0x0000820F
		public void openActivityVideo()
		{
			WindowMessages.SendMessage2WinByName("openActivityVideo", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A020 File Offset: 0x00008220
		public void openMyPc(string url)
		{
			WindowMessages.SendMessage2WinByName("openMyPc??<*>()??" + url, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A037 File Offset: 0x00008237
		public void notifyInfo(string msg)
		{
			WindowMessages.SendMessage2WinByName("notifyInfo??<*>()??" + msg, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A04E File Offset: 0x0000824E
		public void resAddCourse(string data)
		{
			WindowMessages.SendMessage2WinByName("resAddCourse??<*>()??" + data, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A065 File Offset: 0x00008265
		public void addHomework(string data)
		{
			WindowMessages.SendMessage2WinByName("addHomework??<*>()??" + data, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x04000046 RID: 70
		private HttpHelper mHttpOper = new HttpHelper();

		// Token: 0x04000047 RID: 71
		private UpLoadHelper mUploadOper = new UpLoadHelper();

		// Token: 0x04000048 RID: 72
		private LoginHelper mLoginOper = new LoginHelper();

		// Token: 0x04000049 RID: 73
		private TransferTextBookDataAccess mTextBookDataOper = new TransferTextBookDataAccess();

		// Token: 0x0400004A RID: 74
		private ApplyToolsDataAccess mApplyToolsDA = new ApplyToolsDataAccess();

		// Token: 0x0400004B RID: 75
		private JsonHelper mJsonOper = new JsonHelper();

		// Token: 0x0400004C RID: 76
		private UpLoadHelper uploadhelper = new UpLoadHelper();

		// Token: 0x0400004D RID: 77
		private DownloadHelper downloadhelper = new DownloadHelper();

		// Token: 0x0400004E RID: 78
		private List<UserResourceModel> TotalUserResourcesList = new List<UserResourceModel>();

		// Token: 0x0400004F RID: 79
		private UserResourceDataAccess mUserResourceDA = new UserResourceDataAccess();

		// Token: 0x04000050 RID: 80
		private ReducedImage mReduceImageOper = new ReducedImage();

		// Token: 0x04000051 RID: 81
		private object objLock = new object();

		// Token: 0x04000052 RID: 82
		private const string mSepFunc = "??<*>()??";

		// Token: 0x04000053 RID: 83
		private const string mSepParam = "&&<*>&&";
	}
}
