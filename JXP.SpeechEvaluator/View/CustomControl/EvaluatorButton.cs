using System;
using System.Windows;
using System.Windows.Controls;

namespace JXP.SpeechEvaluator.View.CustomControl
{
	// Token: 0x02000016 RID: 22
	public class EvaluatorButton : Button
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006874 File Offset: 0x00004A74
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006886 File Offset: 0x00004A86
		public string AnimatedSource
		{
			get
			{
				return (string)base.GetValue(EvaluatorButton.AnimatedSourceProperty);
			}
			set
			{
				base.SetValue(EvaluatorButton.AnimatedSourceProperty, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006894 File Offset: 0x00004A94
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000068A6 File Offset: 0x00004AA6
		public string DefaultSource
		{
			get
			{
				return (string)base.GetValue(EvaluatorButton.DefaultSourceProperty);
			}
			set
			{
				base.SetValue(EvaluatorButton.DefaultSourceProperty, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000068B4 File Offset: 0x00004AB4
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000068C6 File Offset: 0x00004AC6
		public string DisabledSource
		{
			get
			{
				return (string)base.GetValue(EvaluatorButton.DisabledSourceProperty);
			}
			set
			{
				base.SetValue(EvaluatorButton.DisabledSourceProperty, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000068D4 File Offset: 0x00004AD4
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000068E6 File Offset: 0x00004AE6
		public string Caption
		{
			get
			{
				return (string)base.GetValue(EvaluatorButton.CaptionProperty);
			}
			set
			{
				base.SetValue(EvaluatorButton.CaptionProperty, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000068F4 File Offset: 0x00004AF4
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006906 File Offset: 0x00004B06
		public EvaluatorButtonState ButtonState
		{
			get
			{
				return (EvaluatorButtonState)base.GetValue(EvaluatorButton.ButtonStateProperty);
			}
			set
			{
				base.SetValue(EvaluatorButton.ButtonStateProperty, value);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000691C File Offset: 0x00004B1C
		static EvaluatorButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(EvaluatorButton), new FrameworkPropertyMetadata(typeof(EvaluatorButton)));
		}

		// Token: 0x04000072 RID: 114
		public static readonly DependencyProperty AnimatedSourceProperty = DependencyProperty.Register("AnimatedSource", typeof(string), typeof(EvaluatorButton), new FrameworkPropertyMetadata(string.Empty));

		// Token: 0x04000073 RID: 115
		public static readonly DependencyProperty DefaultSourceProperty = DependencyProperty.Register("DefaultSource", typeof(string), typeof(EvaluatorButton), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

		// Token: 0x04000074 RID: 116
		public static readonly DependencyProperty DisabledSourceProperty = DependencyProperty.Register("DisabledSource", typeof(string), typeof(EvaluatorButton), new PropertyMetadata(string.Empty));

		// Token: 0x04000075 RID: 117
		public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(EvaluatorButton), new PropertyMetadata(string.Empty));

		// Token: 0x04000076 RID: 118
		public static readonly DependencyProperty ButtonStateProperty = DependencyProperty.Register("ButtonState", typeof(EvaluatorButtonState), typeof(EvaluatorButton), new FrameworkPropertyMetadata(EvaluatorButtonState.Default));
	}
}
