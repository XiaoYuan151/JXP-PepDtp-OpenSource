using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JXP.LocalDB;
using JXP.Models;

namespace JXP.PepData
{
	// Token: 0x02000002 RID: 2
	internal class ChaptersDataAccess
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void InsertChapters(ChaptersModel chaptersInfo)
		{
			if (chaptersInfo == null)
			{
				return;
			}
			SQLiteHelper.SqliteConn.Insert(chaptersInfo);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		public void InsertChapterInfo(ChaptersModelList lstChapter)
		{
			if (lstChapter == null || lstChapter.Count == 0)
			{
				return;
			}
			SQLiteHelper.SqliteConn.InsertAll(lstChapter, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
		public void InsertChapterInfo(IList lstChapter)
		{
			if (lstChapter == null || lstChapter.Count == 0)
			{
				return;
			}
			SQLiteHelper.SqliteConn.InsertAll(lstChapter, true);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		public void SaveChaptersByTbid(string strTbid, ChaptersModelList lstAllChapters)
		{
			if (lstAllChapters == null || lstAllChapters.Count == 0)
			{
				return;
			}
			this.DeleteChaptersByTbId(strTbid);
			SQLiteHelper.SqliteConn.InsertAll(lstAllChapters, true);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020BA File Offset: 0x000002BA
		public void SaveChaptersByTbid(string strTbid, List<ChaptersModel> lstAllChapters)
		{
			if (lstAllChapters == null || lstAllChapters.Count == 0)
			{
				return;
			}
			this.DeleteChaptersByTbId(strTbid);
			SQLiteHelper.SqliteConn.InsertAll(lstAllChapters, true);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public void DelChapters(string strID)
		{
			string query = "DELETE FROM chapters WHERE id =?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				strID
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002108 File Offset: 0x00000308
		public void DeleteChaptersByTbId(string strTbId)
		{
			try
			{
				string query = "DELETE FROM chapters WHERE tb_id = ?";
				SQLiteHelper.SqliteConn.Execute(query, new object[]
				{
					strTbId
				});
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		public void DelChaptersByTbid(List<string> lstTbid)
		{
			if (lstTbid == null || lstTbid.Count == 0)
			{
				return;
			}
			string text = string.Empty;
			foreach (string arg in lstTbid)
			{
				text += string.Format("'{0}',", arg);
			}
			text = text.TrimEnd(new char[]
			{
				','
			});
			string query = string.Format("DELETE FROM chapters WHERE tb_id  in ({0})", text);
			SQLiteHelper.SqliteConn.Execute(query, new object[0]);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		public void UpdateChapters(ChaptersModel chaptersInfo)
		{
			if (chaptersInfo == null)
			{
				return;
			}
			SQLiteHelper.SqliteConn.Update(chaptersInfo);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		public ChaptersModelList GetAllChapters()
		{
			string query = "SELECT * FROM chapters ORDER BY id";
			List<ChaptersModel> retList = SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[0]);
			return this.ConverChaptersModelList(retList);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		public ChaptersModel GetChapterByID(string id)
		{
			string query = "SELECT * FROM chapters where id=?";
			return SQLiteHelper.SqliteConn.FindWithQuery<ChaptersModel>(query, new object[]
			{
				id
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
		public List<ChaptersModel> GetChapterByIDs(List<string> lstIds)
		{
			List<string> list = new List<string>();
			foreach (string str in lstIds)
			{
				list.Add("'" + str + "'");
			}
			string str2 = "(" + string.Join(",", list) + ")";
			string query = "SELECT * FROM chapters where id in " + str2;
			return SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[0]);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022EC File Offset: 0x000004EC
		public ChaptersModelList GetChaptersByTbId(string strTbId)
		{
			string query = "SELECT * FROM chapters WHERE tb_id = ? ORDER BY level,sort_num ASC";
			List<ChaptersModel> retList = SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[]
			{
				strTbId
			});
			return this.ConverChaptersModelList(this.SortCharpters(retList));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002324 File Offset: 0x00000524
		public List<ChaptersModel> GetLstChaptersByTbId(string strTbId)
		{
			string query = "SELECT * FROM chapters WHERE tb_id = ? ORDER BY level,sort_num ASC";
			List<ChaptersModel> retList = SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[]
			{
				strTbId
			});
			return this.SortCharpters(retList);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002354 File Offset: 0x00000554
		public ChaptersModelList GetContentChaptersByTbId(string strTbId)
		{
			string query = "SELECT * FROM chapters WHERE tb_id = ? and is_chapter = 1 ORDER BY level,sort_num ASC";
			List<ChaptersModel> retList = SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[]
			{
				strTbId
			});
			return this.ConverChaptersModelList(this.SortCharpters(retList));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000238C File Offset: 0x0000058C
		public ChaptersModel GetChaptersByPageNum(string strBookId, int nPageNum)
		{
			string query = "SELECT * FROM chapters WHERE tb_id = ? AND index_s <= ? AND index_e >= ? ORDER BY level DESC, index_e DESC,id DESC";
			return SQLiteHelper.SqliteConn.FindWithQuery<ChaptersModel>(query, new object[]
			{
				strBookId,
				nPageNum,
				nPageNum
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C8 File Offset: 0x000005C8
		public int GetChaptersCountByTbId(string strTbId)
		{
			return SQLiteHelper.SqliteConn.Table<ChaptersModel>().Count((ChaptersModel item) => item.TbId == strTbId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002450 File Offset: 0x00000650
		public bool CheckChaptersExist(string id)
		{
			return SQLiteHelper.SqliteConn.Table<ChaptersModel>().Count((ChaptersModel item) => item.ID == id) > 0;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024DC File Offset: 0x000006DC
		public List<ChaptersModel> GetChaptersByTbIdPreview(string strTbId)
		{
			string query = "SELECT * FROM chapters WHERE tb_id = ? ORDER BY level,sort_num ASC";
			((IDictionary<string, object>)new Dictionary<string, object>()).Add("tb_id", strTbId);
			List<ChaptersModel> list = SQLiteHelper.SqliteConn.Query<ChaptersModel>(query, new object[]
			{
				strTbId
			});
			if (list == null)
			{
				return null;
			}
			List<ChaptersModel> list2 = new List<ChaptersModel>();
			foreach (ChaptersModel chaptersModel in list)
			{
				if (chaptersModel != null)
				{
					list2.Add(chaptersModel);
				}
			}
			return list2;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002568 File Offset: 0x00000768
		public string GetKeywords(string id)
		{
			string query = "SELECT ex_keywords FROM chapters WHERE id = ?";
			((IDictionary<string, object>)new Dictionary<string, object>()).Add("id", id);
			ChaptersModel chaptersModel = SQLiteHelper.SqliteConn.FindWithQuery<ChaptersModel>(query, new object[]
			{
				id
			});
			return ((chaptersModel != null) ? chaptersModel.Keywords : null) ?? string.Empty;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025B8 File Offset: 0x000007B8
		private List<ChaptersModel> SortCharpters(List<ChaptersModel> retList)
		{
			if (retList == null || retList.Count == 0)
			{
				return retList;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			foreach (ChaptersModel chaptersModel in retList)
			{
				string id = chaptersModel.ID;
				if (!string.IsNullOrEmpty(id) && id.Length == 19)
				{
					string a = id.Substring(13, 1);
					if (a == "p")
					{
						list.Add(id);
					}
					else if (a == "s")
					{
						list3.Add(id);
					}
					else
					{
						list2.Add(id);
					}
				}
			}
			list.Sort();
			list2.Sort();
			list3.Sort();
			List<string> list4 = new List<string>();
			list4.AddRange(list);
			list4.AddRange(list2);
			list4.AddRange(list3);
			List<ChaptersModel> list5 = new List<ChaptersModel>();
			using (List<string>.Enumerator enumerator2 = list4.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string strId = enumerator2.Current;
					IEnumerable<ChaptersModel> collection = from item in retList
					where item.ID == strId
					select item;
					list5.AddRange(collection);
				}
			}
			return list5;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002710 File Offset: 0x00000910
		private ChaptersModelList ConverChaptersModelList(List<ChaptersModel> retList)
		{
			if (retList == null)
			{
				return null;
			}
			ChaptersModelList chaptersModelList = new ChaptersModelList();
			foreach (ChaptersModel chaptersModel in retList)
			{
				if (chaptersModel != null)
				{
					chaptersModelList.Add(chaptersModel);
				}
			}
			return chaptersModelList;
		}
	}
}
