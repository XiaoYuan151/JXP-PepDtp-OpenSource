using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gma.System.MouseKeyHook;
using JXP.Controls;
using JXP.Controls.CustomControl;
using JXP.Controls.UserControls;
using JXP.Data;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.DataStatistics;
using JXP.PepDtp.Paramter;
using pep.Nobook.Views.Standard;
using pep.sdk.reader.View;
using pep.sdk.reader.View.UserControls;
using pep.sdk.utility.Common;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000044 RID: 68
	public partial class ResManageControl : UserControl, INotifyPropertyChanged, IStyleConnector
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00015DC3 File Offset: 0x00013FC3
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00015DCB File Offset: 0x00013FCB
		public bool IsOnline { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00015DD4 File Offset: 0x00013FD4
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00015DDC File Offset: 0x00013FDC
		public bool MyResChecked
		{
			get
			{
				return this.mMyResChecked;
			}
			set
			{
				this.mMyResChecked = value;
				this.OnPropertyRaised("MyResChecked");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00015DF0 File Offset: 0x00013FF0
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public bool SyncResChecked
		{
			get
			{
				return this.mSyncResChecked;
			}
			set
			{
				this.mSyncResChecked = value;
				this.OnPropertyRaised("SyncResChecked");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00015E0C File Offset: 0x0001400C
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00015E14 File Offset: 0x00014014
		public bool MyCollectChecked
		{
			get
			{
				return this.mMyCollectChecked;
			}
			set
			{
				this.mMyCollectChecked = value;
				this.OnPropertyRaised("MyCollectChecked");
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060003D2 RID: 978 RVA: 0x00015E28 File Offset: 0x00014028
		// (remove) Token: 0x060003D3 RID: 979 RVA: 0x00015E60 File Offset: 0x00014060
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003D4 RID: 980 RVA: 0x00015E95 File Offset: 0x00014095
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00015EAE File Offset: 0x000140AE
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00015EC0 File Offset: 0x000140C0
		public ObservableCollection<MetaModel> MyResLYLst
		{
			get
			{
				return (ObservableCollection<MetaModel>)base.GetValue(ResManageControl.MyResLYLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.MyResLYLstProperty, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00015ECE File Offset: 0x000140CE
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00015EE0 File Offset: 0x000140E0
		public MetaList MyResZyFormatLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.MyResZyFormatLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.MyResZyFormatLstProperty, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00015EEE File Offset: 0x000140EE
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00015F00 File Offset: 0x00014100
		public MetaList MyResZyLbLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.MyResZyLbLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.MyResZyLbLstProperty, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00015F0E File Offset: 0x0001410E
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00015F20 File Offset: 0x00014120
		public MetaList SyncResZyFormatLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.SyncResZyFormatLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncResZyFormatLstProperty, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00015F2E File Offset: 0x0001412E
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00015F40 File Offset: 0x00014140
		public MetaList SyncResZyLbLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.SyncResZyLbLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncResZyLbLstProperty, value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00015F4E File Offset: 0x0001414E
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00015F60 File Offset: 0x00014160
		public MetaModel SyncResSelectSort
		{
			get
			{
				return (MetaModel)base.GetValue(ResManageControl.SyncResSelectSortProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncResSelectSortProperty, value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00015F6E File Offset: 0x0001416E
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00015F80 File Offset: 0x00014180
		public MetaList SyncResSortLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.SyncResSortLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncResSortLstProperty, value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00015F8E File Offset: 0x0001418E
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00015FA0 File Offset: 0x000141A0
		public ObservableCollection<MetaModel> SyncLYLst
		{
			get
			{
				return (ObservableCollection<MetaModel>)base.GetValue(ResManageControl.SyncLYLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncLYLstProperty, value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00015FAE File Offset: 0x000141AE
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00015FC0 File Offset: 0x000141C0
		public MetaModel SyncResSelectZyLy
		{
			get
			{
				return (MetaModel)base.GetValue(ResManageControl.SyncResSelectZyLyProperty);
			}
			set
			{
				base.SetValue(ResManageControl.SyncResSelectZyLyProperty, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00015FCE File Offset: 0x000141CE
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00015FE0 File Offset: 0x000141E0
		public MetaList MyCollectFormatLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.MyCollectFormatLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.MyCollectFormatLstProperty, value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00015FEE File Offset: 0x000141EE
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00016000 File Offset: 0x00014200
		public MetaList MyCollectLbLst
		{
			get
			{
				return (MetaList)base.GetValue(ResManageControl.MyCollectLbLstProperty);
			}
			set
			{
				base.SetValue(ResManageControl.MyCollectLbLstProperty, value);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001600E File Offset: 0x0001420E
		public ResManageControl()
		{
			this.InitializeComponent();
			base.Loaded += this.ResManageControl_Loaded;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001604C File Offset: 0x0001424C
		private void ResManageControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitZyFormat();
			this.IntiZyLb();
			this.InitLy();
			this.InitSort();
			this.RemoveComboxEvent();
			this.ucMyResKeyword.btnSearch.Click -= this.MyResBtnSearch_Click;
			this.ucMyResKeyword.btnSearch.Click += this.MyResBtnSearch_Click;
			this.ucMyResKeyword.txtKeyWord.PreviewKeyDown -= this.MyResTxtKeyWord_PreviewKeyDown;
			this.ucMyResKeyword.txtKeyWord.PreviewKeyDown += this.MyResTxtKeyWord_PreviewKeyDown;
			this.ucMyResChapter.ChanpterSelectChangedCallBack = new ChanpterSelectChanged(this.MyResChapter_SelectionChanged);
			this.ucMyRes.GetResultCountCallBack = new GetResultCount(this.MyResSetSearchResult);
			this.ucSyncResKeyword.btnSearch.Click -= this.BtnSyncSearch_Click;
			this.ucSyncResKeyword.btnSearch.Click += this.BtnSyncSearch_Click;
			this.ucSyncResKeyword.txtKeyWord.PreviewKeyDown -= this.TxtKeyWordSync_PreviewKeyDown;
			this.ucSyncResKeyword.txtKeyWord.PreviewKeyDown += this.TxtKeyWordSync_PreviewKeyDown;
			this.ucSyncResChapter.ChanpterSelectChangedCallBack = new ChanpterSelectChanged(this.SyncResChapter_SelectionChanged);
			this.ucSyncRes.GetResultCountCallBack = new GetResultCount(this.SyncResSetSearchResult);
			this.ucMyCollectKeyword.btnSearch.Click -= this.MyCollectBtnSearch_Click;
			this.ucMyCollectKeyword.btnSearch.Click += this.MyCollectBtnSearch_Click;
			this.ucMyCollectKeyword.txtKeyWord.PreviewKeyDown -= this.MyCollectKeyWord_PreviewKeyDown;
			this.ucMyCollectKeyword.txtKeyWord.PreviewKeyDown += this.MyCollectKeyWord_PreviewKeyDown;
			this.ucMyCollectChapter.ChanpterSelectChangedCallBack = new ChanpterSelectChanged(this.MyCollectChapter_SelectionChanged);
			this.ucMyCollectRes.GetResultCountCallBack = new GetResultCount(this.MyCollectSetSearchResult);
			this.infoPop.MouseLeftButtonDown -= this.InfoPop_MouseLeftButtonDown;
			this.infoPop.MouseLeftButtonDown += this.InfoPop_MouseLeftButtonDown;
			this.AddComboxEvent();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00016285 File Offset: 0x00014485
		private void RemoveComboxEvent()
		{
			this.syncResComboxLY.SelectionChanged -= this.SyncResCombox_SelectionChanged;
			this.syncResComboxSort.SelectionChanged -= this.SyncResComboxSort_SelectionChanged;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000162B5 File Offset: 0x000144B5
		private void AddComboxEvent()
		{
			this.syncResComboxLY.SelectionChanged += this.SyncResCombox_SelectionChanged;
			this.syncResComboxSort.SelectionChanged += this.SyncResComboxSort_SelectionChanged;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000162E8 File Offset: 0x000144E8
		private void InitZyFormat()
		{
			this.MyResZyFormatLst = CommonHandle.GetConstData(2014, "全部");
			MetaModel metaModel = this.MyResZyFormatLst.FirstOrDefault<MetaModel>();
			if (metaModel != null)
			{
				metaModel.Selected = true;
			}
			this.SyncResZyFormatLst = CommonHandle.GetConstData(2014, "全部");
			MetaModel metaModel2 = this.SyncResZyFormatLst.FirstOrDefault<MetaModel>();
			if (metaModel2 != null)
			{
				metaModel2.Selected = true;
			}
			this.MyCollectFormatLst = CommonHandle.GetConstData(2014, "全部");
			MetaModel metaModel3 = this.MyCollectFormatLst.FirstOrDefault<MetaModel>();
			if (metaModel3 != null)
			{
				metaModel3.Selected = true;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016378 File Offset: 0x00014578
		private void IntiZyLb()
		{
			this.MyResZyLbLst = CommonHandle.GetConstData(2020, "全部");
			MetaList myResZyLbLst = this.MyResZyLbLst;
			MetaModel metaModel = (myResZyLbLst != null) ? myResZyLbLst.FirstOrDefault<MetaModel>() : null;
			if (metaModel != null)
			{
				metaModel.Selected = true;
			}
			this.SyncResZyLbLst = CommonHandle.GetConstData(2020, "全部");
			MetaList syncResZyLbLst = this.SyncResZyLbLst;
			MetaModel metaModel2 = (syncResZyLbLst != null) ? syncResZyLbLst.FirstOrDefault<MetaModel>() : null;
			if (metaModel2 != null)
			{
				metaModel2.Selected = true;
			}
			this.MyCollectLbLst = CommonHandle.GetConstData(2020, "全部");
			MetaList myCollectLbLst = this.MyCollectLbLst;
			MetaModel metaModel3 = (myCollectLbLst != null) ? myCollectLbLst.FirstOrDefault<MetaModel>() : null;
			if (metaModel3 != null)
			{
				metaModel3.Selected = true;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001641C File Offset: 0x0001461C
		private void InitLy()
		{
			this.MyResLYLst = new ObservableCollection<MetaModel>();
			this.MyResLYLst.Add(new MetaModel
			{
				Id = "",
				Value = "全部",
				Selected = true
			});
			this.MyResLYLst.Add(new MetaModel
			{
				Id = "",
				Value = "我的上传",
				Selected = false
			});
			this.MyResLYLst.Add(new MetaModel
			{
				Id = "",
				Value = "我的收藏",
				Selected = false
			});
			this.SyncLYLst = new ObservableCollection<MetaModel>();
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "10",
				Value = "全部",
				Selected = true
			});
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "12",
				Value = "教材资源"
			});
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "11",
				Value = "配套资源"
			});
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "01",
				Value = "本校共享"
			});
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "00",
				Value = "群组共享"
			});
			this.SyncLYLst.Add(new MetaModel
			{
				Id = "06",
				Value = "平台共享 "
			});
			this.SyncResSelectZyLy = this.SyncLYLst.FirstOrDefault<MetaModel>();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000165C4 File Offset: 0x000147C4
		private void InitSort()
		{
			this.SyncResSortLst = new MetaList();
			this.SyncResSortLst.Add(new MetaModel
			{
				Id = "",
				Value = "默认排序"
			});
			this.SyncResSortLst.Add(new MetaModel
			{
				Id = "total,DESC",
				Value = "使用数从大到小"
			});
			this.SyncResSelectSort = this.SyncResSortLst.FirstOrDefault<MetaModel>();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016639 File Offset: 0x00014839
		private void MyResRbtn_Click(object sender, RoutedEventArgs e)
		{
			this.SearchMyRes();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00016639 File Offset: 0x00014839
		private void MyResCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.SearchMyRes();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016639 File Offset: 0x00014839
		private void MyResFormatRadioButton_Click(object sender, RoutedEventArgs e)
		{
			this.SearchMyRes();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016641 File Offset: 0x00014841
		private void MyResTxtKeyWord_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.MyResBtnSearch_Click(null, null);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016639 File Offset: 0x00014839
		private void MyResBtnSearch_Click(object sender, RoutedEventArgs e)
		{
			this.SearchMyRes();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00016639 File Offset: 0x00014839
		private void MyResChapter_SelectionChanged(string bookId, List<string> chaptcerId, string bookName, string chapterName, ObservableCollection<TextBookIdNameModel> lstBook)
		{
			this.SearchMyRes();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016654 File Offset: 0x00014854
		private void SearchMyRes()
		{
			try
			{
				UserChapterInfoModel userChapterInfoModel = new UserChapterInfoModel();
				userChapterInfoModel.BookId = this.ucMyResChapter.BookId;
				userChapterInfoModel.BookName = this.ucMyResChapter.BookName;
				List<string> chapterIds = this.ucMyResChapter.ChapterIds;
				userChapterInfoModel.ChapterId = ((chapterIds != null && chapterIds.Count > 0) ? string.Join(",", this.ucMyResChapter.ChapterIds) : string.Empty);
				userChapterInfoModel.ChapterName = this.ucMyResChapter.ChapterName;
				userChapterInfoModel.TypeFlg = 1001.ToString();
				UserChapterInfoModel chapterInfo = userChapterInfoModel;
				DtpUserBookChapter.Instance.SaveChapterInfo(chapterInfo);
				MetaModel metaModel = (from a in this.MyResZyFormatLst
				where a.Selected
				select a).FirstOrDefault<MetaModel>();
				string zyFormate = (metaModel != null) ? metaModel.Id : null;
				MetaModel metaModel2 = (from a in this.MyResZyLbLst
				where a.Selected
				select a).First<MetaModel>();
				string zyLb = (metaModel2 != null) ? metaModel2.Id : null;
				this.ucMyRes.SearchDataAsync(zyFormate, zyLb, this.ucMyResKeyword.txtKeyWord.Text, this.ucMyResChapter.BookId, this.ucMyResChapter.ChapterIds);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("我的资源检索失败：{0}。", arg));
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000167DC File Offset: 0x000149DC
		private void btnCreateRes_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CreateNewResParamter createNewResParamter = new CreateNewResParamter();
				if (!string.IsNullOrEmpty(this.ucMyResChapter.BookId))
				{
					createNewResParamter = new CreateNewResParamter();
					createNewResParamter.BookID = this.ucMyResChapter.BookId;
					createNewResParamter.BookName = this.ucMyResChapter.BookName;
					if (this.ucMyResChapter.ChapterIds != null && this.ucMyResChapter.ChapterIds.Count > 0)
					{
						createNewResParamter.ChapterID = this.ucMyResChapter.ChapterIds[0];
						createNewResParamter.ChapterName = this.ucMyResChapter.ChapterName;
					}
				}
				CreateNewResWindow createNewResWindow = new CreateNewResWindow(createNewResParamter);
				createNewResWindow.CreatedUserResourceEvent += this.CreatNewRes_CreatedUserResourceEvent;
				createNewResWindow.UploadDataCallback = new Action<string, string>(StatisticsHelper.Instance.UploadCreateResData);
				createNewResWindow.SetBookRefershCallback = new Action(this.InitData);
				createNewResWindow.Owner = Window.GetWindow(this);
				createNewResWindow.CreateGroupDataCallBack = this.CreateGroupDataCallBack;
				createNewResWindow.ShowDialog();
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("我的上传打开失败：{0}。", arg));
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000168FC File Offset: 0x00014AFC
		private void CreatNewRes_CreatedUserResourceEvent(UserResourceModel newUserResMdl)
		{
			try
			{
				MetaModel metaModel = (from a in this.MyResZyFormatLst
				where a.Selected
				select a).FirstOrDefault<MetaModel>();
				string zyFormate = (metaModel != null) ? metaModel.Id : null;
				MetaModel metaModel2 = (from a in this.MyResZyLbLst
				where a.Selected
				select a).First<MetaModel>();
				string zyLb = (metaModel2 != null) ? metaModel2.Id : null;
				this.ucMyRes.SearchDataAsync(zyFormate, zyLb, this.ucMyResKeyword.txtKeyWord.Text, this.ucMyResChapter.BookId, this.ucMyResChapter.ChapterIds);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "新建资源完成回调重新检索我的资源数据出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000169EC File Offset: 0x00014BEC
		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ucMyResKeyword.txtKeyWord.Text = string.Empty;
				this.SearchMyRes();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "新关键字检索返回重新检索我的资源数据出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016A4C File Offset: 0x00014C4C
		private void MyResSetSearchResult(int nCount)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (!string.IsNullOrEmpty(this.ucMyResKeyword.txtKeyWord.Text))
				{
					this.lblResult.Text = string.Format("输入的关键字\"{0}\",共检索到{1}条资源", this.ucMyResKeyword.txtKeyWord.Text, nCount);
					this.gridKeywordResult.Visibility = Visibility.Visible;
					return;
				}
				this.gridKeywordResult.Visibility = Visibility.Collapsed;
			}), new object[0]);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016A8B File Offset: 0x00014C8B
		private void SyncResComboxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.SyncResSearch();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016A8B File Offset: 0x00014C8B
		private void SyncResCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.SyncResSearch();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016A8B File Offset: 0x00014C8B
		private void SyncResRBtn_Click(object sender, RoutedEventArgs e)
		{
			this.SyncResSearch();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00016A8B File Offset: 0x00014C8B
		private void BtnSyncSearch_Click(object sender, RoutedEventArgs e)
		{
			this.SyncResSearch();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00016A93 File Offset: 0x00014C93
		private void TxtKeyWordSync_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.BtnSyncSearch_Click(null, null);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00016A8B File Offset: 0x00014C8B
		private void SyncResChapter_SelectionChanged(string bookId, List<string> chaptcerId, string bookName, string chapterName, ObservableCollection<TextBookIdNameModel> lstBook)
		{
			this.SyncResSearch();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00016AA8 File Offset: 0x00014CA8
		private void SyncResSearch()
		{
			try
			{
				UserChapterInfoModel userChapterInfoModel = new UserChapterInfoModel();
				userChapterInfoModel.BookId = this.ucSyncResChapter.BookId;
				userChapterInfoModel.BookName = this.ucSyncResChapter.BookName;
				List<string> chapterIds = this.ucSyncResChapter.ChapterIds;
				userChapterInfoModel.ChapterId = ((chapterIds != null && chapterIds.Count > 0) ? string.Join(",", this.ucSyncResChapter.ChapterIds) : string.Empty);
				userChapterInfoModel.ChapterName = this.ucSyncResChapter.ChapterName;
				userChapterInfoModel.TypeFlg = 1014.ToString();
				UserChapterInfoModel chapterInfo = userChapterInfoModel;
				DtpUserBookChapter.Instance.SaveChapterInfo(chapterInfo);
				IEnumerable<MetaModel> enumerable = from a in this.SyncResZyFormatLst
				where a.Selected
				select a;
				string text;
				if (enumerable == null)
				{
					text = null;
				}
				else
				{
					MetaModel metaModel = enumerable.FirstOrDefault<MetaModel>();
					text = ((metaModel != null) ? metaModel.Id : null);
				}
				string text2 = text;
				IEnumerable<MetaModel> enumerable2 = from a in this.SyncResZyLbLst
				where a.Selected
				select a;
				string text3;
				if (enumerable2 == null)
				{
					text3 = null;
				}
				else
				{
					MetaModel metaModel2 = enumerable2.FirstOrDefault<MetaModel>();
					text3 = ((metaModel2 != null) ? metaModel2.Id : null);
				}
				string text4 = text3;
				SchoolResControl schoolResControl = this.ucSyncRes;
				string zyFormate = text2;
				string zyLb = text4;
				string text5 = this.ucSyncResKeyword.txtKeyWord.Text;
				string bookId = this.ucSyncResChapter.BookId;
				List<string> chapterIds2 = this.ucSyncResChapter.ChapterIds;
				string groupId = "";
				MetaModel syncResSelectZyLy = this.SyncResSelectZyLy;
				string resLy = (syncResSelectZyLy != null) ? syncResSelectZyLy.Id : null;
				MetaModel syncResSelectSort = this.SyncResSelectSort;
				schoolResControl.SearchDataAsync(zyFormate, zyLb, text5, bookId, chapterIds2, groupId, resLy, (syncResSelectSort != null) ? syncResSelectSort.Id : null);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "同步资源检索出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00016C70 File Offset: 0x00014E70
		private void btnSyncBack_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ucSyncResKeyword.txtKeyWord.Text = string.Empty;
				this.SyncResSearch();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关键字检索返回重新检索同步资源数据出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00016CD0 File Offset: 0x00014ED0
		private void SyncResSetSearchResult(int nCount)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (!string.IsNullOrEmpty(this.ucSyncResKeyword.txtKeyWord.Text))
				{
					this.lblSyncResult.Text = string.Format("输入的关键字\"{0}\",共检索到{1}条资源", this.ucSyncResKeyword.txtKeyWord.Text, nCount);
					this.gridSyncKeywordResult.Visibility = Visibility.Visible;
					return;
				}
				this.gridSyncKeywordResult.Visibility = Visibility.Collapsed;
			}), new object[0]);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00016D10 File Offset: 0x00014F10
		public void InitData()
		{
			if (!this.MyResChecked && !this.SyncResChecked && !this.MyCollectChecked)
			{
				this.SyncResChecked = true;
			}
			ObservableCollection<TextBookIdNameModel> bookList = DtpUserBookChapter.Instance.GetBookList();
			if (this.MyResChecked)
			{
				this.infoPop.PlacementTarget = this.ucMyResChapter.btnSetting;
				this.OpenInfo();
				this.ucMyRes.ResList.Clear();
				this.ucMyResChapter.SetBookList(bookList);
				UserChapterInfoModel userChapterInfoModel = null;
				if (string.IsNullOrEmpty(this.ucMyResChapter.BookId))
				{
					userChapterInfoModel = DtpUserBookChapter.Instance.GetUserChapter(UserSelectChapterType.MyRes);
				}
				if (userChapterInfoModel != null)
				{
					string bookId = userChapterInfoModel.BookId;
					if (bookId != null && bookId.Length >= 13)
					{
						List<string> list = new List<string>();
						if (!string.IsNullOrEmpty(userChapterInfoModel.ChapterId))
						{
							string[] collection = userChapterInfoModel.ChapterId.Split(new char[]
							{
								','
							}, StringSplitOptions.RemoveEmptyEntries);
							list.AddRange(collection);
						}
						string chapterId = (list.Count > 0) ? list[0] : string.Empty;
						this.ucMyResChapter.SetBookChapterInfo(userChapterInfoModel.BookId, userChapterInfoModel.BookName, userChapterInfoModel.BookId.Substring(1, 1), userChapterInfoModel.BookId.Substring(2, 2), chapterId);
					}
				}
				this.SearchMyRes();
				return;
			}
			if (this.SyncResChecked)
			{
				this.infoPop.PlacementTarget = this.ucSyncResChapter.btnSetting;
				this.OpenInfo();
				this.ucSyncResChapter.SetBookList(bookList);
				UserChapterInfoModel userChapterInfoModel2 = null;
				if (string.IsNullOrEmpty(this.ucSyncResChapter.BookId))
				{
					userChapterInfoModel2 = DtpUserBookChapter.Instance.GetUserChapter(UserSelectChapterType.SyncRes);
				}
				if (userChapterInfoModel2 != null && userChapterInfoModel2.BookId.Length >= 13)
				{
					List<string> list2 = new List<string>();
					if (!string.IsNullOrEmpty(userChapterInfoModel2.ChapterId))
					{
						string[] collection2 = userChapterInfoModel2.ChapterId.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						list2.AddRange(collection2);
					}
					string chapterId2 = (list2.Count > 0) ? list2[0] : string.Empty;
					this.ucSyncResChapter.SetBookChapterInfo(userChapterInfoModel2.BookId, userChapterInfoModel2.BookName, userChapterInfoModel2.BookId.Substring(1, 1), userChapterInfoModel2.BookId.Substring(2, 2), chapterId2);
				}
				this.SyncResSearch();
				return;
			}
			if (this.MyCollectChecked)
			{
				this.infoPop.PlacementTarget = this.ucMyCollectChapter.btnSetting;
				this.OpenInfo();
				this.ucMyCollectChapter.SetBookList(bookList);
				UserChapterInfoModel userChapterInfoModel3 = null;
				if (string.IsNullOrEmpty(this.ucMyCollectChapter.BookId))
				{
					userChapterInfoModel3 = DtpUserBookChapter.Instance.GetUserChapter(UserSelectChapterType.MyCollect);
				}
				if (userChapterInfoModel3 != null && userChapterInfoModel3.BookId.Length >= 13)
				{
					List<string> list3 = new List<string>();
					if (!string.IsNullOrEmpty(userChapterInfoModel3.ChapterId))
					{
						string[] collection3 = userChapterInfoModel3.ChapterId.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						list3.AddRange(collection3);
					}
					string chapterId3 = (list3.Count > 0) ? list3[0] : string.Empty;
					this.ucMyCollectChapter.SetBookChapterInfo(userChapterInfoModel3.BookId, userChapterInfoModel3.BookName, userChapterInfoModel3.BookId.Substring(1, 1), userChapterInfoModel3.BookId.Substring(2, 2), chapterId3);
				}
				this.MyCollectSearch();
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001705B File Offset: 0x0001525B
		public void ClearData()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.RemoveComboxEvent();
				this.MyResChecked = false;
				this.ucMyResKeyword.txtKeyWord.Text = string.Empty;
				this.gridKeywordResult.Visibility = Visibility.Collapsed;
				this.ucMyResChapter.ClearData();
				this.ucMyRes.ClearData();
				MetaModel metaModel = this.MyResZyFormatLst.FirstOrDefault<MetaModel>();
				if (metaModel != null)
				{
					metaModel.Selected = true;
				}
				MetaModel metaModel2 = this.MyResZyLbLst.FirstOrDefault<MetaModel>();
				if (metaModel2 != null)
				{
					metaModel2.Selected = true;
				}
				this.SyncResChecked = true;
				this.ucSyncResKeyword.txtKeyWord.Text = string.Empty;
				this.gridSyncKeywordResult.Visibility = Visibility.Collapsed;
				this.ucSyncResChapter.ClearData();
				this.ucSyncRes.ClearData();
				MetaList syncResZyFormatLst = this.SyncResZyFormatLst;
				MetaModel metaModel3 = (syncResZyFormatLst != null) ? syncResZyFormatLst.FirstOrDefault<MetaModel>() : null;
				if (metaModel3 != null)
				{
					metaModel3.Selected = true;
				}
				MetaModel metaModel4 = this.SyncResZyLbLst.FirstOrDefault<MetaModel>();
				if (metaModel4 != null)
				{
					metaModel4.Selected = true;
				}
				this.SyncResSelectZyLy = this.SyncLYLst.FirstOrDefault<MetaModel>();
				this.SyncResSelectSort = this.SyncResSortLst.FirstOrDefault<MetaModel>();
				this.MyCollectChecked = false;
				this.ucMyCollectKeyword.txtKeyWord.Text = string.Empty;
				this.gridMyCollectKeywordResult.Visibility = Visibility.Collapsed;
				this.ucMyCollectChapter.ClearData();
				this.ucMyCollectRes.ClearData();
				MetaList myCollectFormatLst = this.MyCollectFormatLst;
				MetaModel metaModel5 = (myCollectFormatLst != null) ? myCollectFormatLst.FirstOrDefault<MetaModel>() : null;
				if (metaModel5 != null)
				{
					metaModel5.Selected = true;
				}
				MetaModel metaModel6 = this.MyCollectLbLst.FirstOrDefault<MetaModel>();
				if (metaModel6 != null)
				{
					metaModel6.Selected = true;
				}
				this.AddComboxEvent();
			}), new object[0]);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001707C File Offset: 0x0001527C
		private void radio_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.InitData();
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("资源检索数据失败：{0}。", arg));
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000170BC File Offset: 0x000152BC
		private void InfoPop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.CloseInfoPop();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000170C4 File Offset: 0x000152C4
		private void CloseInfoPop()
		{
			try
			{
				this.mPopInfoDA.InsertPopInfo(new PopInfoModel
				{
					TypeFlg = "1008",
					UserId = CommonParamter.Instance.LoginUserId,
					Comment = "设置常用教材提示"
				});
				if (this.infoPop.IsOpen)
				{
					this.infoPop.IsOpen = false;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00017148 File Offset: 0x00015348
		private void OpenInfo()
		{
			try
			{
				if (this.mPopInfoDA.GetPopInfo("1008", CommonParamter.Instance.LoginUserId) == null)
				{
					this.infoPop.IsOpen = true;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("打开提示pop失败：[{0}]。", arg));
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000171AC File Offset: 0x000153AC
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			try
			{
				base.OnPropertyChanged(e);
				if (e.Property == UIElement.VisibilityProperty && (Visibility)e.NewValue != Visibility.Visible && this.infoPop.IsOpen)
				{
					this.infoPop.IsOpen = false;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00017218 File Offset: 0x00015418
		private void MyCollectFormat_Click(object sender, RoutedEventArgs e)
		{
			this.MyCollectSearch();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00017220 File Offset: 0x00015420
		private void btnMyCollectBack_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ucMyCollectKeyword.txtKeyWord.Text = string.Empty;
				this.MyCollectSearch();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关键字检索返回重新检索我的收藏数据出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00017218 File Offset: 0x00015418
		private void MyCollectComboxLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.MyCollectSearch();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00017218 File Offset: 0x00015418
		private void MyCollectBtnSearch_Click(object sender, RoutedEventArgs e)
		{
			this.MyCollectSearch();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00017280 File Offset: 0x00015480
		private void MyCollectKeyWord_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.MyCollectBtnSearch_Click(null, null);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00017218 File Offset: 0x00015418
		private void MyCollectChapter_SelectionChanged(string bookId, List<string> chaptcerId, string bookName, string chapterName, ObservableCollection<TextBookIdNameModel> lstBook)
		{
			this.MyCollectSearch();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00017294 File Offset: 0x00015494
		private void MyCollectSearch()
		{
			try
			{
				UserChapterInfoModel userChapterInfoModel = new UserChapterInfoModel();
				userChapterInfoModel.BookId = this.ucMyCollectChapter.BookId;
				userChapterInfoModel.BookName = this.ucMyCollectChapter.BookName;
				List<string> chapterIds = this.ucMyCollectChapter.ChapterIds;
				userChapterInfoModel.ChapterId = ((chapterIds != null && chapterIds.Count > 0) ? string.Join(",", this.ucMyCollectChapter.ChapterIds) : string.Empty);
				userChapterInfoModel.ChapterName = this.ucMyCollectChapter.ChapterName;
				userChapterInfoModel.TypeFlg = 1016.ToString();
				UserChapterInfoModel chapterInfo = userChapterInfoModel;
				DtpUserBookChapter.Instance.SaveChapterInfo(chapterInfo);
				IEnumerable<MetaModel> enumerable = from a in this.MyCollectFormatLst
				where a.Selected
				select a;
				string text;
				if (enumerable == null)
				{
					text = null;
				}
				else
				{
					MetaModel metaModel = enumerable.FirstOrDefault<MetaModel>();
					text = ((metaModel != null) ? metaModel.Id : null);
				}
				string zyFormate = text;
				IEnumerable<MetaModel> enumerable2 = from a in this.MyCollectLbLst
				where a.Selected
				select a;
				string text2;
				if (enumerable2 == null)
				{
					text2 = null;
				}
				else
				{
					MetaModel metaModel2 = enumerable2.FirstOrDefault<MetaModel>();
					text2 = ((metaModel2 != null) ? metaModel2.Id : null);
				}
				string zyLb = text2;
				this.ucMyCollectRes.SearchDataAsync(zyFormate, zyLb, this.ucMyCollectKeyword.txtKeyWord.Text, this.ucMyCollectChapter.BookId, this.ucMyCollectChapter.ChapterIds, "", "13", "");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "我的收藏检索出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00017444 File Offset: 0x00015644
		private void MyCollectSetSearchResult(int nCount)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (!string.IsNullOrEmpty(this.ucMyCollectKeyword.txtKeyWord.Text))
				{
					this.myCollectLblResult.Text = string.Format("输入的关键字\"{0}\",共检索到{1}条资源", this.ucMyCollectKeyword.txtKeyWord.Text, nCount);
					this.gridMyCollectKeywordResult.Visibility = Visibility.Visible;
					return;
				}
				this.gridMyCollectKeywordResult.Visibility = Visibility.Collapsed;
			}), new object[0]);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00017484 File Offset: 0x00015684
		private void btnCourseWare_Click(object sender, RoutedEventArgs e)
		{
			IEnumerable<PrepareLessonWindow> enumerable = Application.Current.Windows.OfType<PrepareLessonWindow>();
			PrepareLessonWindow prepareLessonWindow = (enumerable != null) ? enumerable.FirstOrDefault<PrepareLessonWindow>() : null;
			if (prepareLessonWindow != null)
			{
				prepareLessonWindow.WindowState = WindowState.Maximized;
				prepareLessonWindow.Show();
				return;
			}
			new PrepareLessonWindow
			{
				Owner = Window.GetWindow(this)
			}.Show();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000177F4 File Offset: 0x000159F4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId <= 18)
			{
				if (connectionId == 7)
				{
					((RadioButton)target).Click += this.SyncResRBtn_Click;
					return;
				}
				if (connectionId == 8)
				{
					((RadioButton)target).Click += this.SyncResRBtn_Click;
					return;
				}
				if (connectionId != 18)
				{
					return;
				}
				((RadioButton)target).Click += this.MyResRbtn_Click;
				return;
			}
			else
			{
				if (connectionId == 19)
				{
					((RadioButton)target).Click += this.MyResFormatRadioButton_Click;
					return;
				}
				if (connectionId == 28)
				{
					((RadioButton)target).Click += this.MyCollectFormat_Click;
					return;
				}
				if (connectionId != 29)
				{
					return;
				}
				((RadioButton)target).Click += this.MyCollectFormat_Click;
				return;
			}
		}

		// Token: 0x040001F3 RID: 499
		private UserChapterInfoDataAccess mUserChapterInfoDA = new UserChapterInfoDataAccess();

		// Token: 0x040001F4 RID: 500
		private IKeyboardMouseEvents m_Events;

		// Token: 0x040001F5 RID: 501
		private PopInfoDataAccess mPopInfoDA = new PopInfoDataAccess();

		// Token: 0x040001F7 RID: 503
		public CreateGroupData CreateGroupDataCallBack;

		// Token: 0x040001F8 RID: 504
		private bool mMyResChecked;

		// Token: 0x040001F9 RID: 505
		private bool mSyncResChecked = true;

		// Token: 0x040001FA RID: 506
		private bool mMyCollectChecked;

		// Token: 0x040001FC RID: 508
		public static readonly DependencyProperty MyResLYLstProperty = DependencyProperty.Register("MyResLYLst", typeof(ObservableCollection<MetaModel>), typeof(ResManageControl), new PropertyMetadata(new ObservableCollection<MetaModel>()));

		// Token: 0x040001FD RID: 509
		public static readonly DependencyProperty MyResZyFormatLstProperty = DependencyProperty.Register("MyResZyFormatLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x040001FE RID: 510
		public static readonly DependencyProperty MyResZyLbLstProperty = DependencyProperty.Register("MyResZyLbLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x040001FF RID: 511
		public static readonly DependencyProperty SyncResZyFormatLstProperty = DependencyProperty.Register("SyncResZyFormatLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x04000200 RID: 512
		public static readonly DependencyProperty SyncResZyLbLstProperty = DependencyProperty.Register("SyncResZyLbLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x04000201 RID: 513
		public static readonly DependencyProperty SyncResSelectSortProperty = DependencyProperty.Register("SyncResSelectSort", typeof(MetaModel), typeof(ResManageControl), new PropertyMetadata(null));

		// Token: 0x04000202 RID: 514
		public static readonly DependencyProperty SyncResSortLstProperty = DependencyProperty.Register("SyncResSortLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x04000203 RID: 515
		public static readonly DependencyProperty SyncLYLstProperty = DependencyProperty.Register("SyncLYLst", typeof(ObservableCollection<MetaModel>), typeof(ResManageControl), new PropertyMetadata(new ObservableCollection<MetaModel>()));

		// Token: 0x04000204 RID: 516
		public static readonly DependencyProperty SyncResSelectZyLyProperty = DependencyProperty.Register("SyncResSelectZyLy", typeof(MetaModel), typeof(ResManageControl), new PropertyMetadata(null));

		// Token: 0x04000205 RID: 517
		public static readonly DependencyProperty MyCollectFormatLstProperty = DependencyProperty.Register("MyCollectFormatLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));

		// Token: 0x04000206 RID: 518
		public static readonly DependencyProperty MyCollectLbLstProperty = DependencyProperty.Register("MyCollectLbLst", typeof(MetaList), typeof(ResManageControl), new PropertyMetadata(new MetaList()));
	}
}
