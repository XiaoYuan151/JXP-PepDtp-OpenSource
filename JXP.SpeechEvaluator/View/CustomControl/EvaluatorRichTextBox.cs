using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Xunfei;
using JXP.SpeechEvaluator.Utility;

namespace JXP.SpeechEvaluator.View.CustomControl
{
	// Token: 0x02000015 RID: 21
	public class EvaluatorRichTextBox : RichTextBox
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000062CE File Offset: 0x000044CE
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000062E0 File Offset: 0x000044E0
		public Sentence Text
		{
			get
			{
				return (Sentence)base.GetValue(EvaluatorRichTextBox.TextProperty);
			}
			set
			{
				base.SetValue(EvaluatorRichTextBox.TextProperty, value);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000062F0 File Offset: 0x000044F0
		private new static void TextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EvaluatorRichTextBox richTextBox = (EvaluatorRichTextBox)obj;
			richTextBox.Dispatcher.BeginInvoke(new Action(delegate()
			{
				richTextBox.SetDefaultRichText(e.NewValue as Sentence);
			}), DispatcherPriority.Send, new object[0]);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000633B File Offset: 0x0000453B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000634D File Offset: 0x0000454D
		public ReadChapter XfResult
		{
			get
			{
				return (ReadChapter)base.GetValue(EvaluatorRichTextBox.XfResultProperty);
			}
			set
			{
				base.SetValue(EvaluatorRichTextBox.XfResultProperty, value);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000635C File Offset: 0x0000455C
		private static void XfResultChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			EvaluatorRichTextBox richTextBox = (EvaluatorRichTextBox)obj;
			richTextBox.Dispatcher.BeginInvoke(new Action(delegate()
			{
				richTextBox.ChangeRichTextByXf(e.NewValue as ReadChapter);
			}), DispatcherPriority.Send, new object[0]);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000063A8 File Offset: 0x000045A8
		public void SetDefaultRichText(Sentence sentence)
		{
			if (sentence == null)
			{
				return;
			}
			System.Windows.Documents.Paragraph paragraph = base.Document.Blocks.FirstBlock as System.Windows.Documents.Paragraph;
			InlineUIContainer inlineUIContainer = paragraph.Inlines.LastInline as InlineUIContainer;
			TextPointer textPointer = paragraph.ContentStart;
			TextPointer contentStart = inlineUIContainer.ContentStart;
			TextRange textRange = new TextRange(textPointer, contentStart);
			textRange.ClearAllProperties();
			textRange.Text = sentence.Content;
			List<string> list = (sentence.Italic == null) ? new List<string>() : sentence.Italic.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
			while (textPointer != null && list.Count > 0 && textPointer.CompareTo(contentStart) < 0)
			{
				int num = textPointer.GetTextInRun(LogicalDirection.Forward).IndexOf(list[0], StringComparison.OrdinalIgnoreCase);
				if (num < 0)
				{
					textPointer = textPointer.GetNextContextPosition(LogicalDirection.Forward);
				}
				else
				{
					textPointer = textPointer.GetPositionAtOffset(num);
					TextPointer positionAtOffset = textPointer.GetPositionAtOffset(list[0].Length);
					TextRange textRange2 = new TextRange(textPointer, positionAtOffset);
					textRange2.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
					list.RemoveAt(0);
					textPointer = textRange2.End;
				}
			}
			if (!string.IsNullOrEmpty(sentence.Hidden))
			{
				textPointer = paragraph.ContentStart;
				while (textPointer != null && textPointer.CompareTo(contentStart) < 0)
				{
					string textInRun = textPointer.GetTextInRun(LogicalDirection.Forward);
					string[] array = textInRun.Split(new char[]
					{
						' ',
						' '
					}, StringSplitOptions.RemoveEmptyEntries);
					string text = (array.Length != 0) ? array[0] : string.Empty;
					if (!string.IsNullOrEmpty(text))
					{
						int offset = textInRun.IndexOf(text);
						textPointer = textPointer.GetPositionAtOffset(offset);
						TextRange textRange3 = new TextRange(textPointer.GetPositionAtOffset(text.Length), contentStart);
						textRange3.ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.HIDDEN_Fg);
						textRange3.ApplyPropertyValue(TextElement.BackgroundProperty, EvaluatorRichTextBox.HIDDEN_Bg);
						return;
					}
					textPointer = textPointer.GetNextContextPosition(LogicalDirection.Forward);
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000657C File Offset: 0x0000477C
		public void ChangeRichTextByXf(ReadChapter readChapter)
		{
			if (readChapter == null)
			{
				this.SetDefaultRichText(this.Text);
				return;
			}
			if (!readChapter.NeedChangeColor)
			{
				return;
			}
			System.Windows.Documents.Paragraph paragraph = base.Document.Blocks.FirstBlock as System.Windows.Documents.Paragraph;
			InlineUIContainer inlineUIContainer = paragraph.Inlines.LastInline as InlineUIContainer;
			TextPointer textPointer = paragraph.ContentStart;
			TextPointer nextContextPosition = inlineUIContainer.ContentStart.GetNextContextPosition(LogicalDirection.Backward);
			if (readChapter.IsRejected)
			{
				new TextRange(textPointer, nextContextPosition).ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.RED);
				return;
			}
			List<Word> list = new List<Word>();
			if (readChapter.Sentences == null)
			{
				goto IL_1D5;
			}
			using (List<XSentence>.Enumerator enumerator = readChapter.Sentences.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					XSentence xsentence = enumerator.Current;
					foreach (Word word in xsentence.Words)
					{
						if (word.TotalScore >= 0.0)
						{
							list.Add(word);
						}
					}
				}
				goto IL_1D5;
			}
			IL_FE:
			int num = textPointer.GetTextInRun(LogicalDirection.Forward).IndexOf(list[0].Content, StringComparison.OrdinalIgnoreCase);
			if (num < 0)
			{
				textPointer = textPointer.GetNextContextPosition(LogicalDirection.Forward);
			}
			else
			{
				textPointer = textPointer.GetPositionAtOffset(num);
				TextPointer positionAtOffset = textPointer.GetPositionAtOffset(list[0].Content.Length);
				TextRange textRange = new TextRange(textPointer, positionAtOffset);
				int score = EvaluatorContext.GetScore(list[0].TotalScore);
				if (score >= 85)
				{
					textRange.ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.GREEN);
				}
				else if (score >= 70)
				{
					textRange.ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.YELLOW);
				}
				else if (score >= 60)
				{
					textRange.ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.BLUE);
				}
				else
				{
					textRange.ApplyPropertyValue(TextElement.ForegroundProperty, EvaluatorRichTextBox.RED);
				}
				list.RemoveAt(0);
				textPointer = textRange.End;
			}
			IL_1D5:
			if (textPointer == null || list.Count <= 0 || textPointer.CompareTo(nextContextPosition) >= 0)
			{
				return;
			}
			goto IL_FE;
		}

		// Token: 0x0400006A RID: 106
		private static readonly Brush RED = Brushes.Red;

		// Token: 0x0400006B RID: 107
		private static readonly Brush GREEN = Brushes.Green;

		// Token: 0x0400006C RID: 108
		private static readonly Brush BLUE = Brushes.Blue;

		// Token: 0x0400006D RID: 109
		private static readonly Brush YELLOW = (SolidColorBrush)new BrushConverter().ConvertFrom("#ebaf33");

		// Token: 0x0400006E RID: 110
		private static readonly Brush HIDDEN_Fg = (SolidColorBrush)new BrushConverter().ConvertFrom("#00FFFFFF");

		// Token: 0x0400006F RID: 111
		private static readonly Brush HIDDEN_Bg = (SolidColorBrush)new BrushConverter().ConvertFrom("#88f5ece4");

		// Token: 0x04000070 RID: 112
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(Sentence), typeof(EvaluatorRichTextBox), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(EvaluatorRichTextBox.TextChanged)));

		// Token: 0x04000071 RID: 113
		public static readonly DependencyProperty XfResultProperty = DependencyProperty.Register("XfResult", typeof(ReadChapter), typeof(EvaluatorRichTextBox), new PropertyMetadata(null, new PropertyChangedCallback(EvaluatorRichTextBox.XfResultChanged)));
	}
}
