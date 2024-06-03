using System;
using System.Threading;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000028 RID: 40
	internal class InterProcessLocker
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00004410 File Offset: 0x00002610
		internal static void Execute(string lockString, Action action)
		{
			using (Mutex mutex = new Mutex(false, lockString))
			{
				bool flag = false;
				try
				{
					flag = mutex.WaitOne(TimeSpan.FromSeconds(3.0));
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				if (flag)
				{
					try
					{
						action();
					}
					finally
					{
						mutex.ReleaseMutex();
					}
				}
			}
		}
	}
}
