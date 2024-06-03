using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JXP.Models;
using JXP.Networks;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200006D RID: 109
	public class SelectGroupViewModel : BaseModel
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00026583 File Offset: 0x00024783
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0002658B File Offset: 0x0002478B
		public ObservableCollection<GroupInfoModel> GroupCollection
		{
			get
			{
				return this.mGroupCollection;
			}
			set
			{
				this.mGroupCollection = value;
				this.OnPropertyChanged<ObservableCollection<GroupInfoModel>>(() => this.GroupCollection);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000265C9 File Offset: 0x000247C9
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x000265D1 File Offset: 0x000247D1
		public GroupInfoModel SelectGroupInfo
		{
			get
			{
				return this.mSelectGroupInfo;
			}
			set
			{
				this.mSelectGroupInfo = value;
				base.OnPropertyChanged<GroupInfoModel>(() => this.SelectGroupInfo);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0002660F File Offset: 0x0002480F
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00026617 File Offset: 0x00024817
		public bool ScreenFlg
		{
			get
			{
				return this.mScreenFlg;
			}
			set
			{
				this.mScreenFlg = value;
				base.OnPropertyChanged<bool>(() => this.ScreenFlg);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00026655 File Offset: 0x00024855
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0002665D File Offset: 0x0002485D
		public string WifiName
		{
			get
			{
				return this.mWifiName;
			}
			set
			{
				this.mWifiName = value;
				base.OnPropertyChanged<string>(() => this.WifiName);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0002669B File Offset: 0x0002489B
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x000266A3 File Offset: 0x000248A3
		public bool IsWifiInfoVisible
		{
			get
			{
				return this.mIsWifiInfoVisible;
			}
			set
			{
				this.mIsWifiInfoVisible = value;
				base.OnPropertyChanged<bool>(() => this.IsWifiInfoVisible);
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000266E4 File Offset: 0x000248E4
		public void InitData(List<GroupInfoModel> lstGroups)
		{
			this.GroupCollection = new ObservableCollection<GroupInfoModel>(lstGroups);
			if (this.GroupCollection.Count > 0)
			{
				this.SelectGroupInfo = this.GroupCollection.FirstOrDefault<GroupInfoModel>();
			}
			this.WifiName = WifiHelper.Instance.WifiName;
			this.IsWifiInfoVisible = !object.Equals(WifiHelper.Instance.WifiIP, IPAddress.None);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002674C File Offset: 0x0002494C
		public Task<List<GroupInfoModel>> GetGroupList(string userId, string serverUrl)
		{
			SelectGroupViewModel.<GetGroupList>d__21 <GetGroupList>d__;
			<GetGroupList>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<GroupInfoModel>>.Create();
			<GetGroupList>d__.userId = userId;
			<GetGroupList>d__.serverUrl = serverUrl;
			<GetGroupList>d__.<>1__state = -1;
			<GetGroupList>d__.<>t__builder.Start<SelectGroupViewModel.<GetGroupList>d__21>(ref <GetGroupList>d__);
			return <GetGroupList>d__.<>t__builder.Task;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00026798 File Offset: 0x00024998
		public Task<List<GroupUserInfoModel>> GetUserInfo(string groupId, string serverUrl)
		{
			SelectGroupViewModel.<GetUserInfo>d__22 <GetUserInfo>d__;
			<GetUserInfo>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<GroupUserInfoModel>>.Create();
			<GetUserInfo>d__.groupId = groupId;
			<GetUserInfo>d__.serverUrl = serverUrl;
			<GetUserInfo>d__.<>1__state = -1;
			<GetUserInfo>d__.<>t__builder.Start<SelectGroupViewModel.<GetUserInfo>d__22>(ref <GetUserInfo>d__);
			return <GetUserInfo>d__.<>t__builder.Task;
		}

		// Token: 0x040003DA RID: 986
		private ObservableCollection<GroupInfoModel> mGroupCollection;

		// Token: 0x040003DB RID: 987
		private GroupInfoModel mSelectGroupInfo;

		// Token: 0x040003DC RID: 988
		private bool mScreenFlg = true;

		// Token: 0x040003DD RID: 989
		private string mWifiName = string.Empty;

		// Token: 0x040003DE RID: 990
		private bool mIsWifiInfoVisible;
	}
}
