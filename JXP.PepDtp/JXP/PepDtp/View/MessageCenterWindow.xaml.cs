using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.Windows;
using JXP.Windows.View;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x02000027 RID: 39
	public partial class MessageCenterWindow : Window
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000D518 File Offset: 0x0000B718
		public MessageCenterWindow()
		{
			this.InitializeComponent();
			string startUrl = ExplorerHelper.CreateWebUrlByIndex(61);
			this.cefBrowser.StartUrl = startUrl;
			base.Closed += this.MessageCenterWindow_Closed;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D558 File Offset: 0x0000B758
		private void MessageCenterWindow_Closed(object sender, EventArgs e)
		{
			try
			{
				this.cefBrowser.Dispose();
				MaskLayerWindow.SingleInstance().CloseMaskWindow();
			}
			catch
			{
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D590 File Offset: 0x0000B790
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭消息中心画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}
	}
}
