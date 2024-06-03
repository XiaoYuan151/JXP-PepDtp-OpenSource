using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Utility;
using JXP.OsInfo;

namespace JXP.DataAnalytics.Network
{
	// Token: 0x0200003D RID: 61
	internal class HttpWebRequestHelper
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00006EF8 File Offset: 0x000050F8
		static HttpWebRequestHelper()
		{
			if (Environment.OSVersion.IsLessThan(OsVersion.Vista))
			{
				ServicePointManager.ServerCertificateValidationCallback = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006F20 File Offset: 0x00005120
		internal static string GetWebRequestResult(string requestUri, IDictionary<string, string> postData, int timeout = 10000)
		{
			HttpWebResponse httpWebResponse = null;
			Stream stream = null;
			StreamReader streamReader = null;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (postData != null)
				{
					bool flag = true;
					foreach (KeyValuePair<string, string> keyValuePair in postData)
					{
						if (flag)
						{
							flag = false;
							stringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(keyValuePair.Key), HttpUtility.UrlEncode(keyValuePair.Value));
						}
						else
						{
							stringBuilder.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(keyValuePair.Key), HttpUtility.UrlEncode(keyValuePair.Value));
						}
					}
				}
				HttpWebRequest httpWebRequest = HttpWebRequestHelper.CreatePostRequest(requestUri, stringBuilder.ToString(), timeout);
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				stream = httpWebRequest.GetResponse().GetResponseStream();
				if (stream == null)
				{
					return string.Empty;
				}
				streamReader = new StreamReader(stream, Encoding.UTF8);
				return streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			finally
			{
				try
				{
					if (streamReader != null)
					{
						streamReader.Close();
					}
					if (stream != null)
					{
						stream.Close();
					}
					if (httpWebResponse != null)
					{
						httpWebResponse.Close();
					}
				}
				catch
				{
				}
			}
			return string.Empty;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000706C File Offset: 0x0000526C
		private static HttpWebRequest CreatePostRequest(string requestUri, string postData, int timeout)
		{
			WebRequest webRequest = WebRequest.Create(requestUri);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Timeout = timeout;
			byte[] bytes = Encoding.UTF8.GetBytes(postData);
			webRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = webRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			return (HttpWebRequest)webRequest;
		}
	}
}
