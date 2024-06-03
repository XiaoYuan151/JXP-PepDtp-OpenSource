using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using JXP.DataAnalytics.Bootstrap;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000071 RID: 113
	public class SharedResourceViewModel : BaseModel
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00027232 File Offset: 0x00025432
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0002723A File Offset: 0x0002543A
		public string TextBookId { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00027243 File Offset: 0x00025443
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0002724B File Offset: 0x0002544B
		public List<string> LstChapterId { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00027254 File Offset: 0x00025454
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0002725C File Offset: 0x0002545C
		public string UserId { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00027265 File Offset: 0x00025465
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0002726D File Offset: 0x0002546D
		public int TotalPages { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00027276 File Offset: 0x00025476
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0002727E File Offset: 0x0002547E
		public int CurrentPage { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00027287 File Offset: 0x00025487
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0002728F File Offset: 0x0002548F
		public SharedResourceViewModel.SetPageCnt SetPageCntCallback { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00027298 File Offset: 0x00025498
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x000272A0 File Offset: 0x000254A0
		public string SelectedResEx_zynrlx { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x000272A9 File Offset: 0x000254A9
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x000272B1 File Offset: 0x000254B1
		public string SelectedResDzwjlx { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000272BA File Offset: 0x000254BA
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x000272C2 File Offset: 0x000254C2
		public List<ZyTypeModel> LstZynrlx { get; set; } = new List<ZyTypeModel>();

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000272CB File Offset: 0x000254CB
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x000272D3 File Offset: 0x000254D3
		public List<ZyTypeModel> LstZygs { get; set; } = new List<ZyTypeModel>();

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000272DC File Offset: 0x000254DC
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000272E4 File Offset: 0x000254E4
		public MainMenuEnums ResourcesType { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000272ED File Offset: 0x000254ED
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x000272F5 File Offset: 0x000254F5
		public Visibility KeywordSearchVisible
		{
			get
			{
				return this.mKeywordSearchVisible;
			}
			set
			{
				this.mKeywordSearchVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.KeywordSearchVisible);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00027333 File Offset: 0x00025533
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002733B File Offset: 0x0002553B
		public Visibility KeywordSearchBackVisible
		{
			get
			{
				return this.mKeywordSearchBackVisible;
			}
			set
			{
				this.mKeywordSearchBackVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.KeywordSearchBackVisible);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00027379 File Offset: 0x00025579
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00027381 File Offset: 0x00025581
		public Visibility KeywordSearchHaveResultVisible
		{
			get
			{
				return this.mKeywordSearchHaveResultVisible;
			}
			set
			{
				this.mKeywordSearchHaveResultVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.KeywordSearchHaveResultVisible);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000273BF File Offset: 0x000255BF
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000273C7 File Offset: 0x000255C7
		public Visibility KeywordSearchNoResultVisible
		{
			get
			{
				return this.mKeywordSearchNoResultVisible;
			}
			set
			{
				this.mKeywordSearchNoResultVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.KeywordSearchNoResultVisible);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00027405 File Offset: 0x00025605
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002740D File Offset: 0x0002560D
		public string FindKeywordCount
		{
			get
			{
				return this.mFindKeywordCount;
			}
			set
			{
				this.mFindKeywordCount = value;
				base.OnPropertyChanged<string>(() => this.FindKeywordCount);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002744B File Offset: 0x0002564B
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00027453 File Offset: 0x00025653
		public string TitleText
		{
			get
			{
				return this.mTitleText;
			}
			set
			{
				this.mTitleText = value;
				base.OnPropertyChanged<string>(() => this.TitleText);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00027491 File Offset: 0x00025691
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00027499 File Offset: 0x00025699
		public bool CloudResCheck
		{
			get
			{
				return this.mCloudResCheck;
			}
			set
			{
				this.mCloudResCheck = value;
				base.OnPropertyChanged<bool>(() => this.CloudResCheck);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000274D7 File Offset: 0x000256D7
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000274DF File Offset: 0x000256DF
		public bool ShareResCheck
		{
			get
			{
				return this.mShareResCheck;
			}
			set
			{
				this.mShareResCheck = value;
				base.OnPropertyChanged<bool>(() => this.ShareResCheck);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002751D File Offset: 0x0002571D
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00027525 File Offset: 0x00025725
		public Visibility SharResSelectTypeVisible
		{
			get
			{
				return this.mSharResSelectTypeVisible;
			}
			set
			{
				this.mSharResSelectTypeVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.SharResSelectTypeVisible);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00027563 File Offset: 0x00025763
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002756B File Offset: 0x0002576B
		public SharedResDataModelList ResDataCollection
		{
			get
			{
				return this.mResDataCollection;
			}
			set
			{
				this.mResDataCollection = value;
				base.OnPropertyChanged<SharedResDataModelList>(() => this.ResDataCollection);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000275A9 File Offset: 0x000257A9
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x000275B1 File Offset: 0x000257B1
		public bool SchoolRes
		{
			get
			{
				return this.mSchoolRes;
			}
			set
			{
				this.mSchoolRes = value;
				base.OnPropertyChanged<bool>(() => this.SchoolRes);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000275EF File Offset: 0x000257EF
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000275F7 File Offset: 0x000257F7
		public bool UserRes
		{
			get
			{
				return this.mUserRes;
			}
			set
			{
				this.mUserRes = value;
				base.OnPropertyChanged<bool>(() => this.UserRes);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00027635 File Offset: 0x00025835
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0002763D File Offset: 0x0002583D
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002767B File Offset: 0x0002587B
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00027683 File Offset: 0x00025883
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

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x000276C1 File Offset: 0x000258C1
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x000276C9 File Offset: 0x000258C9
		public string MessageType
		{
			get
			{
				return this.mMessageType;
			}
			set
			{
				this.mMessageType = value;
				base.OnPropertyChanged<string>(() => this.MessageType);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00027707 File Offset: 0x00025907
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0002770F File Offset: 0x0002590F
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0002774D File Offset: 0x0002594D
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00027755 File Offset: 0x00025955
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00027793 File Offset: 0x00025993
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0002779B File Offset: 0x0002599B
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000277D9 File Offset: 0x000259D9
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x000277E1 File Offset: 0x000259E1
		public bool ApplicationTypeSortAsc
		{
			get
			{
				return this.mApplicationTypeSortAsc;
			}
			set
			{
				this.mApplicationTypeSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.ApplicationTypeSortAsc);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0002781F File Offset: 0x00025A1F
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00027827 File Offset: 0x00025A27
		public bool ApplicationTypeSortDesc
		{
			get
			{
				return this.mApplicationTypeSortDesc;
			}
			set
			{
				this.mApplicationTypeSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.ApplicationTypeSortDesc);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00027865 File Offset: 0x00025A65
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0002786D File Offset: 0x00025A6D
		public bool FormatSortAsc
		{
			get
			{
				return this.mFormatSortAsc;
			}
			set
			{
				this.mFormatSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.FormatSortAsc);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000278AB File Offset: 0x00025AAB
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x000278B3 File Offset: 0x00025AB3
		public bool FormatSortDesc
		{
			get
			{
				return this.mFormatSortDesc;
			}
			set
			{
				this.mFormatSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.FormatSortDesc);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000278F1 File Offset: 0x00025AF1
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x000278F9 File Offset: 0x00025AF9
		public bool ScoreSortAsc
		{
			get
			{
				return this.mScoreSortAsc;
			}
			set
			{
				this.mScoreSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.ScoreSortAsc);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00027937 File Offset: 0x00025B37
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0002793F File Offset: 0x00025B3F
		public bool ScoreSortDesc
		{
			get
			{
				return this.mScoreSortDesc;
			}
			set
			{
				this.mScoreSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.ScoreSortDesc);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0002797D File Offset: 0x00025B7D
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00027985 File Offset: 0x00025B85
		public bool ResSizeSortAsc
		{
			get
			{
				return this.mResSizeSortAsc;
			}
			set
			{
				this.mResSizeSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.ResSizeSortAsc);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000279C3 File Offset: 0x00025BC3
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x000279CB File Offset: 0x00025BCB
		public bool ResSizeSortDesc
		{
			get
			{
				return this.mResSizeSortDesc;
			}
			set
			{
				this.mResSizeSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.ResSizeSortDesc);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00027A09 File Offset: 0x00025C09
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00027A11 File Offset: 0x00025C11
		public bool TimeSortAsc
		{
			get
			{
				return this.mTimeSortAsc;
			}
			set
			{
				this.mTimeSortAsc = value;
				base.OnPropertyChanged<bool>(() => this.TimeSortAsc);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00027A4F File Offset: 0x00025C4F
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00027A57 File Offset: 0x00025C57
		public bool TimeSortDesc
		{
			get
			{
				return this.mTimeSortDesc;
			}
			set
			{
				this.mTimeSortDesc = value;
				base.OnPropertyChanged<bool>(() => this.TimeSortDesc);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00027A95 File Offset: 0x00025C95
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00027A9D File Offset: 0x00025C9D
		public Visibility ColApplicationTypeVisible
		{
			get
			{
				return this.mColApplicationTypeVisible;
			}
			set
			{
				this.mColApplicationTypeVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ColApplicationTypeVisible);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00027ADB File Offset: 0x00025CDB
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00027AE3 File Offset: 0x00025CE3
		public Visibility ColFormatVisible
		{
			get
			{
				return this.mColFormatVisible;
			}
			set
			{
				this.mColFormatVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ColFormatVisible);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00027B21 File Offset: 0x00025D21
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00027B29 File Offset: 0x00025D29
		public Visibility ColAuthorVisible
		{
			get
			{
				return this.mColAuthorVisible;
			}
			set
			{
				this.mColAuthorVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ColAuthorVisible);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00027B67 File Offset: 0x00025D67
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00027B6F File Offset: 0x00025D6F
		public Visibility ColUnitVisible
		{
			get
			{
				return this.mColUnitVisible;
			}
			set
			{
				this.mColUnitVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ColUnitVisible);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00027BAD File Offset: 0x00025DAD
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00027BB5 File Offset: 0x00025DB5
		public Visibility ColScoreVisible
		{
			get
			{
				return this.mColScoreVisible;
			}
			set
			{
				this.mColScoreVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ColScoreVisible);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00027BF3 File Offset: 0x00025DF3
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x00027BFB File Offset: 0x00025DFB
		public string FlashResImage
		{
			get
			{
				return this.mFlashResImage;
			}
			set
			{
				this.mFlashResImage = value;
				base.OnPropertyChanged<string>(() => this.FlashResImage);
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027CE8 File Offset: 0x00025EE8
		public void InitData()
		{
			MainMenuEnums resourcesType = this.ResourcesType;
			if (resourcesType != MainMenuEnums.SharedResources)
			{
				if (resourcesType == MainMenuEnums.PepResources)
				{
					this.TitleText = "人教资源";
					this.SharResSelectTypeVisible = Visibility.Collapsed;
					this.CloudResCheck = true;
					this.ShareResCheck = false;
				}
			}
			else
			{
				this.TitleText = "共享资源";
				this.SharResSelectTypeVisible = Visibility.Visible;
				this.SchoolRes = true;
				this.UserRes = false;
				this.CloudResCheck = false;
				this.ShareResCheck = true;
			}
			this.SetResTypeState();
			this.KeywordSearchVisible = Visibility.Visible;
			this.KeywordSearchBackVisible = Visibility.Hidden;
			this.KeywordSearchHaveResultVisible = Visibility.Hidden;
			this.KeywordSearchNoResultVisible = Visibility.Hidden;
			this.FindKeywordCount = "0";
			this.Keyword = string.Empty;
			foreach (ZyTypeModel zyTypeModel in this.LstZynrlx)
			{
				zyTypeModel.IsChecked = (string.IsNullOrEmpty(zyTypeModel.Id) > false);
			}
			foreach (ZyTypeModel zyTypeModel2 in this.LstZygs)
			{
				zyTypeModel2.IsChecked = (string.IsNullOrEmpty(zyTypeModel2.Id) > false);
			}
			this.TitleSortAsc = false;
			this.TitleSortDesc = false;
			this.ApplicationTypeSortAsc = false;
			this.ApplicationTypeSortDesc = false;
			this.FormatSortAsc = false;
			this.FormatSortDesc = false;
			this.ResSizeSortAsc = false;
			this.ResSizeSortDesc = false;
			this.TimeSortAsc = false;
			this.TimeSortDesc = false;
			this.ScoreSortAsc = false;
			this.ScoreSortDesc = false;
			this.CurrentPage = 0;
			this.TotalPages = 0;
			this.ResDataCollection = new SharedResDataModelList();
			this.ShowNoDataMessage = false;
			this.MessageType = string.Empty;
			this.MessageContent = string.Empty;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00027EB8 File Offset: 0x000260B8
		private void SetAfterSearchData(bool IsHaveData)
		{
			if (!string.IsNullOrEmpty(this.Keyword))
			{
				this.TitleText = "检索结果";
				this.KeywordSearchVisible = Visibility.Hidden;
				this.KeywordSearchBackVisible = Visibility.Visible;
				if (IsHaveData)
				{
					this.KeywordSearchHaveResultVisible = Visibility.Visible;
					this.KeywordSearchNoResultVisible = Visibility.Hidden;
				}
				else
				{
					this.KeywordSearchHaveResultVisible = Visibility.Hidden;
					this.KeywordSearchNoResultVisible = Visibility.Visible;
				}
				this.SharResSelectTypeVisible = Visibility.Collapsed;
				return;
			}
			MainMenuEnums resourcesType = this.ResourcesType;
			if (resourcesType != MainMenuEnums.SharedResources)
			{
				if (resourcesType == MainMenuEnums.PepResources)
				{
					this.TitleText = "人教资源";
				}
			}
			else
			{
				this.TitleText = "共享资源";
			}
			this.KeywordSearchVisible = Visibility.Visible;
			this.KeywordSearchBackVisible = Visibility.Hidden;
			this.KeywordSearchNoResultVisible = Visibility.Hidden;
			this.KeywordSearchNoResultVisible = Visibility.Hidden;
			this.FindKeywordCount = "0";
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00027F68 File Offset: 0x00026168
		internal void SearchResListData()
		{
			SharedResourceViewModel.<SearchResListData>d__186 <SearchResListData>d__;
			<SearchResListData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<SearchResListData>d__.<>4__this = this;
			<SearchResListData>d__.<>1__state = -1;
			<SearchResListData>d__.<>t__builder.Start<SharedResourceViewModel.<SearchResListData>d__186>(ref <SearchResListData>d__);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00027FA0 File Offset: 0x000261A0
		private Task<SharedResListResultModel> GetSharedResDataListAsync()
		{
			SharedResourceViewModel.<GetSharedResDataListAsync>d__187 <GetSharedResDataListAsync>d__;
			<GetSharedResDataListAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<SharedResListResultModel>.Create();
			<GetSharedResDataListAsync>d__.<>4__this = this;
			<GetSharedResDataListAsync>d__.<>1__state = -1;
			<GetSharedResDataListAsync>d__.<>t__builder.Start<SharedResourceViewModel.<GetSharedResDataListAsync>d__187>(ref <GetSharedResDataListAsync>d__);
			return <GetSharedResDataListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00027FE4 File Offset: 0x000261E4
		private Dictionary<string, string> GetPostParameter(out string strParamter)
		{
			strParamter = string.Empty;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["userid"] = this.UserId;
			if (this.LstChapterId != null && this.LstChapterId.Count > 0)
			{
				dictionary["ori_tree_code"] = string.Join(",", this.LstChapterId);
			}
			else if (!string.IsNullOrEmpty(this.TextBookId))
			{
				dictionary["tbId"] = this.TextBookId;
			}
			if (!string.IsNullOrWhiteSpace(this.Keyword))
			{
				dictionary["title"] = this.Keyword;
			}
			if (this.CloudResCheck)
			{
				dictionary["sch_res_flag"] = "11";
			}
			else if (this.SchoolRes)
			{
				dictionary["sch_res_flag"] = "01";
			}
			else if (this.UserRes)
			{
				dictionary["sch_res_flag"] = "00";
			}
			IEnumerable<ZyTypeModel> enumerable = from a in this.LstZygs
			where a.IsChecked
			select a;
			string text;
			if (enumerable == null)
			{
				text = null;
			}
			else
			{
				ZyTypeModel zyTypeModel = enumerable.FirstOrDefault<ZyTypeModel>();
				text = ((zyTypeModel != null) ? zyTypeModel.Id : null);
			}
			string value = text;
			if (!string.IsNullOrEmpty(value))
			{
				dictionary["dzwjlx"] = value;
			}
			IEnumerable<ZyTypeModel> enumerable2 = from a in this.LstZynrlx
			where a.IsChecked
			select a;
			string text2;
			if (enumerable2 == null)
			{
				text2 = null;
			}
			else
			{
				ZyTypeModel zyTypeModel2 = enumerable2.FirstOrDefault<ZyTypeModel>();
				text2 = ((zyTypeModel2 != null) ? zyTypeModel2.Id : null);
			}
			string value2 = text2;
			if (!string.IsNullOrEmpty(value2))
			{
				dictionary["ex_zynrlx"] = value2;
			}
			if (this.TitleSortAsc)
			{
				dictionary["orderby"] = "title asc";
			}
			if (this.TitleSortDesc)
			{
				dictionary["orderby"] = "title desc";
			}
			if (this.ScoreSortAsc)
			{
				dictionary["orderby"] = "score asc";
			}
			if (this.ScoreSortDesc)
			{
				dictionary["orderby"] = "score desc";
			}
			if (this.TimeSortAsc)
			{
				dictionary["orderby"] = "s_modify_time asc";
			}
			if (this.TimeSortDesc)
			{
				dictionary["orderby"] = "s_modify_time desc";
			}
			if (this.ResSizeSortAsc)
			{
				dictionary["orderby"] = "file_size asc";
			}
			if (this.ResSizeSortDesc)
			{
				dictionary["orderby"] = "file_size desc";
			}
			if (this.ApplicationTypeSortAsc)
			{
				dictionary["orderby"] = "ex_zynrlx_name asc";
			}
			if (this.ApplicationTypeSortDesc)
			{
				dictionary["orderby"] = "ex_zynrlx_name desc";
			}
			if (this.FormatSortAsc)
			{
				dictionary["orderby"] = "dzwjlx_name asc";
			}
			if (this.FormatSortDesc)
			{
				dictionary["orderby"] = "dzwjlx_name desc";
			}
			dictionary["pagesize"] = 20.ToString();
			if (this.CurrentPage <= 0)
			{
				this.CurrentPage = 1;
			}
			dictionary["pageno"] = this.CurrentPage.ToString();
			strParamter = new JavaScriptSerializer().Serialize(dictionary);
			return dictionary;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000282E4 File Offset: 0x000264E4
		public void SetResTypeState()
		{
			if (this.CloudResCheck)
			{
				this.SharResSelectTypeVisible = Visibility.Collapsed;
				this.ColApplicationTypeVisible = Visibility.Visible;
				this.ColFormatVisible = Visibility.Visible;
				this.ColAuthorVisible = Visibility.Hidden;
				this.ColUnitVisible = Visibility.Hidden;
				this.ColScoreVisible = Visibility.Hidden;
				return;
			}
			if (this.ShareResCheck)
			{
				this.SharResSelectTypeVisible = Visibility.Visible;
				this.SchoolRes = true;
				this.UserRes = false;
				this.ColApplicationTypeVisible = Visibility.Hidden;
				this.ColFormatVisible = Visibility.Hidden;
				this.ColAuthorVisible = Visibility.Visible;
				this.ColUnitVisible = Visibility.Visible;
				this.ColScoreVisible = Visibility.Visible;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00028364 File Offset: 0x00026564
		public void NotifyDownloadCount(string type, string id, string userid, int num)
		{
			try
			{
				HttpHelper.HttpGet(ConfigHelper.WebServerUrl + string.Format("statistic/countDownload.json?type={0}&id={1}&num={2}&userid={3}", new object[]
				{
					type,
					id,
					num,
					userid
				}), null, true, "");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "调用平台下载次数接口失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x040003EF RID: 1007
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"ViewModel",
			"SharedResourceViewModel"
		});

		// Token: 0x040003F0 RID: 1008
		private string mTitleText = "资源中心";

		// Token: 0x040003F1 RID: 1009
		private Visibility mKeywordSearchVisible;

		// Token: 0x040003F2 RID: 1010
		private Visibility mKeywordSearchBackVisible = Visibility.Hidden;

		// Token: 0x040003F3 RID: 1011
		private Visibility mKeywordSearchHaveResultVisible = Visibility.Hidden;

		// Token: 0x040003F4 RID: 1012
		private Visibility mKeywordSearchNoResultVisible = Visibility.Hidden;

		// Token: 0x040003F5 RID: 1013
		private bool mCloudResCheck = true;

		// Token: 0x040003F6 RID: 1014
		private bool mShareResCheck;

		// Token: 0x040003F7 RID: 1015
		private Visibility mSharResSelectTypeVisible = Visibility.Collapsed;

		// Token: 0x040003F8 RID: 1016
		private string mFindKeywordCount = "0";

		// Token: 0x040003F9 RID: 1017
		private SharedResDataModelList mResDataCollection;

		// Token: 0x040003FA RID: 1018
		private bool mSchoolRes = true;

		// Token: 0x040003FB RID: 1019
		private bool mUserRes;

		// Token: 0x040003FC RID: 1020
		private string mKeyword = string.Empty;

		// Token: 0x040003FD RID: 1021
		private bool mTitleSortAsc;

		// Token: 0x040003FE RID: 1022
		private bool mTitleSortDesc;

		// Token: 0x040003FF RID: 1023
		private bool mApplicationTypeSortAsc;

		// Token: 0x04000400 RID: 1024
		private bool mApplicationTypeSortDesc;

		// Token: 0x04000401 RID: 1025
		private bool mFormatSortAsc;

		// Token: 0x04000402 RID: 1026
		private bool mFormatSortDesc;

		// Token: 0x04000403 RID: 1027
		private bool mScoreSortAsc;

		// Token: 0x04000404 RID: 1028
		private bool mScoreSortDesc;

		// Token: 0x04000405 RID: 1029
		private bool mResSizeSortAsc;

		// Token: 0x04000406 RID: 1030
		private bool mResSizeSortDesc;

		// Token: 0x04000407 RID: 1031
		private bool mTimeSortAsc;

		// Token: 0x04000408 RID: 1032
		private bool mTimeSortDesc;

		// Token: 0x04000409 RID: 1033
		private bool mShowNoDataMessage;

		// Token: 0x0400040A RID: 1034
		private string mMessageType = string.Empty;

		// Token: 0x0400040B RID: 1035
		private string mMessageContent = string.Empty;

		// Token: 0x0400040C RID: 1036
		private Visibility mColApplicationTypeVisible;

		// Token: 0x0400040D RID: 1037
		private Visibility mColFormatVisible;

		// Token: 0x0400040E RID: 1038
		private Visibility mColAuthorVisible = Visibility.Hidden;

		// Token: 0x0400040F RID: 1039
		private Visibility mColUnitVisible = Visibility.Hidden;

		// Token: 0x04000410 RID: 1040
		private Visibility mColScoreVisible = Visibility.Hidden;

		// Token: 0x04000411 RID: 1041
		private string mFlashResImage = string.Empty;

		// Token: 0x04000412 RID: 1042
		private const int PAGE_SIZE_LIST = 20;

		// Token: 0x02000147 RID: 327
		// (Invoke) Token: 0x06000B8D RID: 2957
		public delegate bool SetPageCnt(int totalpage, int curpage);
	}
}
