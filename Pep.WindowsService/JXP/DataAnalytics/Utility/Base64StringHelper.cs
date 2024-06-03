using System;
using System.Security.Cryptography;
using System.Text;
using JXP.DataAnalytics.Debugging;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000024 RID: 36
	internal class Base64StringHelper
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003B82 File Offset: 0x00001D82
		internal static string Encode(string plainText)
		{
			if (string.IsNullOrWhiteSpace(plainText))
			{
				return plainText;
			}
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003BA0 File Offset: 0x00001DA0
		internal static string Decode(string encodedText)
		{
			if (string.IsNullOrWhiteSpace(encodedText))
			{
				return encodedText;
			}
			try
			{
				byte[] bytes = Convert.FromBase64String(encodedText);
				return Encoding.UTF8.GetString(bytes);
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			return string.Empty;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003BEC File Offset: 0x00001DEC
		internal static string StringEncrypt(string plainString)
		{
			string result;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					string cipher_KEY = Base64StringHelper.CIPHER_KEY;
					byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(cipher_KEY));
					tripleDESCryptoServiceProvider.Key = key;
					tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
					byte[] bytes = Encoding.UTF8.GetBytes(plainString);
					result = Convert.ToBase64String(tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
				}
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003C88 File Offset: 0x00001E88
		internal static string StringDecrypt(string cipherString)
		{
			string @string;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					string cipher_KEY = Base64StringHelper.CIPHER_KEY;
					byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(cipher_KEY));
					tripleDESCryptoServiceProvider.Key = key;
					tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
					byte[] array = Convert.FromBase64String(cipherString);
					@string = Encoding.UTF8.GetString(tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length));
				}
			}
			return @string;
		}

		// Token: 0x0400004F RID: 79
		private static readonly string CIPHER_KEY = "tY.Ao34-6b";
	}
}
