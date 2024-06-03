using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Data;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using pep.Nobook.Helpers;

namespace JXP.PepDtp.View
{
	// Token: 0x02000015 RID: 21
	public partial class CollectWindow : Window
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000A96C File Offset: 0x00008B6C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000A974 File Offset: 0x00008B74
		public SharedResDataModel SharedResData { get; set; }

		// Token: 0x0600016E RID: 366 RVA: 0x0000A980 File Offset: 0x00008B80
		public CollectWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.CollectWindow_Loaded;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A9D7 File Offset: 0x00008BD7
		private void CollectWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.SharedResData == null)
			{
				LogHelper.Instance.Error("共享资源数据为空");
				return;
			}
			ThreadEx.Run(delegate()
			{
				this.CollectRes();
			});
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000AA04 File Offset: 0x00008C04
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭收藏资源画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000AA50 File Offset: 0x00008C50
		private void CollectRes()
		{
			string msg = "资源收藏失败";
			try
			{
				if (string.Compare(this.SharedResData.FileFormat, ".nbex", true) == 0)
				{
					if (NobookSingleActionHelper.Ref(this.SharedResData.SourceId, this.SharedResData.ResTitle, false) != null)
					{
						msg = "资源收藏成功，请到我的资源中查看";
					}
				}
				else
				{
					UserResourceJsonModel centerResData = this.mDataObjectProvider.GetCenterResData(this.SharedResData.ResId, this.SharedResData.ResFlg, true);
					if (centerResData != null && centerResData.UserResourcesList != null)
					{
						UserResourceModel userResourceModel = null;
						if (centerResData.UserResourcesList.Count > 0)
						{
							userResourceModel = centerResData.UserResourcesList[0];
						}
						if (userResourceModel == null)
						{
							LogHelper.Instance.Error("转换的用户资源为空，资源id:" + this.SharedResData.ResId);
						}
						else
						{
							Tuple<bool, string, string, DeviceFlags> userResInfo = UserResourcesManager.Instance.GetUserResInfo(userResourceModel, CommonParamter.Instance.LoginUserId);
							if (userResInfo.Item1)
							{
								string item = userResInfo.Item2;
								string item2 = userResInfo.Item3;
								DeviceFlags item3 = userResInfo.Item4;
								string convertPath = PathHelper.GetConvertPath(PathHelper.GetRelativeFileName(userResourceModel.FilePath));
								if (!this.mDownlaod.DownLoadFile(item, item2, true, false, item3, convertPath))
								{
									LogHelper.Instance.Error(string.Concat(new string[]
									{
										"下载共享资源失败,资源id:[",
										userResourceModel.ID,
										"]资源路径：[",
										item,
										"]"
									}));
								}
								else
								{
									UserResourceModel userResourceModel2 = UserResourcesManager.Instance.CreateNewUserResByUserRes(userResourceModel, false, true, true);
									string serverSaveDir = PathHelper.GetServerSaveDir(userResourceModel2.FilePath);
									string convertPath2 = PathHelper.GetConvertPath(userResourceModel2.FullPath);
									if (!this.mUpload.UploadFile2(convertPath2, serverSaveDir, "", null))
									{
										LogHelper.Instance.Error(string.Concat(new string[]
										{
											"上传用户资源失败,资源id:[",
											userResourceModel2.ID,
											"]资源路径：[",
											serverSaveDir,
											"]"
										}));
									}
									else
									{
										if (!string.IsNullOrEmpty(userResourceModel2.EcryType))
										{
											userResourceModel2.SourceId = this.SharedResData.ResId;
										}
										List<UserResourceModel> list = new List<UserResourceModel>();
										list.Add(userResourceModel2);
										string value = JsonHelper.Instance.ListToJson<UserResourceModel>(list);
										string text = WebRequestHelper.HttpPostRequest("resuser/save.json", new Dictionary<string, string>
										{
											{
												"resList",
												value
											}
										}, new int?(30000), 0, false, true);
										ReturnJsonUserResourcesModel returnJsonUserResourcesModel = JsonHelper.Instance.JsonsDeserialize<ReturnJsonUserResourcesModel>(text);
										if (returnJsonUserResourcesModel != null && returnJsonUserResourcesModel.Result == "110")
										{
											userResourceModel2.PosType = 3;
											if (this.mUserResesDA.ChechIsExist(userResourceModel2.ID))
											{
												this.mUserResesDA.UpdateUserRes(userResourceModel2);
											}
											else
											{
												this.mUserResesDA.InsertUserResurce(userResourceModel2);
											}
											base.Dispatcher.Invoke(new Action(delegate()
											{
												this.DialogResult = new bool?(true);
											}), new object[0]);
											msg = "资源收藏成功，请到我的资源中查看";
										}
										else
										{
											LogHelper.Instance.Error("调用后台接口[resuser/save.json]失败[" + text + "]。");
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("收藏资源失败:[{0}].", arg));
			}
			finally
			{
				base.Dispatcher.Invoke(new Action(delegate()
				{
					ToastWin.GetToaster(true, 450.0, 100.0).ShowInfo(new ToastInfo
					{
						Content = msg,
						IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
					});
					this.Close();
				}), new object[0]);
			}
		}

		// Token: 0x04000066 RID: 102
		private ResDataObjectProvider mDataObjectProvider = new ResDataObjectProvider();

		// Token: 0x04000067 RID: 103
		private DownloadHelper mDownlaod = new DownloadHelper();

		// Token: 0x04000068 RID: 104
		private UserResourceDataAccess mUserResesDA = new UserResourceDataAccess();

		// Token: 0x04000069 RID: 105
		private UpLoadHelper mUpload = new UpLoadHelper();
	}
}
