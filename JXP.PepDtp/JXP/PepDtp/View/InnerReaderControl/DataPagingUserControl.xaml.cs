using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JXP.PepDtp.View.InnerReaderControl
{
	// Token: 0x02000052 RID: 82
	public partial class DataPagingUserControl : UserControl
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001E74B File Offset: 0x0001C94B
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001E753 File Offset: 0x0001C953
		public PageIndexChannged PageIndexChanngedCallBack { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001E75C File Offset: 0x0001C95C
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001E764 File Offset: 0x0001C964
		public int TotalPages
		{
			get
			{
				return this.mTotalPages;
			}
			set
			{
				this.mTotalPages = value;
				for (int i = 1; i < 11; i++)
				{
					Button button = this.Wrap1.Children[i] as Button;
					if (i > this.mTotalPages)
					{
						button.Visibility = Visibility.Collapsed;
					}
					else
					{
						button.Visibility = Visibility.Visible;
					}
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001E7B5 File Offset: 0x0001C9B5
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		public int CurrentPage
		{
			get
			{
				return this.mCurPage;
			}
			set
			{
				this.mCurPage = value;
				if (value == 1)
				{
					for (int i = 1; i < 11; i++)
					{
						Button button = this.Wrap1.Children[i] as Button;
						if (i == 1)
						{
							button.Style = (Style)base.FindResource("SelectPagingButtonStyle");
						}
						else
						{
							button.Style = (Style)base.FindResource("PagingButtonDefault");
						}
						button.Content = i.ToString();
					}
				}
				if (this.mTotalPages == 1)
				{
					this.btnPrePage.Style = (Style)base.FindResource("PagingButtonEnnable");
					this.btnNextPage.Style = (Style)base.FindResource("PagingButtonEnnable");
					return;
				}
				if (this.mCurPage == 1)
				{
					this.btnPrePage.Style = (Style)base.FindResource("PagingButtonEnnable");
					this.btnNextPage.Style = (Style)base.FindResource("PagingButtonDefault");
					return;
				}
				if (this.mCurPage == this.mTotalPages)
				{
					this.btnPrePage.Style = (Style)base.FindResource("PagingButtonDefault");
					this.btnNextPage.Style = (Style)base.FindResource("PagingButtonEnnable");
					return;
				}
				this.btnPrePage.Style = (Style)base.FindResource("PagingButtonDefault");
				this.btnNextPage.Style = (Style)base.FindResource("PagingButtonDefault");
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001E936 File Offset: 0x0001CB36
		public DataPagingUserControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001E944 File Offset: 0x0001CB44
		private void ClickHandler(string buttonContent)
		{
			this.mPrevPage = this.mCurPage;
			if (buttonContent == "上一页")
			{
				if (this.CurrentPage == 1)
				{
					return;
				}
				this.CurrentPage--;
			}
			else if (buttonContent == "下一页")
			{
				if (this.CurrentPage == this.TotalPages)
				{
					return;
				}
				this.CurrentPage++;
			}
			else
			{
				this.CurrentPage = (int)Convert.ToInt16(buttonContent);
			}
			if (this.mPrevPage == this.mCurPage)
			{
				return;
			}
			this.ChangePage(true);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		private void ChangePage(bool bCallBackChangePageFlg = true)
		{
			int num = (int)Convert.ToInt16((this.Wrap1.Children[1] as Button).Content);
			int num2;
			if (this.mTotalPages > 10)
			{
				num2 = (int)Convert.ToInt16((this.Wrap1.Children[10] as Button).Content);
			}
			else
			{
				num2 = this.mTotalPages;
			}
			int num3 = (num + num2) / 2 + 1;
			if (this.mCurPage > this.mPrevPage)
			{
				if (this.mCurPage > num3)
				{
					int num4 = this.mCurPage - num3;
					if (num4 + num2 < this.mTotalPages)
					{
						for (int i = 1; i < 11; i++)
						{
							(this.Wrap1.Children[i] as Button).Content = (int)Convert.ToInt16((this.Wrap1.Children[i] as Button).Content) + num4;
						}
					}
					else
					{
						num4 = this.mTotalPages - num2;
						for (int j = 1; j < 11; j++)
						{
							(this.Wrap1.Children[j] as Button).Content = (int)Convert.ToInt16((this.Wrap1.Children[j] as Button).Content) + num4;
						}
					}
				}
			}
			else if (this.mCurPage < num3)
			{
				int num4 = num3 - this.mCurPage;
				if (num > num4)
				{
					for (int k = 1; k < 11; k++)
					{
						(this.Wrap1.Children[k] as Button).Content = (int)Convert.ToInt16((this.Wrap1.Children[k] as Button).Content) - num4;
					}
				}
				else
				{
					num4 = num - 1;
					for (int l = 1; l < 11; l++)
					{
						(this.Wrap1.Children[l] as Button).Content = (int)Convert.ToInt16((this.Wrap1.Children[l] as Button).Content) - num4;
					}
				}
			}
			this.SetbuttonColor();
			if (this.PageIndexChanngedCallBack != null && bCallBackChangePageFlg)
			{
				this.PageIndexChanngedCallBack(this.mCurPage);
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		public void SetbuttonColor()
		{
			for (int i = 1; i < 11; i++)
			{
				Button button = this.Wrap1.Children[i] as Button;
				int num;
				if (button != null && button.Content != null && int.TryParse(button.Content.ToString(), out num))
				{
					if (num == this.mCurPage)
					{
						button.Style = (Style)base.FindResource("SelectPagingButtonStyle");
					}
					else
					{
						button.Style = (Style)base.FindResource("PagingButtonDefault");
					}
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		public void RessetPageNum(int Pagenum)
		{
			Button button = this.Wrap1.Children[1] as Button;
			int num;
			if (button == null || button.Content == null || !int.TryParse(button.Content.ToString(), out num) || button.Visibility != Visibility.Visible || num <= 1)
			{
				return;
			}
			for (int i = 1; i < 11; i++)
			{
				button = (this.Wrap1.Children[i] as Button);
				if (button != null && button.Content != null && int.TryParse(button.Content.ToString(), out num) && button.Visibility == Visibility.Visible && num > 1)
				{
					(this.Wrap1.Children[i] as Button).Content = (int)(Convert.ToInt16((this.Wrap1.Children[i] as Button).Content) - 1);
				}
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001ED8B File Offset: 0x0001CF8B
		private void btnPage_Click(object sender, RoutedEventArgs e)
		{
			this.ClickHandler((e.Source as Button).Content.ToString());
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		private void btnPrePage_Click(object sender, RoutedEventArgs e)
		{
			this.ClickHandler("上一页");
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001EDB5 File Offset: 0x0001CFB5
		private void btnNextPage_Click(object sender, RoutedEventArgs e)
		{
			this.ClickHandler("下一页");
		}

		// Token: 0x040002EF RID: 751
		private int mTotalPages;

		// Token: 0x040002F0 RID: 752
		private int mCurPage;

		// Token: 0x040002F1 RID: 753
		private int mPrevPage;
	}
}
