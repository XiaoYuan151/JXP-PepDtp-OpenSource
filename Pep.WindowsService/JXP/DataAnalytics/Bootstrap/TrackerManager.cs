using System;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Exceptions;
using JXP.DataAnalytics.Network;
using JXP.DataAnalytics.Tracker;

namespace JXP.DataAnalytics.Bootstrap
{
	// Token: 0x02000047 RID: 71
	public class TrackerManager
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007410 File Offset: 0x00005610
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007417 File Offset: 0x00005617
		public static bool IsDebuggerEnabled
		{
			get
			{
				return DebugTracer.IsDebuggerEnabled;
			}
			set
			{
				DebugTracer.IsDebuggerEnabled = true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000741F File Offset: 0x0000561F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00007426 File Offset: 0x00005626
		public static ITracker Tracker { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000742E File Offset: 0x0000562E
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00007435 File Offset: 0x00005635
		public static ITracker RtTracker { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000743D File Offset: 0x0000563D
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00007444 File Offset: 0x00005644
		private static IScanner Scanner { get; set; }

		// Token: 0x06000203 RID: 515 RVA: 0x00007454 File Offset: 0x00005654
		public static void Init(TrackerFactory factory, string trackerName = "", string scannerName = "")
		{
			if (factory == null)
			{
				throw new TrackerException("参数[factory]不能为空.", null);
			}
			DefaultNetProvider.Current = factory.NewNetProvider();
			TrackerManager.RtTracker = factory.NewRealTimeTracker("Rt" + trackerName);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007488 File Offset: 0x00005688
		public static void OnStop()
		{
			try
			{
				if (TrackerManager.Tracker != null)
				{
					TrackerManager.Tracker.Close();
					TrackerManager.Tracker = null;
				}
			}
			catch
			{
			}
			try
			{
				if (TrackerManager.RtTracker != null)
				{
					TrackerManager.RtTracker.Close();
					TrackerManager.RtTracker = null;
				}
			}
			catch
			{
			}
			try
			{
				if (TrackerManager.Scanner != null)
				{
					TrackerManager.Scanner.Stop();
					TrackerManager.Scanner = null;
				}
			}
			catch
			{
			}
		}
	}
}
