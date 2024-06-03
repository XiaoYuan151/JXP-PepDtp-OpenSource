using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using JXP.Cef;
using JXP.LocalDB;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.Player.Web;
using pep.Nobook.Web;

namespace JXP.PepDtp.Web
{
	// Token: 0x0200000E RID: 14
	[Export(typeof(RenderProcessHandler))]
	public class TeacherRenderProcessHandler : RenderProcessHandler
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000A07C File Offset: 0x0000827C
		protected override void ProcessBrowserData(string dataFromBrowser)
		{
			try
			{
				SQLiteHelper.InitSQLiteConn(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConsts.DB_NAME), AppConsts.DATABASE_PASSWORD);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "Cef子进程设置数据访问字符串失败.";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			base.ProcessBrowserData(dataFromBrowser);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A0E4 File Offset: 0x000082E4
		protected override Dictionary<string, object[]> GetMultiJsFunctions()
		{
			return new Dictionary<string, object[]>
			{
				{
					"Cef",
					new object[]
					{
						new CSharpJSEvents(),
						new NbJSEvents(),
						new PlayerJsEvent()
					}
				}
			};
		}
	}
}
