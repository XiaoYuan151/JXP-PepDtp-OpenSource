using System;
using System.Diagnostics;

namespace JXP.DataAnalytics.Debugging
{
	// Token: 0x02000045 RID: 69
	internal class DebugTracer
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007207 File Offset: 0x00005407
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000720E File Offset: 0x0000540E
		internal static bool IsDebuggerEnabled { get; set; }

		// Token: 0x060001ED RID: 493 RVA: 0x00007218 File Offset: 0x00005418
		internal static void Write(string message)
		{
			if (!DebugTracer.IsDebuggerEnabled)
			{
				return;
			}
			try
			{
				Trace.WriteLine(message);
			}
			catch
			{
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000724C File Offset: 0x0000544C
		internal static void Write(Exception ex)
		{
			if (ex == null)
			{
				return;
			}
			DebugTracer.Write(ex.ToString());
		}
	}
}
