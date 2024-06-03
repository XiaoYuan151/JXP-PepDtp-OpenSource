using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009B RID: 155
	public class RevertBoolToVisible : IValueConverter
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Visibility.Visible;
			}
			bool flag;
			if (!bool.TryParse(value.ToString(), out flag) || !flag)
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
