using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000099 RID: 153
	public class ResApprovalStatusColorMultiConverter : IMultiValueConverter
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush result = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 26, 132, 89));
			if (values == null || values.Length != 3)
			{
				return result;
			}
			string a = values[0].ToString();
			string a2 = values[1].ToString();
			string a3 = values[2].ToString();
			if (a3 == "90")
			{
				return new SolidColorBrush(Color.FromArgb(byte.MaxValue, 254, 139, 144));
			}
			if (a == "0" || a2 == "1")
			{
				return result;
			}
			if (!(a3 == "90"))
			{
				if (!(a3 == "99") && !(a3 == "100"))
				{
					if (!(a3 == "110"))
					{
					}
				}
				else
				{
					result = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 26, 132, 89));
				}
			}
			else
			{
				result = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 254, 139, 144));
			}
			return result;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
