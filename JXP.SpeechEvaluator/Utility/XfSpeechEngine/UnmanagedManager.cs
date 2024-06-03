using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace JXP.SpeechEvaluator.Utility.XfSpeechEngine
{
	// Token: 0x02000046 RID: 70
	public class UnmanagedManager
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000A798 File Offset: 0x00008998
		public static IntPtr UnmanagedReader(Stream s, int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			int num = 0;
			for (;;)
			{
				int num2 = s.ReadByte();
				if (num2 == -1)
				{
					break;
				}
				Marshal.WriteByte(intPtr, num, (byte)num2);
				num++;
			}
			return intPtr;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A7CC File Offset: 0x000089CC
		public static string GetStringFromUnmanagedMemory(IntPtr h)
		{
			List<byte> list = new List<byte>();
			while (Marshal.ReadByte(h) != 0)
			{
				list.Add(Marshal.ReadByte(h));
				h += 1;
			}
			list.ToArray();
			return Encoding.UTF8.GetString(list.ToArray());
		}
	}
}
