using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009A RID: 154
	public class ResBoolToVisible : IValueConverter
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Visibility.Collapsed;
			}
			bool flag;
			if (!bool.TryParse(value.ToString(), out flag) || !flag)
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
