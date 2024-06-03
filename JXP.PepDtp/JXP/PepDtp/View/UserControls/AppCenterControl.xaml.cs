using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Controls;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.DataAnalytics.Tracker;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.DataStatistics;
using JXP.PepDtp.ViewModel;
using JXP.PepUtility;
using JXP.Utilities;
using JXP.Windows;
using JXP.Windows.MsgBox;
using pep.Course.Commons;
using pep.Course.Views;
using pep.Nobook.Helpers;
using pep.sdk.reader.TextbookUtils;
using pep.sdk.utility.Paramter;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000038 RID: 56
	public partial class AppCenterControl : UserControl, IStyleConnector
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00011B6C File Offset: 0x0000FD6C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00011B74 File Offset: 0x0000FD74
		public Window OwnerWin { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00011B7D File Offset: 0x0000FD7D
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00011B85 File Offset: 0x0000FD85
		public Action<Window> OpenAppCenterWindowCallBack { get; set; }

		// Token: 0x06000304 RID: 772 RVA: 0x00011B90 File Offset: 0x0000FD90
		public AppCenterControl()
		{
			this.InitializeComponent();
			this.mAppCenterVM = new AppCenterViewModel();
			base.DataContext = this.mAppCenterVM;
			this.mAppCenterVM.SetItmWidthCallback = new Action(this.SetItemWidth);
			base.SizeChanged += this.AppCenterControl_SizeChanged;
			this.OwnerWin = Window.GetWindow(this);
			MessageBus.Default.Unregister<string>(this, SdkConstants.MESSAGEBUS_TOKEN_APPCENTER, new Action<string>(this.SetAppDownloadState));
			MessageBus.Default.Register<string>(this, SdkConstants.MESSAGEBUS_TOKEN_APPCENTER, new Action<string>(this.SetAppDownloadState), false);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011C39 File Offset: 0x0000FE39
		private void AppCenterControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.SetItemWidth();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void SetItemWidth()
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00011C44 File Offset: 0x0000FE44
		public void InitData()
		{
			try
			{
				object obj = AppCenterControl.objInit;
				lock (obj)
				{
					if (!this.mAppCenterVM.InitFlg)
					{
						this.gridList.Visibility = Visibility.Visible;
						this.gridDetail.Visibility = Visibility.Collapsed;
						this.mAppCenterVM.InitData();
					}
				}
			}
			catch (Exception ex)
			{
				this.mAppCenterVM.MessageContent = "数据检索失败";
				LogHelper instance = LogHelper.Instance;
				string str = "初始化应用中心数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		private void BtnDownload_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AppCenterModel appCenterModel = this.GetAppData(e);
				if (appCenterModel == null)
				{
					if (this.mAppCenterVM.CurAppCenterData == null)
					{
						return;
					}
					appCenterModel = this.mAppCenterVM.CurAppCenterData;
				}
				this.StartDownload(appCenterModel, false);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "下载应用失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011D68 File Offset: 0x0000FF68
		private void StartDownload(AppCenterModel appData, bool isUpdate)
		{
			BackgroundWorker backgroundWorker = null;
			if (this.mDicWork.TryGetValue(appData.ApplyId, out backgroundWorker) && backgroundWorker != null && backgroundWorker.IsBusy)
			{
				return;
			}
			this.mDicWork.TryRemove(appData.ApplyId, out backgroundWorker);
			appData.IsShowDownloading = true;
			BackgroundWorker backgroundWorker2 = new BackgroundWorker();
			backgroundWorker2.WorkerSupportsCancellation = true;
			backgroundWorker2.DoWork += this.DownloadWork_DoWork;
			backgroundWorker2.RunWorkerCompleted += this.DownloadWork_RunWorkerCompleted;
			DownloadParamter argument = new DownloadParamter
			{
				AppCenterData = appData,
				DownloadWork = backgroundWorker2,
				IsUpdate = isUpdate
			};
			backgroundWorker2.RunWorkerAsync(argument);
			this.mDicWork.TryAdd(appData.ApplyId, backgroundWorker2);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00011E1C File Offset: 0x0001001C
		private void DownloadWork_DoWork(object sender, DoWorkEventArgs e)
		{
			DownloadParamter downloadParamter = e.Argument as DownloadParamter;
			if (downloadParamter == null || downloadParamter.AppCenterData == null)
			{
				return;
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx510021",
				FromPos = AppCenterControl.mClassPath + ".DownloadWork_DoWork",
				Params = string.Format("下载学科工具开始，工具id:" + downloadParamter.AppCenterData.ApplyId, new object[0])
			});
			e.Result = downloadParamter.AppCenterData.ApplyId;
			this.mAppCenterVM.DownloadApp(downloadParamter);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00011EB8 File Offset: 0x000100B8
		private void DownloadWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Result != null)
			{
				BackgroundWorker backgroundWorker;
				this.mDicWork.TryRemove(e.Result.ToString(), out backgroundWorker);
			}
			ITracker tracker = TrackerManager.Tracker;
			EventActivity eventActivity = new EventActivity();
			eventActivity.ActionId = "jx510022";
			eventActivity.FromPos = AppCenterControl.mClassPath + ".DownloadWork_RunWorkerCompleted";
			EventActivity eventActivity2 = eventActivity;
			string str = "下载学科工具结束，工具id:";
			string str2;
			if (e == null)
			{
				str2 = null;
			}
			else
			{
				object result = e.Result;
				str2 = ((result != null) ? result.ToString() : null);
			}
			eventActivity2.Params = string.Format(str + str2, new object[0]);
			tracker.OnEvent(eventActivity);
			int message = (from a in this.mAppCenterVM.LstShowAppData
			where a.IsShowUpdate
			select a).Count<AppCenterModel>();
			MessageBus.Default.Send<int>(message, SdkConstants.MESSAGEBUS_TOKEN_APPLY_CENTER);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00011F90 File Offset: 0x00010190
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AppCenterModel appCenterModel = this.GetAppData(e);
				if (appCenterModel == null)
				{
					if (this.mAppCenterVM.CurAppCenterData == null)
					{
						return;
					}
					appCenterModel = this.mAppCenterVM.CurAppCenterData;
				}
				if (CustomMessageBox.Question("确定取消吗?", "确定", "取消", this.OwnerWin, WindowStartupLocation.CenterOwner, true, MessageIconType.WarnRound, ""))
				{
					BackgroundWorker backgroundWorker;
					this.mDicWork.TryGetValue(appCenterModel.ApplyId, out backgroundWorker);
					backgroundWorker.CancelAsync();
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "应用中心取消下载失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001203C File Offset: 0x0001023C
		private void BtnUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AppCenterModel appCenterModel = this.GetAppData(e);
				if (appCenterModel == null)
				{
					if (this.mAppCenterVM.CurAppCenterData == null)
					{
						return;
					}
					appCenterModel = this.mAppCenterVM.CurAppCenterData;
				}
				this.StartDownload(appCenterModel, true);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "下载应用失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000120B0 File Offset: 0x000102B0
		private void BtnOpen_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AppCenterModel appData = this.GetAppData(e);
				if (appData != null)
				{
					this.OpenApply(appData);
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "打开应用失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00012108 File Offset: 0x00010308
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				AppCenterModel appData = this.GetAppData(e);
				if (appData != null)
				{
					if (appData.IsShowOpen)
					{
						this.OpenApply(appData);
					}
					else if (appData.IsShowUpdate)
					{
						this.StartDownload(appData, true);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "应用中心点击封皮失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001217C File Offset: 0x0001037C
		private void OpenApply(AppCenterModel appData)
		{
			this.OpenSubjectTool((appData != null) ? appData.ApplyId : null, appData, false, this.OwnerWin, "", true);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000121A0 File Offset: 0x000103A0
		public void OpenSubjectTool(string applyId, AppCenterModel toolInfo, bool isOpenAppCenter, Window owner, string tbid = "", bool isDialog = true)
		{
			try
			{
				StatisticsHelper.Instance.OpenTool(applyId);
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx510024",
					FromPos = "Jxp.Subject.Tool.Commons.OpenSubjectTool",
					Passive = ((toolInfo != null) ? toolInfo.ApplyId : null),
					Params = string.Format("打开学科工具开始，工具id:" + ((toolInfo != null) ? toolInfo.ApplyId : null), new object[0])
				});
				string appToolPath = PepPathHelper.GetAppToolPath(applyId);
				AppType appType = (AppType)applyId.ToIntEx(0);
				if (appType <= AppType.SpeechEvaluatorOld)
				{
					if (appType != AppType.AddTool)
					{
						switch (appType)
						{
						case AppType.GGB:
						{
							string text = Path.Combine(appToolPath, toolInfo.StartUrl);
							if (!File.Exists(text))
							{
								if (isOpenAppCenter)
								{
									Action<Window> openAppCenterWindowCallBack = this.OpenAppCenterWindowCallBack;
									if (openAppCenterWindowCallBack != null)
									{
										openAppCenterWindowCallBack(owner);
									}
								}
								else
								{
									MessageBus.Default.Send<ToastInfo>(new ToastInfo
									{
										Content = "GGB工具不存在，请重新下载!",
										IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
									});
								}
								return;
							}
							OpenResParamter openResPara = new OpenResParamter
							{
								ShowResInfo = false,
								IsShowSaveBtn = true,
								IsCloseWhenSave = false,
								IsNqRes = false,
								SourceType = ResSourceType.MyRes,
								OwnerWin = owner
							};
							PlayResHelper.Instance.OpenGGB(text, "", openResPara, isDialog);
							goto IL_4CD;
						}
						case (AppType)1002:
							goto IL_4CD;
						case AppType.Card:
						{
							string text2 = Path.Combine(appToolPath, toolInfo.StartUrl);
							if (!File.Exists(text2))
							{
								if (isOpenAppCenter)
								{
									Action<Window> openAppCenterWindowCallBack2 = this.OpenAppCenterWindowCallBack;
									if (openAppCenterWindowCallBack2 != null)
									{
										openAppCenterWindowCallBack2(owner);
									}
								}
								else
								{
									MessageBus.Default.Send<ToastInfo>(new ToastInfo
									{
										Content = "汉字卡片工具不存在，请重新下载!",
										IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
									});
								}
								return;
							}
							string url = text2 + "?mode=1";
							OpenResParamter openResPara2 = new OpenResParamter
							{
								ShowResInfo = false,
								IsChineseCardRes = true,
								OwnerWin = owner
							};
							PlayResHelper.Instance.OpenNormalH5(url, openResPara2, isDialog);
							goto IL_4CD;
						}
						case AppType.SpeechEvaluatorOld:
							break;
						default:
							goto IL_4CD;
						}
					}
					else
					{
						Action<Window> openAppCenterWindowCallBack3 = this.OpenAppCenterWindowCallBack;
						if (openAppCenterWindowCallBack3 == null)
						{
							goto IL_4CD;
						}
						openAppCenterWindowCallBack3(owner);
						goto IL_4CD;
					}
				}
				else if (appType != AppType.Pinyin)
				{
					switch (appType)
					{
					case AppType.Freepiano:
					{
						string text3 = Path.Combine(appToolPath, toolInfo.StartUrl);
						if (!File.Exists(text3))
						{
							if (isOpenAppCenter)
							{
								Action<Window> openAppCenterWindowCallBack4 = this.OpenAppCenterWindowCallBack;
								if (openAppCenterWindowCallBack4 != null)
								{
									openAppCenterWindowCallBack4(owner);
								}
							}
							else
							{
								MessageBus.Default.Send<ToastInfo>(new ToastInfo
								{
									Content = "钢琴工具不存在，请重新下载!",
									IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
								});
							}
							return;
						}
						OpenResParamter openResPara3 = new OpenResParamter
						{
							OwnerWin = owner
						};
						PlayResHelper.Instance.OpenNormalH5(text3, openResPara3, isDialog);
						goto IL_4CD;
					}
					case (AppType)2002:
					case (AppType)2003:
					case (AppType)2004:
					case (AppType)2005:
					case AppType.Chemistrymodel:
						goto IL_4CD;
					case AppType.Physicsexperiment:
						new ToolDetailListWindow
						{
							IsDialog = isDialog,
							Owner = owner,
							SingleToolOwner = owner,
							AppToolType = AppType.Physicsexperiment
						}.ShowDialog();
						goto IL_4CD;
					case AppType.Biologyexperiment:
						new ToolDetailListWindow
						{
							IsDialog = isDialog,
							Owner = owner,
							AppToolType = AppType.Biologyexperiment
						}.ShowDialog();
						goto IL_4CD;
					case AppType.Chemistryexperiment:
						new ToolDetailListWindow
						{
							IsDialog = isDialog,
							Owner = owner,
							SingleToolOwner = owner,
							AppToolType = AppType.Chemistryexperiment
						}.ShowDialog();
						goto IL_4CD;
					case AppType.SpeechEvaluator:
						break;
					case AppType.Province:
					{
						string text4 = "https://jx-file10.mypep.cn/pub_cloud/110/2011/res/index.html";
						OpenResParamter openResPara4 = new OpenResParamter
						{
							PlayerType = PlayerEunmType.NormalH5,
							PlayerUrl = text4,
							OwnerWin = owner
						};
						PlayResHelper.Instance.OpenNormalH5(text4, openResPara4, isDialog);
						goto IL_4CD;
					}
					case AppType.Mathematics:
						new ToolDetailListWindow
						{
							IsDialog = isDialog,
							Owner = owner,
							SingleToolOwner = owner,
							AppToolType = AppType.Mathematics
						}.ShowDialog();
						goto IL_4CD;
					case AppType.ClasscalPoerty:
					{
						string classicalPoetryUrl = SdkConstants.ClassicalPoetryUrl;
						OpenResParamter openResPara5 = new OpenResParamter
						{
							PlayerType = PlayerEunmType.NormalH5,
							PlayerUrl = classicalPoetryUrl,
							OwnerWin = owner
						};
						PlayResHelper.Instance.OpenNormalH5(classicalPoetryUrl, openResPara5, isDialog);
						goto IL_4CD;
					}
					default:
						if (appType != AppType.ClassActivity)
						{
							goto IL_4CD;
						}
						new ClassActivityListWindow
						{
							IsDialog = isDialog,
							ClassActivityOwner = owner,
							Owner = owner
						}.ShowDialog();
						goto IL_4CD;
					}
				}
				else
				{
					string text5 = Path.Combine(appToolPath, toolInfo.StartUrl);
					if (!File.Exists(text5))
					{
						if (isOpenAppCenter)
						{
							Action<Window> openAppCenterWindowCallBack5 = this.OpenAppCenterWindowCallBack;
							if (openAppCenterWindowCallBack5 != null)
							{
								openAppCenterWindowCallBack5(owner);
							}
						}
						else
						{
							MessageBus.Default.Send<ToastInfo>(new ToastInfo
							{
								Content = "拼音工具不存在，请重新下载!",
								IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
							});
						}
						return;
					}
					OpenResParamter openResPara6 = new OpenResParamter
					{
						PlayerType = PlayerEunmType.NormalH5,
						PlayerUrl = text5,
						OwnerWin = owner
					};
					PlayResHelper.Instance.OpenNormalH5(text5, openResPara6, isDialog);
					goto IL_4CD;
				}
				SpeechEvaluatorHelper.OpenSpeechEvaluator(owner, tbid, isDialog);
				IL_4CD:
				StatisticsHelper.Instance.CloseTool(applyId);
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("打开应用失败，应用id：" + applyId + ex.ToString());
				CustomMessageBox.Info("打开应用失败!", "确定", "", owner, WindowStartupLocation.CenterOwner, true);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000126DC File Offset: 0x000108DC
		private void BtnDetail_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AppCenterModel appCenterModel = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as AppCenterModel;
				if (appCenterModel != null)
				{
					this.mAppCenterVM.CurAppCenterData = appCenterModel;
					this.gridList.Visibility = Visibility.Collapsed;
					this.gridDetail.Visibility = Visibility.Visible;
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "应用中心打开详情数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00012768 File Offset: 0x00010968
		private void BtnDel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.mAppCenterVM.CurAppCenterData != null)
				{
					if (CustomMessageBox.Question("确定删除吗?", "确定", "取消", this.OwnerWin, WindowStartupLocation.CenterOwner, true, MessageIconType.WarnRound, ""))
					{
						this.mAppCenterVM.DelApplyTool(this.mAppCenterVM.CurAppCenterData);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "删除应用失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000127F8 File Offset: 0x000109F8
		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.gridDetail.Visibility = Visibility.Collapsed;
				this.gridList.Visibility = Visibility.Visible;
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "应用中心返回列表数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012854 File Offset: 0x00010A54
		private void btnMoreMenue_Click(object sender, RoutedEventArgs e)
		{
			Button placementTarget = sender as Button;
			this.courseMorePop.PlacementTarget = placementTarget;
			this.courseMorePop.IsOpen = true;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00012880 File Offset: 0x00010A80
		private void btnNewCreate_Click(object sender, RoutedEventArgs e)
		{
			this.courseMorePop.IsOpen = false;
			NobookSingleActionHelper.Create(this.OwnerWin);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001289A File Offset: 0x00010A9A
		private void btnImportPPT_Click(object sender, RoutedEventArgs e)
		{
			this.courseMorePop.IsOpen = false;
			NobookSingleActionHelper.ImportPpt(true, this.OwnerWin);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000128B5 File Offset: 0x00010AB5
		private void btnImportCourse_Click(object sender, RoutedEventArgs e)
		{
			this.courseMorePop.IsOpen = false;
			NobookSingleActionHelper.ImportByPepcx();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000128CC File Offset: 0x00010ACC
		private AppCenterModel GetAppData(RoutedEventArgs e)
		{
			ListBoxItem listBoxItem = VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem;
			AppCenterModel appCenterModel = ((listBoxItem != null) ? listBoxItem.DataContext : null) as AppCenterModel;
			if (appCenterModel == null)
			{
				return null;
			}
			return appCenterModel;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00012908 File Offset: 0x00010B08
		private void SetAppDownloadState(string appid)
		{
			try
			{
				if (this.mAppCenterVM.SaveApplyData(appid))
				{
					BackgroundWorker backgroundWorker;
					if (!this.mDicWork.TryGetValue(appid, out backgroundWorker))
					{
						ObservableCollection<AppCenterModel> lstShowAppData = this.mAppCenterVM.LstShowAppData;
						AppCenterModel appCenterModel = (lstShowAppData != null) ? (from a in lstShowAppData
						where a.ApplyId == appid
						select a).FirstOrDefault<AppCenterModel>() : null;
						if (appCenterModel != null)
						{
							appCenterModel.IsShowOpen = true;
							appCenterModel.IsShowDelete = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "教材下时保存应用数据设失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void SetTitleVisible(bool showFlg)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012C04 File Offset: 0x00010E04
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 3:
				((Image)target).MouseLeftButtonDown += this.Image_MouseLeftButtonDown;
				return;
			case 4:
				((Button)target).Click += this.BtnDetail_Click;
				return;
			case 5:
				((IconButton)target).Click += this.BtnCancel_Click;
				return;
			case 6:
				((Button)target).Click += this.BtnDownload_Click;
				return;
			case 7:
				((Button)target).Click += this.BtnOpen_Click;
				return;
			case 8:
				((Button)target).Click += this.BtnUpdate_Click;
				return;
			case 9:
				((Button)target).Click += this.btnNewCreate_Click;
				return;
			case 10:
				((Button)target).Click += this.btnMoreMenue_Click;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000181 RID: 385
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"UserControls",
			"AppCenterControl"
		});

		// Token: 0x04000182 RID: 386
		private AppCenterViewModel mAppCenterVM;

		// Token: 0x04000183 RID: 387
		private static object objInit = new object();

		// Token: 0x04000184 RID: 388
		private ConcurrentDictionary<string, BackgroundWorker> mDicWork = new ConcurrentDictionary<string, BackgroundWorker>();
	}
}
