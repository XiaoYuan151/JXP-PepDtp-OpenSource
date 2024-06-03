using System;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000025 RID: 37
	internal class Constants
	{
		// Token: 0x04000050 RID: 80
		internal const string DefaultTrackerDir = ".pa";

		// Token: 0x04000051 RID: 81
		internal const string ScanDir = "scan";

		// Token: 0x04000052 RID: 82
		internal const string TrackDir = "track";

		// Token: 0x04000053 RID: 83
		internal const string DatFileExt = "padat";

		// Token: 0x04000054 RID: 84
		internal const string SqliteFile = "padat.bin";

		// Token: 0x04000055 RID: 85
		internal const string EndOfRow = "\\r\\n";

		// Token: 0x04000056 RID: 86
		internal const string SecretHeader = "|||";

		// Token: 0x04000057 RID: 87
		internal const string Config_TrackerSwitch = "TrackerSwitch";

		// Token: 0x04000058 RID: 88
		internal const int MaxDataFileSizeWhenWrite = 143360;

		// Token: 0x04000059 RID: 89
		internal const int MaxDataFileSizeWhenScan = 512000;

		// Token: 0x0400005A RID: 90
		internal const int SqliteMaxRowCount = 20000;

		// Token: 0x0400005B RID: 91
		internal const int SqliteMaxPostCount = 300;

		// Token: 0x0400005C RID: 92
		internal const int HttpRequestTimeOut = 10000;

		// Token: 0x0400005D RID: 93
		internal const string TokenUrl = "https://bd-in.mypep.cn/data_collect/collect/service/token.json";

		// Token: 0x0400005E RID: 94
		internal const string EndPointUrl = "https://bd-in.mypep.cn/data_collect/collect/service/url.json";

		// Token: 0x0400005F RID: 95
		internal const string Code_Success = "500110";

		// Token: 0x04000060 RID: 96
		internal const string Code_Expired = "501096";

		// Token: 0x04000061 RID: 97
		internal const string Code_NotRight = "501095";
	}
}
