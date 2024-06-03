using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using JXP.Cef.Utils;
using JXP.Configuration;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Model;
using JXP.Threading;
using JXP.Utilities;
using JXP.Utilities.Cryptography;
using Newtonsoft.Json;
using Xilium.CefGlue;
using ZXing;
using ZXing.Common;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x0200005D RID: 93
	public class LoginViewModel : BaseModel
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00020F00 File Offset: 0x0001F100
		public LoginViewModel()
		{
			this.LoadData();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00020FC1 File Offset: 0x0001F1C1
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00020FC9 File Offset: 0x0001F1C9
		public UserModel UserInfo
		{
			get
			{
				return this.mUserInfo;
			}
			set
			{
				this.mUserInfo = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00020FD2 File Offset: 0x0001F1D2
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00020FDA File Offset: 0x0001F1DA
		public string ResetUserID { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00020FE3 File Offset: 0x0001F1E3
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x00020FEB File Offset: 0x0001F1EB
		public string ResetRandomCode { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x00020FFC File Offset: 0x0001F1FC
		public string ResetPhone { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00021005 File Offset: 0x0001F205
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0002100D File Offset: 0x0001F20D
		public string CurValidCode { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00021016 File Offset: 0x0001F216
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0002101E File Offset: 0x0001F21E
		public string UploadPhotoRelativeUrl { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00021027 File Offset: 0x0001F227
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0002102F File Offset: 0x0001F22F
		public LoginViewModel.CheckLoginUserData CheckLoginUserDataCallBack { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00021038 File Offset: 0x0001F238
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00021040 File Offset: 0x0001F240
		public ObservableCollection<XKViewModel> XKList
		{
			get
			{
				return this.mXKList;
			}
			set
			{
				this.mXKList = value;
				base.OnPropertyChanged<ObservableCollection<XKViewModel>>(() => this.XKList);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0002107E File Offset: 0x0001F27E
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00021086 File Offset: 0x0001F286
		public ObservableCollection<XDViewModel> XDList
		{
			get
			{
				return this.mXDList;
			}
			set
			{
				this.mXDList = value;
				base.OnPropertyChanged<ObservableCollection<XDViewModel>>(() => this.XDList);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000210C4 File Offset: 0x0001F2C4
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x000210CC File Offset: 0x0001F2CC
		public XDViewModel SelectedXD
		{
			get
			{
				return this.mSelectedXD;
			}
			set
			{
				this.mSelectedXD = value;
				base.OnPropertyChanged<XDViewModel>(() => this.SelectedXD);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0002110A File Offset: 0x0001F30A
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00021112 File Offset: 0x0001F312
		public ObservableCollection<GradeViewModel> GradeList
		{
			get
			{
				return this.mGradeList;
			}
			set
			{
				this.mGradeList = value;
				base.OnPropertyChanged<ObservableCollection<GradeViewModel>>(() => this.GradeList);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00021150 File Offset: 0x0001F350
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00021158 File Offset: 0x0001F358
		public GradeViewModel SelectedGrade
		{
			get
			{
				return this.mSelectedGrade;
			}
			set
			{
				this.mSelectedGrade = value;
				base.OnPropertyChanged<GradeViewModel>(() => this.SelectedGrade);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00021196 File Offset: 0x0001F396
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x000211A0 File Offset: 0x0001F3A0
		public bool SexMan
		{
			get
			{
				return this.mSexMan;
			}
			set
			{
				this.mSexMan = value;
				if (this.mSexMan)
				{
					this.SexId = 2;
					this.SexName = "男";
				}
				base.OnPropertyChanged<bool>(() => this.SexMan);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00021203 File Offset: 0x0001F403
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0002120C File Offset: 0x0001F40C
		public bool SexWoman
		{
			get
			{
				return this.mSexWoman;
			}
			set
			{
				this.mSexWoman = value;
				if (this.mSexWoman)
				{
					this.SexId = 1;
					this.SexName = "女";
				}
				base.OnPropertyChanged<bool>(() => this.SexWoman);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0002126F File Offset: 0x0001F46F
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00021277 File Offset: 0x0001F477
		public int SexId
		{
			get
			{
				return this.mSexId;
			}
			set
			{
				this.mSexId = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00021280 File Offset: 0x0001F480
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00021288 File Offset: 0x0001F488
		public string SexName
		{
			get
			{
				return this.mSexName;
			}
			set
			{
				this.mSexName = value;
				base.OnPropertyChanged<string>(() => this.SexName);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x000212C6 File Offset: 0x0001F4C6
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x000212D0 File Offset: 0x0001F4D0
		public bool Teacher
		{
			get
			{
				return this.mTeacher;
			}
			set
			{
				this.mTeacher = value;
				if (this.mTeacher)
				{
					this.TypeId = "A02";
					this.TypeName = "教师";
					this.XDVisibility = Visibility.Visible;
					this.GradeVisibility = Visibility.Hidden;
					this.XKVisibility = Visibility.Visible;
				}
				base.OnPropertyChanged<bool>(() => this.Teacher);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0002134C File Offset: 0x0001F54C
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00021354 File Offset: 0x0001F554
		public bool Student
		{
			get
			{
				return this.mStudent;
			}
			set
			{
				this.mStudent = value;
				if (this.mStudent)
				{
					this.TypeId = "A01";
					this.TypeName = "学生";
					this.XDVisibility = Visibility.Hidden;
					this.GradeVisibility = Visibility.Visible;
					this.XKVisibility = Visibility.Collapsed;
				}
				base.OnPropertyChanged<bool>(() => this.Student);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000213D0 File Offset: 0x0001F5D0
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000213D8 File Offset: 0x0001F5D8
		public string TypeId
		{
			get
			{
				return this.mTypeId;
			}
			set
			{
				this.mTypeId = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000213E1 File Offset: 0x0001F5E1
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000213E9 File Offset: 0x0001F5E9
		public string TypeName
		{
			get
			{
				return this.mTypeName;
			}
			set
			{
				this.mTypeName = value;
				base.OnPropertyChanged<string>(() => this.TypeName);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00021427 File Offset: 0x0001F627
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0002142F File Offset: 0x0001F62F
		public Visibility ResetVisibility
		{
			get
			{
				return this.mResetVisibility;
			}
			set
			{
				this.mResetVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.ResetVisibility);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0002146D File Offset: 0x0001F66D
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00021475 File Offset: 0x0001F675
		public Visibility GetTestCodeVisibility
		{
			get
			{
				return this.mGetTestCodeVisibility;
			}
			set
			{
				this.mGetTestCodeVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.GetTestCodeVisibility);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000214B3 File Offset: 0x0001F6B3
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000214BC File Offset: 0x0001F6BC
		public Visibility LoginVisibility
		{
			get
			{
				return this.mLoginVisibility;
			}
			set
			{
				this.mLoginVisibility = value;
				if (value == Visibility.Visible)
				{
					this.LoadQRCode();
				}
				else
				{
					this.StopQRWork();
				}
				base.OnPropertyChanged<Visibility>(() => this.LoginVisibility);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00021516 File Offset: 0x0001F716
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0002151E File Offset: 0x0001F71E
		public bool ShowContactCustomer
		{
			get
			{
				return this.mShowContactCustomer;
			}
			set
			{
				this.mShowContactCustomer = value;
				base.OnPropertyChanged<bool>(() => this.ShowContactCustomer);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0002155C File Offset: 0x0001F75C
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00021564 File Offset: 0x0001F764
		public Visibility UserActivateVisibility
		{
			get
			{
				return this.mUserActivateVisibility;
			}
			set
			{
				this.mUserActivateVisibility = value;
				if (value == Visibility.Visible)
				{
					this.InitActiveData();
				}
				base.OnPropertyChanged<Visibility>(() => this.UserActivateVisibility);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x000215B6 File Offset: 0x0001F7B6
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x000215BE File Offset: 0x0001F7BE
		public Visibility XDVisibility
		{
			get
			{
				return this.mXDVisibility;
			}
			set
			{
				this.mXDVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.XDVisibility);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000215FC File Offset: 0x0001F7FC
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00021604 File Offset: 0x0001F804
		public Visibility GradeVisibility
		{
			get
			{
				return this.mGradeVisibility;
			}
			set
			{
				this.mGradeVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.GradeVisibility);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00021642 File Offset: 0x0001F842
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0002164A File Offset: 0x0001F84A
		public Visibility WarningVisibility
		{
			get
			{
				return this.mWarningVisibility;
			}
			set
			{
				this.mWarningVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.WarningVisibility);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00021688 File Offset: 0x0001F888
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00021690 File Offset: 0x0001F890
		public Visibility PhoneBindingVisibility
		{
			get
			{
				return this.mPhoneBindingVisibility;
			}
			set
			{
				this.mPhoneBindingVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.PhoneBindingVisibility);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x000216CE File Offset: 0x0001F8CE
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x000216D6 File Offset: 0x0001F8D6
		public Visibility XKVisibility
		{
			get
			{
				return this.mXKVisibility;
			}
			set
			{
				this.mXKVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.XKVisibility);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00021714 File Offset: 0x0001F914
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0002171C File Offset: 0x0001F91C
		public Visibility UploadPhotoVisibility
		{
			get
			{
				return this.mUploadPhotoVisibility;
			}
			set
			{
				this.mUploadPhotoVisibility = value;
				base.OnPropertyChanged<Visibility>(() => this.UploadPhotoVisibility);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0002175A File Offset: 0x0001F95A
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00021764 File Offset: 0x0001F964
		public BitmapImage UploadPhotoLocalUrl
		{
			get
			{
				return this.mUploadPhotoLocalUrl;
			}
			set
			{
				this.mUploadPhotoLocalUrl = value;
				if (value != null)
				{
					this.UploadPhotoVisibility = Visibility.Collapsed;
				}
				else
				{
					this.UploadPhotoVisibility = Visibility.Visible;
				}
				base.OnPropertyChanged<BitmapImage>(() => this.UploadPhotoLocalUrl);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x000217C0 File Offset: 0x0001F9C0
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x000217C8 File Offset: 0x0001F9C8
		public string VerificationCode
		{
			get
			{
				return this.mVerificationCode;
			}
			set
			{
				this.mVerificationCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.VerificationCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.VerificationCode);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00021824 File Offset: 0x0001FA24
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0002182C File Offset: 0x0001FA2C
		public string VerificationCodeTip
		{
			get
			{
				return this.mVerificationCodeTip;
			}
			set
			{
				this.mVerificationCodeTip = value;
				base.OnPropertyChanged<string>(() => this.VerificationCodeTip);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0002186A File Offset: 0x0001FA6A
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00021872 File Offset: 0x0001FA72
		public string SendMessageTip
		{
			get
			{
				return this.mSendMessageTip;
			}
			set
			{
				this.mSendMessageTip = value;
				base.OnPropertyChanged<string>(() => this.SendMessageTip);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000218B0 File Offset: 0x0001FAB0
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000218B8 File Offset: 0x0001FAB8
		public string ResetCode
		{
			get
			{
				return this.mResetCode;
			}
			set
			{
				this.mResetCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.ResetCode);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00021914 File Offset: 0x0001FB14
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0002191C File Offset: 0x0001FB1C
		public string ResetCodeAgain
		{
			get
			{
				return this.mResetCodeAgain;
			}
			set
			{
				this.mResetCodeAgain = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.ResetCodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.ResetCodeAgain);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00021978 File Offset: 0x0001FB78
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00021980 File Offset: 0x0001FB80
		public string ResetCodeTip
		{
			get
			{
				return this.mResetCodeTip;
			}
			set
			{
				this.mResetCodeTip = value;
				base.OnPropertyChanged<string>(() => this.ResetCodeTip);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000219BE File Offset: 0x0001FBBE
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000219C8 File Offset: 0x0001FBC8
		public string GetBackId
		{
			get
			{
				return this.mGetBackId;
			}
			set
			{
				this.mGetBackId = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.GetBackIdTip = "";
				}
				base.OnPropertyChanged<string>(() => this.GetBackId);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00021A24 File Offset: 0x0001FC24
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00021A2C File Offset: 0x0001FC2C
		public string GetBackIdTip
		{
			get
			{
				return this.mGetBackIdTip;
			}
			set
			{
				this.mGetBackIdTip = value;
				base.OnPropertyChanged<string>(() => this.GetBackIdTip);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00021A6A File Offset: 0x0001FC6A
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00021A74 File Offset: 0x0001FC74
		public string TestCode
		{
			get
			{
				return this.mTestCode;
			}
			set
			{
				this.mTestCode = value;
				base.OnPropertyChanged<string>(() => this.TestCode);
				this.TestCodeTip = string.Empty;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		public string TestCodeTip
		{
			get
			{
				return this.mTestCodeTip;
			}
			set
			{
				this.mTestCodeTip = value;
				base.OnPropertyChanged<string>(() => this.TestCodeTip);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00021B0E File Offset: 0x0001FD0E
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00021B16 File Offset: 0x0001FD16
		public bool AutoLogin
		{
			get
			{
				return this.mAutoLogin;
			}
			set
			{
				this.mAutoLogin = value;
				base.OnPropertyChanged<bool>(() => this.AutoLogin);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00021B54 File Offset: 0x0001FD54
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00021B5C File Offset: 0x0001FD5C
		public string OrgIds
		{
			get
			{
				return this.mOrgIds;
			}
			set
			{
				this.mOrgIds = value;
				base.OnPropertyChanged<string>(() => this.OrgIds);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00021B9A File Offset: 0x0001FD9A
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00021BA2 File Offset: 0x0001FDA2
		public string SchoolId
		{
			get
			{
				return this.mSchoolId;
			}
			set
			{
				this.mSchoolId = value;
				base.OnPropertyChanged<string>(() => this.SchoolId);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		public string SchoolShowName
		{
			get
			{
				return this.mSchoolShowName;
			}
			set
			{
				this.mSchoolShowName = value;
				base.OnPropertyChanged<string>(() => this.SchoolShowName);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00021C26 File Offset: 0x0001FE26
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00021C30 File Offset: 0x0001FE30
		public string SchoolName
		{
			get
			{
				return this.mSchoolName;
			}
			set
			{
				this.mSchoolName = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.SchoolShowName = value;
				}
				base.OnPropertyChanged<string>(() => this.SchoolName);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00021C88 File Offset: 0x0001FE88
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00021C90 File Offset: 0x0001FE90
		public string UserId
		{
			get
			{
				return this.mUserId;
			}
			set
			{
				this.mUserId = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.UserIdTips = "";
				}
				base.OnPropertyChanged<string>(() => this.UserId);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00021CEC File Offset: 0x0001FEEC
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00021CF4 File Offset: 0x0001FEF4
		public string UserIdTips
		{
			get
			{
				return this.mUserIdTips;
			}
			set
			{
				this.mUserIdTips = value;
				base.OnPropertyChanged<string>(() => this.UserIdTips);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00021D32 File Offset: 0x0001FF32
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00021D3C File Offset: 0x0001FF3C
		public string Password
		{
			get
			{
				return this.mPassword;
			}
			set
			{
				this.mPassword = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.PasswordTips = "";
				}
				base.OnPropertyChanged<string>(() => this.Password);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00021D98 File Offset: 0x0001FF98
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		public string PasswordTips
		{
			get
			{
				return this.mPasswordTips;
			}
			set
			{
				this.mPasswordTips = value;
				base.OnPropertyChanged<string>(() => this.PasswordTips);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00021DDE File Offset: 0x0001FFDE
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00021DE6 File Offset: 0x0001FFE6
		public string UserName
		{
			get
			{
				return this.mUserName;
			}
			set
			{
				this.mUserName = value;
				base.OnPropertyChanged<string>(() => this.UserName);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00021E24 File Offset: 0x00020024
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00021E2C File Offset: 0x0002002C
		public string PhoneNumber
		{
			get
			{
				return this.mPhoneNumber;
			}
			set
			{
				this.mPhoneNumber = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.PhoneNumberTip = "";
				}
				base.OnPropertyChanged<string>(() => this.PhoneNumber);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00021E88 File Offset: 0x00020088
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00021E90 File Offset: 0x00020090
		public string PhoneNumberTip
		{
			get
			{
				return this.mPhoneNumberTipTip;
			}
			set
			{
				this.mPhoneNumberTipTip = value;
				base.OnPropertyChanged<string>(() => this.PhoneNumberTip);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00021ECE File Offset: 0x000200CE
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00021ED8 File Offset: 0x000200D8
		public string Code
		{
			get
			{
				return this.mCode;
			}
			set
			{
				this.mCode = value;
				if (!string.IsNullOrEmpty(value))
				{
					this.CodeTip = "";
				}
				base.OnPropertyChanged<string>(() => this.Code);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00021F34 File Offset: 0x00020134
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00021F3C File Offset: 0x0002013C
		public string CodeTip
		{
			get
			{
				return this.mCodeTip;
			}
			set
			{
				this.mCodeTip = value;
				base.OnPropertyChanged<string>(() => this.CodeTip);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00021F7A File Offset: 0x0002017A
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00021F82 File Offset: 0x00020182
		public BitmapImage QRImage
		{
			get
			{
				return this.mQRImage;
			}
			set
			{
				this.mQRImage = value;
				base.OnPropertyChanged<BitmapImage>(() => this.QRImage);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00021FC0 File Offset: 0x000201C0
		public Task<UserModel> GetUserInfoAsync()
		{
			LoginViewModel.<GetUserInfoAsync>d__240 <GetUserInfoAsync>d__;
			<GetUserInfoAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<UserModel>.Create();
			<GetUserInfoAsync>d__.<>4__this = this;
			<GetUserInfoAsync>d__.<>1__state = -1;
			<GetUserInfoAsync>d__.<>t__builder.Start<LoginViewModel.<GetUserInfoAsync>d__240>(ref <GetUserInfoAsync>d__);
			return <GetUserInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00022004 File Offset: 0x00020204
		private void SetCookie(IEnumerable<string> cookies)
		{
			if (cookies == null || cookies.Count<string>() == 0)
			{
				return;
			}
			CookieHelper.DeleteCefCookie("JSESSIONID");
			CookieHelper.DeleteCefCookie("GSID");
			CookieContainer cookieContainer = new CookieContainer();
			string ipFromString = this.GetIpFromString(ConfigManager.AppSettings["WebServerUrl"]);
			foreach (string text in cookies)
			{
				string[] array = text.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Count<string>() != 0)
				{
					string text2 = string.Empty;
					string text3 = string.Empty;
					string value = string.Empty;
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string[] array3 = array2[i].Split(new char[]
						{
							'='
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array3 != null && array3.Count<string>() == 2)
						{
							string text4 = array3[0];
							string a = (text4 != null) ? text4.Trim() : null;
							if (!(a == "JSESSIONID"))
							{
								if (!(a == "GSID"))
								{
									if (a == "Path")
									{
										text2 = array3[1];
									}
								}
								else
								{
									text3 = "GSID";
									value = array3[1];
								}
							}
							else
							{
								text3 = "JSESSIONID";
								value = array3[1];
							}
						}
					}
					if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(value))
					{
						Cookie cookie = new Cookie(text3, value, text2, ipFromString);
						cookieContainer.Add(cookie);
						CookieHelper.SetCefCookies(cookie);
						CefCookie cefCookie = new CefCookie();
						cefCookie.Name = cookie.Name;
						cefCookie.Value = cookie.Value;
						cefCookie.Path = cookie.Path;
						cefCookie.Domain = cookie.Domain;
						CookieHelper.mCefWebCookieVisitor.SetCefCookies(cefCookie);
					}
				}
			}
			CookieHelper.RefreshCookies();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000221F8 File Offset: 0x000203F8
		private HttpContent GetPostParameter()
		{
			string password = new RSA("-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----", true).Encode(this.Password);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			((IDictionary<string, string>)dictionary)["username"] = this.UserId;
			((IDictionary<string, string>)dictionary)["userpassword"] = this.PasswordBase64Encode(this.PasswordBase64Encode(password));
			((IDictionary<string, string>)dictionary)["client"] = "pc";
			((IDictionary<string, string>)dictionary)["pw"] = "1";
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00022270 File Offset: 0x00020470
		public Task<UserActivateModel> UpdateUserInfoAsync()
		{
			LoginViewModel.<UpdateUserInfoAsync>d__243 <UpdateUserInfoAsync>d__;
			<UpdateUserInfoAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<UserActivateModel>.Create();
			<UpdateUserInfoAsync>d__.<>4__this = this;
			<UpdateUserInfoAsync>d__.<>1__state = -1;
			<UpdateUserInfoAsync>d__.<>t__builder.Start<LoginViewModel.<UpdateUserInfoAsync>d__243>(ref <UpdateUserInfoAsync>d__);
			return <UpdateUserInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000222B4 File Offset: 0x000204B4
		private HttpContent GetUpdatePostParameter()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["name"] = this.UserName;
			dictionary["sex"] = this.SexId.ToString();
			dictionary["shenf"] = this.TypeId;
			dictionary["schoolId"] = this.SchoolId;
			dictionary["orgIds"] = this.OrgIds;
			if (this.Teacher)
			{
				dictionary["select_xd"] = this.SelectedXD.Id;
				string text = null;
				foreach (XKViewModel xkviewModel in this.XKList)
				{
					if (xkviewModel.Selected)
					{
						text = text + xkviewModel.Id + ",";
					}
				}
				text = text.TrimEnd(new char[]
				{
					','
				});
				dictionary["select_xk"] = text;
			}
			if (this.Student)
			{
				dictionary["select_nj"] = this.SelectedGrade.Id;
			}
			dictionary["img"] = this.UploadPhotoRelativeUrl;
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000223F0 File Offset: 0x000205F0
		public Task<List<UserGetVerificationCodeModel>> GetPhoneCodeAsync()
		{
			LoginViewModel.<GetPhoneCodeAsync>d__245 <GetPhoneCodeAsync>d__;
			<GetPhoneCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<List<UserGetVerificationCodeModel>>.Create();
			<GetPhoneCodeAsync>d__.<>4__this = this;
			<GetPhoneCodeAsync>d__.<>1__state = -1;
			<GetPhoneCodeAsync>d__.<>t__builder.Start<LoginViewModel.<GetPhoneCodeAsync>d__245>(ref <GetPhoneCodeAsync>d__);
			return <GetPhoneCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022433 File Offset: 0x00020633
		private HttpContent GetCodePostParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			((IDictionary<string, string>)dictionary)["iphone"] = this.PhoneNumber;
			((IDictionary<string, string>)dictionary)["flag"] = "bd";
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00022460 File Offset: 0x00020660
		public Task<UserBindingPhoneModel> BindingPhoneNubmerAsync()
		{
			LoginViewModel.<BindingPhoneNubmerAsync>d__247 <BindingPhoneNubmerAsync>d__;
			<BindingPhoneNubmerAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<UserBindingPhoneModel>.Create();
			<BindingPhoneNubmerAsync>d__.<>4__this = this;
			<BindingPhoneNubmerAsync>d__.<>1__state = -1;
			<BindingPhoneNubmerAsync>d__.<>t__builder.Start<LoginViewModel.<BindingPhoneNubmerAsync>d__247>(ref <BindingPhoneNubmerAsync>d__);
			return <BindingPhoneNubmerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000224A3 File Offset: 0x000206A3
		private HttpContent BindPhoneNumberPostParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			((IDictionary<string, string>)dictionary)["phone"] = this.PhoneNumber;
			((IDictionary<string, string>)dictionary)["code"] = this.Code;
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000224D4 File Offset: 0x000206D4
		public Task<UserGetBackCodeModel> GetBackCodeAsync()
		{
			LoginViewModel.<GetBackCodeAsync>d__249 <GetBackCodeAsync>d__;
			<GetBackCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<UserGetBackCodeModel>.Create();
			<GetBackCodeAsync>d__.<>4__this = this;
			<GetBackCodeAsync>d__.<>1__state = -1;
			<GetBackCodeAsync>d__.<>t__builder.Start<LoginViewModel.<GetBackCodeAsync>d__249>(ref <GetBackCodeAsync>d__);
			return <GetBackCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022517 File Offset: 0x00020717
		private HttpContent GetBackCodePostParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			((IDictionary<string, string>)dictionary)["reg_name"] = this.GetBackId;
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00022534 File Offset: 0x00020734
		public Task<ModifyPwdResultModel> ResetCodeAsync()
		{
			LoginViewModel.<ResetCodeAsync>d__251 <ResetCodeAsync>d__;
			<ResetCodeAsync>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<ModifyPwdResultModel>.Create();
			<ResetCodeAsync>d__.<>4__this = this;
			<ResetCodeAsync>d__.<>1__state = -1;
			<ResetCodeAsync>d__.<>t__builder.Start<LoginViewModel.<ResetCodeAsync>d__251>(ref <ResetCodeAsync>d__);
			return <ResetCodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00022578 File Offset: 0x00020778
		private HttpContent ResetCodePostParameter()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			((IDictionary<string, string>)dictionary)["phone"] = this.ResetPhone;
			((IDictionary<string, string>)dictionary)["code"] = this.VerificationCode;
			((IDictionary<string, string>)dictionary)["user_id"] = this.ResetUserID;
			((IDictionary<string, string>)dictionary)["random_code"] = this.ResetRandomCode;
			string value = new RSA("-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----", true).Encode(this.ResetCode);
			((IDictionary<string, string>)dictionary)["pwd"] = value;
			((IDictionary<string, string>)dictionary)["pw"] = "1";
			return new FormUrlEncodedContent(dictionary);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00022608 File Offset: 0x00020808
		public void InitData()
		{
			this.SexMan = true;
			this.Teacher = true;
			this.UploadPhotoVisibility = Visibility.Visible;
			this.LoginVisibility = Visibility.Visible;
			this.ResetVisibility = Visibility.Hidden;
			this.GetTestCodeVisibility = Visibility.Hidden;
			this.UserActivateVisibility = Visibility.Hidden;
			this.PhoneBindingVisibility = Visibility.Hidden;
			this.WarningVisibility = Visibility.Hidden;
			this.ShowContactCustomer = false;
			this.mUserInfo = null;
			this.ResetUserID = string.Empty;
			this.ResetRandomCode = string.Empty;
			this.ResetPhone = string.Empty;
			this.CurValidCode = string.Empty;
			this.UploadPhotoRelativeUrl = string.Empty;
			this.SexWoman = false;
			this.UserId = string.Empty;
			this.Password = string.Empty;
			this.UserIdTips = string.Empty;
			this.PasswordTips = string.Empty;
			this.PhoneNumber = string.Empty;
			this.Code = string.Empty;
			this.PhoneNumberTip = string.Empty;
			this.CodeTip = string.Empty;
			this.GetBackId = string.Empty;
			this.GetBackIdTip = string.Empty;
			this.TestCode = string.Empty;
			this.TestCodeTip = string.Empty;
			this.SendMessageTip = string.Empty;
			this.VerificationCode = string.Empty;
			this.VerificationCodeTip = string.Empty;
			this.ResetCode = string.Empty;
			this.ResetCodeTip = string.Empty;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002275C File Offset: 0x0002095C
		private void LoadData()
		{
			try
			{
				foreach (MetaModel metaModel in CommonHandle.GetConstDataReal(1009, "学科"))
				{
					XKViewModel xkviewModel = new XKViewModel();
					xkviewModel.Id = metaModel.Id;
					xkviewModel.Value = metaModel.Value;
					this.XKList.Add(xkviewModel);
				}
				foreach (MetaModel metaModel2 in CommonHandle.GetConstDataReal(1007, "学段"))
				{
					XDViewModel xdviewModel = new XDViewModel();
					xdviewModel.Id = metaModel2.Id;
					xdviewModel.Value = metaModel2.Value;
					this.XDList.Add(xdviewModel);
				}
				foreach (MetaModel metaModel3 in CommonHandle.GetConstDataReal(1008, "年级"))
				{
					GradeViewModel gradeViewModel = new GradeViewModel();
					gradeViewModel.Id = metaModel3.Id;
					gradeViewModel.Value = metaModel3.Value;
					this.GradeList.Add(gradeViewModel);
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "登录界面初始化加载数据失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00022914 File Offset: 0x00020B14
		private string GetIpFromString(string strings)
		{
			return new Uri(strings).Host;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00022924 File Offset: 0x00020B24
		private string PasswordBase64Encode(string password)
		{
			string result = string.Empty;
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			try
			{
				result = Convert.ToBase64String(bytes);
			}
			catch
			{
				result = password;
			}
			return result;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00022964 File Offset: 0x00020B64
		private void InitActiveData()
		{
			try
			{
				this.UploadPhotoLocalUrl = null;
				this.UserName = "";
				this.SchoolShowName = "点击选择学校名称";
				this.SchoolName = "";
				this.SexMan = true;
				this.SexWoman = false;
				this.Teacher = true;
				this.Student = false;
				this.SelectedXD = null;
				this.SelectedGrade = null;
				foreach (XKViewModel xkviewModel in (from t in this.XKList
				where t.Selected
				select t).ToList<XKViewModel>())
				{
					xkviewModel.Selected = false;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00022A54 File Offset: 0x00020C54
		public void LoadQRCode()
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					object obj = this.objlock;
					lock (obj)
					{
						this.mManualre.Reset();
						this.mIsDoWork = false;
						string value = string.Empty;
						try
						{
							value = this.GetQRToken(this.mQrCodeToken);
						}
						catch (Exception ex)
						{
							LogHelper instance = LogHelper.Instance;
							string str = "获取二维码token失败。";
							Exception ex2 = ex;
							instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
						}
						if (string.IsNullOrEmpty(value))
						{
							this.CreateErrorImage();
						}
						else
						{
							this.mQrCodeToken = value;
							this.CreateQRImage(this.mWxUrl);
							this.mManualre.Set();
							this.StartQRWork();
						}
					}
				}
				catch (Exception ex3)
				{
					LogHelper instance2 = LogHelper.Instance;
					string str2 = "加载二维码失败。";
					Exception ex4 = ex3;
					instance2.Error(str2 + ((ex4 != null) ? ex4.ToString() : null));
					this.CreateErrorImage();
				}
			});
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00022A68 File Offset: 0x00020C68
		private void StartQRWork()
		{
			this.mIsDoWork = true;
			if (this.mQrWorker == null)
			{
				this.mQrWorker = new BackgroundWorker();
				this.mQrWorker.WorkerSupportsCancellation = true;
				this.mQrWorker.DoWork += this.MQrWorker_DoWork;
				this.mQrWorker.RunWorkerCompleted += this.MQrWorker_RunWorkerCompleted;
				this.mQrWorker.RunWorkerAsync();
				return;
			}
			if (!this.mQrWorker.IsBusy)
			{
				this.mQrWorker.RunWorkerAsync();
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00022AF0 File Offset: 0x00020CF0
		private void MQrWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while (this.mIsDoWork)
			{
				this.mManualre.WaitOne();
				object obj = this.objlock;
				lock (obj)
				{
					Thread.Sleep(1000);
					QRLoginInfoModel qrcodeLoginStatus = this.GetQRCodeLoginStatus(this.mQrCodeToken);
					if (!this.mIsDoWork)
					{
						break;
					}
					if (qrcodeLoginStatus == null)
					{
						this.mIsDoWork = false;
						this.CreateErrorImage();
						break;
					}
					switch (qrcodeLoginStatus.State)
					{
					case 0:
						continue;
					case 1:
					{
						this.mIsDoWork = false;
						if (qrcodeLoginStatus.LstUserData == null || qrcodeLoginStatus.LstUserData.Count == 0)
						{
							this.CreateErrorImage();
							continue;
						}
						this.UserId = string.Empty;
						this.Password = string.Empty;
						LoginViewModel.CheckLoginUserData checkLoginUserDataCallBack = this.CheckLoginUserDataCallBack;
						if (checkLoginUserDataCallBack == null)
						{
							continue;
						}
						checkLoginUserDataCallBack(qrcodeLoginStatus.LstUserData[0]);
						continue;
					}
					case 2:
						if (this.mConfirmImage)
						{
							continue;
						}
						this.CreateConfirmImage();
						continue;
					}
					this.mIsDoWork = false;
					this.CreateErrorImage();
				}
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00022C1C File Offset: 0x00020E1C
		private void MQrWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.CreateErrorImage();
				LogHelper.Instance.Error("轮询二维码失败。" + e.Error.ToString());
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00022C4B File Offset: 0x00020E4B
		public void StopQRWork()
		{
			if (this.mQrWorker != null)
			{
				this.mIsDoWork = false;
				this.mQrWorker.CancelAsync();
				this.mQrWorker.Dispose();
				this.mQrWorker = null;
				this.QRImage = null;
				this.mConfirmImage = false;
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00022C88 File Offset: 0x00020E88
		private string GetQRToken(string oldToken)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = TimeSpan.FromSeconds(10.0);
			string requestUri = string.Empty;
			if (!string.IsNullOrEmpty(oldToken))
			{
				requestUri = ConfigHelper.WebServerUrl + "authqrcode/getToken.json?token=" + oldToken;
			}
			else
			{
				requestUri = ConfigHelper.WebServerUrl + "authqrcode/getToken.json";
			}
			HttpResponseMessage result = httpClient.GetAsync(requestUri).Result;
			if (!result.IsSuccessStatusCode)
			{
				return null;
			}
			QRTokenModel qrtokenModel = JsonConvert.DeserializeObject<QRTokenModel>(result.Content.ReadAsStringAsync().Result);
			if (qrtokenModel == null || qrtokenModel.Result != 110 || string.IsNullOrEmpty(qrtokenModel.Token))
			{
				LogHelper.Instance.Error(string.Format("二维码获取token失败。返回Code：[{0}],返回消息：[{1}]", (qrtokenModel != null) ? new int?(qrtokenModel.Result) : null, (qrtokenModel != null) ? qrtokenModel.Msg : null));
				return null;
			}
			IEnumerable<string> values = result.Headers.GetValues("Set-Cookie");
			this.SetCookie(values);
			this.mWxUrl = qrtokenModel.WxUrl;
			return qrtokenModel.Token;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00022D90 File Offset: 0x00020F90
		private QRLoginInfoModel GetQRCodeLoginStatus(string token)
		{
			QRLoginInfoModel result2;
			try
			{
				HttpClient httpClient = new HttpClient(new HttpClientHandler
				{
					CookieContainer = HttpHelper.GetCookieContainer(),
					AllowAutoRedirect = true,
					UseCookies = true
				});
				httpClient.Timeout = TimeSpan.FromSeconds(30.0);
				string requestUri = ConfigHelper.WebServerUrl + "p/user/AutoQRlogin";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["token"] = token;
				dictionary["client"] = "pc";
				HttpResponseMessage result = httpClient.PostAsync(requestUri, new FormUrlEncodedContent(dictionary)).Result;
				if (!result.IsSuccessStatusCode)
				{
					result2 = null;
				}
				else
				{
					string result3 = result.Content.ReadAsStringAsync().Result;
					QRLoginInfoModel qrloginInfoModel = JsonHelper.Instance.JsonsDeserialize<QRLoginInfoModel>(result3);
					if (qrloginInfoModel == null || qrloginInfoModel.Result != 110)
					{
						LogHelper.Instance.Error(string.Format("获取二维码状态失败，返回的Code：[{0}],返回消息：[{1}]。", (qrloginInfoModel != null) ? new int?(qrloginInfoModel.Result) : null, (qrloginInfoModel != null) ? qrloginInfoModel.Msg : null));
						result2 = null;
					}
					else
					{
						result2 = qrloginInfoModel;
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "获取二维码登录状态失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				result2 = null;
			}
			return result2;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00022EEC File Offset: 0x000210EC
		private void CreateQRImage(string token)
		{
			this.QRImage = null;
			BarcodeWriter barcodeWriter = new BarcodeWriter();
			barcodeWriter.Format = BarcodeFormat.QR_CODE;
			EncodingOptions encodingOptions = new EncodingOptions();
			encodingOptions.Height = 250;
			encodingOptions.Width = 250;
			encodingOptions.Margin = 0;
			encodingOptions.Hints[EncodeHintType.CHARACTER_SET] = "UTF-8";
			encodingOptions.Hints[EncodeHintType.MARGIN] = 2;
			barcodeWriter.Options = encodingOptions;
			Bitmap bitmap = barcodeWriter.Write(token);
			BitmapImage bitmapImage = new BitmapImage();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Bmp);
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();
			}
			this.QRImage = bitmapImage;
			this.mConfirmImage = false;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00022FC4 File Offset: 0x000211C4
		private void CreateErrorImage()
		{
			Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
			{
				string uriString = "pack://application:,,,/Resources/Images/qr_error.png";
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(uriString);
				bitmapImage.EndInit();
				this.QRImage = bitmapImage;
				this.mConfirmImage = false;
			}), new object[0]);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00022FE8 File Offset: 0x000211E8
		private void CreateConfirmImage()
		{
			Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
			{
				string uriString = "pack://application:,,,/Resources/Images/qr_confrim.png";
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(uriString);
				bitmapImage.EndInit();
				this.QRImage = bitmapImage;
				this.mConfirmImage = true;
			}), new object[0]);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002300C File Offset: 0x0002120C
		public string GetBookid()
		{
			HttpClient httpClient = new HttpClient(new HttpClientHandler
			{
				CookieContainer = HttpHelper.GetCookieContainer(),
				UseCookies = true
			});
			httpClient.Timeout = TimeSpan.FromSeconds(10.0);
			string requestUri = ConfigHelper.WebServerUrl + "p/textBook/initialDownloadTextbook.do";
			HttpResponseMessage result = httpClient.PostAsync(requestUri, null).Result;
			if (!result.IsSuccessStatusCode)
			{
				return null;
			}
			UserBookIdModel userBookIdModel = JsonConvert.DeserializeObject<UserBookIdModel>(result.Content.ReadAsStringAsync().Result);
			if (userBookIdModel == null || userBookIdModel.Result != "110")
			{
				return null;
			}
			return userBookIdModel.Tbid;
		}

		// Token: 0x04000324 RID: 804
		private string JSESSIONID;

		// Token: 0x04000325 RID: 805
		private string GSID;

		// Token: 0x04000326 RID: 806
		private BackgroundWorker mQrWorker;

		// Token: 0x04000327 RID: 807
		private bool mIsDoWork;

		// Token: 0x04000328 RID: 808
		private BitmapImage mQRImage;

		// Token: 0x04000329 RID: 809
		private string mQrCodeToken = string.Empty;

		// Token: 0x0400032A RID: 810
		private string mWxUrl = string.Empty;

		// Token: 0x0400032B RID: 811
		private ManualResetEvent mManualre = new ManualResetEvent(true);

		// Token: 0x0400032C RID: 812
		private object objlock = new object();

		// Token: 0x0400032D RID: 813
		private bool mConfirmImage;

		// Token: 0x0400032E RID: 814
		private const string PUBLICKEY = "-----BEGIN PUBLIC KEY-----\r\n        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDlKNohrEhgCHoEQIqGnN7LQynkB5TyjE4dp+72AhN0zHaSrfSjzkdXFh56LqopMAbHnK9uS6mPXongawAFmgfGzyvRwkm5Da1jagtNhf8BU2516GC7lncu74xUPAHmwFDVh/XNQaTU0hZpl9e4sh/Ux24o3i3ueD4JEszRowqYbQIDAQAB\r\n        -----END PUBLIC KEY-----";

		// Token: 0x0400032F RID: 815
		private UserModel mUserInfo;

		// Token: 0x04000336 RID: 822
		private ObservableCollection<XKViewModel> mXKList = new ObservableCollection<XKViewModel>();

		// Token: 0x04000337 RID: 823
		private ObservableCollection<XDViewModel> mXDList = new ObservableCollection<XDViewModel>();

		// Token: 0x04000338 RID: 824
		private XDViewModel mSelectedXD;

		// Token: 0x04000339 RID: 825
		private ObservableCollection<GradeViewModel> mGradeList = new ObservableCollection<GradeViewModel>();

		// Token: 0x0400033A RID: 826
		private GradeViewModel mSelectedGrade;

		// Token: 0x0400033B RID: 827
		private bool mSexMan;

		// Token: 0x0400033C RID: 828
		private bool mSexWoman;

		// Token: 0x0400033D RID: 829
		private int mSexId;

		// Token: 0x0400033E RID: 830
		private string mSexName;

		// Token: 0x0400033F RID: 831
		private bool mTeacher;

		// Token: 0x04000340 RID: 832
		private bool mStudent;

		// Token: 0x04000341 RID: 833
		private string mTypeId;

		// Token: 0x04000342 RID: 834
		private string mTypeName;

		// Token: 0x04000343 RID: 835
		private Visibility mResetVisibility = Visibility.Hidden;

		// Token: 0x04000344 RID: 836
		private Visibility mGetTestCodeVisibility = Visibility.Hidden;

		// Token: 0x04000345 RID: 837
		private Visibility mLoginVisibility;

		// Token: 0x04000346 RID: 838
		private bool mShowContactCustomer;

		// Token: 0x04000347 RID: 839
		private Visibility mUserActivateVisibility = Visibility.Hidden;

		// Token: 0x04000348 RID: 840
		private Visibility mXDVisibility;

		// Token: 0x04000349 RID: 841
		private Visibility mGradeVisibility;

		// Token: 0x0400034A RID: 842
		private Visibility mWarningVisibility = Visibility.Hidden;

		// Token: 0x0400034B RID: 843
		private Visibility mPhoneBindingVisibility = Visibility.Hidden;

		// Token: 0x0400034C RID: 844
		private Visibility mXKVisibility;

		// Token: 0x0400034D RID: 845
		private Visibility mUploadPhotoVisibility;

		// Token: 0x0400034E RID: 846
		private BitmapImage mUploadPhotoLocalUrl;

		// Token: 0x0400034F RID: 847
		private string mVerificationCode;

		// Token: 0x04000350 RID: 848
		private string mVerificationCodeTip;

		// Token: 0x04000351 RID: 849
		private string mSendMessageTip;

		// Token: 0x04000352 RID: 850
		private string mResetCode;

		// Token: 0x04000353 RID: 851
		private string mResetCodeAgain;

		// Token: 0x04000354 RID: 852
		private string mResetCodeTip;

		// Token: 0x04000355 RID: 853
		private string mGetBackId;

		// Token: 0x04000356 RID: 854
		private string mGetBackIdTip;

		// Token: 0x04000357 RID: 855
		private string mTestCode;

		// Token: 0x04000358 RID: 856
		private string mTestCodeTip;

		// Token: 0x04000359 RID: 857
		private bool mAutoLogin;

		// Token: 0x0400035A RID: 858
		private string mOrgIds = "";

		// Token: 0x0400035B RID: 859
		private string mSchoolId = "";

		// Token: 0x0400035C RID: 860
		private string mSchoolShowName = "点击选择学校名称";

		// Token: 0x0400035D RID: 861
		private string mSchoolName = "";

		// Token: 0x0400035E RID: 862
		private string mUserId = string.Empty;

		// Token: 0x0400035F RID: 863
		private string mUserIdTips;

		// Token: 0x04000360 RID: 864
		private string mPassword;

		// Token: 0x04000361 RID: 865
		private string mPasswordTips;

		// Token: 0x04000362 RID: 866
		private string mUserName;

		// Token: 0x04000363 RID: 867
		private string mPhoneNumber;

		// Token: 0x04000364 RID: 868
		private string mPhoneNumberTipTip;

		// Token: 0x04000365 RID: 869
		private string mCode;

		// Token: 0x04000366 RID: 870
		private string mCodeTip;

		// Token: 0x02000121 RID: 289
		// (Invoke) Token: 0x06000B22 RID: 2850
		public delegate void CheckLoginUserData(UserModel userData);
	}
}
