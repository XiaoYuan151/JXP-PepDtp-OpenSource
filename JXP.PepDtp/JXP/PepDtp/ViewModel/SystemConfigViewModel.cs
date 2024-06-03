using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Utilities;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000076 RID: 118
	public class SystemConfigViewModel : BaseModel
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00029366 File Offset: 0x00027566
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0002936E File Offset: 0x0002756E
		public bool AutoLockClient
		{
			get
			{
				return this.mAutoLockClient;
			}
			set
			{
				this.mAutoLockClient = value;
				this.OnPropertyChanged<bool>(() => this.AutoLockClient);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x000293AC File Offset: 0x000275AC
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x000293B4 File Offset: 0x000275B4
		public bool IsRemindSyncRes
		{
			get
			{
				return this.mIsRemindSyncRes;
			}
			set
			{
				this.mIsRemindSyncRes = value;
				this.OnPropertyChanged<bool>(() => this.IsRemindSyncRes);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x000293F2 File Offset: 0x000275F2
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x000293FA File Offset: 0x000275FA
		public bool AutoStartUp
		{
			get
			{
				return this.mAutoStartUp;
			}
			set
			{
				this.mAutoStartUp = value;
				this.OnPropertyChanged<bool>(() => this.AutoStartUp);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00029438 File Offset: 0x00027638
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00029440 File Offset: 0x00027640
		public bool MinisizeToPallet
		{
			get
			{
				return this.mMinisizeToPallet;
			}
			set
			{
				this.mMinisizeToPallet = value;
				this.OnPropertyChanged<bool>(() => this.MinisizeToPallet);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002947E File Offset: 0x0002767E
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00029486 File Offset: 0x00027686
		public bool ColseToPallet
		{
			get
			{
				return this.mColseToPallet;
			}
			set
			{
				this.mColseToPallet = value;
				this.OnPropertyChanged<bool>(() => this.ColseToPallet);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000294C4 File Offset: 0x000276C4
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x000294CC File Offset: 0x000276CC
		public bool AutoUpdateClient
		{
			get
			{
				return this.mAutoUpdateClient;
			}
			set
			{
				this.mAutoUpdateClient = value;
				this.OnPropertyChanged<bool>(() => this.AutoUpdateClient);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002950A File Offset: 0x0002770A
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00029514 File Offset: 0x00027714
		public Visibility SystemConfigVisibility
		{
			get
			{
				return this.mSystemConfigVisibility;
			}
			private set
			{
				this.mSystemConfigVisibility = value;
				this.OnPropertyChanged<Visibility>(() => this.SystemConfigVisibility);
				if (this.mSystemConfigVisibility == Visibility.Visible)
				{
					this.AboutVisibility = Visibility.Collapsed;
					this.ReportVisibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00029573 File Offset: 0x00027773
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0002957C File Offset: 0x0002777C
		public Visibility AboutVisibility
		{
			get
			{
				return this.mAboutVisibility;
			}
			private set
			{
				this.mAboutVisibility = value;
				this.OnPropertyChanged<Visibility>(() => this.AboutVisibility);
				if (this.mAboutVisibility == Visibility.Visible)
				{
					this.SystemConfigVisibility = Visibility.Collapsed;
					this.ReportVisibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000295DB File Offset: 0x000277DB
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000295E4 File Offset: 0x000277E4
		public Visibility ReportVisibility
		{
			get
			{
				return this.mReportVisibility;
			}
			private set
			{
				this.mReportVisibility = value;
				this.OnPropertyChanged<Visibility>(() => this.ReportVisibility);
				if (this.mReportVisibility == Visibility.Visible)
				{
					this.SystemConfigVisibility = Visibility.Collapsed;
					this.AboutVisibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00029643 File Offset: 0x00027843
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0002964C File Offset: 0x0002784C
		public bool IsSystemSelected
		{
			get
			{
				return this.mIsSystemSelected;
			}
			set
			{
				this.mIsSystemSelected = value;
				this.OnPropertyChanged<bool>(() => this.IsSystemSelected);
				if (this.mIsSystemSelected)
				{
					this.IsAboutSelected = false;
					this.IsReportSelected = false;
					this.SystemConfigVisibility = Visibility.Visible;
				}
				this.ChangeColor();
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000296B8 File Offset: 0x000278B8
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x000296C0 File Offset: 0x000278C0
		public bool IsAboutSelected
		{
			get
			{
				return this.mIsAboutSelected;
			}
			set
			{
				this.mIsAboutSelected = value;
				this.OnPropertyChanged<bool>(() => this.IsAboutSelected);
				if (this.mIsAboutSelected)
				{
					this.IsReportSelected = false;
					this.IsSystemSelected = false;
					this.AboutVisibility = Visibility.Visible;
				}
				this.ChangeColor();
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0002972C File Offset: 0x0002792C
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00029734 File Offset: 0x00027934
		public bool IsReportSelected
		{
			get
			{
				return this.mIsReportSelected;
			}
			set
			{
				this.mIsReportSelected = value;
				this.OnPropertyChanged<bool>(() => this.IsReportSelected);
				if (this.mIsReportSelected)
				{
					this.IsAboutSelected = false;
					this.IsSystemSelected = false;
					this.ReportVisibility = Visibility.Visible;
				}
				this.ChangeColor();
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000297A0 File Offset: 0x000279A0
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x000297A8 File Offset: 0x000279A8
		public SolidColorBrush SysBackGroundColorBrush
		{
			get
			{
				return this.mSysBackGroundColorBrush;
			}
			private set
			{
				this.mSysBackGroundColorBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.SysBackGroundColorBrush);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000297E6 File Offset: 0x000279E6
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x000297EE File Offset: 0x000279EE
		public SolidColorBrush SysForegroundBrush
		{
			get
			{
				return this.mSysForegroundBrush;
			}
			private set
			{
				this.mSysForegroundBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.SysForegroundBrush);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002982C File Offset: 0x00027A2C
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00029834 File Offset: 0x00027A34
		public SolidColorBrush AboutBackGroundColorBrush
		{
			get
			{
				return this.mAboutBackGroundColorBrush;
			}
			private set
			{
				this.mAboutBackGroundColorBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.AboutBackGroundColorBrush);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00029872 File Offset: 0x00027A72
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0002987A File Offset: 0x00027A7A
		public SolidColorBrush AboutForegroundBrush
		{
			get
			{
				return this.mAboutForegroundBrush;
			}
			private set
			{
				this.mAboutForegroundBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.AboutForegroundBrush);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000298B8 File Offset: 0x00027AB8
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x000298C0 File Offset: 0x00027AC0
		public SolidColorBrush ReportBackGroundColorBrush
		{
			get
			{
				return this.mReportBackGroundColorBrush;
			}
			private set
			{
				this.mReportBackGroundColorBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.ReportBackGroundColorBrush);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000298FE File Offset: 0x00027AFE
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00029906 File Offset: 0x00027B06
		public SolidColorBrush ReportForegroundBrush
		{
			get
			{
				return this.mReportForegroundBrush;
			}
			private set
			{
				this.mReportForegroundBrush = value;
				this.OnPropertyChanged<SolidColorBrush>(() => this.ReportForegroundBrush);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00029944 File Offset: 0x00027B44
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0002994C File Offset: 0x00027B4C
		public Visibility UpdateVisibility
		{
			get
			{
				return this.mUpdateVisibility;
			}
			set
			{
				this.mUpdateVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.UpdateVisibility);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002998A File Offset: 0x00027B8A
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00029992 File Offset: 0x00027B92
		public Visibility UploadImgeVisibility
		{
			get
			{
				return this.mUploadImgeVisibility;
			}
			set
			{
				this.mUploadImgeVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.UploadImgeVisibility);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000299D0 File Offset: 0x00027BD0
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000299D8 File Offset: 0x00027BD8
		public bool CheckLog
		{
			get
			{
				return this.mCheckLog;
			}
			set
			{
				this.mCheckLog = value;
				base.OnPropertyChanged<bool>(() => this.CheckLog);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00029A16 File Offset: 0x00027C16
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00029A1E File Offset: 0x00027C1E
		public Visibility WaitingVisibility
		{
			get
			{
				return this.mWaitingVisibility;
			}
			set
			{
				this.mWaitingVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.WaitingVisibility);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00029A5C File Offset: 0x00027C5C
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00029A64 File Offset: 0x00027C64
		public string ImagePath { get; set; }

		// Token: 0x060008B0 RID: 2224 RVA: 0x00029A70 File Offset: 0x00027C70
		private void ChangeColor()
		{
			if (this.mIsSystemSelected)
			{
				this.SysBackGroundColorBrush = SystemConfigViewModel.SelectedBackgoundBrush;
				this.SysForegroundBrush = SystemConfigViewModel.SelectedForegoundBrush;
			}
			else
			{
				this.SysBackGroundColorBrush = SystemConfigViewModel.UnSelectBackgroundBrush;
				this.SysForegroundBrush = SystemConfigViewModel.UnSelectForegroundBrush;
			}
			if (this.mIsAboutSelected)
			{
				this.AboutBackGroundColorBrush = SystemConfigViewModel.SelectedBackgoundBrush;
				this.AboutForegroundBrush = SystemConfigViewModel.SelectedForegoundBrush;
			}
			else
			{
				this.AboutBackGroundColorBrush = SystemConfigViewModel.UnSelectBackgroundBrush;
				this.AboutForegroundBrush = SystemConfigViewModel.UnSelectForegroundBrush;
			}
			if (this.mIsReportSelected)
			{
				this.ReportBackGroundColorBrush = SystemConfigViewModel.SelectedBackgoundBrush;
				this.ReportForegroundBrush = SystemConfigViewModel.SelectedForegoundBrush;
				return;
			}
			this.ReportBackGroundColorBrush = SystemConfigViewModel.UnSelectBackgroundBrush;
			this.ReportForegroundBrush = SystemConfigViewModel.UnSelectForegroundBrush;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00029B20 File Offset: 0x00027D20
		public bool CommitReport(string content, string version)
		{
			bool result;
			try
			{
				string value = string.Empty;
				string value2 = string.Empty;
				if (!string.IsNullOrEmpty(this.ImagePath) && File.Exists(this.ImagePath))
				{
					string destPath = string.Concat(new string[]
					{
						"/my_cloud/",
						CommonParamter.Instance.OrgsPath,
						"/",
						CommonParamter.Instance.LoginUserId,
						"/sys/resuser"
					});
					Tuple<bool, string> tuple = this.mUpload.UploadFile3(this.ImagePath, destPath, DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1) + Path.GetExtension(this.ImagePath), null);
					if (!tuple.Item1)
					{
						LogHelper.Instance.Error("上传图片失败。");
						return false;
					}
					value = tuple.Item2;
				}
				if (this.CheckLog)
				{
					string strB = DateTime.Now.ToString("yyyyMMdd");
					List<string> list = new List<string>();
					string text = Path.Combine(LogHelper.Instance.GetLogBaseDir(), "Log");
					DirectoryInfo directoryInfo = new DirectoryInfo(text);
					if (directoryInfo.Exists)
					{
						foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.log*"))
						{
							if (fileInfo.Exists && string.Compare(fileInfo.CreationTime.ToString("yyyyMMdd"), strB) == 0)
							{
								list.Add(fileInfo.FullName);
							}
						}
						if (list.Count > 0)
						{
							string text2 = Path.Combine(text, DateTime.Now.ToString(SdkConstants.DATE_TIME_FORMATE1) + ".zip");
							if (!DotZipHelper.CompressMulti(list, text2, false))
							{
								LogHelper.Instance.Error("上传log压缩zip包失败。");
							}
							string destPath2 = string.Concat(new string[]
							{
								"/my_cloud/",
								CommonParamter.Instance.OrgsPath,
								"/",
								CommonParamter.Instance.LoginUserId,
								"/sys/resuser"
							});
							Tuple<bool, string> tuple2 = this.mUpload.UploadFile3(text2, destPath2, "", null);
							if (!tuple2.Item1)
							{
								LogHelper.Instance.Error("上传Log失败。");
								return false;
							}
							value2 = tuple2.Item2;
						}
					}
				}
				string jsonStr = HttpHelper.HttpPost(ConfigHelper.WebServerUrl + "judge/feedback.json", new Dictionary<string, string>
				{
					{
						"userId",
						CommonParamter.Instance.CurrentUserId
					},
					{
						"content",
						content
					},
					{
						"file_path_pic",
						value
					},
					{
						"file_path_log",
						value2
					},
					{
						"version",
						version
					}
				}, new int?(300000), "", true);
				ReportResultModel reportResultModel = new JsonHelper().JsonsDeserialize<ReportResultModel>(jsonStr);
				if (reportResultModel != null && reportResultModel.Code == "110")
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("提交反馈信息失败，失败原因:[{0}]。", arg));
				result = false;
			}
			return result;
		}

		// Token: 0x0400043A RID: 1082
		private static readonly SolidColorBrush SelectedBackgoundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE8F2F1"));

		// Token: 0x0400043B RID: 1083
		private static readonly SolidColorBrush UnSelectBackgroundBrush = new SolidColorBrush(Colors.Transparent);

		// Token: 0x0400043C RID: 1084
		private static readonly SolidColorBrush SelectedForegoundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));

		// Token: 0x0400043D RID: 1085
		private static readonly SolidColorBrush UnSelectForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a9a9a9"));

		// Token: 0x0400043E RID: 1086
		private bool mIsSystemSelected = true;

		// Token: 0x0400043F RID: 1087
		private bool mIsAboutSelected;

		// Token: 0x04000440 RID: 1088
		private bool mIsReportSelected;

		// Token: 0x04000441 RID: 1089
		private Visibility mSystemConfigVisibility;

		// Token: 0x04000442 RID: 1090
		private Visibility mAboutVisibility = Visibility.Collapsed;

		// Token: 0x04000443 RID: 1091
		private Visibility mReportVisibility = Visibility.Collapsed;

		// Token: 0x04000444 RID: 1092
		private SolidColorBrush mSysBackGroundColorBrush = SystemConfigViewModel.SelectedBackgoundBrush;

		// Token: 0x04000445 RID: 1093
		private SolidColorBrush mSysForegroundBrush = SystemConfigViewModel.SelectedForegoundBrush;

		// Token: 0x04000446 RID: 1094
		private SolidColorBrush mAboutBackGroundColorBrush = SystemConfigViewModel.UnSelectBackgroundBrush;

		// Token: 0x04000447 RID: 1095
		private SolidColorBrush mAboutForegroundBrush = SystemConfigViewModel.UnSelectForegroundBrush;

		// Token: 0x04000448 RID: 1096
		private SolidColorBrush mReportBackGroundColorBrush = SystemConfigViewModel.UnSelectBackgroundBrush;

		// Token: 0x04000449 RID: 1097
		private SolidColorBrush mReportForegroundBrush = SystemConfigViewModel.UnSelectForegroundBrush;

		// Token: 0x0400044A RID: 1098
		private bool mAutoStartUp;

		// Token: 0x0400044B RID: 1099
		private bool mMinisizeToPallet;

		// Token: 0x0400044C RID: 1100
		private bool mAutoUpdateClient;

		// Token: 0x0400044D RID: 1101
		private bool mIsRemindSyncRes;

		// Token: 0x0400044E RID: 1102
		private bool mAutoLockClient;

		// Token: 0x0400044F RID: 1103
		private bool mColseToPallet = true;

		// Token: 0x04000450 RID: 1104
		private Visibility mUpdateVisibility = Visibility.Collapsed;

		// Token: 0x04000451 RID: 1105
		private Visibility mUploadImgeVisibility;

		// Token: 0x04000452 RID: 1106
		private bool mCheckLog = true;

		// Token: 0x04000453 RID: 1107
		private Visibility mWaitingVisibility = Visibility.Collapsed;

		// Token: 0x04000454 RID: 1108
		private UpLoadHelper mUpload = new UpLoadHelper();

		// Token: 0x04000455 RID: 1109
		private DotZipHelper mZip = new DotZipHelper();
	}
}
