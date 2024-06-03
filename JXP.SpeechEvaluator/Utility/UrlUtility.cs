using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000038 RID: 56
	internal static class UrlUtility
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static string GenerateId(string srcUrl, bool useParentUrl = false)
		{
			if (string.IsNullOrWhiteSpace(srcUrl))
			{
				throw new ArgumentNullException("srcUrl");
			}
			if (useParentUrl)
			{
				srcUrl = UrlUtility.GetUrlParent(srcUrl);
			}
			int length;
			if ((length = srcUrl.LastIndexOf('?')) >= 0)
			{
				srcUrl = srcUrl.Substring(0, length);
			}
			srcUrl = srcUrl.ToLowerInvariant();
			string result;
			using (MD5 md = new MD5CryptoServiceProvider())
			{
				result = BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(srcUrl))).Replace("-", string.Empty).ToLowerInvariant();
			}
			return result;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008C84 File Offset: 0x00006E84
		public static string GetUrlParent(string srcUrl)
		{
			if (string.IsNullOrWhiteSpace(srcUrl))
			{
				throw new ArgumentNullException("srcUrl");
			}
			int num;
			if ((num = srcUrl.LastIndexOf('/')) >= 0)
			{
				srcUrl = srcUrl.Substring(0, num + 1);
			}
			return srcUrl.ToLowerInvariant();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008CC5 File Offset: 0x00006EC5
		public static string GetUrlFileName(string srcUrl)
		{
			return Path.GetFileName(new Uri(srcUrl).LocalPath);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008CD7 File Offset: 0x00006ED7
		public static bool IsHttpFile(string srcUrl)
		{
			if (string.IsNullOrWhiteSpace(srcUrl))
			{
				throw new ArgumentNullException("srcUrl");
			}
			return srcUrl.StartsWith("http:") || srcUrl.StartsWith("https:");
		}
	}
}
