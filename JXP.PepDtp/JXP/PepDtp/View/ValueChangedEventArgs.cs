using System;

namespace JXP.PepDtp.View
{
	// Token: 0x02000031 RID: 49
	public class ValueChangedEventArgs : EventArgs
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00010282 File Offset: 0x0000E482
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0001028A File Offset: 0x0000E48A
		public bool CurrentValue { get; set; }

		// Token: 0x060002A9 RID: 681 RVA: 0x00010293 File Offset: 0x0000E493
		public ValueChangedEventArgs(bool currentValue)
		{
			this.CurrentValue = currentValue;
		}
	}
}
