using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.PepDtp.Events;
using JXP.PepDtp.ViewModel;

namespace JXP.PepDtp.PepClass.View
{
	// Token: 0x02000080 RID: 128
	public partial class SelectGroupWin : Window
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000900 RID: 2304 RVA: 0x0002B468 File Offset: 0x00029668
		// (remove) Token: 0x06000901 RID: 2305 RVA: 0x0002B4A0 File Offset: 0x000296A0
		public event EventHandler<PepClassEventArgs> OnStartPepClass;

		// Token: 0x06000902 RID: 2306 RVA: 0x0002B4D5 File Offset: 0x000296D5
		public SelectGroupWin()
		{
			this.InitializeComponent();
			this.mSelectGroupVM = new SelectGroupViewModel();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002B4F0 File Offset: 0x000296F0
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SelectGroupWin.<Window_Loaded>d__5 <Window_Loaded>d__;
			<Window_Loaded>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Window_Loaded>d__.<>4__this = this;
			<Window_Loaded>d__.<>1__state = -1;
			<Window_Loaded>d__.<>t__builder.Start<SelectGroupWin.<Window_Loaded>d__5>(ref <Window_Loaded>d__);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00004541 File Offset: 0x00002741
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002B528 File Offset: 0x00029728
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			SelectGroupWin.<btnOK_Click>d__8 <btnOK_Click>d__;
			<btnOK_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<btnOK_Click>d__.<>4__this = this;
			<btnOK_Click>d__.<>1__state = -1;
			<btnOK_Click>d__.<>t__builder.Start<SelectGroupWin.<btnOK_Click>d__8>(ref <btnOK_Click>d__);
		}

		// Token: 0x0400047D RID: 1149
		private SelectGroupViewModel mSelectGroupVM;
	}
}
