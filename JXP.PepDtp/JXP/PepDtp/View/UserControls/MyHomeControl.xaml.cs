using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Models;
using JXP.PepDtp.ViewModel;
using pep.Course.Views;
using pep.sdk.reader.View.UserControls;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000041 RID: 65
	public partial class MyHomeControl : UserControl
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00013932 File Offset: 0x00011B32
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0001393A File Offset: 0x00011B3A
		public Action<int> NavigateToMenueCallback { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00013943 File Offset: 0x00011B43
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0001394B File Offset: 0x00011B4B
		public Action<CourseModel> EditeCourseCallback { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00013954 File Offset: 0x00011B54
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0001395C File Offset: 0x00011B5C
		public Action<CourseModel, BookChaptersInfo> GotoEditeCallback { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00013965 File Offset: 0x00011B65
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0001396D File Offset: 0x00011B6D
		public Action<CourseModel> StartClassCallback { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00013976 File Offset: 0x00011B76
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0001397E File Offset: 0x00011B7E
		public IsDownloadBook IsDownloadBookCallBack { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00013987 File Offset: 0x00011B87
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0001398F File Offset: 0x00011B8F
		public Func<string, bool> IsContainBookidCallback { get; set; }

		// Token: 0x0600037A RID: 890 RVA: 0x00013998 File Offset: 0x00011B98
		public MyHomeControl()
		{
			this.InitializeComponent();
			base.DataContext = this.mHomeVM;
			base.Loaded += this.MyHomeControl_Loaded;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000139D0 File Offset: 0x00011BD0
		private void MyHomeControl_Loaded(object sender, RoutedEventArgs e)
		{
			this.mHomeVM.NavigateToMenueCallback = this.NavigateToMenueCallback;
			this.mHomeVM.EditeCourseCallback = this.EditeCourseCallback;
			this.mHomeVM.GotoEditeCallback = this.GotoEditeCallback;
			this.mHomeVM.StartClassCallback = this.StartClassCallback;
			this.mHomeVM.IsDownloadBookCallBack = this.IsDownloadBookCallBack;
			this.mHomeVM.IsContainBookidCallback = this.IsContainBookidCallback;
			this.mHomeVM.mOwner = Window.GetWindow(this);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013A54 File Offset: 0x00011C54
		public void Init()
		{
			this.mHomeVM.InitMyHomeData();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013A61 File Offset: 0x00011C61
		public void ClearData()
		{
			this.mHomeVM.ClearData();
		}

		// Token: 0x040001C3 RID: 451
		private MyHomeViewModel mHomeVM = new MyHomeViewModel();

		// Token: 0x040001C4 RID: 452
		private Window mainWin;
	}
}
