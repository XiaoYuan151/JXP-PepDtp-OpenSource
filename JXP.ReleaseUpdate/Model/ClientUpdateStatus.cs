using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace JXP.ReleaseUpdate.Model
{
	// Token: 0x02000010 RID: 16
	public class ClientUpdateStatus : INotifyPropertyChanged
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000492E File Offset: 0x00002B2E
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004936 File Offset: 0x00002B36
		public string UpdateCenter
		{
			get
			{
				return this.mUpdateCenter;
			}
			set
			{
				this.mUpdateCenter = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000493F File Offset: 0x00002B3F
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004947 File Offset: 0x00002B47
		public string ClientID
		{
			get
			{
				return this.mClientID;
			}
			set
			{
				this.mClientID = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004950 File Offset: 0x00002B50
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004958 File Offset: 0x00002B58
		public string RemoteRootPath
		{
			get
			{
				return this.mRemoteRootPath;
			}
			set
			{
				this.mRemoteRootPath = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004961 File Offset: 0x00002B61
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004969 File Offset: 0x00002B69
		public string MainProcessPath
		{
			get
			{
				return this.mMainProcessPath;
			}
			set
			{
				this.mMainProcessPath = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004972 File Offset: 0x00002B72
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000497A File Offset: 0x00002B7A
		public string LocalRootPath
		{
			get
			{
				return this.mLocalRootPath;
			}
			set
			{
				this.mLocalRootPath = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004983 File Offset: 0x00002B83
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000498B File Offset: 0x00002B8B
		public string TotalSize
		{
			get
			{
				return this.mTotalSize;
			}
			set
			{
				this.mTotalSize = value;
				this.OnPropertyChanged<string>(() => this.TotalSize);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000049C9 File Offset: 0x00002BC9
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000049D1 File Offset: 0x00002BD1
		public double ProgressValue
		{
			get
			{
				return this.mProgressValue;
			}
			set
			{
				this.mProgressValue = value;
				this.OnPropertyChanged<double>(() => this.ProgressValue);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004A0F File Offset: 0x00002C0F
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004A17 File Offset: 0x00002C17
		public string UpdateLogs
		{
			get
			{
				return this.mUpdateLogs;
			}
			set
			{
				this.mUpdateLogs = value;
				this.OnPropertyChanged<string>(() => this.UpdateLogs);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004A55 File Offset: 0x00002C55
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004A5D File Offset: 0x00002C5D
		public string StatusDescription
		{
			get
			{
				return this.mStatusDescription;
			}
			set
			{
				this.mStatusDescription = value;
				this.OnPropertyChanged<string>(() => this.StatusDescription);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000B4 RID: 180 RVA: 0x00004A9C File Offset: 0x00002C9C
		// (remove) Token: 0x060000B5 RID: 181 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000B6 RID: 182 RVA: 0x00004B09 File Offset: 0x00002D09
		public virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B28 File Offset: 0x00002D28
		public virtual void OnPropertyChanged<T>(Expression<Func<T>> property)
		{
			if (this.PropertyChanged != null)
			{
				MemberExpression memberExpression = property.Body as MemberExpression;
				if (memberExpression != null && memberExpression.Member != null && !string.IsNullOrEmpty(memberExpression.Member.Name))
				{
					this.PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
				}
			}
		}

		// Token: 0x0400003E RID: 62
		private string mUpdateCenter = "https://api.mypep.com.cn/api/";

		// Token: 0x0400003F RID: 63
		private string mRemoteRootPath = "http://192.168.186.20:80/pub_cloud/1300/pc_dtp/";

		// Token: 0x04000040 RID: 64
		private string mMainProcessPath = string.Empty;

		// Token: 0x04000041 RID: 65
		private string mLocalRootPath = string.Empty;

		// Token: 0x04000042 RID: 66
		private string mClientID = string.Empty;

		// Token: 0x04000043 RID: 67
		private string mTotalSize;

		// Token: 0x04000044 RID: 68
		private double mProgressValue;

		// Token: 0x04000045 RID: 69
		private string mUpdateLogs = string.Empty;

		// Token: 0x04000046 RID: 70
		private string mStatusDescription = "正在更新，已完成";
	}
}
