using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JXP.SpeechEvaluator.Model;
using JXP.SpeechEvaluator.Model.Http;
using Microsoft.Practices.Prism.ViewModel;

namespace JXP.SpeechEvaluator.ViewModel
{
	// Token: 0x0200002D RID: 45
	public class IndexPageVM : NotificationObject
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00007654 File Offset: 0x00005854
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00007679 File Offset: 0x00005879
		public ObservableCollection<Speech> Speeches
		{
			get
			{
				return this.mSpeeches = (this.mSpeeches ?? new ObservableCollection<Speech>());
			}
			set
			{
				this.mSpeeches = value;
				base.RaisePropertyChanged<ObservableCollection<Speech>>(() => this.Speeches);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000076B7 File Offset: 0x000058B7
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000076BF File Offset: 0x000058BF
		public List<Speech> XsSpeeches { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000076C8 File Offset: 0x000058C8
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000076D0 File Offset: 0x000058D0
		public List<Speech> DhSpeeches { get; private set; }

		// Token: 0x06000183 RID: 387 RVA: 0x000076DC File Offset: 0x000058DC
		public IndexPageVM()
		{
			this.DhSpeeches = new List<Speech>();
			this.DhSpeeches.Add(new Speech
			{
				Caption = "跟读",
				Type = SpeechTestType.DH_GenDu
			});
			this.DhSpeeches.Add(new Speech
			{
				Caption = "对话",
				Type = SpeechTestType.DH_DuiHua
			});
			this.DhSpeeches.Add(new Speech
			{
				Caption = "背诵",
				Type = SpeechTestType.DH_BeiSong
			});
			this.XsSpeeches = new List<Speech>();
			this.XsSpeeches.Add(new Speech
			{
				Caption = "跟读",
				Type = SpeechTestType.XS_GenDu
			});
			this.XsSpeeches.Add(new Speech
			{
				Caption = "自读",
				Type = SpeechTestType.XS_ZiDu
			});
			this.XsSpeeches.Add(new Speech
			{
				Caption = "背诵",
				Type = SpeechTestType.XS_BeiSong
			});
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000077D4 File Offset: 0x000059D4
		public List<Speech> ResetDhSpeech(VoiceRes res)
		{
			return this.DhSpeeches.Select(delegate(Speech item)
			{
				item.VoiceRes = res;
				return item;
			}).ToList<Speech>();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000780C File Offset: 0x00005A0C
		public List<Speech> ResetXsSpeech(VoiceRes res)
		{
			return this.XsSpeeches.Select(delegate(Speech item)
			{
				item.VoiceRes = res;
				return item;
			}).ToList<Speech>();
		}

		// Token: 0x040000AB RID: 171
		private ObservableCollection<Speech> mSpeeches;
	}
}
