using System;
using System.Linq;

namespace JXP.OsInfo
{
	// Token: 0x0200004C RID: 76
	public static class OsExtensions
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000078CC File Offset: 0x00005ACC
		public static int? GetServicePackVersion(this OperatingSystem os)
		{
			int value;
			if (int.TryParse(os.ServicePack.Replace("Service Pack ", string.Empty).Replace("SP", string.Empty).Trim(), out value))
			{
				return new int?(value);
			}
			return null;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000791B File Offset: 0x00005B1B
		public static bool Is64Bit(this OperatingSystem os)
		{
			return Environment.Is64BitOperatingSystem;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007924 File Offset: 0x00005B24
		public static bool IsEqualTo(this OperatingSystem os, OsVersion osVersion)
		{
			OsInfo osVersionInfo = OsExtensions.GetOsVersionInfo(osVersion);
			return OsExtensions.GetMajorMinorVersion(os.Version) == OsExtensions.GetMajorMinorVersion(osVersionInfo.OperatingSystem.Version);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007958 File Offset: 0x00005B58
		public static bool IsGreaterThan(this OperatingSystem os, OsVersion osVersion)
		{
			OsInfo osVersionInfo = OsExtensions.GetOsVersionInfo(osVersion);
			return OsExtensions.GetMajorMinorVersion(os.Version) > OsExtensions.GetMajorMinorVersion(osVersionInfo.OperatingSystem.Version);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000798C File Offset: 0x00005B8C
		public static bool IsGreaterThanOrEqualTo(this OperatingSystem os, OsVersion osVersion)
		{
			OsInfo osVersionInfo = OsExtensions.GetOsVersionInfo(osVersion);
			return OsExtensions.GetMajorMinorVersion(os.Version) >= OsExtensions.GetMajorMinorVersion(osVersionInfo.OperatingSystem.Version);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000079C0 File Offset: 0x00005BC0
		public static bool IsLessThan(this OperatingSystem os, OsVersion osVersion)
		{
			OsInfo osVersionInfo = OsExtensions.GetOsVersionInfo(osVersion);
			return OsExtensions.GetMajorMinorVersion(os.Version) < OsExtensions.GetMajorMinorVersion(osVersionInfo.OperatingSystem.Version);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000079F4 File Offset: 0x00005BF4
		public static bool IsLessThanOrEqualTo(this OperatingSystem os, OsVersion osVersion)
		{
			OsInfo osVersionInfo = OsExtensions.GetOsVersionInfo(osVersion);
			return OsExtensions.GetMajorMinorVersion(os.Version) <= OsExtensions.GetMajorMinorVersion(osVersionInfo.OperatingSystem.Version);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007A28 File Offset: 0x00005C28
		private static Version GetMajorMinorVersion(Version v)
		{
			return new Version(v.Major, v.Minor);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007A3C File Offset: 0x00005C3C
		private static OsInfo GetOsVersionInfo(OsVersion osVersion)
		{
			OsInfo osInfo = OsExtensions.Versions.FirstOrDefault((OsInfo v) => v.OsVersion == osVersion);
			if (osInfo == null)
			{
				throw new NotImplementedException("OSVersion:" + osVersion.ToString());
			}
			return osInfo;
		}

		// Token: 0x040000D0 RID: 208
		private static readonly OsInfo[] Versions = new OsInfo[]
		{
			new OsInfo(OsVersion.Win32S, PlatformID.Win32S, 0, 0),
			new OsInfo(OsVersion.Win95, PlatformID.Win32Windows, 4, 0),
			new OsInfo(OsVersion.Win98, PlatformID.Win32Windows, 4, 10),
			new OsInfo(OsVersion.WinME, PlatformID.Win32Windows, 4, 90),
			new OsInfo(OsVersion.WinNT351, PlatformID.Win32NT, 3, 51),
			new OsInfo(OsVersion.WinNT4, PlatformID.Win32NT, 4, 0),
			new OsInfo(OsVersion.Win2000, PlatformID.Win32NT, 5, 0),
			new OsInfo(OsVersion.WinXP, PlatformID.Win32NT, 5, 1),
			new OsInfo(OsVersion.Win2003, PlatformID.Win32NT, 5, 2),
			new OsInfo(OsVersion.WinXPx64, PlatformID.Win32NT, 5, 2),
			new OsInfo(OsVersion.Vista, PlatformID.Win32NT, 6, 0),
			new OsInfo(OsVersion.WinServer2008, PlatformID.Win32NT, 6, 0),
			new OsInfo(OsVersion.WinServer2008R2, PlatformID.Win32NT, 6, 1),
			new OsInfo(OsVersion.Win7, PlatformID.Win32NT, 6, 1),
			new OsInfo(OsVersion.WinServer2012, PlatformID.Win32NT, 6, 2),
			new OsInfo(OsVersion.Win8, PlatformID.Win32NT, 6, 2),
			new OsInfo(OsVersion.WinServer2012R2, PlatformID.Win32NT, 6, 3),
			new OsInfo(OsVersion.Win8Update1, PlatformID.Win32NT, 6, 3),
			new OsInfo(OsVersion.Win10, PlatformID.Win32NT, 10, 0),
			new OsInfo(OsVersion.WinServer2016, PlatformID.Win32NT, 10, 0)
		};
	}
}
