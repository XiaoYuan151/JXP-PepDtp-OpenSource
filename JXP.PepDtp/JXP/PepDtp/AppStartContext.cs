using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using JXP.Cef;
using JXP.Configuration;
using JXP.Controls;
using JXP.Data.Util;
using JXP.DataAnalytics.Bootstrap;
using JXP.DataAnalytics.Platform;
using JXP.DataAnalytics.Tracker;
using JXP.Enums;
using JXP.LocalDB;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.Transfer;
using JXP.PepUtility;
using JXP.SpeechEvaluator.Utility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using pep.Course.Models;
using pep.Nobook;
using pep.sdk.reader.Common;
using pep.sdk.utility;

namespace JXP.PepDtp
{
	// Token: 0x02000006 RID: 6
	internal class AppStartContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000035A0 File Offset: 0x000017A0
		internal static AppStartContext Instance
		{
			get
			{
				return AppStartContext.singleton.Value;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000035AC File Offset: 0x000017AC
		internal int OnStart(string[] args)
		{
			SdkConstants.SdkDirectory = AppDomain.CurrentDomain.BaseDirectory;
			SdkConstants.DB_NAME = AppConsts.DB_NAME;
			SdkConstants.DATABASE_PASSWORD = AppConsts.DATABASE_PASSWORD;
			ConfigHelper.WebServerUrl = this.GetWebUrl();
			ConfigHelper.JCServerUrl = this.GetJCUrl();
			ConfigHelper.JCHomeworkUrl = this.GetJCHomeworkUrl();
			WebApiConstants.NET_STATE = "p/user/userSession";
			NbParameters.Instance.HasPepClass = false;
			SdkConstants.PROJECT_BRANCH_TYPE = ProjectBranchEnum.GuangXiProject;
			WaitingControls.PROJECT_BRANCH_TYPE = ProjectBranchEnum.GuangXiProject;
			CommonParamter.Instance.ClientId = AppConsts.JXP_APPKEY;
			CommonParamter.Instance.ProductId = AppConsts.JXP_PRODUCTID;
			CommonParamter.Instance.IsYongZhongEnable = true;
			CustomMessageBox.MsgStyle = MsgBoxStyle.Style4;
			HttpHelper.IsCheckParamter = false;
			WebApiConstants.HTTPMODE = AppConsts.HTTPMODE;
			this.SetAppToolJsonName();
			this.SetclassicalPoetryUrl();
			TBDownloadOperate.IsCheckThumbSuccess = false;
			PepSdkReaderContext.Instance.UploadResInfoText = "八桂教学通资源上传规定";
			PepSdkReaderContext.Instance.UserAgreementText = "《八桂教学通用户服务协议》";
			SdkConstants.BookMaxFactor = 8;
			SdkConstants.UserAgreementUrl = "https://szgs-100.oss-cn-beijing.aliyuncs.com/ysxy/userprotocolGX.html";
			SdkConstants.PrivacyAgreementUrl = "https://szgs-100.oss-cn-beijing.aliyuncs.com/ysxy/policy8gt.html";
			WndProcHelper.Instance.Init();
			IProductInfoProvider productInfoProvider = new DefaultProductInfoProvider();
			productInfoProvider.ProductId = AppConsts.JXP_PRODUCTID;
			IProductInfoProvider productInfoProvider2 = productInfoProvider;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			string productVersion;
			if (entryAssembly == null)
			{
				productVersion = null;
			}
			else
			{
				Version version = entryAssembly.GetName().Version;
				productVersion = ((version != null) ? version.ToString() : null);
			}
			productInfoProvider2.ProductVersion = productVersion;
			ITrackerConfig trackerConfig = new DefaultTrackerConfig();
			trackerConfig.AppKey = AppConsts.JXP_APPKEY;
			TrackerManager.Init(new TrackerFactory().SetConfig(trackerConfig).SetProductInfoProvider(productInfoProvider).Build(), "main", "");
			try
			{
				this.CreateDatabase();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("数据文件创建失败![{0}]", ex.Message));
				return 1;
			}
			Task.Factory.StartNew(delegate()
			{
				try
				{
					string dataDir = PepPathHelper.GetDataDir();
					if (!Directory.Exists(dataDir))
					{
						Directory.CreateDirectory(dataDir);
					}
					Functions.HideFolderOrFile(dataDir);
					string toolDir = PepPathHelper.GetToolDir();
					if (!Directory.Exists(toolDir))
					{
						Directory.CreateDirectory(toolDir);
					}
					Functions.HideFolderOrFile(toolDir);
				}
				catch
				{
				}
			});
			EnvironmentHelper.AddEnvironmentPaths(new List<string>
			{
				AppDomain.CurrentDomain.BaseDirectory + SdkConstants.ThirdLib_Vlc
			});
			ThreadEx.Run(delegate()
			{
				try
				{
					ProcessHelper.StartWithoutWindow(PepPathHelper.GetVlcCacheGenPath(), PepPathHelper.GetVlcPluginPath() ?? "", "", "");
				}
				catch (Exception ex2)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "Vlc初始化失败.";
					Exception ex3 = ex2;
					instance.Error(str + ((ex3 != null) ? ex3.ToString() : null));
				}
			});
			ThreadEx.Run(delegate()
			{
				EvaluatorContext.DeleteRecordingBaseDir(15U);
				EvaluatorContext.Start();
			});
			BrowserProcessStartHelper.SingleStart("", true, null, "");
			return 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003804 File Offset: 0x00001A04
		private void CreateDatabase()
		{
			IDictionary<string, Type> dictionary = new Dictionary<string, Type>();
			dictionary.Add("users", typeof(UserModel));
			dictionary.Add("constants", typeof(ConstantsModel));
			dictionary.Add("chapters", typeof(ChaptersModel));
			dictionary.Add("chapters_download", typeof(TransferChapterModel));
			dictionary.Add("textbooks", typeof(TextbookModel));
			dictionary.Add("devices", typeof(DeviceModel));
			dictionary.Add("operate_records", typeof(OperateRecordModel));
			dictionary.Add("resources", typeof(ResourcesModel));
			dictionary.Add("httptransferinfos", typeof(HttpTransferInfosModel));
			dictionary.Add("taskresult", typeof(TaskResultModel));
			dictionary.Add("userresources", typeof(UserResourceModel));
			dictionary.Add("textbook_open_history", typeof(OpenBookHistoryModel));
			dictionary.Add("bookmark", typeof(BookMarkModel));
			dictionary.Add("textbooks_state", typeof(TextbooksStateModel));
			dictionary.Add("textbook_note", typeof(NoteModel));
			dictionary.Add("textbook_info", typeof(TextBookInfoModel));
			dictionary.Add("appcenter", typeof(AppCenterModel));
			dictionary.Add("appcenter_state", typeof(AppCenterStateModel));
			dictionary.Add("userfirstlogin", typeof(UserFirstLoginModel));
			dictionary.Add("user_select_chapterinfo", typeof(UserChapterInfoModel));
			dictionary.Add("course_package", typeof(CourseModel));
			dictionary.Add("offline_Import_course", typeof(OffLineImportModel));
			dictionary.Add("tutorial", typeof(TutorialModel));
			dictionary.Add("popInfo", typeof(PopInfoModel));
			string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SdkConstants.DB_NAME);
			SQLiteHelper.InitSQLiteConn(text, SdkConstants.DATABASE_PASSWORD);
			DbManagementHelper.CreateDatabase(SQLiteHelper.SqliteConn, dictionary);
			Functions.HideFolderOrFile(text);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003A40 File Offset: 0x00001C40
		private string GetWebUrl()
		{
			string text = "/";
			string text2 = ConfigManager.AppSettings["WebServerUrl"];
			if (!text2.EndsWith(text))
			{
				text2 += text;
			}
			return text2;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003A78 File Offset: 0x00001C78
		private string GetJCUrl()
		{
			string text = "/";
			string text2 = ConfigManager.AppSettings["JCServerUrl"];
			if (!text2.EndsWith(text))
			{
				text2 += text;
			}
			return text2;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003AB0 File Offset: 0x00001CB0
		private string GetJCHomeworkUrl()
		{
			string text = "/";
			string text2 = ConfigManager.AppSettings["JCHoweworkUrl"];
			if (!text2.EndsWith(text))
			{
				text2 += text;
			}
			return text2;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private void SetAppToolJsonName()
		{
			try
			{
				string text = ConfigManager.AppSettings["tooljson"];
				if (!string.IsNullOrEmpty(text))
				{
					SdkConstants.ToolJsonName = text;
				}
				else
				{
					SdkConstants.ToolJsonName = "dtpGXAppList.json";
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003B34 File Offset: 0x00001D34
		private void SetclassicalPoetryUrl()
		{
			try
			{
				string text = ConfigManager.AppSettings["classicalPoetryUrl"];
				if (!string.IsNullOrEmpty(text))
				{
					SdkConstants.ClassicalPoetryUrl = text;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly Lazy<AppStartContext> singleton = new Lazy<AppStartContext>();
	}
}
