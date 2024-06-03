using System;
using JXP.SpeechEvaluator.Model.Http;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.Model
{
	// Token: 0x0200004B RID: 75
	public class BookItem : NotificationObject
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000A916 File Offset: 0x00008B16
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000A91E File Offset: 0x00008B1E
		public BookData BookData { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A927 File Offset: 0x00008B27
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000A92F File Offset: 0x00008B2F
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A96D File Offset: 0x00008B6D
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000A975 File Offset: 0x00008B75
		public string BookName
		{
			get
			{
				return this.mBookName;
			}
			set
			{
				this.mBookName = value;
				base.RaisePropertyChanged<string>(() => this.BookName);
			}
		}

		// Token: 0x040001B8 RID: 440
		private bool mIsSelected;

		// Token: 0x040001B9 RID: 441
		private string mBookName;
	}
}
