using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using JXP.DataAnalytics.Bootstrap;
using JXP.Logs;
using JXP.PepDtp.ViewModel;
using JXP.Utilities;

namespace JXP.PepDtp.View.InnerReaderControl
{
	// Token: 0x02000054 RID: 84
	public partial class ResScoreControl : UserControl
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
		public ResScoreControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001F033 File Offset: 0x0001D233
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.mScoreVM = new ScoreViewModel();
			base.DataContext = this.mScoreVM;
			base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate()
			{
				this.AddEventHandler();
			}));
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001F068 File Offset: 0x0001D268
		private void RegisterCheckBoxEvent()
		{
			this.chBox1.Checked += this.chBox_Checked;
			this.chBox1.Unchecked += this.chBox_Unchecked;
			this.chBox2.Checked += this.chBox_Checked;
			this.chBox2.Unchecked += this.chBox_Unchecked;
			this.chBox3.Checked += this.chBox_Checked;
			this.chBox3.Unchecked += this.chBox_Unchecked;
			this.chBox4.Checked += this.chBox_Checked;
			this.chBox4.Unchecked += this.chBox_Unchecked;
			this.chBox5.Checked += this.chBox_Checked;
			this.chBox5.Unchecked += this.chBox_Unchecked;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001F15C File Offset: 0x0001D35C
		private void DelCheckBoxEvent()
		{
			this.chBox1.Checked -= this.chBox_Checked;
			this.chBox1.Unchecked -= this.chBox_Unchecked;
			this.chBox2.Checked -= this.chBox_Checked;
			this.chBox2.Unchecked -= this.chBox_Unchecked;
			this.chBox3.Checked -= this.chBox_Checked;
			this.chBox3.Unchecked -= this.chBox_Unchecked;
			this.chBox4.Checked -= this.chBox_Checked;
			this.chBox4.Unchecked -= this.chBox_Unchecked;
			this.chBox5.Checked -= this.chBox_Checked;
			this.chBox5.Unchecked -= this.chBox_Unchecked;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001F250 File Offset: 0x0001D450
		private void chBox_MouseEnter(object sender, MouseEventArgs e)
		{
			try
			{
				CheckBox checkBox = sender as CheckBox;
				int num;
				if (checkBox != null && checkBox.Content != null && int.TryParse(checkBox.Content.ToString(), out num))
				{
					for (int i = num; i >= 1; i--)
					{
						CheckBox checkBox2 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + i.ToString());
						if (checkBox2 != null)
						{
							checkBox2.Style = (Style)base.FindResource("StarCheckBoxStyleChecked");
						}
					}
					for (int j = num + 1; j <= 5; j++)
					{
						CheckBox checkBox3 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + j.ToString());
						if (checkBox3 != null)
						{
							checkBox3.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001F33C File Offset: 0x0001D53C
		private void chBox_MouseLeave(object sender, MouseEventArgs e)
		{
			try
			{
				CheckBox checkBox = sender as CheckBox;
				int num;
				if (checkBox != null && checkBox.Content != null && int.TryParse(checkBox.Content.ToString(), out num))
				{
					for (int i = 1; i <= 5; i++)
					{
						CheckBox checkBox2 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + i.ToString());
						if (checkBox2 != null)
						{
							if (checkBox2.IsChecked.Value)
							{
								checkBox2.Style = (Style)base.FindResource("StarCheckBoxStyleChecked");
							}
							else
							{
								checkBox2.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001F408 File Offset: 0x0001D608
		private void chBox_Unchecked(object sender, RoutedEventArgs e)
		{
			try
			{
				this.DelCheckBoxEvent();
				CheckBox checkBox = sender as CheckBox;
				int num;
				if (checkBox != null && checkBox.Content != null && int.TryParse(checkBox.Content.ToString(), out num))
				{
					for (int i = num - 1; i >= 1; i--)
					{
						CheckBox checkBox2 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + i.ToString());
						if (checkBox2 != null)
						{
							checkBox2.IsChecked = new bool?(true);
							checkBox2.Style = (Style)base.FindResource("StarCheckBoxStyleChecked");
						}
					}
					for (int j = num; j <= 5; j++)
					{
						CheckBox checkBox3 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + j.ToString());
						if (checkBox3 != null)
						{
							if (checkBox3.Name == "chBox1")
							{
								checkBox3.IsChecked = new bool?(true);
								checkBox3.Style = (Style)base.FindResource("StarCheckBoxStyleChecked");
							}
							else
							{
								checkBox3.IsChecked = new bool?(false);
								checkBox3.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
			finally
			{
				this.RegisterCheckBoxEvent();
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001F580 File Offset: 0x0001D780
		private void chBox_Checked(object sender, RoutedEventArgs e)
		{
			try
			{
				this.DelCheckBoxEvent();
				CheckBox checkBox = sender as CheckBox;
				int nSelectScore;
				if (checkBox != null && checkBox.Content != null && int.TryParse(checkBox.Content.ToString(), out nSelectScore))
				{
					this.CheckedSorce(nSelectScore);
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
			finally
			{
				this.RegisterCheckBoxEvent();
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		private void CheckedSorce(int nSelectScore)
		{
			for (int i = nSelectScore; i >= 1; i--)
			{
				CheckBox checkBox = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + i.ToString());
				if (checkBox != null)
				{
					checkBox.IsChecked = new bool?(true);
					checkBox.Style = (Style)base.FindResource("StarCheckBoxStyleChecked");
				}
			}
			for (int j = nSelectScore + 1; j <= 5; j++)
			{
				CheckBox checkBox2 = VisualHelper.FindFirstVisualChild<CheckBox>(this.panelScore, "chBox" + j.ToString());
				if (checkBox2 != null)
				{
					checkBox2.IsChecked = new bool?(false);
					checkBox2.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public void InitScore(string userId, string userType, string targetId, string targetType, string strTitle, string strIcoPath)
		{
			ResScoreControl.<InitScore>d__15 <InitScore>d__;
			<InitScore>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<InitScore>d__.<>4__this = this;
			<InitScore>d__.userId = userId;
			<InitScore>d__.userType = userType;
			<InitScore>d__.targetId = targetId;
			<InitScore>d__.targetType = targetType;
			<InitScore>d__.strTitle = strTitle;
			<InitScore>d__.strIcoPath = strIcoPath;
			<InitScore>d__.<>1__state = -1;
			<InitScore>d__.<>t__builder.Start<ResScoreControl.<InitScore>d__15>(ref <InitScore>d__);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001F714 File Offset: 0x0001D914
		private void InitScoreStare()
		{
			this.chBox1.IsChecked = new bool?(false);
			this.chBox1.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
			this.chBox2.IsChecked = new bool?(false);
			this.chBox2.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
			this.chBox3.IsChecked = new bool?(false);
			this.chBox3.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
			this.chBox4.IsChecked = new bool?(false);
			this.chBox4.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
			this.chBox5.IsChecked = new bool?(false);
			this.chBox5.Style = (Style)base.FindResource("StarCheckBoxStyleUnChecked");
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001F800 File Offset: 0x0001DA00
		private Task<bool> AddScore(string userId, string userType, string targetId, string targetType)
		{
			ResScoreControl.<AddScore>d__17 <AddScore>d__;
			<AddScore>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<AddScore>d__.<>4__this = this;
			<AddScore>d__.userId = userId;
			<AddScore>d__.userType = userType;
			<AddScore>d__.targetId = targetId;
			<AddScore>d__.targetType = targetType;
			<AddScore>d__.<>1__state = -1;
			<AddScore>d__.<>t__builder.Start<ResScoreControl.<AddScore>d__17>(ref <AddScore>d__);
			return <AddScore>d__.<>t__builder.Task;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001F864 File Offset: 0x0001DA64
		private void SetScore(int nScore)
		{
			int num = nScore / 20;
			if (num >= 5)
			{
				this.chBox5.IsChecked = new bool?(true);
				return;
			}
			if (num >= 4)
			{
				this.chBox4.IsChecked = new bool?(true);
				return;
			}
			if (num >= 3)
			{
				this.chBox3.IsChecked = new bool?(true);
				return;
			}
			if (num >= 5)
			{
				this.chBox2.IsChecked = new bool?(true);
				return;
			}
			if (num >= 1)
			{
				this.chBox1.IsChecked = new bool?(true);
				return;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		private int GetScore()
		{
			if (this.chBox5.IsChecked.Value)
			{
				return 100;
			}
			if (this.chBox4.IsChecked.Value)
			{
				return 80;
			}
			if (this.chBox3.IsChecked.Value)
			{
				return 60;
			}
			if (this.chBox2.IsChecked.Value)
			{
				return 40;
			}
			bool value = this.chBox1.IsChecked.Value;
			return 20;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001F968 File Offset: 0x0001DB68
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			ResScoreControl.<btnOK_Click>d__20 <btnOK_Click>d__;
			<btnOK_Click>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<btnOK_Click>d__.<>4__this = this;
			<btnOK_Click>d__.<>1__state = -1;
			<btnOK_Click>d__.<>t__builder.Start<ResScoreControl.<btnOK_Click>d__20>(ref <btnOK_Click>d__);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001F99F File Offset: 0x0001DB9F
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Visibility = Visibility.Hidden;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		private void AddEventHandler()
		{
			Window window = Window.GetWindow(this);
			if (window != null)
			{
				window.MouseDown -= this.HostWin_MouseDown;
				window.MouseDown += this.HostWin_MouseDown;
			}
			this.dataArea.MouseDown -= this.DataArea_MouseDown;
			this.dataArea.MouseDown += this.DataArea_MouseDown;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001FA11 File Offset: 0x0001DC11
		private void DataArea_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001FA1A File Offset: 0x0001DC1A
		private void HostWin_MouseDown(object sender, MouseButtonEventArgs e)
		{
			base.Visibility = Visibility.Collapsed;
		}

		// Token: 0x040002FA RID: 762
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"InnerReaderControl",
			"ResScoreControl"
		});

		// Token: 0x040002FB RID: 763
		private ScoreViewModel mScoreVM;

		// Token: 0x040002FC RID: 764
		private string mUserID = string.Empty;

		// Token: 0x040002FD RID: 765
		private string mUserType = string.Empty;

		// Token: 0x040002FE RID: 766
		private string mTargetId = string.Empty;

		// Token: 0x040002FF RID: 767
		private string mTargetType = string.Empty;
	}
}
