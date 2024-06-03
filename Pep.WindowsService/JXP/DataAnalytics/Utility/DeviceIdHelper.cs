using System;
using System.IO;
using System.Text;
using JXP.DataAnalytics.Debugging;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000026 RID: 38
	internal class DeviceIdHelper
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003D40 File Offset: 0x00001F40
		internal static string GetId()
		{
			try
			{
				string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PepAnalytics", "1.0");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string path = Path.Combine(text, "DeviceId");
				string text2 = string.Empty;
				using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						text2 = streamReader.ReadToEnd();
					}
				}
				if (string.IsNullOrWhiteSpace(text2))
				{
					text2 = Guid.NewGuid().ToString("N");
					File.WriteAllText(path, text2);
				}
				return text2;
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			return string.Empty;
		}
	}
}
