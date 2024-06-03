using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Controls;
using JXP.PepDtp.View.CustomControl;

namespace JXP.PepDtp.View.InnerReaderControl
{
	// Token: 0x02000053 RID: 83
	public class KeyworldSearchUserControl : UserControl, IComponentConnector
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0001EF8E File Offset: 0x0001D18E
		public KeyworldSearchUserControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001EF9C File Offset: 0x0001D19C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/JXP.PepDtp;component/view/innerreadercontrol/keywordsearchusercontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000082C4 File Offset: 0x000064C4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.txtKeyWord = (WatermarkTextBoxWithErrorInfo)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.btnSearch = (ImageButton)target;
		}

		// Token: 0x040002F7 RID: 759
		internal WatermarkTextBoxWithErrorInfo txtKeyWord;

		// Token: 0x040002F8 RID: 760
		internal ImageButton btnSearch;

		// Token: 0x040002F9 RID: 761
		private bool _contentLoaded;
	}
}
