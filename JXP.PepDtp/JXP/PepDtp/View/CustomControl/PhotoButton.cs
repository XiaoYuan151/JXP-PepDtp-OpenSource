using System;
using System.Windows;
using System.Windows.Controls;

namespace JXP.PepDtp.View.CustomControl
{
	// Token: 0x02000056 RID: 86
	public class PhotoButton : Button
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0001FD94 File Offset: 0x0001DF94
		static PhotoButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PhotoButton), new FrameworkPropertyMetadata(typeof(PhotoButton)));
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001FE1F File Offset: 0x0001E01F
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001FE31 File Offset: 0x0001E031
		public string RelativeUrl
		{
			get
			{
				return (string)base.GetValue(PhotoButton.RelativeUrlProperty);
			}
			set
			{
				base.SetValue(PhotoButton.RelativeUrlProperty, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001FE3F File Offset: 0x0001E03F
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001FE51 File Offset: 0x0001E051
		public Visibility DefaultVisibility
		{
			get
			{
				return (Visibility)base.GetValue(PhotoButton.DefaultVisibilityProperty);
			}
			set
			{
				base.SetValue(PhotoButton.DefaultVisibilityProperty, value);
			}
		}

		// Token: 0x0400030D RID: 781
		public static readonly DependencyProperty RelativeUrlProperty = DependencyProperty.Register("RelativeUrl", typeof(string), typeof(PhotoButton), new PropertyMetadata(""));

		// Token: 0x0400030E RID: 782
		public static readonly DependencyProperty DefaultVisibilityProperty = DependencyProperty.Register("DefaultVisibility", typeof(Visibility), typeof(PhotoButton), new PropertyMetadata(Visibility.Visible));
	}
}
