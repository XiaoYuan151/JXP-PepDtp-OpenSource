using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Win32;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000029 RID: 41
	public class NetFxVersion
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004494 File Offset: 0x00002694
		public static string GetInstalledFxVersion()
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder(2048);
				List<string> list = NetFxVersion.Get1To45VersionFromRegistry();
				if (list.Count > 0)
				{
					stringBuilder.Append(string.Join(",", list));
				}
				List<string> list2 = NetFxVersion.Get45PlusFromRegistry();
				if (list2.Count > 0)
				{
					if (list.Count > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(string.Join(",", list2));
				}
				return stringBuilder.ToString();
			}
			catch (Exception value)
			{
				Trace.WriteLine(value);
			}
			return string.Empty;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000452C File Offset: 0x0000272C
		private static List<string> Get1To45VersionFromRegistry()
		{
			List<string> list = new List<string>();
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\"))
			{
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (!(text == "v4") && text.StartsWith("v"))
					{
						RegistryKey registryKey2 = registryKey.OpenSubKey(text);
						string text2 = (string)registryKey2.GetValue("Version", "");
						string text3 = registryKey2.GetValue("SP", "").ToString();
						string text4 = registryKey2.GetValue("Install", "").ToString();
						if (string.IsNullOrEmpty(text4))
						{
							NetFxVersion.AddVersionToList(list, text2, "");
						}
						else if (!string.IsNullOrEmpty(text3) && text4 == "1")
						{
							NetFxVersion.AddVersionToList(list, text2, text3);
						}
						if (string.IsNullOrEmpty(text2))
						{
							foreach (string name in registryKey2.GetSubKeyNames())
							{
								RegistryKey registryKey3 = registryKey2.OpenSubKey(name);
								text2 = (string)registryKey3.GetValue("Version", "");
								if (!string.IsNullOrEmpty(text2))
								{
									text3 = registryKey3.GetValue("SP", "").ToString();
								}
								text4 = registryKey3.GetValue("Install", "").ToString();
								if (string.IsNullOrEmpty(text4))
								{
									NetFxVersion.AddVersionToList(list, text2, "");
								}
								else if (!string.IsNullOrEmpty(text3) && text4 == "1")
								{
									NetFxVersion.AddVersionToList(list, text2, text3);
								}
								else if (text4 == "1")
								{
									NetFxVersion.AddVersionToList(list, text2, "");
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000472C File Offset: 0x0000292C
		private static List<string> Get45PlusFromRegistry()
		{
			List<string> list = new List<string>();
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
			{
				if (registryKey == null)
				{
					return list;
				}
				if (registryKey.GetValue("Version") != null)
				{
					NetFxVersion.AddVersionToList(list, registryKey.GetValue("Version").ToString(), "");
				}
				else if (registryKey != null && registryKey.GetValue("Release") != null)
				{
					NetFxVersion.AddVersionToList(list, NetFxVersion.<Get45PlusFromRegistry>g__CheckFor45PlusVersion|2_0((int)registryKey.GetValue("Release")), "");
				}
			}
			return list;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000047D0 File Offset: 0x000029D0
		private static void AddVersionToList(List<string> list, string version, string sp = "")
		{
			if (string.IsNullOrWhiteSpace(version))
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(sp))
			{
				list.Add(version);
				return;
			}
			list.Add(version + " sp" + sp);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004808 File Offset: 0x00002A08
		[CompilerGenerated]
		internal static string <Get45PlusFromRegistry>g__CheckFor45PlusVersion|2_0(int releaseKey)
		{
			if (releaseKey >= 528040)
			{
				return "4.8";
			}
			if (releaseKey >= 461808)
			{
				return "4.7.2";
			}
			if (releaseKey >= 461308)
			{
				return "4.7.1";
			}
			if (releaseKey >= 460798)
			{
				return "4.7";
			}
			if (releaseKey >= 394802)
			{
				return "4.6.2";
			}
			if (releaseKey >= 394254)
			{
				return "4.6.1";
			}
			if (releaseKey >= 393295)
			{
				return "4.6";
			}
			if (releaseKey >= 379893)
			{
				return "4.5.2";
			}
			if (releaseKey >= 378675)
			{
				return "4.5.1";
			}
			if (releaseKey >= 378389)
			{
				return "4.5";
			}
			return "";
		}
	}
}
