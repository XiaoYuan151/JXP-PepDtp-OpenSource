using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using JXP.Controls.Buttons;

namespace JXP.PepDtp.View.TextBookReader
{
	// Token: 0x02000050 RID: 80
	public partial class TextBookToolBar : UserControl, INotifyPropertyChanged
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001DA60 File Offset: 0x0001BC60
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001DA68 File Offset: 0x0001BC68
		public int ZoomValue
		{
			get
			{
				return this.mZoomValue;
			}
			set
			{
				this.mZoomValue = value;
				this.OnPropertyRaised("ZoomValue");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001DA84 File Offset: 0x0001BC84
		public int ZoomMinimum
		{
			get
			{
				return this.mZoomMinimum;
			}
			set
			{
				this.mZoomMinimum = value;
				this.OnPropertyRaised("ZoomMinimum");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001DA98 File Offset: 0x0001BC98
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		public int ZoomMaximum
		{
			get
			{
				return this.mZoomMaximum;
			}
			set
			{
				this.mZoomMaximum = value;
				this.OnPropertyRaised("ZoomMaximum");
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000548 RID: 1352 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
		// (remove) Token: 0x06000549 RID: 1353 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600054A RID: 1354 RVA: 0x0001DB21 File Offset: 0x0001BD21
		private void OnPropertyRaised(string propertyname)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyname));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001DB3A File Offset: 0x0001BD3A
		public TextBookToolBar()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600054C RID: 1356 RVA: 0x0001DB48 File Offset: 0x0001BD48
		// (remove) Token: 0x0600054D RID: 1357 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public event EventHandler OnShowChapterList;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600054E RID: 1358 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		// (remove) Token: 0x0600054F RID: 1359 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		public event EventHandler OnZoomIn;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000550 RID: 1360 RVA: 0x0001DC28 File Offset: 0x0001BE28
		// (remove) Token: 0x06000551 RID: 1361 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public event EventHandler OnZoomInPop;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000552 RID: 1362 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (remove) Token: 0x06000553 RID: 1363 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public event EventHandler OnZoomOut;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000554 RID: 1364 RVA: 0x0001DD08 File Offset: 0x0001BF08
		// (remove) Token: 0x06000555 RID: 1365 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public event EventHandler OnZoomRevert;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000556 RID: 1366 RVA: 0x0001DD78 File Offset: 0x0001BF78
		// (remove) Token: 0x06000557 RID: 1367 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public event EventHandler OnCreateRes;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000558 RID: 1368 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		// (remove) Token: 0x06000559 RID: 1369 RVA: 0x0001DE20 File Offset: 0x0001C020
		public event EventHandler OnChangePageLayout;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600055A RID: 1370 RVA: 0x0001DE58 File Offset: 0x0001C058
		// (remove) Token: 0x0600055B RID: 1371 RVA: 0x0001DE90 File Offset: 0x0001C090
		public event EventHandler OnShowSyncList;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600055C RID: 1372 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		// (remove) Token: 0x0600055D RID: 1373 RVA: 0x0001DF00 File Offset: 0x0001C100
		public event EventHandler OnSync;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600055E RID: 1374 RVA: 0x0001DF38 File Offset: 0x0001C138
		// (remove) Token: 0x0600055F RID: 1375 RVA: 0x0001DF70 File Offset: 0x0001C170
		public event EventHandler OnShowOrHideAnnots;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000560 RID: 1376 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
		// (remove) Token: 0x06000561 RID: 1377 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		public event EventHandler OnShowOrHideHotZone;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000562 RID: 1378 RVA: 0x0001E018 File Offset: 0x0001C218
		// (remove) Token: 0x06000563 RID: 1379 RVA: 0x0001E050 File Offset: 0x0001C250
		public event EventHandler OnInputUserData;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000564 RID: 1380 RVA: 0x0001E088 File Offset: 0x0001C288
		// (remove) Token: 0x06000565 RID: 1381 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		public event EventHandler OnOutputUserData;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000566 RID: 1382 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
		// (remove) Token: 0x06000567 RID: 1383 RVA: 0x0001E130 File Offset: 0x0001C330
		public event EventHandler OnBookShare;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000568 RID: 1384 RVA: 0x0001E168 File Offset: 0x0001C368
		// (remove) Token: 0x06000569 RID: 1385 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public event EventHandler OnOpShowMyCenter;

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		private void imgBtnSwitch_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button == null)
			{
				return;
			}
			object tag = button.Tag;
			string a = (tag != null) ? tag.ToString() : null;
			if (button.Tag == null || a == "0")
			{
				button.Tag = "1";
				this.switchPopup.IsOpen = true;
				return;
			}
			button.Tag = "0";
			this.switchPopup.IsOpen = false;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001E247 File Offset: 0x0001C447
		private void switchPopup_Closed(object sender, EventArgs e)
		{
			this.imgBtnSwitch.Tag = "0";
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001E259 File Offset: 0x0001C459
		private void imgBtnHotSwitch_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onShowOrHideHotZone = this.OnShowOrHideHotZone;
			if (onShowOrHideHotZone == null)
			{
				return;
			}
			onShowOrHideHotZone(sender, e);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001E26D File Offset: 0x0001C46D
		private void imgBtnResSwitch_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onShowOrHideAnnots = this.OnShowOrHideAnnots;
			if (onShowOrHideAnnots == null)
			{
				return;
			}
			onShowOrHideAnnots(sender, e);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001E281 File Offset: 0x0001C481
		private void imgBtnSync_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onSync = this.OnSync;
			if (onSync == null)
			{
				return;
			}
			onSync(sender, e);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001E295 File Offset: 0x0001C495
		private void imgBtnInput_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onInputUserData = this.OnInputUserData;
			if (onInputUserData == null)
			{
				return;
			}
			onInputUserData(sender, e);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001E2A9 File Offset: 0x0001C4A9
		private void imgBtnOutput_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onOutputUserData = this.OnOutputUserData;
			if (onOutputUserData == null)
			{
				return;
			}
			onOutputUserData(sender, e);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001E2BD File Offset: 0x0001C4BD
		private void imgBtnShare_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onBookShare = this.OnBookShare;
			if (onBookShare == null)
			{
				return;
			}
			onBookShare(sender, e);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		private void imgBtnSyncList_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onShowSyncList = this.OnShowSyncList;
			if (onShowSyncList == null)
			{
				return;
			}
			onShowSyncList(sender, e);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001E2E5 File Offset: 0x0001C4E5
		private void imgBtnNewResource_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onCreateRes = this.OnCreateRes;
			if (onCreateRes == null)
			{
				return;
			}
			onCreateRes(sender, e);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		public void SetZommValue(int nInitZoom, int minimum, int maximum)
		{
			try
			{
				this.zoomSlider.ValueChanged -= this.zoomSlider_ValueChanged;
				if (this.ZoomMinimum != minimum)
				{
					this.ZoomMinimum = minimum;
				}
				if (this.ZoomMaximum != maximum)
				{
					this.ZoomMaximum = maximum;
				}
				this.ZoomValue = nInitZoom;
			}
			finally
			{
				this.zoomSlider.ValueChanged += this.zoomSlider_ValueChanged;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001E374 File Offset: 0x0001C574
		private void imgBtnZoominPop_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onZoomInPop = this.OnZoomInPop;
			if (onZoomInPop != null)
			{
				onZoomInPop(sender, e);
			}
			this.bookZoomPop.IsOpen = true;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001E395 File Offset: 0x0001C595
		private void imgBtnZoomin_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onZoomIn = this.OnZoomIn;
			if (onZoomIn == null)
			{
				return;
			}
			onZoomIn(sender, e);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001E3A9 File Offset: 0x0001C5A9
		private void imgBtnZoomout_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onZoomOut = this.OnZoomOut;
			if (onZoomOut == null)
			{
				return;
			}
			onZoomOut(sender, e);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001E3BD File Offset: 0x0001C5BD
		private void imgBtnRervert_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onZoomRevert = this.OnZoomRevert;
			if (onZoomRevert == null)
			{
				return;
			}
			onZoomRevert(sender, e);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001E3D1 File Offset: 0x0001C5D1
		private void imgBtnZoomClose_Click(object sender, RoutedEventArgs e)
		{
			this.bookZoomPop.IsOpen = false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001E3DF File Offset: 0x0001C5DF
		private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Action<int> setZoomLevel = this.SetZoomLevel;
			if (setZoomLevel == null)
			{
				return;
			}
			setZoomLevel((int)e.NewValue);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		private void imgBtnDoublePage_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onChangePageLayout = this.OnChangePageLayout;
			if (onChangePageLayout == null)
			{
				return;
			}
			onChangePageLayout(sender, e);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001E40C File Offset: 0x0001C60C
		private void imgBtnMyCenter_Click(object sender, RoutedEventArgs e)
		{
			EventHandler onOpShowMyCenter = this.OnOpShowMyCenter;
			if (onOpShowMyCenter == null)
			{
				return;
			}
			onOpShowMyCenter(sender, e);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001E420 File Offset: 0x0001C620
		private void imgBtnBookMenu_Click(object sender, RoutedEventArgs e)
		{
			this.OnShowChapterList(sender, e);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void SetPageLayoutBtnText(bool doubleSheet)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001E42F File Offset: 0x0001C62F
		public void SetResSwitchBtnText(bool resSwitch)
		{
			this.cbRes.IsChecked = new bool?(resSwitch);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001E442 File Offset: 0x0001C642
		public void SetHotZoneBtnColor(bool hotZoneFlg)
		{
			this.cbHotZone.IsChecked = new bool?(hotZoneFlg);
		}

		// Token: 0x040002C8 RID: 712
		private int mZoomValue;

		// Token: 0x040002C9 RID: 713
		private int mZoomMinimum;

		// Token: 0x040002CA RID: 714
		private int mZoomMaximum;

		// Token: 0x040002D1 RID: 721
		public Action<int> SetZoomLevel;
	}
}
