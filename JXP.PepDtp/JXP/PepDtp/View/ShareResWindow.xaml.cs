using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Model;
using JXP.PepDtp.Paramter;
using JXP.PepDtp.ViewModel;
using JXP.Windows;
using JXP.Windows.View;

namespace JXP.PepDtp.View
{
	// Token: 0x0200002F RID: 47
	public partial class ShareResWindow : Window, IStyleConnector
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600026F RID: 623 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		// (remove) Token: 0x06000270 RID: 624 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public event ShareResWindow.ShareResourceHandle ShareResourceEvent;

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000EC3D File Offset: 0x0000CE3D
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000EC45 File Offset: 0x0000CE45
		public UserResourceModel UserRes { get; set; }

		// Token: 0x06000273 RID: 627 RVA: 0x0000EC4E File Offset: 0x0000CE4E
		public ShareResWindow()
		{
			this.InitializeComponent();
			base.Closed += this.ShareResWindow_Closed;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AEEF File Offset: 0x000090EF
		private void ShareResWindow_Closed(object sender, EventArgs e)
		{
			MaskLayerWindow.SingleInstance().CloseMaskWindow();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000EC88 File Offset: 0x0000CE88
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			if (this.UserRes == null)
			{
				base.Close();
				return;
			}
			if (!this.mShareResVM.AllStationChecked && !this.mShareResVM.SchoolChecked && !this.mShareResVM.GroupChecked)
			{
				ToastWin.GetToaster(this.txtTargetTip, true).ShowInfo(new ToastInfo
				{
					IconType = new ToastMessageIconType?(ToastMessageIconType.Warn),
					Content = "请选择分享范围"
				});
				return;
			}
			if (this.mShareResVM.GroupChecked && !this.mShareResVM.AllStationChecked && !this.mShareResVM.SchoolChecked)
			{
				IEnumerable<ShareResGroupModel> enumerable = from o in this.mShareResVM.ShareResGroupList
				where o.IsChecked
				select o;
				if (enumerable == null || enumerable.Count<ShareResGroupModel>() == 0)
				{
					ToastWin.GetToaster(this.txtTargetTip, true).ShowInfo(new ToastInfo
					{
						IconType = new ToastMessageIconType?(ToastMessageIconType.Warn),
						Content = "对不起您还没有加入任何群组"
					});
					return;
				}
			}
			if (this.mWorkerShare.IsBusy)
			{
				return;
			}
			this.gridWaiting.Visibility = Visibility.Visible;
			this.mWorkerShare.RunWorkerAsync();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000EDB1 File Offset: 0x0000CFB1
		private void MWorkerShare_DoWork(object sender, DoWorkEventArgs e)
		{
			this.mShareResVM.DoWorkAgrs = e;
			this.mShareResVM.SubmitShareResData(CommonParamter.Instance.LoginUserId, this.UserRes);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		private void MWorkerShare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.gridWaiting.Visibility = Visibility.Hidden;
				if (e.Error != null)
				{
					LogHelper.Instance.Error(e.Error.ToString());
					ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
					{
						IconType = new ToastMessageIconType?(ToastMessageIconType.Warn),
						Content = "分享失败"
					});
				}
				else if (e.Result != null)
				{
					if (e.Result.ToString() == "Error")
					{
						ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
						{
							IconType = new ToastMessageIconType?(ToastMessageIconType.Warn),
							Content = "分享失败"
						});
					}
					else
					{
						ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
						{
							IconType = new ToastMessageIconType?(ToastMessageIconType.Warn),
							Content = e.Result.ToString()
						});
					}
				}
				else
				{
					ShareResWindow.ShareResourceHandle shareResourceEvent = this.ShareResourceEvent;
					if (shareResourceEvent != null)
					{
						shareResourceEvent(string.Empty);
					}
					ToastWin.GetToaster(true, 300.0, 40.0).ShowInfo(new ToastInfo
					{
						IconType = new ToastMessageIconType?(ToastMessageIconType.OK),
						Content = "分享成功"
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EF90 File Offset: 0x0000D190
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.mShareResVM.ResetData();
			base.Close();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ShareResWindow.<Window_Loaded>d__17 <Window_Loaded>d__;
			<Window_Loaded>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Window_Loaded>d__.<>4__this = this;
			<Window_Loaded>d__.<>1__state = -1;
			<Window_Loaded>d__.<>t__builder.Start<ShareResWindow.<Window_Loaded>d__17>(ref <Window_Loaded>d__);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00004541 File Offset: 0x00002741
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		private void ckbItemGroup_Click(object sender, RoutedEventArgs e)
		{
			bool groupChecked = false;
			using (IEnumerator<ShareResGroupModel> enumerator = this.mShareResVM.ShareResGroupList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsChecked)
					{
						groupChecked = true;
						break;
					}
				}
			}
			this.mShareResVM.GroupChecked = groupChecked;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void ckbSchool_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F040 File Offset: 0x0000D240
		private void ckbGroups_Click(object sender, RoutedEventArgs e)
		{
			foreach (ShareResGroupModel shareResGroupModel in this.mShareResVM.ShareResGroupList)
			{
				shareResGroupModel.IsChecked = this.mShareResVM.GroupChecked;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F09C File Offset: 0x0000D29C
		private void ckbAllStation_Click(object sender, RoutedEventArgs e)
		{
			foreach (ShareResGroupModel shareResGroupModel in this.mShareResVM.ShareResGroupList)
			{
				shareResGroupModel.IsChecked = false;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F297 File Offset: 0x0000D497
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 8)
			{
				((CheckBox)target).Click += this.ckbItemGroup_Click;
			}
		}

		// Token: 0x04000113 RID: 275
		private ShareResViewModel mShareResVM = new ShareResViewModel();

		// Token: 0x04000114 RID: 276
		private BackgroundWorker mWorkerShare;

		// Token: 0x04000115 RID: 277
		private ManualResetEvent mReset = new ManualResetEvent(true);

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x06000A86 RID: 2694
		public delegate void ShareResourceHandle(string sharedRst);
	}
}
