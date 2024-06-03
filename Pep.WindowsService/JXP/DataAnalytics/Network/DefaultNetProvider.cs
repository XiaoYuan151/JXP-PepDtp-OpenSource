using System;
using System.Collections.Generic;
using JXP.DataAnalytics.Debugging;
using JXP.DataAnalytics.Exceptions;
using JXP.DataAnalytics.Network.Model;
using JXP.DataAnalytics.Tracker;
using JXP.DataAnalytics.Utility;
using Pep.WindowsService.Utils;

namespace JXP.DataAnalytics.Network
{
	// Token: 0x0200003B RID: 59
	internal class DefaultNetProvider : INetProvider
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006A5F File Offset: 0x00004C5F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006A67 File Offset: 0x00004C67
		internal EndPointInfo UploadEndPoint { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006A70 File Offset: 0x00004C70
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006A78 File Offset: 0x00004C78
		internal TokenInfo Token { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006A81 File Offset: 0x00004C81
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006A89 File Offset: 0x00004C89
		internal ITrackerConfig TrackerConfig { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006A92 File Offset: 0x00004C92
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006A99 File Offset: 0x00004C99
		internal static INetProvider Current { get; set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x00006AA1 File Offset: 0x00004CA1
		internal DefaultNetProvider(ITrackerConfig config)
		{
			if (config == null)
			{
				throw new TrackerException("参数[config]不能为空.", null);
			}
			this.TrackerConfig = config;
			this.InternalInit();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public bool SendTrackerData(string data)
		{
			return this.CheckAndRefreshToken() && this.CheckAndRefreshEndPoint() && this.PostData(data) != null;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006AE7 File Offset: 0x00004CE7
		public void SendTrackerDataAsync(string data)
		{
			ThreadEx.Run(delegate()
			{
				this.SendTrackerData(data);
			});
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006B0D File Offset: 0x00004D0D
		public TokenInfo GetTokenData()
		{
			this.CheckAndRefreshToken();
			return this.Token;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006B1C File Offset: 0x00004D1C
		private void InternalInit()
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006B20 File Offset: 0x00004D20
		private bool CheckAndRefreshToken()
		{
			object obj = DefaultNetProvider.mLockObj;
			lock (obj)
			{
				this.Token = this.GetTokenInfo();
			}
			this.GetEndPointFromToken();
			return this.Token != null;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006B74 File Offset: 0x00004D74
		private void ClearToken()
		{
			if (this.Token != null)
			{
				object obj = DefaultNetProvider.mLockObj;
				lock (obj)
				{
					if (this.Token != null)
					{
						this.Token = null;
					}
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006BC4 File Offset: 0x00004DC4
		private void GetEndPointFromToken()
		{
			if (this.UploadEndPoint == null)
			{
				TokenInfo token = this.Token;
				if (!string.IsNullOrEmpty((token != null) ? token.Url : null))
				{
					object obj = DefaultNetProvider.mLockObj;
					lock (obj)
					{
						if (this.UploadEndPoint == null)
						{
							TokenInfo token2 = this.Token;
							if (!string.IsNullOrEmpty((token2 != null) ? token2.Url : null))
							{
								this.UploadEndPoint = new EndPointInfo
								{
									Url = this.Token.Url
								};
							}
						}
					}
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006C5C File Offset: 0x00004E5C
		[Obsolete("服务端获取token的接口可以获取所有信息,该方法仅过渡期间使用")]
		private bool CheckAndRefreshEndPoint()
		{
			if (this.UploadEndPoint == null)
			{
				object obj = DefaultNetProvider.mLockObj;
				lock (obj)
				{
					if (this.UploadEndPoint == null)
					{
						this.UploadEndPoint = this.GetUploadEndPointInfo();
					}
				}
			}
			return this.UploadEndPoint != null;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006CBC File Offset: 0x00004EBC
		private TokenInfo GetTokenInfo()
		{
			try
			{
				string webRequestResult = HttpWebRequestHelper.GetWebRequestResult("https://bd-in.mypep.cn/data_collect/collect/service/token.json", new Dictionary<string, string>
				{
					{
						"key",
						this.TrackerConfig.AppKey
					}
				}, 10000);
				if (string.IsNullOrWhiteSpace(webRequestResult))
				{
					return null;
				}
				TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(webRequestResult);
				if (tokenModel == null || tokenModel.ErrCode != "500110")
				{
					DebugTracer.Write("GetTokenInfo.ErrCode:" + ((tokenModel != null) ? tokenModel.ErrCode : null));
					return null;
				}
				return tokenModel.Result;
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			return null;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006D60 File Offset: 0x00004F60
		private EndPointInfo GetUploadEndPointInfo()
		{
			EndPointModel endPointModel = this.ExecuteWithTokenCheck(delegate
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("key", this.TrackerConfig.AppKey);
				IDictionary<string, string> dictionary2 = dictionary;
				string key = "token";
				TokenInfo token = this.Token;
				dictionary2.Add(key, (token != null) ? token.AccessToken : null);
				string webRequestResult = HttpWebRequestHelper.GetWebRequestResult("https://bd-in.mypep.cn/data_collect/collect/service/url.json", dictionary, 10000);
				if (string.IsNullOrWhiteSpace(webRequestResult))
				{
					return null;
				}
				EndPointModel endPointModel2 = JsonConvert.DeserializeObject<EndPointModel>(webRequestResult);
				DebugTracer.Write("GetUploadEndPointInfo.ErrCode:" + ((endPointModel2 != null) ? endPointModel2.ErrCode : null));
				return endPointModel2;
			}) as EndPointModel;
			if (endPointModel == null)
			{
				return null;
			}
			return endPointModel.Result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006D84 File Offset: 0x00004F84
		private BaseModel PostData(string data)
		{
			return this.ExecuteWithTokenCheck(delegate
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("key", this.TrackerConfig.AppKey);
				IDictionary<string, string> dictionary2 = dictionary;
				string key = "token";
				TokenInfo token = this.Token;
				dictionary2.Add(key, (token != null) ? token.AccessToken : null);
				dictionary.Add("data", data);
				string webRequestResult = HttpWebRequestHelper.GetWebRequestResult(this.UploadEndPoint.Url, dictionary, this.TrackerConfig.PostTimeout);
				if (string.IsNullOrWhiteSpace(webRequestResult))
				{
					return null;
				}
				return JsonConvert.DeserializeObject<BaseModel>(webRequestResult);
			});
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006DB8 File Offset: 0x00004FB8
		private BaseModel ExecuteWithTokenCheck(Func<BaseModel> func)
		{
			try
			{
				BaseModel baseModel = func();
				if (baseModel == null || baseModel.ErrCode == "500110")
				{
					return baseModel;
				}
				if (baseModel.ErrCode == "501096" || baseModel.ErrCode == "501095")
				{
					this.ClearToken();
					if (!this.CheckAndRefreshToken())
					{
						return null;
					}
					baseModel = func();
					if (baseModel == null || baseModel.ErrCode == "500110")
					{
						return baseModel;
					}
				}
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			return null;
		}

		// Token: 0x0400009E RID: 158
		private static readonly object mLockObj = new object();
	}
}
