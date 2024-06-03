using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000004 RID: 4
	public class Md5Helper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002914 File Offset: 0x00000B14
		public static string GetFileMd5Value(string strFilePath)
		{
			string result;
			try
			{
				if (!File.Exists(strFilePath))
				{
					result = null;
				}
				else
				{
					using (FileStream fileStream = new FileStream(strFilePath, FileMode.Open))
					{
						byte[] array = new MD5CryptoServiceProvider().ComputeHash(fileStream);
						fileStream.Close();
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
						result = stringBuilder.ToString();
					}
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static string GetStringMd5Value(string strDataIn)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(strDataIn);
			byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
			md5CryptoServiceProvider.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X").PadLeft(2, '0'));
			}
			return stringBuilder.ToString();
		}
	}
}
