using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using Newtonsoft.Json;

namespace JXP.SpeechEvaluator.Model.Http
{
	// Token: 0x0200005F RID: 95
	public class IndexJson : NotificationObject
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000B691 File Offset: 0x00009891
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000B699 File Offset: 0x00009899
		[JsonProperty("type")]
		public string Type
		{
			get
			{
				return this.mType;
			}
			set
			{
				this.mType = value;
				base.RaisePropertyChanged<string>(() => this.Type);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B6D7 File Offset: 0x000098D7
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000B6DF File Offset: 0x000098DF
		[JsonProperty("audio")]
		public string Audio
		{
			get
			{
				return this.mAudio;
			}
			set
			{
				this.mAudio = value;
				base.RaisePropertyChanged<string>(() => this.Audio);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B71D File Offset: 0x0000991D
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000B725 File Offset: 0x00009925
		[JsonProperty("roles")]
		public ObservableCollection<Role> Roles
		{
			get
			{
				return this.mRoles;
			}
			set
			{
				this.mRoles = value;
				base.RaisePropertyChanged<ObservableCollection<Role>>(() => this.Roles);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B763 File Offset: 0x00009963
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000B76B File Offset: 0x0000996B
		[JsonProperty("paragraphs")]
		public ObservableCollection<Paragraph> Paragraphs
		{
			get
			{
				return this.mParagraphs;
			}
			set
			{
				this.mParagraphs = value;
				base.RaisePropertyChanged<ObservableCollection<Paragraph>>(() => this.Paragraphs);
			}
		}

		// Token: 0x0400022A RID: 554
		private string mType;

		// Token: 0x0400022B RID: 555
		private string mAudio;

		// Token: 0x0400022C RID: 556
		private ObservableCollection<Role> mRoles = new ObservableCollection<Role>();

		// Token: 0x0400022D RID: 557
		private ObservableCollection<Paragraph> mParagraphs = new ObservableCollection<Paragraph>();
	}
}
