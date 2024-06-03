using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000002 RID: 2
	public class DotZipHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool CompressMulti(List<string> list, string strZipName, bool isDirStruct = true)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (string.IsNullOrEmpty(strZipName))
			{
				throw new ArgumentNullException("strZipName");
			}
			string directoryName = Path.GetDirectoryName(strZipName);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			bool result;
			try
			{
				using (ZipFile zipFile = new ZipFile(Encoding.UTF8))
				{
					foreach (string text in list)
					{
						string fileName = Path.GetFileName(text);
						if (Directory.Exists(text))
						{
							if (isDirStruct)
							{
								zipFile.AddDirectory(text, fileName);
							}
							else
							{
								zipFile.AddDirectory(text);
							}
						}
						if (File.Exists(text))
						{
							if (isDirStruct)
							{
								zipFile.AddFile(text);
							}
							else
							{
								zipFile.AddFile(text, "");
							}
						}
					}
					zipFile.Save(strZipName);
					result = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002160 File Offset: 0x00000360
		public static bool CompressMulti(List<string> list, string strZipName, string password, EventHandler<AddProgressEventArgs> compressProgress, EventHandler<SaveProgressEventArgs> saveProgress, bool isDirStruct = true)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (string.IsNullOrEmpty(strZipName))
			{
				throw new ArgumentNullException("strZipName");
			}
			string directoryName = Path.GetDirectoryName(strZipName);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			bool result;
			try
			{
				using (ZipFile zipFile = new ZipFile(Encoding.UTF8))
				{
					zipFile.Password = password;
					zipFile.AddProgress += compressProgress;
					zipFile.SaveProgress += saveProgress;
					foreach (string text in list)
					{
						string fileName = Path.GetFileName(text);
						if (Directory.Exists(text))
						{
							if (isDirStruct)
							{
								zipFile.AddDirectory(text, fileName);
							}
							else
							{
								zipFile.AddDirectory(text);
							}
						}
						if (File.Exists(text))
						{
							if (isDirStruct)
							{
								zipFile.AddFile(text);
							}
							else
							{
								zipFile.AddFile(text, "");
							}
						}
					}
					zipFile.Save(strZipName);
					result = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002288 File Offset: 0x00000488
		public static bool Decompression(string zipFullName, string unZipPath, bool isOverWrite = false, DotZipHelper.DecompressionInfo DecompressionInfoCallback = null)
		{
			if (string.IsNullOrEmpty(zipFullName))
			{
				throw new ArgumentNullException("zipFullName");
			}
			if (string.IsNullOrEmpty(unZipPath))
			{
				string directoryName = Path.GetDirectoryName(zipFullName);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipFullName);
				unZipPath = Path.Combine(directoryName, fileNameWithoutExtension);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(unZipPath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			ReadOptions options = new ReadOptions
			{
				Encoding = Encoding.UTF8
			};
			bool result;
			using (ZipFile zipFile = ZipFile.Read(zipFullName, options))
			{
				ExtractExistingFileAction extractExistingFile = isOverWrite ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.DoNotOverwrite;
				int num = 0;
				foreach (ZipEntry zipEntry in zipFile)
				{
					zipEntry.Extract(unZipPath, extractExistingFile);
					num++;
					bool? flag = (DecompressionInfoCallback != null) ? new bool?(DecompressionInfoCallback(num, zipFile.Count)) : null;
					if (flag != null && flag.Value)
					{
						return false;
					}
				}
				if (DecompressionInfoCallback != null)
				{
					DecompressionInfoCallback(zipFile.Count, zipFile.Count);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023B4 File Offset: 0x000005B4
		public static bool Decompression(string zipFullName, string unZipPath, string password, bool isOverWrite = false, DotZipHelper.DecompressionInfo DecompressionInfoCallback = null)
		{
			if (string.IsNullOrEmpty(zipFullName))
			{
				throw new ArgumentNullException("zipFullName");
			}
			if (string.IsNullOrEmpty(unZipPath))
			{
				string directoryName = Path.GetDirectoryName(zipFullName);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipFullName);
				unZipPath = Path.Combine(directoryName, fileNameWithoutExtension);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(unZipPath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			ReadOptions options = new ReadOptions
			{
				Encoding = Encoding.UTF8
			};
			bool result;
			using (ZipFile zipFile = ZipFile.Read(zipFullName, options))
			{
				zipFile.Password = password;
				ExtractExistingFileAction extractExistingFile = isOverWrite ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.DoNotOverwrite;
				int num = 0;
				foreach (ZipEntry zipEntry in zipFile)
				{
					zipEntry.Extract(unZipPath, extractExistingFile);
					num++;
					bool? flag = (DecompressionInfoCallback != null) ? new bool?(DecompressionInfoCallback(num, zipFile.Count)) : null;
					if (flag != null && flag.Value)
					{
						return false;
					}
				}
				if (DecompressionInfoCallback != null)
				{
					DecompressionInfoCallback(zipFile.Count, zipFile.Count);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024EC File Offset: 0x000006EC
		public static void Decompression(Stream srcZipStream, string unZipPath, bool isOverWrite = false, DotZipHelper.DecompressionInfo DecompressionInfoCallback = null)
		{
			if (srcZipStream == null)
			{
				throw new ArgumentNullException("srcZipStream");
			}
			if (string.IsNullOrWhiteSpace(unZipPath))
			{
				throw new ArgumentNullException("unZipPath");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(unZipPath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			ReadOptions options = new ReadOptions
			{
				Encoding = Encoding.UTF8
			};
			using (ZipFile zipFile = ZipFile.Read(srcZipStream, options))
			{
				ExtractExistingFileAction extractExistingFile = isOverWrite ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.DoNotOverwrite;
				int num = 0;
				foreach (ZipEntry zipEntry in zipFile)
				{
					zipEntry.Extract(unZipPath, extractExistingFile);
					num++;
					bool? flag = (DecompressionInfoCallback != null) ? new bool?(DecompressionInfoCallback(num, zipFile.Count)) : null;
					if (flag != null && flag.Value)
					{
						return;
					}
				}
				if (DecompressionInfoCallback != null)
				{
					DecompressionInfoCallback(zipFile.Count, zipFile.Count);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002600 File Offset: 0x00000800
		public static void Decompression(Stream srcZipStream, string unZipPath, string searchPattern, string replace, DotZipHelper.DecompressionInfo DecompressionInfoCallback = null)
		{
			if (srcZipStream == null)
			{
				throw new ArgumentNullException("srcZipStream");
			}
			if (string.IsNullOrWhiteSpace(unZipPath))
			{
				throw new ArgumentNullException("unZipPath");
			}
			if (string.IsNullOrWhiteSpace(searchPattern))
			{
				throw new ArgumentNullException("searchPattern");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(unZipPath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			ReadOptions options = new ReadOptions
			{
				Encoding = Encoding.UTF8
			};
			using (ZipFile zipFile = ZipFile.Read(srcZipStream, options))
			{
				int num = 0;
				foreach (ZipEntry zipEntry in zipFile)
				{
					if (string.IsNullOrEmpty(zipEntry.FileName))
					{
						zipEntry.Extract(unZipPath, ExtractExistingFileAction.OverwriteSilently);
					}
					else
					{
						Regex regex = new Regex(searchPattern, RegexOptions.IgnoreCase);
						Match match = regex.Match(zipEntry.FileName);
						if (!match.Success || match.Index != 0)
						{
							zipEntry.Extract(unZipPath, ExtractExistingFileAction.OverwriteSilently);
						}
						else
						{
							string text = regex.Replace(zipEntry.FileName, replace, 1);
							text = Path.Combine(unZipPath, text);
							if (zipEntry.IsDirectory)
							{
								if (!Directory.Exists(text))
								{
									Directory.CreateDirectory(text);
								}
							}
							else
							{
								FileMode mode = FileMode.OpenOrCreate;
								if (File.Exists(text))
								{
									mode = FileMode.Truncate;
								}
								else
								{
									string directoryName = Path.GetDirectoryName(text);
									if (!Directory.Exists(directoryName))
									{
										Directory.CreateDirectory(directoryName);
									}
								}
								using (Stream stream = File.Open(text, mode, FileAccess.Write, FileShare.None))
								{
									stream.Position = 0L;
									zipEntry.Extract(stream);
								}
								num++;
								bool? flag = (DecompressionInfoCallback != null) ? new bool?(DecompressionInfoCallback(num, zipFile.Count)) : null;
								if (flag != null && flag.Value)
								{
									return;
								}
							}
						}
					}
				}
				if (DecompressionInfoCallback != null)
				{
					DecompressionInfoCallback(zipFile.Count, zipFile.Count);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000283C File Offset: 0x00000A3C
		public static bool IsZipFile(string filePath)
		{
			string extension = Path.GetExtension(filePath);
			string a = (extension != null) ? extension.ToLowerInvariant() : null;
			return a == ".zip" || a == ".images";
		}

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000C4 RID: 196
		public delegate bool DecompressionInfo(int finishCount, int totalCount);
	}
}
