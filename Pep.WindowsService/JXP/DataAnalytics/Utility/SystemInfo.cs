using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using JXP.DataAnalytics.Debugging;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x0200002A RID: 42
	internal class SystemInfo
	{
		// Token: 0x060000F5 RID: 245
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int smIndex);

		// Token: 0x060000F6 RID: 246
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		// Token: 0x060000F7 RID: 247
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x060000F8 RID: 248
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

		// Token: 0x060000F9 RID: 249
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalMemoryStatusEx([In] [Out] SystemInfo.MEMORYSTATUSEX lpBuffer);

		// Token: 0x060000FA RID: 250 RVA: 0x000048A8 File Offset: 0x00002AA8
		internal static long GetPhysicalMemory()
		{
			SystemInfo.MEMORYSTATUSEX memorystatusex = new SystemInfo.MEMORYSTATUSEX();
			if (SystemInfo.GlobalMemoryStatusEx(memorystatusex))
			{
				return (long)Math.Floor(memorystatusex.ullTotalPhys / 1048576.0);
			}
			return 0L;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000048E0 File Offset: 0x00002AE0
		internal static float GetScreenFactor()
		{
			try
			{
				using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
				{
					IntPtr hdc = graphics.GetHdc();
					int deviceCaps = SystemInfo.GetDeviceCaps(hdc, 10);
					float deviceCaps2 = (float)SystemInfo.GetDeviceCaps(hdc, 117);
					int deviceCaps3 = SystemInfo.GetDeviceCaps(hdc, 90);
					graphics.ReleaseHdc(hdc);
					float num = deviceCaps2 / (float)deviceCaps;
					float num2 = (float)deviceCaps3 / 96f;
					if (num > 1f)
					{
						return num;
					}
					if (num2 > 1f)
					{
						return num2;
					}
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			return 1f;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004988 File Offset: 0x00002B88
		internal static IEnumerable<IPAddress> GetIpAddresses()
		{
			List<IPAddress> list = new List<IPAddress>();
			foreach (NetworkInterface networkInterface in from nic in NetworkInterface.GetAllNetworkInterfaces()
			where nic.OperationalStatus == OperationalStatus.Up
			select nic)
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
				{
					foreach (UnicastIPAddressInformation unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
					{
						if (!list.Contains(unicastIPAddressInformation.Address))
						{
							list.Add(unicastIPAddressInformation.Address);
						}
					}
				}
			}
			return list.OrderByDescending(new Func<IPAddress, int>(SystemInfo.RankIpAddress)).ToList<IPAddress>();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004A70 File Offset: 0x00002C70
		internal static int RankIpAddress(IPAddress addr)
		{
			int num = 1000;
			if (addr.AddressFamily == AddressFamily.InterNetwork)
			{
				num += 100;
			}
			if (addr.ToString().StartsWith("10."))
			{
				num += 100;
			}
			if (addr.ToString().StartsWith("172.30."))
			{
				num += 100;
			}
			if (addr.ToString().StartsWith("192.168.1."))
			{
				num += 100;
			}
			if (addr.ToString().StartsWith("169."))
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal static string GetCpuName()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from Win32_Processor"))
				{
					ManagementObject managementObject = managementObjectSearcher.Get().Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
					if (managementObject != null)
					{
						return managementObject["Name"] as string;
					}
				}
			}
			catch
			{
			}
			return "NonCpu";
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004B60 File Offset: 0x00002D60
		internal static int[] GetMonitor()
		{
			int[] result;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ManagementBaseObject managementBaseObject = enumerator.Current;
							int num = Convert.ToInt32(managementBaseObject["CurrentHorizontalResolution"]);
							int num2 = Convert.ToInt32(managementBaseObject["CurrentVerticalResolution"]);
							return new int[]
							{
								num,
								num2
							};
						}
					}
				}
				result = new int[]
				{
					1920,
					1080
				};
			}
			catch (Exception)
			{
				result = new int[]
				{
					1920,
					1080
				};
			}
			return result;
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class MEMORYSTATUSEX
		{
			// Token: 0x06000269 RID: 617 RVA: 0x00007FFD File Offset: 0x000061FD
			public MEMORYSTATUSEX()
			{
				this.dwLength = (uint)Marshal.SizeOf(typeof(SystemInfo.MEMORYSTATUSEX));
			}

			// Token: 0x040000F5 RID: 245
			public uint dwLength;

			// Token: 0x040000F6 RID: 246
			public uint dwMemoryLoad;

			// Token: 0x040000F7 RID: 247
			public ulong ullTotalPhys;

			// Token: 0x040000F8 RID: 248
			public ulong ullAvailPhys;

			// Token: 0x040000F9 RID: 249
			public ulong ullTotalPageFile;

			// Token: 0x040000FA RID: 250
			public ulong ullAvailPageFile;

			// Token: 0x040000FB RID: 251
			public ulong ullTotalVirtual;

			// Token: 0x040000FC RID: 252
			public ulong ullAvailVirtual;

			// Token: 0x040000FD RID: 253
			public ulong ullAvailExtendedVirtual;
		}

		// Token: 0x02000055 RID: 85
		internal enum DeviceCap
		{
			// Token: 0x040000FF RID: 255
			DRIVERVERSION,
			// Token: 0x04000100 RID: 256
			TECHNOLOGY = 2,
			// Token: 0x04000101 RID: 257
			HORZSIZE = 4,
			// Token: 0x04000102 RID: 258
			VERTSIZE = 6,
			// Token: 0x04000103 RID: 259
			HORZRES = 8,
			// Token: 0x04000104 RID: 260
			VERTRES = 10,
			// Token: 0x04000105 RID: 261
			BITSPIXEL = 12,
			// Token: 0x04000106 RID: 262
			PLANES = 14,
			// Token: 0x04000107 RID: 263
			NUMBRUSHES = 16,
			// Token: 0x04000108 RID: 264
			NUMPENS = 18,
			// Token: 0x04000109 RID: 265
			NUMMARKERS = 20,
			// Token: 0x0400010A RID: 266
			NUMFONTS = 22,
			// Token: 0x0400010B RID: 267
			NUMCOLORS = 24,
			// Token: 0x0400010C RID: 268
			PDEVICESIZE = 26,
			// Token: 0x0400010D RID: 269
			CURVECAPS = 28,
			// Token: 0x0400010E RID: 270
			LINECAPS = 30,
			// Token: 0x0400010F RID: 271
			POLYGONALCAPS = 32,
			// Token: 0x04000110 RID: 272
			TEXTCAPS = 34,
			// Token: 0x04000111 RID: 273
			CLIPCAPS = 36,
			// Token: 0x04000112 RID: 274
			RASTERCAPS = 38,
			// Token: 0x04000113 RID: 275
			ASPECTX = 40,
			// Token: 0x04000114 RID: 276
			ASPECTY = 42,
			// Token: 0x04000115 RID: 277
			ASPECTXY = 44,
			// Token: 0x04000116 RID: 278
			SHADEBLENDCAPS,
			// Token: 0x04000117 RID: 279
			LOGPIXELSX = 88,
			// Token: 0x04000118 RID: 280
			LOGPIXELSY = 90,
			// Token: 0x04000119 RID: 281
			SIZEPALETTE = 104,
			// Token: 0x0400011A RID: 282
			NUMRESERVED = 106,
			// Token: 0x0400011B RID: 283
			COLORRES = 108,
			// Token: 0x0400011C RID: 284
			PHYSICALWIDTH = 110,
			// Token: 0x0400011D RID: 285
			PHYSICALHEIGHT,
			// Token: 0x0400011E RID: 286
			PHYSICALOFFSETX,
			// Token: 0x0400011F RID: 287
			PHYSICALOFFSETY,
			// Token: 0x04000120 RID: 288
			SCALINGFACTORX,
			// Token: 0x04000121 RID: 289
			SCALINGFACTORY,
			// Token: 0x04000122 RID: 290
			VREFRESH,
			// Token: 0x04000123 RID: 291
			DESKTOPVERTRES,
			// Token: 0x04000124 RID: 292
			DESKTOPHORZRES,
			// Token: 0x04000125 RID: 293
			BLTALIGNMENT
		}
	}
}
