using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace JXP.ReleaseUpdate.Common
{
	// Token: 0x02000012 RID: 18
	public class AppUtilities
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004C14 File Offset: 0x00002E14
		public static string ReadStringFromFile(string fileFullName)
		{
			string result = string.Empty;
			try
			{
				FileInfo fileInfo = new FileInfo(fileFullName);
				if (fileInfo.Exists)
				{
					StreamReader streamReader = fileInfo.OpenText();
					result = streamReader.ReadToEnd();
					streamReader.Close();
					streamReader.Dispose();
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004C64 File Offset: 0x00002E64
		public static void SaveString2File(string json, string fileFullName)
		{
			FileInfo fileInfo = new FileInfo(fileFullName);
			if (fileInfo.Exists)
			{
				fileInfo.Delete();
			}
			DirectoryInfo directory = fileInfo.Directory;
			if (!directory.Exists)
			{
				directory.Create();
			}
			StreamWriter streamWriter = fileInfo.CreateText();
			streamWriter.Write(json);
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public static void CheckExistAndBackupFile(string strFilePath)
		{
			if (File.Exists(strFilePath))
			{
				string path = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(strFilePath), DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetExtension(strFilePath));
				string text = Path.Combine(Path.GetDirectoryName(strFilePath), path);
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Move(strFilePath, text);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D14 File Offset: 0x00002F14
		public static bool CheckFileIdentity(string strFilePath, string strIdentity, bool isInstPack = false)
		{
			if (string.IsNullOrEmpty(strFilePath) || !File.Exists(strFilePath))
			{
				return false;
			}
			string fileMd5Value = Md5Helper.GetFileMd5Value(strFilePath);
			if (isInstPack)
			{
				if (strIdentity.CompareTo(fileMd5Value) == 0)
				{
					return true;
				}
			}
			else if (strIdentity.CompareTo(Md5Helper.GetStringMd5Value(fileMd5Value.Substring(5, 13)).Substring(3, 8)) == 0)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004D68 File Offset: 0x00002F68
		public static void ExcuteTheProcess(string strExeFile, string strArg = "", bool isProWait = false)
		{
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo();
			process.StartInfo.FileName = strExeFile;
			process.StartInfo.Arguments = strArg;
			process.Start();
			if (isProWait)
			{
				process.WaitForExit();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static void ClearExistFilesByDir(string strDir, string[] strSufFilterArr = null)
		{
			if (string.IsNullOrEmpty(strDir) || !Directory.Exists(strDir))
			{
				return;
			}
			string[] files = Directory.GetFiles(strDir);
			if (files != null && files.Length != 0)
			{
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string strFile = array[i];
					if (strSufFilterArr == null)
					{
						File.Delete(strFile);
					}
					else if (strSufFilterArr.Any((string a) => strFile.EndsWith(a)))
					{
						File.Delete(strFile);
					}
				}
			}
			string[] directories = Directory.GetDirectories(strDir);
			if (directories != null && directories.Length != 0)
			{
				string[] array = directories;
				for (int i = 0; i < array.Length; i++)
				{
					string strSubDir = array[i];
					if (strSufFilterArr == null)
					{
						Directory.Delete(strSubDir);
					}
					else if (strSufFilterArr.Any((string a) => strSubDir.EndsWith(a)))
					{
						Directory.Delete(strSubDir, true);
					}
					else
					{
						AppUtilities.ClearExistFilesByDir(strSubDir, strSufFilterArr);
					}
				}
			}
		}
	}
}
