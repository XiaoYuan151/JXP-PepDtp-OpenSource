using System;
using System.Windows;
using System.Windows.Controls;

namespace JXP.PepDtp.View.CustomControl
{
	// Token: 0x02000055 RID: 85
	public class InterImageButton : Button
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		static InterImageButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(InterImageButton), new FrameworkPropertyMetadata(typeof(InterImageButton)));
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001FD47 File Offset: 0x0001DF47
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001FD59 File Offset: 0x0001DF59
		public string Number
		{
			get
			{
				return (string)base.GetValue(InterImageButton.NumberProperty);
			}
			set
			{
				base.SetValue(InterImageButton.NumberProperty, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001FD67 File Offset: 0x0001DF67
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0001FD79 File Offset: 0x0001DF79
		public Visibility ShowNumber
		{
			get
			{
				return (Visibility)base.GetValue(InterImageButton.ShowNumberProperty);
			}
			set
			{
				base.SetValue(InterImageButton.ShowNumberProperty, value);
			}
		}

		// Token: 0x0400030B RID: 779
		public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(InterImageButton), new PropertyMetadata("0"));

		// Token: 0x0400030C RID: 780
		public static readonly DependencyProperty ShowNumberProperty = DependencyProperty.Register("ShowNumber", typeof(Visibility), typeof(InterImageButton), new PropertyMetadata(Visibility.Collapsed));
	}
}
