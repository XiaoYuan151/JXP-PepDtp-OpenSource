using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009F RID: 159
	public class ResMultiBoolToVisible1 : IMultiValueConverter
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				if (values == null || values.Length < 2)
				{
					result = Visibility.Collapsed;
				}
				else
				{
					object obj = values[0];
					bool flag;
					if (bool.TryParse((obj != null) ? obj.ToString() : null, out flag))
					{
						object obj2 = values[1];
						bool flag2;
						if (bool.TryParse((obj2 != null) ? obj2.ToString() : null, out flag2))
						{
							if (parameter != null && !(parameter.ToString() == "0"))
							{
								return Visibility.Collapsed;
							}
							if (flag && !flag2)
							{
								return Visibility.Visible;
							}
							return Visibility.Collapsed;
						}
					}
					result = Visibility.Collapsed;
				}
			}
			catch (Exception)
			{
				result = Visibility.Collapsed;
			}
			return result;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
