using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using JXP.Controls.UserControls;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.View.InnerReaderControl;
using JXP.PepDtp.ViewModel;
using JXP.Utilities;
using JXP.Windows;
using pep.sdk.utility;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000048 RID: 72
	public partial class ShareBookControl : UserControl, IStyleConnector
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00019E7D File Offset: 0x0001807D
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00019E85 File Offset: 0x00018085
		public OpenShareBook OpenShareBookCallBack { get; set; }

		// Token: 0x06000483 RID: 1155 RVA: 0x00019E90 File Offset: 0x00018090
		public ShareBookControl()
		{
			this.InitializeComponent();
			this.mShareBookVM = new ShareBookViewModel();
			base.DataContext = this.mShareBookVM;
			MessageBus.Default.Unregister<SdkMessage>(this);
			MessageBus.Default.Register<SdkMessage>(this, new Action<SdkMessage>(this.HandleSdkMessage), false);
			base.Loaded += this.ShareBookControl_Loaded;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00019EF8 File Offset: 0x000180F8
		private void ShareBookControl_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ownerWin = Window.GetWindow(this);
				base.Dispatcher.UnhandledException -= this.Dispatcher_UnhandledException;
				base.Dispatcher.UnhandledException += this.Dispatcher_UnhandledException;
				this.mShareBookVM.SetPagingCountCallBack = new SetPagingCount(this.SetPagingCount);
				this.ucPaging.PageIndexChanngedCallBack = new PageIndexChannged(this.ChanagePage);
				this.ucKeywordSeaarch.txtKeyWord.PreviewKeyDown -= this.txtKeyWord_PreviewKeyDown;
				this.ucKeywordSeaarch.btnSearch.Click -= this.btnSearch_Click;
				this.ucKeywordSeaarch.txtKeyWord.PreviewKeyDown += this.txtKeyWord_PreviewKeyDown;
				this.ucKeywordSeaarch.btnSearch.Click += this.btnSearch_Click;
				this.AddComboxEvent();
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("共享教材初始化加载数据失败：{0}。", arg));
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001A010 File Offset: 0x00018210
		private void AddComboxEvent()
		{
			this.comBoxXD.SelectionChanged -= this.XDComboBox_SelectionChanged;
			this.comBoxXK.SelectionChanged -= this.ComboBox_SelectionChanged;
			this.comBoxLY.SelectionChanged -= this.ComboBox_SelectionChanged;
			this.comBoxXD.SelectionChanged += this.XDComboBox_SelectionChanged;
			this.comBoxXK.SelectionChanged += this.ComboBox_SelectionChanged;
			this.comBoxLY.SelectionChanged += this.ComboBox_SelectionChanged;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001A0A8 File Offset: 0x000182A8
		private void RemoveComboxEvent()
		{
			this.comBoxXD.SelectionChanged -= this.XDComboBox_SelectionChanged;
			this.comBoxXK.SelectionChanged -= this.ComboBox_SelectionChanged;
			this.comBoxLY.SelectionChanged -= this.ComboBox_SelectionChanged;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001A0FC File Offset: 0x000182FC
		private void HandleSdkMessage(SdkMessage message)
		{
			if (message == null)
			{
				return;
			}
			SdkCommand command = message.Command;
			if (command == SdkCommand.Login)
			{
				this.HandleLogin();
				return;
			}
			if (command != SdkCommand.Logout)
			{
				return;
			}
			this.HandleLogout();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001A129 File Offset: 0x00018329
		private void HandleLogin()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					this.RemoveComboxEvent();
					this.mShareBookVM.InitData();
					this.mShareBookVM.InitComboxData(CommonParamter.Instance.UserXD, CommonParamter.Instance.UserXK, "");
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "共享教材处理登录完成初始数据失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
				finally
				{
					this.AddComboxEvent();
				}
			}), new object[0]);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001A149 File Offset: 0x00018349
		public void HandleLogout()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.mShareBookVM.InitData();
			}), new object[0]);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001A16C File Offset: 0x0001836C
		private void ChanagePage(int nPageNnm)
		{
			try
			{
				this.mShareBookVM.CurPage = nPageNnm;
				this.mShareBookVM.GetShareBookListAsync();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("共享教材翻页请求数据失败!" + ex.ToString());
				this.mShareBookVM.MessageContent = "检索失败";
				CustomMessageBox.Info("检索失败!", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, false);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001A1EC File Offset: 0x000183EC
		private void SetPagingCount(int totlePages, int curPage)
		{
			this.ucPaging.TotalPages = totlePages;
			this.ucPaging.CurrentPage = curPage;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001A206 File Offset: 0x00018406
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			this.SearchData();
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200084",
				FromPos = ShareBookControl.mClassPath + ".btnSearch_Click"
			});
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A23D File Offset: 0x0001843D
		private void txtKeyWord_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.ucKeywordSeaarch.btnSearch.Focus();
				this.btnSearch_Click(null, null);
				this.ucKeywordSeaarch.txtKeyWord.Focus();
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001A272 File Offset: 0x00018472
		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			this.mShareBookVM.Keyword = string.Empty;
			this.mShareBookVM.ShowKeyword = true;
			this.SearchData();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001A298 File Offset: 0x00018498
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				this.SearchData();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001A2D0 File Offset: 0x000184D0
		private void XDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				this.RemoveComboxEvent();
				string id = this.mShareBookVM.SelectXD.Id;
				this.mShareBookVM.RefreshOnlyXKsByXD(id);
				if (!this.mShareBookVM.XKLst.Any(delegate(MetaModel a)
				{
					string id2 = a.Id;
					MetaModel selectXK = this.mShareBookVM.SelectXK;
					return id2 == ((selectXK != null) ? selectXK.Id : null);
				}))
				{
					this.comBoxXK.SelectedIndex = 0;
				}
				this.SearchData();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
			finally
			{
				this.AddComboxEvent();
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001A36C File Offset: 0x0001856C
		public void SearchData()
		{
			try
			{
				this.mShareBookVM.CurPage = 1;
				this.mShareBookVM.GetShareBookListAsync();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("共享教材检索数据失败" + ex.ToString());
				this.mShareBookVM.MessageContent = "检索失败";
				CustomMessageBox.Info("检索失败!", "确定", "", this.ownerWin, WindowStartupLocation.CenterOwner, false);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001A3EC File Offset: 0x000185EC
		private void TBImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ShareBookDataModel data = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as ShareBookDataModel;
			this.OpenTextBook(data);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001A420 File Offset: 0x00018620
		private void TBImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			LogHelper.Instance.Error("设置封皮失败[TBImage_ImageFailed]！" + ((e != null) ? e.ToString() : null));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001A444 File Offset: 0x00018644
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			try
			{
				e.Handled = true;
				LogHelper instance = LogHelper.Instance;
				string str = "共享教材出错：";
				Exception exception = e.Exception;
				instance.Error(str + ((exception != null) ? exception.ToString() : null));
			}
			catch
			{
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001A494 File Offset: 0x00018694
		private void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			ShareBookDataModel data = (VisualHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject) as ListBoxItem).DataContext as ShareBookDataModel;
			this.OpenTextBook(data);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001A4C8 File Offset: 0x000186C8
		private void OpenTextBook(ShareBookDataModel data)
		{
			if (data == null)
			{
				return;
			}
			OpenShareBook openShareBookCallBack = this.OpenShareBookCallBack;
			if (openShareBookCallBack == null)
			{
				return;
			}
			openShareBookCallBack(data.TextBookID, data.SubscribeID, data.FdfUrl);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001A610 File Offset: 0x00018810
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 9:
				((Rectangle)target).MouseLeftButtonDown += this.TBImage_MouseLeftButtonDown;
				return;
			case 10:
				((Image)target).MouseLeftButtonDown += this.TBImage_MouseLeftButtonDown;
				((Image)target).ImageFailed += this.TBImage_ImageFailed;
				return;
			case 11:
				((Button)target).Click += this.btnOpen_Click;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400025E RID: 606
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"UserControls",
			"ShareBookControl"
		});

		// Token: 0x0400025F RID: 607
		private ShareBookViewModel mShareBookVM;

		// Token: 0x04000260 RID: 608
		private Window ownerWin;
	}
}
