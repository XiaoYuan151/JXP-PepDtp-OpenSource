using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JXP.Models;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000075 RID: 117
	public class SubscibeBookViewModel : BaseModel
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00028EF4 File Offset: 0x000270F4
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00028EFC File Offset: 0x000270FC
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
				if (value)
				{
					this.PagingVisible = Visibility.Hidden;
					return;
				}
				this.PagingVisible = Visibility.Visible;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00028F57 File Offset: 0x00027157
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00028F5F File Offset: 0x0002715F
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

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00028F9D File Offset: 0x0002719D
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00028FA5 File Offset: 0x000271A5
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00028FE3 File Offset: 0x000271E3
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00028FEB File Offset: 0x000271EB
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00029029 File Offset: 0x00027229
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00029031 File Offset: 0x00027231
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002906F File Offset: 0x0002726F
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00029077 File Offset: 0x00027277
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000290B5 File Offset: 0x000272B5
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x000290BD File Offset: 0x000272BD
		public bool ScoreAsc
		{
			get
			{
				return this.mScoreAsc;
			}
			set
			{
				this.mScoreAsc = value;
				base.OnPropertyChanged<bool>(() => this.ScoreAsc);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000290FB File Offset: 0x000272FB
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00029103 File Offset: 0x00027303
		public bool ScoreDesc
		{
			get
			{
				return this.mScoreDesc;
			}
			set
			{
				this.mScoreDesc = value;
				base.OnPropertyChanged<bool>(() => this.ScoreDesc);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00029141 File Offset: 0x00027341
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00029149 File Offset: 0x00027349
		public string BookID { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00029152 File Offset: 0x00027352
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0002915A File Offset: 0x0002735A
		public string UserID { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00029163 File Offset: 0x00027363
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0002916B File Offset: 0x0002736B
		public string OrgID { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00029174 File Offset: 0x00027374
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0002917C File Offset: 0x0002737C
		public SetPagingCount SetPagingCountCallBack { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00029185 File Offset: 0x00027385
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0002918D File Offset: 0x0002738D
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

		// Token: 0x0600087B RID: 2171 RVA: 0x00029196 File Offset: 0x00027396
		public void InitMySubscibeData()
		{
			this.ShowNoDataMessage = false;
			this.PagingVisible = Visibility.Hidden;
			this.mCurPage = 1;
			this.InitSort();
			this.GetMySubscibeListAsync();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000291BC File Offset: 0x000273BC
		public void GetMySubscibeListAsync()
		{
			SubscibeBookViewModel.<GetMySubscibeListAsync>d__53 <GetMySubscibeListAsync>d__;
			<GetMySubscibeListAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<GetMySubscibeListAsync>d__.<>4__this = this;
			<GetMySubscibeListAsync>d__.<>1__state = -1;
			<GetMySubscibeListAsync>d__.<>t__builder.Start<SubscibeBookViewModel.<GetMySubscibeListAsync>d__53>(ref <GetMySubscibeListAsync>d__);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000291F4 File Offset: 0x000273F4
		private Dictionary<string, string> GetPostShareBookLstParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["textbookId"] = this.BookID;
			dictionary["userId"] = this.UserID;
			string sortFilter = this.GetSortFilter();
			if (!string.IsNullOrEmpty(sortFilter))
			{
				dictionary["sortOrder"] = sortFilter;
			}
			dictionary["_APP_P_NO"] = this.mCurPage.ToString();
			return dictionary;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002925C File Offset: 0x0002745C
		public Task<bool> DelShareTextbook(string id)
		{
			SubscibeBookViewModel.<DelShareTextbook>d__55 <DelShareTextbook>d__;
			<DelShareTextbook>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<DelShareTextbook>d__.<>4__this = this;
			<DelShareTextbook>d__.id = id;
			<DelShareTextbook>d__.<>1__state = -1;
			<DelShareTextbook>d__.<>t__builder.Start<SubscibeBookViewModel.<DelShareTextbook>d__55>(ref <DelShareTextbook>d__);
			return <DelShareTextbook>d__.<>t__builder.Task;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000292A7 File Offset: 0x000274A7
		public void InitSort()
		{
			this.TitleSortAsc = false;
			this.TitleSortDesc = false;
			this.ScoreDesc = false;
			this.ScoreAsc = false;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000292C8 File Offset: 0x000274C8
		private string GetSortFilter()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.TitleSortAsc)
			{
				stringBuilder.Append("ctree_name ASC,");
			}
			else if (this.TitleSortDesc)
			{
				stringBuilder.Append("ctree_name DESC,");
			}
			if (this.ScoreAsc)
			{
				stringBuilder.Append("score ASC,");
			}
			else if (this.ScoreDesc)
			{
				stringBuilder.Append("score DESC,");
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}

		// Token: 0x0400042D RID: 1069
		private bool mShowNoDataMessage;

		// Token: 0x0400042E RID: 1070
		private string mMessageContent = "没有符合条件的记录";

		// Token: 0x0400042F RID: 1071
		private ShareBookDataModelList mShareBookDataCollection;

		// Token: 0x04000430 RID: 1072
		private int mCurPage = 1;

		// Token: 0x04000431 RID: 1073
		private Visibility mPagingVisible = Visibility.Hidden;

		// Token: 0x04000432 RID: 1074
		private bool mTitleSortAsc;

		// Token: 0x04000433 RID: 1075
		private bool mTitleSortDesc;

		// Token: 0x04000434 RID: 1076
		private bool mScoreAsc;

		// Token: 0x04000435 RID: 1077
		private bool mScoreDesc;
	}
}
