using System;
using System.Windows;
using System.Windows.Controls;

namespace JXP.PepDtp.View.CustomControl
{
	// Token: 0x02000058 RID: 88
	public class TRadioButton : RadioButton
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00020074 File Offset: 0x0001E274
		static TRadioButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TRadioButton), new FrameworkPropertyMetadata(typeof(TRadioButton)));
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00020158 File Offset: 0x0001E358
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0002016A File Offset: 0x0001E36A
		public string Image
		{
			get
			{
				return (string)base.GetValue(TRadioButton.ImageProperty);
			}
			set
			{
				base.SetValue(TRadioButton.ImageProperty, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00020178 File Offset: 0x0001E378
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0002018A File Offset: 0x0001E38A
		public string CheckedImage
		{
			get
			{
				return (string)base.GetValue(TRadioButton.CheckedImageProperty);
			}
			set
			{
				base.SetValue(TRadioButton.CheckedImageProperty, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00020198 File Offset: 0x0001E398
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000201AA File Offset: 0x0001E3AA
		public string UnCheckedImage
		{
			get
			{
				return (string)base.GetValue(TRadioButton.UnCheckedImageProperty);
			}
			set
			{
				base.SetValue(TRadioButton.UnCheckedImageProperty, value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000201B8 File Offset: 0x0001E3B8
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000201CA File Offset: 0x0001E3CA
		public string Text
		{
			get
			{
				return (string)base.GetValue(TRadioButton.TextProperty);
			}
			set
			{
				base.SetValue(TRadioButton.TextProperty, value);
			}
		}

		// Token: 0x04000311 RID: 785
		public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(string), typeof(TRadioButton), new PropertyMetadata(""));

		// Token: 0x04000312 RID: 786
		public static readonly DependencyProperty CheckedImageProperty = DependencyProperty.Register("CheckedImage", typeof(string), typeof(TRadioButton), new PropertyMetadata(""));

		// Token: 0x04000313 RID: 787
		public static readonly DependencyProperty UnCheckedImageProperty = DependencyProperty.Register("UnCheckedImage", typeof(string), typeof(TRadioButton), new PropertyMetadata(""));

		// Token: 0x04000314 RID: 788
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TRadioButton), new PropertyMetadata(""));
	}
}
