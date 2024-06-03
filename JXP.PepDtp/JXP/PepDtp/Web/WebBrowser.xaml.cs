using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using JXP.Logs;
using JXP.OsInfo;
using JXP.Windows;
using JXP.WmTouchDevice;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.Web
{
	// Token: 0x02000010 RID: 16
	public partial class WebBrowser : Window
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000A129 File Offset: 0x00008329
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000A13B File Offset: 0x0000833B
		public string StartUrl
		{
			get
			{
				return (string)base.GetValue(WebBrowser.mStartUrl);
			}
			set
			{
				this.cefBrowser.StartUrl = value;
				base.SetValue(WebBrowser.mStartUrl, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000A155 File Offset: 0x00008355
		public CefWebBrowser CefBrowser
		{
			get
			{
				return this.cefBrowser;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A15D File Offset: 0x0000835D
		public WebBrowser()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A16B File Offset: 0x0000836B
		public WebBrowser(string url) : this()
		{
			this.StartUrl = url;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A17A File Offset: 0x0000837A
		public void NavigateTo(string url)
		{
			if (string.IsNullOrEmpty(this.StartUrl))
			{
				this.StartUrl = url;
			}
			this.cefBrowser.NavigateTo(url);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A19C File Offset: 0x0000839C
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.cefBrowser.LoadEnd += this.cefBrowser_LoadEnd;
			base.LocationChanged += this.WebBrowser_LocationChanged;
			base.SizeChanged += this.WebBrowser_SizeChanged;
			this.CefBrowser.LoadingStateChange += this.CefBrowser_LoadingStateChange;
			this.cefBrowser.LoadError += this.CefBrowser_LoadError;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A212 File Offset: 0x00008412
		private void CefBrowser_LoadError(object sender, LoadErrorEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.FailedUrl))
			{
				UrlNavigateError urlNavigateErrorCallBack = this.UrlNavigateErrorCallBack;
				if (urlNavigateErrorCallBack == null)
				{
					return;
				}
				urlNavigateErrorCallBack(true);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A232 File Offset: 0x00008432
		private void CefBrowser_LoadingStateChange(object sender, LoadingStateChangeEventArgs e)
		{
			UrlNavigateError urlNavigateErrorCallBack = this.UrlNavigateErrorCallBack;
			if (urlNavigateErrorCallBack == null)
			{
				return;
			}
			urlNavigateErrorCallBack(e.IsLoading);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A24A File Offset: 0x0000844A
		private void cefBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A24A File Offset: 0x0000844A
		private void WebBrowser_LocationChanged(object sender, EventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A24A File Offset: 0x0000844A
		private void WebBrowser_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.RePositionCefBrowser();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A254 File Offset: 0x00008454
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

		// Token: 0x0600014D RID: 333 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public void ShowDebugWindow()
		{
			CefBrowserHost host = this.cefBrowser.Browser.GetHost();
			CefWindowInfo cefWindowInfo = CefWindowInfo.Create();
			cefWindowInfo.SetAsPopup(IntPtr.Zero, "DevTools");
			host.ShowDevTools(cefWindowInfo, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A314 File Offset: 0x00008514
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

		// Token: 0x0600014F RID: 335 RVA: 0x0000A368 File Offset: 0x00008568
		private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				MessageTouchDevice.WndProc(window, msg, wParam, lParam, ref handled);
			}
			return IntPtr.Zero;
		}

		// Token: 0x04000054 RID: 84
		private static readonly DependencyProperty mStartUrl = DependencyProperty.Register("StartUrl", typeof(string), typeof(WebBrowser), new UIPropertyMetadata(""));

		// Token: 0x04000055 RID: 85
		public UrlNavigateError UrlNavigateErrorCallBack;
	}
}
