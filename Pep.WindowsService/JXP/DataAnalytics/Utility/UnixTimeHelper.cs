using System;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x0200002C RID: 44
	public class UnixTimeHelper
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004CB7 File Offset: 0x00002EB7
		public static long Now
		{
			get
			{
				return UnixTimeHelper.ToUnixTimeMilliseconds(DateTimeOffset.UtcNow);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public static long ToUnixTimeSeconds(DateTimeOffset offset)
		{
			return offset.UtcDateTime.Ticks / 10000000L - 62135596800L;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004CF1 File Offset: 0x00002EF1
		public static long ToUnixTimeMilliseconds(DateTimeOffset offset)
		{
			return offset.Ticks / 10000L - 62135596800000L;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D0C File Offset: 0x00002F0C
		public static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			if (seconds < -62135596800L || seconds > 253402300799L)
			{
				throw new ArgumentOutOfRangeException("seconds", string.Format("Min:[{0}], Max:[{1}]", -62135596800L, 253402300799L));
			}
			return new DateTimeOffset(seconds * 10000000L + 621355968000000000L, TimeSpan.Zero);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004D80 File Offset: 0x00002F80
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", string.Format("Min:[{0}], Max:[{1}]", -62135596800000L, 253402300799999L));
			}
			return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
		}

		// Token: 0x04000062 RID: 98
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x04000063 RID: 99
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04000064 RID: 100
		private const long TicksPerMinute = 600000000L;

		// Token: 0x04000065 RID: 101
		private const long TicksPerHour = 36000000000L;

		// Token: 0x04000066 RID: 102
		private const long TicksPerDay = 864000000000L;

		// Token: 0x04000067 RID: 103
		private const int DaysPerYear = 365;

		// Token: 0x04000068 RID: 104
		private const int DaysPer4Years = 1461;

		// Token: 0x04000069 RID: 105
		private const int DaysPer100Years = 36524;

		// Token: 0x0400006A RID: 106
		private const int DaysPer400Years = 146097;

		// Token: 0x0400006B RID: 107
		internal const int DaysTo1970 = 719162;

		// Token: 0x0400006C RID: 108
		private const int DaysTo10000 = 3652059;

		// Token: 0x0400006D RID: 109
		private const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x0400006E RID: 110
		private const long UnixEpochSeconds = 62135596800L;

		// Token: 0x0400006F RID: 111
		private const long UnixEpochMilliseconds = 62135596800000L;

		// Token: 0x04000070 RID: 112
		internal const long MinTicks = 0L;

		// Token: 0x04000071 RID: 113
		internal const long MaxTicks = 3155378975999999999L;
	}
}
