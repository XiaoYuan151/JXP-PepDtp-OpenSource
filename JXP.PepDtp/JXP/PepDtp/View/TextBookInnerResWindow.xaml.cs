using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Controls.UserControls;
using JXP.Logs;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.View.UserControls;
using JXP.PepUtility;
using JXP.Windows;
using pep.sdk.reader.View;

namespace JXP.PepDtp.View
{
	// Token: 0x02000032 RID: 50
	public partial class TextBookInnerResWindow : Window, INotifyPropertyChanged
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000102A2 File Offset: 0x0000E4A2
		// (set) Token: 0x060002AB RID: 683 RVA: 0x000102AA File Offset: 0x0000E4AA
		public string TextBookID
		{
			get
			{
				return this.mTextBookID;
			}
			set
			{
				this.mTextBookID = value;
				this.IsUsePinyinFont = UtilityHelper.IsUsePinyinFont(value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000102BF File Offset: 0x0000E4BF
		// (set) Token: 0x060002AD RID: 685 RVA: 0x000102C7 File Offset: 0x0000E4C7
		public bool IsUsePinyinFont
		{
			get
			{
				return this.mIsUsePinyinFont;
			}
			set
			{
				this.mIsUsePinyinFont = value;
				this.OnPropertyRaised("IsUsePinyinFont");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000102DB File Offset: 0x0000E4DB
		// (set) Token: 0x060002AF RID: 687 RVA: 0x000102E3 File Offset: 0x0000E4E3
		public List<string> LstChapterId { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000102EC File Offset: 0x0000E4EC
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public string TitleName
		{
			get
			{
				return this.mTitleName;
			}
			set
			{
				this.mTitleName = value;
				this.OnPropertyRaised("TitleName");
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002B2 RID: 690 RVA: 0x00010308 File Offset: 0x0000E508
		// (remove) Token: 0x060002B3 RID: 691 RVA: 0x00010340 File Offset: 0x0000E540
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060002B4 RID: 692 RVA: 0x00010375 File Offset: 0x0000E575
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001038E File Offset: 0x0000E58E
		public TextBookInnerResWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000103B2 File Offset: 0x0000E5B2
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Owner = null;
			base.Hide();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000103C1 File Offset: 0x0000E5C1
		private void radio_Click(object sender, RoutedEventArgs e)
		{
			this.InitData();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000103CC File Offset: 0x0000E5CC
		private void SearchMyResData()
		{
			try
			{
				this.ucMyRes.SearchDataAsync("", "", "", this.TextBookID, this.LstChapterId);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("阅读内页我的资源加载数据失败：{0}。", arg));
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001042C File Offset: 0x0000E62C
		private void NqResSearch()
		{
			try
			{
				this.ucSyncRes.SearchDataAsync("", "", "", this.TextBookID, this.LstChapterId, "", "10", "");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "阅读内页同步资源检索出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000104A4 File Offset: 0x0000E6A4
		private void MyCollectSearch()
		{
			try
			{
				this.ucMyCollectRes.SearchDataAsync("", "", "", this.TextBookID, this.LstChapterId, "", "13", "");
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "阅读内页我的收藏资源检索出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001051C File Offset: 0x0000E71C
		private void lblChapterName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				string textBookID = this.TextBookID;
				string strChapterid = string.Empty;
				if (this.LstChapterId != null && this.LstChapterId.Count > 0)
				{
					strChapterid = this.LstChapterId[0];
				}
				new TextbookSelectChapter(textBookID, strChapterid, false, false)
				{
					IsShowActivedBooks = true,
					ActivedBooksId = CommonParamter.Instance.lstAuthorizeBookIds,
					SetBookChapterInfoCallBack = new SetBookChapterInfo(this.SelectChapterInfo),
					Owner = this,
					WindowStartupLocation = WindowStartupLocation.CenterOwner
				}.ShowDialog();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
				CustomMessageBox.Info("教材章节选择启动失败!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000105DC File Offset: 0x0000E7DC
		private void SelectChapterInfo(Tuple<string, string, string, string, string> tupleInfo)
		{
			try
			{
				string item = tupleInfo.Item1;
				string item2 = tupleInfo.Item2;
				string item3 = tupleInfo.Item3;
				string item4 = tupleInfo.Item4;
				if (string.IsNullOrEmpty(item) || string.IsNullOrEmpty(item2))
				{
					CustomMessageBox.Info("选择的教材信息为空!", "确定", "", this, WindowStartupLocation.CenterOwner, false);
				}
				else
				{
					List<string> list = new List<string>();
					if (!string.IsNullOrEmpty(item3))
					{
						list.Add(item3);
						this.TitleName = item4;
					}
					else
					{
						this.TitleName = item2;
					}
					this.TextBookID = item;
					this.LstChapterId = list;
					this.InitData();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010694 File Offset: 0x0000E894
		public void InitData()
		{
			if (!this.radioMyRes.IsChecked.Value && !this.radioSyncRes.IsChecked.Value && !this.radioMyCollect.IsChecked.Value)
			{
				this.radioMyRes.IsChecked = new bool?(true);
			}
			if (this.radioMyRes.IsChecked.Value)
			{
				this.SearchMyResData();
				return;
			}
			if (this.radioSyncRes.IsChecked.Value)
			{
				this.NqResSearch();
				return;
			}
			if (this.radioMyCollect.IsChecked.Value)
			{
				this.MyCollectSearch();
			}
		}

		// Token: 0x04000145 RID: 325
		private string mTitleName = string.Empty;

		// Token: 0x04000146 RID: 326
		private string mTextBookID = string.Empty;

		// Token: 0x04000147 RID: 327
		private bool mIsUsePinyinFont;
	}
}
