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
	// Token: 0x02000013 RID: 19
	public partial class AppQRCodeWindow : Window
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000A58B File Offset: 0x0000878B
		public AppQRCodeWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A59C File Offset: 0x0000879C
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭下载二维码画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}
	}
}
