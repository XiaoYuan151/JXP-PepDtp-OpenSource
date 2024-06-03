using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.Windows;
using pep.sdk.books.View.DtpUserControls;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x0200003A RID: 58
	public partial class DigitalTextbookControl : UserControl, INotifyPropertyChanged
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00012D95 File Offset: 0x00010F95
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00012D9D File Offset: 0x00010F9D
		public bool MyBookChecked
		{
			get
			{
				return this.mMyBookChecked;
			}
			set
			{
				this.mMyBookChecked = value;
				this.OnPropertyRaised("MyBookChecked");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00012DB1 File Offset: 0x00010FB1
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00012DB9 File Offset: 0x00010FB9
		public bool BookCenterChecked
		{
			get
			{
				return this.mBookCenterChecked;
			}
			set
			{
				this.mBookCenterChecked = value;
				this.OnPropertyRaised("BookCenterChecked");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00012DCD File Offset: 0x00010FCD
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00012DD5 File Offset: 0x00010FD5
		public bool ShareBookChecked
		{
			get
			{
				return this.mShareBookChecked;
			}
			set
			{
				this.mShareBookChecked = value;
				this.OnPropertyRaised("ShareBookChecked");
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000329 RID: 809 RVA: 0x00012DEC File Offset: 0x00010FEC
		// (remove) Token: 0x0600032A RID: 810 RVA: 0x00012E24 File Offset: 0x00011024
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600032B RID: 811 RVA: 0x00012E59 File Offset: 0x00011059
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00012E72 File Offset: 0x00011072
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00012E84 File Offset: 0x00011084
		public bool IsShowNoticeCount
		{
			get
			{
				return (bool)base.GetValue(DigitalTextbookControl.IsShowNoticeCountProperty);
			}
			set
			{
				base.SetValue(DigitalTextbookControl.IsShowNoticeCountProperty, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00012E97 File Offset: 0x00011097
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00012EA9 File Offset: 0x000110A9
		public int NoticeCount
		{
			get
			{
				return (int)base.GetValue(DigitalTextbookControl.NoticeCountProperty);
			}
			set
			{
				base.SetValue(DigitalTextbookControl.NoticeCountProperty, value);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00012EBC File Offset: 0x000110BC
		public DigitalTextbookControl()
		{
			this.InitializeComponent();
			base.Loaded += this.DigitalTextbookControl_Loaded;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012EE3 File Offset: 0x000110E3
		private void DigitalTextbookControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.ucMyBook.OnAddTextbook = new EventHandler(this.BtnAddTextBook_Click);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012EFC File Offset: 0x000110FC
		private void BtnAddTextBook_Click(object sender, EventArgs e)
		{
			if (!this.radioBookCenter.IsEnabled)
			{
				this.ReceiveToastInfo(new ToastInfo
				{
					Content = "当前网络服务状态出现问题，请稍后尝试!",
					IconType = new ToastMessageIconType?(ToastMessageIconType.OK)
				});
				return;
			}
			this.BookCenterChecked = true;
			this.InitData();
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200168",
				Passive = "我的教材",
				FromPos = DigitalTextbookControl.mClassPath + ".BtnAddTextBook_Click"
			});
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012F80 File Offset: 0x00011180
		public void SetMyBookDownloadCount(string count)
		{
			int num;
			if (!int.TryParse(count, out num))
			{
				this.NoticeCount = 0;
				this.IsShowNoticeCount = false;
				return;
			}
			this.NoticeCount = num;
			if (num > 0)
			{
				this.IsShowNoticeCount = true;
				return;
			}
			this.IsShowNoticeCount = false;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00012FC0 File Offset: 0x000111C0
		private void ReceiveToastInfo(ToastInfo info)
		{
			if (base.Dispatcher.CheckAccess())
			{
				ToastWin.GetToaster(this.topgrid, true).ShowInfo(info);
				return;
			}
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.ReceiveToastInfo(info);
			}), new object[0]);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013024 File Offset: 0x00011224
		private void radio_Click(object sender, RoutedEventArgs e)
		{
			this.InitData();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001302C File Offset: 0x0001122C
		public void InitData()
		{
			try
			{
				if (!this.MyBookChecked && !this.BookCenterChecked && !this.ShareBookChecked)
				{
					this.MyBookChecked = true;
				}
				if (!this.MyBookChecked && this.BookCenterChecked)
				{
					this.ucBookCenter.SearchBookData();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("数字教材获取数据失败：{0}", arg));
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000130A0 File Offset: 0x000112A0
		public void ClrarData()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.MyBookChecked = true;
				this.BookCenterChecked = false;
				this.ShareBookChecked = false;
				this.ucMyBook.HandleLogout();
			}), new object[0]);
		}

		// Token: 0x04000198 RID: 408
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"UserControls",
			"DigitalTextbookControl"
		});

		// Token: 0x04000199 RID: 409
		private bool mMyBookChecked = true;

		// Token: 0x0400019A RID: 410
		private bool mBookCenterChecked;

		// Token: 0x0400019B RID: 411
		private bool mShareBookChecked;

		// Token: 0x0400019D RID: 413
		public static readonly DependencyProperty IsShowNoticeCountProperty = DependencyProperty.Register("IsShowNoticeCount", typeof(bool), typeof(DigitalTextbookControl), new PropertyMetadata(false));

		// Token: 0x0400019E RID: 414
		public static readonly DependencyProperty NoticeCountProperty = DependencyProperty.Register("NoticeCount", typeof(int), typeof(DigitalTextbookControl), new PropertyMetadata(0));
	}
}
