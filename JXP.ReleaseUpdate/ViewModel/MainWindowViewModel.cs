using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using JXP.ReleaseUpdate.Common;
using JXP.ReleaseUpdate.Model;

namespace JXP.ReleaseUpdate.ViewModel
{
	// Token: 0x0200000F RID: 15
	public class MainWindowViewModel
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000356B File Offset: 0x0000176B
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003573 File Offset: 0x00001773
		public ClientUpdateStatus CurrentClientUpdateStatus { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000357C File Offset: 0x0000177C
		public bool IsCoerceUpdate
		{
			get
			{
				return this.mRemoteUM != null && this.mRemoteUM.IsCoerce == 1;
			}
		}

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003598 File Offset: 0x00001798
		private double UpdateTotalSize
		{
			set
			{
				double num = value / 1024.0;
				if (num > 1024.0)
				{
					num /= 1024.0;
					this.CurrentClientUpdateStatus.TotalSize = num.ToString("f2") + "MB";
					return;
				}
				this.CurrentClientUpdateStatus.TotalSize = num.ToString("f2") + "KB";
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002879 File Offset: 0x00000A79
		public MainWindowViewModel(string strWebUrl, string strRemoteUrl, string strMainProcessPath)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000360C File Offset: 0x0000180C
		public MainWindowViewModel(string strMainProcessPath, string strClientID)
		{
			this.CurrentClientUpdateStatus = new ClientUpdateStatus();
			this.CurrentClientUpdateStatus.MainProcessPath = strMainProcessPath;
			this.CurrentClientUpdateStatus.ClientID = strClientID;
			this.CurrentClientUpdateStatus.LocalRootPath = Path.GetDirectoryName(strMainProcessPath).Trim();
			this.Init();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000365E File Offset: 0x0000185E
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003666 File Offset: 0x00001866
		public DownloadCompeleted DownloadCompeletedCallBack { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000366F File Offset: 0x0000186F
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003677 File Offset: 0x00001877
		public UpdateCompeleted UpdateCompletedCallBack { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003680 File Offset: 0x00001880
		private void Init()
		{
			if (string.IsNullOrEmpty(this.CurrentClientUpdateStatus.LocalRootPath))
			{
				this.CurrentClientUpdateStatus.LocalRootPath = Environment.CurrentDirectory;
			}
			this.mWebClient = this.GetNewWebClient();
			ServicePointManager.ServerCertificateValidationCallback = ((object <p0>, X509Certificate <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true);
			this.mJsonHelper = new JsonHelper();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000036EA File Offset: 0x000018EA
		public bool CheckNewReleaseUpdate()
		{
			return Assembly.GetCallingAssembly().GetModules()[0].Name.Contains("temp");
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000370C File Offset: 0x0000190C
		public bool CheckUpdateInfo()
		{
			this.mLocalUM = this.GetLastVersionFromLocal();
			if (this.mLocalUM == null)
			{
				this.mLocalUM = new UpdateModel();
			}
			if (string.IsNullOrEmpty(this.mLocalUM.Version))
			{
				this.mLocalUM.Version = this.GetLastVersionFromMainProcess();
			}
			else
			{
				this.mLocalUM.Version = this.mLocalUM.Version;
			}
			UpdateJasonModel lastVersionFromServer = this.GetLastVersionFromServer(this.mLocalUM.Version);
			if (lastVersionFromServer == null || lastVersionFromServer.Result != 500110 || lastVersionFromServer.UpdateInfo == null)
			{
				return false;
			}
			this.mRemoteUM = lastVersionFromServer.UpdateInfo;
			if (this.CompareVersion(this.mLocalUM.Version, this.mRemoteUM.Version) == 0)
			{
				return false;
			}
			this.mRemoteUM.AutoUpdate = this.mLocalUM.AutoUpdate;
			this.mLocalUM.State = 0;
			this.SaveLastVersionInfo(this.mLocalUM);
			if (!this.mLocalUM.AutoUpdate && this.mRemoteUM.IsCoerce != 1)
			{
				return false;
			}
			this.CurrentClientUpdateStatus.UpdateLogs = this.mRemoteUM.Description;
			this.GetUpdateList();
			if (this.mUpdateReleaseLst == null || this.mUpdateReleaseLst.Count < 1)
			{
				if (string.IsNullOrEmpty(this.mRemoteUM.DownloadPath))
				{
					return false;
				}
				this.mIsDownloadInstallPack = true;
			}
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003864 File Offset: 0x00001A64
		private void GetUpdateList()
		{
			if (this.mRemoteUM == null || this.mRemoteUM.ReleaseFileList == null)
			{
				return;
			}
			this.CheckUpdateList();
			this.CountUpdateTotalSize();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003888 File Offset: 0x00001A88
		private int CompareVersion(string strOldVer, string strNewVer)
		{
			try
			{
				Version v = new Version(strOldVer);
				Version v2 = new Version(strNewVer);
				if (v2 < v)
				{
					return -1;
				}
				if (v2 > v)
				{
					return 1;
				}
			}
			catch
			{
			}
			return 0;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038D8 File Offset: 0x00001AD8
		private UpdateModel GetLastVersionFromLocal()
		{
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", "lastver.json");
			if (string.IsNullOrEmpty(text) || !File.Exists(text))
			{
				return null;
			}
			string text2 = AppUtilities.ReadStringFromFile(text);
			if (string.IsNullOrEmpty(text2))
			{
				return null;
			}
			return this.mJsonHelper.JsonsDeserialize<UpdateModel>(text2);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003930 File Offset: 0x00001B30
		private string GetLastVersionFromMainProcess()
		{
			string result = "0.0.0.0";
			if (!string.IsNullOrEmpty(this.CurrentClientUpdateStatus.MainProcessPath) && File.Exists(this.CurrentClientUpdateStatus.MainProcessPath))
			{
				result = FileVersionInfo.GetVersionInfo(this.CurrentClientUpdateStatus.MainProcessPath).FileVersion;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003980 File Offset: 0x00001B80
		private UpdateJasonModel GetLastVersionFromServer(string strLocalLastVer)
		{
			UpdateJasonModel result = null;
			string path = string.Format("a/release/client/versionCheck?client_version={0}&product_id={1}&client_type=01 ", strLocalLastVer, this.CurrentClientUpdateStatus.ClientID);
			string address = Path.Combine(this.CurrentClientUpdateStatus.UpdateCenter, path);
			string jsonStr = string.Empty;
			try
			{
				using (StreamReader streamReader = new StreamReader(this.mWebClient.OpenRead(address), Encoding.UTF8))
				{
					jsonStr = streamReader.ReadToEnd();
				}
				result = this.mJsonHelper.JsonsDeserialize<UpdateJasonModel>(jsonStr);
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A1C File Offset: 0x00001C1C
		private void CheckUpdateList()
		{
			if (this.mRemoteUM == null || this.mRemoteUM.ReleaseFileList == null || this.mRemoteUM.ReleaseFileList.Count < 1)
			{
				return;
			}
			if (this.mLocalUM == null || this.mLocalUM.ReleaseFileList == null || this.mLocalUM.ReleaseFileList.Count < 1)
			{
				this.mUpdateReleaseLst = this.mRemoteUM.ReleaseFileList;
				return;
			}
			this.mUpdateReleaseLst = new List<ReleaseFileInfo>();
			Path.GetFileName(this.CurrentClientUpdateStatus.MainProcessPath);
			using (List<ReleaseFileInfo>.Enumerator enumerator = this.mRemoteUM.ReleaseFileList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ReleaseFileInfo releaseItem = enumerator.Current;
					ReleaseFileInfo releaseFileInfo = this.mLocalUM.ReleaseFileList.FirstOrDefault((ReleaseFileInfo a) => a.FilePath == releaseItem.FilePath);
					if (releaseFileInfo == null || this.CompareVersion(releaseFileInfo.LastVersion, releaseItem.LastVersion) != 0)
					{
						this.mUpdateReleaseLst.Add(releaseItem);
					}
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B44 File Offset: 0x00001D44
		private void CountUpdateTotalSize()
		{
			if (this.mUpdateReleaseLst != null)
			{
				foreach (ReleaseFileInfo releaseFileInfo in this.mUpdateReleaseLst)
				{
					this.mUpdateTotalSize += releaseFileInfo.FileSize;
				}
				this.UpdateTotalSize = (double)this.mUpdateTotalSize;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public void StartDownload()
		{
			this.mIsUpdateCanceling = false;
			this.mIsUpdateCanceled = false;
			if (this.mIsDownloadInstallPack)
			{
				this.StartDownloadPack();
				return;
			}
			if (this.mUpdateReleaseLst == null || this.mUpdateReleaseLst.Count < 1)
			{
				this.UpdateCompleted(false, false);
				return;
			}
			this.mDownloadFileList = new List<ReleaseFileInfo>();
			this.mDownloadFileList.AddRange(this.mUpdateReleaseLst);
			this.downloadThread = new Thread(new ThreadStart(this.ProcDownload));
			this.downloadThread.Name = "download";
			this.downloadThread.IsBackground = true;
			this.downloadThread.Start();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003C5C File Offset: 0x00001E5C
		public string StartDownloadPack()
		{
			if (this.mRemoteUM == null || string.IsNullOrEmpty(this.mRemoteUM.DownloadPath))
			{
				return "下载路径获取失败！";
			}
			this.mIsUpdateCanceling = false;
			this.mIsUpdateCanceled = false;
			string fileName = Path.GetFileName(this.mRemoteUM.DownloadPath);
			ReleaseFileInfo relFile = new ReleaseFileInfo();
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", "setup");
			relFile.FilePath = Path.Combine(text, fileName);
			relFile.Identity = this.mRemoteUM.Comment;
			this.mUpdateReleaseLst = new List<ReleaseFileInfo>();
			this.mUpdateReleaseLst.Add(relFile);
			this.evtDownload = new ManualResetEvent(true);
			this.mIsDownloadInstallPack = true;
			if (!AppUtilities.CheckFileIdentity(Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", relFile.FilePath), relFile.Identity, true))
			{
				AppUtilities.ClearExistFilesByDir(text, null);
				new Thread(delegate()
				{
					this.DownloadExcute(relFile, this.mRemoteUM.DownloadPath, relFile.FilePath);
					this.evtDownload.WaitOne();
					this.DownloadCompleted();
				})
				{
					IsBackground = true
				}.Start();
			}
			else
			{
				this.DownloadCompleted();
			}
			return string.Empty;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D98 File Offset: 0x00001F98
		private void ProcDownload()
		{
			if (string.IsNullOrEmpty(this.mRemoteUM.DownloadPath))
			{
				return;
			}
			this.evtDownload = new ManualResetEvent(true);
			string str = this.mRemoteUM.DownloadPath.Replace(Path.GetFileName(this.mRemoteUM.DownloadPath), "update_list/");
			while (this.mDownloadFileList.Count > 0)
			{
				while (this.mIsUpdateCanceling)
				{
					Thread.Sleep(500);
					if (this.mIsUpdateCanceled)
					{
						return;
					}
				}
				ReleaseFileInfo releaseFileInfo = this.mDownloadFileList[0];
				string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", releaseFileInfo.FilePath);
				if (AppUtilities.CheckFileIdentity(text, releaseFileInfo.Identity, false))
				{
					this.evtDownload.Set();
					this.SetDownloadProgress(releaseFileInfo.FileSize, 0);
				}
				else
				{
					this.DownloadExcute(releaseFileInfo, str + releaseFileInfo.FilePath, text);
				}
				this.evtDownload.WaitOne();
				this.mDownloadedTotalSize += releaseFileInfo.FileSize;
				this.mDownloadFileList.Remove(releaseFileInfo);
			}
			this.DownloadCompleted();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003EB8 File Offset: 0x000020B8
		public void DownloadExcute(ReleaseFileInfo file, string strSrc, string strDest)
		{
			this.evtDownload.Reset();
			this.mWebClient = this.GetNewWebClient();
			string directoryName = Path.GetDirectoryName(strDest);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.mWebClient.DownloadFileAsync(new Uri(strSrc), strDest, file);
			this.evtDownload.WaitOne();
			this.mWebClient.Dispose();
			this.mWebClient = null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F24 File Offset: 0x00002124
		private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			while (this.mIsUpdateCanceling)
			{
				Thread.Sleep(500);
				if (this.mIsUpdateCanceled)
				{
					return;
				}
			}
			this.evtDownload.Set();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F50 File Offset: 0x00002150
		private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			if (this.mUpdateTotalSize == 0L)
			{
				this.UpdateTotalSize = (double)(this.mUpdateTotalSize = e.TotalBytesToReceive);
			}
			this.SetDownloadProgress(e.BytesReceived, e.ProgressPercentage);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003F90 File Offset: 0x00002190
		private void SetDownloadProgress(long lReceivedSize, int iProgressPercentage = 0)
		{
			if (this.mIsUpdateCanceling)
			{
				return;
			}
			if (this.mUpdateTotalSize == 0L)
			{
				this.CurrentClientUpdateStatus.ProgressValue = (double)iProgressPercentage;
				return;
			}
			double num = (double)(this.mDownloadedTotalSize + lReceivedSize) * 100.0 / (double)this.mUpdateTotalSize;
			num = Math.Round(num, 2);
			if (num >= 99.0)
			{
				this.CurrentClientUpdateStatus.ProgressValue = 99.0;
				return;
			}
			if (num >= this.CurrentClientUpdateStatus.ProgressValue)
			{
				this.CurrentClientUpdateStatus.ProgressValue = num;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000401B File Offset: 0x0000221B
		private void DownloadCompleted()
		{
			this.CurrentClientUpdateStatus.ProgressValue = 99.0;
			if (this.DownloadCompeletedCallBack != null)
			{
				this.mUpdateTotalSize = 0L;
				this.mDownloadedTotalSize = 0L;
				this.DownloadCompeletedCallBack();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004054 File Offset: 0x00002254
		public void StartUpdate()
		{
			if (this.mIsDownloadInstallPack)
			{
				this.ReInstallTheClient();
				return;
			}
			this.UpdateTheClient();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000406C File Offset: 0x0000226C
		private void UpdateTheClient()
		{
			bool flag = true;
			foreach (ReleaseFileInfo relFile in this.mUpdateReleaseLst)
			{
				if (this.mIsUpdateCanceled)
				{
					return;
				}
				flag = this.UpdateTheFile(relFile);
				if (!flag)
				{
					break;
				}
				while (this.mIsUpdateCanceling)
				{
					Thread.Sleep(500);
				}
			}
			if (flag)
			{
				flag = this.ExcuteTheSetup(this.mStrExeLst);
			}
			this.UpdateCompleted(flag, false);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040FC File Offset: 0x000022FC
		private bool UpdateMySelf()
		{
			if (this.mUpdateReleaseLst == null)
			{
				return false;
			}
			string strCurAssemblyName = Assembly.GetCallingAssembly().GetModules()[0].Name;
			ReleaseFileInfo releaseFileInfo = this.mUpdateReleaseLst.FirstOrDefault((ReleaseFileInfo a) => a.FilePath.EndsWith(strCurAssemblyName));
			if (!this.UpdateTheFile(releaseFileInfo))
			{
				return false;
			}
			string strDotNetZipDllFileName = "DotNetZip.dll";
			ReleaseFileInfo relFile = this.mUpdateReleaseLst.FirstOrDefault((ReleaseFileInfo a) => a.FilePath.EndsWith(strDotNetZipDllFileName));
			if (!this.UpdateTheFile(relFile))
			{
				return false;
			}
			if (this.UpdateCompletedCallBack != null)
			{
				this.mUpdateTotalSize = 0L;
				this.mDownloadedTotalSize = 0L;
				this.UpdateCompletedCallBack(true, true);
			}
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, releaseFileInfo.FilePath);
			string strArg = string.Format("\"{0}\" \"{1}\"", this.CurrentClientUpdateStatus.MainProcessPath, this.CurrentClientUpdateStatus.ClientID);
			if (File.Exists(text))
			{
				AppUtilities.ExcuteTheProcess(text, strArg, false);
			}
			return true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000041F4 File Offset: 0x000023F4
		private bool UpdateTheFile(ReleaseFileInfo relFile)
		{
			if (relFile == null)
			{
				return false;
			}
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", relFile.FilePath);
			if (!AppUtilities.CheckFileIdentity(text, relFile.Identity, false))
			{
				return false;
			}
			if (relFile.FilePath.EndsWith(".zip"))
			{
				return this.UnzipAndUpdateDir(relFile);
			}
			try
			{
				string text2 = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, relFile.FilePath);
				if (File.Exists(text2))
				{
					string text3 = text2 + ".old";
					if (File.Exists(text3))
					{
						File.Delete(text3);
					}
					File.Move(text2, text3);
				}
				else
				{
					string directoryName = Path.GetDirectoryName(text2);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
				}
				File.Move(text, text2);
				if (relFile.NeedInst == 1)
				{
					if (this.mStrExeLst == null)
					{
						this.mStrExeLst = new List<string>();
					}
					this.mStrExeLst.Add(text2);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000042F4 File Offset: 0x000024F4
		private bool UnzipAndUpdateDir(ReleaseFileInfo relFile)
		{
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", relFile.FilePath);
			if (!File.Exists(text))
			{
				return false;
			}
			try
			{
				string text2 = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, relFile.FilePath);
				text2 = text2.Substring(0, text2.LastIndexOf(".zip"));
				if (Directory.Exists(text2))
				{
					string text3 = text2 + ".old";
					if (Directory.Exists(text3))
					{
						Directory.Delete(text3, true);
					}
					Directory.Move(text2, text3);
				}
				DirectoryInfo parent = Directory.GetParent(text2);
				DotZipHelper.Decompression(text, parent.FullName, true, null);
				if (!this.CheckFoxitAndReg(text2))
				{
					return false;
				}
				File.Delete(text);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000043C4 File Offset: 0x000025C4
		private bool CheckFoxitAndReg(string strDir)
		{
			if (string.IsNullOrEmpty(strDir) || !Directory.Exists(strDir) || !strDir.EndsWith(AppConsts.STR_DIR_FOIXT))
			{
				return true;
			}
			string text = Path.Combine(strDir, AppConsts.STR_FOIXT_SDK_ACTIVEX_OCX_FILE_NAME);
			if (!File.Exists(text))
			{
				return true;
			}
			try
			{
				Process process = Process.Start(new ProcessStartInfo
				{
					FileName = "Regsvr32.exe",
					Arguments = string.Format("/s \"{0}\"", text),
					UseShellExecute = false,
					CreateNoWindow = true
				});
				process.WaitForExit(5000);
				if (process != null && !process.HasExited)
				{
					process.Kill();
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
			string text2 = Path.Combine(strDir, AppConsts.STR_FOIXT_SDK_ACTIVEX_DLL_FILE_NAME);
			string text3 = Path.Combine(strDir, AppConsts.STR_FOIXT_SDK_LIB_DLL_FILE_NAME);
			if (!File.Exists(text2) || !File.Exists(text3))
			{
				return true;
			}
			string text4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConsts.STR_FOIXT_SDK_ACTIVEX_DLL_FILE_NAME);
			string text5 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConsts.STR_FOIXT_SDK_LIB_DLL_FILE_NAME);
			if (File.Exists(text4))
			{
				File.Delete(text4);
			}
			if (File.Exists(text5))
			{
				File.Delete(text5);
			}
			File.Move(text2, text4);
			File.Move(text3, text5);
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000450C File Offset: 0x0000270C
		private void ReInstallTheClient()
		{
			if (this.mUpdateReleaseLst == null || this.mUpdateReleaseLst.Count < 1)
			{
				this.UpdateCompleted(false, false);
				return;
			}
			ReleaseFileInfo releaseFileInfo = this.mUpdateReleaseLst[0];
			bool isSuccess = false;
			if (AppUtilities.CheckFileIdentity(Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", releaseFileInfo.FilePath), releaseFileInfo.Identity, true))
			{
				AppUtilities.ExcuteTheProcess(releaseFileInfo.FilePath, "", false);
				isSuccess = true;
			}
			this.UpdateCompleted(isSuccess, true);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000458C File Offset: 0x0000278C
		private bool ExcuteTheSetup(List<string> setupLst)
		{
			if (setupLst == null)
			{
				return true;
			}
			try
			{
				foreach (string text in setupLst)
				{
					if (!File.Exists(text))
					{
						return false;
					}
					AppUtilities.ExcuteTheProcess(text, "", true);
					File.Delete(text);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004610 File Offset: 0x00002810
		private void UpdateCompleted(bool isSuccess, bool isReinstall = false)
		{
			if (isSuccess && this.mRemoteUM != null)
			{
				this.mRemoteUM.State = 1;
				this.ClearBackupFiles();
				this.CurrentClientUpdateStatus.ProgressValue = 100.0;
				if (!isReinstall)
				{
					this.SaveLastVersionInfo(this.mRemoteUM);
				}
			}
			else
			{
				this.RestoreCurrentUpdate();
				this.CurrentClientUpdateStatus.ProgressValue = 0.0;
			}
			if (this.UpdateCompletedCallBack != null)
			{
				this.UpdateCompletedCallBack(isSuccess, !isReinstall);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004694 File Offset: 0x00002894
		public void SaveLastVersionInfo(UpdateModel updateModel)
		{
			if (updateModel != null)
			{
				string json = this.mJsonHelper.SerializeString<UpdateModel>(updateModel);
				string fileFullName = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, "update_list/", "lastver.json");
				AppUtilities.SaveString2File(json, fileFullName);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002A23 File Offset: 0x00000C23
		private void SaveLastReleaseLst()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000046D4 File Offset: 0x000028D4
		private void RestoreCurrentUpdate()
		{
			if (this.mUpdateReleaseLst == null)
			{
				return;
			}
			foreach (ReleaseFileInfo releaseFileInfo in this.mUpdateReleaseLst)
			{
				try
				{
					if (releaseFileInfo.FilePath.ToLower().EndsWith(".zip"))
					{
						this.RestoreTheDir(releaseFileInfo);
					}
					else
					{
						this.RestoreTheFile(releaseFileInfo);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004764 File Offset: 0x00002964
		private void RestoreTheFile(ReleaseFileInfo relFile)
		{
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, relFile.FilePath);
			string text2 = text + ".old";
			if (!File.Exists(text2))
			{
				return;
			}
			if (relFile.Identity != Md5Helper.GetStringMd5Value(Md5Helper.GetFileMd5Value(text).Substring(5, 13)).Substring(3, 8))
			{
				return;
			}
			File.Delete(text);
			File.Move(text2, text);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000047D4 File Offset: 0x000029D4
		private void RestoreTheDir(ReleaseFileInfo relFile)
		{
			string text = Path.Combine(this.CurrentClientUpdateStatus.LocalRootPath, relFile.FilePath).Replace(".zip", "");
			string text2 = text + ".old";
			if (!Directory.Exists(text2))
			{
				return;
			}
			if (Directory.Exists(text))
			{
				Directory.Delete(text);
			}
			File.Move(text2, text);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004831 File Offset: 0x00002A31
		private void ClearBackupFiles()
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += this.Bw_DoWork;
			backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004850 File Offset: 0x00002A50
		private void Bw_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				string[] strSufFilterArr = new string[]
				{
					".old"
				};
				AppUtilities.ClearExistFilesByDir(this.CurrentClientUpdateStatus.LocalRootPath, strSufFilterArr);
			}
			catch
			{
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004894 File Offset: 0x00002A94
		public void StartCancel(bool isCanceling)
		{
			this.mIsUpdateCanceling = isCanceling;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000489D File Offset: 0x00002A9D
		public void Cancel()
		{
			this.mIsUpdateCanceling = false;
			this.mIsUpdateCanceled = true;
			if (this.mWebClient != null)
			{
				this.mWebClient.CancelAsync();
			}
			this.RestoreCurrentUpdate();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000048C6 File Offset: 0x00002AC6
		public void SetAutoUpdate(bool isAutoUpdate)
		{
			if (this.mRemoteUM == null)
			{
				return;
			}
			this.mLocalUM.AutoUpdate = isAutoUpdate;
			this.mRemoteUM.AutoUpdate = isAutoUpdate;
			this.SaveLastVersionInfo(this.mLocalUM);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000048F5 File Offset: 0x00002AF5
		private WebClient GetNewWebClient()
		{
			WebClient webClient = new WebClient();
			webClient.Proxy = null;
			webClient.UseDefaultCredentials = true;
			webClient.DownloadProgressChanged += this.OnDownloadProgressChanged;
			webClient.DownloadFileCompleted += this.OnDownloadFileCompleted;
			return webClient;
		}

		// Token: 0x0400002D RID: 45
		private WebClient mWebClient;

		// Token: 0x0400002E RID: 46
		private JsonHelper mJsonHelper;

		// Token: 0x0400002F RID: 47
		private UpdateModel mLocalUM;

		// Token: 0x04000030 RID: 48
		private UpdateModel mRemoteUM;

		// Token: 0x04000031 RID: 49
		private List<ReleaseFileInfo> mUpdateReleaseLst;

		// Token: 0x04000032 RID: 50
		private long mUpdateTotalSize;

		// Token: 0x04000033 RID: 51
		private long mDownloadedTotalSize;

		// Token: 0x04000034 RID: 52
		private List<ReleaseFileInfo> mDownloadFileList;

		// Token: 0x04000035 RID: 53
		private ManualResetEvent evtDownload;

		// Token: 0x04000036 RID: 54
		private Thread downloadThread;

		// Token: 0x04000037 RID: 55
		private bool mIsUpdateCanceled;

		// Token: 0x04000038 RID: 56
		private bool mIsUpdateCanceling;

		// Token: 0x04000039 RID: 57
		private bool mIsDownloadInstallPack;

		// Token: 0x0400003A RID: 58
		private List<string> mStrExeLst;
	}
}
