using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Utilities;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000066 RID: 102
	public class MyResesViewModel : BaseModel
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00024296 File Offset: 0x00022496
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0002429E File Offset: 0x0002249E
		public bool ShowNoDataMessage
		{
			get
			{
				return this.mShowNoDataMessage;
			}
			set
			{
				this.mShowNoDataMessage = value;
				base.OnPropertyChanged<bool>(() => this.ShowNoDataMessage);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000242DC File Offset: 0x000224DC
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x000242E4 File Offset: 0x000224E4
		public SetPagingControl SetPagingControlCallBack { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000242ED File Offset: 0x000224ED
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000242F5 File Offset: 0x000224F5
		public List<UserResourceModel> AllMyUserResesList { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x000242FE File Offset: 0x000224FE
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00024306 File Offset: 0x00022506
		public List<ZyTypeModel> LstZyLx { get; set; } = new List<ZyTypeModel>();

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0002430F File Offset: 0x0002250F
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00024317 File Offset: 0x00022517
		public List<string> LstCurChapterID
		{
			get
			{
				return this.mlstCurChapterID;
			}
			set
			{
				this.mlstCurChapterID = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00024320 File Offset: 0x00022520
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00024328 File Offset: 0x00022528
		public string CurBookID { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00024331 File Offset: 0x00022531
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00024339 File Offset: 0x00022539
		public bool IsOnLine
		{
			get
			{
				return this.mIsOnLine;
			}
			set
			{
				this.mIsOnLine = value;
				base.OnPropertyChanged<bool>(() => this.IsOnLine);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00024377 File Offset: 0x00022577
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0002437F File Offset: 0x0002257F
		public ObservableCollection<UserResourceModel> MyUserResourcesList
		{
			get
			{
				return this.myUserResourcesList;
			}
			private set
			{
				this.myUserResourcesList = value;
				this.OnPropertyChanged<ObservableCollection<UserResourceModel>>(() => this.MyUserResourcesList);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x000243BD File Offset: 0x000225BD
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x000243C8 File Offset: 0x000225C8
		public bool IsAllChecked
		{
			get
			{
				return this.mIsAllChecked;
			}
			set
			{
				this.mIsAllChecked = value;
				base.OnPropertyChanged<bool>(() => this.IsAllChecked);
				if (this.MyUserResourcesList != null)
				{
					foreach (UserResourceModel userResourceModel in this.MyUserResourcesList)
					{
						userResourceModel.IsChecked = value;
					}
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00024458 File Offset: 0x00022658
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x00024460 File Offset: 0x00022660
		public string Keyword
		{
			get
			{
				return this.mKeyword;
			}
			set
			{
				this.mKeyword = value;
				base.OnPropertyChanged<string>(() => this.Keyword);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0002449E File Offset: 0x0002269E
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x000244A6 File Offset: 0x000226A6
		public int CurPageNum
		{
			get
			{
				return this.mCurPageNum;
			}
			set
			{
				this.mCurPageNum = value;
				base.OnPropertyChanged<int>(() => this.CurPageNum);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x000244E4 File Offset: 0x000226E4
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x000244EC File Offset: 0x000226EC
		public bool TitleSortAsc
		{
			get
			{
				return this.mTitleSortAsc;
			}
			set
			{
				this.mTitleSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.TitleSortAsc);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0002452A File Offset: 0x0002272A
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00024532 File Offset: 0x00022732
		public bool TitleSortDesc
		{
			get
			{
				return this.mTitleSortDesc;
			}
			set
			{
				this.mTitleSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.TitleSortDesc);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00024570 File Offset: 0x00022770
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00024578 File Offset: 0x00022778
		public bool PosSortAsc
		{
			get
			{
				return this.mPosSortAsc;
			}
			set
			{
				this.mPosSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.PosSortAsc);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000245B6 File Offset: 0x000227B6
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x000245BE File Offset: 0x000227BE
		public bool PosSortDesc
		{
			get
			{
				return this.mPosSortDesc;
			}
			set
			{
				this.mPosSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.PosSortDesc);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000245FC File Offset: 0x000227FC
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00024604 File Offset: 0x00022804
		public bool SizeSortAsc
		{
			get
			{
				return this.mSizeSortAsc;
			}
			set
			{
				this.mSizeSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.SizeSortAsc);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00024642 File Offset: 0x00022842
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0002464A File Offset: 0x0002284A
		public bool SizeSortDesc
		{
			get
			{
				return this.mSizeSortDesc;
			}
			set
			{
				this.mSizeSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.SizeSortDesc);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00024688 File Offset: 0x00022888
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00024690 File Offset: 0x00022890
		public int TotlePage
		{
			get
			{
				return this.mTotlePage;
			}
			set
			{
				this.mTotlePage = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00024699 File Offset: 0x00022899
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000246A1 File Offset: 0x000228A1
		public MyResourceParamter MyResParamter { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000246AA File Offset: 0x000228AA
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000246B2 File Offset: 0x000228B2
		public bool IsDrag
		{
			get
			{
				return this.bIsDrag;
			}
			set
			{
				this.bIsDrag = value;
				base.OnPropertyChanged<bool>(() => this.IsDrag);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000246F0 File Offset: 0x000228F0
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000246F8 File Offset: 0x000228F8
		public string MessageContent
		{
			get
			{
				return this.mMessageContent;
			}
			set
			{
				this.mMessageContent = value;
				base.OnPropertyChanged<string>(() => this.MessageContent);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00024738 File Offset: 0x00022938
		public void InitZyLxSelectState()
		{
			foreach (ZyTypeModel zyTypeModel in this.LstZyLx)
			{
				zyTypeModel.IsChecked = (string.IsNullOrEmpty(zyTypeModel.Id) > false);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00024798 File Offset: 0x00022998
		public void ReloadUserResources()
		{
			this.GetUserResources();
			this.RefreshDisplayUserRes();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000247A8 File Offset: 0x000229A8
		public void RefreshDisplayUserRes()
		{
			try
			{
				Application.Current.Dispatcher.Invoke(new Action(delegate()
				{
					this.IsAllChecked = false;
					if (this.MyUserResourcesList == null)
					{
						this.MyUserResourcesList = new ObservableCollection<UserResourceModel>();
						return;
					}
					this.MyUserResourcesList.Clear();
				}), new object[0]);
				if (this.AllMyUserResesList != null && this.AllMyUserResesList.Count != 0)
				{
					IEnumerable<UserResourceModel> lstUserResTemp = from a in this.AllMyUserResesList
					orderby a.EcryType descending, a.CreateTime descending
					select a;
					string text = string.Empty;
					string userResTypeID = this.GetResType();
					if (!string.IsNullOrEmpty(userResTypeID))
					{
						lstUserResTemp = from a in lstUserResTemp
						where a.Dzwjlx == userResTypeID
						select a;
						text = text + "Dzwjlx:" + userResTypeID;
					}
					if (!string.IsNullOrEmpty(this.Keyword))
					{
						lstUserResTemp = from a in lstUserResTemp
						where a.Title.Contains(this.Keyword)
						select a;
						text = text + ",Keyword:" + userResTypeID;
					}
					if (this.LstCurChapterID != null && this.LstCurChapterID.Count > 0)
					{
						lstUserResTemp = from res in lstUserResTemp
						where this.LstCurChapterID.Contains(res.OriTreeCode)
						select res;
						text = text + ",OriTreeCode:" + string.Join<UserResourceModel>(",", lstUserResTemp);
					}
					else if (!string.IsNullOrEmpty(this.CurBookID))
					{
						lstUserResTemp = from a in lstUserResTemp
						where a.Tbid == this.CurBookID
						select a;
						text = text + ",Tbid:" + this.CurBookID;
					}
					if (lstUserResTemp != null && lstUserResTemp.Count<UserResourceModel>() != 0)
					{
						if (this.TitleSortAsc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.Title
							select a;
							text += ",标题:升序";
						}
						else if (this.TitleSortDesc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.Title descending
							select a;
							text += ",标题:降序";
						}
						else if (this.PosSortAsc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.PosType
							select a;
							text += ",位置:升序";
						}
						else if (this.PosSortDesc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.PosType descending
							select a;
							text += ",位置:降序";
						}
						else if (this.SizeSortAsc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.FileSize
							select a;
							text += ",资源大小:升序";
						}
						else if (this.SizeSortDesc)
						{
							lstUserResTemp = from a in lstUserResTemp
							orderby a.FileSize descending
							select a;
							text += ",资源大小:降序";
						}
						int num = lstUserResTemp.Count<UserResourceModel>() % 20;
						this.TotlePage = ((num > 0) ? (lstUserResTemp.Count<UserResourceModel>() / 20 + 1) : (lstUserResTemp.Count<UserResourceModel>() / 20));
						text += string.Format(",总页码数:{0}", this.TotlePage);
						int count = (this.CurPageNum <= 1) ? 0 : ((this.CurPageNum - 1) * 20);
						if (this.CurPageNum > 1)
						{
							int curPageNum = this.CurPageNum;
						}
						lstUserResTemp = lstUserResTemp.Skip(count).Take(20);
						text += string.Format(",当前页码:{0}", this.CurPageNum);
						if (lstUserResTemp != null)
						{
							Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
							{
								foreach (UserResourceModel item in lstUserResTemp)
								{
									this.MyUserResourcesList.Add(item);
								}
							}), new object[0]);
						}
						TrackerManager.Tracker.OnEvent(new EventActivity
						{
							ActionId = "jx200143",
							Passive = "我的资源",
							FromPos = MyResesViewModel.mClassPath + ".RefreshDisplayUserRes",
							Params = text
						});
					}
				}
			}
			finally
			{
				SetPagingControl setPagingControlCallBack = this.SetPagingControlCallBack;
				if (setPagingControlCallBack != null)
				{
					setPagingControlCallBack();
				}
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00024C80 File Offset: 0x00022E80
		private string GetResType()
		{
			string result = string.Empty;
			IEnumerable<ZyTypeModel> enumerable = from a in this.LstZyLx
			where a.IsChecked
			select a;
			ZyTypeModel zyTypeModel = (enumerable != null) ? enumerable.FirstOrDefault<ZyTypeModel>() : null;
			if (zyTypeModel != null)
			{
				result = zyTypeModel.Id;
			}
			return result;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00024CD8 File Offset: 0x00022ED8
		public void GetUserResources()
		{
			if (this.AllMyUserResesList == null)
			{
				this.AllMyUserResesList = new List<UserResourceModel>();
			}
			else
			{
				this.AllMyUserResesList.Clear();
			}
			List<UserResourceModel> userResList = UserResourcesManager.Instance.GetUserResList(this.IsOnLine, CommonParamter.Instance.LoginUserId, "", false, true, false);
			if (userResList == null)
			{
				throw new Exception("获取平台用户资源失败。");
			}
			this.AllMyUserResesList.AddRange(userResList);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00024D42 File Offset: 0x00022F42
		public void InsertNewUserResource(UserResourceModel userResMdl)
		{
			List<UserResourceModel> allMyUserResesList = this.AllMyUserResesList;
			if (allMyUserResesList == null)
			{
				return;
			}
			allMyUserResesList.Insert(0, userResMdl);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00024D58 File Offset: 0x00022F58
		public bool ConfirmExistSyncUserResources(IEnumerable<UserResourceModel> lstSyncUserRes, ref List<UserResourceModel> lstUploadUserRes, ref List<UserResourceModel> lstDownUserRes, ref List<string> lstDelid)
		{
			bool result = false;
			if (lstSyncUserRes == null || lstSyncUserRes.Count<UserResourceModel>() < 1)
			{
				return result;
			}
			List<UserResourceModel> list = new List<UserResourceModel>();
			List<string> list2 = new List<string>();
			foreach (UserResourceModel userResourceModel in lstSyncUserRes)
			{
				switch (userResourceModel.PosType)
				{
				case 1:
					lstUploadUserRes.Add(userResourceModel);
					break;
				case 2:
					lstDownUserRes.Add(userResourceModel);
					break;
				case 3:
					list.Add(userResourceModel);
					list2.Add(userResourceModel.ID);
					break;
				}
			}
			lstDelid.AddRange(list2);
			List<UserResourceModel> userResesByResIDs = this.GetUserResesByResIDs(list2);
			if (userResesByResIDs != null)
			{
				using (List<UserResourceModel>.Enumerator enumerator2 = userResesByResIDs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						UserResourceModel yunUserResMdl = enumerator2.Current;
						UserResourceModel userResourceModel2 = (from t in list
						where t.ID == yunUserResMdl.ID
						select t).FirstOrDefault<UserResourceModel>();
						if (userResourceModel2 != null)
						{
							int num = yunUserResMdl.ModifyTime.CompareTo(userResourceModel2.ModifyTime);
							if (num > 0)
							{
								lstDownUserRes.Add(yunUserResMdl);
							}
							else if (num < 0)
							{
								UtilityHelper.IsUserResChanged(userResourceModel2);
								lstUploadUserRes.Add(userResourceModel2);
							}
							else if (UtilityHelper.IsUserResChanged(userResourceModel2))
							{
								lstUploadUserRes.Add(userResourceModel2);
							}
						}
						if (lstDelid.Contains(yunUserResMdl.ID))
						{
							lstDelid.Remove(yunUserResMdl.ID);
						}
					}
				}
			}
			if ((lstUploadUserRes != null && lstUploadUserRes.Count<UserResourceModel>() > 0) || (lstDownUserRes != null && lstDownUserRes.Count<UserResourceModel>() > 0) || (lstDelid != null && lstDelid.Count > 0))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00024F38 File Offset: 0x00023138
		public List<UserResourceModel> GetUserResesByResIDs(List<string> lstUserResIDs)
		{
			List<UserResourceModel> result = null;
			if (lstUserResIDs == null || lstUserResIDs.Count < 1)
			{
				return result;
			}
			string text = string.Empty;
			foreach (string str in lstUserResIDs)
			{
				text += str;
				text += ",";
			}
			text = text.Substring(0, text.Length - 1);
			UserResourceJsonModel userResesJsonModelByResIDs = this.GetUserResesJsonModelByResIDs(text);
			return (userResesJsonModelByResIDs != null) ? userResesJsonModelByResIDs.UserResourcesList : null;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00024FD0 File Offset: 0x000231D0
		public UserResourceJsonModel GetUserResesJsonModelByResIDs(string strUserResIDs)
		{
			UserResourceJsonModel result = null;
			if (string.IsNullOrEmpty(strUserResIDs))
			{
				return result;
			}
			string jsonStr = WebRequestHelper.HttpGetRequest(string.Format("resuser/list.json?resIds={0}", strUserResIDs), new int?(30000), 0, false);
			return this.mJsonOper.JsonsDeserialize<UserResourceJsonModel>(jsonStr);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00025014 File Offset: 0x00023214
		public void DeleteUserResource(List<UserResourceModel> lstUserRes, bool isDelCloud = true)
		{
			if (lstUserRes == null || lstUserRes.Count == 0)
			{
				return;
			}
			if (isDelCloud)
			{
				List<string> list = (from a in lstUserRes
				where a.PosType == 2 || a.PosType == 3
				select a.ID).ToList<string>();
				if (list != null && list.Count > 0)
				{
					string arg = string.Join(",", list);
					WebRequestHelper.WebSubmitDataRequest(string.Format("resuser/remove.json?id={0}", arg));
				}
			}
			List<string> lstids = (from a in lstUserRes
			select a.ID).ToList<string>();
			this.mUserResesDA.DelUserResByids(lstids);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000250E0 File Offset: 0x000232E0
		public List<UserResourceModel> GetCheckedUserResMdls()
		{
			List<UserResourceModel> list = new List<UserResourceModel>();
			ObservableCollection<UserResourceModel> observableCollection = this.MyUserResourcesList;
			IEnumerable<UserResourceModel> enumerable;
			if (observableCollection == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = from o in observableCollection
				where o.IsChecked
				select o;
			}
			IEnumerable<UserResourceModel> enumerable2 = enumerable;
			if (enumerable2 != null && enumerable2.Count<UserResourceModel>() > 0)
			{
				list.AddRange(enumerable2);
			}
			return list;
		}

		// Token: 0x0400039A RID: 922
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"ViewModel",
			"MyResesViewModel"
		});

		// Token: 0x0400039B RID: 923
		private const int PAGESIZE = 20;

		// Token: 0x0400039C RID: 924
		private UserResourceDataAccess mUserResesDA = new UserResourceDataAccess();

		// Token: 0x0400039D RID: 925
		private Md5Helper mMd5Oper = new Md5Helper();

		// Token: 0x0400039E RID: 926
		private JsonHelper mJsonOper = new JsonHelper();

		// Token: 0x0400039F RID: 927
		private bool mShowNoDataMessage;

		// Token: 0x040003A0 RID: 928
		private string mKeyword = string.Empty;

		// Token: 0x040003A1 RID: 929
		private int mCurPageNum = 1;

		// Token: 0x040003A2 RID: 930
		private int mTotlePage;

		// Token: 0x040003A3 RID: 931
		private bool mResFormatAll = true;

		// Token: 0x040003A4 RID: 932
		private bool mResFormatTxt;

		// Token: 0x040003A5 RID: 933
		private bool mResFormatPic;

		// Token: 0x040003A6 RID: 934
		private bool mResFormatAudio;

		// Token: 0x040003A7 RID: 935
		private bool mResFormatVideo;

		// Token: 0x040003A8 RID: 936
		private bool mResFormatFlash;

		// Token: 0x040003A9 RID: 937
		private bool mResFormatKJ;

		// Token: 0x040003AA RID: 938
		private bool mResFormatWK;

		// Token: 0x040003AB RID: 939
		private List<string> mlstCurChapterID = new List<string>();

		// Token: 0x040003AC RID: 940
		private string mMessageContent = "没有符合条件的记录";

		// Token: 0x040003AD RID: 941
		private bool mTitleSortAsc;

		// Token: 0x040003AE RID: 942
		private bool mTitleSortDesc;

		// Token: 0x040003AF RID: 943
		private bool mPosSortAsc;

		// Token: 0x040003B0 RID: 944
		private bool mPosSortDesc;

		// Token: 0x040003B1 RID: 945
		private bool mSizeSortAsc;

		// Token: 0x040003B2 RID: 946
		private bool mSizeSortDesc;

		// Token: 0x040003B3 RID: 947
		private bool mIsAllChecked;

		// Token: 0x040003B4 RID: 948
		private bool mIsOnLine;

		// Token: 0x040003B5 RID: 949
		private bool bIsDrag;

		// Token: 0x040003BA RID: 954
		private ObservableCollection<UserResourceModel> myUserResourcesList = new ObservableCollection<UserResourceModel>();
	}
}
