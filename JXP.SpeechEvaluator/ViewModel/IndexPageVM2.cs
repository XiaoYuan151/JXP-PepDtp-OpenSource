using System;
using System.Collections.ObjectModel;
using JXP.SpeechEvaluator.Model;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002B RID: 43
	public class IndexPageVM2 : IndexPageVM
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000073D4 File Offset: 0x000055D4
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000073F9 File Offset: 0x000055F9
		public ObservableCollection<BookItem> BookList
		{
			get
			{
				return this.mBooks = (this.mBooks ?? new ObservableCollection<BookItem>());
			}
			set
			{
				this.mBooks = value;
				base.RaisePropertyChanged<ObservableCollection<BookItem>>(() => this.BookList);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007437 File Offset: 0x00005637
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000743F File Offset: 0x0000563F
		public string Title
		{
			get
			{
				return this.mTitle;
			}
			set
			{
				this.mTitle = value;
				base.RaisePropertyChanged<string>(() => this.Title);
			}
		}

		// Token: 0x040000A3 RID: 163
		private string mTitle;

		// Token: 0x040000A4 RID: 164
		private ObservableCollection<BookItem> mBooks;
	}
}
