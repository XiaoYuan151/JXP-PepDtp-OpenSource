using System;
using System.Globalization;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000089 RID: 137
	public class MaxWidthConverter : IValueConverter
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x0002C42C File Offset: 0x0002A62C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double num;
			double num2;
			if (!double.TryParse((value != null) ? value.ToString() : null, out num) || !double.TryParse((parameter != null) ? parameter.ToString() : null, out num2) || num <= num2)
			{
				return 150;
			}
			return num - num2;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
