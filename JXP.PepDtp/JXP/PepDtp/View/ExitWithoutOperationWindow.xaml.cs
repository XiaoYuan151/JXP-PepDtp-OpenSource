using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JXP.PepDtp.View
{
	// Token: 0x0200001C RID: 28
	public partial class ExitWithoutOperationWindow : Window
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		public Action ExitLoginCallback { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BEBE File Offset: 0x0000A0BE
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		public Action SetCurTimeCallback { get; set; }

		// Token: 0x060001BA RID: 442 RVA: 0x0000BECF File Offset: 0x0000A0CF
		public ExitWithoutOperationWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.ExitWithoutOperationWindow_Loaded;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		private void ExitWithoutOperationWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.dispatcherTimer = new Timer();
			this.dispatcherTimer.Elapsed += this.DispatcherTimer_Elapsed;
			this.dispatcherTimer.Interval = 1000.0;
			this.dispatcherTimer.Start();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BF46 File Offset: 0x0000A146
		private void DispatcherTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				if (this.second > 0)
				{
					this.second--;
					this.tbSecond.Text = string.Format("{0}秒", this.second);
					return;
				}
				try
				{
					this.dispatcherTimer.Stop();
					this.dispatcherTimer.Dispose();
					base.Close();
				}
				catch
				{
				}
				Action exitLoginCallback = this.ExitLoginCallback;
				if (exitLoginCallback == null)
				{
					return;
				}
				exitLoginCallback();
			}), new object[0]);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BF68 File Offset: 0x0000A168
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Action setCurTimeCallback = this.SetCurTimeCallback;
				if (setCurTimeCallback != null)
				{
					setCurTimeCallback();
				}
				Timer timer = this.dispatcherTimer;
				if (timer != null)
				{
					timer.Dispose();
				}
				base.Close();
			}
			catch
			{
			}
		}

		// Token: 0x0400009B RID: 155
		private Timer dispatcherTimer;

		// Token: 0x0400009C RID: 156
		private int second = 30;
	}
}
