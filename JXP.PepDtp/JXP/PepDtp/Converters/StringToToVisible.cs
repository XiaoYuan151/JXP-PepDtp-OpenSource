using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x020000A0 RID: 160
	public class StringToToVisible : IValueConverter
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x0002D198 File Offset: 0x0002B398
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (string.IsNullOrEmpty((value != null) ? value.ToString() : null))
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
