using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.ViewModel;
using JXP.Windows;
using Xilium.CefGlue.WindowsForms;

namespace JXP.PepDtp.View
{
	// Token: 0x02000026 RID: 38
	public partial class ModifyPwdWindow : Window
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000D309 File Offset: 0x0000B509
		public ModifyPwdWindow(string phone)
		{
			this.InitializeComponent();
			this.mPhoneNum = phone;
			base.Loaded += this.ModifyPwdWindow_Loaded;
			this.cefBrowser.StartUrl = "https://web-hub.gxjyzy.cn/resetpwdBox?terminal=5&theme=009999&subTheme=33adad";
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D340 File Offset: 0x0000B540
		private void ModifyPwdWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.mModifyPwdVm = new ModifyPwdViewModel();
			base.DataContext = this.mModifyPwdVm;
			this.mModifyPwdVm.PhoneNum = this.mPhoneNum;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000D36C File Offset: 0x0000B56C
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭修改初始密码画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		private void BtnFinish_Click(object sender, RoutedEventArgs e)
		{
			ModifyPwdWindow.<BtnFinish_Click>d__6 <BtnFinish_Click>d__;
			<BtnFinish_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<BtnFinish_Click>d__.<>4__this = this;
			<BtnFinish_Click>d__.<>1__state = -1;
			<BtnFinish_Click>d__.<>t__builder.Start<ModifyPwdWindow.<BtnFinish_Click>d__6>(ref <BtnFinish_Click>d__);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		private void BtnGetCode_Click(object sender, RoutedEventArgs e)
		{
			ModifyPwdWindow.<BtnGetCode_Click>d__7 <BtnGetCode_Click>d__;
			<BtnGetCode_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<BtnGetCode_Click>d__.<>4__this = this;
			<BtnGetCode_Click>d__.<>1__state = -1;
			<BtnGetCode_Click>d__.<>t__builder.Start<ModifyPwdWindow.<BtnGetCode_Click>d__7>(ref <BtnGetCode_Click>d__);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000D428 File Offset: 0x0000B628
		private void BtnNext_Click(object sender, RoutedEventArgs e)
		{
			ModifyPwdWindow.<BtnNext_Click>d__8 <BtnNext_Click>d__;
			<BtnNext_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<BtnNext_Click>d__.<>1__state = -1;
			<BtnNext_Click>d__.<>t__builder.Start<ModifyPwdWindow.<BtnNext_Click>d__8>(ref <BtnNext_Click>d__);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void WatermarkPasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x040000D4 RID: 212
		private ModifyPwdViewModel mModifyPwdVm;

		// Token: 0x040000D5 RID: 213
		private string mPhoneNum;
	}
}
