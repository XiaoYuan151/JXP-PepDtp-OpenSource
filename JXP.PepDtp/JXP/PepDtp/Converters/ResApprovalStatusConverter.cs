using System;
using System.Globalization;
using System.Windows.Data;
using JXP.Models;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000097 RID: 151
	public class ResApprovalStatusConverter : IValueConverter
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0002CC60 File Offset: 0x0002AE60
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is UserResourceModel))
			{
				return string.Empty;
			}
			UserResourceModel userResourceModel = (UserResourceModel)value;
			if (userResourceModel.RangeType == "0" || userResourceModel.PosType == 1 || userResourceModel.Ex1 == "本地")
			{
				return string.Empty;
			}
			string result = string.Empty;
			int state = userResourceModel.State;
			if (state != 90)
			{
				if (state - 99 > 1)
				{
					if (state != 110)
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

		// Token: 0x0600097C RID: 2428 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
