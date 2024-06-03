using System;
using System.Windows;
using System.Windows.Controls;
using JXP.SpeechEvaluator.Model;

namespace JXP.SpeechEvaluator.View.Selector
{
	// Token: 0x02000014 RID: 20
	public class DuiHuaSentenceTemplateSelector : DataTemplateSelector
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00006250 File Offset: 0x00004450
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (!(item is Sentence))
			{
				return null;
			}
			Sentence sentence = item as Sentence;
			if (sentence.RoleAlignment == RoleAlignment.Right)
			{
				return Application.Current.FindResource("DuiHuaSentenceRightTemplate") as DataTemplate;
			}
			if (sentence.RoleAlignment == RoleAlignment.Center)
			{
				return Application.Current.FindResource("DuiHuaSentenceCenterTemplate") as DataTemplate;
			}
			return Application.Current.FindResource("DuiHuaSentenceLeftTemplate") as DataTemplate;
		}
	}
}
