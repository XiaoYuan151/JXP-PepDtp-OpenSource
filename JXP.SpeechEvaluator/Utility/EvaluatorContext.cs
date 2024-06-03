using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using JXP.Logs;
using JXP.SpeechEvaluator.Model.Xunfei;
using JXP.SpeechEvaluator.Utility.XfSpeechEngine;
using NAudio.Wave;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000036 RID: 54
	public static class EvaluatorContext
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000085C6 File Offset: 0x000067C6
		static EvaluatorContext()
		{
			EvaluatorContext.BaseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SpeechEvaluator");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000085EC File Offset: 0x000067EC
		public static string CreateDownloadDir(string srcUrl)
		{
			string text = Path.Combine(EvaluatorContext.BaseDir, "Data", UrlUtility.GenerateId(srcUrl, true));
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008620 File Offset: 0x00006820
		public static string CreateIndexJsonDir()
		{
			string text = Path.Combine(EvaluatorContext.BaseDir, "Index");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008650 File Offset: 0x00006850
		public static string CreateRecordingDir(string dirName = "")
		{
			if (string.IsNullOrEmpty(dirName))
			{
				dirName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			}
			string text = Path.Combine(EvaluatorContext.BaseDir, "Recording", dirName);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000869C File Offset: 0x0000689C
		public static void DeleteRecordingBaseDir(uint days = 0U)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(EvaluatorContext.BaseDir, "Recording"));
				if (directoryInfo.Exists)
				{
					if (days == 0U)
					{
						EvaluatorContext.DeleteFolder(directoryInfo.FullName);
					}
					else
					{
						foreach (DirectoryInfo directoryInfo2 in directoryInfo.EnumerateDirectories())
						{
							if (DateTime.Now.AddDays((double)(-(double)((ulong)days))) > directoryInfo2.CreationTime)
							{
								EvaluatorContext.DeleteFolder(directoryInfo2.FullName);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000874C File Offset: 0x0000694C
		public static void ClearCache()
		{
			try
			{
				if (Directory.Exists(EvaluatorContext.BaseDir))
				{
					foreach (string folder in Directory.EnumerateDirectories(EvaluatorContext.BaseDir))
					{
						EvaluatorContext.DeleteFolder(folder);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000087BC File Offset: 0x000069BC
		public static ReadChapter ConvertXmlToObject(string xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_chapter");
			if (xmlNode == null)
			{
				xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_sentence");
			}
			if (xmlNode == null)
			{
				return null;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RecPaper));
			ReadChapter result;
			using (StringReader stringReader = new StringReader(xmlNode.InnerXml))
			{
				RecPaper recPaper = (RecPaper)xmlSerializer.Deserialize(stringReader);
				result = ((recPaper != null) ? recPaper.ReadChapter : null);
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000884C File Offset: 0x00006A4C
		public static RecPaper ConvertXmlToObject2(string xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_chapter");
			if (xmlNode == null)
			{
				xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_sentence");
			}
			if (xmlNode == null)
			{
				return null;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RecPaper));
			RecPaper result;
			using (StringReader stringReader = new StringReader(xmlNode.InnerXml))
			{
				result = (RecPaper)xmlSerializer.Deserialize(stringReader);
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static List<XSentence> ConvertXmlToObject3(string xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_chapter/rec_paper/read_chapter");
			if (xmlNode == null)
			{
				xmlNode = xmlDocument.SelectSingleNode("/xml_result/read_sentence/rec_paper/read_sentence");
			}
			if (xmlNode == null)
			{
				return null;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<XSentence>));
			List<XSentence> result;
			using (StringReader stringReader = new StringReader(xmlNode.InnerXml))
			{
				result = (((List<XSentence>)xmlSerializer.Deserialize(stringReader)) ?? new List<XSentence>());
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000895C File Offset: 0x00006B5C
		public static void DeleteFolder(string folder)
		{
			try
			{
				if (Directory.Exists(folder))
				{
					foreach (string path in Directory.EnumerateFiles(folder))
					{
						try
						{
							File.Delete(path);
						}
						catch
						{
						}
					}
					if (Directory.Exists(folder))
					{
						Directory.Delete(folder, true);
					}
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("文件夹[{0}]删除失败.{1}", folder, arg));
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000089FC File Offset: 0x00006BFC
		public static int GetScore(double realScore)
		{
			int num = (int)Math.Ceiling(realScore * 20.0);
			if (num <= 100)
			{
				return num;
			}
			return 100;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008A24 File Offset: 0x00006C24
		public static int GetScore2(double realScore)
		{
			int num = (int)Math.Ceiling(realScore * 20.0);
			if (num <= 100)
			{
				return num;
			}
			return 100;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008A4C File Offset: 0x00006C4C
		public static int GetScore3(double realScore)
		{
			int num = (int)Math.Ceiling(realScore);
			if (num <= 100)
			{
				return num;
			}
			return 100;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static bool DetectRecordingDevice()
		{
			int deviceCount = WaveIn.DeviceCount;
			if (deviceCount <= 0)
			{
				return false;
			}
			for (int i = 0; i < deviceCount; i++)
			{
				WaveInCapabilities capabilities = WaveIn.GetCapabilities(i);
				LogHelper.Instance.Debug(string.Format("Device {0}: {1}, {2} channels", i, capabilities.ProductName, capabilities.Channels));
			}
			return true;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008AC6 File Offset: 0x00006CC6
		public static void Start()
		{
			ISEServerAgent.Instance.Login();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008AD2 File Offset: 0x00006CD2
		public static void End()
		{
			ISEServerAgent.Instance.Dispose();
		}

		// Token: 0x040000D8 RID: 216
		public static string BaseDir = string.Empty;
	}
}
