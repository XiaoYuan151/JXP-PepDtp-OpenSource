using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009C RID: 156
	public class ResMultiBoolToVisible : IMultiValueConverter
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0002CF28 File Offset: 0x0002B128
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
							if (flag || flag2)
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

		// Token: 0x0600098B RID: 2443 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
