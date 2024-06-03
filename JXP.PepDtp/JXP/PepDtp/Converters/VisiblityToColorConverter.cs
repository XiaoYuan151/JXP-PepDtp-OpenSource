using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000090 RID: 144
	public class VisiblityToColorConverter : IValueConverter
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Color = Colors.Black;
			object result;
			try
			{
				if ((Visibility)value == Visibility.Visible)
				{
					solidColorBrush.Color = (Color)ColorConverter.ConvertFromString("#009999");
				}
				result = solidColorBrush;
			}
			catch
			{
				result = solidColorBrush;
			}
			return result;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
