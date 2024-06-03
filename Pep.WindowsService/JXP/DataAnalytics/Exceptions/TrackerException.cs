using System;

namespace JXP.DataAnalytics.Exceptions
{
	// Token: 0x02000044 RID: 68
	public class TrackerException : Exception
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000071F5 File Offset: 0x000053F5
		public TrackerException()
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000071FD File Offset: 0x000053FD
		public TrackerException(string message, Exception inner = null) : base(message, inner)
		{
		}
	}
}
