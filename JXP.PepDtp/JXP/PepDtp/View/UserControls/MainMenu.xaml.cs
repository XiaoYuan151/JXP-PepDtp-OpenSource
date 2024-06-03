using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Enums;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.View.CustomControl;
using JXP.Utilities.Cryptography;
using Newtonsoft.Json;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x0200003E RID: 62
	public partial class MainMenu : UserControl, IStyleConnector
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000348 RID: 840 RVA: 0x000133F0 File Offset: 0x000115F0
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x00013428 File Offset: 0x00011628
		public event MenuItemMouseLeftButtonDown MenuMouseLeftButtonDownEvent;

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001345D File Offset: 0x0001165D
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0001346F File Offset: 0x0001166F
		public ObservableCollection<MenuItemModel> MenuLst
		{
			get
			{
				return (ObservableCollection<MenuItemModel>)base.GetValue(MainMenu.MenuLstProperty);
			}
			set
			{
				base.SetValue(MainMenu.MenuLstProperty, value);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001347D File Offset: 0x0001167D
		public MainMenu()
		{
			this.InitializeComponent();
			this.InitMenuList();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00013494 File Offset: 0x00011694
		private void InitMenuList()
		{
			this.MenuLst = new ObservableCollection<MenuItemModel>();
			this.MenuLst.Add(new MenuItemModel("", "数字教材", MainMenuEnums.Szjc, 22, false, ""));
			this.MenuLst.Add(new MenuItemModel("", "教学课件", MainMenuEnums.PrepareLessons, 22, false, ""));
			this.MenuLst.Add(new MenuItemModel("", "资源管理", MainMenuEnums.Zygl, 22, false, ""));
			this.MenuLst.Add(new MenuItemModel("", "学科工具", MainMenuEnums.PFTeachingCenter, 22, false, ""));
			this.MenuLst.Add(new MenuItemModel("", "数据轨迹", MainMenuEnums.PepResources, 22, false, ""));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00013560 File Offset: 0x00011760
		public void SetSelectedItemByIndex(MainMenuEnums menuIndex)
		{
			MenuItemModel menuItemModel = this.MenuLst.FirstOrDefault((MenuItemModel a) => a.PageIndex == menuIndex);
			if (menuItemModel != null)
			{
				this.lstMenu.SelectedItem = menuItemModel;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000135A4 File Offset: 0x000117A4
		public bool IsSelected(MainMenuEnums menuIndex)
		{
			MenuItemModel menuItemModel = this.lstMenu.SelectedItem as MenuItemModel;
			return menuItemModel != null && menuItemModel.PageIndex == menuIndex;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000135D1 File Offset: 0x000117D1
		private void lstMenu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Pressed)
			{
				e.Handled = true;
				return;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000135E4 File Offset: 0x000117E4
		private void lstMenu_MouseLeftButtonDown(object sender, RoutedEventArgs e)
		{
			ListBoxItem listBoxItem = sender as ListBoxItem;
			if (listBoxItem != null)
			{
				try
				{
					MenuItemModel menuItemModel = listBoxItem.DataContext as MenuItemModel;
					if (menuItemModel != null && menuItemModel.PageIndex == MainMenuEnums.GjZhJyPatform)
					{
						e.Handled = true;
						Process.Start("https://basic.smartedu.cn/");
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx512000",
							Passive = "客户端左侧菜单点击"
						});
						return;
					}
				}
				catch (Exception)
				{
				}
				listBoxItem.IsSelected = true;
			}
			MenuItemMouseLeftButtonDown menuMouseLeftButtonDownEvent = this.MenuMouseLeftButtonDownEvent;
			if (menuMouseLeftButtonDownEvent == null)
			{
				return;
			}
			menuMouseLeftButtonDownEvent(sender, e);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001367C File Offset: 0x0001187C
		private UserInfoResult GetUserInfo()
		{
			UserInfoResult result;
			try
			{
				string value = new RSA("-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----", true).Encode(CommonParamter.Instance.LoginUserId);
				string strUrl = ConfigHelper.WebServerUrl + "user/queryUserInfo.json";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["user_id"] = value;
				dictionary["flag"] = "hd";
				result = JsonConvert.DeserializeObject<UserInfoResult>(HttpHelper.HttpPost(strUrl, dictionary, new int?(300000), "", true));
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("获取用户信息失败:[{0}].", arg));
				result = null;
			}
			return result;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00013814 File Offset: 0x00011A14
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 3)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = UIElement.PreviewMouseLeftButtonDownEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.lstMenu_MouseLeftButtonDown);
				((Style)target).Setters.Add(eventSetter);
			}
		}

		// Token: 0x040001AD RID: 429
		private const string PUBLICKEY = "-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----";

		// Token: 0x040001AF RID: 431
		public static readonly DependencyProperty MenuLstProperty = DependencyProperty.Register("MenuLst", typeof(ObservableCollection<MenuItemModel>), typeof(MainMenu), new PropertyMetadata(new ObservableCollection<MenuItemModel>()));
	}
}
