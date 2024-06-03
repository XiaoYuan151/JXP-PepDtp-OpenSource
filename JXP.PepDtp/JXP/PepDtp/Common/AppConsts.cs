using System;
using JXP.Configuration;

namespace JXP.PepDtp.Common
{
	// Token: 0x020000A4 RID: 164
	public class AppConsts
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0002D79C File Offset: 0x0002B99C
		static AppConsts()
		{
			AppConsts.JXP_PRODUCTID = ConfigManager.AppSettings["ProductId"];
			AppConsts.JXP_APPKEY = ConfigManager.AppSettings["AppKey"];
			AppConsts.Activitie_Url = ConfigManager.AppSettings["ActivitieUrl"];
			AppConsts.HTTPMODE = ConfigManager.AppSettings["httpMode"];
			AppConsts.PesonalData_Url = ConfigManager.AppSettings["PesonalDataUrl"];
			string text = ConfigManager.AppSettings["ExitWithoutOperationTime"];
			double exitWithoutOperationTime;
			if (!string.IsNullOrEmpty(text) && double.TryParse(text, out exitWithoutOperationTime))
			{
				AppConsts.ExitWithoutOperationTime = exitWithoutOperationTime;
			}
		}

		// Token: 0x0400048F RID: 1167
		public static readonly string DATABASE_PASSWORD = "CB8C92BFC9204AE9AA442480ED3B8866";

		// Token: 0x04000490 RID: 1168
		public static readonly string DB_NAME = "teacher.bin";

		// Token: 0x04000491 RID: 1169
		public static readonly string PEPCLASS_EXE = "JXP.PepClass.Host.exe";

		// Token: 0x04000492 RID: 1170
		public static readonly string JXP_PRODUCTID;

		// Token: 0x04000493 RID: 1171
		public static readonly string JXP_APPKEY;

		// Token: 0x04000494 RID: 1172
		public static readonly string HTTPMODE;

		// Token: 0x04000495 RID: 1173
		public static readonly string COURSE_PACKAGE_EXTENSION = ".pepkc";

		// Token: 0x04000496 RID: 1174
		public static readonly string JXP_REGISTER_TOKEN = "regsiter_token";

		// Token: 0x04000497 RID: 1175
		public static readonly string FOLDER_LINK_NAME = "PepDtp_gx";

		// Token: 0x04000498 RID: 1176
		public static readonly string Activitie_Url;

		// Token: 0x04000499 RID: 1177
		public static readonly string PesonalData_Url;

		// Token: 0x0400049A RID: 1178
		public static double ExitWithoutOperationTime = 120.0;

		// Token: 0x0400049B RID: 1179
		public static readonly string Online_Customer_service_Url = "https://gxyxjccbsyxgs.qiyukf.com/client?k=01f25d3362ff5bc94f511404ed93c079&wp=1&robotShuntSwitch=0&shuntId=0";
	}
}
