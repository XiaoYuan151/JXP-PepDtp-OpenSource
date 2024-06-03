using System;
using System.ComponentModel;
using JXP.Models;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005B RID: 91
	public class DownloadParamter
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00020E3F File Offset: 0x0001F03F
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00020E47 File Offset: 0x0001F047
		public bool IsUpdate { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00020E50 File Offset: 0x0001F050
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00020E58 File Offset: 0x0001F058
		public BackgroundWorker DownloadWork { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00020E61 File Offset: 0x0001F061
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00020E69 File Offset: 0x0001F069
		public AppCenterModel AppCenterData { get; set; }
	}
}
