using System;
using System.Globalization;
using System.Net;
using System.Text;
using JXP.DataAnalytics.Utility;

namespace JXP.DataAnalytics.Platform
{
	// Token: 0x02000037 RID: 55
	public class DefaultEnvironmentProvider : IEnvironmentProvider
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006824 File Offset: 0x00004A24
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000682C File Offset: 0x00004A2C
		public string Os { get; set; } = string.Empty;

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006835 File Offset: 0x00004A35
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000683D File Offset: 0x00004A3D
		public string Hardware { get; set; } = string.Empty;

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006846 File Offset: 0x00004A46
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000684E File Offset: 0x00004A4E
		public string IpAddr { get; set; } = string.Empty;

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006857 File Offset: 0x00004A57
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000685F File Offset: 0x00004A5F
		public string SoftwareDependency { get; set; } = string.Empty;

		// Token: 0x0600018A RID: 394 RVA: 0x00006868 File Offset: 0x00004A68
		public DefaultEnvironmentProvider()
		{
			this.Os = string.Format("culture:{0},bit:{1},os:{2}", CultureInfo.CurrentUICulture.Name, Environment.Is64BitOperatingSystem ? "X64" : "X86", Environment.OSVersion.VersionString);
			this.SoftwareDependency = NetFxVersion.GetInstalledFxVersion();
			int[] monitor = SystemInfo.GetMonitor();
			int num = monitor[1];
			int num2 = monitor[0];
			string text = string.Format("{0}MB", SystemInfo.GetPhysicalMemory());
			float screenFactor = SystemInfo.GetScreenFactor();
			string cpuName = SystemInfo.GetCpuName();
			string id = DeviceIdHelper.GetId();
			this.Hardware = string.Format("dpi:{0}*{1},factor:{2},mem:{3},deviceId:{4},cpu:{5}", new object[]
			{
				num2,
				num,
				screenFactor,
				text,
				id,
				cpuName
			});
			StringBuilder stringBuilder = new StringBuilder(2048);
			bool flag = true;
			foreach (IPAddress ipaddress in SystemInfo.GetIpAddresses())
			{
				if (flag)
				{
					stringBuilder.Append(ipaddress);
					flag = false;
				}
				else
				{
					stringBuilder.AppendFormat(",{0}", ipaddress);
				}
			}
			this.IpAddr = stringBuilder.ToString();
		}
	}
}
