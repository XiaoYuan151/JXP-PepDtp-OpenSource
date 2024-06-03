using System;
using System.Globalization;
using System.Windows.Data;
using JXP.PepData;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000091 RID: 145
	public class XDConverter : IValueConverter
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0002CA44 File Offset: 0x0002AC44
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result = "全部";
			if (value == null)
			{
				return result;
			}
			string text = value.ToString();
			if (text == "0")
			{
				return result;
			}
			return new ConstantsDataAccess().GetContentEnum(1007, text, "", false);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
