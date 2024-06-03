using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.Controls;
using JXP.Logs;
using JXP.PepUtility;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x0200003C RID: 60
	public partial class LockScreen : UserControl
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000132F1 File Offset: 0x000114F1
		public LockScreen()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00013300 File Offset: 0x00011500
		public void LoadingStart()
		{
			try
			{
				System.Drawing.Image image = System.Drawing.Image.FromFile(PepPathHelper.GetLoadingImagePath());
				this.gifLoading.AnimatedBitmap = (Bitmap)image;
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013350 File Offset: 0x00011550
		public void LoadingEnd()
		{
			try
			{
				this.gifLoading.StopAnimate();
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}
	}
}
