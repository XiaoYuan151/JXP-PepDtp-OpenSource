using System;
using System.Collections.Generic;
using JXP.LocalDB;
using JXP.Logs;
using JXP.Models;

namespace JXP.PepData
{
	// Token: 0x02000005 RID: 5
	internal class TextbooksDataAccess
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003320 File Offset: 0x00001520
		public TextbooksModelList GetAllTextbooksInfo()
		{
			TextbooksModelList textbooksModelList = new TextbooksModelList();
			string query = "SELECT * FROM textbooks ORDER BY s_modify_time DESC";
			List<TextbookModel> list = SQLiteHelper.SqliteConn.Query<TextbookModel>(query, new object[0]);
			if (list == null)
			{
				return null;
			}
			foreach (TextbookModel textbookModel in list)
			{
				if (textbookModel != null)
				{
					textbooksModelList.Add(textbookModel);
				}
			}
			return textbooksModelList;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003398 File Offset: 0x00001598
		public void UpdateTextbooksAllInfo(TextbookModel textbooks)
		{
			if (textbooks == null)
			{
				return;
			}
			SQLiteHelper.SqliteConn.Update(textbooks, typeof(TextbookModel));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000033B4 File Offset: 0x000015B4
		public void InsertTextbooksInfo(TextbookModel textbooks)
		{
			if (textbooks == null)
			{
				return;
			}
			SQLiteHelper.SqliteConn.Insert(textbooks, typeof(TextbookModel));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033D0 File Offset: 0x000015D0
		public void DeleteLocalTextbooks(string ID)
		{
			try
			{
				string query = "DELETE FROM textbooks WHERE id = ?";
				SQLiteHelper.SqliteConn.Execute(query, new object[]
				{
					ID
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("教材信息删除(DeleteLocalTextbooks)出错：" + ex.Message);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003428 File Offset: 0x00001628
		public void SaveTextbook(TextbookModel textbook, bool bSetInfo = true)
		{
			if (textbook == null)
			{
				return;
			}
			if (this.CheckBookExistByID(textbook.ID))
			{
				this.UpdateTextbooksAllInfo(textbook);
				return;
			}
			this.InsertTextbooksInfo(textbook);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003450 File Offset: 0x00001650
		public bool CheckBookExistByID(string strID)
		{
			return SQLiteHelper.SqliteConn.Table<TextbookModel>().Count((TextbookModel item) => item.ID == strID) > 0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000034DC File Offset: 0x000016DC
		public int GetStateByID(string strID)
		{
			string query = "SELECT s_state FROM textbooks WHERE id = ?";
			TextbookModel textbookModel = SQLiteHelper.SqliteConn.FindWithQuery<TextbookModel>(query, new object[]
			{
				strID
			});
			if (textbookModel == null)
			{
				return -1;
			}
			return textbookModel.State;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003510 File Offset: 0x00001710
		public void UpdateStateByID(int nState, string strID)
		{
			string query = "UPDATE textbooks SET \r\n                            s_state = ? \r\n                            WHERE id = ?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				nState,
				strID
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003544 File Offset: 0x00001744
		public TextbookModel GetTexbooksInfoByID(string strID)
		{
			string query = "SELECT * FROM textbooks  WHERE id = ?";
			return SQLiteHelper.SqliteConn.FindWithQuery<TextbookModel>(query, new object[]
			{
				strID
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000356C File Offset: 0x0000176C
		public void ResetTextBookID(string strOldID, string strNewID)
		{
			this.DeleteLocalTextbooks(strNewID);
			string query = "UPDATE textbooks SET\r\n                            id = ?\r\n                            WHERE id = ?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				strNewID,
				strOldID
			});
		}
	}
}
