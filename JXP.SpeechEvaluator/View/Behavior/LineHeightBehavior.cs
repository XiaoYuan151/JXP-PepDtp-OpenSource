using System;
using System.Windows;
using System.Windows.Controls;

namespace JXP.SpeechEvaluator.View.Behavior
{
	// Token: 0x02000028 RID: 40
	public class LineHeightBehavior
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00006F4C File Offset: 0x0000514C
		public static void SetMaxLines(DependencyObject element, int value)
		{
			element.SetValue(LineHeightBehavior.MaxLinesProperty, value);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006F5F File Offset: 0x0000515F
		public static int GetMaxLines(DependencyObject element)
		{
			return (int)element.GetValue(LineHeightBehavior.MaxLinesProperty);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006F74 File Offset: 0x00005174
		private static void OnMaxLinesPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TextBlock textBlock = d as TextBlock;
			if (textBlock != null)
			{
				textBlock.MaxHeight = textBlock.LineHeight * (double)LineHeightBehavior.GetMaxLines(textBlock);
			}
		}

		// Token: 0x0400009F RID: 159
		public static readonly DependencyProperty MaxLinesProperty = DependencyProperty.RegisterAttached("MaxLines", typeof(int), typeof(LineHeightBehavior), new PropertyMetadata(0, new PropertyChangedCallback(LineHeightBehavior.OnMaxLinesPropertyChangedCallback)));
	}
}
