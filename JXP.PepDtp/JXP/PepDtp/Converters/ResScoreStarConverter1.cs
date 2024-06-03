using System;
using System.Globalization;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008A RID: 138
	public class ResScoreStarConverter1 : IValueConverter
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0002C47C File Offset: 0x0002A67C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			decimal num;
			if (value == null || !decimal.TryParse(value.ToString(), out num) || num < 0m)
			{
				num = 0m;
			}
			decimal num2 = num;
			if (0m == num2)
			{
				return 0;
			}
			if (0m < num2 && num2 <= 10m)
			{
				return 0.5;
			}
			if (10m < num2 && num2 <= 20m)
			{
				return 1;
			}
			if (20m < num2 && num2 <= 30m)
			{
				return 1.5;
			}
			if (30m < num2 && num2 <= 40m)
			{
				return 2;
			}
			if (40m < num2 && num2 <= 50m)
			{
				return 2.5;
			}
			if (50m < num2 && num2 <= 60m)
			{
				return 3;
			}
			if (60m < num2 && num2 <= 70m)
			{
				return 3.5;
			}
			if (70m < num2 && num2 <= 80m)
			{
				return 4;
			}
			if (80m < num2 && num2 <= 90m)
			{
				return 4.5;
			}
			return 5;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
