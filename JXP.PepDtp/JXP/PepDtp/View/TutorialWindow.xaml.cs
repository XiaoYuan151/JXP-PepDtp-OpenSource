using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Controls.Controls;
using JXP.Data;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Paramter;

namespace JXP.PepDtp.View
{
	// Token: 0x02000033 RID: 51
	public partial class TutorialWindow : Window
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public TutorialWindow()
		{
			this.InitializeComponent();
			base.Closed += this.TutorialWindow_Closed;
			base.Loaded += this.TutorialWindow_Loaded;
			this.lstImage.SelectionChanged += this.LstImage_SelectionChanged;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010908 File Offset: 0x0000EB08
		private void TutorialWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.columClose.Width = new GridLength(base.Width * 0.04);
			this.btnClose.Width = base.Width * 0.04;
			this.btnClose.Height = base.Width * 0.04;
			this.btnClose.Margin = new Thickness(0.0, base.Width * 0.04, 0.0, 0.0);
			this.btnContinue.Width = base.Width * 0.2;
			this.btnContinue.Height = base.Height * 0.086;
			double bottom = base.Height * 0.1;
			this.btnContinue.Margin = new Thickness(0.0, 0.0, 0.0, bottom);
			double left = base.Width * 0.089;
			this.btnPrev.Margin = new Thickness(left, 0.0, 0.0, bottom);
			this.btnStart.Width = base.Width * 0.269;
			this.btnStart.Height = base.Height * 0.158;
			this.btnPrev.Visibility = Visibility.Collapsed;
			this.btnStart.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010A9C File Offset: 0x0000EC9C
		private void TutorialWindow_Closed(object sender, EventArgs e)
		{
			try
			{
				TutorialWindow.mPopInfoDA.InsertPopInfo(new PopInfoModel
				{
					TypeFlg = "2100",
					UserId = CommonParamter.Instance.LoginUserId,
					Comment = "八桂教学通V3.0.0引导提示"
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010B00 File Offset: 0x0000ED00
		private void LstImage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.lstImage.SelectedIndex == 0)
			{
				this.btnPrev.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.btnPrev.Visibility = Visibility.Visible;
			}
			if (this.lstImage.SelectedIndex == this.lstImage.Items.Count - 1)
			{
				this.btnStart.Visibility = Visibility.Visible;
				this.btnContinue.Visibility = Visibility.Collapsed;
				return;
			}
			this.btnStart.Visibility = Visibility.Collapsed;
			this.btnContinue.Visibility = Visibility.Visible;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010B84 File Offset: 0x0000ED84
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		private void btnContinue_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.lstImage.SelectedIndex != this.lstImage.Items.Count - 1)
				{
					this.lstImage.NextAnimateSliderValueTo();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("引导图图片点击失败:[{0}]。", arg));
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010C1C File Offset: 0x0000EE1C
		private void btnPrev_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.lstImage.SelectedIndex > 0)
				{
					this.lstImage.PrevAnimateSliderValueTo();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("引导图图片点击失败:[{0}]。", arg));
			}
		}

		// Token: 0x04000155 RID: 341
		private static PopInfoDataAccess mPopInfoDA = new PopInfoDataAccess();
	}
}
