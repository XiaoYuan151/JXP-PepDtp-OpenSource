using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using JXP.PepUtility;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000088 RID: 136
	public class LyTextConverter : IMultiValueConverter
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0002C348 File Offset: 0x0002A548
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || values.Length < 4)
			{
				return string.Empty;
			}
			object obj = values[0];
			string a = (obj != null) ? obj.ToString() : null;
			object obj2 = values[1];
			string a2 = (obj2 != null) ? obj2.ToString() : null;
			object obj3 = values[2];
			string value = (obj3 != null) ? obj3.ToString() : null;
			object obj4 = values[3];
			string value2 = (obj4 != null) ? obj4.ToString() : null;
			if (a == SdkConstants.RJ_CLOUD_RES_TYPE)
			{
				return "来源: 配套资源";
			}
			if (a == SdkConstants.NQ_RES_TYPE)
			{
				return "来源: 教材资源";
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (a2 == "1")
			{
				stringBuilder.Append("平台,");
			}
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("本校,");
			}
			stringBuilder.Append(value2);
			return "来源: " + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
