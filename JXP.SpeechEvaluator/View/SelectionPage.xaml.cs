using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Http;
using JXP.SpeechEvaluator.Utility;
using JXP.SpeechEvaluator.View.Navigation;
using JXP.SpeechEvaluator.View.Navigation.NaviParas;
using JXP.SpeechEvaluator.ViewModel;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x0200000A RID: 10
	public partial class SelectionPage : UserControl, INavigationPage
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003059 File Offset: 0x00001259
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003061 File Offset: 0x00001261
		private SelectionPageVM mPageVm { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x0000306A File Offset: 0x0000126A
		public SelectionPage()
		{
			this.InitializeComponent();
			this.mPageVm = new SelectionPageVM();
			base.DataContext = this.mPageVm;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003090 File Offset: 0x00001290
		public void Init(NavigationParas paras)
		{
			SelectionPage.<Init>d__8 <Init>d__;
			<Init>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Init>d__.<>4__this = this;
			<Init>d__.paras = paras;
			<Init>d__.<>1__state = -1;
			<Init>d__.<>t__builder.Start<SelectionPage.<Init>d__8>(ref <Init>d__);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000030D0 File Offset: 0x000012D0
		private void InitUiState()
		{
			this.mPageVm.ParagraphsVisible = false;
			this.mPageVm.RolesVisible = false;
			this.mPageVm.ToolBar1Visible = false;
			this.mPageVm.ToolBar2Visible = false;
			switch (this.mNaviParas.SelPageType)
			{
			case SelectionPageType.DuiHua:
				this.mPageVm.RolesVisible = true;
				this.mPageVm.ToolBar2Visible = true;
				return;
			case SelectionPageType.BeiSong:
				this.mPageVm.ParagraphsVisible = true;
				this.mPageVm.ToolBar1Visible = true;
				return;
			case SelectionPageType.ZiDu:
				this.mPageVm.ParagraphsVisible = true;
				this.mPageVm.ToolBar2Visible = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003178 File Offset: 0x00001378
		private void InitPagingButtonState()
		{
			this.mPageVm.LeftPageVisible = false;
			this.mPageVm.RightPageVisible = false;
			if (this.mPageVm.Cards.GetTotalPage() <= 1)
			{
				return;
			}
			if (this.mPageVm.Cards.CurrentPage == 0)
			{
				this.mPageVm.RightPageVisible = true;
				return;
			}
			if (this.mPageVm.Cards.CurrentPage == this.mPageVm.Cards.GetTotalPage() - 1)
			{
				this.mPageVm.LeftPageVisible = true;
				return;
			}
			this.mPageVm.LeftPageVisible = true;
			this.mPageVm.RightPageVisible = true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000321C File Offset: 0x0000141C
		private void InitData(IndexJson indexData)
		{
			List<SelectionPageItemVM> list = new List<SelectionPageItemVM>();
			List<SelectionPageItemVM> list2 = new List<SelectionPageItemVM>();
			foreach (Paragraph paragraph in indexData.Paragraphs)
			{
				foreach (Sentence sentence in paragraph.Sentences)
				{
					if (!string.IsNullOrEmpty(sentence.Audio))
					{
						sentence.Audio = this.mBaseUrl + sentence.Audio;
					}
					if (!string.IsNullOrEmpty(sentence.Image))
					{
						sentence.Image = this.mBaseUrl + sentence.Image;
					}
				}
			}
			if (this.mNaviParas.SelPageType == SelectionPageType.DuiHua)
			{
				foreach (Role role in indexData.Roles)
				{
					list.Add(new SelectionPageItemVM
					{
						ImgName = role.Image,
						RoleImg = this.mBaseUrl + "a" + role.Image,
						RoleName = role.Name
					});
				}
				this.mPageVm.Cards = new PaginatedObservableCollection<SelectionPageItemVM>(list, 3);
			}
			else
			{
				List<string> list3 = new List<string>();
				foreach (Paragraph paragraph2 in indexData.Paragraphs)
				{
					foreach (Sentence sentence2 in paragraph2.Sentences)
					{
						if (string.IsNullOrWhiteSpace(sentence2.Role))
						{
							list3.Add(sentence2.Content ?? "");
						}
						else
						{
							list3.Add(sentence2.Role + ":" + sentence2.Content);
						}
						if (list3.Count >= 6)
						{
							break;
						}
					}
				}
				list2.Add(new SelectionPageItemVM
				{
					Caption = "Passage",
					Desc = string.Join(Environment.NewLine, list3),
					Index = -1,
					MaxLines = 6
				});
				if (indexData.Paragraphs.Count > 1)
				{
					foreach (Paragraph paragraph3 in indexData.Paragraphs)
					{
						list3 = new List<string>();
						foreach (Sentence sentence3 in paragraph3.Sentences)
						{
							if (string.IsNullOrWhiteSpace(sentence3.Role))
							{
								list3.Add(sentence3.Content ?? "");
							}
							else
							{
								list3.Add(sentence3.Role + ":" + sentence3.Content);
							}
							if (list3.Count >= 3)
							{
								break;
							}
						}
						list2.Add(new SelectionPageItemVM
						{
							Caption = string.Format("Para. {0}", paragraph3.Index),
							Desc = string.Join(Environment.NewLine, list3),
							Index = paragraph3.Index
						});
					}
				}
				this.mPageVm.Cards = new PaginatedObservableCollection<SelectionPageItemVM>(list2, 3);
			}
			if (this.mPageVm.Cards.Count > 0)
			{
				foreach (SelectionPageItemVM selectionPageItemVM in this.mPageVm.Cards)
				{
					selectionPageItemVM.IsSelected = false;
				}
				this.mPageVm.Cards[0].IsSelected = true;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003638 File Offset: 0x00001838
		private void NavigationBar_Return(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.NavigateTo(PageType.Index, null, NavigationDirection.Backward);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003647 File Offset: 0x00001847
		private void NavigationBar_Close(object sender, RoutedEventArgs e)
		{
			PageNavigation.Instance.Close(PageType.Selection);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003654 File Offset: 0x00001854
		private void EvaluatorButton_Click(object sender, RoutedEventArgs e)
		{
			object tag = (sender as Button).Tag;
			int num = int.Parse((tag != null) ? tag.ToString() : null);
			switch (this.mNaviParas.SelPageType)
			{
			case SelectionPageType.DuiHua:
			{
				if (this.mPageVm.Cards.Count == 0)
				{
					return;
				}
				SelectionPageItemVM selectionPageItemVM = this.mPageVm.Cards.FirstOrDefault((SelectionPageItemVM role) => role.IsSelected);
				if (selectionPageItemVM == null)
				{
					return;
				}
				NavigationParas paras = new DuiHuaPageParas
				{
					SelectedRole = selectionPageItemVM.RoleName,
					SelectedRoleImg = selectionPageItemVM.RoleImg,
					Sentences = this.GetRoleSentences(selectionPageItemVM.RoleName)
				};
				PageNavigation.Instance.NavigateTo(PageType.DuiHua, paras, NavigationDirection.Forward);
				return;
			}
			case SelectionPageType.BeiSong:
			{
				NavigationParas paras = new BeiSongPageParas
				{
					Sentences = this.GetParagraphSentences()
				};
				if (num == 0)
				{
					PageNavigation.Instance.NavigateTo(PageType.BeiSongTips, paras, NavigationDirection.Forward);
					return;
				}
				if (num != 1)
				{
					return;
				}
				PageNavigation.Instance.NavigateTo(PageType.BeiSongNoTips, paras, NavigationDirection.Forward);
				return;
			}
			case SelectionPageType.ZiDu:
			{
				NavigationParas paras = new ZiDuPageParas
				{
					Sentences = this.GetParagraphSentences()
				};
				PageNavigation.Instance.NavigateTo(PageType.ZiDu, paras, NavigationDirection.Forward);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000377C File Offset: 0x0000197C
		private List<Sentence> GetParagraphSentences()
		{
			List<Sentence> list = new List<Sentence>();
			if (this.mPageVm.Cards.Count == 0)
			{
				return list;
			}
			SelectionPageItemVM p = this.mPageVm.Cards.FirstOrDefault((SelectionPageItemVM item) => item.IsSelected);
			if (p == null)
			{
				return list;
			}
			Paragraph paragraph = this.mIndexModel.Paragraphs.FirstOrDefault((Paragraph item) => item.Index == p.Index);
			if (paragraph == null)
			{
				using (IEnumerator<Paragraph> enumerator = this.mIndexModel.Paragraphs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Paragraph paragraph2 = enumerator.Current;
						foreach (Sentence sentence in paragraph2.Sentences)
						{
							list.Add(sentence.Clone());
						}
					}
					return list;
				}
			}
			foreach (Sentence sentence2 in paragraph.Sentences)
			{
				list.Add(sentence2.Clone());
			}
			return list;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038D8 File Offset: 0x00001AD8
		private List<Sentence> GetRoleSentences(string selectedRole)
		{
			List<Sentence> list = new List<Sentence>();
			foreach (Paragraph paragraph in this.mIndexModel.Paragraphs)
			{
				foreach (Sentence sentence in paragraph.Sentences)
				{
					list.Add(sentence.Clone());
				}
			}
			foreach (Role role in this.mIndexModel.Roles)
			{
				foreach (Sentence sentence2 in list)
				{
					if (string.IsNullOrEmpty(sentence2.Role))
					{
						sentence2.RoleAlignment = RoleAlignment.Center;
					}
					else
					{
						string[] array = sentence2.Role.Split(new char[]
						{
							'/',
							'\\'
						});
						int i = 0;
						while (i < array.Length)
						{
							string strA = array[i];
							if (string.Compare(strA, role.Name, true) == 0)
							{
								if (string.Compare(strA, selectedRole, true) == 0)
								{
									sentence2.RoleAlignment = RoleAlignment.Right;
									sentence2.RoleImg = this.mBaseUrl + "b1" + role.Image;
									break;
								}
								sentence2.RoleImg = this.mBaseUrl + "b2" + role.Image;
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003AD4 File Offset: 0x00001CD4
		private void Paging_Click(object sender, RoutedEventArgs e)
		{
			object tag = (sender as Button).Tag;
			string a = (tag != null) ? tag.ToString() : null;
			if (!(a == "L"))
			{
				if (a == "R")
				{
					this.mPageVm.Cards.CurrentPage++;
				}
			}
			else
			{
				this.mPageVm.Cards.CurrentPage--;
			}
			this.InitPagingButtonState();
			if (this.mPageVm.Cards.Count > 0)
			{
				foreach (SelectionPageItemVM selectionPageItemVM in this.mPageVm.Cards)
				{
					selectionPageItemVM.IsSelected = false;
				}
				this.mPageVm.Cards[0].IsSelected = true;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BBC File Offset: 0x00001DBC
		public void BringFront()
		{
			base.Visibility = Visibility.Visible;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public void BringBack()
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BCE File Offset: 0x00001DCE
		public void Close(bool dispose = false)
		{
		}

		// Token: 0x04000026 RID: 38
		private string mBaseUrl;

		// Token: 0x04000027 RID: 39
		private IndexJson mIndexModel;

		// Token: 0x04000028 RID: 40
		private SelectionPageParas mNaviParas;
	}
}
