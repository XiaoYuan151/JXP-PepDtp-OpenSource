using System;
using Microsoft.Practices.Prism.ViewModel;
using Newtonsoft.Json;

namespace JXP.SpeechEvaluator.Model
{
	// Token: 0x0200004F RID: 79
	public class Role : NotificationObject
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000AC16 File Offset: 0x00008E16
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000AC1E File Offset: 0x00008E1E
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
				base.RaisePropertyChanged<string>(() => this.Name);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000AC5C File Offset: 0x00008E5C
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000AC64 File Offset: 0x00008E64
		[JsonProperty("image")]
		public string Image
		{
			get
			{
				return this.mImage;
			}
			set
			{
				this.mImage = value;
				base.RaisePropertyChanged<string>(() => this.Image);
			}
		}

		// Token: 0x040001CB RID: 459
		private string mName;

		// Token: 0x040001CC RID: 460
		private string mImage;
	}
}
