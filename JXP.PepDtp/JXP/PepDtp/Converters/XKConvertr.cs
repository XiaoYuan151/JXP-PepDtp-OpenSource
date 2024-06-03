using System;
using System.Globalization;
using System.Windows.Data;
using JXP.PepData;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000092 RID: 146
	public class XKConvertr : IValueConverter
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x0002CA8C File Offset: 0x0002AC8C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result = "全部";
			if (value == null || ((value != null) ? value.ToString() : null) == "0")
			{
				return result;
			}
			string strValue = value.ToString();
			return new ConstantsDataAccess().GetContentEnum(1009, strValue, "", false);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
