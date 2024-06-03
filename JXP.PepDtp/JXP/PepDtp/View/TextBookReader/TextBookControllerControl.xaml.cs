using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JXP.PepDtp.View.TextBookReader
{
	// Token: 0x0200004E RID: 78
	public partial class TextBookControllerControl : UserControl
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public TextBookControllerControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001CB72 File Offset: 0x0001AD72
		public TextBookInnerOperator TextBookOperator
		{
			get
			{
				return this.ucBookOperator;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001CB7A File Offset: 0x0001AD7A
		public TextBookToolBar TextBookToolBar
		{
			get
			{
				return this.ucBookToolBar;
			}
		}
	}
}
