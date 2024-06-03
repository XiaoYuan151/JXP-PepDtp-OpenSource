using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.View;

namespace JXP.PepDtp.PepClass
{
	// Token: 0x0200007C RID: 124
	public class PepClassCallbackCommand
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0002AB08 File Offset: 0x00028D08
		public static void DownloadOrView(string classId, string downloadUrlwithoutHost)
		{
			string fileName = Path.GetFileName(downloadUrlwithoutHost);
			string text = Path.Combine(PepClassCallbackCommand.PepFileRootPath, classId, fileName);
			if (PepClassCallbackCommand.View(text))
			{
				return;
			}
			PepClassCallbackCommand.Download(downloadUrlwithoutHost, text);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002AB3A File Offset: 0x00028D3A
		private static bool View(string path)
		{
			if (!File.Exists(path))
			{
				return false;
			}
			Process.Start(path);
			return true;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002AB50 File Offset: 0x00028D50
		private static bool Download(string url, string saveAs)
		{
			string directoryName = Path.GetDirectoryName(saveAs);
			Directory.CreateDirectory(directoryName);
			DownloadResourcesWindow.GetInstance().ShowTranslateWindow(Application.Current.MainWindow);
			DownloadResourcesWindow.GetInstance().downloadrbtn.IsChecked = new bool?(true);
			SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.AllIsFinishedCallBack = null;
			SyncResourcesManager.Instance.LstDownloadUserRes.TransferOperList.SingleIsFinishedCallBack = new TransferListHelper.SingleIsFinished(PepClassCallbackCommand.ResDownloadIsFinished);
			SyncResourcesManager.Instance.LstDownloadUserRes.DownLoadFile2Path(url, directoryName, DateTime.Now.ToString("s"), true, false, new NotifyInfo(DownloadResourcesWindow.GetInstance().NotifyChange));
			return true;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002AC00 File Offset: 0x00028E00
		private static void ResDownloadIsFinished(TransferFileModel transFile)
		{
			Application.Current.Dispatcher.Invoke(new Action(delegate()
			{
				DownloadResourcesWindow.GetInstance().resultrbtn.IsChecked = new bool?(true);
				DownloadResourcesWindow.GetInstance().NotifyDownloadIsFinished1(transFile);
				PepClassCallbackCommand.View(Path.Combine(transFile.DestPath, transFile.FileName));
			}), new object[0]);
		}

		// Token: 0x04000468 RID: 1128
		public static string PepFileRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PepClassFileData");
	}
}
