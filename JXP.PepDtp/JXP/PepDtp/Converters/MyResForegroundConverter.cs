using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000095 RID: 149
	public class MyResForegroundConverter : IValueConverter
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0002CBA4 File Offset: 0x0002ADA4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush result = new SolidColorBrush(Color.FromRgb(85, 85, 85));
			if (value == null || (value.ToString() != "1" && value.ToString() != "01"))
			{
				return result;
			}
			return new SolidColorBrush(Color.FromRgb(26, 133, 91));
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
