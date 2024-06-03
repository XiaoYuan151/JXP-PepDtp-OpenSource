using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x020000A3 RID: 163
	public class ResScoreStarConverter : IValueConverter
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0002D2E4 File Offset: 0x0002B4E4
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
				return new Rect
				{
					X = 367.0,
					Y = 393.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (0m < num2 && num2 <= 10m)
			{
				return new Rect
				{
					X = 448.0,
					Y = 407.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (10m < num2 && num2 <= 20m)
			{
				return new Rect
				{
					X = 367.0,
					Y = 407.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (20m < num2 && num2 <= 30m)
			{
				return new Rect
				{
					X = 448.0,
					Y = 421.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (30m < num2 && num2 <= 40m)
			{
				return new Rect
				{
					X = 367.0,
					Y = 421.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (40m < num2 && num2 <= 50m)
			{
				return new Rect
				{
					X = 448.0,
					Y = 435.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (50m < num2 && num2 <= 60m)
			{
				return new Rect
				{
					X = 367.0,
					Y = 435.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (60m < num2 && num2 <= 70m)
			{
				return new Rect
				{
					X = 448.0,
					Y = 449.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (70m < num2 && num2 <= 80m)
			{
				return new Rect
				{
					X = 367.0,
					Y = 449.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			if (80m < num2 && num2 <= 90m)
			{
				return new Rect
				{
					X = 448.0,
					Y = 463.0,
					Width = 60.0,
					Height = 12.0
				};
			}
			return new Rect
			{
				X = 367.0,
				Y = 463.0,
				Width = 60.0,
				Height = 12.0
			};
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002D797 File Offset: 0x0002B997
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
