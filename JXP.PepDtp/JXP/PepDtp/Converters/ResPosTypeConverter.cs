using System;
using System.Globalization;
using System.Windows.Data;
using JXP.Logs;

namespace JXP.PepDtp.Converters
{
	// Token: 0x020000A2 RID: 162
	public class ResPosTypeConverter : IValueConverter
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result = string.Empty;
			try
			{
				if (value != null)
				{
					switch ((int)value)
					{
					case 1:
						result = "本地";
						break;
					case 2:
						result = "云端";
						break;
					case 3:
						result = "两端";
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("资源位置类型转换（ResPosTypeConverter）Convert出错：" + ex.Message);
			}
			return result;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002D260 File Offset: 0x0002B460
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int num = 0;
			try
			{
				if (value != null)
				{
					string a = (string)value;
					if (!(a == "本地"))
					{
						if (!(a == "云端"))
						{
							if (a == "两端")
							{
								num = 3;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						num = 1;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("资源位置类型转换（ResPosTypeConverter）ConvertBack出错：" + ex.Message);
			}
			return num;
		}
	}
}
