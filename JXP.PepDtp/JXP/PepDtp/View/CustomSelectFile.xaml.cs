using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Common;

namespace JXP.PepDtp.View
{
	// Token: 0x02000017 RID: 23
	public partial class CustomSelectFile : Window
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B052 File Offset: 0x00009252
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000B05A File Offset: 0x0000925A
		public string ImageFullName { get; set; }

		// Token: 0x0600017F RID: 383 RVA: 0x0000B063 File Offset: 0x00009263
		public CustomSelectFile()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B07C File Offset: 0x0000927C
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ImageFullName = this.mReduceImageOper.GetReducedImageFullName();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "压缩头像出错：";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			finally
			{
				base.Hide();
			}
		}

		// Token: 0x04000075 RID: 117
		private ReducedImage mReduceImageOper = new ReducedImage();
	}
}
