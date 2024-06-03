using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Controls.CustomControl;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.DataStatistics;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using JXP.Windows.View;
using pep.Course.Commons;
using pep.Course.Views;
using pep.Nobook.Helpers;
using pep.sdk.utility.Paramter;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000045 RID: 69
	public partial class SchoolResControl : System.Windows.Controls.UserControl, INotifyPropertyChanged, IStyleConnector
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017C35 File Offset: 0x00015E35
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00017C3D File Offset: 0x00015E3D
		public Action<string, string, string, Window> OpenLocalUserRes { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00017C46 File Offset: 0x00015E46
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00017C4E File Offset: 0x00015E4E
		public Action<Window> CreateGroupDataCallback { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00017C57 File Offset: 0x00015E57
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00017C5F File Offset: 0x00015E5F
		public GetResultCount GetResultCountCallBack { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00017C68 File Offset: 0x00015E68
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00017C70 File Offset: 0x00015E70
		public MyResourceParamter ResParamter { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00017C79 File Offset: 0x00015E79
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00017C81 File Offset: 0x00015E81
		public ObservableCollection<SharedResDataModel> ResList { get; set; } = new ObservableCollection<SharedResDataModel>();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00017C8A File Offset: 0x00015E8A
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00017C92 File Offset: 0x00015E92
		public SharedResDataModel SelectRes
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00017CA6 File Offset: 0x00015EA6
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00017CAE File Offset: 0x00015EAE
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00017CC2 File Offset: 0x00015EC2
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00017CCA File Offset: 0x00015ECA
		public bool ShowGroupResNoData
		{
			get
			{
				return this.mShowGroupResNoData;
			}
			set
			{
				this.mShowGroupResNoData = value;
				this.OnPropertyRaised("ShowGroupResNoData");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00017CDE File Offset: 0x00015EDE
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00017CE6 File Offset: 0x00015EE6
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00017CFA File Offset: 0x00015EFA
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00017D02 File Offset: 0x00015F02
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00017D16 File Offset: 0x00015F16
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00017D1E File Offset: 0x00015F1E
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017D32 File Offset: 0x00015F32
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00017D44 File Offset: 0x00015F44
		public bool IsDrag
		{
			get
			{
				return (bool)base.GetValue(SchoolResControl.IsDragProperty);
			}
			set
			{
				base.SetValue(SchoolResControl.IsDragProperty, value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00017D57 File Offset: 0x00015F57
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00017D69 File Offset: 0x00015F69
		public bool ShowMoreBtn
		{
			get
			{
				return (bool)base.GetValue(SchoolResControl.ShowMoreBtnProperty);
			}
			set
			{
				base.SetValue(SchoolResControl.ShowMoreBtnProperty, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00017D7C File Offset: 0x00015F7C
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00017D8E File Offset: 0x00015F8E
		public int ShowMaxPageNumCount
		{
			get
			{
				return (int)base.GetValue(SchoolResControl.ShowMaxPageNumCountProperty);
			}
			set
			{
				base.SetValue(SchoolResControl.ShowMaxPageNumCountProperty, value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00017DA1 File Offset: 0x00015FA1
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00017DB3 File Offset: 0x00015FB3
		public bool ShowTotalCount
		{
			get
			{
				return (bool)base.GetValue(SchoolResControl.ShowTotalCountProperty);
			}
			set
			{
				base.SetValue(SchoolResControl.ShowTotalCountProperty, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00017DC6 File Offset: 0x00015FC6
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00017DD8 File Offset: 0x00015FD8
		public bool ImageTextChecked
		{
			get
			{
				return (bool)base.GetValue(SchoolResControl.ImageTextCheckedProperty);
			}
			set
			{
				base.SetValue(SchoolResControl.ImageTextCheckedProperty, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600043D RID: 1085 RVA: 0x00017DEC File Offset: 0x00015FEC
		// (remove) Token: 0x0600043E RID: 1086 RVA: 0x00017E24 File Offset: 0x00016024
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600043F RID: 1087 RVA: 0x00017E59 File Offset: 0x00016059
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00017E74 File Offset: 0x00016074
		public SchoolResControl()
		{
			this.InitializeComponent();
			this.ucPagingControl.PageIndexChanngedCallBack = new Action<int>(this.PageIndexChanngedCallBack);
			base.Loaded += this.SchoolResControl_Loaded;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00017EFF File Offset: 0x000160FF
		private void SchoolResControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.ownerWin = Window.GetWindow(this);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00017F10 File Offset: 0x00016110
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
			{
				SharedResDataModel sharedResDataModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
				if (sharedResDataModel != null)
				{
					sharedResDataModel.ThumbUrl = ResourcesHelper.GetResThumbByExtension(sharedResDataModel.FileFormat);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("设置默认图片失败:[{0}]。", arg));
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00017F80 File Offset: 0x00016180
		private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.PreviewRes(model);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00017FB4 File Offset: 0x000161B4
		private void btnPreview_Click(object sender, RoutedEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.PreviewRes(model);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00017FE8 File Offset: 0x000161E8
		private void btnPreview1_Click(object sender, RoutedEventArgs e)
		{
			this.popBtn.IsOpen = false;
			this.popImageLst.IsOpen = false;
			this.PreviewRes(this.SelectRes);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018010 File Offset: 0x00016210
		private void PreviewRes(SharedResDataModel model)
		{
			try
			{
				if (model == null)
				{
					LogHelper.Instance.Error("预览失败！[获取不到Model对象]");
				}
				else if (model.State == "70" || model.State == "90")
				{
					ToastWin.GetToaster(true, 450.0, 100.0).ShowInfo(new ToastInfo
					{
						Content = "当前资源已被删除，请取消收藏",
						IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
					});
				}
				else
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200057",
						Passive = model.ResId,
						FromPos = SchoolResControl.mClassPath + ".PreviewRes"
					});
					string type = "1320";
					OpenResParamter openResParamter = new OpenResParamter
					{
						IsShowSaveBtn = false,
						IsShowPlayer = true,
						IsEditeMode = false,
						IsCloseWhenSave = false,
						OfficeFileLocal = false,
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
						UserRes = null,
						ResId = model.ResId,
						OwnerWin = this.ownerWin,
						ResIsCollect = !string.IsNullOrEmpty(model.ResCollect),
						CollectShareSyncResCallBack = new Func<SharedResDataModel, Window, Task<bool>>(this.CollectRes),
						DownloadShareSyncResCallBack = new Action<SharedResDataModel, Window>(this.DownloadRes),
						AddCourseShareSyncResCallBack = new Action<SharedResDataModel, Window>(this.AddCourse)
					};
					if (model.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE || model.ResFlg == SdkConstants.NQ_RES_TYPE)
					{
						type = "1330";
						openResParamter.IsNqRes = true;
						openResParamter.SourceType = ResSourceType.SyncRes;
						bool flag = false;
						PlayResHelper.Instance.OpenSyncRes(null, openResParamter, ref flag);
					}
					else
					{
						openResParamter.IsNqRes = false;
						openResParamter.SourceType = ResSourceType.ShareRes;
						PlayResHelper.Instance.OpenUserRes(openResParamter);
					}
					if (model.ResAuthorId != CommonParamter.Instance.LoginUserId)
					{
						this.NotifyPreviewCount(type, model.ResId, CommonParamter.Instance.LoginUserId);
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("预览失败！[{0}]", arg));
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018278 File Offset: 0x00016478
		private void btnCollect_Click(object sender, RoutedEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.CollectRes(model, this.ownerWin);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000182B3 File Offset: 0x000164B3
		private void btnCollect1_Click(object sender, RoutedEventArgs e)
		{
			this.popBtn.IsOpen = false;
			this.popImageLst.IsOpen = false;
			this.CollectRes(this.SelectRes, this.ownerWin);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000182E0 File Offset: 0x000164E0
		private Task<bool> CollectRes(SharedResDataModel model, Window owner)
		{
			SchoolResControl.<CollectRes>d__87 <CollectRes>d__;
			<CollectRes>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<CollectRes>d__.<>4__this = this;
			<CollectRes>d__.model = model;
			<CollectRes>d__.owner = owner;
			<CollectRes>d__.<>1__state = -1;
			<CollectRes>d__.<>t__builder.Start<SchoolResControl.<CollectRes>d__87>(ref <CollectRes>d__);
			return <CollectRes>d__.<>t__builder.Task;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00018334 File Offset: 0x00016534
		private void btnDownload_Click(object sender, RoutedEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.DownloadRes(model, this.ownerWin);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001836E File Offset: 0x0001656E
		private void btnDownload1_Click(object sender, RoutedEventArgs e)
		{
			this.popBtn.IsOpen = false;
			this.popImageLst.IsOpen = false;
			this.DownloadRes(this.SelectRes, this.ownerWin);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001839C File Offset: 0x0001659C
		private void DownloadRes(SharedResDataModel model, Window owner)
		{
			if (model == null)
			{
				return;
			}
			if (model.State == "70" || model.State == "90")
			{
				ToastWin.GetToaster(true, 450.0, 100.0).ShowInfo(new ToastInfo
				{
					Content = "当前资源已被删除，请取消收藏",
					IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
				});
				return;
			}
			if (string.Compare(model.FileFormat, ".nbex", true) == 0)
			{
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx510007",
					FromPos = SchoolResControl.mClassPath + ".DownloadRes",
					Params = string.Format("资源ID:" + model.ResId, new object[0])
				});
				if (string.IsNullOrEmpty(model.SourceId))
				{
					LogHelper.Instance.Error("课件资源下载时，用户资源id:[" + model.ResId + "]的课件id为空。");
					return;
				}
				NobookSingleActionHelper.ExportByPepcx(model.SourceId);
				StatisticsHelper.Instance.DownloadRes(model.ResId);
				return;
			}
			else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = model.ResTitle;
				FileDialog fileDialog = saveFileDialog;
				string fileFormat = model.FileFormat;
				fileDialog.DefaultExt = ((fileFormat != null) ? fileFormat.TrimStart(new char[]
				{
					'.'
				}) : null);
				saveFileDialog.FileName = UtilityHelper.GetInvalidFileName(model.ResTitle + model.FileFormat);
				saveFileDialog.Filter = "files (*" + model.FileFormat + ")|*" + model.FileFormat;
				SaveFileDialog saveFileDialog2 = saveFileDialog;
				if (saveFileDialog2.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				string fileName = saveFileDialog2.FileName;
				bool isNqRes = false;
				DeviceFlags deviceFlag = DeviceFlags.CentralUserResource;
				if (model.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE)
				{
					isNqRes = true;
					deviceFlag = DeviceFlags.CentralResources;
				}
				DownloadResParamter downloadResPara = new DownloadResParamter
				{
					ResId = model.ResId,
					DownloadUrl = model.FilePath,
					DeviceFlag = deviceFlag,
					IsNqRes = isNqRes,
					SaveFilePath = fileName,
					ResUserId = model.ResAuthorId
				};
				new DownloadResWindow
				{
					DownloadResPara = downloadResPara,
					Owner = owner
				}.ShowDialog();
				return;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000185C1 File Offset: 0x000167C1
		private void btnAddCourseMenu_Click(object sender, RoutedEventArgs e)
		{
			this.popBtn.IsOpen = false;
			this.AddCourse(this.SelectRes, this.ownerWin);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000185E4 File Offset: 0x000167E4
		private void btnAddCourse_Click(object sender, RoutedEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.AddCourse(model, this.ownerWin);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00018620 File Offset: 0x00016820
		private void AddCourse(SharedResDataModel model, Window owner)
		{
			try
			{
				if (model == null || string.IsNullOrEmpty(model.OriTreeCode) || string.IsNullOrEmpty(model.TbId))
				{
					LogHelper.Instance.Error("资源添加至课程失败！[获取不到Model对象]");
				}
				else if (model.State == "70" || model.State == "90")
				{
					ToastWin.GetToaster(true, 450.0, 100.0).ShowInfo(new ToastInfo
					{
						Content = "当前资源已被删除，请取消收藏",
						IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
					});
				}
				else
				{
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx230002",
						Passive = model.ResId,
						FromPos = SchoolResControl.mClassPath + ".AddCourse"
					});
					string resLyId = string.Empty;
					string resLYTitle = string.Empty;
					string type = "1370";
					if (model.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE)
					{
						resLyId = Consts.PepRes;
						resLYTitle = "教材配套";
					}
					else if (model.ResFlg == SdkConstants.NQ_RES_TYPE)
					{
						resLyId = Consts.NqRes;
						resLYTitle = "教材资源";
					}
					else
					{
						type = "1360";
						if (model.ResAuthorId == CommonParamter.Instance.LoginUserId)
						{
							resLyId = Consts.MyRes;
							resLYTitle = Consts.MyResTitle;
						}
						else
						{
							resLyId = Consts.ShareRes;
							resLYTitle = "共享资源";
						}
					}
					ResAddCourseWindow resAddCourseWindow = new ResAddCourseWindow();
					resAddCourseWindow.DataStatistic = StatisticsHelper.Instance;
					resAddCourseWindow.CourseRes = new CourseResModel
					{
						Id = model.ResId,
						Title = model.ResTitle,
						Remarks = model.ResTitle,
						Dzwjlx = model.Dzwjlx,
						DzwjlxName = model.DzwjlxName,
						DzwjLxZhpt = model.DzwjLxZhpt,
						DzwjLxZhptName = model.DzwjLxZhptName,
						FileFormat = model.FileFormat,
						SourceId = model.SourceId,
						ResPath = model.FilePath,
						UserId = model.ResAuthorId,
						ResLyId = resLyId,
						ResLYTitle = resLYTitle,
						ZynrlxId = model.ExZynrlID,
						ZynrlxName = model.ExZynrlName,
						ZyLxZhpt = model.ZyLxZhpt,
						ZyLxZhptName = model.ZyLxZhptName,
						ResLxType = ((model.ZyLxZhpt == "01") ? 1.ToString() : 2.ToString())
					};
					resAddCourseWindow.Tbid = model.TbId;
					resAddCourseWindow.TbName = TextBookInfoDataHelper.Instance.GetTextBookName(model.TbId);
					resAddCourseWindow.LstChapterIds = UtilityHelper.GetSubChapterLst(model.OriTreeCode, model.TbId);
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
						this.NotifyDownloadCount(type, model.ResId, CommonParamter.Instance.LoginUserId, 1);
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("资源添加至课程[{0}]。", arg));
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001898C File Offset: 0x00016B8C
		private void btnQuote_Click(object sender, RoutedEventArgs e)
		{
			SharedResDataModel model = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as SharedResDataModel;
			this.QuoteNbRes(model);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000189C0 File Offset: 0x00016BC0
		private void btnQuote1_Click(object sender, RoutedEventArgs e)
		{
			this.popBtn.IsOpen = false;
			this.QuoteNbRes(this.SelectRes);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000189DC File Offset: 0x00016BDC
		private void QuoteNbRes(SharedResDataModel model)
		{
			try
			{
				if (model == null || string.Compare(model.FileFormat, ".nbex", true) != 0 || string.IsNullOrEmpty(model.SourceId))
				{
					LogHelper.Instance.Error("引用失败！[获取不到Model对象],资源id:[" + ((model != null) ? model.ResId : null) + "]。");
				}
				else
				{
					NobookSingleActionHelper.Ref(model.SourceId, model.ResTitle, true);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("引用NB精品资源失败：[{0}]。", arg));
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00018A70 File Offset: 0x00016C70
		private void btnMore_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
				ListBoxItem listBoxItem = VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
				listBoxItem.IsSelected = true;
				SharedResDataModel sharedResDataModel = listBoxItem.DataContext as SharedResDataModel;
				if (sharedResDataModel != null && button != null)
				{
					if (this.SelectRes != null)
					{
						this.btnPreview1.Visibility = Visibility.Visible;
						this.btnAddCourse.Visibility = Visibility.Visible;
						this.btnCollect1.Visibility = Visibility.Visible;
						this.btnDownload1.Visibility = Visibility.Visible;
						if (sharedResDataModel.ResFlg == "00" || (sharedResDataModel.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE && (this.SelectRes.Dzwjlx == "01" || this.SelectRes.Dzwjlx == "10")))
						{
							this.btnDownload1.Visibility = Visibility.Visible;
						}
						else
						{
							this.btnDownload1.Visibility = Visibility.Collapsed;
						}
						if (!string.IsNullOrEmpty(sharedResDataModel.ResCollect))
						{
							this.btnCollect1.Content = "取消收藏";
						}
						else
						{
							this.btnCollect1.Content = "收藏";
						}
						this.popBtn.PlacementTarget = button;
						this.popBtn.IsOpen = true;
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("人教/共享资源点击更多按钮处理失败:{0}。", arg));
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00018BDC File Offset: 0x00016DDC
		private void btnImageLstMore_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
			ListBoxItem listBoxItem = VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
			listBoxItem.IsSelected = true;
			SharedResDataModel sharedResDataModel = listBoxItem.DataContext as SharedResDataModel;
			if (sharedResDataModel == null || button == null)
			{
				return;
			}
			if (this.SelectRes == null)
			{
				return;
			}
			this.btnPreview2.Visibility = Visibility.Visible;
			this.btnCollect2.Visibility = Visibility.Visible;
			this.btnDownload2.Visibility = Visibility.Visible;
			if (sharedResDataModel.ResFlg == "00" || (sharedResDataModel.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE && (this.SelectRes.Dzwjlx == "01" || this.SelectRes.Dzwjlx == "10")))
			{
				this.btnDownload2.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnDownload2.Visibility = Visibility.Collapsed;
			}
			if (!string.IsNullOrEmpty(sharedResDataModel.ResCollect))
			{
				this.btnCollect2.Content = "取消收藏";
			}
			else
			{
				this.btnCollect2.Content = "收藏";
			}
			this.popImageLst.PlacementTarget = button;
			this.popImageLst.IsOpen = true;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00018CFE File Offset: 0x00016EFE
		public void NotifyDownloadCount(string type, string id, string userid, int num)
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

		// Token: 0x06000456 RID: 1110 RVA: 0x00018D33 File Offset: 0x00016F33
		public void NotifyPreviewCount(string type, string id, string userid)
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(string.Concat(new string[]
					{
						ConfigHelper.WebServerUrl,
						"statistic/countOpen.json?type=",
						type,
						"&id=",
						id,
						"&num=1&userid=",
						userid
					}), null, true, "");
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "调用平台预览次数接口失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			});
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00018D60 File Offset: 0x00016F60
		private void PageIndexChanngedCallBack(int pageNum)
		{
			try
			{
				this.mCurPageNum = pageNum;
				this.SearchResListData();
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("本校资源翻页检索失败：{0}。", arg));
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public void SearchDataAsync(string zyFormate, string zyLb, string keyword, string bookId, List<string> lstChapterId, string groupId = "", string resLy = "", string sort = "")
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
			this.ResParamter.GroupId = groupId;
			this.ResParamter.ResLy = resLy;
			this.ResParamter.Sort = sort;
			this.ShowBottomInfo = false;
			this.mCurPageNum = 1;
			this.mTotlePage = 0;
			this.mDicPageMark.Clear();
			this.SearchResListData();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00018E50 File Offset: 0x00017050
		internal void SearchResListData()
		{
			SchoolResControl.<SearchResListData>d__103 <SearchResListData>d__;
			<SearchResListData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<SearchResListData>d__.<>4__this = this;
			<SearchResListData>d__.<>1__state = -1;
			<SearchResListData>d__.<>t__builder.Start<SchoolResControl.<SearchResListData>d__103>(ref <SearchResListData>d__);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018E88 File Offset: 0x00017088
		private Task<SharedResListResultModel> GetSharedResDataListAsync()
		{
			SchoolResControl.<GetSharedResDataListAsync>d__104 <GetSharedResDataListAsync>d__;
			<GetSharedResDataListAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<SharedResListResultModel>.Create();
			<GetSharedResDataListAsync>d__.<>4__this = this;
			<GetSharedResDataListAsync>d__.<>1__state = -1;
			<GetSharedResDataListAsync>d__.<>t__builder.Start<SchoolResControl.<GetSharedResDataListAsync>d__104>(ref <GetSharedResDataListAsync>d__);
			return <GetSharedResDataListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00018ECC File Offset: 0x000170CC
		private Dictionary<string, string> GetPostParameter(out string strParamter)
		{
			strParamter = string.Empty;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["userid"] = CommonParamter.Instance.LoginUserId;
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
				if (!string.IsNullOrEmpty(this.ResParamter.GroupId))
				{
					dictionary["groupid"] = this.ResParamter.GroupId;
				}
			}
			string text = (this.ResParamter == null) ? "07" : this.ResParamter.ResLy;
			dictionary["sch_res_flag"] = text;
			if (text == "00")
			{
				dictionary["groupType"] = "0";
			}
			MyResourceParamter resParamter = this.ResParamter;
			if (!string.IsNullOrEmpty((resParamter != null) ? resParamter.Sort : null))
			{
				dictionary["sort"] = this.ResParamter.Sort;
			}
			dictionary["pagesize"] = 10.ToString();
			if (this.mCurPageNum <= 0)
			{
				this.mCurPageNum = 1;
			}
			dictionary["pageno"] = this.mCurPageNum.ToString();
			if (this.mDicPageMark.ContainsKey(this.mCurPageNum))
			{
				dictionary["cursorMark"] = this.mDicPageMark[this.mCurPageNum];
			}
			if (this.mCurPageNum == 1)
			{
				dictionary["cursorMark"] = "*";
			}
			strParamter = new JavaScriptSerializer().Serialize(dictionary);
			return dictionary;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001911F File Offset: 0x0001731F
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

		// Token: 0x0600045D RID: 1117 RVA: 0x0001914C File Offset: 0x0001734C
		public void ClearData()
		{
			this.ShowBottomInfo = false;
			this.mCurPageNum = 1;
			this.mTotlePage = 0;
			this.ResParamter = null;
			this.ResList.Clear();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00019175 File Offset: 0x00017375
		private void CreateGroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Action<Window> createGroupDataCallback = this.CreateGroupDataCallback;
			if (createGroupDataCallback == null)
			{
				return;
			}
			createGroupDataCallback(this.ownerWin);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000193CC File Offset: 0x000175CC
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
				((System.Windows.Controls.Button)target).Click += this.btnPreview_Click;
				return;
			case 5:
				((System.Windows.Controls.Button)target).Click += this.btnCollect_Click;
				return;
			case 6:
				((System.Windows.Controls.Button)target).Click += this.btnDownload_Click;
				return;
			case 7:
				((System.Windows.Controls.Button)target).Click += this.btnAddCourse_Click;
				return;
			case 8:
				((System.Windows.Controls.Button)target).Click += this.btnMore_Click;
				return;
			case 9:
				((Image)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				((Image)target).ImageFailed += this.Image_ImageFailed;
				return;
			case 10:
				((TextBlock)target).PreviewMouseLeftButtonUp += this.TextBlock_PreviewMouseLeftButtonUp;
				return;
			case 11:
				((System.Windows.Controls.Button)target).Click += this.btnAddCourse_Click;
				return;
			case 12:
				((System.Windows.Controls.Button)target).Click += this.btnImageLstMore_Click;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000226 RID: 550
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"UserControls",
			"SchoolResControl"
		});

		// Token: 0x04000227 RID: 551
		private const int PAGESIZE = 10;

		// Token: 0x04000228 RID: 552
		private ResDataObjectProvider mDataObjectProvider = new ResDataObjectProvider();

		// Token: 0x04000229 RID: 553
		private int mCurPageNum = 1;

		// Token: 0x0400022A RID: 554
		private int mTotlePage;

		// Token: 0x0400022B RID: 555
		private Window ownerWin;

		// Token: 0x0400022C RID: 556
		private DownloadHelper mDownlaod = new DownloadHelper();

		// Token: 0x0400022D RID: 557
		private EncryptorHelper mEncry = new EncryptorHelper();

		// Token: 0x0400022E RID: 558
		private volatile int countRequest;

		// Token: 0x0400022F RID: 559
		private int mTotalCount;

		// Token: 0x04000230 RID: 560
		private bool mShowBottomInfo;

		// Token: 0x04000231 RID: 561
		private Dictionary<int, string> mDicPageMark = new Dictionary<int, string>();

		// Token: 0x04000237 RID: 567
		private SharedResDataModel mSelectRes;

		// Token: 0x04000238 RID: 568
		private bool mShowNoDataMessage;

		// Token: 0x04000239 RID: 569
		private bool mShowGroupResNoData;

		// Token: 0x0400023A RID: 570
		private string mMessageContent = string.Empty;

		// Token: 0x0400023B RID: 571
		public static readonly DependencyProperty IsDragProperty = DependencyProperty.Register("IsDrag", typeof(bool), typeof(SchoolResControl), new PropertyMetadata(true));

		// Token: 0x0400023C RID: 572
		public static readonly DependencyProperty ShowMoreBtnProperty = DependencyProperty.Register("ShowMoreBtn", typeof(bool), typeof(SchoolResControl), new PropertyMetadata(false));

		// Token: 0x0400023D RID: 573
		public static readonly DependencyProperty ShowMaxPageNumCountProperty = DependencyProperty.Register("ShowMaxPageNumCount", typeof(int), typeof(SchoolResControl), new PropertyMetadata(10));

		// Token: 0x0400023E RID: 574
		public static readonly DependencyProperty ShowTotalCountProperty = DependencyProperty.Register("ShowTotalCount", typeof(bool), typeof(SchoolResControl), new PropertyMetadata(false));

		// Token: 0x0400023F RID: 575
		public static readonly DependencyProperty ImageTextCheckedProperty = DependencyProperty.Register("ImageTextChecked", typeof(bool), typeof(SchoolResControl), new PropertyMetadata(false));
	}
}
