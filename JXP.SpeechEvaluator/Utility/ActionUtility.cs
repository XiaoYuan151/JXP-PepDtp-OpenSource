using System;
using System.Diagnostics;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000033 RID: 51
	internal class ActionUtility
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00008314 File Offset: 0x00006514
		public static void IgnoreExp(params Action[] actions)
		{
			if (actions == null || actions.Length == 0)
			{
				return;
			}
			foreach (Action action in actions)
			{
				try
				{
					if (action != null)
					{
						action();
					}
				}
				catch (Exception value)
				{
					Trace.WriteLine(value);
				}
			}
		}
	}
}
