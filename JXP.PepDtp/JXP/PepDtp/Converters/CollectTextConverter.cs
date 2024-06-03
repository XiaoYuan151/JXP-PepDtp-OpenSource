using System;
using System.Globalization;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000083 RID: 131
	public class CollectTextConverter : IValueConverter
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0002BF74 File Offset: 0x0002A174
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string value2 = (value != null) ? value.ToString() : null;
			string value3 = (parameter != null) ? parameter.ToString() : null;
			if (string.IsNullOrEmpty(value2))
			{
				if (string.IsNullOrEmpty(value3))
				{
					return "";
				}
				return "收藏";
			}
			else
			{
				if (string.IsNullOrEmpty(value3))
				{
					return "";
				}
				return "取消收藏";
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
