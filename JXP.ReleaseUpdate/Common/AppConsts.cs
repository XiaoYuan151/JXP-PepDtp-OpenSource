using System;

namespace JXP.ReleaseUpdate.Common
{
	// Token: 0x02000011 RID: 17
	public class AppConsts
	{
		// Token: 0x04000048 RID: 72
		public const string STR_FORMAT_COMMON_BACKUP_FILE_NAME = "{0}_{1}{2}";

		// Token: 0x04000049 RID: 73
		public const string STR_FORMAT_DETAIL_WITH_VERSION_FILE_NAME = "detail_{0}.json";

		// Token: 0x0400004A RID: 74
		public const string STR_FORMAT_GET_LAST_UPDATE_INFO = "a/release/client/versionCheck?client_version={0}&product_id={1}&client_type=01 ";

		// Token: 0x0400004B RID: 75
		public const string STR_FORMAT_GET_LAST_CLIENT_VERSION = "client/client_version.json?client={0}&version={1}";

		// Token: 0x0400004C RID: 76
		public const string STR_SUFFIX_UPDATE_FILE = ".old";

		// Token: 0x0400004D RID: 77
		public const string STR_DIR_SETUP = "setup";

		// Token: 0x0400004E RID: 78
		public const string STR_DIR_UPDATELIST = "update_list/";

		// Token: 0x0400004F RID: 79
		public const string STR_DIR_UPDATELOGS = "update_logs/";

		// Token: 0x04000050 RID: 80
		public const string STR_FILE_NAME_COMMON_DETAIL = "detail.json";

		// Token: 0x04000051 RID: 81
		public const string STR_FILE_NAME_LAST_VERSION = "lastver.json";

		// Token: 0x04000052 RID: 82
		public const string STR_FILE_NAME_TEMP_SUFFIX = "temp";

		// Token: 0x04000053 RID: 83
		public static readonly string STR_DIR_FOIXT = "foxit";

		// Token: 0x04000054 RID: 84
		public static readonly string STR_FOIXT_SDK_ACTIVEX_OCX_FILE_NAME = "FoxitPDFSDK_AX_Pro_sig.ocx";

		// Token: 0x04000055 RID: 85
		public static readonly string STR_FOIXT_SDK_ACTIVEX_DLL_FILE_NAME = "FoxitPDFSDKActiveX_Pro.dll";

		// Token: 0x04000056 RID: 86
		public static readonly string STR_FOIXT_SDK_LIB_DLL_FILE_NAME = "FoxitPDFSDKProLib.dll";
	}
}
