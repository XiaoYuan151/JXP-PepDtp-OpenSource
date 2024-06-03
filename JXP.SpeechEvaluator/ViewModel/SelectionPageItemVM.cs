using System;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x02000030 RID: 48
	public class SelectionPageItemVM : NotificationObject
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007D37 File Offset: 0x00005F37
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007D3F File Offset: 0x00005F3F
		public int Index { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007D48 File Offset: 0x00005F48
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007D50 File Offset: 0x00005F50
		public string Caption
		{
			get
			{
				return this.mCaption;
			}
			set
			{
				this.mCaption = value;
				base.RaisePropertyChanged<string>(() => this.Caption);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007D8E File Offset: 0x00005F8E
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007D96 File Offset: 0x00005F96
		public string Desc
		{
			get
			{
				return this.mDesc;
			}
			set
			{
				this.mDesc = value;
				base.RaisePropertyChanged<string>(() => this.Desc);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007DD4 File Offset: 0x00005FD4
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007DDC File Offset: 0x00005FDC
		public int MaxLines
		{
			get
			{
				return this.mMaxLines;
			}
			set
			{
				this.mMaxLines = value;
				base.RaisePropertyChanged<int>(() => this.MaxLines);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007E1A File Offset: 0x0000601A
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007E22 File Offset: 0x00006022
		public string RoleImg
		{
			get
			{
				return this.mRoleImg;
			}
			set
			{
				this.mRoleImg = value;
				base.RaisePropertyChanged<string>(() => this.RoleImg);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007E60 File Offset: 0x00006060
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007E68 File Offset: 0x00006068
		public string ImgName { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007E71 File Offset: 0x00006071
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00007E79 File Offset: 0x00006079
		public string RoleName
		{
			get
			{
				return this.mRoleName;
			}
			set
			{
				this.mRoleName = value;
				base.RaisePropertyChanged<string>(() => this.RoleName);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007EB7 File Offset: 0x000060B7
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00007EBF File Offset: 0x000060BF
		public bool IsSelected
		{
			get
			{
				return this.mIsSelected;
			}
			set
			{
				this.mIsSelected = value;
				base.RaisePropertyChanged<bool>(() => this.IsSelected);
			}
		}

		// Token: 0x040000BF RID: 191
		private string mCaption;

		// Token: 0x040000C0 RID: 192
		private string mDesc;

		// Token: 0x040000C1 RID: 193
		private string mRoleImg;

		// Token: 0x040000C2 RID: 194
		private string mRoleName;

		// Token: 0x040000C3 RID: 195
		private bool mIsSelected;

		// Token: 0x040000C4 RID: 196
		private int mMaxLines = 3;
	}
}
