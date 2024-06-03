using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using JXP.Data;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200006F RID: 111
	public class ShareBookViewModel : BaseModel
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000267FD File Offset: 0x000249FD
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x00026805 File Offset: 0x00024A05
		public SetPagingCount SetPagingCountCallBack { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0002680E File Offset: 0x00024A0E
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00026816 File Offset: 0x00024A16
		public int CurPage
		{
			get
			{
				return this.mCurPage;
			}
			set
			{
				this.mCurPage = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0002681F File Offset: 0x00024A1F
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00026827 File Offset: 0x00024A27
		public MetaList XDLst
		{
			get
			{
				return this.mXDLst;
			}
			set
			{
				this.mXDLst = value;
				base.OnPropertyChanged<MetaList>(() => this.XDLst);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00026865 File Offset: 0x00024A65
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0002686D File Offset: 0x00024A6D
		public MetaModel SelectXD
		{
			get
			{
				return this.mSelectXD;
			}
			set
			{
				this.mSelectXD = value;
				base.OnPropertyChanged<MetaModel>(() => this.SelectXD);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000268AB File Offset: 0x00024AAB
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x000268B3 File Offset: 0x00024AB3
		public MetaList XKLst
		{
			get
			{
				return this.mXKLst;
			}
			set
			{
				this.mXKLst = value;
				base.OnPropertyChanged<MetaList>(() => this.XKLst);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000268F1 File Offset: 0x00024AF1
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x000268F9 File Offset: 0x00024AF9
		public MetaModel SelectXK
		{
			get
			{
				return this.mSelectXK;
			}
			set
			{
				this.mSelectXK = value;
				base.OnPropertyChanged<MetaModel>(() => this.SelectXK);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00026937 File Offset: 0x00024B37
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0002693F File Offset: 0x00024B3F
		public MetaList LYLst
		{
			get
			{
				return this.mLYLst;
			}
			set
			{
				this.mLYLst = value;
				base.OnPropertyChanged<MetaList>(() => this.LYLst);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002697D File Offset: 0x00024B7D
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00026985 File Offset: 0x00024B85
		public MetaModel SelectLY
		{
			get
			{
				return this.mSelectLY;
			}
			set
			{
				this.mSelectLY = value;
				base.OnPropertyChanged<MetaModel>(() => this.SelectLY);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000269C3 File Offset: 0x00024BC3
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x000269CB File Offset: 0x00024BCB
		public int ResultCount
		{
			get
			{
				return this.mResultCount;
			}
			set
			{
				this.mResultCount = value;
				base.OnPropertyChanged<int>(() => this.ResultCount);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00026A09 File Offset: 0x00024C09
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00026A11 File Offset: 0x00024C11
		public ShareBookDataModelList ShareBookDataCollection
		{
			get
			{
				return this.mShareBookDataCollection;
			}
			set
			{
				this.mShareBookDataCollection = value;
				base.OnPropertyChanged<ShareBookDataModelList>(() => this.ShareBookDataCollection);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00026A4F File Offset: 0x00024C4F
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00026A57 File Offset: 0x00024C57
		public Visibility PagingVisible
		{
			get
			{
				return this.mPagingVisible;
			}
			set
			{
				this.mPagingVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.PagingVisible);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00026A95 File Offset: 0x00024C95
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00026A9D File Offset: 0x00024C9D
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00026ADB File Offset: 0x00024CDB
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00026AE3 File Offset: 0x00024CE3
		public bool ShowKeyword
		{
			get
			{
				return this.mShowKeyword;
			}
			set
			{
				this.mShowKeyword = value;
				base.OnPropertyChanged<bool>(() => this.ShowKeyword);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00026B21 File Offset: 0x00024D21
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00026B29 File Offset: 0x00024D29
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00026B67 File Offset: 0x00024D67
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00026B6F File Offset: 0x00024D6F
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

		// Token: 0x060007D2 RID: 2002 RVA: 0x00026C25 File Offset: 0x00024E25
		public void InitData()
		{
			this.Keyword = string.Empty;
			this.ShowNoDataMessage = true;
			this.MessageContent = string.Empty;
			this.mCurPage = 1;
			this.ShareBookDataCollection = new ShareBookDataModelList();
			this.ResultCount = 0;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00026C60 File Offset: 0x00024E60
		public void GetShareBookListAsync()
		{
			ShareBookViewModel.<GetShareBookListAsync>d__63 <GetShareBookListAsync>d__;
			<GetShareBookListAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<GetShareBookListAsync>d__.<>4__this = this;
			<GetShareBookListAsync>d__.<>1__state = -1;
			<GetShareBookListAsync>d__.<>t__builder.Start<ShareBookViewModel.<GetShareBookListAsync>d__63>(ref <GetShareBookListAsync>d__);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00026C98 File Offset: 0x00024E98
		private Dictionary<string, string> GetPostShareBookLstParameter(string userId, string bookIds)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["textbookIds"] = bookIds;
			dictionary["userId"] = userId;
			if (!string.IsNullOrEmpty(this.Keyword))
			{
				dictionary["keyWords"] = this.Keyword;
			}
			dictionary["rangeType"] = "4";
			if (this.SelectXD.Id != "-100")
			{
				dictionary["rkxd"] = this.SelectXD.Id;
			}
			if (this.SelectXK.Id != "-100")
			{
				dictionary["zxxkc"] = this.SelectXK.Id;
			}
			if (this.mSelectLY.Id != "-100")
			{
				dictionary["publisher"] = this.mSelectLY.Id;
			}
			dictionary["_APP_P_NO"] = this.mCurPage.ToString();
			return dictionary;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00026D90 File Offset: 0x00024F90
		private string GetDownloadBookid(string userId)
		{
			TransferTextbookModelList userDownloadedTextbooks = this.mTextBookDA.GetUserDownloadedTextbooks(userId);
			if (userDownloadedTextbooks == null)
			{
				return null;
			}
			IEnumerable<string> values = from a in userDownloadedTextbooks
			select a.TextBookId;
			return string.Join(",", values);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public void InitComboxData(string strInitXD = "", string strInitXK = "", string strInitLY = "")
		{
			try
			{
				this.XDLst = CommonHandle.GetAuthorizeConstData(1007, "学段", CommonParamter.Instance.lstAuthorizeXD);
				if (!string.IsNullOrEmpty(strInitXD) && this.XDLst.Any((MetaModel a) => a.Id == strInitXD))
				{
					this.SelectXD = (from a in this.XDLst
					where a.Id == strInitXD
					select a).FirstOrDefault<MetaModel>();
				}
				else
				{
					this.SelectXD = this.XDLst.FirstOrDefault<MetaModel>();
				}
				if (this.SelectXD.Id != "-100")
				{
					this.XKLst = this.RefreshOnlyXKsByXD(this.SelectXD.Id);
				}
				else
				{
					this.XKLst = CommonHandle.GetAuthorizeConstData(1009, "学科", CommonParamter.Instance.lstAuthorizeXK);
				}
				if (!string.IsNullOrEmpty(strInitXK) && this.XKLst.Any((MetaModel a) => a.Id == strInitXK))
				{
					this.SelectXK = (from a in this.XKLst
					where a.Id == strInitXK
					select a).FirstOrDefault<MetaModel>();
				}
				else
				{
					this.SelectXK = this.XKLst.FirstOrDefault<MetaModel>();
				}
				this.LYLst = CommonHandle.GetAuthorizeConstData(1012, "出版社", CommonParamter.Instance.lstAuthorizePublisher);
				if (!string.IsNullOrEmpty(strInitLY) && this.LYLst.Any((MetaModel a) => a.Id == strInitLY))
				{
					this.SelectLY = (from a in this.LYLst
					where a.Id == strInitLY
					select a).FirstOrDefault<MetaModel>();
				}
				else
				{
					this.SelectLY = this.LYLst.FirstOrDefault<MetaModel>();
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "初始化常量失败，等待完成后刷新：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00026FE8 File Offset: 0x000251E8
		internal MetaList RefreshOnlyXKsByXD(string xdId)
		{
			MetaList authorizeConstData = CommonHandle.GetAuthorizeConstData(1009, "学科", CommonParamter.Instance.lstAuthorizeXK);
			MetaList metaList = new MetaList();
			ConstDatasHelper constDatasHelper = new ConstDatasHelper();
			Dictionary<string, string> xds = constDatasHelper.GetXDs();
			if (!string.IsNullOrEmpty(xdId) && xds != null && xds.ContainsKey(xdId))
			{
				IEnumerable<MetaModel> enumerable = from o in authorizeConstData
				where o.Id == "-100"
				select o;
				if (enumerable != null && enumerable.Count<MetaModel>() > 0)
				{
					metaList.Add(enumerable.First<MetaModel>());
				}
				Dictionary<string, string> xdcascadeXK = constDatasHelper.GetXDCascadeXK(xdId);
				if (xdcascadeXK != null && xdcascadeXK.Count > 0)
				{
					foreach (MetaModel metaModel in authorizeConstData)
					{
						if (xdcascadeXK.ContainsKey(metaModel.Id))
						{
							metaList.Add(metaModel);
						}
					}
				}
				IEnumerable<MetaModel> enumerable2 = from o in authorizeConstData
				where o.Id == "91"
				select o;
				if (enumerable2 != null && enumerable2.Count<MetaModel>() > 0)
				{
					if (metaList.FirstOrDefault((MetaModel m) => m.Id == "91") == null)
					{
						metaList.Add(enumerable2.First<MetaModel>());
					}
				}
			}
			else
			{
				metaList = authorizeConstData;
			}
			return metaList;
		}

		// Token: 0x040003DF RID: 991
		private TransferTextBookDataAccess mTextBookDA = new TransferTextBookDataAccess();

		// Token: 0x040003E0 RID: 992
		private MetaList mXDLst;

		// Token: 0x040003E1 RID: 993
		private MetaModel mSelectXD = new MetaModel
		{
			Id = "-100"
		};

		// Token: 0x040003E2 RID: 994
		private MetaList mXKLst;

		// Token: 0x040003E3 RID: 995
		private MetaModel mSelectXK = new MetaModel
		{
			Id = "-100"
		};

		// Token: 0x040003E4 RID: 996
		private MetaList mLYLst;

		// Token: 0x040003E5 RID: 997
		private MetaModel mSelectLY;

		// Token: 0x040003E6 RID: 998
		private int mResultCount;

		// Token: 0x040003E7 RID: 999
		private string mKeyword = string.Empty;

		// Token: 0x040003E8 RID: 1000
		private bool mShowKeyword = true;

		// Token: 0x040003E9 RID: 1001
		private bool mShowNoDataMessage = true;

		// Token: 0x040003EA RID: 1002
		private string mMessageContent = string.Empty;

		// Token: 0x040003EB RID: 1003
		private ShareBookDataModelList mShareBookDataCollection;

		// Token: 0x040003EC RID: 1004
		private int mCurPage = 1;

		// Token: 0x040003ED RID: 1005
		private Visibility mPagingVisible;
	}
}
