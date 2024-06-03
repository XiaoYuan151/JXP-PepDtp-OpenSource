using System;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace JXP.PepDtp.Common
{
	// Token: 0x020000A7 RID: 167
	public static class PasswordBoxHelper
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0002DBFF File Offset: 0x0002BDFF
		public static string GetPassword(DependencyObject obj)
		{
			return (string)obj.GetValue(PasswordBoxHelper.PasswordProperty);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002DC11 File Offset: 0x0002BE11
		public static void SetPassword(DependencyObject obj, string value)
		{
			obj.SetValue(PasswordBoxHelper.PasswordProperty, value);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002DC20 File Offset: 0x0002BE20
		private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WatermarkPasswordBox watermarkPasswordBox = d as WatermarkPasswordBox;
			if (watermarkPasswordBox != null)
			{
				watermarkPasswordBox.PasswordChanged -= PasswordBoxHelper.PasswordBox_PasswordChanged;
				if (!PasswordBoxHelper.GetIsUpdating(watermarkPasswordBox))
				{
					watermarkPasswordBox.Password = (string)e.NewValue;
				}
				watermarkPasswordBox.PasswordChanged += PasswordBoxHelper.PasswordBox_PasswordChanged;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002DC78 File Offset: 0x0002BE78
		private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			WatermarkPasswordBox watermarkPasswordBox = sender as WatermarkPasswordBox;
			if (watermarkPasswordBox != null)
			{
				PasswordBoxHelper.SetIsUpdating(watermarkPasswordBox, true);
				PasswordBoxHelper.SetPassword(watermarkPasswordBox, watermarkPasswordBox.Password);
				PasswordBoxHelper.SetIsUpdating(watermarkPasswordBox, false);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002DCA9 File Offset: 0x0002BEA9
		public static bool GetAttach(DependencyObject obj)
		{
			return (bool)obj.GetValue(PasswordBoxHelper.AttachProperty);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002DCBB File Offset: 0x0002BEBB
		public static void SetAttach(DependencyObject obj, bool value)
		{
			obj.SetValue(PasswordBoxHelper.AttachProperty, value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002DCD0 File Offset: 0x0002BED0
		private static void Attach(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WatermarkPasswordBox watermarkPasswordBox = d as WatermarkPasswordBox;
			if (watermarkPasswordBox == null)
			{
				return;
			}
			if ((bool)e.OldValue)
			{
				watermarkPasswordBox.PasswordChanged -= PasswordBoxHelper.PasswordBox_PasswordChanged;
			}
			if ((bool)e.NewValue)
			{
				watermarkPasswordBox.PasswordChanged += PasswordBoxHelper.PasswordBox_PasswordChanged;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002DD28 File Offset: 0x0002BF28
		public static bool GetIsUpdating(DependencyObject obj)
		{
			return (bool)obj.GetValue(PasswordBoxHelper.IsUpdatingProperty);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002DD3A File Offset: 0x0002BF3A
		public static void SetIsUpdating(DependencyObject obj, bool value)
		{
			obj.SetValue(PasswordBoxHelper.IsUpdatingProperty, value);
		}

		// Token: 0x040004A0 RID: 1184
		public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(string.Empty, new PropertyChangedCallback(PasswordBoxHelper.OnPasswordPropertyChanged)));

		// Token: 0x040004A1 RID: 1185
		public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, new PropertyChangedCallback(PasswordBoxHelper.Attach)));

		// Token: 0x040004A2 RID: 1186
		public static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper));
	}
}
