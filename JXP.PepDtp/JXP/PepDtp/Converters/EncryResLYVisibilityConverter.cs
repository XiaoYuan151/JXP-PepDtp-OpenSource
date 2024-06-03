using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000084 RID: 132
	public class EncryResLYVisibilityConverter : IValueConverter
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0002BFCF File Offset: 0x0002A1CF
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!string.IsNullOrEmpty((value != null) ? value.ToString() : null))
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
