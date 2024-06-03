using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008F RID: 143
	public class StateToVisibleConverter : IMultiValueConverter
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x0002C844 File Offset: 0x0002AA44
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || values == null || values.Length < 7)
			{
				return Visibility.Collapsed;
			}
			int num = 0;
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			bool flag5;
			bool flag6;
			bool flag7;
			if (!int.TryParse(parameter.ToString(), out num) || !bool.TryParse(values[0].ToString(), out flag) || !bool.TryParse(values[1].ToString(), out flag2) || !bool.TryParse(values[2].ToString(), out flag3) || !bool.TryParse(values[3].ToString(), out flag4) || !bool.TryParse(values[4].ToString(), out flag5) || !bool.TryParse(values[5].ToString(), out flag6) || !bool.TryParse(values[6].ToString(), out flag7))
			{
				return Visibility.Collapsed;
			}
			if (num == 0)
			{
				if (flag6)
				{
					return Visibility.Collapsed;
				}
				if (flag4 && !flag3)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
			else if (num == 1)
			{
				if (flag6)
				{
					return Visibility.Collapsed;
				}
				if (flag4)
				{
					return Visibility.Collapsed;
				}
				if (flag && !flag3)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
			else if (num == 2)
			{
				if (flag6)
				{
					if (flag7)
					{
						return Visibility.Collapsed;
					}
					return Visibility.Visible;
				}
				else
				{
					if (flag4)
					{
						return Visibility.Collapsed;
					}
					if (flag2 && !flag3)
					{
						return Visibility.Visible;
					}
					return Visibility.Collapsed;
				}
			}
			else if (num == 3)
			{
				if (flag6)
				{
					return Visibility.Collapsed;
				}
				if (flag4)
				{
					return Visibility.Collapsed;
				}
				if (flag5 && !flag3)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
			else if (num == 4)
			{
				if (flag6)
				{
					return Visibility.Collapsed;
				}
				if (flag3)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
			else
			{
				if (num != 5)
				{
					return Visibility.Collapsed;
				}
				if (flag6 && flag7)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
