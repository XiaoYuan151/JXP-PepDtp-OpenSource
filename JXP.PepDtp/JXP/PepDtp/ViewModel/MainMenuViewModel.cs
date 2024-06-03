using System;
using System.Collections.ObjectModel;
using JXP.Models;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005F RID: 95
	public class MainMenuViewModel : BaseModel
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00023249 File Offset: 0x00021449
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00023251 File Offset: 0x00021451
		public ObservableCollection<MenuItemModel> MyMenuLst
		{
			get
			{
				return this.mMyMenuLst;
			}
			set
			{
				this.mMyMenuLst = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0002325A File Offset: 0x0002145A
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00023262 File Offset: 0x00021462
		public ObservableCollection<MenuItemModel> CentreMenuLst
		{
			get
			{
				return this.mCentreMenuLst;
			}
			set
			{
				this.mCentreMenuLst = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002326B File Offset: 0x0002146B
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00023273 File Offset: 0x00021473
		public ObservableCollection<MenuItemModel> MenuLst
		{
			get
			{
				return this.mMenuLst;
			}
			set
			{
				this.mMenuLst = value;
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void InitData()
		{
		}

		// Token: 0x04000369 RID: 873
		private ObservableCollection<MenuItemModel> mMyMenuLst = new ObservableCollection<MenuItemModel>();

		// Token: 0x0400036A RID: 874
		private ObservableCollection<MenuItemModel> mCentreMenuLst = new ObservableCollection<MenuItemModel>();

		// Token: 0x0400036B RID: 875
		private ObservableCollection<MenuItemModel> mMenuLst = new ObservableCollection<MenuItemModel>();
	}
}
