using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Controls;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.PepUtility;
using JXP.Windows;
using JXP.Windows.MsgBox;
using Xceed.Wpf.Toolkit;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x02000022 RID: 34
	public class LoginUserControl : UserControl, IComponentConnector, IStyleConnector
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000C6AD File Offset: 0x0000A8AD
		public UpdateLoginData UpdateLoginDataCallBack { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000C6B6 File Offset: 0x0000A8B6
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000C6BE File Offset: 0x0000A8BE
		public HideControl HideControlCallBack { get; set; }

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public LoginUserControl()
		{
			this.InitializeComponent();
			base.Loaded += this.LoginWindow_Loaded;
			base.Unloaded += this.LoginUserControl_Unloaded;
			if (base.DataContext is LoginViewModel)
			{
				((LoginViewModel)base.DataContext).CheckLoginUserDataCallBack = new LoginViewModel.CheckLoginUserData(this.CheckUserData);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void LoginUserControl_Unloaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C740 File Offset: 0x0000A940
		private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (CommonParamter.Instance.CourseIsOnline)
			{
				this.mOwnerWin = Window.GetWindow(this);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C75A File Offset: 0x0000A95A
		private void WatermarkPasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				e.Handled = true;
				this.btnLogin_Click(sender, e);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C774 File Offset: 0x0000A974
		private void WatermarkTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Key == Key.Return)
				{
					TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
					UIElement uielement = Keyboard.FocusedElement as UIElement;
					if (uielement != null)
					{
						uielement.MoveFocus(request);
					}
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		private void btnUserDel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (CustomMessageBox.Question(" 确认要删除该用户的登录记录吗？", "确定", "取消", this.mOwnerWin, System.Windows.WindowStartupLocation.CenterOwner, false, MessageIconType.None, ""))
				{
					List<string> list = this.comboxUserId.ItemsSource as List<string>;
					JXP.Controls.IconButton iconButton = sender as JXP.Controls.IconButton;
					list.Remove(iconButton.Tag.ToString());
					this.comboxUserId.ItemsSource = null;
					this.comboxUserId.ItemsSource = list;
					this.SaveUserInfo(list);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("从下拉列表删除用户资源失败！" + ex.ToString());
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			LoginUserControl.<btnLogin_Click>d__20 <btnLogin_Click>d__;
			<btnLogin_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<btnLogin_Click>d__.<>4__this = this;
			<btnLogin_Click>d__.<>1__state = -1;
			<btnLogin_Click>d__.<>t__builder.Start<LoginUserControl.<btnLogin_Click>d__20>(ref <btnLogin_Click>d__);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		private void CheckUserData(UserModel userData)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				if (!(this.DataContext is LoginViewModel))
				{
					return;
				}
				LoginViewModel loginViewModel = (LoginViewModel)this.DataContext;
				if (userData == null || string.IsNullOrEmpty(userData.Status))
				{
					CustomMessageBox.Info((userData == null) ? "登录接口调用失败,无法登录" : userData.Msg, "确定", "", this.mOwnerWin, System.Windows.WindowStartupLocation.CenterOwner, false);
					return;
				}
				if (!string.IsNullOrEmpty(loginViewModel.UserId))
				{
					List<string> list = this.comboxUserId.ItemsSource as List<string>;
					if (list != null && !list.Contains(loginViewModel.UserId))
					{
						list.Insert(0, loginViewModel.UserId);
						this.SaveUserInfo(list);
					}
				}
				loginViewModel.UserInfo = userData;
				if (!(userData.Status == "110"))
				{
					CustomMessageBox.Info(string.IsNullOrEmpty(userData.Msg) ? "登录失败!" : userData.Msg, "确定", "", this.mOwnerWin, System.Windows.WindowStartupLocation.CenterOwner, false);
					return;
				}
				this.StopQRWork();
				if (userData.ResetPwd == "1")
				{
					new ModifyPwdWindow(userData.Phone)
					{
						Owner = this.mOwnerWin
					}.ShowDialog();
				}
				HideControl hideControlCallBack = this.HideControlCallBack;
				if (hideControlCallBack != null)
				{
					hideControlCallBack();
				}
				LoginSuccess loginSuccessCallBack = this.LoginSuccessCallBack;
				if (loginSuccessCallBack == null)
				{
					return;
				}
				loginSuccessCallBack(loginViewModel, this);
			}), new object[0]);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		private void btnBack1_Click(object sender, RoutedEventArgs e)
		{
			if (!(base.DataContext is LoginViewModel))
			{
				return;
			}
			LoginViewModel loginViewModel = (LoginViewModel)base.DataContext;
			loginViewModel.LoginVisibility = Visibility.Visible;
			loginViewModel.GetTestCodeVisibility = Visibility.Hidden;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C91C File Offset: 0x0000AB1C
		private void lblForget_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LoginViewModel loginViewModel = base.DataContext as LoginViewModel;
			if (loginViewModel == null)
			{
				return;
			}
			loginViewModel.LoginVisibility = Visibility.Hidden;
			loginViewModel.GetTestCodeVisibility = Visibility.Visible;
			this.cefBrowser.NavigateTo("https://web-hub.gxjyzy.cn/resetpwdNew?terminal=5&theme=009999&subTheme=33adad");
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200115",
				Passive = "客户端",
				FromPos = LoginUserControl.mClassPath + ".lblForget_MouseLeftButtonDown"
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C994 File Offset: 0x0000AB94
		private List<string> GetUserLst()
		{
			List<string> list = new List<string>();
			try
			{
				string userInfoFilePath = PepPathHelper.GetUserInfoFilePath();
				if (File.Exists(userInfoFilePath))
				{
					using (StreamReader streamReader = new StreamReader(userInfoFilePath))
					{
						while (!streamReader.EndOfStream)
						{
							string text = streamReader.ReadLine();
							string text2 = (text != null) ? text.Trim() : null;
							if (!string.IsNullOrEmpty(text2) && !list.Contains(text2))
							{
								list.Add(text2);
							}
						}
						streamReader.Close();
						streamReader.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("读取用户信息文件失败。" + ex.ToString());
			}
			return list;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000CA48 File Offset: 0x0000AC48
		private void SaveUserInfo(List<string> lstUser)
		{
			try
			{
				string userInfoFilePath = PepPathHelper.GetUserInfoFilePath();
				string directoryName = Path.GetDirectoryName(userInfoFilePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using (StreamWriter streamWriter = new StreamWriter(userInfoFilePath, false))
				{
					foreach (string value in lstUser)
					{
						streamWriter.WriteLine(value);
					}
					streamWriter.Close();
					streamWriter.Dispose();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("保存用户信息文件失败。" + ex.ToString());
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public void ReSetData()
		{
			this.comboxUserId.ItemsSource = this.GetUserLst();
			((LoginViewModel)base.DataContext).InitData();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000CB30 File Offset: 0x0000AD30
		public void StopQRWork()
		{
			try
			{
				LoginViewModel loginViewModel = (LoginViewModel)base.DataContext;
				if (loginViewModel != null)
				{
					loginViewModel.StopQRWork();
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "停止二维码轮询失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		private void BtnRefershCode_Click(object sender, RoutedEventArgs e)
		{
			LoginViewModel loginViewModel = (LoginViewModel)base.DataContext;
			if (loginViewModel == null)
			{
				return;
			}
			loginViewModel.LoadQRCode();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		private void BtnApp_Click(object sender, RoutedEventArgs e)
		{
			AppQRCodeWindow appQRCodeWindow = new AppQRCodeWindow();
			if (this.mOwnerWin == null)
			{
				appQRCodeWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
			}
			appQRCodeWindow.Owner = this.mOwnerWin;
			appQRCodeWindow.ShowDialog();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		private void lblTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LoginViewModel loginViewModel = (LoginViewModel)base.DataContext;
			if (loginViewModel == null)
			{
				return;
			}
			loginViewModel.ShowContactCustomer = false;
			loginViewModel.LoadQRCode();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CC14 File Offset: 0x0000AE14
		private void lblContactCustomer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				LoginViewModel loginViewModel = (LoginViewModel)base.DataContext;
				if (loginViewModel != null)
				{
					loginViewModel.ShowContactCustomer = true;
					loginViewModel.StopQRWork();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("联系客服处理失败:[{0}]。", arg));
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start(AppConsts.Online_Customer_service_Url);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("在线客服失败:[{0}]。", arg));
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Image_MouseLeftButtonDown(sender, null);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private void lblAdministrator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				Process.Start("https://web-hub.gxjyzy.cn/login");
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("管理员登录失败:[{0}]。", arg));
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CD00 File Offset: 0x0000AF00
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/JXP.PepDtp;component/view/loginwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000082C4 File Offset: 0x000064C4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CD30 File Offset: 0x0000AF30
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.gridTitle = (Grid)target;
				return;
			case 2:
				((StackPanel)target).MouseLeftButtonDown += this.lblTitle_MouseLeftButtonDown;
				return;
			case 3:
				this.lblAdministrator = (TextBlock)target;
				this.lblAdministrator.MouseLeftButtonDown += this.lblAdministrator_MouseLeftButtonDown;
				return;
			case 4:
				this.lblContactCustomer = (TextBlock)target;
				this.lblContactCustomer.MouseLeftButtonDown += this.lblContactCustomer_MouseLeftButtonDown;
				return;
			case 5:
				this.btnMinimized = (Button)target;
				return;
			case 6:
				this.btnClose = (Button)target;
				return;
			case 7:
				this.lblQRInfo = (TextBlock)target;
				return;
			case 8:
				this.btnApp = (Button)target;
				this.btnApp.Click += this.BtnApp_Click;
				return;
			case 9:
				this.btnRefershCode = (Button)target;
				this.btnRefershCode.Click += this.BtnRefershCode_Click;
				return;
			case 10:
				this.comboxUserId = (ComboBox)target;
				this.comboxUserId.PreviewKeyDown += this.WatermarkTextBox_PreviewKeyDown;
				return;
			case 12:
				((WatermarkPasswordBox)target).PreviewKeyDown += this.WatermarkPasswordBox_PreviewKeyDown;
				return;
			case 13:
				this.lblForget = (TextBlock)target;
				this.lblForget.MouseLeftButtonDown += this.lblForget_MouseLeftButtonDown;
				return;
			case 14:
				this.btnLogin = (Button)target;
				this.btnLogin.Click += this.btnLogin_Click;
				return;
			case 15:
				((Image)target).MouseLeftButtonDown += this.Image_MouseLeftButtonDown;
				return;
			case 16:
				((Button)target).Click += this.Button_Click;
				return;
			case 17:
				this.gridTitle1 = (Grid)target;
				return;
			case 18:
				((StackPanel)target).MouseLeftButtonDown += new MouseButtonEventHandler(this.btnBack1_Click);
				return;
			case 19:
				this.wfhMainBrowser = (CefWindowsFormsHost)target;
				return;
			case 20:
				this.cefBrowser = (CefWebBrowser)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CF70 File Offset: 0x0000B170
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 11)
			{
				((JXP.Controls.IconButton)target).Click += this.btnUserDel_Click;
			}
		}

		// Token: 0x040000B0 RID: 176
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"LoginUserControl"
		});

		// Token: 0x040000B1 RID: 177
		public LoginSuccess LoginSuccessCallBack;

		// Token: 0x040000B2 RID: 178
		public BindingPhone BindingPhoneCallBack;

		// Token: 0x040000B3 RID: 179
		public MainMenuEnums mainmenuenus = MainMenuEnums.MyHomePage;

		// Token: 0x040000B4 RID: 180
		private UserFirstLoginDataAccess mUserFirstLoginDA = new UserFirstLoginDataAccess();

		// Token: 0x040000B5 RID: 181
		private Window mOwnerWin;

		// Token: 0x040000B8 RID: 184
		internal Grid gridTitle;

		// Token: 0x040000B9 RID: 185
		internal TextBlock lblAdministrator;

		// Token: 0x040000BA RID: 186
		internal TextBlock lblContactCustomer;

		// Token: 0x040000BB RID: 187
		internal Button btnMinimized;

		// Token: 0x040000BC RID: 188
		internal Button btnClose;

		// Token: 0x040000BD RID: 189
		internal TextBlock lblQRInfo;

		// Token: 0x040000BE RID: 190
		internal Button btnApp;

		// Token: 0x040000BF RID: 191
		internal Button btnRefershCode;

		// Token: 0x040000C0 RID: 192
		internal ComboBox comboxUserId;

		// Token: 0x040000C1 RID: 193
		internal TextBlock lblForget;

		// Token: 0x040000C2 RID: 194
		internal Button btnLogin;

		// Token: 0x040000C3 RID: 195
		internal Grid gridTitle1;

		// Token: 0x040000C4 RID: 196
		internal CefWindowsFormsHost wfhMainBrowser;

		// Token: 0x040000C5 RID: 197
		internal CefWebBrowser cefBrowser;

		// Token: 0x040000C6 RID: 198
		private bool _contentLoaded;
	}
}
