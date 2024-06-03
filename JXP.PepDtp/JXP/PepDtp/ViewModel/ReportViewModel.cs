using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JXP.Models;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200006A RID: 106
	public class ReportViewModel : BaseModel
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000258E3 File Offset: 0x00023AE3
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000258EB File Offset: 0x00023AEB
		public IDictionary<string, string> DicParameters
		{
			get
			{
				return this.mDicParameters;
			}
			set
			{
				this.mDicParameters = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000258F4 File Offset: 0x00023AF4
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x000258FC File Offset: 0x00023AFC
		public string Description
		{
			get
			{
				return this.mDescription;
			}
			set
			{
				this.mDescription = value;
				base.OnPropertyChanged<string>(() => this.Description);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0002593A File Offset: 0x00023B3A
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00025942 File Offset: 0x00023B42
		public bool DescErrOccurred
		{
			get
			{
				return this.mDescErrOccurred;
			}
			set
			{
				this.mDescErrOccurred = value;
				base.OnPropertyChanged<bool>(() => this.DescErrOccurred);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00025980 File Offset: 0x00023B80
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00025988 File Offset: 0x00023B88
		public bool BadContentIsChecked
		{
			get
			{
				return this.mBadContentIsChecked;
			}
			set
			{
				this.mBadContentIsChecked = value;
				base.OnPropertyChanged<bool>(() => this.BadContentIsChecked);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000259C6 File Offset: 0x00023BC6
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x000259CE File Offset: 0x00023BCE
		public bool CorrectionIsChecked
		{
			get
			{
				return this.mCorrectionIsChecked;
			}
			set
			{
				this.mCorrectionIsChecked = value;
				base.OnPropertyChanged<bool>(() => this.CorrectionIsChecked);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00025A0C File Offset: 0x00023C0C
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00025A14 File Offset: 0x00023C14
		public string UserName
		{
			get
			{
				return this.mUserName;
			}
			set
			{
				this.mUserName = value;
				base.OnPropertyChanged<string>(() => this.UserName);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00025A52 File Offset: 0x00023C52
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00025A5A File Offset: 0x00023C5A
		public string Telephone
		{
			get
			{
				return this.mTelephone;
			}
			set
			{
				this.mTelephone = value;
				base.OnPropertyChanged<string>(() => this.Telephone);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00025A98 File Offset: 0x00023C98
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00025AA0 File Offset: 0x00023CA0
		public bool TelErrOccurred
		{
			get
			{
				return this.mTelErrOccurred;
			}
			set
			{
				this.mTelErrOccurred = value;
				base.OnPropertyChanged<bool>(() => this.TelErrOccurred);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00025ADE File Offset: 0x00023CDE
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00025AE6 File Offset: 0x00023CE6
		public string Email
		{
			get
			{
				return this.mEmail;
			}
			set
			{
				this.mEmail = value;
				base.OnPropertyChanged<string>(() => this.Email);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00025B24 File Offset: 0x00023D24
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00025B2C File Offset: 0x00023D2C
		public bool EmailErrOccurred
		{
			get
			{
				return this.mEmailErrOccurred;
			}
			set
			{
				this.mEmailErrOccurred = value;
				base.OnPropertyChanged<bool>(() => this.EmailErrOccurred);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00025B6C File Offset: 0x00023D6C
		public void InitData(IDictionary<string, string> parameters)
		{
			if (parameters == null || !parameters.ContainsKey("userId"))
			{
				throw new ArgumentException("参数错误！");
			}
			this.UserName = parameters["contactName"];
			this.Telephone = parameters["contactPhone"];
			this.mDicParameters = parameters;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00025BC0 File Offset: 0x00023DC0
		public void ClearData()
		{
			this.BadContentIsChecked = true;
			this.CorrectionIsChecked = false;
			this.Description = string.Empty;
			this.DescErrOccurred = false;
			this.Telephone = string.Empty;
			this.TelErrOccurred = false;
			this.Email = string.Empty;
			this.EmailErrOccurred = false;
			this.UserName = string.Empty;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00025C1C File Offset: 0x00023E1C
		public Task<ReportResultModel> PostDataAsync(Dictionary<string, string> parameters)
		{
			ReportViewModel.<PostDataAsync>d__42 <PostDataAsync>d__;
			<PostDataAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<ReportResultModel>.Create();
			<PostDataAsync>d__.parameters = parameters;
			<PostDataAsync>d__.<>1__state = -1;
			<PostDataAsync>d__.<>t__builder.Start<ReportViewModel.<PostDataAsync>d__42>(ref <PostDataAsync>d__);
			return <PostDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00025C60 File Offset: 0x00023E60
		public Dictionary<string, string> GetPostParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["userId"] = this.mDicParameters["userId"];
			dictionary["userType"] = this.mDicParameters["userType"];
			dictionary["targetId"] = this.mDicParameters["targetId"];
			dictionary["targetType"] = this.mDicParameters["targetType"];
			if (this.BadContentIsChecked)
			{
				dictionary["contentType"] = "1";
			}
			else if (this.CorrectionIsChecked)
			{
				dictionary["contentType"] = "2";
			}
			dictionary["content"] = this.Description;
			dictionary["contactName"] = this.UserName;
			dictionary["contactPhone"] = this.Telephone;
			dictionary["contactEmail"] = this.Email;
			return dictionary;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00025D58 File Offset: 0x00023F58
		public bool CheckInputData()
		{
			if (string.IsNullOrWhiteSpace(this.Description))
			{
				this.DescErrOccurred = true;
				return false;
			}
			if (!string.IsNullOrWhiteSpace(this.Telephone) && !this.IsValidPhone(this.Telephone.Trim()))
			{
				this.TelErrOccurred = true;
				return false;
			}
			if (!string.IsNullOrWhiteSpace(this.Email) && !this.IsValidEmail(this.Email.Trim()))
			{
				this.EmailErrOccurred = true;
				return false;
			}
			return true;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00025DD0 File Offset: 0x00023FD0
		private bool IsValidEmail(string address)
		{
			bool result;
			try
			{
				new MailAddress(address);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00025E00 File Offset: 0x00024000
		private bool IsValidPhone(string number)
		{
			return Regex.IsMatch(number, "^\\d+$");
		}

		// Token: 0x040003CC RID: 972
		private IDictionary<string, string> mDicParameters;

		// Token: 0x040003CD RID: 973
		private string mDescription = string.Empty;

		// Token: 0x040003CE RID: 974
		private bool mDescErrOccurred;

		// Token: 0x040003CF RID: 975
		private bool mBadContentIsChecked = true;

		// Token: 0x040003D0 RID: 976
		private bool mCorrectionIsChecked;

		// Token: 0x040003D1 RID: 977
		private string mUserName = string.Empty;

		// Token: 0x040003D2 RID: 978
		private string mTelephone = string.Empty;

		// Token: 0x040003D3 RID: 979
		private bool mTelErrOccurred;

		// Token: 0x040003D4 RID: 980
		private string mEmail = string.Empty;

		// Token: 0x040003D5 RID: 981
		private bool mEmailErrOccurred;
	}
}
