using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using JXP.Models;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000093 RID: 147
	public class FailVisibleConverter : IValueConverter
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x0002CADB File Offset: 0x0002ACDB
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((TransferState)value != TransferState.Failed)
			{
				return Visibility.Hidden;
			}
			return Visibility.Visible;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
