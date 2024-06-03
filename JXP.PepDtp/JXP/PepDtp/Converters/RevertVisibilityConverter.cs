using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008C RID: 140
	[ValueConversion(typeof(Visibility), typeof(Visibility))]
	public class RevertVisibilityConverter : IValueConverter
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0002C6A4 File Offset: 0x0002A8A4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Visibility)value == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400048E RID: 1166
		public static RevertVisibilityConverter Instance = new RevertVisibilityConverter();
	}
}
