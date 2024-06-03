using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JXP.Data;
using JXP.Logs;
using JXP.Models;
using JXP.PepUtility;
using JXP.Utilities;
using Microsoft.Practices.Prism.Commands;
using pep.Course.Models;
using pep.Course.Views;
using pep.sdk.reader.View.UserControls;
using pep.sdk.utility.Common;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000061 RID: 97
	public class MyHomeViewModel : BaseModel
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000236C8 File Offset: 0x000218C8
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x000236D0 File Offset: 0x000218D0
		public BookDataModel CurBookData
		{
			get
			{
				return this.mCurBookData;
			}
			set
			{
				this.mCurBookData = value;
				base.OnPropertyChanged<BookDataModel>(() => this.CurBookData);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0002370E File Offset: 0x0002190E
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00023716 File Offset: 0x00021916
		public ObservableCollection<CourseModel> LstCourse { get; set; } = new ObservableCollection<CourseModel>();

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0002371F File Offset: 0x0002191F
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00023727 File Offset: 0x00021927
		public CourseModel SelectCourse
		{
			get
			{
				return this.mSelectCourse;
			}
			set
			{
				this.mSelectCourse = value;
				base.OnPropertyChanged<CourseModel>(() => this.SelectCourse);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00023765 File Offset: 0x00021965
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0002376D File Offset: 0x0002196D
		public bool ShowWaiting
		{
			get
			{
				return this.mShowWaiting;
			}
			set
			{
				this.mShowWaiting = value;
				base.OnPropertyChanged<bool>(() => this.ShowWaiting);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000237AB File Offset: 0x000219AB
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x000237B3 File Offset: 0x000219B3
		public bool ShowDataMessage
		{
			get
			{
				return this.mShowDataMessage;
			}
			set
			{
				this.mShowDataMessage = value;
				base.OnPropertyChanged<bool>(() => this.ShowDataMessage);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x000237F1 File Offset: 0x000219F1
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x000237F9 File Offset: 0x000219F9
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00023837 File Offset: 0x00021A37
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0002383F File Offset: 0x00021A3F
		public bool ShowNoBook
		{
			get
			{
				return this.mShowNoBook;
			}
			set
			{
				this.mShowNoBook = value;
				base.OnPropertyChanged<bool>(() => this.ShowNoBook);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0002387D File Offset: 0x00021A7D
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00023885 File Offset: 0x00021A85
		public bool ShowNoCourse
		{
			get
			{
				return this.mShowNoCourse;
			}
			set
			{
				this.mShowNoCourse = value;
				base.OnPropertyChanged<bool>(() => this.ShowNoCourse);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000238C3 File Offset: 0x00021AC3
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x000238CB File Offset: 0x00021ACB
		public ObservableCollection<PageRadioModel> LstPageBtn
		{
			get
			{
				return this.mLstPageBtn;
			}
			set
			{
				this.mLstPageBtn = value;
				base.OnPropertyChanged<ObservableCollection<PageRadioModel>>(() => this.LstPageBtn);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00023909 File Offset: 0x00021B09
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00023911 File Offset: 0x00021B11
		public Window mOwner { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0002391A File Offset: 0x00021B1A
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00023922 File Offset: 0x00021B22
		public Action<int> NavigateToMenueCallback { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0002392B File Offset: 0x00021B2B
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00023933 File Offset: 0x00021B33
		public Action<CourseModel> EditeCourseCallback { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0002393C File Offset: 0x00021B3C
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00023944 File Offset: 0x00021B44
		public Action<CourseModel, BookChaptersInfo> GotoEditeCallback { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0002394D File Offset: 0x00021B4D
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00023955 File Offset: 0x00021B55
		public Action<CourseModel> StartClassCallback { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0002395E File Offset: 0x00021B5E
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00023966 File Offset: 0x00021B66
		public IsDownloadBook IsDownloadBookCallBack { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0002396F File Offset: 0x00021B6F
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00023977 File Offset: 0x00021B77
		public Func<string, bool> IsContainBookidCallback { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00023980 File Offset: 0x00021B80
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00023988 File Offset: 0x00021B88
		public ICommand OpenBookCommand { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00023991 File Offset: 0x00021B91
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00023999 File Offset: 0x00021B99
		public ICommand PageBtnCommand { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000239A2 File Offset: 0x00021BA2
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x000239AA File Offset: 0x00021BAA
		public ICommand EditeCourseCommand { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x000239B3 File Offset: 0x00021BB3
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x000239BB File Offset: 0x00021BBB
		public ICommand StartClassCommand { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000239C4 File Offset: 0x00021BC4
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x000239CC File Offset: 0x00021BCC
		public ICommand CreateCourseCommand { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000239D5 File Offset: 0x00021BD5
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x000239DD File Offset: 0x00021BDD
		public ICommand AddBookCommand { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x000239E6 File Offset: 0x00021BE6
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x000239EE File Offset: 0x00021BEE
		public ICommand MoreBookCommand { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000239F7 File Offset: 0x00021BF7
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x000239FF File Offset: 0x00021BFF
		public ICommand MoreCourseCommand { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00023A08 File Offset: 0x00021C08
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00023A10 File Offset: 0x00021C10
		public ICommand MyResEnterCommand { get; set; }

		// Token: 0x060006EC RID: 1772 RVA: 0x00023A1C File Offset: 0x00021C1C
		public MyHomeViewModel()
		{
			this.PageBtnCommand = new DelegateCommand(new Action(this.PageBtnClick));
			this.OpenBookCommand = new DelegateCommand<string>(new Action<string>(this.OpenBook));
			this.EditeCourseCommand = new DelegateCommand<string>(new Action<string>(this.EditeCourse));
			this.StartClassCommand = new DelegateCommand<string>(new Action<string>(this.StartClass));
			this.CreateCourseCommand = new DelegateCommand(new Action(this.CreateCourse));
			this.AddBookCommand = new DelegateCommand(new Action(this.AddBook));
			this.MoreCourseCommand = new DelegateCommand(new Action(this.MoreCourse));
			this.MyResEnterCommand = new DelegateCommand(new Action(this.MyResEnter));
			this.MoreBookCommand = new DelegateCommand(new Action(this.MoreBook));
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00023B58 File Offset: 0x00021D58
		private void PageBtnClick()
		{
			try
			{
				IEnumerable<PageRadioModel> enumerable = from a in this.LstPageBtn
				where a.Selected
				select a;
				PageRadioModel pageRadioModel = (enumerable != null) ? enumerable.FirstOrDefault<PageRadioModel>() : null;
				if (pageRadioModel != null)
				{
					if (pageRadioModel.Index >= 0 && pageRadioModel.Index < this.LstBookData.Count)
					{
						if (this.mCurDataIndex != pageRadioModel.Index)
						{
							this.mCurDataIndex = pageRadioModel.Index;
							this.SetCurData();
						}
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("我的主页下方翻页处理失败：[{0}]。", arg));
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00023C0C File Offset: 0x00021E0C
		private void OpenBook(string tbid)
		{
			WindowMessages.SendMessage2WinByName("OpenTextBook??<*>()??" + tbid, SdkConstants.JXP_PRODUCT_NAME_TEACHER);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00023C23 File Offset: 0x00021E23
		private void MoreBook()
		{
			Action<int> navigateToMenueCallback = this.NavigateToMenueCallback;
			if (navigateToMenueCallback == null)
			{
				return;
			}
			navigateToMenueCallback(0);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00023C38 File Offset: 0x00021E38
		private void EditeCourse(string courseId)
		{
			MyHomeViewModel.<EditeCourse>d__110 <EditeCourse>d__;
			<EditeCourse>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<EditeCourse>d__.<>4__this = this;
			<EditeCourse>d__.courseId = courseId;
			<EditeCourse>d__.<>1__state = -1;
			<EditeCourse>d__.<>t__builder.Start<MyHomeViewModel.<EditeCourse>d__110>(ref <EditeCourse>d__);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023C78 File Offset: 0x00021E78
		private void StartClass(string courseId)
		{
			MyHomeViewModel.<StartClass>d__111 <StartClass>d__;
			<StartClass>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<StartClass>d__.<>4__this = this;
			<StartClass>d__.courseId = courseId;
			<StartClass>d__.<>1__state = -1;
			<StartClass>d__.<>t__builder.Start<MyHomeViewModel.<StartClass>d__111>(ref <StartClass>d__);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00023CB8 File Offset: 0x00021EB8
		private void CreateCourse()
		{
			MyHomeViewModel.<CreateCourse>d__112 <CreateCourse>d__;
			<CreateCourse>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<CreateCourse>d__.<>4__this = this;
			<CreateCourse>d__.<>1__state = -1;
			<CreateCourse>d__.<>t__builder.Start<MyHomeViewModel.<CreateCourse>d__112>(ref <CreateCourse>d__);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00023CEF File Offset: 0x00021EEF
		private void GotoCourseEdite(CourseModel data, BookChaptersInfo chapterInfo)
		{
			Action<int> navigateToMenueCallback = this.NavigateToMenueCallback;
			if (navigateToMenueCallback != null)
			{
				navigateToMenueCallback(2);
			}
			Action<CourseModel, BookChaptersInfo> gotoEditeCallback = this.GotoEditeCallback;
			if (gotoEditeCallback == null)
			{
				return;
			}
			gotoEditeCallback(data, chapterInfo);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00023D15 File Offset: 0x00021F15
		private void AddBook()
		{
			Action<int> navigateToMenueCallback = this.NavigateToMenueCallback;
			if (navigateToMenueCallback == null)
			{
				return;
			}
			navigateToMenueCallback(1);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00023D28 File Offset: 0x00021F28
		private void MoreCourse()
		{
			try
			{
				Action<int> navigateToMenueCallback = this.NavigateToMenueCallback;
				if (navigateToMenueCallback != null)
				{
					navigateToMenueCallback(2);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("首页模块跳转到我的课程失败:[{0}]。", arg));
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00023D74 File Offset: 0x00021F74
		private void MyResEnter()
		{
			Action<int> navigateToMenueCallback = this.NavigateToMenueCallback;
			if (navigateToMenueCallback == null)
			{
				return;
			}
			navigateToMenueCallback(3);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00023D88 File Offset: 0x00021F88
		public void InitMyHomeData()
		{
			MyHomeViewModel.<InitMyHomeData>d__117 <InitMyHomeData>d__;
			<InitMyHomeData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<InitMyHomeData>d__.<>4__this = this;
			<InitMyHomeData>d__.<>1__state = -1;
			<InitMyHomeData>d__.<>t__builder.Start<MyHomeViewModel.<InitMyHomeData>d__117>(ref <InitMyHomeData>d__);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00023DC0 File Offset: 0x00021FC0
		private Task GetBookData()
		{
			MyHomeViewModel.<GetBookData>d__118 <GetBookData>d__;
			<GetBookData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Create();
			<GetBookData>d__.<>4__this = this;
			<GetBookData>d__.<>1__state = -1;
			<GetBookData>d__.<>t__builder.Start<MyHomeViewModel.<GetBookData>d__118>(ref <GetBookData>d__);
			return <GetBookData>d__.<>t__builder.Task;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00023E04 File Offset: 0x00022004
		private Task<bool> GetCourseData()
		{
			MyHomeViewModel.<GetCourseData>d__119 <GetCourseData>d__;
			<GetCourseData>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<GetCourseData>d__.<>4__this = this;
			<GetCourseData>d__.<>1__state = -1;
			<GetCourseData>d__.<>t__builder.Start<MyHomeViewModel.<GetCourseData>d__119>(ref <GetCourseData>d__);
			return <GetCourseData>d__.<>t__builder.Task;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00023E48 File Offset: 0x00022048
		private void SetCurData()
		{
			if (this.mCurDataIndex >= 0 && this.mCurDataIndex < this.LstBookData.Count)
			{
				foreach (PageRadioModel pageRadioModel in this.LstPageBtn)
				{
					pageRadioModel.Selected = (pageRadioModel.Index == this.mCurDataIndex);
				}
				this.CurBookData = this.LstBookData[this.mCurDataIndex];
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00023ED4 File Offset: 0x000220D4
		public void ClearData()
		{
			this.CurBookData = null;
			this.LstBookData.Clear();
			ObservableCollection<PageRadioModel> lstPageBtn = this.LstPageBtn;
			if (lstPageBtn != null)
			{
				lstPageBtn.Clear();
			}
			this.LstCourse.Clear();
			this.SelectCourse = null;
			this.mCurDataIndex = 0;
			this.ShowWaiting = false;
			this.ShowDataMessage = true;
			this.MessageContent = "数据检索中...";
			this.ShowNoBook = true;
			this.ShowNoCourse = true;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00023F44 File Offset: 0x00022144
		private static Task<CourseDataResultModel> GetCourseDataAsync(string courseId, string userId)
		{
			MyHomeViewModel.<GetCourseDataAsync>d__122 <GetCourseDataAsync>d__;
			<GetCourseDataAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<CourseDataResultModel>.Create();
			<GetCourseDataAsync>d__.courseId = courseId;
			<GetCourseDataAsync>d__.userId = userId;
			<GetCourseDataAsync>d__.<>1__state = -1;
			<GetCourseDataAsync>d__.<>t__builder.Start<MyHomeViewModel.<GetCourseDataAsync>d__122>(ref <GetCourseDataAsync>d__);
			return <GetCourseDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00023F90 File Offset: 0x00022190
		private Task<BookChaptersInfo> GetCurBookChapterInfo(string tbid)
		{
			MyHomeViewModel.<GetCurBookChapterInfo>d__123 <GetCurBookChapterInfo>d__;
			<GetCurBookChapterInfo>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<BookChaptersInfo>.Create();
			<GetCurBookChapterInfo>d__.<>4__this = this;
			<GetCurBookChapterInfo>d__.tbid = tbid;
			<GetCurBookChapterInfo>d__.<>1__state = -1;
			<GetCurBookChapterInfo>d__.<>t__builder.Start<MyHomeViewModel.<GetCurBookChapterInfo>d__123>(ref <GetCurBookChapterInfo>d__);
			return <GetCurBookChapterInfo>d__.<>t__builder.Task;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00023FDC File Offset: 0x000221DC
		private int GetCloudBookPageNum(string bookid)
		{
			int result2;
			try
			{
				List<OperationBookModel> result = OperationBookHelper.GetOssData().Result;
				OperationBookModel operationBookModel;
				if (result == null)
				{
					operationBookModel = null;
				}
				else
				{
					IEnumerable<OperationBookModel> enumerable = from a in result
					where a.BookId == bookid
					select a;
					operationBookModel = ((enumerable != null) ? enumerable.FirstOrDefault<OperationBookModel>() : null);
				}
				OperationBookModel operationBookModel2 = operationBookModel;
				int num;
				if (operationBookModel2 != null && int.TryParse(operationBookModel2.PageNum, out num))
				{
					result2 = num;
				}
				else
				{
					result2 = -1;
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("获取服务端教材id:[{0}],教材关闭时页码失败[{1}]。", bookid, arg));
				result2 = -1;
			}
			return result2;
		}

		// Token: 0x04000376 RID: 886
		private const string mSepFunc = "??<*>()??";

		// Token: 0x04000377 RID: 887
		private const string mSepParam = "&&<*>&&";

		// Token: 0x04000378 RID: 888
		private bool mShowWaiting;

		// Token: 0x04000379 RID: 889
		private bool mShowDataMessage = true;

		// Token: 0x0400037A RID: 890
		private string mMessageContent = "数据检索中...";

		// Token: 0x0400037B RID: 891
		private bool mShowNoBook = true;

		// Token: 0x0400037C RID: 892
		private bool mShowNoCourse = true;

		// Token: 0x0400037D RID: 893
		private int mCurDataIndex;

		// Token: 0x0400037E RID: 894
		private TransferTextBookDataAccess mTextBookDA = new TransferTextBookDataAccess();

		// Token: 0x0400037F RID: 895
		private UserChapterInfoDataAccess mUserChapterInfoDA = new UserChapterInfoDataAccess();

		// Token: 0x04000380 RID: 896
		private List<BookDataModel> LstBookData = new List<BookDataModel>();

		// Token: 0x04000381 RID: 897
		private BookDataModel mCurBookData;

		// Token: 0x04000382 RID: 898
		private ObservableCollection<PageRadioModel> mLstPageBtn = new ObservableCollection<PageRadioModel>();

		// Token: 0x04000383 RID: 899
		private CourseModel mSelectCourse;
	}
}
