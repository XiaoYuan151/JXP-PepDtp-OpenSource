using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using JXP.Controls.CustomControl;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.DataStatistics;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using JXP.Windows.View;
using pep.Course.Commons;
using pep.Course.Views;
using pep.Nobook;
using pep.Nobook.Helpers;
using pep.sdk.reader.TextbookUtils;
using pep.sdk.reader.View;
using pep.sdk.reader.View.Textbook;
using pep.sdk.utility.Paramter;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000043 RID: 67
	public partial class MyResControl : System.Windows.Controls.UserControl, INotifyPropertyChanged, IStyleConnector
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00013AA9 File Offset: 0x00011CA9
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00013AB1 File Offset: 0x00011CB1
		public GetResultCount GetResultCountCallBack { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00013ABA File Offset: 0x00011CBA
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00013AC2 File Offset: 0x00011CC2
		public Action<ClassActivityInfoModel, UserResourceModel, Window> SaveCallBack { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00013ACB File Offset: 0x00011CCB
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00013AD3 File Offset: 0x00011CD3
		public ObservableCollection<UserResourceModel> ResList { get; set; } = new ObservableCollection<UserResourceModel>();

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00013ADC File Offset: 0x00011CDC
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public bool ShowNoDataMessage
		{
			get
			{
				return this.mShowNoDataMessage;
			}
			set
			{
				this.mShowNoDataMessage = value;
				this.OnPropertyRaised("ShowNoDataMessage");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00013AF8 File Offset: 0x00011CF8
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00013B00 File Offset: 0x00011D00
		public string MessageContent
		{
			get
			{
				return this.mMessageContent;
			}
			set
			{
				this.mMessageContent = value;
				this.OnPropertyRaised("MessageContent");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00013B14 File Offset: 0x00011D14
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00013B1C File Offset: 0x00011D1C
		public bool IsOnLine
		{
			get
			{
				return this.mIsOnLine;
			}
			set
			{
				this.mIsOnLine = value;
				this.OnPropertyRaised("IsOnLine");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00013B30 File Offset: 0x00011D30
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00013B38 File Offset: 0x00011D38
		public UserResourceModel SelectRes
		{
			get
			{
				return this.mSelectRes;
			}
			set
			{
				this.mSelectRes = value;
				this.OnPropertyRaised("SelectRes");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00013B4C File Offset: 0x00011D4C
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00013B54 File Offset: 0x00011D54
		public int TotalCount
		{
			get
			{
				return this.mTotalCount;
			}
			set
			{
				this.mTotalCount = value;
				this.OnPropertyRaised("TotalCount");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00013B68 File Offset: 0x00011D68
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00013B70 File Offset: 0x00011D70
		public bool ShowBottomInfo
		{
			get
			{
				return this.mShowBottomInfo;
			}
			set
			{
				this.mShowBottomInfo = value;
				this.OnPropertyRaised("ShowBottomInfo");
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000396 RID: 918 RVA: 0x00013B84 File Offset: 0x00011D84
		// (remove) Token: 0x06000397 RID: 919 RVA: 0x00013BBC File Offset: 0x00011DBC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000398 RID: 920 RVA: 0x00013BF1 File Offset: 0x00011DF1
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00013C0A File Offset: 0x00011E0A
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00013C1C File Offset: 0x00011E1C
		public bool IsDrag
		{
			get
			{
				return (bool)base.GetValue(MyResControl.IsDragProperty);
			}
			set
			{
				base.SetValue(MyResControl.IsDragProperty, value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00013C2F File Offset: 0x00011E2F
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00013C41 File Offset: 0x00011E41
		public bool ShowMoreBtn
		{
			get
			{
				return (bool)base.GetValue(MyResControl.ShowMoreBtnProperty);
			}
			set
			{
				base.SetValue(MyResControl.ShowMoreBtnProperty, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00013C54 File Offset: 0x00011E54
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00013C66 File Offset: 0x00011E66
		public bool ImageTextChecked
		{
			get
			{
				return (bool)base.GetValue(MyResControl.ImageTextCheckedProperty);
			}
			set
			{
				base.SetValue(MyResControl.ImageTextCheckedProperty, value);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00013C7C File Offset: 0x00011E7C
		public MyResControl()
		{
			this.InitializeComponent();
			this.ucPagingControl.PageIndexChanngedCallBack = new Action<int>(this.ChangePage);
			base.Loaded += this.MyResControl_Loaded;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013D03 File Offset: 0x00011F03
		private void MyResControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.ownerWin = Window.GetWindow(this);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00013D14 File Offset: 0x00011F14
		public void SearchDataAsync(string zyFormate, string zyLb, string keyword, string bookId, List<string> lstChapterId)
		{
			if (this.ResParamter == null)
			{
				this.ResParamter = new MyResourceParamter();
			}
			this.ResParamter.ZyFormate = zyFormate;
			this.ResParamter.ZyLb = zyLb;
			this.ResParamter.Keyword = keyword;
			this.ResParamter.BookID = bookId;
			this.ResParamter.LstChapteID = lstChapterId;
			this.ShowBottomInfo = false;
			this.mCurPageNum = 1;
			this.mTotlePage = 0;
			this.SearchResListData();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00013D90 File Offset: 0x00011F90
		private Task SearchResListData()
		{
			MyResControl.<SearchResListData>d__66 <SearchResListData>d__;
			<SearchResListData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<SearchResListData>d__.<>4__this = this;
			<SearchResListData>d__.<>1__state = -1;
			<SearchResListData>d__.<>t__builder.Start<MyResControl.<SearchResListData>d__66>(ref <SearchResListData>d__);
			return <SearchResListData>d__.<>t__builder.Task;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00013DD4 File Offset: 0x00011FD4
		private Task<UserResourceJsonModel> GetUserResDataListAsync()
		{
			MyResControl.<GetUserResDataListAsync>d__67 <GetUserResDataListAsync>d__;
			<GetUserResDataListAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<UserResourceJsonModel>.Create();
			<GetUserResDataListAsync>d__.<>4__this = this;
			<GetUserResDataListAsync>d__.<>1__state = -1;
			<GetUserResDataListAsync>d__.<>t__builder.Start<MyResControl.<GetUserResDataListAsync>d__67>(ref <GetUserResDataListAsync>d__);
			return <GetUserResDataListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00013E18 File Offset: 0x00012018
		private Dictionary<string, string> GetPostParameter(out string strParamter)
		{
			strParamter = string.Empty;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["userId"] = CommonParamter.Instance.LoginUserId;
			dictionary["pvt_biz_type"] = "0";
			if (this.ResParamter != null)
			{
				if (this.ResParamter.LstChapteID != null && this.ResParamter.LstChapteID.Count > 0)
				{
					dictionary["ori_tree_code"] = string.Join(",", this.ResParamter.LstChapteID);
				}
				else if (!string.IsNullOrEmpty(this.ResParamter.BookID))
				{
					dictionary["tbId"] = this.ResParamter.BookID;
				}
				if (!string.IsNullOrWhiteSpace(this.ResParamter.Keyword))
				{
					dictionary["title"] = this.ResParamter.Keyword;
				}
				string zyFormate = this.ResParamter.ZyFormate;
				if (!string.IsNullOrEmpty(zyFormate) && zyFormate != "-100")
				{
					dictionary["dzwjlx_zhpt"] = zyFormate;
				}
				string zyLb = this.ResParamter.ZyLb;
				if (!string.IsNullOrEmpty(zyLb) && zyLb != "-100")
				{
					dictionary["zynrlx_zhpt"] = zyLb;
				}
			}
			dictionary["pagesize"] = 10.ToString();
			if (this.mCurPageNum <= 0)
			{
				this.mCurPageNum = 1;
			}
			dictionary["pageno"] = this.mCurPageNum.ToString();
			strParamter = new JavaScriptSerializer().Serialize(dictionary);
			return dictionary;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013F97 File Offset: 0x00012197
		private void SetPageCnt(int totalPages, int currentPage)
		{
			if (totalPages <= 0)
			{
				this.ShowBottomInfo = false;
				return;
			}
			this.ucPagingControl.PageCount = totalPages;
			this.ucPagingControl.Current = currentPage;
			this.ShowBottomInfo = true;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013FC4 File Offset: 0x000121C4
		private void btnShareResource_Click(object sender, RoutedEventArgs e)
		{
			UserResourceModel userResourceModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as UserResourceModel;
			if (userResourceModel == null)
			{
				return;
			}
			this.ShareRes(userResourceModel, this.ownerWin);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014002 File Offset: 0x00012202
		private void btnShare_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			this.ShareRes(this.SelectRes, this.ownerWin);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014024 File Offset: 0x00012224
		private void ShareRes(UserResourceModel userResMdl, Window owner)
		{
			try
			{
				if (userResMdl != null)
				{
					if (!string.IsNullOrEmpty(userResMdl.EcryType))
					{
						CustomMessageBox.Info("抱歉，该资源为：教材配套资源，暂不支持用户分享", "确定", "", owner, WindowStartupLocation.CenterOwner, true);
					}
					else
					{
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx200133",
							Passive = userResMdl.ID,
							FromPos = MyResControl.mClassPath + ".ShareRes"
						});
						ShareResWindow shareResWindow = new ShareResWindow();
						MaskLayerWindow.SingleInstance().ShowMaskWindow(owner);
						shareResWindow.Owner = MaskLayerWindow.SingleInstance();
						shareResWindow.UserRes = userResMdl;
						shareResWindow.ShowDialog();
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("分享资源失败。" + ex.ToString());
				CustomMessageBox.Info("分享资源失败，请重试", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, true);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014114 File Offset: 0x00012314
		private void btnSynchro_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			this.SyncRes(this.SelectRes);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014130 File Offset: 0x00012330
		private void SyncRes(UserResourceModel userResMdl)
		{
			if (userResMdl == null)
			{
				return;
			}
			Action <>9__3;
			ThreadEx.Run(delegate()
			{
				try
				{
					UserResourceModel uploadRes = null;
					UserResourceModel downloadRes = null;
					string text = null;
					if (this.ConfirmExistSyncRes(userResMdl, out uploadRes, out downloadRes, out text))
					{
						if (uploadRes != null || downloadRes != null)
						{
							this.Dispatcher.Invoke(new Action(delegate()
							{
								DownloadResourcesWindow.GetInstance().ShowTranslateWindow(this.ownerWin);
								if (uploadRes != null)
								{
									DownloadResourcesWindow.GetInstance().uploadrbtn.IsChecked = new bool?(true);
									return;
								}
								if (downloadRes != null)
								{
									DownloadResourcesWindow.GetInstance().downloadrbtn.IsChecked = new bool?(true);
								}
							}), new object[0]);
							this.SyncUploadUserReses(uploadRes);
							this.SyncDownloadUserReses(downloadRes);
						}
					}
					else
					{
						this.Dispatcher.Invoke(new Action(delegate()
						{
							ToastWin.GetToaster(true, 400.0, 40.0).ShowInfo(new ToastInfo
							{
								IconType = new ToastMessageIconType?(ToastMessageIconType.OK),
								Content = "资源已为最新,无需同步"
							});
						}), new object[0]);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error("同步资源(SyncUserResources)出错：" + ex.ToString());
					Dispatcher dispatcher = this.Dispatcher;
					Action method;
					if ((method = <>9__3) == null)
					{
						method = (<>9__3 = delegate()
						{
							CustomMessageBox.Info("同步失败！", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, true);
						});
					}
					dispatcher.Invoke(method, new object[0]);
				}
			});
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001416C File Offset: 0x0001236C
		private void btnEditResource_Click(object sender, RoutedEventArgs e)
		{
			UserResourceModel userResourceModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as UserResourceModel;
			if (userResourceModel == null)
			{
				return;
			}
			this.EditeRes(userResourceModel);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000141A4 File Offset: 0x000123A4
		private void EditeRes(UserResourceModel userResMdl)
		{
			try
			{
				if (userResMdl != null)
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200135",
						Passive = userResMdl.ID,
						FromPos = MyResControl.mClassPath + ".EditeRes"
					});
					if (userResMdl.State == 90)
					{
						userResMdl.RangeType = "0";
						userResMdl.GroupIds = string.Empty;
					}
					new EditeResWindow(userResMdl)
					{
						Owner = this.ownerWin,
						ShowShareInfo = (string.IsNullOrEmpty(userResMdl.EcryType) > false),
						CreateGroupDataCallBack = this.CreateGroupDataCallBack,
						SaveSuccessCallBack = new Action<UserResourceModel>(this.ModdifyNBData)
					}.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("我的资源点击编辑资源失败：" + ex.ToString());
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014290 File Offset: 0x00012490
		private void ModdifyNBData(UserResourceModel userResMdl)
		{
			if (userResMdl == null || (userResMdl.PvtbizType != "201" && string.Compare(userResMdl.FileFormat, ".nbex", true) != 0) || string.IsNullOrEmpty(userResMdl.SourceId))
			{
				return;
			}
			NobookSingleActionHelper.ModifyChapterAndName(userResMdl.SourceId, new PepCoursewareInfoResponse
			{
				BookId = userResMdl.Tbid,
				BookName = userResMdl.TbName,
				ChapterId = userResMdl.OriTreeCode,
				ChapterName = userResMdl.OriTreeName
			}, userResMdl.Title);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001431A File Offset: 0x0001251A
		private void btnDeleteResource_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			this.DelRes(this.SelectRes);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014334 File Offset: 0x00012534
		private void DelRes(UserResourceModel userResMdl)
		{
			MyResControl.<DelRes>d__79 <DelRes>d__;
			<DelRes>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<DelRes>d__.<>4__this = this;
			<DelRes>d__.userResMdl = userResMdl;
			<DelRes>d__.<>1__state = -1;
			<DelRes>d__.<>t__builder.Start<MyResControl.<DelRes>d__79>(ref <DelRes>d__);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014374 File Offset: 0x00012574
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
			{
				UserResourceModel userResourceModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as UserResourceModel;
				if (userResourceModel != null)
				{
					userResourceModel.ThumbUrl = ResourcesHelper.GetResThumbByExtension(userResourceModel.FileFormat);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("设置我的资源缩略图失败:[{0}]", arg));
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000143E4 File Offset: 0x000125E4
		private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				UserResourceModel userResourceModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as UserResourceModel;
				if (userResourceModel != null)
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200154",
						Passive = userResourceModel.ID,
						FromPos = MyResControl.mClassPath + ".TextBlock_PreviewMouseLeftButtonUp"
					});
					StatisticsHelper.Instance.OpenMyRes(userResourceModel.ID, userResourceModel.ExZynrlID);
					if (userResourceModel.PvtbizType == "201" || string.Compare(userResourceModel.FileFormat, ".nbex", true) == 0)
					{
						if (string.IsNullOrEmpty(userResourceModel.SourceId))
						{
							LogHelper.Instance.Error("课件资源打开时，用户资源id:[" + ((userResourceModel != null) ? userResourceModel.ID : null) + "]的课件id为空。");
						}
						else
						{
							NobookSingleActionHelper.Open(userResourceModel.SourceId, false, false);
						}
					}
					else
					{
						OpenResParamter openResPara = new OpenResParamter
						{
							IsShowSaveBtn = false,
							IsShowPlayer = true,
							IsEditeMode = false,
							IsCloseWhenSave = false,
							OfficeFileLocal = true,
							CloseWhenPlayCompleted = false,
							PlayWhenLoaded = true,
							ShowResInfo = true,
							IsNqRes = false,
							GgbDataSaveCallBack = null,
							ClassActivityDataSaveCallBack = null,
							ClassActivityInfo = null,
							DicTurnPage = null,
							TurnPageCallBack = null,
							NqRes = null,
							UserRes = userResourceModel,
							ResId = userResourceModel.ID,
							SourceType = ResSourceType.MyRes,
							UseLocalResPlay = (userResourceModel.PosType != 2),
							OwnerWin = this.ownerWin,
							DownloadUserResCallBack = new Action<UserResourceModel, Window>(this.DownloadRes),
							ShareUserResCallBack = new Action<UserResourceModel, Window>(this.ShareRes),
							UserResAddCourseCallBack = new Action<UserResourceModel, Window>(this.AddCourse)
						};
						PlayResHelper.Instance.OpenUserRes(openResPara);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("资源打开失败！" + ex.ToString());
				CustomMessageBox.Info("资源打开失败！", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, true);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001461C File Offset: 0x0001281C
		private void btnDownload_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			this.DownloadRes(this.SelectRes, this.ownerWin);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001463C File Offset: 0x0001283C
		private void DownloadRes(UserResourceModel userResMdl, Window owner)
		{
			try
			{
				if (userResMdl != null)
				{
					if (userResMdl.PvtbizType == "201" || string.Compare(userResMdl.FileFormat, ".nbex", true) == 0)
					{
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx510007",
							FromPos = MyResControl.mClassPath + ".DownloadRes",
							Params = string.Format("资源ID:" + userResMdl.ID, new object[0])
						});
						if (string.IsNullOrEmpty(userResMdl.SourceId))
						{
							LogHelper.Instance.Error("课件资源下载时，用户资源id:[" + ((userResMdl != null) ? userResMdl.ID : null) + "]的课件id为空。");
						}
						else
						{
							NobookSingleActionHelper.ExportByPepcx(userResMdl.SourceId);
							StatisticsHelper.Instance.DownloadRes(userResMdl.ID);
						}
					}
					else if (!string.IsNullOrEmpty(userResMdl.EcryType))
					{
						CustomMessageBox.Info("抱歉，该资源为：教材配套资源，暂不支持用户下载", "确定", "", owner, WindowStartupLocation.CenterOwner, true);
					}
					else
					{
						SaveFileDialog saveFileDialog = new SaveFileDialog();
						saveFileDialog.Title = UtilityHelper.GetInvalidFileName(userResMdl.Title ?? "");
						FileDialog fileDialog = saveFileDialog;
						string fileFormat = userResMdl.FileFormat;
						fileDialog.DefaultExt = ((fileFormat != null) ? fileFormat.TrimStart(new char[]
						{
							'.'
						}) : null);
						saveFileDialog.FileName = UtilityHelper.GetInvalidFileName(userResMdl.Title + userResMdl.FileFormat);
						saveFileDialog.Filter = "files (*" + userResMdl.FileFormat + ")|*" + userResMdl.FileFormat;
						SaveFileDialog saveFileDialog2 = saveFileDialog;
						if (saveFileDialog2.ShowDialog() == DialogResult.OK)
						{
							TrackerManager.Tracker.OnEvent(new EventActivity
							{
								ActionId = "jx510020",
								FromPos = MyResControl.mClassPath + ".DownloadRes",
								Params = string.Format("资源ID:" + userResMdl.ID, new object[0])
							});
							string fileName = saveFileDialog2.FileName;
							DownloadResParamter downloadResPara = new DownloadResParamter
							{
								ResId = userResMdl.ID,
								DownloadUrl = userResMdl.FilePath,
								DeviceFlag = DeviceFlags.CentralUserResource,
								IsNqRes = false,
								SaveFilePath = fileName,
								ResUserId = userResMdl.Creator
							};
							new DownloadResWindow
							{
								DownloadResPara = downloadResPara,
								Owner = this.ownerWin
							}.ShowDialog();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("下载用户资源失败。" + ex.ToString());
				CustomMessageBox.Info("下载资源失败，请重试", "确定", "", owner, WindowStartupLocation.CenterOwner, true);
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000148F0 File Offset: 0x00012AF0
		private void btnAddCourseMenu_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			this.AddCourse(this.SelectRes, this.ownerWin);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00014910 File Offset: 0x00012B10
		private void btnAddCourse_Click(object sender, RoutedEventArgs e)
		{
			UserResourceModel userResourceModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as UserResourceModel;
			if (userResourceModel == null)
			{
				return;
			}
			this.AddCourse(userResourceModel, this.ownerWin);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014950 File Offset: 0x00012B50
		private void AddCourse(UserResourceModel model, Window owner)
		{
			try
			{
				if (model == null || string.IsNullOrEmpty(model.OriTreeCode) || string.IsNullOrEmpty(model.Tbid))
				{
					LogHelper.Instance.Error("资源添加至课程失败！[获取不到Model对象]");
				}
				else
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx230002",
						Passive = model.ID,
						FromPos = MyResControl.mClassPath + ".AddCourse"
					});
					string myRes = Consts.MyRes;
					string myResTitle = Consts.MyResTitle;
					string type = "1360";
					ResAddCourseWindow resAddCourseWindow = new ResAddCourseWindow();
					resAddCourseWindow.DataStatistic = StatisticsHelper.Instance;
					resAddCourseWindow.CourseRes = new CourseResModel
					{
						Id = model.ID,
						Title = model.Title,
						Remarks = model.Title,
						Dzwjlx = model.Dzwjlx,
						DzwjlxName = model.DzwjlxName,
						DzwjLxZhpt = model.DzwjLxZhpt,
						DzwjLxZhptName = model.DzwjLxZhptName,
						FileFormat = model.FileFormat,
						SourceId = model.SourceId,
						ResPath = model.FilePath,
						UserId = model.Creator,
						ResLyId = myRes,
						ResLYTitle = myResTitle,
						ZynrlxId = model.ExZynrlID,
						ZynrlxName = model.ExZynrlName,
						ZyLxZhpt = model.ZyLxZhpt,
						ZyLxZhptName = model.ZyLxZhptName,
						ResLxType = ((model.ZyLxZhpt == "01") ? 1.ToString() : 2.ToString())
					};
					resAddCourseWindow.Tbid = model.Tbid;
					resAddCourseWindow.TbName = TextBookInfoDataHelper.Instance.GetTextBookName(model.Tbid);
					resAddCourseWindow.LstChapterIds = UtilityHelper.GetSubChapterLst(model.OriTreeCode, model.Tbid);
					resAddCourseWindow.ChapterName = model.OriTreeName;
					MaskLayerWindow.SingleInstance().ShowMaskWindow(owner);
					resAddCourseWindow.Owner = MaskLayerWindow.SingleInstance();
					bool? flag = resAddCourseWindow.ShowDialog();
					if (flag != null && flag.Value)
					{
						ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
						{
							Content = "添加成功",
							IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
						});
						this.NotifyAddCourseCount(type, model.ID, CommonParamter.Instance.LoginUserId, 1);
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("资源添加至课程[{0}]。", arg));
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00014BE4 File Offset: 0x00012DE4
		private void NotifyAddCourseCount(string type, string id, string userid, int num)
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(ConfigHelper.WebServerUrl + string.Format("statistic/countDownload.json?type={0}&id={1}&num={2}&userid={3}", new object[]
					{
						type,
						id,
						num,
						userid
					}), null, true, "");
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "调用平台下载次数接口失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			});
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014C1C File Offset: 0x00012E1C
		private void btnMore_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
				ListBoxItem listBoxItem = VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
				listBoxItem.IsSelected = true;
				if (listBoxItem.DataContext is UserResourceModel && button != null)
				{
					if (this.SelectRes != null)
					{
						this.btnShare.Visibility = Visibility.Visible;
						this.btnAddCourse.Visibility = Visibility.Visible;
						this.btnEditeNb.Visibility = Visibility.Visible;
						this.btnCopyNb.Visibility = Visibility.Visible;
						this.btnSynchro.Visibility = Visibility.Visible;
						this.btnDownload.Visibility = Visibility.Visible;
						this.btnDel.Visibility = Visibility.Visible;
						if (!this.ShowMoreBtn)
						{
							this.btnShare.Visibility = Visibility.Collapsed;
							this.btnAddCourse.Visibility = Visibility.Collapsed;
						}
						if (!string.IsNullOrEmpty(this.SelectRes.EcryType) || !this.IsOnLine)
						{
							this.btnDownload.Visibility = Visibility.Collapsed;
							this.btnShare.Visibility = Visibility.Collapsed;
						}
						if (string.Compare(this.SelectRes.FileFormat, ".nbex", true) != 0)
						{
							this.btnEditeNb.Visibility = Visibility.Collapsed;
							this.btnCopyNb.Visibility = Visibility.Collapsed;
						}
						else
						{
							this.btnSynchro.Visibility = Visibility.Collapsed;
						}
						if (string.Compare(this.SelectRes.FileFormat, ".ca", true) == 0)
						{
							this.btnEditeNb.Visibility = Visibility.Visible;
						}
						if (string.Compare(this.SelectRes.FileFormat, ".ggb", true) == 0)
						{
							this.btnEditeNb.Visibility = Visibility.Visible;
						}
						this.resManagerMorePop.PlacementTarget = button;
						this.resManagerMorePop.IsOpen = true;
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("我的资源点击更多按钮处理失败:{0}。", arg));
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014DE8 File Offset: 0x00012FE8
		private void btnEditeNb_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			if (this.SelectRes == null)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "用户资源id:[";
				UserResourceModel selectRes = this.SelectRes;
				instance.Error(str + ((selectRes != null) ? selectRes.ID : null) + "]空。");
				return;
			}
			if (this.SelectRes.PvtbizType == "201" || string.Compare(this.SelectRes.FileFormat, ".nbex", true) == 0)
			{
				if (string.IsNullOrEmpty(this.SelectRes.SourceId))
				{
					LogHelper instance2 = LogHelper.Instance;
					string str2 = "用户资源id:[";
					UserResourceModel selectRes2 = this.SelectRes;
					instance2.Error(str2 + ((selectRes2 != null) ? selectRes2.ID : null) + "]的课件id为空。");
					return;
				}
				NobookSingleActionHelper.Edit(this.SelectRes.SourceId);
				return;
			}
			else
			{
				if (string.Compare(this.SelectRes.FileFormat, ".ca", true) == 0)
				{
					ClassActivityInfoModel userResCaData = ClassActivityHelper.GetUserResCaData(this.SelectRes, this.SelectRes.FullPath);
					OpenResParamter openResPara = new OpenResParamter
					{
						ResId = this.SelectRes.ID,
						ClassActivityInfo = userResCaData,
						ShowResInfo = false,
						IsEditeMode = true,
						IsShowSaveBtn = true,
						IsCloseWhenSave = false,
						SourceType = ResSourceType.MyRes,
						ClassActivityDataSaveCallBack = this.SaveCallBack,
						UserRes = this.SelectRes,
						OwnerWin = this.ownerWin
					};
					PlayResHelper.Instance.OpenCaData(openResPara, true);
					return;
				}
				if (string.Compare(this.SelectRes.FileFormat, ".ggb", true) == 0)
				{
					OpenResParamter openResParamter = new OpenResParamter
					{
						ResId = this.SelectRes.ID,
						ShowResInfo = false,
						IsShowSaveBtn = true,
						IsCloseWhenSave = false,
						IsNqRes = false,
						SourceType = ResSourceType.MyRes,
						UserRes = this.SelectRes,
						OwnerWin = this.ownerWin
					};
					string text = this.SelectRes.FullPath;
					if (File.Exists(text))
					{
						if (!string.IsNullOrEmpty(openResParamter.UserRes.EcryType))
						{
							string resUserFolder = PepPathHelper.GetResUserFolder(CommonParamter.Instance.CurrentUserId);
							text = CommonHandle.DecryptNormalRes(PathHelper.GetRelativeFileName(text), Path.GetDirectoryName(text), resUserFolder);
						}
						text = "file:///" + text;
					}
					else
					{
						text = TransFileCommonHelper.GetResUrl(DeviceFlags.CentralUserResource, this.SelectRes.FilePath);
					}
					PlayResHelper.Instance.OpenGGB("", text, openResParamter, true);
					this.SelectRes.ThumbUrl = this.SelectRes.ThumbUrl;
				}
				return;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015058 File Offset: 0x00013258
		private void btnCopyNb_Click(object sender, RoutedEventArgs e)
		{
			this.resManagerMorePop.IsOpen = false;
			if (this.SelectRes == null || string.IsNullOrEmpty(this.SelectRes.SourceId))
			{
				LogHelper instance = LogHelper.Instance;
				string str = "用户资源id:[";
				UserResourceModel selectRes = this.SelectRes;
				instance.Error(str + ((selectRes != null) ? selectRes.ID : null) + "]的课件id为空。");
				return;
			}
			NobookSingleActionHelper.Copy(this.SelectRes.SourceId, new Action<UserResourceModel>(this.CopyNbResFinish));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000150D4 File Offset: 0x000132D4
		private void CopyNbResFinish(UserResourceModel usRes)
		{
			if (usRes == null)
			{
				CustomMessageBox.Info("复制资源失败!", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, true);
				return;
			}
			usRes.PosType = 2;
			this.SearchResListData();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015108 File Offset: 0x00013308
		private bool ConfirmExistSyncRes(UserResourceModel curRes, out UserResourceModel uploadRes, out UserResourceModel downloadRes, out string delResId)
		{
			uploadRes = null;
			downloadRes = null;
			delResId = null;
			if (curRes == null)
			{
				return false;
			}
			switch (curRes.PosType)
			{
			case 1:
				uploadRes = curRes;
				break;
			case 2:
				downloadRes = curRes;
				break;
			case 3:
			{
				UserResourceModel userResByID = this.mUserResesDA.GetUserResByID(curRes.ID);
				if (userResByID == null)
				{
					downloadRes = curRes;
				}
				else
				{
					int num = curRes.ModifyTime.CompareTo(userResByID.ModifyTime);
					if (num > 0)
					{
						downloadRes = curRes;
					}
					else if (num < 0)
					{
						UtilityHelper.IsUserResChanged(curRes);
						uploadRes = curRes;
					}
					else if (UtilityHelper.IsUserResChanged(curRes))
					{
						uploadRes = curRes;
					}
				}
				break;
			}
			}
			return uploadRes != null || downloadRes != null || !string.IsNullOrEmpty(delResId);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000151B0 File Offset: 0x000133B0
		private void DelPageUserResInfo(List<UserResourceModel> lstUserRes)
		{
			if (lstUserRes == null || lstUserRes.Count == 0)
			{
				return;
			}
			if (BaseBookReader.Instance == null || !BaseBookReader.Instance.IsDocOpened)
			{
				return;
			}
			string strBookId = BaseBookReader.Instance.TextbookID;
			IEnumerable<UserResourceModel> enumerable = from a in lstUserRes
			where a.Tbid == strBookId
			select a;
			List<UserResourceModel> list = (enumerable != null) ? enumerable.ToList<UserResourceModel>() : null;
			if (list == null || list.Count == 0)
			{
				return;
			}
			Parallel.ForEach<UserResourceModel>(lstUserRes, delegate(UserResourceModel model)
			{
				BaseBookReader.Instance.RemoveUserAnnotByModel(model);
			});
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00015244 File Offset: 0x00013444
		private void SetUserResIcon(List<UserResourceModel> lstUserRes, bool isSetLocalIco)
		{
			if (lstUserRes == null || lstUserRes.Count == 0)
			{
				return;
			}
			if (BaseBookReader.Instance == null || !BaseBookReader.Instance.IsDocOpened)
			{
				return;
			}
			string strBookId = BaseBookReader.Instance.TextbookID;
			IEnumerable<UserResourceModel> enumerable = from a in lstUserRes
			where a.Tbid == strBookId
			select a;
			List<UserResourceModel> list = (enumerable != null) ? enumerable.ToList<UserResourceModel>() : null;
			if (list == null || list.Count == 0)
			{
				return;
			}
			Parallel.ForEach<UserResourceModel>(lstUserRes, delegate(UserResourceModel model)
			{
				BaseBookReader.Instance.SetUserResLocalInfo(model, isSetLocalIco);
			});
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000152CC File Offset: 0x000134CC
		private void SyncUploadUserReses(UserResourceModel uploadRes)
		{
			if (uploadRes == null)
			{
				return;
			}
			SyncResourcesManager.Instance.LstUploadUserRes.TransferList.SingleIsFinishedCallBack = new TransferListHelper.SingleIsFinished(this.ResUploadIsFinished);
			SyncResourcesManager.Instance.LstUploadUserRes.TransferList.SingleFailedCallback = null;
			SyncResourcesManager.Instance.LstUploadUserRes.TransferList.AllIsFinishedCallBack = null;
			if (CommonHandle.CheckIsUploading(uploadRes.ID))
			{
				DownloadResourcesWindow.GetInstance().ReStartUpload(uploadRes.ID);
				return;
			}
			string serverSaveDir = PathHelper.GetServerSaveDir(uploadRes.FilePath);
			string convertPath = PathHelper.GetConvertPath(uploadRes.FullPath);
			SyncResourcesManager.Instance.LstUploadUserRes.UpLoadFile(convertPath, serverSaveDir, uploadRes, true, new NotifyInfo(DownloadResourcesWindow.GetInstance().NotifyChange));
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200137",
				Passive = uploadRes.ID,
				FromPos = MyResControl.mClassPath + ".SyncUploadUserReses"
			});
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000153BC File Offset: 0x000135BC
		private void ResUploadIsFinished(TransferFileModel usermodel)
		{
			if (usermodel != null)
			{
				base.Dispatcher.BeginInvoke(new Action(delegate()
				{
					DownloadResourcesWindow.GetInstance().NotifyUploadIsFinished(usermodel);
				}), new object[0]);
				if (!this.SyncUploadUserResInfo(usermodel.UserRm))
				{
					CustomMessageBox.Info("同步失败！", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, true);
				}
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001542C File Offset: 0x0001362C
		private bool SyncUploadUserResInfo(UserResourceModel userResMdl)
		{
			bool result = false;
			try
			{
				if (userResMdl == null)
				{
					return result;
				}
				int state = userResMdl.State;
				userResMdl.State = 100;
				userResMdl.H5PageNum = 0;
				userResMdl.OffineH5Staus = 0;
				userResMdl.H5Url = string.Empty;
				List<UserResourceModel> list = new List<UserResourceModel>();
				list.Add(userResMdl);
				string text = JsonHelper.Instance.ListToJson<UserResourceModel>(list);
				string text2 = WebRequestHelper.HttpPostRequest("resuser/save.json", new Dictionary<string, string>
				{
					{
						"resList",
						text
					}
				}, new int?(30000), 0, false, true);
				ReturnJsonUserResourcesModel returnJsonUserResourcesModel = JsonHelper.Instance.JsonsDeserialize<ReturnJsonUserResourcesModel>(text2);
				if (returnJsonUserResourcesModel != null && returnJsonUserResourcesModel.Result == "110")
				{
					userResMdl.PosType = 3;
					this.mUserResesDA.UpdateUserRes(userResMdl);
					result = true;
				}
				else
				{
					userResMdl.State = state;
					LogHelper.Instance.Error("调用后台接口[resuser/save.json]失败[" + text2 + "]。");
				}
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx200138",
					Passive = userResMdl.ID,
					FromPos = MyResControl.mClassPath + ".SyncUploadUserResInfo",
					Request = Path.Combine(ConfigHelper.WebServerUrl, "resuser/save.json"),
					Params = "资源文件上传完成后上传资源数据Json:" + text,
					RetCode = returnJsonUserResourcesModel.Result
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
			return result;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000155BC File Offset: 0x000137BC
		private void SyncDownloadUserReses(UserResourceModel downloadRes)
		{
			if (downloadRes == null)
			{
				return;
			}
			SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.SingleIsFinishedCallBack = new TransferListHelper.SingleIsFinished(this.ResDownloadIsFinished);
			SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.AllIsFinishedCallBack = null;
			Tuple<bool, string, string, DeviceFlags> userResInfo = UserResourcesManager.Instance.GetUserResInfo(downloadRes, CommonParamter.Instance.LoginUserId);
			if (!userResInfo.Item1)
			{
				return;
			}
			string item = userResInfo.Item2;
			string item2 = userResInfo.Item3;
			DeviceFlags item3 = userResInfo.Item4;
			if (CommonHandle.CheckIsDownloading(Path.Combine(item2, Path.GetFileName(PathHelper.GetConvertPath(downloadRes.FilePath)))))
			{
				DownloadResourcesWindow.GetInstance().ReStartDownload(downloadRes.ID);
				return;
			}
			SyncResourcesManager.Instance.LstDownloadUserRes.DownLoadFile2Path(item, item2, downloadRes, true, item3, new NotifyInfo(DownloadResourcesWindow.GetInstance().NotifyChange));
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200139",
				Passive = downloadRes.ID,
				FromPos = MyResControl.mClassPath + ".SyncDownloadUserReses"
			});
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000156C4 File Offset: 0x000138C4
		private void ResDownloadIsFinished(TransferFileModel userMdl)
		{
			try
			{
				System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
				{
					try
					{
						DownloadResourcesWindow.GetInstance().NotifyDownloadIsFinished1(userMdl);
					}
					catch (Exception ex2)
					{
						LogHelper instance3 = LogHelper.Instance;
						string str = "我的资源下载资源，下载界面从下载列表移动到完成列表失败！";
						Exception ex3 = ex2;
						instance3.Error(str + ((ex3 != null) ? ex3.ToString() : null));
					}
				}), new object[0]);
				if (userMdl != null || userMdl.UserRm != null)
				{
					UserResourceModel userResMdl = userMdl.UserRm;
					if (userResMdl != null)
					{
						userResMdl.TbName = TextBookInfoDataHelper.Instance.GetTextBookName(userResMdl.Tbid);
						if (!string.IsNullOrEmpty(userResMdl.OriTreePos) && userResMdl.OriTreePos.Split(new char[]
						{
							','
						}).Length >= 2)
						{
							int pageNum = Convert.ToInt32(userResMdl.OriTreePos.Split(new char[]
							{
								','
							})[1]);
							userResMdl.PageNum = pageNum;
						}
						string fileMd5Value = new Md5Helper().GetFileMd5Value(PathHelper.GetConvertPath(userResMdl.FullPath));
						userResMdl.FileMd5 = fileMd5Value;
						userResMdl.PosType = 3;
						userResMdl.SourcePath = string.Empty;
						this.mUserResesDA.SaveUserRes(userResMdl);
						if (BaseBookReader.Instance != null && BaseBookReader.Instance.IsDocOpened)
						{
							BaseBookReader instance = BaseBookReader.Instance;
							if (instance != null)
							{
								instance.SetUserResLocalInfo(userResMdl, true);
							}
							BaseBookReader instance2 = BaseBookReader.Instance;
							if (instance2 != null)
							{
								instance2.AddUserResAnnot(userResMdl, true);
							}
						}
						Func<UserResourceModel, bool> <>9__2;
						base.Dispatcher.BeginInvoke(new Action(delegate()
						{
							ObservableCollection<UserResourceModel> resList = this.ResList;
							IEnumerable<UserResourceModel> enumerable;
							if (resList == null)
							{
								enumerable = null;
							}
							else
							{
								Func<UserResourceModel, bool> predicate;
								if ((predicate = <>9__2) == null)
								{
									predicate = (<>9__2 = ((UserResourceModel a) => a.ID == userResMdl.ID));
								}
								enumerable = resList.Where(predicate);
							}
							IEnumerable<UserResourceModel> enumerable2 = enumerable;
							if (enumerable2 != null && enumerable2.Count<UserResourceModel>() > 0)
							{
								enumerable2.FirstOrDefault<UserResourceModel>().PosType = 3;
							}
						}), new object[0]);
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx200140",
							Passive = userResMdl.ID,
							FromPos = MyResControl.mClassPath + ".ResDownloadIsFinished"
						});
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("我的资源下载资源，下载完成时回调函数[ResDownloadIsFinished]失败！" + ex.ToString());
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015914 File Offset: 0x00013B14
		private void ChangePage(int nPagNum)
		{
			this.mCurPageNum = nPagNum;
			this.SearchResListData();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00015924 File Offset: 0x00013B24
		public void ClearData()
		{
			this.ResParamter = null;
			this.ShowBottomInfo = false;
			this.mCurPageNum = 1;
			this.mTotlePage = 0;
			this.ResList.Clear();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015B60 File Offset: 0x00013D60
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				((Image)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				((Image)target).ImageFailed += this.Image_ImageFailed;
				return;
			case 3:
				((TextBlock)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				return;
			case 4:
				((System.Windows.Controls.Button)target).Click += this.btnEditResource_Click;
				return;
			case 5:
				((System.Windows.Controls.Button)target).Click += this.btnShareResource_Click;
				return;
			case 6:
				((System.Windows.Controls.Button)target).Click += this.btnAddCourse_Click;
				return;
			case 7:
				((System.Windows.Controls.Button)target).Click += this.btnMore_Click;
				return;
			case 8:
				((Image)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				((Image)target).ImageFailed += this.Image_ImageFailed;
				return;
			case 9:
				((TextBlock)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				return;
			case 10:
				((System.Windows.Controls.Button)target).Click += this.btnEditResource_Click;
				return;
			case 11:
				((System.Windows.Controls.Button)target).Click += this.btnShareResource_Click;
				return;
			case 12:
				((System.Windows.Controls.Button)target).Click += this.btnAddCourse_Click;
				return;
			case 13:
				((System.Windows.Controls.Button)target).Click += this.btnMore_Click;
				return;
			default:
				return;
			}
		}

		// Token: 0x040001CC RID: 460
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"UserControls",
			"MyResControl"
		});

		// Token: 0x040001CD RID: 461
		private const int PAGESIZE = 10;

		// Token: 0x040001CE RID: 462
		private Window ownerWin;

		// Token: 0x040001CF RID: 463
		private UserResourceDataAccess mUserResesDA = new UserResourceDataAccess();

		// Token: 0x040001D0 RID: 464
		private Md5Helper mMd5Oper = new Md5Helper();

		// Token: 0x040001D1 RID: 465
		public MyResourceParamter ResParamter;

		// Token: 0x040001D2 RID: 466
		private int mCurPageNum = 1;

		// Token: 0x040001D3 RID: 467
		private int mTotlePage;

		// Token: 0x040001D4 RID: 468
		private UserResourceModel mSelectRes;

		// Token: 0x040001D5 RID: 469
		private DownloadHelper mDownlaod = new DownloadHelper();

		// Token: 0x040001D6 RID: 470
		private volatile int countRequest;

		// Token: 0x040001D7 RID: 471
		private int mTotalCount;

		// Token: 0x040001D8 RID: 472
		private bool mShowBottomInfo;

		// Token: 0x040001D9 RID: 473
		public CreateGroupData CreateGroupDataCallBack;

		// Token: 0x040001DD RID: 477
		private bool mShowNoDataMessage;

		// Token: 0x040001DE RID: 478
		private string mMessageContent = string.Empty;

		// Token: 0x040001DF RID: 479
		private bool mIsOnLine = true;

		// Token: 0x040001E1 RID: 481
		public static readonly DependencyProperty IsDragProperty = DependencyProperty.Register("IsDrag", typeof(bool), typeof(MyResControl), new PropertyMetadata(true));

		// Token: 0x040001E2 RID: 482
		public static readonly DependencyProperty ShowMoreBtnProperty = DependencyProperty.Register("ShowMoreBtn", typeof(bool), typeof(MyResControl), new PropertyMetadata(false));

		// Token: 0x040001E3 RID: 483
		public static readonly DependencyProperty ImageTextCheckedProperty = DependencyProperty.Register("ImageTextChecked", typeof(bool), typeof(MyResControl), new PropertyMetadata(false));
	}
}
