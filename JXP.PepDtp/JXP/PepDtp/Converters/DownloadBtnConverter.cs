using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JXP.PepUtility;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008D RID: 141
	public class DownloadBtnConverter : IMultiValueConverter
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
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
					string a = (obj != null) ? obj.ToString() : null;
					string a2 = values[1].ToString();
					if ((bool)values[2])
					{
						result = Visibility.Collapsed;
					}
					else if (a == SdkConstants.NQ_RES_TYPE)
					{
						result = Visibility.Collapsed;
					}
					else if (a == SdkConstants.RJ_CLOUD_RES_TYPE)
					{
						if (a2 == "01" || a2 == "10")
						{
							result = Visibility.Visible;
						}
						else
						{
							result = Visibility.Collapsed;
						}
					}
					else
					{
						result = Visibility.Visible;
					}
				}
			}
			catch
			{
				result = Visibility.Collapsed;
			}
			return result;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
