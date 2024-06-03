using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using JXP.Logs;
using JXP.Networks;
using JXP.OsInfo;
using JXP.PepDtp.Common;
using JXP.PepUtility;
using JXP.Utilities;

namespace JXP.PepDtp.PepClass
{
	// Token: 0x0200007E RID: 126
	public class PepClassService
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0002AC7B File Offset: 0x00028E7B
		public static string GetPepServerPath()
		{
			return AppDomain.CurrentDomain.BaseDirectory + PepClassService.ThirdLib_PepServer + "\\pepserver.exe";
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002AC96 File Offset: 0x00028E96
		public static string GetPepServerDirPath()
		{
			return AppDomain.CurrentDomain.BaseDirectory + PepClassService.ThirdLib_PepServer;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002ACAC File Offset: 0x00028EAC
		public static string GetPepServerConfigPath()
		{
			return AppDomain.CurrentDomain.BaseDirectory + PepClassService.ThirdLib_PepServer + "\\conf\\pepserver.conf";
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		public static bool StartPepServer()
		{
			string text = PepClassService.GetPepServerPath();
			if (!File.Exists(text))
			{
				LogHelper.Instance.Error("PepServer不存在。");
				return false;
			}
			try
			{
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "jxp\\" + AppConsts.FOLDER_LINK_NAME);
				DiskJunctionHelper.Create(text2, AppDomain.CurrentDomain.BaseDirectory, true);
				if (DiskJunctionHelper.Exists(text2))
				{
					string text3 = Path.Combine(text2, PepClassService.ThirdLib_PepServer + "\\pepserver.exe");
					if (File.Exists(text3))
					{
						text = text3;
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("创建映射文件夹失败:[{0}]。", arg));
			}
			try
			{
				if (!Environment.OSVersion.IsGreaterThanOrEqualTo(OsVersion.Win10))
				{
					if (Environment.OSVersion.IsGreaterThanOrEqualTo(OsVersion.Vista))
					{
						NetManager.Instance.InitializeFirewall("JXP.PepDtp", "JXP.PepDtp");
						FirewallHelper.CreateAppInBoundRule(text, "pepserver");
						FirewallHelper.CreateAppOutBoundRule(text, "pepserver");
					}
					else
					{
						FirewallHelper.CreateAppInBoundRule(text, "pepserver");
					}
				}
			}
			catch (Exception arg2)
			{
				LogHelper.Instance.Error(string.Format("把PepServer加入防火墙时失败了.[Reason:{0}]", arg2));
			}
			PepClassService.StopPepServer();
			try
			{
				for (int i = 1; i <= 3; i++)
				{
					try
					{
						ProcessHelper.StartWithoutWindow(text, string.Empty, string.Empty, Path.GetDirectoryName(text));
						Thread.Sleep(500);
						Process[] processesByName = Process.GetProcessesByName("pepserver");
						if (processesByName != null && processesByName.Length != 0)
						{
							break;
						}
					}
					catch
					{
						if (i == 3)
						{
							throw;
						}
					}
				}
			}
			catch (Exception arg3)
			{
				LogHelper.Instance.Error(string.Format("PepServer启动过程中 发生错误。Reason:[{0}]", arg3));
				return false;
			}
			return true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002AE88 File Offset: 0x00029088
		public static void StopPepServer()
		{
			try
			{
				string pepServerPath = PepClassService.GetPepServerPath();
				if (!File.Exists(pepServerPath))
				{
					LogHelper.Instance.Error("PepServer不存在。");
				}
				else
				{
					ProcessHelper.StartWithoutWindow(pepServerPath, " -s stop", string.Empty, Path.GetDirectoryName(pepServerPath));
					foreach (Process process in Process.GetProcessesByName("pepserver"))
					{
						try
						{
							process.Kill();
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("PepServer停止过程中 发生错误。Reason:[{0}]", arg));
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002AF2C File Offset: 0x0002912C
		private static void ModifyPepServerConfg()
		{
			try
			{
				int randomAvailablePort = IpAddressHelper.GetRandomAvailablePort(29931);
				string pepServerConfigPath = PepClassService.GetPepServerConfigPath();
				if (File.Exists(pepServerConfigPath))
				{
					List<string> list = new List<string>();
					foreach (string text in File.ReadAllLines(pepServerConfigPath))
					{
						if (text.Contains("#document_port#"))
						{
							text = string.Format("listen {0};#document_port#", randomAvailablePort);
						}
						list.Add(text);
					}
					Encoding encoding = new UTF8Encoding(false);
					File.WriteAllLines(pepServerConfigPath, list, encoding);
					SdkConstants.YZ_SERVER_PORT = randomAvailablePort.ToString();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("修改PepServer Config失败:[{0}]。", arg));
			}
		}

		// Token: 0x0400046B RID: 1131
		public const string FIREWALL_PEPSERVER = "pepserver";

		// Token: 0x0400046C RID: 1132
		public static readonly string ThirdLibBase = "thirdlib";

		// Token: 0x0400046D RID: 1133
		public static readonly string ThirdLib_PepServer = PepClassService.ThirdLibBase + "\\pepserver";
	}
}
