using System;
using System.Collections.Generic;
using JXP.LocalDB;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.DataAccess
{
	// Token: 0x020000A9 RID: 169
	public class ApplyToolsDataAccess
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x0002DF9C File Offset: 0x0002C19C
		public List<AppToolsModel> GetApplyTools(string userid)
		{
			if (string.IsNullOrEmpty(userid))
			{
				return null;
			}
			string text = "SELECT * FROM appcenter A,appcenter_state B Where A.apply_id = B.apply_id AND B.userid = ? ORDER BY B.downloadtime DESC";
			string[] array = new string[]
			{
				userid
			};
			SQLiteConnection sqliteConn = SQLiteHelper.SqliteConn;
			string query = text;
			object[] args = array;
			List<AppToolsModel> list = sqliteConn.Query<AppToolsModel>(query, args);
			if (list == null)
			{
				return null;
			}
			List<AppToolsModel> list2 = new List<AppToolsModel>();
			foreach (AppToolsModel appToolsModel in list)
			{
				if (appToolsModel != null && !list2.Contains(appToolsModel))
				{
					list2.Add(appToolsModel);
				}
			}
			return list2;
		}
	}
}
