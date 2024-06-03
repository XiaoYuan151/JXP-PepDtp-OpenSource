using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009D RID: 157
	public class ResMulti3BoolToVisible : IMultiValueConverter
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0002CFB4 File Offset: 0x0002B1B4
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				if (values == null || values.Length < 3)
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
							object obj3 = values[2];
							bool flag3;
							if (bool.TryParse((obj3 != null) ? obj3.ToString() : null, out flag3))
							{
								if (flag || flag2 || flag3)
								{
									return Visibility.Visible;
								}
								return Visibility.Collapsed;
							}
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

		// Token: 0x0600098E RID: 2446 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
