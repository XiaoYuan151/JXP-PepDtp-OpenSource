using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.PepDtp.Converters
{
	// Token: 0x0200008E RID: 142
	public class ShowPageConverter : IMultiValueConverter
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0002C788 File Offset: 0x0002A988
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
					string text;
					if (obj == null)
					{
						text = null;
					}
					else
					{
						string text2 = obj.ToString();
						text = ((text2 != null) ? text2.ToLower() : null);
					}
					string a = text;
					if (a == ".doc" || a == ".docx" || a == ".ppt" || a == ".pptx")
					{
						object obj2 = values[1];
						int num;
						if (int.TryParse((obj2 != null) ? obj2.ToString() : null, out num) && num > 0)
						{
							return Visibility.Visible;
						}
					}
					result = Visibility.Collapsed;
				}
			}
			catch (Exception)
			{
				result = Visibility.Collapsed;
			}
			return result;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
