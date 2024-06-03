using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using Gma.System.MouseKeyHook;
using JXP.Cef.Utils;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.Networks;
using JXP.PepData;
using JXP.PepDtp.Common;
using JXP.PepDtp.DataStatistics;
using JXP.PepDtp.Events;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.PepClass;
using JXP.PepDtp.PepClass.View;
using JXP.PepDtp.View;
using JXP.PepDtp.View.TextBookReader;
using JXP.PepDtp.View.UserControls;
using JXP.PepDtp.ViewModel;
using JXP.PepUtility;
using JXP.Player.Players;
using JXP.Player.Players.UserControls;
using JXP.Threading;
using JXP.Utilities;
using JXP.WinAPI;
using JXP.Windows;
using JXP.Windows.MsgBox;
using JXP.Windows.View;
using JXP.WmTouchDevice;
using Microsoft.Windows.Shell;
using NamedPipeWrapper;
using Newtonsoft.Json;
using pep.Course.Commons;
using pep.Course.Delegates;
using pep.Course.Views;
using pep.Course.Views.Usercontrols;
using pep.Nobook;
using pep.Nobook.Helpers;
using pep.Nobook.Windows;
using pep.sdk.books.View.DtpWindows;
using pep.sdk.models;
using pep.sdk.reader.TextbookUtils;
using pep.sdk.reader.View;
using pep.sdk.reader.View.Textbook;
using pep.sdk.reader.View.UserControls;
using pep.sdk.reader.ViewModel;
using pep.sdk.utility;
using pep.sdk.utility.Common;
using pep.sdk.utility.Paramter;
using pep.sdk.utility.View;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp
{
	// Token: 0x02000007 RID: 7
	public partial class MainWindow : Window
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003B80 File Offset: 0x00001D80
		private void ReceiveToastInfo(ToastInfo info)
		{
			if (base.Dispatcher.CheckAccess())
			{
				ToastWin.GetToaster(this.txtTargetTip, true).ShowInfo(info);
				return;
			}
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.ReceiveToastInfo(info);
			}), new object[0]);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public void SetBtnsVisibleByLoginState(bool isLogin)
		{
			Visibility visibility = Visibility.Collapsed;
			if (isLogin)
			{
				visibility = Visibility.Visible;
			}
			this.ucMainMenu.btnMessage.Visibility = visibility;
			this.ucTeacherTitle.btnSetting.Visibility = visibility;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003C1C File Offset: 0x00001E1C
		public void SetMainUserInfo(string userPhoto, string userName, string schoolName)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					if (string.IsNullOrEmpty(userName))
					{
						this.ucMainMenu.lblName.Text = "未命名";
					}
					else
					{
						this.ucMainMenu.lblName.Text = userName;
					}
					this.ucMainMenu.lblSchool.Text = schoolName;
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "设置主窗体上的用户头像、显示名称( SetMainUserInfo)出错：";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			}), new object[0]);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003C64 File Offset: 0x00001E64
		public void SetMainMenue()
		{
			this.ucMainMenu.MenuLst.Clear();
			if (CommonParamter.Instance.IsLogin)
			{
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "首      页", MainMenuEnums.MyHomePage, 22, false, ""));
			}
			if (CommonParamter.Instance.IsLogin && CommonParamter.Instance.IsTeacher)
			{
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "备课授课", MainMenuEnums.TeachingCourse, 22, false, ""));
			}
			this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "数字教材", MainMenuEnums.Szjc, 22, false, ""));
			if (CommonParamter.Instance.IsLogin)
			{
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "资源中心", MainMenuEnums.Zygl, 22, false, ""));
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "工具中心", MainMenuEnums.PFTeachingCenter, 22, false, ""));
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "个人成长", MainMenuEnums.PepResources, 22, false, ""));
				this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "网络学院", MainMenuEnums.WebSchool, 22, false, ""));
			}
			this.ucMainMenu.MenuLst.Add(new MenuItemModel("", "国家中小学", MainMenuEnums.GjZhJyPatform, 22, true, "智慧教育平台"));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003DFC File Offset: 0x00001FFC
		private void SetOfflineMainMenueEnable()
		{
			foreach (MenuItemModel menuItemModel in this.ucMainMenu.MenuLst)
			{
				if (menuItemModel.PageIndex != MainMenuEnums.Szjc && menuItemModel.PageIndex != MainMenuEnums.Zygl)
				{
					menuItemModel.IsMenuEnnabled = false;
				}
			}
			this.ucSzjc.radioMyTextBook.IsEnabled = true;
			this.ucSzjc.radioBookCenter.IsEnabled = false;
			this.ucSzjc.radioShareBook.IsEnabled = false;
			this.ucResManage.radioMyRes.IsEnabled = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003EA8 File Offset: 0x000020A8
		private void MenuNavigateTo(MainMenuEnums menuIndex)
		{
			if (!CommonParamter.Instance.IsLogin)
			{
				this.ShowLogin();
				return;
			}
			this.MenuNavigate(menuIndex);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003EC4 File Offset: 0x000020C4
		internal void ClientNavigateTo(MainMenuEnums menuIndex)
		{
			this.ucMainMenu.SetSelectedItemByIndex(menuIndex);
			this.MenuNavigateTo(menuIndex);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003EDC File Offset: 0x000020DC
		private void MenuNavigate(MainMenuEnums menuIndex)
		{
			if (menuIndex <= MainMenuEnums.MyFriends)
			{
				if (menuIndex == MainMenuEnums.MyInfo)
				{
					return;
				}
				if (menuIndex == MainMenuEnums.MyHomePage)
				{
					this.controlBrower.Visibility = Visibility.Collapsed;
					this.ucResManage.Visibility = Visibility.Collapsed;
					this.ucSzjc.Visibility = Visibility.Collapsed;
					this.contentAppCenter.Visibility = Visibility.Collapsed;
					this.ucTeachingCourse.Visibility = Visibility.Collapsed;
					this.ucMyHome.Visibility = Visibility.Visible;
					this.ucMyHome.Init();
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx510001",
						Passive = "客户端",
						FromPos = MainWindow.mClassPath + ".MenuNavigate"
					});
					return;
				}
				if (menuIndex == MainMenuEnums.MyFriends)
				{
					return;
				}
			}
			else if (menuIndex <= MainMenuEnums.PFTeachingCenter)
			{
				if (menuIndex == MainMenuEnums.MyMessages)
				{
					return;
				}
				if (menuIndex == MainMenuEnums.PFTeachingCenter)
				{
					this.controlBrower.Visibility = Visibility.Collapsed;
					this.ucSzjc.Visibility = Visibility.Collapsed;
					this.ucResManage.Visibility = Visibility.Collapsed;
					this.ucTeachingCourse.Visibility = Visibility.Collapsed;
					this.ucMyHome.Visibility = Visibility.Collapsed;
					this.contentAppCenter.Visibility = Visibility.Visible;
					this.AddAppCenterContent();
					this.ucAppCenter.SetTitleVisible(true);
					this.ucAppCenter.InitData();
					this.ucAppCenter.SetItemWidth();
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx510004",
						Passive = "客户端",
						FromPos = MainWindow.mClassPath + ".MenuNavigate"
					});
					return;
				}
			}
			else
			{
				switch (menuIndex)
				{
				case MainMenuEnums.PrepareLessons:
					return;
				case MainMenuEnums.PepResources:
				case MainMenuEnums.Activity:
					break;
				case MainMenuEnums.Szjc:
					this.controlBrower.Visibility = Visibility.Collapsed;
					this.ucResManage.Visibility = Visibility.Collapsed;
					this.contentAppCenter.Visibility = Visibility.Collapsed;
					this.ucTeachingCourse.Visibility = Visibility.Collapsed;
					this.ucMyHome.Visibility = Visibility.Collapsed;
					this.ucSzjc.Visibility = Visibility.Visible;
					this.ucSzjc.InitData();
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200095",
						Passive = "客户端",
						FromPos = MainWindow.mClassPath + ".MenuNavigate"
					});
					return;
				case MainMenuEnums.Zygl:
					this.controlBrower.Visibility = Visibility.Collapsed;
					this.ucSzjc.Visibility = Visibility.Collapsed;
					this.contentAppCenter.Visibility = Visibility.Collapsed;
					this.ucTeachingCourse.Visibility = Visibility.Collapsed;
					this.ucMyHome.Visibility = Visibility.Collapsed;
					this.ucResManage.Visibility = Visibility.Visible;
					this.ucResManage.InitData();
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200094",
						Passive = "客户端",
						FromPos = MainWindow.mClassPath + ".MenuNavigate"
					});
					return;
				default:
					if (menuIndex == MainMenuEnums.TeachingCourse)
					{
						this.controlBrower.Visibility = Visibility.Collapsed;
						this.ucResManage.Visibility = Visibility.Collapsed;
						this.ucSzjc.Visibility = Visibility.Collapsed;
						this.contentAppCenter.Visibility = Visibility.Collapsed;
						this.ucMyHome.Visibility = Visibility.Collapsed;
						this.ucTeachingCourse.Visibility = Visibility.Visible;
						this.ucTeachingCourse.InitData();
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx510002",
							Passive = "客户端",
							FromPos = MainWindow.mClassPath + ".MenuNavigate"
						});
						return;
					}
					break;
				}
			}
			this.MainWinWebNavigate(menuIndex);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004230 File Offset: 0x00002430
		private void MainWinWebNavigate(MainMenuEnums menuIndex)
		{
			if (menuIndex != MainMenuEnums.Activity && menuIndex != MainMenuEnums.PepResources && menuIndex != MainMenuEnums.MyPepClass && menuIndex != MainMenuEnums.Work && menuIndex != MainMenuEnums.WebSchool)
			{
				LogHelper.Instance.Error(string.Format("主窗体Web页面跳转，跳转的索引:{0}不正确。", menuIndex));
				return;
			}
			this.ucResManage.Visibility = Visibility.Collapsed;
			this.ucSzjc.Visibility = Visibility.Collapsed;
			this.ucMyHome.Visibility = Visibility.Collapsed;
			this.ucLogin.Visibility = Visibility.Hidden;
			this.contentAppCenter.Visibility = Visibility.Collapsed;
			this.ucTeachingCourse.Visibility = Visibility.Collapsed;
			Visibility visibility = this.controlBrower.Visibility;
			this.controlBrower.Visibility = Visibility.Visible;
			string url;
			if (menuIndex == MainMenuEnums.WebSchool)
			{
				url = ConfigHelper.JCServerUrl + "rj/apply?token=" + CommonParamter.Instance.JcToken;
			}
			else if (menuIndex == MainMenuEnums.Work)
			{
				url = ConfigHelper.JCHomeworkUrl + "pep-exercise/?token=" + CommonParamter.Instance.JcToken;
			}
			else if (menuIndex == MainMenuEnums.PepResources)
			{
				url = AppConsts.PesonalData_Url + "?token=" + CommonParamter.Instance.JcToken;
			}
			else
			{
				int pageIndex = (int)menuIndex;
				if (menuIndex == MainMenuEnums.PepResources && !CommonParamter.Instance.IsAllPower)
				{
					pageIndex = 62;
				}
				url = ExplorerHelper.CreateWebUrlByIndex(pageIndex);
			}
			this.cefBrowser.NavigateTo(url);
			string actionId = string.Empty;
			if (menuIndex != MainMenuEnums.MyPepClass)
			{
				if (menuIndex == MainMenuEnums.WebSchool)
				{
					actionId = "jx511000";
				}
			}
			else
			{
				actionId = "jx200099";
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = actionId,
				Passive = "客户端",
				FromPos = MainWindow.mClassPath + ".MainWinWebNavigate"
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000043B4 File Offset: 0x000025B4
		private void SetMainWinVisibility(Visibility state)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				try
				{
					if (state != Visibility.Visible || !CommonParamter.Instance.CourseIsClassing)
					{
						this.Visibility = state;
						if (state == Visibility.Visible)
						{
							this.Activate();
						}
					}
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error(ex);
				}
			}), new object[0]);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000043F3 File Offset: 0x000025F3
		private void AddAppCenterContent()
		{
			this.contentAppCenter.Content = this.ucAppCenter;
			this.ucAppCenter.OwnerWin = this;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004412 File Offset: 0x00002612
		private void RemoveAppCenterContent()
		{
			this.contentAppCenter.Content = null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004420 File Offset: 0x00002620
		private void SetApplyUpdateCount(int count)
		{
			MenuItemModel menuItemModel = this.ucMainMenu.MenuLst.FirstOrDefault((MenuItemModel a) => a.PageIndex == MainMenuEnums.PFTeachingCenter);
			if (menuItemModel != null)
			{
				menuItemModel.NoticeCount = count;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004468 File Offset: 0x00002668
		private void InitLogin()
		{
			this.ucLogin.LoginSuccessCallBack = new LoginSuccess(this.LoginSuccessCallBack);
			this.ucLogin.BindingPhoneCallBack = new BindingPhone(this.BindingPhoneCallBack);
			this.ucLogin.UpdateLoginDataCallBack = new UpdateLoginData(this.UpdateLoginData);
			this.ucLogin.HideControlCallBack = new HideControl(this.HideLogin);
			this.ucLogin.btnMinimized.Click += this.imgBtnMinimized_Click;
			this.ucLogin.btnClose.Click += this.imgBtnClose_Click;
			this.ucLogin.gridTitle.MouseLeftButtonDown += this.GridTitle_MouseLeftButtonDown;
			this.ucLogin.gridTitle1.MouseLeftButtonDown += this.GridTitle_MouseLeftButtonDown;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004541 File Offset: 0x00002741
		private void GridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004552 File Offset: 0x00002752
		private void ShowLogin()
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				base.Width = 900.0;
				base.Height = 600.0;
				base.Left = this.mScreenWidth / 2.0 - 450.0;
				base.Top = this.mScreenHeight / 2.0 - 300.0;
				base.WindowState = WindowState.Normal;
				WindowChrome.SetWindowChrome(this, new WindowChrome
				{
					GlassFrameThickness = new Thickness(0.0),
					CaptionHeight = 0.0,
					CornerRadius = new CornerRadius(5.0),
					ResizeBorderThickness = new Thickness(0.0),
					UseAeroCaptionButtons = false
				});
				this.ucLogin.ReSetData();
				this.controlBrower.Visibility = Visibility.Collapsed;
				this.ucMainMenu.Visibility = Visibility.Collapsed;
				this.ucLogin.Visibility = Visibility.Visible;
			}), new object[0]);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004572 File Offset: 0x00002772
		private void HideLogin()
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.controlBrower.Visibility = Visibility.Visible;
				this.ucLogin.Visibility = Visibility.Collapsed;
				this.ucMainMenu.Visibility = Visibility.Visible;
				base.WindowState = WindowState.Maximized;
				WindowChrome.SetWindowChrome(this, new WindowChrome
				{
					GlassFrameThickness = new Thickness(0.0),
					CaptionHeight = 0.0,
					CornerRadius = new CornerRadius(0.0),
					ResizeBorderThickness = new Thickness(0.0),
					UseAeroCaptionButtons = false
				});
				this.ucLogin.StopQRWork();
			}), new object[0]);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004594 File Offset: 0x00002794
		private void LoginSuccessCallBack(LoginViewModel vm, LoginUserControl window)
		{
			this.mCurMouseKeybprdTime = DateTime.Now;
			this.mLoginOper.SaveLoginResultInfo(vm.UserInfo);
			CommonParamter.Instance.IsLogin = true;
			this.SetUserInfo();
			MessageBus.Default.Send<object>(null, AppConsts.JXP_REGISTER_TOKEN);
			this.RefreshLoginUserDatas();
			this.SetMainMenue();
			this.HeartBeatMethod();
			this.SetBtnsVisibleByLoginState(true);
			this.SaveLoginInfo(vm.UserId, vm.Password, vm.AutoLogin);
			this.ClientNavigateTo(MainMenuEnums.MyHomePage);
			DownloadResourcesWindow.GetInstance().UpdateHistoryData();
			DaemonServiceHelper.StartPipeNotifyServerAsync();
			ToolsUpdateHelper.CheckUpdateDocumenH5Async();
			this.ShowTutorials();
			this.ShowMessageInfo();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004638 File Offset: 0x00002838
		private void UpdateLoginData(LoginViewModel vm)
		{
			this.mLoginOper.SaveLoginResultInfo(vm.UserInfo);
			CommonParamter.Instance.IsLogin = true;
			this.SetUserInfo();
			this.RefreshLoginUserDatas();
			this.SetMainMenue();
			this.HeartBeatMethod();
			this.SetBtnsVisibleByLoginState(true);
			this.SaveLoginInfo(vm.UserId, vm.Password, vm.AutoLogin);
			DownloadResourcesWindow.GetInstance().UpdateHistoryData();
			ToolsUpdateHelper.CheckUpdateDocumenH5Async();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000046A8 File Offset: 0x000028A8
		private void HeartBeatMethod()
		{
			if (!CommonParamter.Instance.CourseIsOnline)
			{
				return;
			}
			try
			{
				if (this.dispatcherTimer != null)
				{
					this.dispatcherTimer.Elapsed -= this.DispatcherTimer_Elapsed;
					this.dispatcherTimer.Stop();
					this.dispatcherTimer.Enabled = false;
					this.dispatcherTimer.Dispose();
					this.dispatcherTimer = null;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
			this.dispatcherTimer = new System.Timers.Timer();
			this.dispatcherTimer.Elapsed += this.DispatcherTimer_Elapsed;
			this.dispatcherTimer.Interval = TimeSpan.FromMinutes(5.0).TotalMilliseconds;
			this.dispatcherTimer.Start();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000477C File Offset: 0x0000297C
		private void DispatcherTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				if (CommonParamter.Instance.CourseIsOnline)
				{
					if (DateTime.Now.Subtract(this.mCurMouseKeybprdTime).TotalMinutes >= AppConsts.ExitWithoutOperationTime)
					{
						this.DisposeHeartBeat();
						base.Dispatcher.Invoke(new Action(delegate()
						{
							new ExitWithoutOperationWindow
							{
								Topmost = true,
								WindowStartupLocation = WindowStartupLocation.CenterScreen,
								ExitLoginCallback = delegate
								{
									this.UserOffLine(false);
								},
								SetCurTimeCallback = delegate
								{
									this.mCurMouseKeybprdTime = DateTime.Now;
									this.HeartBeatMethod();
								}
							}.Show();
						}), new object[0]);
					}
					if (!string.IsNullOrEmpty(HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "p/user/delaySession.do", null, true, "")))
					{
						this.UserOffLine(true);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("心跳机制处理异常。" + ex.ToString());
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004840 File Offset: 0x00002A40
		private void DisposeHeartBeat()
		{
			if (this.dispatcherTimer != null)
			{
				this.dispatcherTimer.Elapsed -= this.DispatcherTimer_Elapsed;
				this.dispatcherTimer.Stop();
				this.dispatcherTimer.Enabled = false;
				this.dispatcherTimer.Dispose();
				this.dispatcherTimer = null;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004898 File Offset: 0x00002A98
		private void CloseOwnerMainWindow()
		{
			foreach (object obj in System.Windows.Application.Current.Windows)
			{
				Window window = (Window)obj;
				try
				{
					if (window != null && window.Visibility == Visibility.Visible && !(window is MaskLayerWindow) && (window.Owner == this || window.Owner is MaskLayerWindow) && window != null)
					{
						window.Close();
					}
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "关闭主窗体所有窗口失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004954 File Offset: 0x00002B54
		private void GetMsgCountFromServer()
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					string jsonStr = HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "message/getMessagesCountBz.json?userId=" + CommonParamter.Instance.LoginUserId, null, true, "");
					NotifyMessageModel msgData = JsonHelper.Instance.JsonsDeserialize<NotifyMessageModel>(jsonStr);
					if (msgData != null && msgData.Result == 110)
					{
						base.Dispatcher.BeginInvoke(new Action(delegate()
						{
							this.GetMessageCount(msgData.Count.ToString());
						}), new object[0]);
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("从服务端获取消息个数失败：{0}。", arg));
				}
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004968 File Offset: 0x00002B68
		private void GetMessageCount(string count)
		{
			int num = 0;
			if (int.TryParse(count, out num) && num > 0)
			{
				this.ucMainMenu.btnMessage.ShowNumber = Visibility.Visible;
				return;
			}
			this.ucMainMenu.btnMessage.ShowNumber = Visibility.Collapsed;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000049A8 File Offset: 0x00002BA8
		private void InitCourseCallback()
		{
			PepCourseDeleagte.Instance.UploadDataCallback = new Action<string, string>(StatisticsHelper.Instance.UploadCreateResData);
			PepCourseDeleagte.Instance.OpenTextBookCllaBack = new Action<string, string, List<UserResourceModel>, Window, bool, int>(this.OpenTextBook);
			PepCourseDeleagte.Instance.HiddenBookOperatorCallback = new Action(this.HiddenBookOperator);
			PepCourseDeleagte.Instance.ExitClientCallback = new Action(this.ExitFunc);
			PepCourseDeleagte.Instance.OpenAppCenterWindowCallBack = new Action<Window>(this.OpenAppCenterWin);
			PepCourseDeleagte.Instance.OpenSubjectToolCallBack = new Action<string, AppCenterModel, bool, Window, string, bool>(this.ucAppCenter.OpenSubjectTool);
			PepCourseDeleagte.Instance.GetOnlineToolCalBack = new Func<List<AppToolModel>>(this.mTextBookReader.GetOnlineAppTool);
			PepCourseDeleagte.Instance.DataStatistic = StatisticsHelper.Instance;
			PepCourseDeleagte.Instance.SaveCallBack = new Action<ClassActivityInfoModel, UserResourceModel, Window>(this.SaveClassActivityData);
			PepCourseDeleagte.Instance.SetMainWindowVisibilityCallBack = new Action<Visibility>(this.SetMainWinVisibility);
			PepCourseDeleagte.Instance.DataStatistic = StatisticsHelper.Instance;
			PepCourseDeleagte.Instance.CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin);
			OpenResManager.Instance.DataStatistic = StatisticsHelper.Instance;
			CourseContext.Instance.SetToolBarLogoIcoPath("pack://application:,,,/logo.ico");
			PepCourseDeleagte.Instance.EditeCourseCallback = new Action<CourseModel, BookChaptersInfo>(this.GotoEditCourse);
			PepCourseDeleagte.Instance.SetMainMenusWidth = new Action<double>(this.SetMainMenueWidth);
			CourseContext.Instance.UseNewPlayer = true;
			CourseContext.Instance.ShowUseNum = true;
			CourseContext.Instance.ShowAddGGBBtn = true;
			CourseContext.Instance.ShowAddClassBtn = true;
			CourseContext.Instance.ShowHomeworkBtn = false;
			CourseContext.Instance.ShowRecommendCourse = true;
			CourseContext.Instance.ShowPlatformShareRes = true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B55 File Offset: 0x00002D55
		private void GotoEditCourse(CourseModel courseData, BookChaptersInfo chapterInfo)
		{
			this.ucTeachingCourse.MyCourseChecked = true;
			this.ucTeachingCourse.ucMyCourse.GoEdite(courseData, chapterInfo);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004B75 File Offset: 0x00002D75
		private void SetMainMenueWidth(double dWidth)
		{
			this.menueCol.Width = new GridLength(dWidth);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004B88 File Offset: 0x00002D88
		public void CourseDataStatistic(string courseId, string actionTitle)
		{
			StatisticsHelper.Instance.UploadCourseData(courseId, actionTitle);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004B96 File Offset: 0x00002D96
		private void HiddenBookOperator()
		{
			CommonParamter.Instance.IsHiddenBookOperator = true;
			this.mTextBookReader.mBookOperator.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public void ExitFunc()
		{
			try
			{
				if (CommonParamter.Instance.IsInClass)
				{
					NetManager.Instance.SendPipeDataToClient(new PipeMessagePacket
					{
						Command = "exit"
					}, "");
				}
				CefWindow instance = CefWindow.Instance;
				if (instance != null)
				{
					instance.Close();
				}
				PepClassService.StopPepServer();
				DaemonServiceHelper.ExitPipeNotifyServer();
				ProcessHelper.DeleteProcessByName("JXP.Subprocess.exe");
				this.ExitWindow(true);
				ProcessHelper.DeleteProcessByName("JXP.PepDtp.exe");
				System.Windows.Application.Current.Shutdown();
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("退出客户端失败:[{0}]。", arg));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004C58 File Offset: 0x00002E58
		public void OpenTextBook(string bookId, string userId, List<UserResourceModel> userResList = null, Window owner = null, bool isOpenBookPage = false, int nPageNum = -1)
		{
			if (this.ucSzjc.ucMyBook.CheckBookIsDownload(bookId) == 1)
			{
				this.mTextBookReader.InitPageNum = nPageNum;
				this.mTextBookReader.OpenTextBook(bookId, userId, userResList, owner, isOpenBookPage);
				return;
			}
			OpenBookLoadingWindow openBookLoadingWindow = new OpenBookLoadingWindow(bookId);
			openBookLoadingWindow.InitPageNum = nPageNum;
			openBookLoadingWindow.IsOpenBookPage = isOpenBookPage;
			MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
			openBookLoadingWindow.Owner = MaskLayerWindow.SingleInstance();
			openBookLoadingWindow.ShowDialog();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004CCC File Offset: 0x00002ECC
		private void TextbookPageChange(string tbid, int beforPageNum, int afterPageNum)
		{
			if (this.mTextBookReader.isBookOpened && CommonParamter.Instance.CourseIsClassing && beforPageNum != afterPageNum)
			{
				string message = tbid + string.Format("_{0}.data", beforPageNum);
				MessageBus.Default.Send<string>(message, "SaveStroke");
				MessageBus.Default.Send<object>(null, "ClearStroke");
				string message2 = tbid + string.Format("_{0}.data", afterPageNum);
				MessageBus.Default.Send<string>(message2, "OpenStroke");
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004D54 File Offset: 0x00002F54
		private void OpenBookReuslt(bool result, string bookid, string msg)
		{
			if (result && CommonParamter.Instance.CourseIsClassing)
			{
				OpenResManager.Instance.HidenResWindow("");
				MessageBus.Default.Send<string>(string.Format("{0}_{1}.data", bookid, this.mTextBookReader.GetCurPageNum()), "OpenStroke");
				OpenResManager.Instance.AddResWindow(bookid, this.mTextBookReader);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004DBC File Offset: 0x00002FBC
		private void CloseBook(string tbid, int pageNum)
		{
			if (CommonParamter.Instance.CourseIsClassing)
			{
				MessageBus.Default.Send<string>(string.Format("{0}_{1}.data", tbid, pageNum), "SaveStroke");
				MessageBus.Default.Send<object>(null, "ClearStroke");
				OpenResManager.Instance.ResWin_Closed(this.mTextBookReader, null);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004E18 File Offset: 0x00003018
		private void InitMyHomeCallback()
		{
			this.ucMyHome.NavigateToMenueCallback = new Action<int>(this.NavigateToMenue);
			this.ucMyHome.EditeCourseCallback = new Action<CourseModel>(this.ucTeachingCourse.ucMyCourse.EditeCourseData);
			this.ucMyHome.GotoEditeCallback = new Action<CourseModel, BookChaptersInfo>(this.ucTeachingCourse.ucMyCourse.GoEdite);
			this.ucMyHome.StartClassCallback = new Action<CourseModel>(this.ucTeachingCourse.ucMyCourse.StartClass);
			this.ucMyHome.IsDownloadBookCallBack = new IsDownloadBook(this.ucSzjc.ucMyBook.CheckBookIsDownload);
			this.ucMyHome.IsContainBookidCallback = new Func<string, bool>(this.ucSzjc.ucMyBook.IsContainBookid);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004EE4 File Offset: 0x000030E4
		private void NavigateToMenue(int nType)
		{
			if (nType == 0)
			{
				this.ucSzjc.MyBookChecked = true;
				this.ClientNavigateTo(MainMenuEnums.Szjc);
				return;
			}
			if (nType == 1)
			{
				this.ucSzjc.BookCenterChecked = true;
				this.ClientNavigateTo(MainMenuEnums.Szjc);
				return;
			}
			if (nType == 2)
			{
				this.ucTeachingCourse.MyCourseChecked = true;
				this.ClientNavigateTo(MainMenuEnums.TeachingCourse);
				return;
			}
			if (nType == 3)
			{
				this.ucResManage.MyResChecked = true;
				this.ClientNavigateTo(MainMenuEnums.Zygl);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004F54 File Offset: 0x00003154
		private void InitApplyToolCallback()
		{
			ClassActivityHelper.DataStatistic = StatisticsHelper.Instance;
			SpeechEvaluatorHelper.DataStatistic = StatisticsHelper.Instance;
			ClassActivityHelper.SaveGGBResCallback = new Action<string, string, UserResourceModel, Window>(this.SaveggbRes);
			this.ucAppCenter.OpenAppCenterWindowCallBack = new Action<Window>(this.OpenAppCenterWin);
			PlayResHelper.Instance.AppToolDataSaveCallback = new Action<string, Window>(this.SaveAppToolData);
			PlayResHelper.Instance.SaveGGBResCallback = new Action<string, string, UserResourceModel, Window>(this.SaveggbRes);
			PlayResHelper.Instance.ClassActivityDataSaveCallBack = new Action<ClassActivityInfoModel, UserResourceModel, Window>(this.SaveClassActivityData);
			PlayResHelper.Instance.DataStatistic = StatisticsHelper.Instance;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004FF0 File Offset: 0x000031F0
		private void SaveClassActivityData(ClassActivityInfoModel data, UserResourceModel res, Window owner)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					string text = JsonConvert.SerializeObject(data);
					if (res != null)
					{
						if (!File.Exists(res.FullPath))
						{
							string resUserFolder = PepPathHelper.GetResUserFolder(CommonParamter.Instance.LoginUserId);
							if (!Directory.Exists(resUserFolder))
							{
								Directory.CreateDirectory(resUserFolder);
							}
							res.FullPath = System.IO.Path.Combine(resUserFolder, System.IO.Path.GetFileName(res.FilePath));
							using (StreamWriter streamWriter = new StreamWriter(res.FullPath, false, Encoding.UTF8))
							{
								streamWriter.Write(text);
							}
							res.FileSize = UtilityHelper.GetFileSize(res.FullPath);
							Md5Helper md5Helper = new Md5Helper();
							res.FileMd5 = md5Helper.GetFileMd5Value(res.FullPath);
							string loginUserId = CommonParamter.Instance.LoginUserId;
							string userShowName = CommonParamter.Instance.UserShowName;
							string curTime = CommonHandle.GetCurTime();
							res.Modifier = loginUserId;
							res.ModifierName = userShowName;
							res.ModifyTime = curTime;
						}
						else
						{
							using (StreamWriter streamWriter2 = new StreamWriter(res.FullPath, false, Encoding.UTF8))
							{
								streamWriter2.Write(text);
							}
						}
						if (!this.userResDA.ChechIsExist(res.ID))
						{
							this.userResDA.InsertUserResurce(res);
						}
						new EditeResWindow(res)
						{
							DataInfo = owner,
							Owner = owner,
							CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin),
							SaveSuccessCallBack1 = new Action<UserResourceModel, object>(this.SaveCAResCallback)
						}.ShowDialog();
					}
					else
					{
						string text2 = System.IO.Path.Combine(SdkConstants.SdkDataBaseDirectory, "Data\\ca");
						if (!Directory.Exists(text2))
						{
							Directory.CreateDirectory(text2);
						}
						string text3 = System.IO.Path.Combine(text2, Guid.NewGuid().ToString("N") + ".ca");
						File.WriteAllText(text3, text, Encoding.UTF8);
						new CreateNewResWindow(new CreateNewResParamter
						{
							SelectFilePath = text3,
							IsNeedSelectFile = false,
							InitZyLx = "02",
							InitZyFormat = "09"
						})
						{
							DataInfo = owner,
							UploadDataCallback = new Action<string, string>(StatisticsHelper.Instance.UploadCreateResData),
							CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin),
							Owner = owner,
							FinishCallback = new Action<UserResourceModel, object>(this.SaveCAResCallback)
						}.ShowDialog();
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("保存课堂活动数据失败:{0}。", arg));
				}
			}), new object[0]);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005040 File Offset: 0x00003240
		private void SaveCAResCallback(UserResourceModel res, object objWin)
		{
			ClassActivityPlayerWindow classActivityPlayerWindow = objWin as ClassActivityPlayerWindow;
			if (classActivityPlayerWindow != null)
			{
				classActivityPlayerWindow.UserRes = res;
				return;
			}
			GGBPlayerWindow ggbplayerWindow = objWin as GGBPlayerWindow;
			if (ggbplayerWindow != null)
			{
				ggbplayerWindow.UserRes = res;
				return;
			}
			ResPreviewWindow resPrevWin = objWin as ResPreviewWindow;
			if (resPrevWin != null)
			{
				base.Dispatcher.BeginInvoke(new Action(delegate()
				{
					GGBPlayerControl ggbplayerControl = resPrevWin.contentCtr.Content as GGBPlayerControl;
					if (ggbplayerControl != null)
					{
						ggbplayerControl.UserRes = res;
						return;
					}
					ClassActivityPlayerControl classActivityPlayerControl = resPrevWin.contentCtr.Content as ClassActivityPlayerControl;
					if (classActivityPlayerControl != null)
					{
						classActivityPlayerControl.UserRes = res;
					}
				}), new object[0]);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000050BC File Offset: 0x000032BC
		private void SaveggbRes(string ggbBase64, string pngBase64, UserResourceModel res, Window owner)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					byte[] array = Convert.FromBase64String(ggbBase64);
					string text = System.IO.Path.Combine(SdkConstants.SdkDataBaseDirectory, "Data\\ggb");
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					string text2 = string.Empty;
					if (!string.IsNullOrEmpty(pngBase64))
					{
						string pngBase65 = pngBase64;
						pngBase64 = ((pngBase65 != null) ? pngBase65.Replace("data:image/png;base64,", "") : null);
						byte[] array2 = Convert.FromBase64String(pngBase64);
						text2 = System.IO.Path.Combine(text, Guid.NewGuid().ToString("N") + ".jpg");
						using (FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write))
						{
							fileStream.Write(array2, 0, array2.Length);
							fileStream.Flush();
						}
					}
					if (res != null)
					{
						if (!File.Exists(res.FullPath))
						{
							string resUserFolder = PepPathHelper.GetResUserFolder(CommonParamter.Instance.LoginUserId);
							if (!Directory.Exists(resUserFolder))
							{
								Directory.CreateDirectory(resUserFolder);
							}
							res.FullPath = System.IO.Path.Combine(resUserFolder, System.IO.Path.GetFileName(res.FilePath));
							using (FileStream fileStream2 = new FileStream(res.FullPath, FileMode.Create, FileAccess.Write))
							{
								fileStream2.Write(array, 0, array.Length);
								fileStream2.Flush();
							}
							res.FileSize = UtilityHelper.GetFileSize(res.FullPath);
							Md5Helper md5Helper = new Md5Helper();
							res.FileMd5 = md5Helper.GetFileMd5Value(res.FullPath);
							string loginUserId = CommonParamter.Instance.LoginUserId;
							string userShowName = CommonParamter.Instance.UserShowName;
							string curTime = CommonHandle.GetCurTime();
							res.Modifier = loginUserId;
							res.ModifierName = userShowName;
							res.ModifyTime = curTime;
						}
						else
						{
							using (FileStream fileStream3 = new FileStream(res.FullPath, FileMode.Create, FileAccess.Write))
							{
								fileStream3.Write(array, 0, array.Length);
								fileStream3.Flush();
							}
						}
						if (!this.userResDA.ChechIsExist(res.ID))
						{
							this.userResDA.InsertUserResurce(res);
						}
						new EditeResWindow(res)
						{
							DataInfo = owner,
							Owner = owner,
							CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin),
							SaveSuccessCallBack1 = new Action<UserResourceModel, object>(this.SaveCAResCallback),
							ThumbFile = text2
						}.ShowDialog();
					}
					else
					{
						string text3 = System.IO.Path.Combine(text, Guid.NewGuid().ToString("N") + ".ggb");
						using (FileStream fileStream4 = new FileStream(text3, FileMode.Create, FileAccess.Write))
						{
							fileStream4.Write(array, 0, array.Length);
							fileStream4.Flush();
						}
						new CreateNewResWindow(new CreateNewResParamter
						{
							SelectFilePath = text3,
							ThumbFile = text2,
							IsNeedSelectFile = false,
							InitZyLx = "02",
							InitZyFormat = "09"
						})
						{
							DataInfo = owner,
							UploadDataCallback = new Action<string, string>(StatisticsHelper.Instance.UploadCreateResData),
							CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin),
							Owner = owner,
							FinishCallback = new Action<UserResourceModel, object>(this.SaveCAResCallback)
						}.ShowDialog();
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("保存ggb资源数据失败:{0}。", arg));
				}
			}), new object[0]);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005114 File Offset: 0x00003314
		public void SaveAppToolData(string jsonData, Window owner)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					H5ToolDataResult h5ToolDataResult = JsonConvert.DeserializeObject<H5ToolDataResult>(jsonData);
					string data = h5ToolDataResult.Data;
					string text = h5ToolDataResult.Thumbnail;
					AppType toolType = (AppType)h5ToolDataResult.ToolType;
					if (string.IsNullOrEmpty(data))
					{
						LogHelper.Instance.Error("资源保存Base64字符串为Null。");
						CustomMessageBox.Info("保存失败", "确定", "", owner, WindowStartupLocation.CenterScreen, false);
					}
					else
					{
						byte[] array = Convert.FromBase64String(data);
						string text2 = System.IO.Path.Combine(SdkConstants.SdkDataBaseDirectory, "Data\\apptool");
						if (!Directory.Exists(text2))
						{
							Directory.CreateDirectory(text2);
						}
						string text3 = string.Empty;
						if (!string.IsNullOrEmpty(text))
						{
							text = ((text != null) ? text.Replace("data:image/png;base64,", "") : null);
							byte[] array2 = Convert.FromBase64String(text);
							text3 = System.IO.Path.Combine(text2, Guid.NewGuid().ToString("N") + ".jpg");
							using (FileStream fileStream = new FileStream(text3, FileMode.Create, FileAccess.Write))
							{
								fileStream.Write(array2, 0, array2.Length);
								fileStream.Flush();
							}
						}
						string str = string.Empty;
						if (toolType == AppType.Freepiano)
						{
							str = ".xmid";
						}
						string text4 = System.IO.Path.Combine(text2, Guid.NewGuid().ToString("N") + str);
						using (FileStream fileStream2 = new FileStream(text4, FileMode.Create, FileAccess.Write))
						{
							fileStream2.Write(array, 0, array.Length);
							fileStream2.Flush();
						}
						new CreateNewResWindow(new CreateNewResParamter
						{
							SelectFilePath = text4,
							ThumbFile = text3,
							IsNeedSelectFile = false,
							InitZyLx = "02",
							InitZyFormat = "09"
						})
						{
							DataInfo = owner,
							UploadDataCallback = new Action<string, string>(StatisticsHelper.Instance.UploadCreateResData),
							CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin),
							Owner = owner
						}.ShowDialog();
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("保存学科工具数据失败:{0}。", arg));
				}
			}), new object[0]);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000515C File Offset: 0x0000335C
		private void InitResManagerData()
		{
			this.ucResManage.ucSyncRes.CreateGroupDataCallback = new Action<Window>(this.CreateGroupDataWin);
			this.ucResManage.CreateGroupDataCallBack = new CreateGroupData(this.CreateGroupDataWin);
			this.ucResManage.ucMyRes.CreateGroupDataCallBack = this.ucResManage.CreateGroupDataCallBack;
			this.ucResManage.ucMyRes.SaveCallBack = new Action<ClassActivityInfoModel, UserResourceModel, Window>(this.SaveClassActivityData);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000051D4 File Offset: 0x000033D4
		private void InitMainBrowser()
		{
			CommonParamter.Instance.UserDefaultPhoto = this.ucTeacherTitle.imgBtnUserPhoto.ImageSource;
			MainWindow.mMainBrowser = this.cefBrowser;
			if (CommonParamter.Instance.CourseIsOnline)
			{
				if (!CommonParamter.Instance.GetInternetState)
				{
					this.IsOnline = false;
				}
				else
				{
					this.IsOnline = true;
				}
			}
			else
			{
				this.IsOnline = false;
			}
			this.BeforeLogin();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005240 File Offset: 0x00003440
		private void BeforeLogin()
		{
			MainWindow.<BeforeLogin>d__46 <BeforeLogin>d__;
			<BeforeLogin>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<BeforeLogin>d__.<>4__this = this;
			<BeforeLogin>d__.<>1__state = -1;
			<BeforeLogin>d__.<>t__builder.Start<MainWindow.<BeforeLogin>d__46>(ref <BeforeLogin>d__);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005278 File Offset: 0x00003478
		private bool SetUserInfo()
		{
			bool result = false;
			try
			{
				UserModel firstUser = this.mUserDataOper.GetFirstUser();
				if (firstUser != null)
				{
					TrackerManager.Tracker.ProductInfoProvider.ActiveUser = firstUser.UserId;
					TrackerManager.Tracker.ProductInfoProvider.Organization = firstUser.OrgId;
					CommonParamter.Instance.LoginUserId = firstUser.UserId;
					CommonParamter.Instance.LoginUserName = firstUser.RegName;
					CommonParamter.Instance.CurrentUserId = firstUser.UserId;
					CommonParamter.Instance.UserShowName = firstUser.Name;
					CommonParamter.Instance.IsTeacher = ((firstUser.UserType == "A02") > false);
					CommonParamter.Instance.UserRole = firstUser.UserType;
					string orgsPath = string.Empty;
					string userId = firstUser.UserId;
					if (userId != null && userId.Length > 13)
					{
						orgsPath = firstUser.UserId.Substring(2, 6) + "/" + firstUser.UserId.Substring(2, 11);
					}
					CommonParamter.Instance.OrgID = firstUser.OrgId;
					CommonParamter.Instance.OrgName = firstUser.OrgName;
					CommonParamter.Instance.OrgsPath = orgsPath;
					List<NobookResourceType> lstHideTab = new List<NobookResourceType>();
					List<MyCoursewareMenu> lstHideMyCoursewareMenu = new List<MyCoursewareMenu>
					{
						MyCoursewareMenu.Preview,
						MyCoursewareMenu.AddTeachPlan,
						MyCoursewareMenu.DeleteTeachPlan
					};
					NbParameters.Instance.Initialize(lstHideTab, lstHideMyCoursewareMenu, null);
					NbParameters.Instance.DataStatistic = StatisticsHelper.Instance;
					this.mLoginOper.SetUserAuthorityInfo(firstUser);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("设置用户信息出错：" + ex.Message);
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005430 File Offset: 0x00003630
		public void RefreshLoginUserDatas()
		{
			this.GetMsgCountFromServer();
			MessageBus.Default.Send<SdkMessage>(new SdkMessage
			{
				Command = SdkCommand.Login
			});
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000544E File Offset: 0x0000364E
		public void NotifyMybookLogout()
		{
			MessageBus.Default.Send<SdkMessage>(new SdkMessage
			{
				Command = SdkCommand.Logout
			});
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005468 File Offset: 0x00003668
		private void Instance_OnMessageReceived(object sender, WinMessageArgs e)
		{
			if (e.Message.Msg == 74)
			{
				string lpData = ((WinApi.CopyDataStruct)Marshal.PtrToStructure(e.Message.LParam, typeof(WinApi.CopyDataStruct))).lpData;
				this.SwitchMessages(lpData);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000054B8 File Offset: 0x000036B8
		private void SwitchMessages(string msg)
		{
			string[] separator = new string[]
			{
				"??<*>()??"
			};
			string[] separator2 = new string[]
			{
				"&&<*>&&"
			};
			string[] array = msg.Split(separator, StringSplitOptions.None);
			string text = array[0];
			string[] funcParams = null;
			if (array.Length > 1)
			{
				funcParams = array[1].Split(separator2, StringSplitOptions.None);
			}
			string loginUserId = CommonParamter.Instance.LoginUserId;
			LogHelper.Instance.Info("JS Function[" + text + "] was called");
			if (text != null)
			{
				switch (text.Length)
				{
				case 6:
					if (!(text == "goback"))
					{
						goto IL_69D;
					}
					this.Goback();
					return;
				case 7:
					if (!(text == "PcLogin"))
					{
						goto IL_69D;
					}
					this.PcLogin(funcParams[0]);
					return;
				case 8:
					if (!(text == "openMyPc"))
					{
						goto IL_69D;
					}
					try
					{
						Process.Start(funcParams[0]);
						return;
					}
					catch (Exception)
					{
						LogHelper.Instance.Error("openMyPc失败：[" + funcParams[0] + "]");
						return;
					}
					break;
				case 9:
				{
					char c = text[0];
					if (c != 'E')
					{
						if (c != 'b')
						{
							goto IL_69D;
						}
						if (!(text == "buriedFun"))
						{
							goto IL_69D;
						}
						if (funcParams != null && funcParams.Length >= 5)
						{
							TrackerManager.Tracker.OnEvent(new EventActivity
							{
								ActionId = funcParams[0],
								Passive = funcParams[1],
								FromPos = funcParams[2],
								Request = funcParams[3],
								Params = funcParams[4]
							});
							return;
						}
						return;
					}
					else
					{
						if (!(text == "ExitLogin"))
						{
							goto IL_69D;
						}
						this.ExitLogin();
						return;
					}
					break;
				}
				case 10:
				{
					char c = text[0];
					if (c != 'N')
					{
						if (c != 'r')
						{
							goto IL_69D;
						}
						if (!(text == "resPreview"))
						{
							goto IL_69D;
						}
						base.Dispatcher.BeginInvoke(new Action(delegate()
						{
							if (funcParams != null && funcParams.Length > 1)
							{
								this.ResPreview(funcParams[0], funcParams[1]);
							}
						}), new object[0]);
						return;
					}
					else
					{
						if (!(text == "NavigateTo"))
						{
							goto IL_69D;
						}
						return;
					}
					break;
				}
				case 11:
					if (!(text == "UserOffLine"))
					{
						goto IL_69D;
					}
					this.UserOffLine(true);
					return;
				case 12:
				{
					char c = text[0];
					if (c != 'O')
					{
						if (c != 'r')
						{
							goto IL_69D;
						}
						if (!(text == "resAddCourse"))
						{
							goto IL_69D;
						}
					}
					else
					{
						if (!(text == "OpenTextBook"))
						{
							goto IL_69D;
						}
						base.Dispatcher.BeginInvoke(new Action(delegate()
						{
							CommonParamter.Instance.IsSeeMyBook = true;
							if (this.ucSzjc.ucMyBook.CheckBookIsDownload(funcParams[0]) != 1)
							{
								OpenBookLoadingWindow openBookLoadingWindow = new OpenBookLoadingWindow(funcParams[0]);
								MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
								openBookLoadingWindow.Owner = MaskLayerWindow.SingleInstance();
								openBookLoadingWindow.ShowDialog();
								return;
							}
							bool isOpenBookPage = false;
							int initPageNum = -1;
							if (funcParams.Length > 1 && funcParams[1] == "1")
							{
								isOpenBookPage = true;
							}
							if (funcParams.Length > 2)
							{
								int.TryParse(funcParams[2], out initPageNum);
							}
							this.mTextBookReader.InitPageNum = initPageNum;
							TextBookReader textBookReader = this.mTextBookReader;
							if (textBookReader == null)
							{
								return;
							}
							textBookReader.OpenTextBook(funcParams[0], CommonParamter.Instance.LoginUserId, null, this, isOpenBookPage);
						}), new object[0]);
						return;
					}
					break;
				}
				case 13:
					if (!(text == "StartPepClass"))
					{
						goto IL_69D;
					}
					base.Dispatcher.BeginInvoke(new Action(this.StartPepClass), new object[0]);
					return;
				case 14:
				case 23:
					goto IL_69D;
				case 15:
				{
					char c = text[0];
					if (c != 'G')
					{
						if (c != 'S')
						{
							goto IL_69D;
						}
						if (!(text == "ShowDebugWindow"))
						{
							goto IL_69D;
						}
						this.ShowDebugWindow("");
						return;
					}
					else
					{
						if (!(text == "GetMessageCount"))
						{
							goto IL_69D;
						}
						this.GetMessageCount(funcParams[0]);
						return;
					}
					break;
				}
				case 16:
					if (!(text == "openPepClassFile"))
					{
						goto IL_69D;
					}
					System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
					{
						PepClassCallbackCommand.DownloadOrView(funcParams[0], funcParams[1]);
					}), new object[0]);
					return;
				case 17:
					if (!(text == "openActivityVideo"))
					{
						goto IL_69D;
					}
					this.OpenActivityVideo();
					return;
				case 18:
				{
					char c = text[0];
					if (c != 'M')
					{
						if (c != 'S')
						{
							if (c != 'a')
							{
								goto IL_69D;
							}
							if (!(text == "addmyResByhomework"))
							{
								goto IL_69D;
							}
							if (funcParams != null && funcParams.Length > 4)
							{
								this.MyResLstByHomeWork(funcParams[0], funcParams[1], funcParams[2], funcParams[3], funcParams[4]);
								return;
							}
							return;
						}
						else
						{
							if (!(text == "ShowWebDebugWindow"))
							{
								goto IL_69D;
							}
							this.ShowDebugWindow(funcParams[0]);
							return;
						}
					}
					else
					{
						if (!(text == "MyResourceDownload"))
						{
							goto IL_69D;
						}
						if (funcParams != null && funcParams.Length > 1)
						{
							this.MyResourceDownload(funcParams[0], funcParams[1]);
							return;
						}
						return;
					}
					break;
				}
				case 19:
					if (!(text == "UserResourcePreview"))
					{
						goto IL_69D;
					}
					BaseBookReader.Instance.OpenUserRes(funcParams[0], this);
					return;
				case 20:
				{
					char c = text[0];
					if (c != 'R')
					{
						if (c != 'S')
						{
							goto IL_69D;
						}
						if (!(text == "SetMainWindowTopMost"))
						{
							goto IL_69D;
						}
						this.SetMainWindowTopMost(funcParams[0]);
						return;
					}
					else
					{
						if (!(text == "ReBindingPhoneNumber"))
						{
							goto IL_69D;
						}
						this.BindingPhoneCallBack(funcParams[0]);
						return;
					}
					break;
				}
				case 21:
					if (!(text == "MyResourceDownloadExt"))
					{
						goto IL_69D;
					}
					if (funcParams != null && funcParams.Length > 1)
					{
						this.DownloadResToUserLocal(funcParams[0], funcParams[1]);
						return;
					}
					return;
				case 22:
					if (!(text == "TextbookDownloadAddOne"))
					{
						goto IL_69D;
					}
					this.TextbookDownloadAddOne();
					return;
				case 24:
				{
					char c = text[0];
					if (c != 'G')
					{
						if (c != 'a')
						{
							goto IL_69D;
						}
						if (!(text == "addAppendixResByhomework"))
						{
							goto IL_69D;
						}
						return;
					}
					else
					{
						if (!(text == "GetTextbookDownloadCount"))
						{
							goto IL_69D;
						}
						this.ucSzjc.SetMyBookDownloadCount(funcParams[0]);
						return;
					}
					break;
				}
				default:
					goto IL_69D;
				}
				string[] funcParams2 = funcParams;
				if (funcParams2 != null && funcParams2.Length != 0)
				{
					base.Dispatcher.BeginInvoke(new Action(delegate()
					{
						this.ThirdPartResAddCourse(funcParams[0]);
					}), new object[0]);
					return;
				}
				return;
			}
			IL_69D:
			LogHelper.Instance.Debug(string.Format("消息[{0}]未处理。", msg));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005B88 File Offset: 0x00003D88
		private void BindingPhoneCallBack(string phonenumber)
		{
			CommonParamter.Instance.PhoneNumber = phonenumber;
			new UserDataAccess().UpdateUserPhoneNumber(CommonParamter.Instance.LoginUserId, phonenumber);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void TextbookDownloadAddOne()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005BAC File Offset: 0x00003DAC
		public void ExitLogin()
		{
			try
			{
				NetManager.Instance.SendPipeDataToClient(new PipeMessagePacket
				{
					Command = "exit"
				}, "");
			}
			catch
			{
			}
			this.NotifyMybookLogout();
			this.ucSzjc.ClrarData();
			this.ucResManage.ClearData();
			this.ucMyHome.ClearData();
			this.ucTeachingCourse.ClearData();
			this.CloseOwnerMainWindow();
			this.DisposeHeartBeat();
			this.SetMainUserInfo(string.Empty, "请登录", string.Empty);
			CookieHelper.DeleteCefCookies();
			CookieHelper.RefreshCookies();
			CommonParamter.Instance.IsLogin = false;
			CommonParamter.Instance.LoginUserId = string.Empty;
			CommonParamter.Instance.CurrentUserId = string.Empty;
			CommonParamter.Instance.JcToken = string.Empty;
			CommonParamter.Instance.JcHowmeworkToken = string.Empty;
			RequestUrls.ClearWebDevices();
			DtpUserBookChapter.Instance.ClearData();
			this.SetMainMenue();
			this.mLoginOper.DeleteLoginFile();
			this.mLoginOper.SaveLoginInfo(CommonParamter.Instance.LoginUserName, "******", false);
			this.SetBtnsVisibleByLoginState(false);
			this.GetMessageCount("0");
			WindowMessages.SendMessage2WinByName("GetTextbookDownloadCount??<*>()??0", SdkConstants.JXP_PRODUCT_NAME_TEACHER);
			DownloadResourcesWindow.GetInstance().AbortDownload();
			DownloadResourcesWindow.GetInstance().AbortUpload();
			DownloadResourcesWindow.GetInstance().UpdateHistoryData();
			CefWindow.Instance.Exit();
			IEnumerable<CefEditeWindow> enumerable = System.Windows.Application.Current.Windows.OfType<CefEditeWindow>();
			if (enumerable != null && enumerable.Count<CefEditeWindow>() > 0)
			{
				foreach (CefEditeWindow cefEditeWindow in enumerable)
				{
					if (cefEditeWindow != null)
					{
						cefEditeWindow.Exit();
					}
				}
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200223",
				Passive = "客户端",
				FromPos = MainWindow.mClassPath + ".ExitLogin"
			});
			StatisticsHelper.Instance.ExitLogin();
			this.ShowLogin();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005DB8 File Offset: 0x00003FB8
		private void PcLogin(string menutype)
		{
			this.ShowLogin();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005DC0 File Offset: 0x00003FC0
		private void UserOffLine(bool showMsg = true)
		{
			object obj = this.objUserOfflineLock;
			lock (obj)
			{
				if (CommonParamter.Instance.IsLogin)
				{
					base.Dispatcher.Invoke(new Action(delegate()
					{
						if (this.mTextBookReader.BookIsOpen())
						{
							this.mTextBookReader.CloseBook(true);
						}
						if (CommonParamter.Instance.CourseIsClassing)
						{
							ToolBarWindow.Instance.BreakClass();
						}
						this.ExitLogin();
						if (showMsg)
						{
							CustomMessageBox.Info("您的账号已在其他地方登录,请您重新登录", "确定", "", this, WindowStartupLocation.CenterOwner, false);
						}
					}), new object[0]);
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005E3C File Offset: 0x0000403C
		public void SetMainWindowTopMost(string flag)
		{
			if (flag == "0")
			{
				base.Topmost = false;
				return;
			}
			base.Topmost = true;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005E5C File Offset: 0x0000405C
		private void ResPreview(string strUrl, string strParamter)
		{
			Tuple<string, string, string> result = this.AnalysisParamter(strParamter);
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				StatisticsHelper.Instance.OpenCommonRes(result.Item1, "");
				new BookResHelper(this)
				{
					CommonCloseExMehtod2 = new Action<string, string>(StatisticsHelper.Instance.CloseCommonRes)
				}.OpenOnlineRes(strUrl, strParamter, this.centerGrid, null);
				StatisticsHelper.Instance.CloseCommonRes(result.Item1, "");
			}), new object[0]);
			if (result.Item2 == "02")
			{
				StatisticsHelper.Instance.OpenCenterRes(result.Item1, result.Item3);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005EEC File Offset: 0x000040EC
		private Tuple<string, string, string> AnalysisParamter(string strParamter)
		{
			Tuple<string, string, string> result;
			try
			{
				string[] array = (strParamter != null) ? strParamter.Split(new char[]
				{
					'&'
				}) : null;
				if (array == null || array.Length == 0)
				{
					result = Tuple.Create<string, string, string>(string.Empty, string.Empty, string.Empty);
				}
				else
				{
					string item = string.Empty;
					string item2 = string.Empty;
					string item3 = string.Empty;
					foreach (string text in array)
					{
						string[] array3 = (text != null) ? text.Split(new char[]
						{
							'='
						}) : null;
						if (array3 != null && array3.Length >= 2)
						{
							string text2 = "id";
							string text3 = array3[0];
							if (text2.Equals((text3 != null) ? text3.Trim() : null, StringComparison.InvariantCulture))
							{
								string text4 = array3[1];
								item = ((text4 != null) ? text4.Trim() : null);
							}
							else
							{
								string text5 = "res_location";
								string text6 = array3[0];
								if (text5.Equals((text6 != null) ? text6.Trim() : null, StringComparison.InvariantCulture))
								{
									string text7 = array3[1];
									item2 = ((text7 != null) ? text7.Trim() : null);
								}
								else
								{
									string text8 = "ex_zynrlx";
									string text9 = array3[0];
									if (text8.Equals((text9 != null) ? text9.Trim() : null, StringComparison.InvariantCulture))
									{
										string text10 = array3[1];
										item3 = ((text10 != null) ? text10.Trim() : null);
									}
								}
							}
						}
					}
					result = Tuple.Create<string, string, string>(item, item2, item3);
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "预览资源解析参数失败。参数：";
				string str2 = "。";
				Exception ex2 = ex;
				instance.Error(str + strParamter + str2 + ((ex2 != null) ? ex2.ToString() : null));
				result = Tuple.Create<string, string, string>(string.Empty, string.Empty, string.Empty);
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000608C File Offset: 0x0000428C
		private void SaveLoginInfo(string username, string pwd, bool isAutoLogin)
		{
			this.mLoginOper.SaveLoginInfo(username, pwd, isAutoLogin);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000609C File Offset: 0x0000429C
		public void MyResourceDownload(string resid, string srtType)
		{
			ResDataObjectProvider mDataObjectProvider = new ResDataObjectProvider();
			ThreadEx.Run(delegate()
			{
				UserResourceJsonModel centerResData = mDataObjectProvider.GetCenterResData(resid, srtType, true);
				if (centerResData == null || centerResData.UserResourcesList == null)
				{
					return;
				}
				if (centerResData.UserResourcesList.Count == 1)
				{
					UserResourcesManager.Instance.DownloadUserResFromResCenter(centerResData.UserResourcesList[0], this);
				}
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000060D4 File Offset: 0x000042D4
		public void DownloadResToUserLocal(string resid, string srtType)
		{
			Action <>9__4;
			Action <>9__5;
			Action <>9__7;
			Action <>9__3;
			ThreadEx.Run(delegate()
			{
				try
				{
					string title = string.Empty;
					string format = string.Empty;
					bool flag = false;
					string fileUrl = string.Empty;
					if (srtType == SdkConstants.RJ_CLOUD_RES_TYPE)
					{
						OnlineResModel resInfo = ResHelper.GetResInfo(resid);
						if (resInfo == null)
						{
							Dispatcher dispatcher = this.Dispatcher;
							Action method;
							if ((method = <>9__4) == null)
							{
								method = (<>9__4 = delegate()
								{
									CustomMessageBox.Info("下载资源失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
								});
							}
							dispatcher.Invoke(method, new object[0]);
							return;
						}
						title = UtilityHelper.GetInvalidFileName(resInfo.Title);
						format = resInfo.FileFormat;
						flag = true;
						fileUrl = resInfo.FilePath;
					}
					else
					{
						UserResourceModel cloudUserRes = ResHelper.GetCloudUserRes(resid);
						if (cloudUserRes == null)
						{
							Dispatcher dispatcher2 = this.Dispatcher;
							Action method2;
							if ((method2 = <>9__5) == null)
							{
								method2 = (<>9__5 = delegate()
								{
									CustomMessageBox.Info("下载资源失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
								});
							}
							dispatcher2.Invoke(method2, new object[0]);
							return;
						}
						title = UtilityHelper.GetInvalidFileName(cloudUserRes.Title);
						format = cloudUserRes.FileFormat;
						fileUrl = cloudUserRes.FilePath;
					}
					string filePath = string.Empty;
					this.Dispatcher.Invoke(new Action(delegate()
					{
						SaveFileDialog saveFileDialog = new SaveFileDialog
						{
							Title = title,
							DefaultExt = format.TrimStart(new char[]
							{
								'.'
							}),
							FileName = title + format,
							Filter = "files (*" + format + ")|*" + format
						};
						if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						{
							return;
						}
						filePath = saveFileDialog.FileName;
					}), new object[0]);
					string directoryName = System.IO.Path.GetDirectoryName(filePath);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					string fileName = System.IO.Path.GetFileName(filePath);
					string msg = "资源下载失败!";
					if (flag)
					{
						string dataTempFolder = PepPathHelper.GetDataTempFolder();
						string text = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(filePath);
						string tempFile = System.IO.Path.Combine(dataTempFolder, text);
						if (this.mDownlaod.DownLoadFile(fileUrl, dataTempFolder, true, false, DeviceFlags.CentralResources, text))
						{
							UtilityHelper.DelFile(filePath);
							this.mEncry.FileDecrypt(tempFile, filePath);
							msg = "资源下载完成!";
							this.Dispatcher.Invoke(new Action(delegate()
							{
								ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
								{
									Content = msg,
									IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
								});
							}), new object[0]);
							ThreadEx.Run(delegate()
							{
								try
								{
									UtilityHelper.DelFile(tempFile);
								}
								catch
								{
								}
							});
						}
						else
						{
							Dispatcher dispatcher3 = this.Dispatcher;
							Action method3;
							if ((method3 = <>9__7) == null)
							{
								method3 = (<>9__7 = delegate()
								{
									CustomMessageBox.Info("下载资源失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
								});
							}
							dispatcher3.Invoke(method3, new object[0]);
						}
					}
					else if (this.mDownlaod.DownLoadFile(fileUrl, directoryName, true, false, DeviceFlags.CentralUserResource, fileName))
					{
						msg = "资源下载完成!";
						this.Dispatcher.Invoke(new Action(delegate()
						{
							ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
							{
								Content = msg,
								IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
							});
						}), new object[0]);
					}
					else
					{
						Dispatcher dispatcher4 = this.Dispatcher;
						Action method4;
						if ((method4 = <>9__3) == null)
						{
							method4 = (<>9__3 = delegate()
							{
								CustomMessageBox.Info("下载资源失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
							});
						}
						dispatcher4.Invoke(method4, new object[0]);
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("资源库资源下载失败，资源id：{0}，失败原因:{1}。", resid, arg));
				}
			});
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void Goback()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void CreateNewResByHomeWork(string tbid, string tbName, string chapterId, string chapterName)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006104 File Offset: 0x00004304
		private void NotifyWebResInfo(string resId, string resTitle, string resFormat, string resSize, string resPath)
		{
			try
			{
				string code = string.Concat(new string[]
				{
					"var frame = document.getElementById('framright'); frame.contentWindow._addAppendixResByhomework('",
					resId,
					"','",
					resTitle,
					"','",
					resFormat,
					"','",
					resSize,
					"','",
					resPath,
					"');"
				});
				this.cefBrowser.ExecuteJavaScript(code, "", 0);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "调用js方法_addAppendixResByhomework失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000061B0 File Offset: 0x000043B0
		private void MyResLstByHomeWork(string tbid, string tbName, string chapterId, string chapterName, string resids)
		{
			try
			{
				List<string> lstInitAddResid = new List<string>();
				if (!string.IsNullOrEmpty(resids))
				{
					lstInitAddResid = resids.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
				}
				new MyResSelectByWorkWindow(new SelectMyResParamter
				{
					BookId = tbid,
					BookName = tbName,
					ChapterId = chapterId,
					ChapterName = chapterName,
					LstInitAddResid = lstInitAddResid
				})
				{
					NotifyWebHomeWorkAddResCallBack = new NotifyWebHomeWorkAddRes(this.NotifyWebHomeWorkAddedRes),
					Owner = this
				}.ShowDialog();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "作业模块调用我的资源列表失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006268 File Offset: 0x00004468
		private void NotifyWebHomeWorkAddedRes(string jsonData)
		{
			try
			{
				string code = "var frame = document.getElementById('framright'); frame.contentWindow._addResourcefromPC('" + jsonData + "');";
				this.cefBrowser.ExecuteJavaScript(code, "", 0);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "调用Js方法_addResourcefromPC失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000062D0 File Offset: 0x000044D0
		private void OpenActivityVideo()
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				try
				{
					new VideoIntroduceWindow
					{
						Owner = this,
						OpenActivityVideoCallBack = new Action(this.OpenActivityVideo)
					}.ShowDialog();
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("打开活动上传视频失败:[{0}]。", arg));
				}
			}), new object[0]);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000062F0 File Offset: 0x000044F0
		private void ThirdPartResAddCourse(string data)
		{
			try
			{
				ThirdParttResDataModel thirdParttResDataModel = JsonConvert.DeserializeObject<ThirdParttResDataModel>(data);
				if (string.IsNullOrEmpty((thirdParttResDataModel != null) ? thirdParttResDataModel.Id : null) || string.IsNullOrEmpty((thirdParttResDataModel != null) ? thirdParttResDataModel.Title : null))
				{
					LogHelper.Instance.Error("数字作业调用addHomework时参数:[" + data + "]为空。");
				}
				else
				{
					CourseResModel courseRes = new CourseResModel
					{
						Id = thirdParttResDataModel.Id,
						Title = thirdParttResDataModel.Title,
						Remarks = thirdParttResDataModel.Title,
						ResLyId = Consts.HomeworkRes,
						ResLYTitle = Consts.WorkTitle,
						FromType = thirdParttResDataModel.FromType,
						ResLxType = 2.ToString()
					};
					ResAddCourseWindow resAddCourseWindow = new ResAddCourseWindow();
					resAddCourseWindow.DataStatistic = StatisticsHelper.Instance;
					resAddCourseWindow.CourseRes = courseRes;
					if (!string.IsNullOrEmpty(thirdParttResDataModel.TbId) && !string.IsNullOrEmpty(thirdParttResDataModel.ChapterId) && !string.IsNullOrEmpty(thirdParttResDataModel.ChapterName))
					{
						resAddCourseWindow.Tbid = thirdParttResDataModel.TbId;
						resAddCourseWindow.TbName = TextBookInfoDataHelper.Instance.GetTextBookName(thirdParttResDataModel.TbId);
						resAddCourseWindow.LstChapterIds = UtilityHelper.GetSubChapterLst(thirdParttResDataModel.ChapterId, thirdParttResDataModel.TbId);
						resAddCourseWindow.ChapterName = thirdParttResDataModel.ChapterName;
					}
					else
					{
						resAddCourseWindow.CheckParamter = false;
					}
					MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
					resAddCourseWindow.Owner = MaskLayerWindow.SingleInstance();
					resAddCourseWindow.ShowDialog();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("第三方资源添加课程失败:[{0}]", arg));
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006488 File Offset: 0x00004688
		public void ShowDebugWindow(string flag = "")
		{
			if (!(flag == "1"))
			{
				CefBrowserHost host = this.cefBrowser.Browser.GetHost();
				CefWindowInfo cefWindowInfo = CefWindowInfo.Create();
				cefWindowInfo.SetAsPopup(IntPtr.Zero, "DevTools");
				host.ShowDevTools(cefWindowInfo, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600008E RID: 142 RVA: 0x000064E0 File Offset: 0x000046E0
		// (remove) Token: 0x0600008F RID: 143 RVA: 0x00006518 File Offset: 0x00004718
		public event JXP.Utilities.EventHandler<object> OnPageIndexChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000654D File Offset: 0x0000474D
		public static CefWebBrowser MainBrowser
		{
			get
			{
				return MainWindow.mMainBrowser;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006554 File Offset: 0x00004754
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000655C File Offset: 0x0000475C
		public bool IsOnline { get; set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00006568 File Offset: 0x00004768
		public MainWindow()
		{
			this.InitializeComponent();
			System.Windows.Application.Current.MainWindow = this;
			this.InitMainBrowser();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006670 File Offset: 0x00004870
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Windows.Application.Current.MainWindow = this;
				this.Init();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("程序初期化失败。" + ex.ToString());
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000066C0 File Offset: 0x000048C0
		private void Init()
		{
			WndProcHelper.Instance.OnMessageReceived -= this.Instance_OnMessageReceived;
			WndProcHelper.Instance.OnMessageReceived += this.Instance_OnMessageReceived;
			this.InitReader();
			this.RegisterControlEvent();
			this.namePopup.PlacementTarget = this.ucMainMenu.elpsLogo;
			LogHelper.DeleteExpiredLogFilesAsync();
			this.InitNotifyIcon();
			this.InitLogin();
			CefWindow.Instance.UploadDataCallback = new UploadData(StatisticsHelper.Instance.UploadCreateResData);
			this.InitCourseCallback();
			this.InitResManagerData();
			this.InitMyHomeCallback();
			this.InitApplyToolCallback();
			this.mLoginOper.SetUserPhotoCallBack = new SetUserPhoto(this.SetMainUserInfo);
			TaskEx.Run(delegate()
			{
				PepClassService.StartPepServer();
			});
			CertificateHelper.InstallGlobaSignAsync();
			if (CommonParamter.Instance.CourseIsOnline)
			{
				if (!this.IsOnline)
				{
					new MyNews
					{
						Owner = this,
						WindowStartupLocation = WindowStartupLocation.CenterOwner
					}.ShowDialog();
					new ExitTip().ExitFunc();
				}
				else
				{
					new ConstDatasHelper().UpdateConstsAsync();
					TextBookInfoDataHelper.Instance.CacheTextBookInfo(null, 20);
				}
				this.Unsubscribe();
				this.Subscribe(Gma.System.MouseKeyHook.Hook.GlobalEvents());
				return;
			}
			this.mTextBookReader.SetBtnEnable();
			ImportWindow importWindow = new ImportWindow();
			importWindow.Topmost = true;
			importWindow.StartPipeNotifyServerCallback = new Action(DaemonServiceHelper.StartPipeNotifyServerAsync);
			base.Hide();
			importWindow.Show();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006838 File Offset: 0x00004A38
		private void InitReader()
		{
			if (BaseBookReader.Instance == null || !(BaseBookReader.Instance is TextBookReader))
			{
				BaseBookReader.Instance = new TextBookReader();
			}
			this.mTextBookReader = (BaseBookReader.Instance as TextBookReader);
			this.mTextBookReader.SortMyTextBookCallBack = new SortMyTextBook(this.ucSzjc.ucMyBook.SortTBList);
			this.mTextBookReader.UpdateMyTextBookOpenTimeCallBack = new UpdateMyTextBookOpenTime(this.ucSzjc.ucMyBook.UpdateTbListOpentime);
			this.mTextBookReader.NavigateToMenueCallBack = new TextBookReader.NavigateToMenue(this.ClientNavigateTo);
			this.mTextBookReader.ExitReaderSyncDataCallBack = new TextBookReader.ExitReaderSyncData(this.ExitReaderSysData);
			this.mTextBookReader.SaveCallBack = new Action<ClassActivityInfoModel, UserResourceModel, Window>(this.SaveClassActivityData);
			if (CommonParamter.Instance.CourseIsOnline)
			{
				this.mTextBookReader.SetOwnerVisibilityCallBack = new SetOwnerVisibility(this.SetMainWinVisibility);
				this.mTextBookReader.InitNotDownloadResPop(this.mTextBookReader.ResNotDownloadPop);
			}
			this.mTextBookReader.OnMinimizeClick += this.MTextBookReader_OnMinimizeClick;
			TextBookReader textBookReader = this.mTextBookReader;
			textBookReader.StartClassCallback = (JXP.PepDtp.View.TextBookReader.StartClass)Delegate.Combine(textBookReader.StartClassCallback, new JXP.PepDtp.View.TextBookReader.StartClass(this.StartPepClass));
			this.mTextBookReader.DataStatistic = StatisticsHelper.Instance;
			this.mTextBookReader.mTextBookReaderVM.UploadDataCallback = new UploadData(StatisticsHelper.Instance.UploadCreateResData);
			TextBookReader textBookReader2 = this.mTextBookReader;
			textBookReader2.CreateGroupDataCallBack = (CreateGroupData)Delegate.Combine(textBookReader2.CreateGroupDataCallBack, new CreateGroupData(this.CreateGroupDataWin));
			this.mTextBookReader.OpenAppCenterWindowCallBack = new OpenAppCenterWindow(this.OpenAppCenterWin);
			this.mTextBookReader.OpenSubjectToolCallBack = new Action<string, AppCenterModel, bool, Window, string, bool>(this.ucAppCenter.OpenSubjectTool);
			this.mTextBookReader.OpenNbResCallBack = new Action<string>(this.OpenNBRes);
			this.mTextBookReader.TextBookPageChangeCallBack = new Action<string, int, int>(this.TextbookPageChange);
			this.mTextBookReader.NotifyOpenBookReusltCallBack = new NotifyOpenBookReuslt(this.OpenBookReuslt);
			this.mTextBookReader.ClsoeBookCallBack = new Action<string, int>(this.CloseBook);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006A58 File Offset: 0x00004C58
		private void RegisterControlEvent()
		{
			this.ucMainMenu.MenuMouseLeftButtonDownEvent += this.UcMainMenu_MenuMouseLeftButtonDownEvent;
			this.ucMainMenu.btnMessage.Click += this.BtnMessage_Click;
			this.ucMainMenu.elpsLogo.MouseLeftButtonUp += this.ElpsLogo_MouseLeftButtonUp;
			this.ucMainMenu.lblName.MouseLeftButtonUp += this.ElpsLogo_MouseLeftButtonUp;
			this.ucMainMenu.lblSchool.MouseLeftButtonUp += this.ElpsLogo_MouseLeftButtonUp;
			this.ucTeacherTitle.MouseDown += this.ucTeacherTitle_MouseDown;
			this.ucTeacherTitle.MouseDoubleClick += this.ucTeacherTitle_MouseDoubleClick;
			this.ucTeacherTitle.btnSetting.Click += this.imgBtnSetting_Click;
			this.ucTeacherTitle.btnClose.Click += this.BtnClose_Click;
			this.ucTeacherTitle.btnMaximized.Click += this.imgBtnMaximized_Click;
			this.ucTeacherTitle.btnMinimized.Click += this.imgBtnMinimized_Click;
			this.ucTeacherTitle.lblCustomer.MouseLeftButtonDown += this.LblCustomer_MouseLeftButtonDown;
			base.StateChanged += this.MainWindow_StateChanged;
			base.LocationChanged += this.MainWindow_LocationChanged;
			base.SizeChanged += this.MainWindow_SizeChanged;
			base.Closing += this.MainWindow_Closing;
			this.ucResManage.IsOnline = this.IsOnline;
			MessageBus.Default.Register<ToastInfo>(this, new Action<ToastInfo>(this.ReceiveToastInfo), false);
			MessageBus.Default.Register<ToastInfo>(this, SdkConstants.MESSAGEBUS_TOKEN_BOOKCENTER, new Action<ToastInfo>(this.ReceiveToastInfo), false);
			MessageBus.Default.Register<int>(this, SdkConstants.MESSAGEBUS_TOKEN_APPLY_CENTER, new Action<int>(this.SetApplyUpdateCount), false);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006C54 File Offset: 0x00004E54
		private void InitNotifyIcon()
		{
			this.notifyIcon = new NotifyIcon();
			this.notifyIcon.BalloonTipText = "八桂教学通";
			this.notifyIcon.Text = "八桂教学通";
			this.notifyIcon.Icon = new Icon(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Images/logo.ico"));
			this.notifyIcon.Visible = false;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem.Text = "打开";
			toolStripMenuItem.Click += this.toolStripMenuItemOpen_Click;
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
			toolStripMenuItem2.Text = "退出";
			toolStripMenuItem2.Click += this.toolStripMenuItemClose_Click;
			contextMenuStrip.Items.AddRange(new ToolStripItem[]
			{
				toolStripMenuItem,
				toolStripMenuItem2
			});
			this.notifyIcon.ContextMenuStrip = contextMenuStrip;
			this.notifyIcon.MouseDoubleClick += this.NotifyIcon_MouseDoubleClick;
			this.notifyIcon.ShowBalloonTip(500);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006D54 File Offset: 0x00004F54
		private void UcMainMenu_MenuMouseLeftButtonDownEvent(object sender, RoutedEventArgs e)
		{
			ListBoxItem listBoxItem = sender as ListBoxItem;
			if (listBoxItem == null)
			{
				return;
			}
			MenuItemModel menuItemModel = listBoxItem.DataContext as MenuItemModel;
			if (menuItemModel == null)
			{
				return;
			}
			this.MenuNavigateTo(menuItemModel.PageIndex);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006D88 File Offset: 0x00004F88
		private void ucTeacherTitle_MouseDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (!(e.OriginalSource is Ellipse))
				{
					if (e.LeftButton == MouseButtonState.Pressed)
					{
						base.DragMove();
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("ucTeacherTitle_MouseDown异常。" + ex.ToString());
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private void ucTeacherTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (base.WindowState == WindowState.Maximized)
				{
					base.WindowState = WindowState.Normal;
					base.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				}
				else
				{
					base.WindowState = WindowState.Maximized;
				}
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx200112",
					Passive = "客户端",
					FromPos = MainWindow.mClassPath + ".ucTeacherTitle_MouseDoubleClick"
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("ucTeacherTitle_MouseDoubleClick异常。" + ex.ToString());
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006E7C File Offset: 0x0000507C
		private void ElpsLogo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (this.IsOnline)
				{
					if (!CommonParamter.Instance.IsLogin)
					{
						this.ucLogin.mainmenuenus = MainMenuEnums.MyHomePage;
						this.ShowLogin();
					}
					else
					{
						this.namePopup.IsOpen = !this.namePopup.IsOpen;
					}
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200104",
						Passive = "客户端",
						FromPos = MainWindow.mClassPath + ".ElpsLogo_MouseLeftButtonUp"
					});
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("点击用户头像处理失败：[{0}]。", arg));
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006F34 File Offset: 0x00005134
		private void btnPersonal_Click(object sender, RoutedEventArgs e)
		{
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200222",
				Passive = "客户端",
				FromPos = MainWindow.mClassPath + ".btnPersonal_Click"
			});
			this.namePopup.IsOpen = false;
			PersonalCenterWindow personalCenterWindow = new PersonalCenterWindow(MainMenuEnums.PersonalInfo);
			personalCenterWindow.mLoginOper = this.mLoginOper;
			MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
			personalCenterWindow.Owner = MaskLayerWindow.SingleInstance();
			personalCenterWindow.ShowDialog();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006FB6 File Offset: 0x000051B6
		private void btnGroupInfo_Click(object sender, RoutedEventArgs e)
		{
			GroupInfoWindow groupInfoWindow = new GroupInfoWindow();
			MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
			groupInfoWindow.Owner = MaskLayerWindow.SingleInstance();
			groupInfoWindow.ShowDialog();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006FDC File Offset: 0x000051DC
		private void btnSwitchAccount_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (CommonParamter.Instance.IsLogin)
				{
					this.namePopup.IsOpen = false;
					if (CustomMessageBox.Question("确认要切换登录账号？", "确定", "取消", this, WindowStartupLocation.CenterOwner, true, MessageIconType.WarnRound, ""))
					{
						this.ExitLogin();
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("退出登录出错：" + ex.ToString());
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000705C File Offset: 0x0000525C
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			this.namePopup.IsOpen = false;
			this.imgBtnClose_Click(sender, e);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007074 File Offset: 0x00005274
		private void imgBtnSetting_Click(object sender, RoutedEventArgs e)
		{
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200108",
				Passive = "客户端",
				FromPos = MainWindow.mClassPath + ".imgBtnSetting_Click"
			});
			SystemConfigWindow systemConfigWindow = new SystemConfigWindow();
			MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
			systemConfigWindow.Owner = MaskLayerWindow.SingleInstance();
			systemConfigWindow.ShowDialog();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000070DC File Offset: 0x000052DC
		private void BtnMessage_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MessageCenterWindow messageCenterWindow = new MessageCenterWindow();
				MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
				messageCenterWindow.Owner = MaskLayerWindow.SingleInstance();
				messageCenterWindow.ShowDialog();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "点击消息按钮报错。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007140 File Offset: 0x00005340
		private void imgBtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ExitTip exitTip = new ExitTip();
				MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
				exitTip.Owner = MaskLayerWindow.SingleInstance();
				exitTip.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				exitTip.ExitHandleCallBack = new ExitHandle(this.ExitWindow);
				exitTip.IsOnline = this.IsOnline;
				exitTip.Topmost = true;
				exitTip.Show();
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx200111",
					Passive = "客户端",
					FromPos = MainWindow.mClassPath + ".imgBtnClose_Click"
				});
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("退出客户端失败：[{0}]。", arg));
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007200 File Offset: 0x00005400
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			if (this.ucMainMenu.IsSelected(MainMenuEnums.TeachingCourse) && this.ucTeachingCourse.ucMyCourse.IsEditeCourse())
			{
				this.ucTeachingCourse.ucMyCourse.EditeDataGoBack();
				return;
			}
			string keyValue = ConfigHelper.GetKeyValue("CloseToPallet");
			bool flag = false;
			bool.TryParse(keyValue, out flag);
			if (flag)
			{
				base.Hide();
				base.ShowInTaskbar = false;
				this.notifyIcon.Visible = true;
				return;
			}
			this.imgBtnClose_Click(sender, e);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007278 File Offset: 0x00005478
		private void imgBtnMaximized_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (base.WindowState == WindowState.Maximized)
				{
					base.WindowState = WindowState.Normal;
					base.Width = 1280.0;
					base.Height = 720.0;
					double num = this.mScreenWidth / 2.0 - 640.0;
					if (num < 0.0)
					{
						num = 0.0;
					}
					double num2 = this.mScreenHeight / 2.0 - 360.0;
					if (num2 < 0.0)
					{
						num2 = 0.0;
					}
					base.Left = num;
					base.Top = num2;
				}
				else
				{
					base.WindowState = WindowState.Maximized;
				}
				TrackerManager.Tracker.OnEvent(new EventActivity
				{
					ActionId = "jx200110",
					Passive = "客户端",
					FromPos = MainWindow.mClassPath + ".imgBtnMaximized_Click"
				});
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("设置主窗体在最大化、最小化失败:[{0}]。", arg));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007394 File Offset: 0x00005594
		private void MTextBookReader_OnMinimizeClick(object sender, EventArgs e)
		{
			this.imgBtnMinimized_Click(sender, null);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000073A0 File Offset: 0x000055A0
		private void imgBtnMinimized_Click(object sender, RoutedEventArgs e)
		{
			if (this.mTextBookReader == null || !this.mTextBookReader.BookIsOpen())
			{
				base.WindowState = WindowState.Minimized;
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200110",
				Passive = "客户端",
				FromPos = MainWindow.mClassPath + ".imgBtnMinimized_Click"
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007403 File Offset: 0x00005603
		private void LblCustomer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CustomerWindow customerWindow = new CustomerWindow();
			MaskLayerWindow.SingleInstance().ShowMaskWindow(this);
			customerWindow.Owner = MaskLayerWindow.SingleInstance();
			customerWindow.ShowDialog();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007426 File Offset: 0x00005626
		private void MainWindow_Closing(object sender, CancelEventArgs e)
		{
			this.imgBtnClose_Click(null, null);
			e.Cancel = true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007438 File Offset: 0x00005638
		private Task<bool> ExitWindow(bool bCloseInnerWin)
		{
			MainWindow.<ExitWindow>d__137 <ExitWindow>d__;
			<ExitWindow>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<ExitWindow>d__.<>4__this = this;
			<ExitWindow>d__.<>1__state = -1;
			<ExitWindow>d__.<>t__builder.Start<MainWindow.<ExitWindow>d__137>(ref <ExitWindow>d__);
			return <ExitWindow>d__.<>t__builder.Task;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000747B File Offset: 0x0000567B
		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (base.WindowState == WindowState.Maximized)
			{
				this.ucTeacherTitle.NoticeMsgOper.MaximizedNormalImage = "";
				return;
			}
			this.ucTeacherTitle.NoticeMsgOper.MaximizedNormalImage = "";
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000074B4 File Offset: 0x000056B4
		private void MainWindow_LocationChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cefBrowser != null && this.cefBrowser.Created && this.cefBrowser.Enabled && this.cefBrowser.Browser != null)
				{
					this.cefBrowser.Browser.GetHost().NotifyMoveOrResizeStarted();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007528 File Offset: 0x00005728
		private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.MainWindow_LocationChanged(sender, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007534 File Offset: 0x00005734
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				IntPtr handle = new WindowInteropHelper(window).Handle;
				HwndSource hwndSource = HwndSource.FromHwnd(handle);
				if (hwndSource != null)
				{
					hwndSource.AddHook(new HwndSourceHook(this.WindowProc));
					MessageTouchDevice.RegisterTouchWindow(handle);
					hwndSource.AddHook(new HwndSourceHook(this.Hook));
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000758C File Offset: 0x0000578C
		private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				MessageTouchDevice.WndProc(window, msg, wParam, lParam, ref handled);
			}
			return IntPtr.Zero;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000075B4 File Offset: 0x000057B4
		private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			base.ShowInTaskbar = true;
			base.WindowState = WindowState.Maximized;
			this.notifyIcon.Visible = false;
			base.Visibility = Visibility.Visible;
			base.Activate();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000075DE File Offset: 0x000057DE
		private void toolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			this.imgBtnClose_Click(null, null);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000075E8 File Offset: 0x000057E8
		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			this.NotifyIcon_MouseDoubleClick(null, null);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000075F2 File Offset: 0x000057F2
		private void CreateGroupDataWin(Window owner)
		{
			new GroupInfoWindow
			{
				Owner = ((owner == null) ? this : owner),
				CloseMaskWin = false
			}.ShowDialog();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007614 File Offset: 0x00005814
		private void ShowTutorials()
		{
			if (MainWindow.mPopInfoDA.GetPopInfo("2100", CommonParamter.Instance.LoginUserId) != null)
			{
				return;
			}
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			new TutorialWindow
			{
				Width = primaryScreenWidth * 0.57,
				Height = primaryScreenHeight * 0.68,
				Owner = this
			}.ShowDialog();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007680 File Offset: 0x00005880
		private void OpenAppCenterWin(Window owner)
		{
			AppCentertWindow appCentertWindow = new AppCentertWindow();
			appCentertWindow.Owner = owner;
			appCentertWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			this.ucAppCenter.SetTitleVisible(false);
			this.RemoveAppCenterContent();
			this.ucAppCenter.OwnerWin = owner;
			appCentertWindow.AddContent(this.ucAppCenter);
			this.ucAppCenter.InitData();
			appCentertWindow.ShowDialog();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000076DC File Offset: 0x000058DC
		private void ShowMessageInfo()
		{
			try
			{
				List<MessageInfoModel> messageInfo = this.GetMessageInfo();
				if (messageInfo != null)
				{
					MessageInfoWindow msgWin = new MessageInfoWindow();
					messageInfo.ForEach(delegate(MessageInfoModel a)
					{
						msgWin.LstMessageInfo.Add(a);
					});
					msgWin.Owner = this;
					msgWin.ShowDialog();
					this.GetMsgCountFromServer();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("获取未读消息列表失败:[{0}]。", arg));
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007760 File Offset: 0x00005960
		private List<MessageInfoModel> GetMessageInfo()
		{
			string value = HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "p/notice/list", new int?(5000), true, "");
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			MessageInfoResult messageInfoResult = JsonConvert.DeserializeObject<MessageInfoResult>(value);
			if (messageInfoResult != null)
			{
				List<MessageInfoModel> lstMessageInfo = messageInfoResult.LstMessageInfo;
				int? num = (lstMessageInfo != null) ? new int?(lstMessageInfo.Count) : null;
				int num2 = 0;
				if (num.GetValueOrDefault() > num2 & num != null)
				{
					return messageInfoResult.LstMessageInfo;
				}
			}
			return null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000077E8 File Offset: 0x000059E8
		private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == 274 && wParam.ToInt32() == 61696)
			{
				handled = true;
				return IntPtr.Zero;
			}
			if (msg == 36)
			{
				AdjustWorkingArea.Adjust(hwnd, lParam, 814, 614);
				handled = true;
			}
			return (IntPtr)0;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007836 File Offset: 0x00005A36
		private void RefreshBrowser()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					if (this.cefBrowser != null && this.cefBrowser.Browser != null)
					{
						this.cefBrowser.Browser.GetHost().SetFocus(true);
						this.cefBrowser.Browser.GetHost().Invalidate(CefPaintElementType.View);
					}
				}
				catch (Exception ex)
				{
					LogHelper.Instance.Error(string.Format("浏览器重新加载失败！[{0}]", ex.Message));
				}
			}), new object[0]);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007858 File Offset: 0x00005A58
		private void ExitReaderSysData(string bookid)
		{
			bool flag = false;
			bool.TryParse(ConfigHelper.GetKeyValue(ConfigHelper.RemindSyncResKeyName), out flag);
			if (CommonParamter.Instance.IsSeeMyBook && CommonParamter.Instance.GetInternetState && flag)
			{
				Tuple<bool, List<UserResourceModel>, List<UserResourceModel>, List<UserResourceModel>, FDFInfoTransferModel, BookmarkTransferModel> syncResult = this.mTextBookReader.GettSyncRes(bookid);
				if (syncResult != null && syncResult.Item1)
				{
					base.Dispatcher.Invoke(new Action(delegate()
					{
						SyncMessageBox syncMessageBox = new SyncMessageBox(syncResult);
						syncMessageBox.SyncUserResCallBack = new SyncUserRes(this.mTextBookReader.SyncUserData);
						if (this != null && this.Visibility == Visibility.Visible)
						{
							syncMessageBox.Owner = this;
							syncMessageBox.ShowDialog();
						}
					}), new object[0]);
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000078E8 File Offset: 0x00005AE8
		private void StartPepClass(string bookId, string chapterId, string chapterName)
		{
			if (CommonParamter.Instance.IsInClass)
			{
				MessageBus.Default.Send<ToastInfo>(new ToastInfo
				{
					Content = "已经上课中",
					IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
				});
				return;
			}
			if (!CommonParamter.Instance.IsTeacher)
			{
				CustomMessageBox.Info("抱歉，只有教师才能创建课堂", "确定", "", this, WindowStartupLocation.CenterOwner, false);
				return;
			}
			if (chapterId == null)
			{
				throw new ArgumentNullException("chapterId");
			}
			if (chapterName == null)
			{
				throw new ArgumentNullException("chapterName");
			}
			if (this.mSelectGroupWin != null)
			{
				this.mSelectGroupWin.Activate();
				return;
			}
			this.InitialPepClassParameter(bookId, chapterId, chapterName);
			this.mSelectGroupWin = new SelectGroupWin();
			this.mSelectGroupWin.Owner = this;
			this.mSelectGroupWin.OnStartPepClass += this.SelectGroupWin_OnStartPepClass;
			this.mSelectGroupWin.Closed += this.SelectGroupWin_Closed;
			this.mSelectGroupWin.Show();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000079D8 File Offset: 0x00005BD8
		public void InitialPepClassParameter(string bookId, string chapterId, string chapterName)
		{
			PepClassParameter.SharedData.BookId = bookId;
			PepClassParameter.SharedData.ChapterId = chapterId;
			PepClassParameter.SharedData.ChapterName = chapterName;
			PepClassParameter.SharedData.UserId = CommonParamter.Instance.LoginUserId;
			PepClassParameter.SharedData.UserShowName = CommonParamter.Instance.UserShowName;
			PepClassParameter.SharedData.LoginUserName = CommonParamter.Instance.LoginUserName;
			PepClassParameter.SharedData.CurrentUserId = CommonParamter.Instance.CurrentUserId;
			PepClassParameter.SharedData.Password = CommonParamter.Instance.LoginPsw;
			PepClassParameter.SharedData.OrgsPath = CommonParamter.Instance.OrgsPath;
			PepClassParameter.SharedData.DbPath = AppConsts.DB_NAME;
			PepClassParameter.SharedData.DbPwd = AppConsts.DATABASE_PASSWORD;
			PepClassParameter.SharedData.ServerUrl = ConfigHelper.WebServerUrl;
			PepClassParameter.SharedData.IsServiceAvailable = CommonParamter.Instance.GetInternetState;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007ABF File Offset: 0x00005CBF
		private void SelectGroupWin_Closed(object sender, EventArgs e)
		{
			this.mSelectGroupWin.OnStartPepClass -= this.SelectGroupWin_OnStartPepClass;
			this.mSelectGroupWin.Closed -= this.SelectGroupWin_Closed;
			this.mSelectGroupWin = null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007AF6 File Offset: 0x00005CF6
		private void SelectGroupWin_OnStartPepClass(object sender, PepClassEventArgs e)
		{
			this.InitialPepClassGroupInfo(e.GroupInfo, e.GroupUserInfoList, e.IsSharedScreen, "");
			this.StartPepClassExe();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007B1C File Offset: 0x00005D1C
		public void InitialPepClassGroupInfo(GroupInfoModel groupInfo, List<GroupUserInfoModel> groupUserInfoList, bool isSharedScreen, string courseJson = "")
		{
			PepClassParameter.SharedData.GroupInfo = groupInfo;
			PepClassParameter.SharedData.GroupUserInfoList = groupUserInfoList;
			PepClassParameter.SharedData.IsSharedScreen = isSharedScreen;
			PepClassParameter.SharedData.CoursewareJson = courseJson;
			PepClassParameter.SharedData.OnlineAccessToken = PepService.OnlinePlayerAccessToken().AccessToken;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007B6A File Offset: 0x00005D6A
		public void StartPepClassExe()
		{
			this.StartPipeServer();
			CefWindow.Instance.Close();
			TaskEx.Run(delegate()
			{
				ProcessHelper.Start(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConsts.PEPCLASS_EXE), "", "", "");
			});
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007BA4 File Offset: 0x00005DA4
		private void StartPepClass()
		{
			if (CommonParamter.Instance.IsInClass)
			{
				MessageBus.Default.Send<ToastInfo>(new ToastInfo
				{
					Content = "已经上课中",
					IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
				});
				return;
			}
			if (!CommonParamter.Instance.IsTeacher)
			{
				CustomMessageBox.Info("抱歉，只有教师才能创建课堂", "确定", "", this, WindowStartupLocation.CenterOwner, false);
				return;
			}
			if (this.mSelectGroup7ChapterWin != null)
			{
				this.mSelectGroup7ChapterWin.Activate();
				return;
			}
			PepClassParameter.SharedData.UserId = CommonParamter.Instance.LoginUserId;
			PepClassParameter.SharedData.UserShowName = CommonParamter.Instance.UserShowName;
			PepClassParameter.SharedData.LoginUserName = CommonParamter.Instance.LoginUserName;
			PepClassParameter.SharedData.CurrentUserId = CommonParamter.Instance.CurrentUserId;
			PepClassParameter.SharedData.Password = CommonParamter.Instance.LoginPsw;
			PepClassParameter.SharedData.OrgsPath = CommonParamter.Instance.OrgsPath;
			PepClassParameter.SharedData.DbPath = AppConsts.DB_NAME;
			PepClassParameter.SharedData.DbPwd = AppConsts.DATABASE_PASSWORD;
			PepClassParameter.SharedData.ServerUrl = ConfigHelper.WebServerUrl;
			PepClassParameter.SharedData.IsServiceAvailable = CommonParamter.Instance.GetInternetState;
			this.mSelectGroup7ChapterWin = new SelectGroup7ChapterWin();
			this.mSelectGroup7ChapterWin.Owner = this;
			this.mSelectGroup7ChapterWin.OnStartPepClass += this.SelectGroup7ChapterWin_OnStartPepClass;
			this.mSelectGroup7ChapterWin.Closed += this.SelectGroup7ChapterWin_Closed;
			this.mSelectGroup7ChapterWin.Show();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007D27 File Offset: 0x00005F27
		private void SelectGroup7ChapterWin_Closed(object sender, EventArgs e)
		{
			this.mSelectGroup7ChapterWin.OnStartPepClass -= this.SelectGroupWin_OnStartPepClass;
			this.mSelectGroup7ChapterWin.Closed -= this.SelectGroupWin_Closed;
			this.mSelectGroup7ChapterWin = null;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007D60 File Offset: 0x00005F60
		private void SelectGroup7ChapterWin_OnStartPepClass(object sender, PepClassEventArgs e)
		{
			PepClassParameter.SharedData.GroupInfo = e.GroupInfo;
			PepClassParameter.SharedData.GroupUserInfoList = e.GroupUserInfoList;
			PepClassParameter.SharedData.IsSharedScreen = e.IsSharedScreen;
			PepClassParameter.SharedData.ChapterId = e.ChapterId;
			PepClassParameter.SharedData.ChapterName = e.ChapterName;
			PepClassParameter.SharedData.BookId = e.BookId;
			PepClassParameter.SharedData.OnlineAccessToken = PepService.OnlinePlayerAccessToken().AccessToken;
			this.StartPipeServer();
			CefWindow.Instance.Close();
			TaskEx.Run(delegate()
			{
				ProcessHelper.Start(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConsts.PEPCLASS_EXE), "", "", "");
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007E18 File Offset: 0x00006018
		private void StartPipeServer()
		{
			NetManager.Instance.PipeClientConnected -= this.PipeClientConnected;
			NetManager.Instance.PipeClientDataReceived -= this.PipeClientDataReceived;
			NetManager.Instance.PipeClientDisconnected -= this.PipeClientDisconnected;
			NetManager.Instance.PipeClientConnected += this.PipeClientConnected;
			NetManager.Instance.PipeClientDataReceived += this.PipeClientDataReceived;
			NetManager.Instance.PipeClientDisconnected += this.PipeClientDisconnected;
			NetManager.Instance.StartPipeServer("");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007EB9 File Offset: 0x000060B9
		private void PipeClientDisconnected(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> e)
		{
			TextBookReader textBookReader = this.mTextBookReader;
			if (textBookReader != null)
			{
				textBookReader.ReaderEndClass();
			}
			LogHelper.Instance.Error(string.Format("Pipe客户端断开连接了。{0}-{1}", e.Id, e.Name));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007EF4 File Offset: 0x000060F4
		private void PipeClientConnected(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> e)
		{
			try
			{
				LogHelper.Instance.Error(string.Format("Pipe客户端连接了。{0}-{1}", e.Id, e.Name));
				e.PushMessage(new PipeMessagePacket
				{
					Command = "ping"
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007F5C File Offset: 0x0000615C
		private void PipeClientDataReceived(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> sender, PipeMessagePacket e)
		{
			try
			{
				if (e.ModuleId == "PepClass")
				{
					this.HandlePepClassMessage(sender, e.Command, e.JsonData);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007FB0 File Offset: 0x000061B0
		private void HandlePepClassMessage(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> sender, string command, string jsonData)
		{
			if (string.IsNullOrWhiteSpace(command))
			{
				return;
			}
			command = command.ToLowerInvariant();
			if (command == "start")
			{
				string plainString = JsonConvert.SerializeObject(new PepClassParameter());
				string jsonData2 = new EncryptorHelper().StringEncrypt(plainString);
				sender.PushMessage(new PipeMessagePacket
				{
					Command = "data",
					JsonData = jsonData2
				});
				return;
			}
			if (!(command == "success"))
			{
				if (command == "stop")
				{
					TextBookReader textBookReader = this.mTextBookReader;
					if (textBookReader != null)
					{
						textBookReader.ReaderEndClass();
					}
					CommonParamter.Instance.IsInClass = false;
					return;
				}
				if (command == "fail")
				{
					LogHelper.Instance.Error("互动课堂启动失败。");
					return;
				}
				if (command == "uploadoss")
				{
					PepClassOssUploadModel pepClassOssUploadModel = JsonConvert.DeserializeObject<PepClassOssUploadModel>(jsonData);
					TransferFileModel transferFileModel = new TransferFileModel();
					transferFileModel.DeviceFlag = pepClassOssUploadModel.DeviceFlag;
					transferFileModel.SrcFullName = pepClassOssUploadModel.SrcFullName;
					transferFileModel.FileName = pepClassOssUploadModel.FileName;
					transferFileModel.UploadSubjoinPath = pepClassOssUploadModel.UploadSubjoinPath;
					transferFileModel.IsOverride = pepClassOssUploadModel.IsOverride;
					transferFileModel.TransType = pepClassOssUploadModel.TransType;
					transferFileModel.ShowSpeedFlg = false;
					this.mUploadQueue.TryAdd(transferFileModel);
					return;
				}
				if (!(command == "openchinesecard"))
				{
					return;
				}
				base.Dispatcher.Invoke(new Action(delegate()
				{
					BaseBookReader.Instance.mBookResHelper.OpenH5Resource(jsonData, "", "", null, new Dictionary<string, List<string>>(), null, null, false, false);
				}), new object[0]);
				return;
			}
			else
			{
				if (this.mUploadQueue == null)
				{
					this.InitPepClassOssData();
				}
				TextBookReader textBookReader2 = this.mTextBookReader;
				if (textBookReader2 == null)
				{
					return;
				}
				textBookReader2.ReaderStartClass();
				return;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000814D File Offset: 0x0000634D
		public void InitPepClassOssData()
		{
			TransFileCommonHelper helper = new TransFileCommonHelper();
			this.mUploadQueue = new BlockingCollection<TransferFileModel>();
			TaskEx.Run(delegate()
			{
				try
				{
					foreach (TransferFileModel transferFileModel in this.mUploadQueue.GetConsumingEnumerable())
					{
						bool flag = helper.UploadFile(transferFileModel, null);
						transferFileModel.TransState = (flag ? TransferState.Completed : TransferState.Failed);
					}
				}
				catch (Exception)
				{
				}
			});
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void UpdateVoice()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008182 File Offset: 0x00006382
		private void OpenNBRes(string courseId)
		{
			NobookSingleActionHelper.Open(courseId, false, false);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008190 File Offset: 0x00006390
		private void Subscribe(IKeyboardMouseEvents events)
		{
			this.m_Events = events;
			this.m_Events.KeyDown += this.M_Events_KeyDown;
			this.m_Events.MouseDownExt += new System.EventHandler<MouseEventExtArgs>(this.M_Events_MouseMove);
			this.m_Events.MouseMoveExt += new System.EventHandler<MouseEventExtArgs>(this.M_Events_MouseMove);
			this.m_Events.MouseMove += this.M_Events_MouseMove;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008200 File Offset: 0x00006400
		private void M_Events_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.mCurMouseKeybprdTime = DateTime.Now;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008200 File Offset: 0x00006400
		private void M_Events_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.mCurMouseKeybprdTime = DateTime.Now;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008210 File Offset: 0x00006410
		private void Unsubscribe()
		{
			if (this.m_Events == null)
			{
				return;
			}
			this.m_Events.KeyDown -= this.M_Events_KeyDown;
			this.m_Events.MouseDownExt -= new System.EventHandler<MouseEventExtArgs>(this.M_Events_MouseMove);
			this.m_Events.MouseMoveExt -= new System.EventHandler<MouseEventExtArgs>(this.M_Events_MouseMove);
			this.m_Events.MouseMove -= this.M_Events_MouseMove;
			this.m_Events.Dispose();
			this.m_Events = null;
		}

		// Token: 0x04000002 RID: 2
		private const string PUBLICKEY = "-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----";

		// Token: 0x04000003 RID: 3
		private UserResourceDataAccess userResDA = new UserResourceDataAccess();

		// Token: 0x04000004 RID: 4
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"MainWindow"
		});

		// Token: 0x04000005 RID: 5
		private const string mSepFunc = "??<*>()??";

		// Token: 0x04000006 RID: 6
		private const string mSepParam = "&&<*>&&";

		// Token: 0x04000007 RID: 7
		private UserResourceDataAccess mUserResDAOper = new UserResourceDataAccess();

		// Token: 0x04000008 RID: 8
		private UserResourcesManager mUserResOper = UserResourcesManager.Instance;

		// Token: 0x04000009 RID: 9
		public string mSubscribeUserID = "";

		// Token: 0x0400000A RID: 10
		private string currenttbid = "";

		// Token: 0x0400000B RID: 11
		private System.Timers.Timer dispatcherTimer;

		// Token: 0x0400000C RID: 12
		private TextbooksDataAccess tbDA = new TextbooksDataAccess();

		// Token: 0x0400000D RID: 13
		private object objLock = new object();

		// Token: 0x0400000E RID: 14
		private UpLoadHelper mUploadOper = new UpLoadHelper();

		// Token: 0x0400000F RID: 15
		private ReducedImage mReduceImageOper = new ReducedImage();

		// Token: 0x04000010 RID: 16
		private DownloadHelper mDownlaod = new DownloadHelper();

		// Token: 0x04000011 RID: 17
		private EncryptorHelper mEncry = new EncryptorHelper();

		// Token: 0x04000012 RID: 18
		private object objUserOfflineLock = new object();

		// Token: 0x04000013 RID: 19
		private const int WM_SYSCOMMAND = 274;

		// Token: 0x04000014 RID: 20
		private const int SC_KEYMENU = 61696;

		// Token: 0x04000015 RID: 21
		private double mScreenWidth = SystemParameters.PrimaryScreenWidth;

		// Token: 0x04000016 RID: 22
		private double mScreenHeight = SystemParameters.PrimaryScreenHeight;

		// Token: 0x04000017 RID: 23
		public LoginHelper mLoginOper = new LoginHelper();

		// Token: 0x04000018 RID: 24
		private HttpHelper mHttpOper = new HttpHelper();

		// Token: 0x04000019 RID: 25
		private JsonHelper mJsonOper = new JsonHelper();

		// Token: 0x0400001A RID: 26
		private UserDataAccess mUserDataOper = new UserDataAccess();

		// Token: 0x0400001B RID: 27
		private static CefWebBrowser mMainBrowser;

		// Token: 0x0400001C RID: 28
		private NotifyIcon notifyIcon;

		// Token: 0x0400001D RID: 29
		public LoginFinish LoginFinishedCallback;

		// Token: 0x0400001E RID: 30
		private BlockingCollection<TransferFileModel> mUploadQueue;

		// Token: 0x04000020 RID: 32
		private SelectGroupWin mSelectGroupWin;

		// Token: 0x04000021 RID: 33
		private SelectGroup7ChapterWin mSelectGroup7ChapterWin;

		// Token: 0x04000022 RID: 34
		public TextBookReader mTextBookReader;

		// Token: 0x04000023 RID: 35
		private TutorialDataAccess mTutorialDA = new TutorialDataAccess();

		// Token: 0x04000024 RID: 36
		private static PopInfoDataAccess mPopInfoDA = new PopInfoDataAccess();

		// Token: 0x04000025 RID: 37
		private IKeyboardMouseEvents m_Events;

		// Token: 0x04000026 RID: 38
		private DateTime mCurMouseKeybprdTime = DateTime.Now;
	}
}
