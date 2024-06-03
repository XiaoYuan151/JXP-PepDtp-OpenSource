using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;

namespace JXP.PepDtp.View
{
	// Token: 0x02000012 RID: 18
	public partial class AppCentertWindow : Window
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000A462 File Offset: 0x00008662
		public AppCentertWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A470 File Offset: 0x00008670
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
				this.RemoveContent();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭个人中心画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public void AddContent(Control control)
		{
			this.contentCtr.Content = control;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A4CE File Offset: 0x000086CE
		public void RemoveContent()
		{
			this.contentCtr.Content = null;
		}
	}
}
