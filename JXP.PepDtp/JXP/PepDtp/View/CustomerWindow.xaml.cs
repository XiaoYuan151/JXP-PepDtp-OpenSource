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
using JXP.Windows.View;

namespace JXP.PepDtp.View
{
	// Token: 0x02000016 RID: 22
	public partial class CustomerWindow : Window
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000AECF File Offset: 0x000090CF
		public CustomerWindow()
		{
			this.InitializeComponent();
			base.Closed += this.CustomerWindow_Closed;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000AEEF File Offset: 0x000090EF
		private void CustomerWindow_Closed(object sender, EventArgs e)
		{
			MaskLayerWindow.SingleInstance().CloseMaskWindow();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AEFC File Offset: 0x000090FC
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭联系客服画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AF48 File Offset: 0x00009148
		private void btnCustomer_Click(object sender, RoutedEventArgs e)
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
	}
}
