using System;
using JXP.Models;
using Newtonsoft.Json;

namespace JXP.PepDtp.View
{
	// Token: 0x02000024 RID: 36
	public class MessageInfoModel : BaseModel
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000D19B File Offset: 0x0000B39B
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		[JsonProperty("id")]
		public string Id
		{
			get
			{
				return this.mId;
			}
			set
			{
				this.mId = value;
				base.OnPropertyChanged<string>(() => this.Id);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000D1E1 File Offset: 0x0000B3E1
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000D1E9 File Offset: 0x0000B3E9
		[JsonProperty("name")]
		public string Name
		{
			get
			{
				return this.mName;
			}
			set
			{
				this.mName = value;
				base.OnPropertyChanged<string>(() => this.Name);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000D227 File Offset: 0x0000B427
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000D22F File Offset: 0x0000B42F
		[JsonProperty("content")]
		public string Content
		{
			get
			{
				return this.mContent;
			}
			set
			{
				this.mContent = value;
				base.OnPropertyChanged<string>(() => this.Content);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000D26D File Offset: 0x0000B46D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000D278 File Offset: 0x0000B478
		[JsonProperty("pub_time")]
		public string PubTime
		{
			get
			{
				return this.mPubTime;
			}
			set
			{
				if (value != null && value.Length >= 10)
				{
					this.mPubTime = value.Substring(0, 10);
				}
				else
				{
					this.mPubTime = value;
				}
				base.OnPropertyChanged<string>(() => this.PubTime);
			}
		}

		// Token: 0x040000CE RID: 206
		private string mId;

		// Token: 0x040000CF RID: 207
		private string mName;

		// Token: 0x040000D0 RID: 208
		private string mContent;

		// Token: 0x040000D1 RID: 209
		private string mPubTime;
	}
}
