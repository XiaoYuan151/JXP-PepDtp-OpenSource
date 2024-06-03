using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using JXP.Logs;
using JXP.PepDtp.Common;
using JXP.Threading;

namespace JXP.PepDtp.View
{
	// Token: 0x02000023 RID: 35
	public partial class MessageInfoWindow : Window
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		public ObservableCollection<MessageInfoModel> LstMessageInfo { get; set; } = new ObservableCollection<MessageInfoModel>();

		// Token: 0x060001FE RID: 510 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		public MessageInfoWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00004541 File Offset: 0x00002741
		private void TitleBor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.SaveReadMessage();
				base.Close();
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "关闭消息画面失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D03C File Offset: 0x0000B23C
		private void SaveReadMessage()
		{
			MessageInfoWindow.<>c__DisplayClass7_0 CS$<>8__locals1 = new MessageInfoWindow.<>c__DisplayClass7_0();
			MessageInfoWindow.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			string separator = ",";
			ObservableCollection<MessageInfoModel> lstMessageInfo = this.LstMessageInfo;
			IEnumerable<string> values;
			if (lstMessageInfo == null)
			{
				values = null;
			}
			else
			{
				values = from a in lstMessageInfo
				select a.Id;
			}
			CS$<>8__locals2.strId = string.Join(separator, values);
			if (string.IsNullOrEmpty(CS$<>8__locals1.strId))
			{
				return;
			}
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(ConfigHelper.WebServerUrl + "p/notice/read.do?ids=" + CS$<>8__locals1.strId, null, true, "");
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("标识已读消息失败:[{0}]", arg));
				}
			});
		}
	}
}
