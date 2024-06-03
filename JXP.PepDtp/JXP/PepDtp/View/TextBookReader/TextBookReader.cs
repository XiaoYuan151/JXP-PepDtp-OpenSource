using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Gma.System.MouseKeyHook;
using JXP.Data;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.Enums;
using JXP.Logs;
using JXP.Models;
using JXP.Networks;
using JXP.PepData;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Threading;
using JXP.Utilities;
using JXP.Windows;
using Newtonsoft.Json;
using pep.Course.Commons;
using pep.Nobook;
using pep.Nobook.RemoteControl.Commands;
using pep.Nobook.Windows;
using pep.sdk.reader.TextbookUtils;
using pep.sdk.reader.View.Textbook;
using pep.sdk.utility.Common;
using pep.sdk.utility.Paramter;
using pep.sdk.utility.View;

namespace JXP.PepDtp.View.TextBookReader
{
	// Token: 0x0200004B RID: 75
	public class TextBookReader : BaseBookReader
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001AD35 File Offset: 0x00018F35
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0001AD3D File Offset: 0x00018F3D
		public Action<string, int> ClsoeBookCallBack { get; set; }

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001AD48 File Offset: 0x00018F48
		protected override ContentControl CreateTextbookController()
		{
			TextBookControllerControl textBookControllerControl = new TextBookControllerControl();
			this.mBookOperator = textBookControllerControl.TextBookOperator;
			this.mBookToolBar = textBookControllerControl.TextBookToolBar;
			this.InitTextbookToolBar();
			this.InitTextbookOperator();
			return textBookControllerControl;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001AD80 File Offset: 0x00018F80
		public override void CloseBook(bool bCloseInnerWin = true)
		{
			this.mShowNotDonwloadResInfoFlg = true;
			if (bCloseInnerWin)
			{
				this.CloseInnerReaderWindow();
				try
				{
					if (this.mWhiteBoardWin != null)
					{
						this.mWhiteBoardWin.CloseWin();
					}
				}
				catch
				{
				}
			}
			this.mBookToolBar.SetHotZoneBtnColor(false);
			this.mBookOperator.InitOperateState();
			this.SetResderToolState(true);
			string textbookID = base.TextbookID;
			int curPageNum = base.GetCurPageNum();
			if (this.mTextbookHelper != null)
			{
				int curPageNum2 = this.mTextbookHelper.GetCurPageNum();
				OperationBookHelper.SaveBookDataToOssAsync(base.TextbookID, curPageNum2);
			}
			base.CloseBook(bCloseInnerWin);
			Action<string, int> clsoeBookCallBack = this.ClsoeBookCallBack;
			if (clsoeBookCallBack == null)
			{
				return;
			}
			clsoeBookCallBack(textbookID, curPageNum);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001AE2C File Offset: 0x0001902C
		protected override void PdfSDK_OnDocumentClose(object sender, string e)
		{
			base.PdfSDK_OnDocumentClose(sender, e);
			BookTitleBar bookTitleBar = this.mBookTitleBar;
			if (bookTitleBar != null)
			{
				bookTitleBar.Close();
			}
			this.mBookTitleBar = null;
			this.mShowNotDonwloadResInfoFlg = true;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001AE55 File Offset: 0x00019055
		protected override void ResMarkViewer_DragEnter(object sender, DragEventArgs e)
		{
			this.HideInnerReaderWin();
			base.ResMarkViewer_DragEnter(sender, e);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001AE65 File Offset: 0x00019065
		protected override void InternalOnPageIndexChanged(bool closeRes = true)
		{
			base.InternalOnPageIndexChanged(closeRes);
			ThreadEx.Run(delegate()
			{
				try
				{
					if (CommonParamter.Instance.IsInClass)
					{
						List<string> item = this.GetCurChapterId().Item1;
						if (item != null && item.Count > 0)
						{
							PepClassChaptersInfo t = new PepClassChaptersInfo
							{
								BookId = base.TextbookID,
								ChapterId = item
							};
							string jsonData = this.mJsHelper.ScriptSerialize<PepClassChaptersInfo>(t);
							NetManager.Instance.SendPipeDataToClient(new PipeMessagePacket
							{
								Command = "chaptersinfo",
								JsonData = jsonData
							}, "");
						}
					}
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "翻页时给互动课堂发送消息失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			});
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001AE80 File Offset: 0x00019080
		private Tuple<List<string>, string> GetCurChapterId()
		{
			List<string> list = new List<string>();
			string text = string.Empty;
			int curPageNum = base.TextbookHelper.GetCurPageNum();
			if (curPageNum > 0)
			{
				Tuple<string, string, string> chapterInfo = this.mTextBookReaderVM.GetChapterInfo(base.TextbookID, curPageNum);
				if (chapterInfo != null)
				{
					list.AddRange(UtilityHelper.GetSubChapterLst(chapterInfo.Item1, base.TextbookID));
					text = chapterInfo.Item2;
				}
			}
			string text2 = string.Empty;
			if (base.IsDoubleSheet())
			{
				int doubleRightPageNum = base.TextbookHelper.GetDoubleRightPageNum();
				Tuple<string, string, string> chapterInfo2 = this.mTextBookReaderVM.GetChapterInfo(base.TextbookID, doubleRightPageNum);
				if (chapterInfo2 != null && !list.Contains(chapterInfo2.Item1))
				{
					list.AddRange(UtilityHelper.GetSubChapterLst(chapterInfo2.Item1, base.TextbookID));
					text2 = chapterInfo2.Item2;
				}
			}
			string item = text;
			if (!string.IsNullOrEmpty(text2))
			{
				item = text + "、" + text2;
			}
			return Tuple.Create<List<string>, string>(list, item);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001AF68 File Offset: 0x00019168
		protected override void TextbookOverlay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			base.TextbookOverlay_PreviewMouseDown(sender, e);
			Button button = VisualHelper.VisualUpwardSearch<Button>(e.OriginalSource as DependencyObject) as Button;
			if (button == null || button.Name != "imgBtnMyCenter")
			{
				this.HideInnerReaderWin();
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001AFB0 File Offset: 0x000191B0
		private void SetResderToolState(bool showFlg)
		{
			if (!CommonParamter.Instance.CourseIsOnline)
			{
				this.SetBtnEnable();
				return;
			}
			if (showFlg && CommonParamter.Instance.IsSeeMyBook)
			{
				if (!CommonParamter.Instance.IsInClass && !CommonParamter.Instance.IsHiddenBookOperator)
				{
					this.mBookOperator.Visibility = Visibility.Visible;
				}
				this.mBookToolBar.imgBtnSync.Visibility = Visibility.Visible;
				this.mBookToolBar.imgBtnNewResource.Visibility = Visibility.Visible;
				this.mBookToolBar.imgBtnMyCenter.Visibility = Visibility.Visible;
				return;
			}
			this.mBookOperator.Visibility = Visibility.Collapsed;
			this.mBookToolBar.imgBtnSync.Visibility = Visibility.Collapsed;
			this.mBookToolBar.imgBtnNewResource.Visibility = Visibility.Collapsed;
			this.mBookToolBar.imgBtnMyCenter.Visibility = Visibility.Collapsed;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001B078 File Offset: 0x00019278
		public void SetBtnEnable()
		{
			if (!CommonParamter.Instance.CourseIsOnline)
			{
				this.mBookToolBar.imgBtnSyncList.Visibility = Visibility.Collapsed;
				this.mBookToolBar.imgBtnSync.Visibility = Visibility.Collapsed;
				this.mBookToolBar.imgBtnNewResource.Visibility = Visibility.Collapsed;
				this.mBookToolBar.imgBtnMyCenter.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001B0D5 File Offset: 0x000192D5
		public void ReaderStartClass()
		{
			this.ucReader.SetHandTool();
			CommonParamter.Instance.IsInClass = true;
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.mBookOperator.Visibility = Visibility.Collapsed;
			}), new object[0]);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001B10B File Offset: 0x0001930B
		public void ReaderEndClass()
		{
			CommonParamter.Instance.IsInClass = false;
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (CommonParamter.Instance.IsSeeMyBook && !CommonParamter.Instance.IsHiddenBookOperator)
				{
					this.mBookOperator.Visibility = Visibility.Visible;
				}
			}), new object[0]);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B138 File Offset: 0x00019338
		public static OnlineResModel GetResInfo(string resId)
		{
			string strUrl = ConfigHelper.WebServerUrl + "resource/yunRes.json";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["id"] = resId;
			string text = HttpHelper.HttpPost(strUrl, dictionary, new int?(30000), "", true);
			OnlineResResultModel onlineResResultModel = JsonConvert.DeserializeObject<OnlineResResultModel>((text != null) ? text.Replace("\"id\"", "\"resid\"") : null);
			if (onlineResResultModel != null && onlineResResultModel.ResultCode == 110)
			{
				return onlineResResultModel.ResInfo;
			}
			return null;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B1B0 File Offset: 0x000193B0
		public override void OpenUserRes(string id, Window owner = null)
		{
			try
			{
				UserResourceModel userRes = base.GetUserRes(id);
				if (userRes == null)
				{
					CustomMessageBox.Info("用户资源不存在!", "确定", "", (owner == null) ? this : owner, WindowStartupLocation.CenterOwner, false);
				}
				else
				{
					OpenResParamter openResPara = new OpenResParamter
					{
						IsShowSaveBtn = false,
						IsShowPlayer = true,
						IsEditeMode = false,
						IsCloseWhenSave = false,
						OfficeFileLocal = false,
						CloseWhenPlayCompleted = false,
						PlayWhenLoaded = true,
						ShowResInfo = false,
						IsNqRes = false,
						GgbDataSaveCallBack = null,
						ClassActivityDataSaveCallBack = null,
						ClassActivityInfo = null,
						DicTurnPage = null,
						TurnPageCallBack = null,
						NqRes = null,
						UserRes = userRes,
						ResId = userRes.ID,
						SourceType = ResSourceType.MyRes,
						OwnerWin = ((owner == null) ? this : owner)
					};
					PlayResHelper.Instance.OpenUserRes(openResPara);
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("用户资源锚点打开失败:[{0}]。", arg));
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001B2B8 File Offset: 0x000194B8
		protected override void OpenNQRes(AnnotContentModel annotContent, bool isClosePicView = false)
		{
			if (annotContent == null)
			{
				return;
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200305",
				Passive = annotContent.Id,
				FromPos = TextBookReader.mClassPath + ".OpenNQRes",
				Params = "教材id:" + base.TextbookID
			});
			ResourcesModel resInfoByResId = this.mResourcesDataAccess.GetResInfoByResId(annotContent.Id);
			string extension = Path.GetExtension(annotContent.FilePath);
			string text = (extension != null) ? extension.ToLowerInvariant() : null;
			SortedDictionary<int, int> sortedDictionary = null;
			bool flag = true;
			if (FileFormat.lstFormateSound.Contains(text.ToLower()))
			{
				int pageNum = annotContent.PageNum;
				sortedDictionary = new SortedDictionary<int, int>();
				if (!string.IsNullOrEmpty(annotContent.TurnPageInfo) && base.DicPageNumByIndex.ContainsKey(pageNum))
				{
					int num = base.DicPageNumByIndex[pageNum];
					string[] array = annotContent.TurnPageInfo.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						int key;
						if (int.TryParse(array[i], out key))
						{
							num = (sortedDictionary[key] = num + 1);
						}
					}
				}
				if (annotContent.ResType == 2)
				{
					flag = false;
				}
			}
			OpenResParamter openResPara = new OpenResParamter
			{
				IsShowPlayer = flag,
				CloseWhenPlayCompleted = !flag,
				ShowResInfo = false,
				IsNqRes = true,
				DicTurnPage = sortedDictionary,
				TurnPageCallBack = new Action<int>(base.TurnPage),
				ResId = annotContent.Id,
				SourceType = ResSourceType.SyncRes,
				OwnerWin = this
			};
			PlayResHelper.Instance.OpenSyncRes(resInfoByResId, openResPara, ref this.mShowNotDonwloadResInfoFlg);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001B458 File Offset: 0x00019658
		public override void OpenNQCharacterCard(AnnotContentModel annotContent, string cardId = "", bool isPopMenuRes = false)
		{
			if (annotContent == null)
			{
				return;
			}
			TrackerManager.Tracker.OnEvent(new EventActivity
			{
				ActionId = "jx200305",
				Passive = annotContent.Id,
				FromPos = TextBookReader.mClassPath + ".OpenNQCharacterCard",
				Params = "教材id:" + base.TextbookID
			});
			this.NotifyPreviewCount("1330", annotContent.Id);
			ResourcesModel resInfoByResId = this.mResourcesDataAccess.GetResInfoByResId(annotContent.Id);
			OpenResParamter openResPara = new OpenResParamter
			{
				ShowResInfo = false,
				IsNqRes = true,
				ResId = annotContent.Id,
				SourceType = ResSourceType.SyncRes,
				OwnerWin = this
			};
			PlayResHelper.Instance.OpenSyncRes(resInfoByResId, openResPara, ref this.mShowNotDonwloadResInfoFlg);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001B51D File Offset: 0x0001971D
		public override void OpenNQCharacterCard(string character)
		{
			PlayResHelper.Instance.ChaneseCardResNavigate(character);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001B52A File Offset: 0x0001972A
		public override void NotifyPreviewCount(string type, string resId)
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					HttpHelper.HttpGet(string.Concat(new string[]
					{
						ConfigHelper.WebServerUrl,
						"statistic/countOpen.json?type=",
						type,
						"&id=",
						resId,
						"&num=1&userid=",
						CommonParamter.Instance.LoginUserId
					}), null, true, "");
				}
				catch (Exception ex)
				{
					LogHelper instance = LogHelper.Instance;
					string str = "阅读内页调用平台预览次数接口失败。";
					Exception ex2 = ex;
					instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			});
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001B550 File Offset: 0x00019750
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001B558 File Offset: 0x00019758
		public new SortMyTextBook SortMyTextBookCallBack { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001B561 File Offset: 0x00019761
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001B569 File Offset: 0x00019769
		public new UpdateMyTextBookOpenTime UpdateMyTextBookOpenTimeCallBack { get; set; }

		// Token: 0x060004CA RID: 1226 RVA: 0x0001B574 File Offset: 0x00019774
		public void OpenTextBook(string bookId, string userId, List<UserResourceModel> userResList = null, Window owner = null, bool isOpenBookPage = false)
		{
			BaseBookReader instance = BaseBookReader.Instance;
			instance.TextbookID = bookId;
			instance.UserId = userId;
			instance.SortMyTextBookCallBack = this.SortMyTextBookCallBack;
			instance.UpdateMyTextBookOpenTimeCallBack = this.UpdateMyTextBookOpenTimeCallBack;
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				this.mBookToolBar.SetHotZoneBtnColor(false);
			}), new object[0]);
			instance.ShowBookWinLimit(userResList, false, isOpenBookPage);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B5D8 File Offset: 0x000197D8
		protected override void SetUserOperationHistory(string bookId, string userId)
		{
			base.SetUserOperationHistory(bookId, userId);
			this.mBookToolBar.SetPageLayoutBtnText(CommonParamter.Instance.IsDoubleSheet);
			OpenBookHistoryModel openBookHistoryInfo = new OpenBookHistoryDataAccess().GetOpenBookHistoryInfo(bookId, userId);
			if (openBookHistoryInfo == null || openBookHistoryInfo.PageNum == 0)
			{
				int cloudBookPageNum = this.GetCloudBookPageNum(bookId);
				if (cloudBookPageNum > 0)
				{
					base.GotoPageByPageNum(cloudBookPageNum);
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001B62D File Offset: 0x0001982D
		protected override void SetOtherInfo(string bookId, bool isSubscribe = false)
		{
			if (isSubscribe)
			{
				this.mDicPageResourceDownloadStatus.Clear();
				this.SetSubscibeInfo();
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001B643 File Offset: 0x00019843
		private void SetSubscibeInfo()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (this.mBookTitleBar == null)
				{
					this.mBookTitleBar = new BookTitleBar();
					this.mBookTitleBar.Owner = this;
					this.mBookTitleBar.PlacementTarget = this;
				}
				string info = string.Format("正在阅读：" + CommonParamter.Instance.CurrentSubscribedName + "老师的教材", new object[0]);
				this.mBookTitleBar.SetInfo(info);
				this.mBookTitleBar.Show();
			}), new object[0]);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001B664 File Offset: 0x00019864
		private void GetTextBookResourceDownloadStatus(string strBookID)
		{
			try
			{
				this.mDicPageResourceDownloadStatus.Clear();
				if (base.DicPageNumByIndex != null && base.DicPageNumByIndex.Count != 0)
				{
					ResourcesModelList resourcesInfoByTbid = this.mResDA.GetResourcesInfoByTbid(strBookID);
					if (resourcesInfoByTbid != null && resourcesInfoByTbid.Count != 0)
					{
						this.mDicPageResourceDownloadStatus = base.DicPageNumByIndex.ToDictionary((KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> y) => -1);
						foreach (ResourcesModel resourcesModel in resourcesInfoByTbid)
						{
							if (this.mDicPageResourceDownloadStatus.ContainsKey(resourcesModel.PageNum))
							{
								this.mDicPageResourceDownloadStatus[resourcesModel.PageNum] = 0;
							}
						}
						ChaptersModelList chaptersByTbId = new ChaptersDataAccess().GetChaptersByTbId(strBookID);
						if (chaptersByTbId != null && chaptersByTbId.Count != 0)
						{
							TransferChapterModelList transedChapters = new TransferChaptersDataAccess().GetTransedChapters(strBookID);
							if (transedChapters != null && transedChapters.Count != 0)
							{
								IDictionary<string, int> dictionary = transedChapters.ToDictionary((TransferChapterModel x) => x.ChapterId, (TransferChapterModel y) => y.TransState);
								int nStartPageNum = -1;
								int nEndPageNum = -1;
								using (Dictionary<int, int>.KeyCollection.Enumerator enumerator2 = base.DicPageNumByIndex.Keys.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										int tmpPageNum = enumerator2.Current;
										ChaptersModel chaptersModel = (from x in chaptersByTbId
										where int.TryParse(x.StartIndex, out nStartPageNum) && nStartPageNum <= tmpPageNum && int.TryParse(x.EndIndex, out nEndPageNum) && nEndPageNum >= tmpPageNum
										orderby x.Level descending
										select x).FirstOrDefault<ChaptersModel>();
										if (chaptersModel != null)
										{
											int value = -1;
											if (dictionary.TryGetValue(chaptersModel.ID, out value))
											{
												this.mDicPageResourceDownloadStatus[tmpPageNum] = value;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.mDicPageResourceDownloadStatus.Clear();
				LogHelper.Instance.Error(ex.ToString());
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001B914 File Offset: 0x00019B14
		private int GetCloudBookPageNum(string bookid)
		{
			int result;
			try
			{
				string strUrl = ConfigHelper.WebServerUrl + "statistic/queryActiveDetail.json";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["action_title"] = "jx200701";
				dictionary["passive_obj"] = bookid;
				DtpBehaviorResultModel dtpBehaviorResultModel = JsonConvert.DeserializeObject<DtpBehaviorResultModel>(HttpHelper.HttpPost(strUrl, dictionary, new int?(4000), "", true));
				if (dtpBehaviorResultModel == null || dtpBehaviorResultModel.State != 110)
				{
					result = -1;
				}
				else
				{
					string[] array;
					if (dtpBehaviorResultModel == null)
					{
						array = null;
					}
					else
					{
						DtpBehaviorModel data = dtpBehaviorResultModel.Data;
						if (data == null)
						{
							array = null;
						}
						else
						{
							string passiveObj = data.PassiveObj;
							array = ((passiveObj != null) ? passiveObj.Split(new char[]
							{
								','
							}) : null);
						}
					}
					string[] array2 = array;
					int num;
					if (array2 != null && array2.Length > 1 && int.TryParse(array2[1], out num))
					{
						result = num;
					}
					else
					{
						result = -1;
					}
				}
			}
			catch (Exception)
			{
				LogHelper.Instance.Error("获取服务端教材id:[" + bookid + "],教材关闭时页码失败。");
				result = -1;
			}
			return result;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001BA0C File Offset: 0x00019C0C
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0001BA14 File Offset: 0x00019C14
		public StartClass StartClassCallback { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001BA1D File Offset: 0x00019C1D
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0001BA25 File Offset: 0x00019C25
		public OpenAppCenterWindow OpenAppCenterWindowCallBack { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001BA2E File Offset: 0x00019C2E
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0001BA36 File Offset: 0x00019C36
		public Action<string, AppCenterModel, bool, Window, string, bool> OpenSubjectToolCallBack { get; set; }

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001BA40 File Offset: 0x00019C40
		private void InitTextbookOperator()
		{
			this.mBookOperator.SelectPainBrushInfo -= this.Textbook_SetPainBrushInfo;
			this.mBookOperator.SelectPainBrushInfo += this.Textbook_SetPainBrushInfo;
			this.mBookOperator.MenueOperatorClick -= this.Textbook_MenueOperatorClick;
			this.mBookOperator.MenueOperatorClick += this.Textbook_MenueOperatorClick;
			this.mBookOperator.ApplyToolClicked -= this.BookOperator_ApplyToolClicked;
			this.mBookOperator.ApplyToolClicked += this.BookOperator_ApplyToolClicked;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001BADC File Offset: 0x00019CDC
		protected override void ToolClicked()
		{
			try
			{
				string xd = string.Empty;
				string xk = string.Empty;
				if (!string.IsNullOrEmpty(base.TextbookID) && base.TextbookID.Length >= 13)
				{
					xd = base.TextbookID.Substring(1, 1);
					xk = base.TextbookID.Substring(2, 2);
				}
				this.mBookOperator.GetMyToolData(CommonParamter.Instance.LoginUserId, xd, xk, new Func<List<AppToolModel>>(this.GetOnlineAppTool));
				this.mBookOperator.SetToolPopIsOpen(true);
			}
			catch (Exception ex)
			{
				LogHelper instance = LogHelper.Instance;
				string str = "点击学科工具处理失败。";
				Exception ex2 = ex;
				instance.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001BB94 File Offset: 0x00019D94
		public List<AppToolModel> GetOnlineAppTool()
		{
			List<AppToolModel> result;
			try
			{
				string jsonStr = HttpHelper.HttpGet(SdkConstants.ToolOnlineUrl + "/pub_cloud/110/" + SdkConstants.ToolJsonName, new int?(5000), true, "");
				AppOnlineListResult appOnlineListResult = JsonHelper.Instance.JsonsDeserialize<AppOnlineListResult>(jsonStr);
				List<AppToolModel> list = new List<AppToolModel>();
				foreach (AppToolModel appToolModel in appOnlineListResult.LstData)
				{
					if (!(appToolModel.Zxxkc == "0") && appToolModel.OnlineTool == 1 && appToolModel.LstProductIds.Contains(CommonParamter.Instance.ProductId))
					{
						list.Add(appToolModel);
					}
				}
				result = list;
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("获取在线应用工具失败：[{0}]。", arg));
				result = null;
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001BC84 File Offset: 0x00019E84
		private void BookOperator_ApplyToolClicked(string applyId, AppToolModel toolInfo)
		{
			Action<string, AppCenterModel, bool, Window, string, bool> openSubjectToolCallBack = this.OpenSubjectToolCallBack;
			if (openSubjectToolCallBack == null)
			{
				return;
			}
			openSubjectToolCallBack(applyId, toolInfo, true, this, base.TextbookID, true);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		protected override void OpenScreencast()
		{
			try
			{
				ExecuteRemoteCommand.StartRemoteWithCourse(this, RemoteFrom.Book, true);
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("打开投屏工具失败:[{0}]。", arg));
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001BCE4 File Offset: 0x00019EE4
		protected override void StartClass()
		{
			int curPageNum = this.mTextbookHelper.GetCurPageNum();
			if (curPageNum > 0)
			{
				Tuple<string, string, string> chapterInfo = this.mTextBookReaderVM.GetChapterInfo(base.TextbookID, curPageNum);
				if (chapterInfo != null)
				{
					StartClass startClassCallback = this.StartClassCallback;
					if (startClassCallback == null)
					{
						return;
					}
					startClassCallback(base.TextbookID, chapterInfo.Item1, chapterInfo.Item2);
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001BD3C File Offset: 0x00019F3C
		public void ShowResNotDownloadPop()
		{
			this.ResetResNotDownloadAnimation();
			this.HideResNotDownloadPop();
			if (!base.ResNotDownloadPop.IsOpen)
			{
				base.ResNotDownloadPop.IsOpen = true;
			}
			this.storyResNotDownload.Begin(base.ResNotDownloadPop.Child as FrameworkElement);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001BD89 File Offset: 0x00019F89
		public void HideResNotDownloadPop()
		{
			this.HidePop(base.ResNotDownloadPop);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001BD97 File Offset: 0x00019F97
		protected override void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.HideResNotDownloadPop();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001BD9F File Offset: 0x00019F9F
		protected override void btnGotoDownload_Click(object sender, RoutedEventArgs e)
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				try
				{
					this.TextbookOverlay_OnExit(null, null);
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("跳转到下载页面时发生错误.[Reason:{0}]", arg));
				}
			}), new object[0]);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		private void ResetResNotDownloadAnimation()
		{
			if (this.storyResNotDownload != null)
			{
				this.storyResNotDownload.Completed -= this.StoryResNotDownloadOnCompleted;
				this.storyResNotDownload.Stop();
			}
			else
			{
				this.storyResNotDownload = this.GetResNotDownloadPopupAnimation();
			}
			this.storyResNotDownload.Completed += this.StoryResNotDownloadOnCompleted;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001BD97 File Offset: 0x00019F97
		private void StoryResNotDownloadOnCompleted(object sender, EventArgs eventArgs)
		{
			this.HideResNotDownloadPop();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001BE1C File Offset: 0x0001A01C
		private Storyboard GetResNotDownloadPopupAnimation()
		{
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(2.0))
			{
				BeginTime = new TimeSpan?(TimeSpan.FromSeconds(0.0))
			};
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.Opacity)", new object[0]));
			storyboard.Children.Add(doubleAnimation);
			DoubleAnimation doubleAnimation2 = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(2.0))
			{
				BeginTime = new TimeSpan?(TimeSpan.FromSeconds(3.0))
			};
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("(UIElement.Opacity)", new object[0]));
			storyboard.Children.Add(doubleAnimation2);
			return storyboard;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		private void HidePop(Popup popup)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.Invoke(new Action(delegate()
				{
					this.HidePop(popup);
				}), new object[0]);
				return;
			}
			if (popup.IsOpen)
			{
				popup.IsOpen = false;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001BF64 File Offset: 0x0001A164
		public void InitNotDownloadResPop(Popup popup)
		{
			Window currentWindow = Window.GetWindow(popup);
			if (currentWindow != null)
			{
				currentWindow.LocationChanged += delegate(object innerSender, EventArgs innerArgs)
				{
					this.RedrawPopup(popup);
				};
				currentWindow.SizeChanged += delegate(object innerSender, SizeChangedEventArgs innerArgs)
				{
					this.RedrawPopup(popup);
				};
				currentWindow.Activated += delegate(object innerSender, EventArgs innerArgs)
				{
				};
				currentWindow.Deactivated += delegate(object innerSender, EventArgs innerArgs)
				{
					if (!currentWindow.IsVisible)
					{
						this.HidePop(popup);
					}
				};
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001C010 File Offset: 0x0001A210
		private void RedrawPopup(Popup popup)
		{
			double horizontalOffset = popup.HorizontalOffset;
			popup.HorizontalOffset = horizontalOffset + 1.0;
			popup.HorizontalOffset = horizontalOffset;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001C03C File Offset: 0x0001A23C
		private void SetResNotDownloadPopStatus()
		{
			if (!CommonParamter.Instance.CourseIsOnline)
			{
				return;
			}
			int curPageNum = this.mTextbookHelper.GetCurPageNum();
			int doubleRightPageNum = this.mTextbookHelper.GetDoubleRightPageNum();
			if (this.GetSingPageResDownloadStatus(curPageNum) == 0 || this.GetSingPageResDownloadStatus(doubleRightPageNum) == 0)
			{
				this.ShowResNotDownloadPop();
				return;
			}
			this.HideResNotDownloadPop();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001C090 File Offset: 0x0001A290
		private int GetSingPageResDownloadStatus(int pageNum)
		{
			int result = -1;
			if (!this.mDicPageResourceDownloadStatus.TryGetValue(pageNum, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001C0B2 File Offset: 0x0001A2B2
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0001C0BA File Offset: 0x0001A2BA
		public TextBookReader.NavigateToMenue NavigateToMenueCallBack { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001C0C3 File Offset: 0x0001A2C3
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		public TextBookReader.ExitReaderSyncData ExitReaderSyncDataCallBack { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public Action<ClassActivityInfoModel, UserResourceModel, Window> SaveCallBack { get; set; }

		// Token: 0x060004EE RID: 1262 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		private void InitTextbookToolBar()
		{
			this.mBookToolBar.OnShowOrHideAnnots += this.BookToolBar_OnShowOrHideAnnots;
			this.mBookToolBar.OnShowOrHideHotZone += this.BookToolBar_OnShowOrHideHotZone;
			this.mBookToolBar.OnSync += base.Textbook_OnSync;
			this.mBookToolBar.OnOutputUserData += this.BookToolBar_OnOutputUserData;
			this.mBookToolBar.OnInputUserData += this.BookToolBar_OnInputUserData;
			this.mBookToolBar.OnBookShare += this.BookToolBar_OnBookShare;
			this.mBookToolBar.OnShowSyncList += base.Textbook_OnShowSyncList;
			this.mBookToolBar.OnCreateRes += base.Textbook_OnCreateRes;
			this.mBookToolBar.OnZoomIn += base.Textbook_OnZoomIn;
			this.mBookToolBar.OnZoomOut += base.Textbook_OnZoomOut;
			this.mBookToolBar.OnZoomInPop += this.MBookToolBar_OnZoomInPop;
			this.mBookToolBar.OnZoomRevert += base.MBookThumbWin_OnRestoreClick;
			base.GetBookZoomCallback = new Action<int, int, int>(this.mBookToolBar.SetZommValue);
			this.mBookToolBar.SetZoomLevel = delegate(int zoomlevel)
			{
				this.ucReader.SetZoom(zoomlevel);
			};
			this.mBookToolBar.OnChangePageLayout += this.BookToolBar_OnChangePageLayout;
			this.mBookToolBar.OnOpShowMyCenter += this.BookToolBar_OnOpShowMyCenter;
			this.mBookToolBar.OnShowChapterList += base.Textbook_OnShowChapterList;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001C27C File Offset: 0x0001A47C
		private void MBookToolBar_OnZoomInPop(object sender, EventArgs e)
		{
			Action<int, int, int> getBookZoomCallback = base.GetBookZoomCallback;
			if (getBookZoomCallback == null)
			{
				return;
			}
			getBookZoomCallback(this.ucReader.GetZoomLevel(), base.InitZoomLevel, base.InitZoomLevel * SdkConstants.BookMaxFactor);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		private void BookToolBar_OnShowOrHideAnnots(object sender, EventArgs e)
		{
			this.Textbook_OnShowOrHideAnnots();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001C2B3 File Offset: 0x0001A4B3
		protected override void Textbook_OnShowOrHideAnnots()
		{
			base.Textbook_OnShowOrHideAnnots();
			this.mBookToolBar.SetResSwitchBtnText(CommonParamter.Instance.ResSwitch);
			this.SetResderToolState(CommonParamter.Instance.ResSwitch);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		private void BookToolBar_OnShowOrHideHotZone(object sender, EventArgs e)
		{
			base.Textbook_OnShowOrHideHotZone(sender, e);
			this.mBookToolBar.SetHotZoneBtnColor(CommonParamter.Instance.HotZoneSwitch);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		private void BookToolBar_OnOutputUserData(object sender, EventArgs e)
		{
			base.Textbook_OnExportBookRes();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001C307 File Offset: 0x0001A507
		private void BookToolBar_OnInputUserData(object sender, EventArgs e)
		{
			base.Textbook_OnImportBookRes();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void BookToolBar_OnBookShare(object sender, EventArgs e)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00005BAA File Offset: 0x00003DAA
		private void CloseShareBook()
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001C30F File Offset: 0x0001A50F
		private void BookToolBar_OnChangePageLayout(object sender, EventArgs e)
		{
			if (base.Textbook_OnChangePageLayout() == TextbookPageLayout.Double)
			{
				this.mBookToolBar.SetPageLayoutBtnText(true);
				return;
			}
			this.mBookToolBar.SetPageLayoutBtnText(false);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001C333 File Offset: 0x0001A533
		protected override void PDFOnClick()
		{
			base.PDFOnClick();
			this.HideInnerReaderWin();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001C344 File Offset: 0x0001A544
		private void BookToolBar_OnOpShowMyCenter(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsOnline)
				{
					base.ShowToastInfo(new ToastInfo
					{
						Content = "当前网络状态不可用！",
						IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
					});
				}
				else if (!CommonParamter.Instance.ResSwitch)
				{
					base.ShowToastInfo(new ToastInfo
					{
						Content = "资源开关已经关闭，请先打开资源开关！",
						IconType = new ToastMessageIconType?(ToastMessageIconType.Warn)
					});
				}
				else
				{
					if (this.mInnerReaderWindow == null)
					{
						this.InitInnerReaderWindow();
						this.AutoSetInnerReaderWindowPos();
						this.ShowInnerReaderWindow();
						TextBookInnerResWindow textBookInnerResWindow = this.mInnerReaderWindow;
						if (textBookInnerResWindow != null)
						{
							textBookInnerResWindow.InitData();
						}
					}
					else
					{
						this.AutoSetInnerReaderWindowPos();
						if (this.mInnerReaderWindow.Visibility != Visibility.Visible)
						{
							this.ShowInnerReaderWindow();
							this.mInnerReaderWindow.InitData();
						}
						else
						{
							this.HideInnerReaderWin();
						}
					}
					TrackerManager.Tracker.OnEvent(new EventActivity
					{
						ActionId = "jx200106",
						Passive = "客户端",
						FromPos = TextBookReader.mClassPath + ".BookToolBar_OnOpShowMyCenter"
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("点击阅读内页我的资源中心出错！" + ex.ToString());
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001C478 File Offset: 0x0001A678
		private void AutoSetInnerReaderWindowPos()
		{
			if (this.mInnerReaderWindow != null)
			{
				Point point = this.mBookToolBar.imgBtnMyCenter.PointToScreen(new Point(0.0, 0.0));
				Point point2 = VisualHelper.GetTransformFromDevice(this).Transform(point);
				this.mInnerReaderWindow.Left = point2.X - this.mInnerReaderWindow.Width / 2.0 + 30.0;
				this.mInnerReaderWindow.Top = point2.Y - this.mInnerReaderWindow.Height + 5.0;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001C522 File Offset: 0x0001A722
		private void InitInnerReaderWindow()
		{
			if (this.mInnerReaderWindow != null)
			{
				return;
			}
			this.mInnerReaderWindow = new TextBookInnerResWindow();
			this.mInnerReaderWindow.ucMyRes.SaveCallBack = this.SaveCallBack;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001C54E File Offset: 0x0001A74E
		private void CloseInnerReaderWindow()
		{
			TextBookInnerResWindow textBookInnerResWindow = this.mInnerReaderWindow;
			if (textBookInnerResWindow != null)
			{
				textBookInnerResWindow.Close();
			}
			this.mInnerReaderWindow = null;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001C568 File Offset: 0x0001A768
		private void ShowInnerReaderWindow()
		{
			if (this.mInnerReaderWindow != null)
			{
				this.mInnerReaderWindow.TextBookID = base.TextbookID;
				Tuple<List<string>, string> curChapterId = this.GetCurChapterId();
				this.mInnerReaderWindow.LstChapterId = curChapterId.Item1;
				this.mInnerReaderWindow.TitleName = curChapterId.Item2;
				this.mInnerReaderWindow.Owner = this;
				this.mInnerReaderWindow.WindowState = WindowState.Normal;
				this.mInnerReaderWindow.Show();
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001C5DA File Offset: 0x0001A7DA
		private void HideInnerReaderWin()
		{
			base.Dispatcher.BeginInvoke(new Action(delegate()
			{
				if (this.mInnerReaderWindow == null)
				{
					return;
				}
				this.mInnerReaderWindow.Owner = null;
				this.mInnerReaderWindow.Hide();
			}), new object[0]);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001C5FC File Offset: 0x0001A7FC
		internal void OpenMySubscribe(string bookId, string userid, string path)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.HideInnerReaderWin();
			}), new object[0]);
			if (userid == CommonParamter.Instance.LoginUserId)
			{
				CommonParamter.Instance.IsSeeMyBook = true;
				base.Dispatcher.BeginInvoke(new Action(delegate()
				{
					this.HideInnerReaderWin();
					this.CloseBook(true);
					CommonParamter.Instance.CurrentUserId = CommonParamter.Instance.LoginUserId;
					this.SetResderToolState(true);
					this.OpenTextBook(bookId, CommonParamter.Instance.CurrentUserId, null, null, false);
				}), new object[0]);
				return;
			}
			try
			{
				string webServerUrl = ConfigHelper.WebServerUrl;
				string jsonStr = WebRequestHelper.HttpGetRequest(string.Format("resuser/getDYResList.json?userId={0}&tbId={1}", userid, bookId), new int?(30000), 0, true);
				UserResourceJsonModel userResourceJsonModel = this.mJsHelper.JsonsDeserialize<UserResourceJsonModel>(jsonStr);
				List<UserResourceModel> lstUserRes = new List<UserResourceModel>();
				if (userResourceJsonModel != null && userResourceJsonModel.UserResourcesList.Count > 0)
				{
					lstUserRes = userResourceJsonModel.UserResourcesList;
				}
				foreach (UserResourceModel userResourceModel in lstUserRes)
				{
					string oriTreePos = userResourceModel.OriTreePos;
					string[] array = (oriTreePos != null) ? oriTreePos.Split(new char[]
					{
						','
					}) : null;
					int pageNum;
					if (array != null && array.Length >= 2 && int.TryParse(array[1], out pageNum))
					{
						userResourceModel.PageNum = pageNum;
						if (userResourceModel.PvtbizType == "1")
						{
							Tuple<bool, string, string, DeviceFlags> userResInfo = UserResourcesManager.Instance.GetUserResInfo(userResourceModel, userid);
							if (!userResInfo.Item1)
							{
								return;
							}
							string item = userResInfo.Item2;
							string item2 = userResInfo.Item3;
							DeviceFlags item3 = userResInfo.Item4;
							new DownloadHelper().DownLoadFile2Path(item, item2, userResourceModel, true, item3, null);
						}
					}
				}
				base.Dispatcher.BeginInvoke(new Action(delegate()
				{
					this.CloseBook(false);
					CommonParamter.Instance.CurrentUserId = userid;
					CommonParamter.Instance.IsSeeMyBook = (userid == CommonParamter.Instance.LoginUserId);
					this.SetResderToolState(false);
					this.OpenTextBook(bookId, CommonParamter.Instance.CurrentUserId, lstUserRes, null, false);
				}), new object[0]);
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex.ToString());
				CommonParamter.Instance.CurrentUserId = CommonParamter.Instance.LoginUserId;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001C860 File Offset: 0x0001AA60
		protected override void TextbookOverlay_OnExit(object sender, EventArgs e)
		{
			try
			{
				string textbookID = base.TextbookID;
				base.TextbookOverlay_OnExit(sender, e);
				if (!CommonParamter.Instance.CourseIsClassing)
				{
					SetOwnerVisibility setOwnerVisibilityCallBack = base.SetOwnerVisibilityCallBack;
					if (setOwnerVisibilityCallBack != null)
					{
						setOwnerVisibilityCallBack(Visibility.Visible);
					}
					TextBookReader.ExitReaderSyncData exitReaderSyncDataCallBack = this.ExitReaderSyncDataCallBack;
					if (exitReaderSyncDataCallBack != null)
					{
						exitReaderSyncDataCallBack.BeginInvoke(textbookID, null, null);
					}
				}
				if (!CommonParamter.Instance.CourseIsClassing)
				{
					CefWindow.Instance.Close();
					ThreadEx.Run(delegate()
					{
						ExecuteRemoteCommand.StopRemoteControl();
					});
				}
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error(ex);
			}
		}

		// Token: 0x04000285 RID: 645
		private bool mShowNotDonwloadResInfoFlg = true;

		// Token: 0x04000286 RID: 646
		private PopInfoDataAccess mPopInfoDA = new PopInfoDataAccess();

		// Token: 0x04000287 RID: 647
		private IKeyboardMouseEvents m_Events;

		// Token: 0x04000288 RID: 648
		private ResourcesDataAccess mResourcesDataAccess = new ResourcesDataAccess();

		// Token: 0x0400028A RID: 650
		private Dictionary<int, int> mDicPageResourceDownloadStatus = new Dictionary<int, int>();

		// Token: 0x0400028B RID: 651
		private BookTitleBar mBookTitleBar;

		// Token: 0x0400028C RID: 652
		private ResourcesDataAccess mResDA = new ResourcesDataAccess();

		// Token: 0x04000292 RID: 658
		private Storyboard storyResNotDownload;

		// Token: 0x04000293 RID: 659
		private const double mResPageWidth = 680.0;

		// Token: 0x04000294 RID: 660
		private const double mResPageHeight = 590.0;

		// Token: 0x04000295 RID: 661
		private const double mPageTop = 0.0;

		// Token: 0x04000296 RID: 662
		private const double mPageRightMagin = 40.0;

		// Token: 0x04000297 RID: 663
		private static readonly string mClassPath = TrackerUtils.GetClassOrMethodFullPath(new string[]
		{
			"JXP",
			"PepDtp",
			"View",
			"TextBookReader",
			"TextBookReader"
		});

		// Token: 0x04000298 RID: 664
		public TextBookInnerOperator mBookOperator;

		// Token: 0x04000299 RID: 665
		public TextBookToolBar mBookToolBar;

		// Token: 0x0400029A RID: 666
		private TextBookInnerResWindow mInnerReaderWindow;

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x06000ADC RID: 2780
		public delegate void NavigateToMenue(MainMenuEnums menuIndex);

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x06000AE0 RID: 2784
		public delegate void ExitReaderSyncData(string bookid);
	}
}
