using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JXP.Models;
using JXP.PepDtp.Common;
using pep.sdk.utility.Paramter;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000067 RID: 103
	public class MyResSelectByWorkViewModel : BaseModel
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00025248 File Offset: 0x00023448
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00025250 File Offset: 0x00023450
		public SelectMyResParamter InitParamter { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00025259 File Offset: 0x00023459
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00025261 File Offset: 0x00023461
		public ObservableCollection<SelectUserRes> ShowData
		{
			get
			{
				return this.mShowData;
			}
			set
			{
				this.mShowData = value;
				base.OnPropertyChanged<ObservableCollection<SelectUserRes>>(() => this.ShowData);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0002529F File Offset: 0x0002349F
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x000252A7 File Offset: 0x000234A7
		public MetaList ZylxLst
		{
			get
			{
				return this.mZylxLst;
			}
			set
			{
				this.mZylxLst = value;
				base.OnPropertyChanged<MetaList>(() => this.ZylxLst);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000252E5 File Offset: 0x000234E5
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x000252ED File Offset: 0x000234ED
		public MetaModel SelectZylx
		{
			get
			{
				return this.mSelectZylx;
			}
			set
			{
				this.mSelectZylx = value;
				base.OnPropertyChanged<MetaModel>(() => this.SelectZylx);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0002532B File Offset: 0x0002352B
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x00025333 File Offset: 0x00023533
		public MetaList ZyFormatLst
		{
			get
			{
				return this.mZyFormat;
			}
			set
			{
				this.mZyFormat = value;
				base.OnPropertyChanged<MetaList>(() => this.ZyFormatLst);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00025371 File Offset: 0x00023571
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00025379 File Offset: 0x00023579
		public MetaModel SelectZyFormat
		{
			get
			{
				return this.mSelectZyFormat;
			}
			set
			{
				this.mSelectZyFormat = value;
				base.OnPropertyChanged<MetaModel>(() => this.SelectZyFormat);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x000253B7 File Offset: 0x000235B7
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x000253BF File Offset: 0x000235BF
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x000253FD File Offset: 0x000235FD
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00025405 File Offset: 0x00023605
		public int CurPageNum { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0002540E File Offset: 0x0002360E
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00025416 File Offset: 0x00023616
		public int TotlePage { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0002541F File Offset: 0x0002361F
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00025427 File Offset: 0x00023627
		public SetPagingControl SetPagingControlCallBack { get; set; }

		// Token: 0x06000760 RID: 1888 RVA: 0x00025430 File Offset: 0x00023630
		public Task<List<SelectUserRes>> InitDataAsync()
		{
			MyResSelectByWorkViewModel.<InitDataAsync>d__43 <InitDataAsync>d__;
			<InitDataAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<SelectUserRes>>.Create();
			<InitDataAsync>d__.<>4__this = this;
			<InitDataAsync>d__.<>1__state = -1;
			<InitDataAsync>d__.<>t__builder.Start<MyResSelectByWorkViewModel.<InitDataAsync>d__43>(ref <InitDataAsync>d__);
			return <InitDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00025473 File Offset: 0x00023673
		private void InitZylx()
		{
			this.ZylxLst = CommonHandle.GetConstData(1020, "全部");
			MetaList zylxLst = this.ZylxLst;
			this.SelectZylx = ((zylxLst != null) ? zylxLst.FirstOrDefault<MetaModel>() : null);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000254A2 File Offset: 0x000236A2
		private void InitZyFormat()
		{
			this.ZyFormatLst = CommonHandle.GetConstData(1014, "全部");
			MetaList zyFormatLst = this.ZyFormatLst;
			this.SelectZyFormat = ((zyFormatLst != null) ? zyFormatLst.FirstOrDefault<MetaModel>() : null);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000254D4 File Offset: 0x000236D4
		private Task<List<SelectUserRes>> GetCloudUserRes()
		{
			MyResSelectByWorkViewModel.<GetCloudUserRes>d__46 <GetCloudUserRes>d__;
			<GetCloudUserRes>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<SelectUserRes>>.Create();
			<GetCloudUserRes>d__.<>1__state = -1;
			<GetCloudUserRes>d__.<>t__builder.Start<MyResSelectByWorkViewModel.<GetCloudUserRes>d__46>(ref <GetCloudUserRes>d__);
			return <GetCloudUserRes>d__.<>t__builder.Task;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00025510 File Offset: 0x00023710
		public void FilterData()
		{
			try
			{
				ObservableCollection<SelectUserRes> showData;
				if ((showData = this.ShowData) == null)
				{
					showData = (this.ShowData = new ObservableCollection<SelectUserRes>());
				}
				this.ShowData = showData;
				this.ShowData.Clear();
				if (this.mAllRes != null && this.mAllRes.Count != 0)
				{
					IEnumerable<SelectUserRes> enumerable = from a in this.mAllRes
					orderby a.CreateTime descending
					select a;
					if (this.SelectZylx != null && this.SelectZylx.Id != "-100")
					{
						enumerable = from a in enumerable
						where a.ExZynrlID == this.SelectZylx.Id
						select a;
					}
					if (this.SelectZyFormat != null && this.SelectZyFormat.Id != "-100")
					{
						enumerable = from a in enumerable
						where a.Dzwjlx == this.SelectZyFormat.Id
						select a;
					}
					SelectMyResParamter initParamter = this.InitParamter;
					if (!string.IsNullOrEmpty((initParamter != null) ? initParamter.ChapterId : null))
					{
						enumerable = enumerable.Where(delegate(SelectUserRes a)
						{
							string oriTreeCode = a.OriTreeCode;
							SelectMyResParamter initParamter3 = this.InitParamter;
							return oriTreeCode == ((initParamter3 != null) ? initParamter3.ChapterId : null);
						});
					}
					else
					{
						SelectMyResParamter initParamter2 = this.InitParamter;
						if (!string.IsNullOrEmpty((initParamter2 != null) ? initParamter2.BookId : null))
						{
							enumerable = enumerable.Where(delegate(SelectUserRes a)
							{
								string oriTreeCode = a.OriTreeCode;
								SelectMyResParamter initParamter3 = this.InitParamter;
								return oriTreeCode == ((initParamter3 != null) ? initParamter3.BookId : null);
							});
						}
					}
					if (enumerable != null && enumerable.Count<SelectUserRes>() != 0)
					{
						int num = enumerable.Count<SelectUserRes>() % 9;
						this.TotlePage = ((num > 0) ? (enumerable.Count<SelectUserRes>() / 9 + 1) : (enumerable.Count<SelectUserRes>() / 9));
						int count = (this.CurPageNum <= 1) ? 0 : ((this.CurPageNum - 1) * 9);
						if (this.CurPageNum > 1)
						{
							int curPageNum = this.CurPageNum;
						}
						enumerable = enumerable.Skip(count).Take(9);
						foreach (SelectUserRes item in enumerable)
						{
							this.ShowData.Add(item);
						}
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

		// Token: 0x040003BC RID: 956
		private const int PAGESIZE = 9;

		// Token: 0x040003BD RID: 957
		private MetaList mZylxLst;

		// Token: 0x040003BE RID: 958
		private MetaModel mSelectZylx;

		// Token: 0x040003BF RID: 959
		private MetaList mZyFormat;

		// Token: 0x040003C0 RID: 960
		private MetaModel mSelectZyFormat;

		// Token: 0x040003C1 RID: 961
		public List<SelectUserRes> mAllRes = new List<SelectUserRes>();

		// Token: 0x040003C2 RID: 962
		private bool mShowNoDataMessage;

		// Token: 0x040003C3 RID: 963
		private ObservableCollection<SelectUserRes> mShowData;

		// Token: 0x040003C4 RID: 964
		private List<string> mLstFormate = new List<string>
		{
			".doc",
			".docx",
			".pdf",
			".jpeg",
			".bmp",
			".png",
			".jpg",
			".mp4",
			".mp3"
		};
	}
}
