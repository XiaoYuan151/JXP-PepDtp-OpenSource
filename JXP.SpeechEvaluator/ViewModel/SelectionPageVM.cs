using System;
using JXP.SpeechEvaluator.Utility;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x02000031 RID: 49
	public class SelectionPageVM : NotificationObject
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007F0C File Offset: 0x0000610C
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007F14 File Offset: 0x00006114
		public PaginatedObservableCollection<SelectionPageItemVM> Cards
		{
			get
			{
				return this.mCards;
			}
			set
			{
				this.mCards = value;
				base.RaisePropertyChanged<PaginatedObservableCollection<SelectionPageItemVM>>(() => this.Cards);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007F52 File Offset: 0x00006152
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00007F5A File Offset: 0x0000615A
		public bool RolesVisible
		{
			get
			{
				return this.mRolesVisible;
			}
			set
			{
				this.mRolesVisible = value;
				base.RaisePropertyChanged<bool>(() => this.RolesVisible);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007F98 File Offset: 0x00006198
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007FA0 File Offset: 0x000061A0
		public bool ParagraphsVisible
		{
			get
			{
				return this.mParagraphsVisible;
			}
			set
			{
				this.mParagraphsVisible = value;
				base.RaisePropertyChanged<bool>(() => this.ParagraphsVisible);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007FDE File Offset: 0x000061DE
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00007FE6 File Offset: 0x000061E6
		public bool ToolBar1Visible
		{
			get
			{
				return this.mToolBar1Visible;
			}
			set
			{
				this.mToolBar1Visible = value;
				base.RaisePropertyChanged<bool>(() => this.ToolBar1Visible);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008024 File Offset: 0x00006224
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000802C File Offset: 0x0000622C
		public bool ToolBar2Visible
		{
			get
			{
				return this.mToolBar2Visible;
			}
			set
			{
				this.mToolBar2Visible = value;
				base.RaisePropertyChanged<bool>(() => this.ToolBar2Visible);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000806A File Offset: 0x0000626A
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00008072 File Offset: 0x00006272
		public bool LeftPageVisible
		{
			get
			{
				return this.mLeftPageVisible;
			}
			set
			{
				this.mLeftPageVisible = value;
				base.RaisePropertyChanged<bool>(() => this.LeftPageVisible);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000080B0 File Offset: 0x000062B0
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000080B8 File Offset: 0x000062B8
		public bool RightPageVisible
		{
			get
			{
				return this.mRightPageVisible;
			}
			set
			{
				this.mRightPageVisible = value;
				base.RaisePropertyChanged<bool>(() => this.RightPageVisible);
			}
		}

		// Token: 0x040000C7 RID: 199
		private PaginatedObservableCollection<SelectionPageItemVM> mCards;

		// Token: 0x040000C8 RID: 200
		private bool mRolesVisible;

		// Token: 0x040000C9 RID: 201
		private bool mParagraphsVisible;

		// Token: 0x040000CA RID: 202
		private bool mToolBar1Visible;

		// Token: 0x040000CB RID: 203
		private bool mToolBar2Visible;

		// Token: 0x040000CC RID: 204
		private bool mRightPageVisible;

		// Token: 0x040000CD RID: 205
		private bool mLeftPageVisible;
	}
}
