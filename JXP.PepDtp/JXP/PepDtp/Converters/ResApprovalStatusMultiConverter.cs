using System;
using System.Globalization;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000098 RID: 152
	public class ResApprovalStatusMultiConverter : IMultiValueConverter
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x0002CCE4 File Offset: 0x0002AEE4
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			string result = string.Empty;
			if (values == null || values.Length != 3)
			{
				return result;
			}
			object obj = values[0];
			string text = (obj != null) ? obj.ToString() : null;
			object obj2 = values[1];
			string a = (obj2 != null) ? obj2.ToString() : null;
			object obj3 = values[2];
			string a2 = (obj3 != null) ? obj3.ToString() : null;
			if (a2 == "90")
			{
				return "";
			}
			if (string.IsNullOrEmpty(text) || text == "0" || a == "1")
			{
				return result;
			}
			if (!(a2 == "90"))
			{
				if (!(a2 == "99") && !(a2 == "100"))
				{
					if (!(a2 == "110"))
					{
					}
				}
				else
				{
					result = "";
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
