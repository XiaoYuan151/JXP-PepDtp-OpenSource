using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using JXP.Models;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000060 RID: 96
	public class ModifyPwdViewModel : BaseModel
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000232A5 File Offset: 0x000214A5
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x000232AD File Offset: 0x000214AD
		public string PhoneNum
		{
			get
			{
				return this.mPhoneNum;
			}
			set
			{
				this.mPhoneNum = value;
				if (!string.IsNullOrEmpty(value) && value.Length == 11)
				{
					this.ShowPhoneNum = Regex.Replace(value, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
					return;
				}
				this.ShowPhoneNum = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000232E6 File Offset: 0x000214E6
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000232EE File Offset: 0x000214EE
		public string ShowPhoneNum
		{
			get
			{
				return this.mShowPhoneNum;
			}
			set
			{
				this.mShowPhoneNum = value;
				base.OnPropertyChanged<string>(() => this.ShowPhoneNum);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0002332C File Offset: 0x0002152C
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00023334 File Offset: 0x00021534
		public Visibility StepOneVisibility
		{
			get
			{
				return this.mStepOneVisibility;
			}
			set
			{
				this.mStepOneVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.StepOneVisibility);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00023372 File Offset: 0x00021572
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0002337A File Offset: 0x0002157A
		public Visibility StepTwoVisibility
		{
			get
			{
				return this.mStepTwoVisibility;
			}
			set
			{
				this.mStepTwoVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.StepTwoVisibility);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000233B8 File Offset: 0x000215B8
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x000233C0 File Offset: 0x000215C0
		public string Code
		{
			get
			{
				return this.mCode;
			}
			set
			{
				this.mCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.CodeTip = string.Empty;
				}
				base.OnPropertyChanged<string>(() => this.Code);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0002341C File Offset: 0x0002161C
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00023424 File Offset: 0x00021624
		public string CodeTip
		{
			get
			{
				return this.mCodeTip;
			}
			set
			{
				this.mCodeTip = value;
				base.OnPropertyChanged<string>(() => this.CodeTip);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00023462 File Offset: 0x00021662
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0002346C File Offset: 0x0002166C
		public string OldCode
		{
			get
			{
				return this.mOldCode;
			}
			set
			{
				this.mOldCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.OldCode);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000234C8 File Offset: 0x000216C8
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x000234D0 File Offset: 0x000216D0
		public string ResetCode
		{
			get
			{
				return this.mResetCode;
			}
			set
			{
				this.mResetCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.ResetCode);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002352C File Offset: 0x0002172C
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00023534 File Offset: 0x00021734
		public string ResetCodeAgain
		{
			get
			{
				return this.mResetCodeAgain;
			}
			set
			{
				this.mResetCodeAgain = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.ResetCodeAgain);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00023590 File Offset: 0x00021790
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00023598 File Offset: 0x00021798
		public string ResetCodeTip
		{
			get
			{
				return this.mResetCodeTip;
			}
			set
			{
				this.mResetCodeTip = value;
				base.OnPropertyChanged<string>(() => this.ResetCodeTip);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000235D8 File Offset: 0x000217D8
		public Task<List<UserGetVerificationCodeModel>> GetPhoneCodeAsync()
		{
			ModifyPwdViewModel.<GetPhoneCodeAsync>d__40 <GetPhoneCodeAsync>d__;
			<GetPhoneCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<UserGetVerificationCodeModel>>.Create();
			<GetPhoneCodeAsync>d__.<>4__this = this;
			<GetPhoneCodeAsync>d__.<>1__state = -1;
			<GetPhoneCodeAsync>d__.<>t__builder.Start<ModifyPwdViewModel.<GetPhoneCodeAsync>d__40>(ref <GetPhoneCodeAsync>d__);
			return <GetPhoneCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0002361C File Offset: 0x0002181C
		public Task<string> CheckPhoneCodeAsync()
		{
			ModifyPwdViewModel.<CheckPhoneCodeAsync>d__41 <CheckPhoneCodeAsync>d__;
			<CheckPhoneCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<string>.Create();
			<CheckPhoneCodeAsync>d__.<>4__this = this;
			<CheckPhoneCodeAsync>d__.<>1__state = -1;
			<CheckPhoneCodeAsync>d__.<>t__builder.Start<ModifyPwdViewModel.<CheckPhoneCodeAsync>d__41>(ref <CheckPhoneCodeAsync>d__);
			return <CheckPhoneCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00023660 File Offset: 0x00021860
		public Task<string> ResetCodeAsync()
		{
			ModifyPwdViewModel.<ResetCodeAsync>d__42 <ResetCodeAsync>d__;
			<ResetCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<string>.Create();
			<ResetCodeAsync>d__.<>4__this = this;
			<ResetCodeAsync>d__.<>1__state = -1;
			<ResetCodeAsync>d__.<>t__builder.Start<ModifyPwdViewModel.<ResetCodeAsync>d__42>(ref <ResetCodeAsync>d__);
			return <ResetCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400036C RID: 876
		private string mPhoneNum;

		// Token: 0x0400036D RID: 877
		private string mShowPhoneNum;

		// Token: 0x0400036E RID: 878
		private Visibility mStepOneVisibility;

		// Token: 0x0400036F RID: 879
		private Visibility mStepTwoVisibility = Visibility.Collapsed;

		// Token: 0x04000370 RID: 880
		private string mCode;

		// Token: 0x04000371 RID: 881
		private string mCodeTip = string.Empty;

		// Token: 0x04000372 RID: 882
		private string mResetCodeTip = string.Empty;

		// Token: 0x04000373 RID: 883
		private string mOldCode;

		// Token: 0x04000374 RID: 884
		private string mResetCode;

		// Token: 0x04000375 RID: 885
		private string mResetCodeAgain;
	}
}
