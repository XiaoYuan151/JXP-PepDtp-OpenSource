using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JXP.Threading;

namespace JXP.SpeechEvaluator.Utility.Download
{
	// Token: 0x02000039 RID: 57
	internal class DownloadManager
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00008D09 File Offset: 0x00006F09
		public static Task<string> DownloadAsync(string srcUrl, string downloadDir = null, bool ignoreCache = false)
		{
			return TaskEx.Run<string>(() => DownloadManager.Download(srcUrl, downloadDir, ignoreCache));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008D35 File Offset: 0x00006F35
		public static Task<string> DownloadStringAsync(string srcUrl, Encoding encoding = null)
		{
			return TaskEx.Run<string>(() => DownloadManager.DownloadString(srcUrl, encoding));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008D5C File Offset: 0x00006F5C
		public static string Download(string srcUrl, string downloadDir = null, bool ignoreCache = false)
		{
			if (string.IsNullOrWhiteSpace(srcUrl))
			{
				return string.Empty;
			}
			if (downloadDir == null)
			{
				downloadDir = EvaluatorContext.CreateDownloadDir(srcUrl);
			}
			string text = Path.Combine(downloadDir, UrlUtility.GetUrlFileName(srcUrl));
			if (ignoreCache)
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			else if (File.Exists(text))
			{
				return text;
			}
			string downloadingFile = text + ".part" + Guid.NewGuid().ToString("N");
			Stream destStream = null;
			string result;
			try
			{
				destStream = File.Open(downloadingFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
				destStream.Seek(0L, SeekOrigin.End);
				DownloadManager.InternalDownload(srcUrl, destStream);
				destStream.Close();
				if (!File.Exists(text))
				{
					File.Move(downloadingFile, text);
				}
				result = text;
			}
			finally
			{
				ActionUtility.IgnoreExp(new Action[]
				{
					delegate()
					{
						Stream destStream = destStream;
						if (destStream != null)
						{
							destStream.Close();
						}
						destStream = null;
					},
					delegate()
					{
						if (File.Exists(downloadingFile))
						{
							File.Delete(downloadingFile);
						}
					}
				});
			}
			return result;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008E6C File Offset: 0x0000706C
		public static string DownloadString(string srcUrl, Encoding encoding = null)
		{
			if (string.IsNullOrWhiteSpace(srcUrl))
			{
				return string.Empty;
			}
			encoding = (encoding ?? Encoding.UTF8);
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				DownloadManager.InternalDownload(srcUrl, memoryStream);
				using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008EE4 File Offset: 0x000070E4
		private static void InternalDownload(string srcUrl, Stream destStream)
		{
			HttpWebResponse httpResponse = null;
			Stream httpStream = null;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(srcUrl);
				httpWebRequest.AddRange(destStream.Position);
				httpWebRequest.Timeout = (int)TimeSpan.FromSeconds(5.0).TotalMilliseconds;
				httpWebRequest.ReadWriteTimeout = (int)TimeSpan.FromMinutes(1.0).TotalMilliseconds;
				httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				httpStream = httpResponse.GetResponseStream();
				byte[] array = new byte[Constants.DATA_1K];
				int count;
				while ((count = httpStream.Read(array, 0, array.Length)) > 0)
				{
					destStream.Write(array, 0, count);
				}
			}
			finally
			{
				ActionUtility.IgnoreExp(new Action[]
				{
					delegate()
					{
						Stream httpStream = httpStream;
						if (httpStream != null)
						{
							httpStream.Close();
						}
						httpStream = null;
					},
					delegate()
					{
						HttpWebResponse httpResponse = httpResponse;
						if (httpResponse != null)
						{
							httpResponse.Close();
						}
						httpResponse = null;
					}
				});
			}
		}
	}
}
