using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000085 RID: 133
	public class EncryResVisibilityConverter : IMultiValueConverter
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0002BFF4 File Offset: 0x0002A1F4
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				if (values == null || values.Length < 2)
				{
					result = Visibility.Visible;
				}
				else
				{
					object obj = values[0];
					string value = (obj != null) ? obj.ToString() : null;
					bool flag = (bool)values[1];
					if (!string.IsNullOrEmpty(value))
					{
						result = Visibility.Collapsed;
					}
					else if (flag)
					{
						result = Visibility.Collapsed;
					}
					else
					{
						result = Visibility.Visible;
					}
				}
			}
			catch (Exception)
			{
				result = Visibility.Visible;
			}
			return result;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
