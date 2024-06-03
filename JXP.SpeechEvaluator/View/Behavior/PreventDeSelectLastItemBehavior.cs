using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace JXP.SpeechEvaluator.View.Behavior
{
	// Token: 0x02000027 RID: 39
	public class PreventDeSelectLastItemBehavior : Behavior<ListBox>
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00006EA7 File Offset: 0x000050A7
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.PreviewMouseLeftButtonDown += this.AssociatedObject_PreviewMouseLeftButtonDown;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006EC8 File Offset: 0x000050C8
		private void AssociatedObject_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			if (listBox != null && listBox.SelectedItems.Count == 1)
			{
				UIElement uielement = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItems[0]) as UIElement;
				if (uielement != null)
				{
					Point position = e.GetPosition(uielement);
					if (VisualTreeHelper.HitTest(uielement, position) != null)
					{
						e.Handled = true;
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006F25 File Offset: 0x00005125
		protected override void OnDetaching()
		{
			base.OnDetaching();
			base.AssociatedObject.PreviewMouseLeftButtonDown -= this.AssociatedObject_PreviewMouseLeftButtonDown;
		}
	}
}
