using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000007 RID: 7
	public class UpdateModel
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002B62 File Offset: 0x00000D62
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002B6A File Offset: 0x00000D6A
		[JsonProperty("version")]
		public string Version { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002B73 File Offset: 0x00000D73
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002B7B File Offset: 0x00000D7B
		[JsonProperty("product_id")]
		public string ProductId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002B84 File Offset: 0x00000D84
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002B8C File Offset: 0x00000D8C
		[JsonProperty("product_name")]
		public string ProductName { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002B95 File Offset: 0x00000D95
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002B9D File Offset: 0x00000D9D
		[JsonProperty("is_coerce")]
		public int IsCoerce { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002BA6 File Offset: 0x00000DA6
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002BAE File Offset: 0x00000DAE
		[JsonProperty("package_url")]
		public string DownloadPath { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002BB7 File Offset: 0x00000DB7
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002BBF File Offset: 0x00000DBF
		[JsonProperty("md5")]
		public string Comment { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BC8 File Offset: 0x00000DC8
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002BD0 File Offset: 0x00000DD0
		[JsonProperty("description")]
		public string Description { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002BD9 File Offset: 0x00000DD9
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002BE1 File Offset: 0x00000DE1
		[JsonProperty("s_create_time")]
		public string CreateTime { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BEA File Offset: 0x00000DEA
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002BF2 File Offset: 0x00000DF2
		[JsonProperty("is_current")]
		public int State { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BFB File Offset: 0x00000DFB
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002C03 File Offset: 0x00000E03
		[JsonProperty("is_full")]
		public int IsFull { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C14 File Offset: 0x00000E14
		[JsonProperty("auto_update")]
		public bool AutoUpdate
		{
			get
			{
				return this.mAutoUpdate;
			}
			set
			{
				this.mAutoUpdate = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C1D File Offset: 0x00000E1D
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002C25 File Offset: 0x00000E25
		[JsonProperty("file_list_json")]
		public List<ReleaseFileInfo> ReleaseFileList { get; set; }

		// Token: 0x04000014 RID: 20
		private bool mAutoUpdate = true;
	}
}
