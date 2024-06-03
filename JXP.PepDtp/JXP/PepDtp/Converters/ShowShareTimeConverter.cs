using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x020000A1 RID: 161
	public class ShowShareTimeConverter : IValueConverter
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0002D1BA File Offset: 0x0002B3BA
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (((value != null) ? value.ToString() : null) == "00")
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
