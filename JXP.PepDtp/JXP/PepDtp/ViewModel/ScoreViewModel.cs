using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using JXP.Models;
using JXP.PepDtp.Model;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200006C RID: 108
	public class ScoreViewModel : BaseModel
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00026390 File Offset: 0x00024590
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00026398 File Offset: 0x00024598
		public string Title
		{
			get
			{
				return this.mTitle;
			}
			set
			{
				this.mTitle = value;
				base.OnPropertyChanged<string>(() => this.Title);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000263D6 File Offset: 0x000245D6
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x000263DE File Offset: 0x000245DE
		public string IcoPath
		{
			get
			{
				return this.mIcoPath;
			}
			set
			{
				this.mIcoPath = value;
				base.OnPropertyChanged<string>(() => this.IcoPath);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0002641C File Offset: 0x0002461C
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00026424 File Offset: 0x00024624
		public bool ScoreEnable
		{
			get
			{
				return this.mScoreEnable;
			}
			set
			{
				this.mScoreEnable = value;
				base.OnPropertyChanged<bool>(() => this.ScoreEnable);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00026462 File Offset: 0x00024662
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0002646A File Offset: 0x0002466A
		public Visibility ScoreSuccessVisible
		{
			get
			{
				return this.mScoreSuccessVisible;
			}
			set
			{
				this.mScoreSuccessVisible = value;
				base.OnPropertyChanged<Visibility>(() => this.ScoreSuccessVisible);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000264A8 File Offset: 0x000246A8
		public Task<ScoreResultModel> GetScoreAsync(string userId, string userType, string targetId, string targetType)
		{
			ScoreViewModel.<GetScoreAsync>d__16 <GetScoreAsync>d__;
			<GetScoreAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<ScoreResultModel>.Create();
			<GetScoreAsync>d__.userId = userId;
			<GetScoreAsync>d__.userType = userType;
			<GetScoreAsync>d__.targetId = targetId;
			<GetScoreAsync>d__.targetType = targetType;
			<GetScoreAsync>d__.<>1__state = -1;
			<GetScoreAsync>d__.<>t__builder.Start<ScoreViewModel.<GetScoreAsync>d__16>(ref <GetScoreAsync>d__);
			return <GetScoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00026504 File Offset: 0x00024704
		public Task<bool> AddScore(string userId, string userType, string targetId, string targetType, string score)
		{
			ScoreViewModel.<AddScore>d__17 <AddScore>d__;
			<AddScore>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<bool>.Create();
			<AddScore>d__.userId = userId;
			<AddScore>d__.userType = userType;
			<AddScore>d__.targetId = targetId;
			<AddScore>d__.targetType = targetType;
			<AddScore>d__.score = score;
			<AddScore>d__.<>1__state = -1;
			<AddScore>d__.<>t__builder.Start<ScoreViewModel.<AddScore>d__17>(ref <AddScore>d__);
			return <AddScore>d__.<>t__builder.Task;
		}

		// Token: 0x040003D6 RID: 982
		private string mTitle = string.Empty;

		// Token: 0x040003D7 RID: 983
		private string mIcoPath;

		// Token: 0x040003D8 RID: 984
		private bool mScoreEnable;

		// Token: 0x040003D9 RID: 985
		private Visibility mScoreSuccessVisible = Visibility.Hidden;
	}
}
