using System;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace JXP.PepDtp.View.CustomControl
{
	// Token: 0x02000059 RID: 89
	public class WatermarkTextBoxWithErrorInfo : WatermarkTextBox
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000201E0 File Offset: 0x0001E3E0
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000201F2 File Offset: 0x0001E3F2
		public bool HasError
		{
			get
			{
				return (bool)base.GetValue(WatermarkTextBoxWithErrorInfo.HasErrorProperty);
			}
			set
			{
				base.SetValue(WatermarkTextBoxWithErrorInfo.HasErrorProperty, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00020205 File Offset: 0x0001E405
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00020217 File Offset: 0x0001E417
		public string ErrorMessage
		{
			get
			{
				return (string)base.GetValue(WatermarkTextBoxWithErrorInfo.ErrorMessageProperty);
			}
			set
			{
				base.SetValue(WatermarkTextBoxWithErrorInfo.ErrorMessageProperty, value);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00020225 File Offset: 0x0001E425
		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			if (this.HasError)
			{
				base.Text = string.Empty;
			}
			this.HasError = false;
		}

		// Token: 0x04000315 RID: 789
		public static readonly DependencyProperty HasErrorProperty = DependencyProperty.Register("HasError", typeof(bool), typeof(WatermarkTextBoxWithErrorInfo), new UIPropertyMetadata(false));

		// Token: 0x04000316 RID: 790
		public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(WatermarkTextBoxWithErrorInfo), new UIPropertyMetadata(string.Empty));
	}
}
