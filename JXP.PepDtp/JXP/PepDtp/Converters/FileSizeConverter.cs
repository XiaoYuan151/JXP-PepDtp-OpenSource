using System;
using System.Globalization;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000094 RID: 148
	public class FileSizeConverter : IValueConverter
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0002CAF4 File Offset: 0x0002ACF4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			long num;
			if (value == null || !long.TryParse(value.ToString(), out num))
			{
				return string.Empty;
			}
			double num2 = Math.Round((double)num / 1024.0, 0, MidpointRounding.AwayFromZero);
			if (num2 <= 1024.0)
			{
				return num2.ToString() + "KB";
			}
			double num3 = Math.Round(num2 / 1024.0, 2, MidpointRounding.AwayFromZero);
			if (num3 > 1024.0)
			{
				return Math.Round(num3 / 1024.0, 2, MidpointRounding.AwayFromZero).ToString() + "GB";
			}
			return num3.ToString() + "MB";
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
