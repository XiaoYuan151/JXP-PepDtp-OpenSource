using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.View.CustomControl;
using JXP.Utilities;

namespace JXP.PepDtp.View.UserControls
{
	// Token: 0x02000049 RID: 73
	public partial class TeacherTitle : UserControl
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001A789 File Offset: 0x00018989
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0001A791 File Offset: 0x00018991
		public NoticeMessages NoticeMsgOper
		{
			get
			{
				return this.mNoticeMsgOper;
			}
			set
			{
				this.mNoticeMsgOper = value;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001A79C File Offset: 0x0001899C
		public TeacherTitle()
		{
			this.InitializeComponent();
			base.DataContext = this.mNoticeMsgOper;
			this.canvasMarquee.SizeChanged += this.CanvasMarquee_SizeChanged;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001A7F9 File Offset: 0x000189F9
		public void SetMessage(Tuple<bool, string> tupleInfo)
		{
			this.mNoticeMsgOper.SetMessage(tupleInfo);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001A808 File Offset: 0x00018A08
		public void SetMessage(NoticeMessages message)
		{
			if (message == null)
			{
				return;
			}
			this.mNoticeMsgOper.VisibleMessage = message.VisibleMessage;
			this.mNoticeMsgOper.CurrentMsg = message.CurrentMsg;
			if (this.mNoticeMsgOper.VisibleMessage == Visibility.Visible)
			{
				if (this.mMarqueeAnimation == null)
				{
					this.RightToLeftMarquee();
					return;
				}
			}
			else
			{
				Storyboard storyboard = this.mMarqueeAnimation;
				if (storyboard == null)
				{
					return;
				}
				storyboard.Stop();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001A867 File Offset: 0x00018A67
		private void CanvasMarquee_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.mNoticeMsgOper.VisibleMessage == Visibility.Visible)
			{
				this.RightToLeftMarquee();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001A87C File Offset: 0x00018A7C
		private void RightToLeftMarquee()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					Storyboard storyboard = this.mMarqueeAnimation;
					if (storyboard != null)
					{
						storyboard.Stop();
					}
					this.mMarqueeAnimation = new Storyboard();
					DoubleAnimation doubleAnimation = new DoubleAnimation();
					doubleAnimation.From = new double?(-this.gridMarquee.ActualWidth);
					doubleAnimation.To = new double?(this.canvasMarquee.ActualWidth);
					doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
					int num = (int)Math.Ceiling((decimal)((double)1 * (doubleAnimation.To - doubleAnimation.From) / (double)40).Value);
					doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds((double)num));
					Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Right)", new object[0]));
					this.mMarqueeAnimation.Children.Add(doubleAnimation);
					this.mMarqueeAnimation.Begin(this.gridMarquee);
				}
				catch
				{
				}
			}), new object[0]);
		}

		// Token: 0x0400026D RID: 621
		private NoticeMessages mNoticeMsgOper = new NoticeMessages();

		// Token: 0x0400026E RID: 622
		private HttpHelper mHttpOper = new HttpHelper();

		// Token: 0x0400026F RID: 623
		private JsonHelper mJsonOper = new JsonHelper();

		// Token: 0x04000270 RID: 624
		private Storyboard mMarqueeAnimation;
	}
}
