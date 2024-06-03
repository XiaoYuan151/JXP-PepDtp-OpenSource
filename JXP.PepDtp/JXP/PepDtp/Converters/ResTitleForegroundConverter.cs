using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000096 RID: 150
	public class ResTitleForegroundConverter : IValueConverter
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x0002CC00 File Offset: 0x0002AE00
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush result = new SolidColorBrush(Color.FromRgb(51, 51, 51));
			string a = (value != null) ? value.ToString() : null;
			if (a == "70" || a == "90")
			{
				result = new SolidColorBrush(Color.FromRgb(204, 204, 204));
			}
			return result;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
