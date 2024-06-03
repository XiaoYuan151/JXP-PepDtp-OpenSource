using System;
using JXP.Networks;
using NamedPipeWrapper;

namespace Pep.WindowsService.Data
{
	// Token: 0x02000022 RID: 34
	internal class SmartTeachingClient
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003B1D File Offset: 0x00001D1D
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003B25 File Offset: 0x00001D25
		internal NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> Client { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003B2E File Offset: 0x00001D2E
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003B36 File Offset: 0x00001D36
		internal AnalyticsConnectData Data { get; set; }
	}
}
