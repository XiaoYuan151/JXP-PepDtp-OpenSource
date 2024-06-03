using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using JXP.Utilities;
using JXP.Windows;

namespace JXP.PepDtp.View
{
	// Token: 0x02000014 RID: 20
	public partial class BookTitleBar : WindowOverlay
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000A697 File Offset: 0x00008897
		public BookTitleBar()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A6A5 File Offset: 0x000088A5
		internal BookTitleBar SetInfo(string info)
		{
			this.txtInfo.Text = info;
			return this;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A6B4 File Offset: 0x000088B4
		protected override void OnContentRendered(EventArgs e)
		{
			this.RefreshPosition();
			base.OnContentRendered(e);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A6C4 File Offset: 0x000088C4
		protected override void RefreshPosition()
		{
			FrameworkElement placementTarget = base.PlacementTarget;
			if (placementTarget == null)
			{
				return;
			}
			Point point = placementTarget.PointToScreen(new Point(0.0, 0.0));
			point = VisualHelper.GetTransformFromDevice(placementTarget).Transform(point);
			base.Width = placementTarget.ActualWidth;
			base.Left = point.X;
			base.Top = point.Y;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A730 File Offset: 0x00008930
		private void BtnCollapse_OnClick(object sender, RoutedEventArgs e)
		{
			this.btnCollapse.Visibility = Visibility.Hidden;
			this.btnExpand.Visibility = Visibility.Hidden;
			this.CollapseAnimation();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A750 File Offset: 0x00008950
		private void BtnExpand_OnClick(object sender, RoutedEventArgs e)
		{
			this.btnCollapse.Visibility = Visibility.Hidden;
			this.btnExpand.Visibility = Visibility.Hidden;
			this.ExpandAnimation();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A770 File Offset: 0x00008970
		private void CollapseAnimation()
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, this.infoArea.ActualWidth, TimeSpan.FromSeconds(1.0));
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.(TranslateTransform.X)", new object[0]));
			storyboard.Children.Add(doubleAnimation);
			storyboard.Completed += delegate(object sender, EventArgs e)
			{
				this.infoArea.Visibility = Visibility.Collapsed;
				this.btnExpand.Visibility = Visibility.Visible;
			};
			storyboard.Begin(this.infoArea);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A7F0 File Offset: 0x000089F0
		private void ExpandAnimation()
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation(base.ActualWidth - 30.0, 0.0, TimeSpan.FromSeconds(1.0));
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.(TranslateTransform.X)", new object[0]));
			storyboard.Children.Add(doubleAnimation);
			storyboard.Completed += delegate(object sender, EventArgs e)
			{
				this.btnCollapse.Visibility = Visibility.Visible;
			};
			storyboard.Begin(this.infoArea);
			this.infoArea.Visibility = Visibility.Visible;
		}
	}
}
