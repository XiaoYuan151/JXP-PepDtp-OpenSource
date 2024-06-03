using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace JXP.PepDtp.View
{
	// Token: 0x0200002D RID: 45
	public class MyNews : Window, IComponentConnector
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000EAA9 File Offset: 0x0000CCA9
		public MyNews()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00004541 File Offset: 0x00002741
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/JXP.PepDtp;component/view/mynews%20.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MyNews)target).MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				return;
			case 2:
				this.lblMsg = (TextBlock)target;
				return;
			case 3:
				this.btnCommint = (Button)target;
				this.btnCommint.Click += this.btnOk_Click;
				return;
			case 4:
				this.btnClose = (Button)target;
				this.btnClose.Click += this.btnCancel_Click;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400010E RID: 270
		internal TextBlock lblMsg;

		// Token: 0x0400010F RID: 271
		internal Button btnCommint;

		// Token: 0x04000110 RID: 272
		internal Button btnClose;

		// Token: 0x04000111 RID: 273
		private bool _contentLoaded;
	}
}
