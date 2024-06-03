using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JXP.Data;
using JXP.HttpProxy.Files;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using Newtonsoft.Json;
using pep.sdk.utility.Common;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005A RID: 90
	public class AppCenterViewModel : BaseModel
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000202B8 File Offset: 0x0001E4B8
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x000202C0 File Offset: 0x0001E4C0
		public Action SetItmWidthCallback { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000202C9 File Offset: 0x0001E4C9
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x000202D1 File Offset: 0x0001E4D1
		public bool InitFlg { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000202DA File Offset: 0x0001E4DA
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x000202E2 File Offset: 0x0001E4E2
		public ObservableCollection<AppCenterModel> LstShowAppData
		{
			get
			{
				return this.mLstShowAppData;
			}
			set
			{
				this.mLstShowAppData = value;
				base.OnPropertyChanged<ObservableCollection<AppCenterModel>>(() => this.LstShowAppData);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00020320 File Offset: 0x0001E520
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00020328 File Offset: 0x0001E528
		public AppCenterModel CurAppCenterData
		{
			get
			{
				return this.mCurAppCenterData;
			}
			set
			{
				this.mCurAppCenterData = value;
				base.OnPropertyChanged<AppCenterModel>(() => this.CurAppCenterData);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00020366 File Offset: 0x0001E566
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0002036E File Offset: 0x0001E56E
		public bool LstDataVisible
		{
			get
			{
				return this.mLstDataVisible;
			}
			set
			{
				this.mLstDataVisible = value;
				base.OnPropertyChanged<bool>(() => this.LstDataVisible);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000203AC File Offset: 0x0001E5AC
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public bool MsgVisible
		{
			get
			{
				return this.mMsgVisible;
			}
			set
			{
				this.mMsgVisible = value;
				base.OnPropertyChanged<bool>(() => this.MsgVisible);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000203F2 File Offset: 0x0001E5F2
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x000203FA File Offset: 0x0001E5FA
		public string MessageContent
		{
			get
			{
				return this.mMessageContent;
			}
			set
			{
				this.mMessageContent = value;
				base.OnPropertyChanged<string>(() => this.MessageContent);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00020438 File Offset: 0x0001E638
		public AppCenterViewModel()
		{
			this.mLstShowAppData = new ObservableCollection<AppCenterModel>();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00020468 File Offset: 0x0001E668
		public void InitData()
		{
			this.GetllAppData();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00020470 File Offset: 0x0001E670
		private void GetllAppData()
		{
			AppCenterViewModel.<GetllAppData>d__31 <GetllAppData>d__;
			<GetllAppData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<GetllAppData>d__.<>4__this = this;
			<GetllAppData>d__.<>1__state = -1;
			<GetllAppData>d__.<>t__builder.Start<AppCenterViewModel.<GetllAppData>d__31>(ref <GetllAppData>d__);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000204A8 File Offset: 0x0001E6A8
		private Task<List<AppCenterModel>> GetServerData()
		{
			AppCenterViewModel.<GetServerData>d__32 <GetServerData>d__;
			<GetServerData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<AppCenterModel>>.Create();
			<GetServerData>d__.<>1__state = -1;
			<GetServerData>d__.<>t__builder.Start<AppCenterViewModel.<GetServerData>d__32>(ref <GetServerData>d__);
			return <GetServerData>d__.<>t__builder.Task;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000204E4 File Offset: 0x0001E6E4
		private AppCenterModel GetNBCourseData()
		{
			return new AppCenterModel
			{
				ApplyId = "1100",
				ApplyName = "互动课件",
				ImageUrl = "pack://application:,,,/Resources/Images/hdkj.png",
				DetailImageUrl1 = "pack://application:,,,/Resources/Images/hdkj1.png",
				DetailImageUrl2 = "pack://application:,,,/Resources/Images/hdkj2.png",
				DetailImageUrl3 = "pack://application:,,,/Resources/Images/hdkj3.png",
				Description = "互动课件工具是一款集备授课于一身的互动教学系统，该工具基于云平台、云资源库、学科工具库，以课堂教学使用场景为核心，提供丰富的课堂互动教学工具，为学生提供更多在授课过程中参与和展示的机会，真正实现课前、课中、课后学生为主体的教学创新，完美实现教师与学生互动。通过互动学习、教学数据分析，减轻教师和学生的负担，实现教师精准教学、学生精准提分，提高教学质量。",
				IsOnlineTool = true,
				IsShowMenu = true,
				Rkxd = "0",
				Zxxkc = "0"
			};
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00020568 File Offset: 0x0001E768
		public void DownloadApp(DownloadParamter paramter)
		{
			AppCenterModel applyCenter = paramter.AppCenterData;
			try
			{
				string appToolPath = PepPathHelper.GetAppToolPath(applyCenter.ApplyId);
				if (paramter.IsUpdate)
				{
					if (applyCenter.ApplyId == SdkConstants.VOICE_APP_ID)
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(applyCenter.DownloadUrl);
						string text = Path.Combine(appToolPath, fileNameWithoutExtension);
						string text2 = Path.Combine(appToolPath, fileNameWithoutExtension + "_" + DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1));
						if (Directory.Exists(text))
						{
							ExplorerHelper.MoveDir(text, text2);
							applyCenter.BackupPath = text2;
						}
					}
					else
					{
						string text3 = Path.Combine(Path.GetDirectoryName(appToolPath), applyCenter.ApplyId + "_" + DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1));
						if (Directory.Exists(appToolPath))
						{
							ExplorerHelper.MoveDir(appToolPath, text3);
							applyCenter.BackupPath = text3;
						}
					}
				}
				if (applyCenter.ApplyId == SdkConstants.CHINESE_CARD_APP_ID)
				{
					ThreadEx.Run(delegate()
					{
						ToolsUpdateHelper.DeleteOldData7DownloadNewData(SdkConstants.CHINESE_CARD_APP_ID);
					});
				}
				TransferFileModel transferFileModel = new TransferFileModel();
				transferFileModel.DeviceFlag = DeviceFlags.CentralResources;
				transferFileModel.UrlPathName = applyCenter.DownloadUrl;
				transferFileModel.DestPath = appToolPath;
				transferFileModel.AppCenter = applyCenter;
				transferFileModel.IsOverride = true;
				transferFileModel.TransType = TransferType.Download;
				transferFileModel.TransState = TransferState.UnStarted;
				transferFileModel.IsUnZip = false;
				transferFileModel.ShowSpeedFlg = false;
				if (new TransFileCommonHelper().DownloadFile(transferFileModel, delegate(long finishSize, long totalSize)
				{
					double num = (double)finishSize / (double)totalSize * 100.0;
					num /= 2.0;
					num = Math.Round(num, 0);
					num = ((num <= 0.0) ? 1.0 : num);
					num = ((num >= 50.0) ? 50.0 : num);
					applyCenter.ProValue = (int)num;
					applyCenter.ProValueInfo = ((int)num).ToString() + "%";
					return paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending;
				}))
				{
					this.DownloadApplyToolSuccess(paramter);
				}
				else
				{
					this.DownloadApplyFailed(paramter);
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "下载应用工具失败，应用id：";
				AppCenterModel applyCenter2 = applyCenter;
				string str2 = (applyCenter2 != null) ? applyCenter2.ApplyId : null;
				string str3 = "。";
				Exception ex2 = ex;
				instance.Error(str + str2 + str3 + ((ex2 != null) ? ex2.ToString() : null));
				this.DownloadApplyFailed(paramter);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000207A8 File Offset: 0x0001E9A8
		private void DownloadApplyToolSuccess(DownloadParamter paramter)
		{
			if (paramter == null || paramter.AppCenterData == null)
			{
				return;
			}
			AppCenterModel applyCenter = paramter.AppCenterData;
			string appToolPath = PepPathHelper.GetAppToolPath(applyCenter.ApplyId);
			try
			{
				string strTempDelDir = Path.Combine(appToolPath, "Temp_" + DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1));
				if (!Directory.Exists(strTempDelDir))
				{
					Directory.CreateDirectory(strTempDelDir);
				}
				if (applyCenter.ApplyId == SdkConstants.VOICE_APP_ID)
				{
					string text = Path.Combine(appToolPath, Path.GetFileNameWithoutExtension(applyCenter.DownloadUrl));
					if (Directory.Exists(text))
					{
						ExplorerHelper.MoveDir(text, Path.Combine(strTempDelDir, Path.GetFileName(text)));
					}
				}
				else
				{
					foreach (string text2 in Directory.GetDirectories(appToolPath))
					{
						if (string.Compare(Path.GetFileName(text2), Path.GetFileName(strTempDelDir), true) != 0)
						{
							ExplorerHelper.MoveDir(text2, Path.Combine(strTempDelDir, Path.GetFileName(text2)));
						}
					}
					foreach (string text3 in Directory.GetFiles(appToolPath))
					{
						if (string.Compare(Path.GetFileName(text3), Path.GetFileName(applyCenter.DownloadUrl), true) != 0)
						{
							ExplorerHelper.MoveFile(text3, Path.Combine(strTempDelDir, Path.GetFileName(text3)));
						}
					}
				}
				ThreadEx.Run(delegate()
				{
					ExplorerHelper.DeleteDirFast(strTempDelDir);
				});
			}
			catch
			{
			}
			if (applyCenter.ApplyId == SdkConstants.CHINESE_CARD_APP_ID)
			{
				string path = Path.Combine(appToolPath, Path.GetFileName(applyCenter.DownloadUrl));
				if (!File.Exists(path))
				{
					this.DownloadApplyFailed(paramter);
					return;
				}
				using (FileDecryptedStream fileDecryptedStream = new FileDecryptedStream(File.OpenRead(path)))
				{
					DotZipHelper.Decompression(fileDecryptedStream, appToolPath, "cartTool/", string.Empty, delegate(int finishCount, int totalCount)
					{
						double num = (double)finishCount / (double)totalCount * 100.0;
						num /= 2.0;
						num = Math.Round(num, 0);
						num = ((num <= 0.0) ? 1.0 : num);
						num = ((num >= 50.0) ? 50.0 : num);
						int proValue = 50 + (int)num;
						applyCenter.ProValue = proValue;
						applyCenter.ProValueInfo = proValue.ToString() + "%";
						return paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending;
					});
					goto IL_36A;
				}
			}
			if (applyCenter.ApplyId == SdkConstants.VOICE_APP_ID)
			{
				string path2 = Path.Combine(appToolPath, Path.GetFileName(applyCenter.DownloadUrl));
				if (!File.Exists(path2))
				{
					this.DownloadApplyFailed(paramter);
					return;
				}
				using (FileDecryptedStream fileDecryptedStream2 = new FileDecryptedStream(File.OpenRead(path2)))
				{
					DotZipHelper.Decompression(fileDecryptedStream2, appToolPath, true, delegate(int finishCount, int totalCount)
					{
						double num = (double)finishCount / (double)totalCount * 100.0;
						num /= 2.0;
						num = Math.Round(num, 0);
						num = ((num <= 0.0) ? 1.0 : num);
						num = ((num >= 50.0) ? 50.0 : num);
						int proValue = 50 + (int)num;
						applyCenter.ProValue = proValue;
						applyCenter.ProValueInfo = proValue.ToString() + "%";
						return paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending;
					}, "");
					goto IL_36A;
				}
			}
			if (applyCenter.ApplyId == SdkConstants.PY_APP_ID)
			{
				string path3 = Path.Combine(appToolPath, Path.GetFileName(applyCenter.DownloadUrl));
				if (!File.Exists(path3))
				{
					this.DownloadApplyFailed(paramter);
					return;
				}
				using (FileDecryptedStream fileDecryptedStream3 = new FileDecryptedStream(File.OpenRead(path3)))
				{
					DotZipHelper.Decompression(fileDecryptedStream3, appToolPath, true, delegate(int finishCount, int totalCount)
					{
						double num = (double)finishCount / (double)totalCount * 100.0;
						num /= 2.0;
						num = Math.Round(num, 0);
						num = ((num <= 0.0) ? 1.0 : num);
						num = ((num >= 50.0) ? 50.0 : num);
						int proValue = 50 + (int)num;
						applyCenter.ProValue = proValue;
						applyCenter.ProValueInfo = proValue.ToString() + "%";
						return paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending;
					}, "");
					goto IL_36A;
				}
			}
			string text4 = Path.Combine(appToolPath, Path.GetFileName(applyCenter.DownloadUrl));
			if (!File.Exists(text4))
			{
				this.DownloadApplyFailed(paramter);
				return;
			}
			DotZipHelper.Decompression(text4, appToolPath, true, delegate(int finishCount, int totalCount)
			{
				double num = (double)finishCount / (double)totalCount * 100.0;
				num /= 2.0;
				num = Math.Round(num, 0);
				num = ((num <= 0.0) ? 1.0 : num);
				num = ((num >= 50.0) ? 50.0 : num);
				int proValue = 50 + (int)num;
				applyCenter.ProValue = proValue;
				applyCenter.ProValueInfo = proValue.ToString() + "%";
				return paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending;
			});
			IL_36A:
			if (paramter.DownloadWork != null && paramter.DownloadWork.CancellationPending)
			{
				this.DownloadApplyFailed(paramter);
				return;
			}
			this.SaveApplyCenterInfo(applyCenter);
			applyCenter.IsShowOpen = true;
			applyCenter.IsShowDelete = true;
			if (paramter.IsUpdate)
			{
				ThreadEx.Run(delegate()
				{
					ExplorerHelper.DeleteDirFast(applyCenter.BackupPath);
				});
			}
			applyCenter.IsShowUpdate = false;
			applyCenter.IsShowOpen = true;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00020C0C File Offset: 0x0001EE0C
		private void DownloadApplyFailed(DownloadParamter paramter)
		{
			try
			{
				if (paramter != null && paramter.AppCenterData != null)
				{
					AppCenterModel appCenterData = paramter.AppCenterData;
					if (paramter.IsUpdate)
					{
						string text = PepPathHelper.GetAppToolPath(appCenterData.ApplyId);
						if (appCenterData.ApplyId == SdkConstants.VOICE_APP_ID)
						{
							text = Path.Combine(text, Path.GetFileNameWithoutExtension(appCenterData.DownloadUrl));
						}
						string strTempDirPath = Path.Combine(Path.GetDirectoryName(text), Path.GetFileName(text) + "_temp" + DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1));
						if (Directory.Exists(text))
						{
							ExplorerHelper.MoveDir(text, strTempDirPath);
							ThreadEx.Run(delegate()
							{
								ExplorerHelper.DeleteDirFast(strTempDirPath);
							});
						}
						string backupPath = appCenterData.BackupPath;
						if (Directory.Exists(backupPath))
						{
							ExplorerHelper.MoveDir(backupPath, text);
						}
						appCenterData.IsShowUpdate = true;
					}
					else
					{
						appCenterData.IsShowInstall = true;
					}
					appCenterData.ProValue = 0;
					appCenterData.ProValueInfo = "0%";
					ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
					{
						Content = "下载失败，请重试!",
						IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "应用中心下载失败之后处理失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00020D80 File Offset: 0x0001EF80
		private void SaveApplyCenterInfo(AppCenterModel applyCenter)
		{
			if (applyCenter == null)
			{
				return;
			}
			this.mAppCenterDA.DelApplyCenterData(applyCenter.ApplyId);
			this.mAppCenterDA.InsertApplyCenterData(applyCenter);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public bool SaveApplyData(string applyId)
		{
			AppCenterListResult appCenterListResult = JsonConvert.DeserializeObject<AppCenterListResult>(HttpHelper.HttpGet(SdkConstants.ToolOnlineUrl + "/pub_cloud/110/" + SdkConstants.ToolJsonName, null, true, ""));
			if (appCenterListResult.LstData != null)
			{
				AppCenterModel appCenterModel = (from a in appCenterListResult.LstData
				where a.LstProductIds.Contains(CommonParamter.Instance.ProductId) && a.ApplyId == applyId
				select a).FirstOrDefault<AppCenterModel>();
				if (appCenterModel != null)
				{
					this.SaveApplyCenterInfo(appCenterModel);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00020E1E File Offset: 0x0001F01E
		public void DelApplyTool(AppCenterModel applyCenter)
		{
			this.mAppCenterDA.DelApplyCenterData(applyCenter.ApplyId);
			applyCenter.IsShowInstall = true;
			applyCenter.IsShowDelete = false;
		}

		// Token: 0x04000317 RID: 791
		private AppCenterDataAccess mAppCenterDA = new AppCenterDataAccess();

		// Token: 0x04000318 RID: 792
		private ObservableCollection<AppCenterModel> mLstShowAppData;

		// Token: 0x04000319 RID: 793
		private AppCenterModel mCurAppCenterData;

		// Token: 0x0400031A RID: 794
		private bool mLstDataVisible;

		// Token: 0x0400031B RID: 795
		private bool mMsgVisible = true;

		// Token: 0x0400031C RID: 796
		private string mMessageContent = string.Empty;
	}
}
