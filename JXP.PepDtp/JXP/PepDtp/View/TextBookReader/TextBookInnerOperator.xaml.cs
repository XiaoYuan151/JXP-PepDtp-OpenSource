using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using JXP.Controls;
using JXP.Controls.Enums;
using JXP.Controls.RadioButtons;
using JXP.Controls.UserControls;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Converters;
using JXP.PepDtp.Paramter;
using JXP.Utilities;
using pep.Nobook;
using pep.Nobook.Views;
using pep.Nobook.Windows;
using pep.sdk.reader.Enums;
using pep.sdk.reader.View.Textbook;

namespace JXP.PepDtp.View.TextBookReader
{
	// Token: 0x0200004F RID: 79
	public partial class TextBookInnerOperator : UserControl
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000519 RID: 1305 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		// (remove) Token: 0x0600051A RID: 1306 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		public event PainBrushEventHandler SelectPainBrushInfo;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600051B RID: 1307 RVA: 0x0001CC54 File Offset: 0x0001AE54
		// (remove) Token: 0x0600051C RID: 1308 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		public event OpenApplyTool ApplyToolClicked;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600051D RID: 1309 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		// (remove) Token: 0x0600051E RID: 1310 RVA: 0x0001CCFC File Offset: 0x0001AEFC
		public event JXP.Utilities.EventHandler<OperatorMenueType> MenueOperatorClick;

