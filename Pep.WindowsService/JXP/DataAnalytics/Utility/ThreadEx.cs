using System;
using System.Threading;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x0200002B RID: 43
	internal class ThreadEx
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004C48 File Offset: 0x00002E48
		internal static Thread Run(ThreadStart action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			Thread thread = new Thread(action);
			thread.IsBackground = true;
			thread.Start();
			return thread;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004C6C File Offset: 0x00002E6C
		internal static Thread Run(ParameterizedThreadStart action, object parameter)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			Thread thread = new Thread(action);
			thread.IsBackground = true;
			if (parameter != null)
			{
				thread.Start(parameter);
			}
			return thread;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal static void Delay(TimeSpan dueTime)
		{
			Thread.Sleep((int)dueTime.TotalMilliseconds);
		}
	}
}
