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
	// Token: 0x02000028 RID: 40
	public partial class MyResDelSelectWindow : Window
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000D6B5 File Offset: 0x0000B8B5
		public int SelectDelInfo { get; set; }

		// Token: 0x06000224 RID: 548 RVA: 0x0000D6BE File Offset: 0x0000B8BE
		public MyResDelSelectWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00004541 File Offset: 0x00002741
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		private void btnBoth_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.SelectDelInfo = 1;
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("删除资源选择两端删除失败。" + ex.ToString());
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D718 File Offset: 0x0000B918
		private void btnLocalOnly_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.SelectDelInfo = 2;
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("删除资源选择本地删除失败。" + ex.ToString());
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D764 File Offset: 0x0000B964
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
				this.SelectDelInfo = 0;
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("删除资源选择画面关闭失败。" + ex.ToString());
			}
		}
	}
}