		// Token: 0x0600051F RID: 1311 RVA: 0x0001CD34 File Offset: 0x0001AF34
		public TextBookInnerOperator()
		{
			this.InitializeComponent();
			this.ucPaintBrushInfo.SelectPainBrushInfo += this.UcPaintBrushInfo_SelectPainBrushInfo;
			this.ucTool.ApplyToolClicked += this.UcTool_ApplyToolClicked;
			base.Loaded += this.TextBookInnerOperator_Loaded;
			MessageBus.Default.Unregister<object>(this, AppConsts.JXP_REGISTER_TOKEN);
			MessageBus.Default.Register<object>(this, AppConsts.JXP_REGISTER_TOKEN, new Action<object>(this.HandRegisterMsg), false);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001CDC9 File Offset: 0x0001AFC9
		private void TextBookInnerOperator_Loaded(object sender, RoutedEventArgs e)
		{
			base.Loaded -= this.TextBookInnerOperator_Loaded;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001CDDD File Offset: 0x0001AFDD
		private void HandRegisterMsg(object strMsg)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (CommonParamter.Instance.IsLogin && CommonParamter.Instance.IsTeacher)
				{
					Binding binding = new Binding();
					binding.Path = new PropertyPath("KillMe", new object[0]);
					binding.Source = NbParameters.Instance;
					binding.Mode = BindingMode.OneWay;
					binding.Converter = new BooleanToVisibilityConverter();
					this.coursewareRadioClass.SetBinding(UIElement.VisibilityProperty, binding);
					Binding binding2 = new Binding();
					binding2.ElementName = "coursewareRadioClass";
					binding2.Path = new PropertyPath("Visibility", new object[0]);
					binding2.Mode = BindingMode.OneWay;
					binding2.Converter = RevertVisibilityConverter.Instance;
					this.toCoursewareRadioClass.SetBinding(UIElement.VisibilityProperty, binding2);
					this.mborOptionHeight = 480.0;
					return;
				}
				this.radioClass.Visibility = Visibility.Collapsed;
				BindingOperations.ClearBinding(this.coursewareRadioClass, UIElement.VisibilityProperty);
				this.coursewareRadioClass.Visibility = Visibility.Collapsed;
				BindingOperations.ClearBinding(this.toCoursewareRadioClass, UIElement.VisibilityProperty);
				this.toCoursewareRadioClass.Visibility = Visibility.Collapsed;
				this.mborOptionHeight = 420.0;
			}), new object[0]);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001CDFD File Offset: 0x0001AFFD
		private void btnCollapse_Click(object sender, RoutedEventArgs e)
		{
			this.btnExpand.IsEnabled = false;
			this.btnCollapse.IsEnabled = false;
			this.mborOptionHeight = this.borOption.ActualHeight;
			this.scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			this.CollapseAnimation();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001CE3A File Offset: 0x0001B03A
		private void btnExpand_Click(object sender, RoutedEventArgs e)
		{
			this.btnExpand.IsEnabled = false;
			this.btnCollapse.IsEnabled = false;
			this.scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			this.ExpandAnimation();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001CE68 File Offset: 0x0001B068
		private void CollapseAnimation()
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation(this.borOption.ActualHeight, 0.0, TimeSpan.FromSeconds(0.3));
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Height", new object[0]));
			storyboard.Children.Add(doubleAnimation);
			storyboard.Completed += delegate(object sender, EventArgs e)
			{
				this.borOption.Visibility = Visibility.Collapsed;
				this.borExpand.Visibility = Visibility.Visible;
				this.borCollapse.Visibility = Visibility.Hidden;
				this.btnExpand.IsEnabled = true;
				this.btnCollapse.IsEnabled = true;
				this.scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			};
			storyboard.Begin(this.borOption);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private void ExpandAnimation()
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, this.mborOptionHeight, TimeSpan.FromSeconds(0.3));
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Height", new object[0]));
			storyboard.Children.Add(doubleAnimation);
			storyboard.Completed += delegate(object sender, EventArgs e)
			{
				this.borExpand.Visibility = Visibility.Hidden;
				this.borCollapse.Visibility = Visibility.Visible;
				this.btnExpand.IsEnabled = true;
				this.btnCollapse.IsEnabled = true;
				this.scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				try
				{
					Point point = base.TranslatePoint(new Point(0.0, 0.0), Window.GetWindow(this));
					if (point.Y < 0.0)
					{
						double offsetY = Math.Abs(point.Y) + 10.0;
						Matrix matrix = ((MatrixTransform)base.RenderTransform).Matrix;
						matrix.Translate(0.0, offsetY);
						((MatrixTransform)base.RenderTransform).Matrix = matrix;
					}
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "移动工具条位置失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			};
			this.borOption.Visibility = Visibility.Visible;
			storyboard.Begin(this.borOption);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001CF6F File Offset: 0x0001B16F
		private void UcPaintBrushInfo_SelectPainBrushInfo(PaintBrushInfo paintBrushInfo)
		{
			PainBrushEventHandler selectPainBrushInfo = this.SelectPainBrushInfo;
			if (selectPainBrushInfo == null)
			{
				return;
			}
			selectPainBrushInfo(paintBrushInfo);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001CF82 File Offset: 0x0001B182
		private void ucPaintBrushInfo_MouseLeave(object sender, MouseEventArgs e)
		{
			this.penSelectPop.IsOpen = false;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001CF90 File Offset: 0x0001B190
		private void radioPaintBrush_Click(object sender, RoutedEventArgs e)
		{
			this.ucPaintBrushInfo.SetSelectType();
			PaintBrushInfo paintBrushInfo = this.ucPaintBrushInfo.GetPaintBrushInfo();
			PainBrushEventHandler selectPainBrushInfo = this.SelectPainBrushInfo;
			if (selectPainBrushInfo != null)
			{
				selectPainBrushInfo(paintBrushInfo);
			}
			this.penSelectPop.IsOpen = true;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001CFD2 File Offset: 0x0001B1D2
		private void radioNote_Click(object sender, RoutedEventArgs e)
		{
			this.radioNoteInput.IsChecked = new bool?(false);
			this.radioNoteList.IsChecked = new bool?(false);
			this.notePop.IsOpen = true;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001D002 File Offset: 0x0001B202
		private void radioNoteInput_Click(object sender, RoutedEventArgs e)
		{
			this.notePop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.NoteInput);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001D022 File Offset: 0x0001B222
		private void radioNoteList_Click(object sender, RoutedEventArgs e)
		{
			this.notePop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.NoteList);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001D042 File Offset: 0x0001B242
		private void radioScreenshot_Click(object sender, RoutedEventArgs e)
		{
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Screenshot);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001D056 File Offset: 0x0001B256
		private void radioBookMark_Click(object sender, RoutedEventArgs e)
		{
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.BookMark);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001D06A File Offset: 0x0001B26A
		private void radioSpotlight_Click(object sender, RoutedEventArgs e)
		{
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Spotlight);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001D07E File Offset: 0x0001B27E
		private void radioTool_Click(object sender, RoutedEventArgs e)
		{
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Tool);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001D092 File Offset: 0x0001B292
		private void UcTool_ApplyToolClicked(string applyId, AppToolModel toolInfo)
		{
			this.toolPop.IsOpen = false;
			OpenApplyTool applyToolClicked = this.ApplyToolClicked;
			if (applyToolClicked == null)
			{
				return;
			}
			applyToolClicked(applyId, toolInfo);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		private void radioMore_Click(object sender, RoutedEventArgs e)
		{
			this.radioBlackboard.IsChecked = new bool?(false);
			this.radioTimer.IsChecked = new bool?(false);
			this.radioScreencast.IsChecked = new bool?(false);
			this.infoPop.IsOpen = false;
			this.morePop.IsOpen = true;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001D10C File Offset: 0x0001B30C
		private void radioBlackboard_Click(object sender, RoutedEventArgs e)
		{
			this.morePop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Blackboard);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001D12C File Offset: 0x0001B32C
		private void radioTimer_Click(object sender, RoutedEventArgs e)
		{
			this.morePop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Timer);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001D14C File Offset: 0x0001B34C
		private void radioScreencast_Click(object sender, RoutedEventArgs e)
		{
			this.morePop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(this, OperatorMenueType.Screencast);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001D16D File Offset: 0x0001B36D
		private void radioClass_Click(object sender, RoutedEventArgs e)
		{
			this.radioStartClass.IsChecked = new bool?(false);
			this.classPop.IsOpen = true;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001D18C File Offset: 0x0001B38C
		private void radioStartClass_Click(object sender, RoutedEventArgs e)
		{
			this.classPop.IsOpen = false;
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.StartClass);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		private void CoursewareRadioClass_OnClick(object sender, RoutedEventArgs e)
		{
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick != null)
			{
				menueOperatorClick(this, OperatorMenueType.Courseware);
			}
			string textbookID = BaseBookReader.Instance.TextbookID;
			List<string> chapterId = (from p in BaseBookReader.Instance.CurrentChaptersInfo
			select p.ChapterID).ToList<string>();
			this.courseWareTv.MyCourseWareTv.PepClassInitial(textbookID, chapterId);
			this.coursewarePop.IsOpen = true;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001D22F File Offset: 0x0001B42F
		private void ToCoursewareRadioClass_OnClick(object sender, RoutedEventArgs e)
		{
			CefWindow.Instance.NavigateTo("", CefWindow.Instance.CourseBookInfo, false);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001D24B File Offset: 0x0001B44B
		private void borLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.InitOperateState();
			JXP.Utilities.EventHandler<OperatorMenueType> menueOperatorClick = this.MenueOperatorClick;
			if (menueOperatorClick == null)
			{
				return;
			}
			menueOperatorClick(sender, OperatorMenueType.Logo);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001D268 File Offset: 0x0001B468
		public void InitOperateState()
		{
			this.radioPaintBrush.IsChecked = new bool?(false);
			this.radioNote.IsChecked = new bool?(false);
			this.radioScreenshot.IsChecked = new bool?(false);
			this.radioBookMark.IsChecked = new bool?(false);
			this.radioSpotlight.IsChecked = new bool?(false);
			this.radioTool.IsChecked = new bool?(false);
			this.radioMore.IsChecked = new bool?(false);
			this.radioClass.IsChecked = new bool?(false);
			this.coursewareRadioClass.IsChecked = new bool?(false);
			this.penSelectPop.IsOpen = false;
			this.toolPop.IsOpen = false;
			this.morePop.IsOpen = false;
			this.coursewarePop.IsOpen = false;
			this.radioBlackboard.IsChecked = new bool?(false);
			this.radioTimer.IsChecked = new bool?(false);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001D360 File Offset: 0x0001B560
		public void GetMyToolData(string userid, string xd, string xk, Func<List<AppToolModel>> GetOnlineToolCalBack = null)
		{
			this.ucTool.GetMyToolsData(userid, xd, xk, GetOnlineToolCalBack, null);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001D373 File Offset: 0x0001B573
		public void SetToolPopIsOpen(bool isOpend)
		{
			this.toolPop.IsOpen = isOpend;
		}

		// Token: 0x040002A1 RID: 673
		private double mborOptionHeight = 480.0;
	}
}
