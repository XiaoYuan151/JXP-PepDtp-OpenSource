using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000032 RID: 50
	public class PaginatedObservableCollection<T> : ObservableCollection<T>
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000080FE File Offset: 0x000062FE
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00008106 File Offset: 0x00006306
		public int PageSize
		{
			get
			{
				return this._itemCountPerPage;
			}
			set
			{
				if (value >= 0)
				{
					this._itemCountPerPage = value;
					this.RecalculateThePageItems();
					this.OnPropertyChanged(new PropertyChangedEventArgs("PageSize"));
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008129 File Offset: 0x00006329
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00008131 File Offset: 0x00006331
		public int CurrentPage
		{
			get
			{
				return this._currentPageIndex;
			}
			set
			{
				if (value >= 0)
				{
					this._currentPageIndex = value;
					this.RecalculateThePageItems();
					this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
				}
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008154 File Offset: 0x00006354
		public PaginatedObservableCollection(IEnumerable<T> collecton, int itemsPerPage)
		{
			this._currentPageIndex = 0;
			this._itemCountPerPage = itemsPerPage;
			this.originalCollection = new List<T>(collecton);
			this.RecalculateThePageItems();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000817C File Offset: 0x0000637C
		public PaginatedObservableCollection(int itemsPerPage)
		{
			this._currentPageIndex = 0;
			this._itemCountPerPage = itemsPerPage;
			this.originalCollection = new List<T>();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000819D File Offset: 0x0000639D
		public PaginatedObservableCollection()
		{
			this._currentPageIndex = 0;
			this._itemCountPerPage = 1;
			this.originalCollection = new List<T>();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000081BE File Offset: 0x000063BE
		public int GetTotalPage()
		{
			return (int)Math.Ceiling((double)this.originalCollection.Count * 1.0 / (double)this._itemCountPerPage);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000081E4 File Offset: 0x000063E4
		private void RecalculateThePageItems()
		{
			base.Clear();
			int num = this._currentPageIndex * this._itemCountPerPage;
			for (int i = num; i < num + this._itemCountPerPage; i++)
			{
				if (this.originalCollection.Count > i)
				{
					base.InsertItem(i - num, this.originalCollection[i]);
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000823C File Offset: 0x0000643C
		protected override void InsertItem(int index, T item)
		{
			int num = this._currentPageIndex * this._itemCountPerPage;
			int num2 = num + this._itemCountPerPage;
			if (index >= num && index < num2)
			{
				base.InsertItem(index - num, item);
				if (base.Count > this._itemCountPerPage)
				{
					base.RemoveItem(num2);
				}
			}
			if (index >= base.Count)
			{
				this.originalCollection.Add(item);
				return;
			}
			this.originalCollection.Insert(index, item);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000082AC File Offset: 0x000064AC
		protected override void RemoveItem(int index)
		{
			int num = this._currentPageIndex * this._itemCountPerPage;
			int num2 = num + this._itemCountPerPage;
			if (index >= num && index < num2)
			{
				base.RemoveAt(index - num);
				if (base.Count <= this._itemCountPerPage)
				{
					base.InsertItem(num2 - 1, this.originalCollection[index + 1]);
				}
			}
			this.originalCollection.RemoveAt(index);
		}

		// Token: 0x040000CE RID: 206
		private List<T> originalCollection;

		// Token: 0x040000CF RID: 207
		private int _currentPageIndex;

		// Token: 0x040000D0 RID: 208
		private int _itemCountPerPage;
	}
}
