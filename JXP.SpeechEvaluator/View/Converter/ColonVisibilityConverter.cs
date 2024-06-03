using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JXP.SpeechEvaluator.View.Converter
{
	// Token: 0x02000025 RID: 37
	public class ColonVisibilityConverter : IValueConverter
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00006E02 File Offset: 0x00005002
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006E21 File Offset: 0x00005021
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
