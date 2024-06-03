using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;

namespace JXP.SpeechEvaluator.View.Behavior
{
	// Token: 0x02000029 RID: 41
	public class ScrollIntoViewBehavior : Behavior<ListBox>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00006FE3 File Offset: 0x000051E3
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007004 File Offset: 0x00005204
		private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListBox)
			{
				ListBox listBox = sender as ListBox;
				if (listBox.SelectedItem != null)
				{
					listBox.Dispatcher.BeginInvoke(new Action(delegate()
					{
						if (listBox.SelectedItem != null)
						{
							ScrollIntoViewBehavior.ScrollToCenterOfView(listBox, listBox.SelectedItem);
						}
					}), new object[0]);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000705B File Offset: 0x0000525B
		protected override void OnDetaching()
		{
			base.OnDetaching();
			base.AssociatedObject.SelectionChanged -= this.AssociatedObject_SelectionChanged;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000707C File Offset: 0x0000527C
		public static void ScrollToCenterOfView(ItemsControl itemsControl, object item)
		{
			if (!ScrollIntoViewBehavior.TryScrollToCenterOfView(itemsControl, item))
			{
				if (itemsControl is ListBox)
				{
					((ListBox)itemsControl).ScrollIntoView(item);
				}
				itemsControl.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate()
				{
					ScrollIntoViewBehavior.TryScrollToCenterOfView(itemsControl, item);
				}));
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000070F4 File Offset: 0x000052F4
		private static bool TryScrollToCenterOfView(ItemsControl itemsControl, object item)
		{
			UIElement uielement = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
			if (uielement == null)
			{
				return false;
			}
			ScrollContentPresenter scrollContentPresenter = null;
			Visual visual = uielement;
			while (visual != null && visual != itemsControl && (scrollContentPresenter = (visual as ScrollContentPresenter)) == null)
			{
				visual = (VisualTreeHelper.GetParent(visual) as Visual);
			}
			if (scrollContentPresenter == null)
			{
				return false;
			}
			IScrollInfo scrollInfo;
			if (scrollContentPresenter.CanContentScroll)
			{
				if ((scrollInfo = (scrollContentPresenter.Content as IScrollInfo)) == null)
				{
					scrollInfo = ((ScrollIntoViewBehavior.FirstVisualChild(scrollContentPresenter.Content as ItemsPresenter) as IScrollInfo) ?? scrollContentPresenter);
				}
			}
			else
			{
				IScrollInfo scrollInfo2 = scrollContentPresenter;
				scrollInfo = scrollInfo2;
			}
			IScrollInfo scrollInfo3 = scrollInfo;
			Size renderSize = uielement.RenderSize;
			Point point = uielement.TransformToAncestor((Visual)scrollInfo3).Transform(new Point(renderSize.Width / 2.0, renderSize.Height / 2.0));
			point.Y += scrollInfo3.VerticalOffset;
			point.X += scrollInfo3.HorizontalOffset;
			if (scrollInfo3 is StackPanel || scrollInfo3 is VirtualizingStackPanel)
			{
				double num = (double)itemsControl.ItemContainerGenerator.IndexFromContainer(uielement) + 0.5;
				if (((scrollInfo3 is StackPanel) ? ((StackPanel)scrollInfo3).Orientation : ((VirtualizingStackPanel)scrollInfo3).Orientation) == Orientation.Horizontal)
				{
					point.X = num;
				}
				else
				{
					point.Y = num;
				}
			}
			if (scrollInfo3.CanVerticallyScroll)
			{
				scrollInfo3.SetVerticalOffset(ScrollIntoViewBehavior.CenteringOffset(point.Y, scrollInfo3.ViewportHeight, scrollInfo3.ExtentHeight));
			}
			if (scrollInfo3.CanHorizontallyScroll)
			{
				scrollInfo3.SetHorizontalOffset(ScrollIntoViewBehavior.CenteringOffset(point.X, scrollInfo3.ViewportWidth, scrollInfo3.ExtentWidth));
			}
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007294 File Offset: 0x00005494
		private static double CenteringOffset(double center, double viewport, double extent)
		{
			return Math.Min(extent - viewport, Math.Max(0.0, center - viewport / 2.0));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000072B9 File Offset: 0x000054B9
		private static DependencyObject FirstVisualChild(Visual visual)
		{
			if (visual == null)
			{
				return null;
			}
			if (VisualTreeHelper.GetChildrenCount(visual) == 0)
			{
				return null;
			}
			return VisualTreeHelper.GetChild(visual, 0);
		}
	}
}
