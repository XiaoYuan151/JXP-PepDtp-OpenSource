using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000086 RID: 134
	public class ImageConvert : IValueConverter
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0002C06C File Offset: 0x0002A26C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
				bitmapImage.CacheOption = BitmapCacheOption.None;
				bitmapImage.UriSource = new Uri((value != null) ? value.ToString() : null);
				bitmapImage.EndInit();
				result = bitmapImage;
			}
			catch (Exception)
			{
				result = ((value != null) ? value.ToString() : null);
			}
			return result;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
