using System;

namespace Pep.WindowsService.Data
{
	// Token: 0x02000021 RID: 33
	public enum States : uint
	{
		// Token: 0x04000047 RID: 71
		Join,
		// Token: 0x04000048 RID: 72
		Heart = 1000U,
		// Token: 0x04000049 RID: 73
		Leave = 5000U
	}
}
