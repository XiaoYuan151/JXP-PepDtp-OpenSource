using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace JXP.PepDtp.View
{
	// Token: 0x0200002C RID: 44
	public partial class ResMarkWindow : Window
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000E8C5 File Offset: 0x0000CAC5
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000E8CD File Offset: 0x0000CACD
		public string ResId { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000E8D6 File Offset: 0x0000CAD6
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000E8DE File Offset: 0x0000CADE
		public string PosType { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000E8E7 File Offset: 0x0000CAE7
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000E8EF File Offset: 0x0000CAEF
		public string ResType { get; set; } = "01";

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600025C RID: 604 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		// (remove) Token: 0x0600025D RID: 605 RVA: 0x0000E930 File Offset: 0x0000CB30
		public event ResMarkWindow.ResourceDragedHandle ResourceDraged;

		// Token: 0x0600025E RID: 606 RVA: 0x0000E965 File Offset: 0x0000CB65
		public ResMarkWindow()
		{
			this.InitializeComponent();
			this.InitData();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E984 File Offset: 0x0000CB84
		private void InitData()
		{
			base.WindowState = WindowState.Maximized;
			base.Background = new SolidColorBrush(Colors.White);
			base.Opacity = 0.1;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void Window_MouseLeave(object sender, MouseEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void Window_MouseMove(object sender, MouseEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		private void Window_Drop(object sender, DragEventArgs e)
		{
			this.mDropPoint = e.GetPosition((IInputElement)sender);
			ResMarkWindow.ResourceDragedHandle resourceDraged = this.ResourceDraged;
			if (resourceDraged != null)
			{
				resourceDraged(this.ResId, this.PosType, this.ResType, this.mDropPoint, e);
			}
			base.Close();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000108 RID: 264
		private Point mDropPoint;

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x06000A82 RID: 2690
		public delegate void ResourceDragedHandle(string resId, string posType, string resType, Point moveUpPoint, DragEventArgs e);
	}
}
