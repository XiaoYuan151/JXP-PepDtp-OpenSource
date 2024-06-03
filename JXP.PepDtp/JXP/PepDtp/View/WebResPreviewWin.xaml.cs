using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Threading;
using JXP.Logs;
using JXP.OsInfo;
using JXP.Windows;
using JXP.WmTouchDevice;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x02000037 RID: 55
	public partial class WebResPreviewWin : Window
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00011814 File Offset: 0x0000FA14
		public CefWebBrowser CefBrowser
		{
			get
			{
				return this.cefBrowser;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001181C File Offset: 0x0000FA1C
		public WebResPreviewWin(string strUrl)
		{
			this.InitializeComponent();
			this.cefBrowser.StartUrl = strUrl;
			base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate()
			{
				try
				{
					base.Activate();
					CefBrowser browser = this.cefBrowser.Browser;
					if (browser != null)
					{
						CefBrowserHost host = browser.GetHost();
						if (host != null)
						{
							host.SetFocus(true);
						}
					}
				}
				catch
				{
				}
			}));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011850 File Offset: 0x0000FA50
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				try
				{
					this.cefBrowser.Dispose();
				}
				catch
				{
				}
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("点击平台资源预览关闭按钮失败！" + ex.ToString());
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000118B0 File Offset: 0x0000FAB0
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				if (e.ButtonState == MouseButtonState.Pressed)
				{
					base.DragMove();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000118F4 File Offset: 0x0000FAF4
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.cefBrowser.LoadEnd += this.cefBrowser_LoadEnd;
			base.LocationChanged += this.WebBrowser_LocationChanged;
			base.SizeChanged += this.WebBrowser_SizeChanged;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011931 File Offset: 0x0000FB31
		public void NavigateTo(string url)
		{
			this.cefBrowser.NavigateTo(url);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001193F File Offset: 0x0000FB3F
		private void cefBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001193F File Offset: 0x0000FB3F
		private void WebBrowser_LocationChanged(object sender, EventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001193F File Offset: 0x0000FB3F
		private void WebBrowser_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011948 File Offset: 0x0000FB48
		internal void RePositionCefBrowser()
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

		// Token: 0x060002FB RID: 763 RVA: 0x000119BC File Offset: 0x0000FBBC
		private void Window_SourceInitialized(object sender, EventArgs e)
		{
			if (Environment.OSVersion.IsLessThan(OsVersion.Vista))
			{
				return;
			}
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				IntPtr handle = new WindowInteropHelper(window).Handle;
				MessageTouchDevice.RegisterTouchWindow(handle);
				HwndSource hwndSource = HwndSource.FromHwnd(handle);
				if (hwndSource != null)
				{
					hwndSource.AddHook(new HwndSourceHook(this.Hook));
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011A10 File Offset: 0x0000FC10
		private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				MessageTouchDevice.WndProc(window, msg, wParam, lParam, ref handled);
			}
			return IntPtr.Zero;
		}
	}
}
