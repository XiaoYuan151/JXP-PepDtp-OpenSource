using System;
using System.Globalization;
using System.Windows.Data;
using JXP.PepUtility;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008B RID: 139
	public class ResTitleConverter : IMultiValueConverter
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x0002C640 File Offset: 0x0002A840
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values != null && values.Length < 3)
			{
				return string.Empty;
			}
			object obj = values[0];
			string result = (obj != null) ? obj.ToString() : null;
			object obj2 = values[1];
			string text = (obj2 != null) ? obj2.ToString() : null;
			object obj3 = values[2];
			if (!(((obj3 != null) ? obj3.ToString() : null) == SdkConstants.NQ_RES_TYPE))
			{
				return result;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return result;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
