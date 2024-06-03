using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.View.CustomControl;

namespace JXP.SpeechEvaluator.View
{
	// Token: 0x02000011 RID: 17
	public partial class TestWin : Window
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005A97 File Offset: 0x00003C97
		public TestWin()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005AA8 File Offset: 0x00003CA8
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Sentence sentence = new Sentence();
			sentence.Content = "I often watch TV, but this weekend I have a show. I’ll play the pipa.";
			sentence.Italic = "of,ten,watch,I,have";
			this.richText.SetDefaultRichText(sentence);
		}
	}
}
