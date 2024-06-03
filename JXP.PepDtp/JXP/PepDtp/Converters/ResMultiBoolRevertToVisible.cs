using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JXP.PepUtility;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200009E RID: 158
	public class ResMultiBoolRevertToVisible : IMultiValueConverter
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0002D058 File Offset: 0x0002B258
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
					if (!bool.TryParse((obj != null) ? obj.ToString() : null, out flag))
					{
						result = Visibility.Collapsed;
					}
					else if (flag)
					{
						result = Visibility.Collapsed;
					}
					else
					{
						object obj2 = values[1];
						if (((obj2 != null) ? obj2.ToString() : null) == SdkConstants.NQ_RES_TYPE)
						{
							result = Visibility.Collapsed;
						}
						else
						{
							result = Visibility.Visible;
						}
					}
				}
			}
			catch (Exception)
			{
				result = Visibility.Collapsed;
			}
			return result;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
