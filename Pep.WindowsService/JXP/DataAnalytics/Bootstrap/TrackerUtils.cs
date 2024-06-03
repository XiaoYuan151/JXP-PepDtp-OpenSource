using System;

namespace JXP.DataAnalytics.Bootstrap
{
	// Token: 0x02000048 RID: 72
	public class TrackerUtils
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00007514 File Offset: 0x00005714
		public static string GetClassOrMethodFullPath(params string[] args)
		{
			if (args == null || args.Length <= 1)
			{
				return string.Empty;
			}
			return string.Join(".", args);
		}
	}
}
