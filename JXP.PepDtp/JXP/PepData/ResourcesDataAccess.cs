using System;
using System.Collections.Generic;
using JXP.LocalDB;
using JXP.Logs;
using JXP.Models;

namespace JXP.PepData
{
	// Token: 0x02000004 RID: 4
	internal class ResourcesDataAccess
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002A14 File Offset: 0x00000C14
		public int GetResourcesCountByTbid(string strTbid)
		{
			return SQLiteHelper.SqliteConn.Table<ResourcesModel>().Count((ResourcesModel item) => item.TbId == strTbid);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A9C File Offset: 0x00000C9C
		public void DelResourcesByTbid(string strTbid)
		{
			try
			{
				string query = "DELETE FROM resources WHERE tb_id = ?";
				SQLiteHelper.SqliteConn.Execute(query, new object[]
				{
					strTbid
				});
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("资源信息删除(DelResourcesByTbid)出错：" + ex.Message);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void DelResourcesByID(int nID)
		{
			string query = "DELETE FROM resources WHERE id = ?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				nID
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B24 File Offset: 0x00000D24
		public void InsertResources(ResourcesModelList resList, bool bSetInfo = true)
		{
			if (resList == null || resList.Count == 0)
			{
				return;
			}
			foreach (ResourcesModel resourcesModel in resList)
			{
				resourcesModel.ID = null;
				if (string.IsNullOrEmpty(resourcesModel.TbId))
				{
					resourcesModel.TbId = "";
				}
				if (string.IsNullOrEmpty(resourcesModel.Title))
				{
					resourcesModel.Title = "";
				}
				if (string.IsNullOrEmpty(resourcesModel.Zycj))
				{
					resourcesModel.Zycj = "0";
				}
			}
			SQLiteHelper.SqliteConn.InsertAll(resList, true);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public void UpdateResourceLocalPathByResId(string localPath, string resId)
		{
			if (string.IsNullOrEmpty(resId))
			{
				return;
			}
			string query = "UPDATE resources SET file_path_local = ? WHERE resid = ?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				localPath,
				resId
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C10 File Offset: 0x00000E10
		public void UpdateRsources(ResourcesModelList resList, bool bSetInfo = true)
		{
			if (resList == null || resList.Count == 0)
			{
				return;
			}
			new List<IDictionary<string, object>>();
			foreach (ResourcesModel resourcesModel in resList)
			{
				if (string.IsNullOrEmpty(resourcesModel.Zycj))
				{
					resourcesModel.Zycj = "0";
				}
			}
			SQLiteHelper.SqliteConn.UpdateAll(resList, true);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C88 File Offset: 0x00000E88
		public ResourcesModelList GetResInfoByPageNum(string strTbid, int nPageNum)
		{
			string query = "SELECT * FROM resources WHERE tb_id = ? AND page_num = ? ORDER BY id";
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			((IDictionary<string, object>)dictionary).Add("tb_id", strTbid);
			((IDictionary<string, object>)dictionary).Add("page_num", nPageNum);
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strTbid,
				nPageNum
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public List<ResourcesModel> GetEmbedImageRes(string bookId, int pageNum)
		{
			return (from item in SQLiteHelper.SqliteConn.Table<ResourcesModel>()
			where item.TbId == bookId && item.PageNum == pageNum && item.ShowType == (int?)1002 && item.FileFormat == ".images"
			select item).ToList();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E30 File Offset: 0x00001030
		public ResourcesModelList GetResInfoByPageNumSort(string strTbid, int nPageNum, string strSort)
		{
			string query = string.Format("SELECT * FROM resources WHERE tb_id = ? AND page_num = ? {0}", strSort);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			((IDictionary<string, object>)dictionary).Add("tb_id", strTbid);
			((IDictionary<string, object>)dictionary).Add("page_num", nPageNum);
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strTbid,
				nPageNum
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E90 File Offset: 0x00001090
		public ResourcesModel GetResInfoByResId(string strResId)
		{
			string query = "SELECT * FROM resources WHERE resid = ?";
			return SQLiteHelper.SqliteConn.FindWithQuery<ResourcesModel>(query, new object[]
			{
				strResId
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002EB8 File Offset: 0x000010B8
		public ResourcesModelList GetResourcesInfoByTbid(string strTbid)
		{
			string query = "SELECT * FROM resources WHERE tb_id = ?";
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strTbid
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002EE8 File Offset: 0x000010E8
		public ResourcesModelList GetResourcesInfoByTbid(string strTbid, string strSort)
		{
			string query = string.Format("SELECT * FROM resources WHERE tb_id = ? {0}", strSort);
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strTbid
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F20 File Offset: 0x00001120
		private ResourcesModelList DataTableToIList(List<ResourcesModel> retList)
		{
			ResourcesModelList resourcesModelList = new ResourcesModelList();
			if (retList == null)
			{
				return null;
			}
			foreach (ResourcesModel resourcesModel in retList)
			{
				if (resourcesModel != null)
				{
					resourcesModelList.Add(resourcesModel);
				}
			}
			return resourcesModelList;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F80 File Offset: 0x00001180
		public void ResetTextBookID(string strTbID, string strNewID)
		{
			string query = "UPDATE resources SET\r\n                            tb_id = ?\r\n                            WHERE tb_id = ?";
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			((IDictionary<string, object>)dictionary).Add("id", strNewID);
			((IDictionary<string, object>)dictionary).Add("tbid", strTbID);
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				strNewID,
				strTbID
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FCC File Offset: 0x000011CC
		public ResourcesModelList GetPresetResourcesInfoByChapterID(string strChapterID)
		{
			string query = "SELECT * FROM resources WHERE ori_tree_code = ? and ex_zycj = ? ";
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strChapterID,
				"12"
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003004 File Offset: 0x00001204
		public ResourcesModelList GetResourcesInfoByChapterID(string strChapterID)
		{
			string query = "SELECT * FROM resources WHERE ori_tree_code = ? ";
			List<ResourcesModel> retList = SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[]
			{
				strChapterID
			});
			return this.DataTableToIList(retList);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003034 File Offset: 0x00001234
		public ResourcesModelList GetPageAllResInfo(string strTbid, string strChapterID, int nPageNum)
		{
			ResourcesModelList resourcesModelList = this.GetResInfoByPageNum(strTbid, nPageNum);
			ResourcesModelList presetResourcesInfoByChapterID = this.GetPresetResourcesInfoByChapterID(strChapterID);
			if (resourcesModelList == null)
			{
				resourcesModelList = presetResourcesInfoByChapterID;
			}
			else if (presetResourcesInfoByChapterID != null)
			{
				foreach (ResourcesModel item in presetResourcesInfoByChapterID)
				{
					resourcesModelList.Add(item);
				}
			}
			if (resourcesModelList == null)
			{
				resourcesModelList = new ResourcesModelList();
			}
			return resourcesModelList;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000030A4 File Offset: 0x000012A4
		public void SaveResInfoByResId(ResourcesModel res)
		{
			if (res == null)
			{
				return;
			}
			string query = "DELETE FROM resources WHERE resid = ?";
			SQLiteHelper.SqliteConn.Execute(query, new object[]
			{
				res.ResID
			});
			res.ID = null;
			res.ID = null;
			if (string.IsNullOrEmpty(res.Title))
			{
				res.Title = "";
			}
			if (string.IsNullOrEmpty(res.Zycj))
			{
				res.Zycj = "0";
			}
			SQLiteHelper.SqliteConn.Insert(res);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003130 File Offset: 0x00001330
		public List<ResourcesModel> GetResByChapterIds(List<string> lstChapter)
		{
			if (lstChapter == null || lstChapter.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (string str in lstChapter)
			{
				list.Add("'" + str + "'");
			}
			string str2 = "(" + string.Join(",", list) + ")";
			string empty = string.Empty;
			string query = "SELECT * FROM resources WHERE ori_tree_code in " + str2 + " and ex_zycj = 11";
			return SQLiteHelper.SqliteConn.Query<ResourcesModel>(query, new object[0]);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031E8 File Offset: 0x000013E8
		public void SaveReslst(List<ResourcesModel> reslst)
		{
			if (reslst == null || reslst.Count == 0)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (ResourcesModel resourcesModel in reslst)
			{
				list.Add("'" + resourcesModel.ResID + "'");
			}
			string str = "(" + string.Join(",", list) + ")";
			string query = "DELETE FROM resources WHERE resid in " + str;
			SQLiteHelper.SqliteConn.Execute(query, new object[0]);
			foreach (ResourcesModel resourcesModel2 in reslst)
			{
				resourcesModel2.ID = null;
				if (string.IsNullOrEmpty(resourcesModel2.Title))
				{
					resourcesModel2.Title = "";
				}
				if (string.IsNullOrEmpty(resourcesModel2.Zycj))
				{
					resourcesModel2.Zycj = "0";
				}
			}
			SQLiteHelper.SqliteConn.InsertAll(reslst, true);
		}
	}
}
