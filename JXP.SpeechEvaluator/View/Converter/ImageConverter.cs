using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using JXP.Logs;
using JXP.SpeechEvaluator.Utility.Download;

namespace JXP.SpeechEvaluator.View.Converter
{
	// Token: 0x02000026 RID: 38
	public class ImageConverter : IValueConverter
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00006E2C File Offset: 0x0000502C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (value == null)
				{
					return null;
				}
				string text = DownloadManager.Download(value.ToString(), null, false);
				if (!File.Exists(text))
				{
					return null;
				}
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(text, UriKind.Absolute);
				bitmapImage.EndInit();
				return bitmapImage;
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
			return null;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006E9C File Offset: 0x0000509C
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
