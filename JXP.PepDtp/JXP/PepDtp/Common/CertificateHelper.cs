using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using JXP.Logs;
using JXP.Threading;

namespace JXP.PepDtp.Common
{
	// Token: 0x020000A5 RID: 165
	public class CertificateHelper
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x0002D889 File Offset: 0x0002BA89
		public static void InstallGlobaSignAsync()
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GlobaSign", "GlobaSign.pfx");
					if (File.Exists(text))
					{
						X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
						x509Store.Open(OpenFlags.MaxAllowed);
						bool flag = false;
						foreach (string findValue in CertificateHelper.mLstGlobalSignName)
						{
							if (x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, findValue, false).Count == 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							X509Certificate2 certificate = new X509Certificate2(text, "1");
							x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
							x509Store.Open(OpenFlags.ReadWrite);
							x509Store.Remove(certificate);
							x509Store.Add(certificate);
							x509Store.Close();
						}
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("安装GlobaSign证书失败：[{0}]。", arg));
				}
			});
		}

		// Token: 0x0400049C RID: 1180
		private static List<string> mLstGlobalSignName = new List<string>
		{
			"CN=GlobalSign, O=GlobalSign, OU=GlobalSign Root CA - R3",
			"CN=GlobalSign Root CA, OU=Root CA, O=GlobalSign nv-sa, C=BE",
			"CN=GlobalSign, O=GlobalSign, OU=GlobalSign Root CA - R6",
			"CN=GlobalSign, O=GlobalSign, OU=GlobalSign Root CA - R2",
			"CN=GlobalSign, O=GlobalSign, OU=GlobalSign ECC Root CA - R5"
		};
	}
}
