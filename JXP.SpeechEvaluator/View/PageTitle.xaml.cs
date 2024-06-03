using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x02000010 RID: 16
	public partial class PageTitle : UserControl
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005847 File Offset: 0x00003A47
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005859 File Offset: 0x00003A59
		public Visibility ReturnVisibility
		{
			get
			{
				return (Visibility)base.GetValue(PageTitle.ReturnVisibilityProperty);
			}
			set
			{
				base.SetValue(PageTitle.ReturnVisibilityProperty, value);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000586C File Offset: 0x00003A6C
		private static void ReturnVisibility_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PageTitle)d).RaiseReturnVisibilityChanged(e);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000CE RID: 206 RVA: 0x0000587A File Offset: 0x00003A7A
		// (remove) Token: 0x060000CF RID: 207 RVA: 0x00005888 File Offset: 0x00003A88
		public event RoutedEventHandler Return
		{
			add
			{
				base.AddHandler(PageTitle.ReturnEvent, value);
			}
			remove
			{
				base.RemoveHandler(PageTitle.ReturnEvent, value);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005898 File Offset: 0x00003A98
		private void RaiseReturnEvent()
		{
			RoutedEventArgs e = new RoutedEventArgs(PageTitle.ReturnEvent);
			base.RaiseEvent(e);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000D1 RID: 209 RVA: 0x000058B7 File Offset: 0x00003AB7
		// (remove) Token: 0x060000D2 RID: 210 RVA: 0x000058C5 File Offset: 0x00003AC5
		public event RoutedEventHandler Close
		{
			add
			{
				base.AddHandler(PageTitle.CloseEvent, value);
			}
			remove
			{
				base.RemoveHandler(PageTitle.CloseEvent, value);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000058D4 File Offset: 0x00003AD4
		private void RaiseCloseEvent()
		{
			RoutedEventArgs e = new RoutedEventArgs(PageTitle.CloseEvent);
			base.RaiseEvent(e);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000058F3 File Offset: 0x00003AF3
		public PageTitle()
		{
			this.InitializeComponent();
			this.layoutRoot.DataContext = this;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000590D File Offset: 0x00003B0D
		internal void RaiseReturnVisibilityChanged(DependencyPropertyChangedEventArgs e)
		{
			this.btnReturn.Visibility = (Visibility)e.NewValue;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005926 File Offset: 0x00003B26
		private void BtnReturn_Click(object sender, RoutedEventArgs e)
		{
			this.RaiseReturnEvent();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000592E File Offset: 0x00003B2E
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			this.RaiseCloseEvent();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005936 File Offset: 0x00003B36
		private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				Window window = Window.GetWindow(this);
				if (window == null)
				{
					return;
				}
				window.DragMove();
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005A14 File Offset: 0x00003C14
		// Note: this type is marked as 'beforefieldinit'.
		static PageTitle()
		{
			PageTitle.ReturnEvent = EventManager.RegisterRoutedEvent("Return", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PageTitle));
			PageTitle.CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PageTitle));
		}

		// Token: 0x04000053 RID: 83
		public static readonly DependencyProperty ReturnVisibilityProperty = DependencyProperty.Register("ReturnVisibility", typeof(Visibility), typeof(PageTitle), new PropertyMetadata(Visibility.Collapsed));
	}
}
