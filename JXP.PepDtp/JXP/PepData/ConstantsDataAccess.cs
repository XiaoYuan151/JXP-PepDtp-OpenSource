using System;
using System.Collections.Generic;
using JXP.LocalDB;
using JXP.Models;
using JXP.Utilities;

namespace JXP.PepData
{
	// Token: 0x02000003 RID: 3
	internal class ConstantsDataAccess
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002778 File Offset: 0x00000978
		public ConstantsModelList GetAllConstants()
		{
			string query = "SELECT * FROM constants ORDER BY id";
			List<ConstantsModel> list = SQLiteHelper.SqliteConn.Query<ConstantsModel>(query, new object[0]);
			if (list == null)
			{
				return null;
			}
			ConstantsModelList constantsModelList = new ConstantsModelList();
			foreach (ConstantsModel constantsModel in list)
			{
				if (constantsModel != null)
				{
					constantsModelList.Add(constantsModel);
				}
			}
			return constantsModelList;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027F0 File Offset: 0x000009F0
		public ConstantsModel GetConstantsByID(int ID)
		{
			string query = "SELECT * FROM constants WHERE id = ?";
			return SQLiteHelper.SqliteConn.FindWithQuery<ConstantsModel>(query, new object[]
			{
				ID
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002820 File Offset: 0x00000A20
		public void UpdateLocalConstant(ConstantsModelList lstConstantsModel)
		{
			if (lstConstantsModel == null || lstConstantsModel.Count == 0)
			{
				return;
			}
			foreach (ConstantsModel constantsModel in lstConstantsModel)
			{
				constantsModel.Update_Time = DateTime.Now.ToString(CommonConsts.DATE_TIME_FORMATE);
			}
			SQLiteHelper.SqliteConn.UpdateAll(lstConstantsModel, true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002898 File Offset: 0x00000A98
		public void InsertLocalConstant(ConstantsModelList lstConstantsModel)
		{
			if (lstConstantsModel == null || lstConstantsModel.Count == 0)
			{
				return;
			}
			foreach (ConstantsModel constantsModel in lstConstantsModel)
			{
				constantsModel.Update_Time = DateTime.Now.ToString(CommonConsts.DATE_TIME_FORMATE);
			}
			SQLiteHelper.SqliteConn.InsertAll(lstConstantsModel, true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002910 File Offset: 0x00000B10
		public string GetContentEnum(int ID, string strValue, string strDefault, bool bGetKeyFlg)
		{
			string query = "SELECT content FROM constants WHERE id = ?";
			ConstantsModel constantsModel = SQLiteHelper.SqliteConn.FindWithQuery<ConstantsModel>(query, new object[]
			{
				ID
			});
			if (string.IsNullOrEmpty((constantsModel != null) ? constantsModel.Content : null))
			{
				return strDefault;
			}
			string[] array = constantsModel.Content.TrimStart(new char[]
			{
				'['
			}).TrimEnd(new char[]
			{
				']'
			}).Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'-'
				});
				if (array2.Length == 2)
				{
					if (bGetKeyFlg)
					{
						if (string.Compare(array2[1], strValue, true) == 0)
						{
							return array2[0];
						}
					}
					else if (array2[0] == strValue)
					{
						return array2[1];
					}
				}
			}
			return strDefault;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000029DC File Offset: 0x00000BDC
		public void TrueDelConstById(int id = 0)
		{
			string query = (id == 0) ? "Delete from constants" : "Delete from constants WHERE id=?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				id
			});
		}
	}
}
