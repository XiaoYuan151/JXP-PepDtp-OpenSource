using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JXP.Utilities
{
	// Token: 0x020000AA RID: 170
	internal class EncryptorHelper
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0002E034 File Offset: 0x0002C234
		public EncryptorHelper()
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002E047 File Offset: 0x0002C247
		internal EncryptorHelper(string secretKey) : this()
		{
			if (!string.IsNullOrEmpty(secretKey))
			{
				this.mEncryptorKey = secretKey;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002E05E File Offset: 0x0002C25E
		private RijndaelManaged GetNewAES()
		{
			return new RijndaelManaged
			{
				Mode = CipherMode.CFB,
				Padding = PaddingMode.None,
				BlockSize = 128,
				Key = this.GetAESKey(),
				IV = this.GetIV()
			};
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002E096 File Offset: 0x0002C296
		private byte[] GetAESKey()
		{
			return Encoding.UTF8.GetBytes(this.mEncryptorKey);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		private byte[] GetIV()
		{
			return new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15
			};
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002E0BC File Offset: 0x0002C2BC
		private void CheckParas(string strSrcFile, string strDestFile)
		{
			if (string.IsNullOrEmpty(strSrcFile) || !File.Exists(strSrcFile))
			{
				throw new FileNotFoundException("源文件不存在!");
			}
			if (string.IsNullOrEmpty(strDestFile))
			{
				throw new ArgumentNullException("目标文件路径不能为空!");
			}
			string directoryName = Path.GetDirectoryName(strDestFile);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002E10D File Offset: 0x0002C30D
		private bool CheckSpecialFile(string strSrcFile)
		{
			return strSrcFile.ToLower().EndsWith(".mp4");
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002E120 File Offset: 0x0002C320
		internal void FileEncrypt13(string strSrcFile, string strDestFileTemp, string strDescription, string strDestFileresult)
		{
			int num = 1;
			Random random = new Random();
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open))
			{
				long length = fileStream.Length;
				byte[] buffer = new byte[length];
				using (FileStream fileStream2 = new FileStream(strDestFileTemp, FileMode.Create, FileAccess.Write))
				{
					int num3;
					for (;;)
					{
						int num2 = Convert.ToInt32(Math.Pow(3.0, (double)num) + (double)num);
						if ((long)num2 > length)
						{
							break;
						}
						num3 = fileStream.Read(buffer, 0, num2);
						byte[] array = new byte[1];
						random.NextBytes(array);
						fileStream2.Write(buffer, 0, num3);
						fileStream2.Write(array, 0, array.Length);
						num++;
					}
					num3 = fileStream.Read(buffer, 0, Convert.ToInt32(length));
					fileStream2.Write(buffer, 0, num3);
				}
			}
			using (FileStream fileStream3 = new FileStream(strDestFileTemp, FileMode.Open))
			{
				byte[] array2 = new byte[256];
				byte[] array3 = new byte[256];
				using (FileStream fileStream4 = new FileStream(strDestFileresult, FileMode.Create, FileAccess.Write))
				{
					this.WriteEncryptHead(fileStream4, strDescription, 13);
					for (;;)
					{
						int num3 = fileStream3.Read(array2, 0, 256);
						if (num3 == 0)
						{
							break;
						}
						if (num3 == 256)
						{
							for (int i = 0; i < 16; i++)
							{
								for (int j = 0; j < 16; j++)
								{
									Buffer.SetByte(array3, 16 * i + j, array2[i + 16 * j]);
								}
							}
							fileStream4.Write(array3, 0, array3.Length);
						}
						else
						{
							fileStream4.Write(array2, 0, num3);
						}
					}
				}
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002E2E4 File Offset: 0x0002C4E4
		internal void FileEncrypt11(string strSrcFile, string strDestFile, string strDescription)
		{
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateEncryptor();
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open, FileAccess.Read))
			{
				using (FileStream fileStream2 = new FileStream(strDestFile, FileMode.Create, FileAccess.Write))
				{
					this.WriteEncryptHead(fileStream2, strDescription, 11);
					byte[] array = new byte[1024];
					int i = fileStream.Read(array, 0, 1024);
					if (i == 1024)
					{
						byte[] array2 = cryptoTransform.TransformFinalBlock(array, 0, i);
						fileStream2.Write(array2, 0, array2.Length);
					}
					else
					{
						fileStream2.Write(array, 0, i);
					}
					for (i = fileStream.Read(array, 0, 1024); i > 0; i = fileStream.Read(array, 0, 1024))
					{
						fileStream2.Write(array, 0, i);
					}
				}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		internal void FileEncrypt12(string strSrcFile, string strDestFile, string strDescription)
		{
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateEncryptor();
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open))
			{
				byte[] array = new byte[1024];
				byte[] array2 = new byte[1024];
				using (FileStream fileStream2 = new FileStream(strDestFile, FileMode.Create, FileAccess.Write))
				{
					this.WriteEncryptHead(fileStream2, strDescription, 12);
					for (;;)
					{
						int num = fileStream.Read(array, 0, 1024);
						if (num == 0)
						{
							break;
						}
						if (num == 1024)
						{
							array2 = cryptoTransform.TransformFinalBlock(array, 0, num);
							fileStream2.Write(array2, 0, array2.Length);
						}
						else
						{
							fileStream2.Write(array, 0, num);
						}
					}
				}
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002E488 File Offset: 0x0002C688
		internal string StringEncrypt(string plainString)
		{
			return this.StringEncrypt(plainString, "rjsz2012+$&#2017");
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002E498 File Offset: 0x0002C698
		internal string StringEncrypt(string plainString, string encryptKey)
		{
			string result;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(encryptKey));
					tripleDESCryptoServiceProvider.Key = key;
					tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
					byte[] bytes = Encoding.UTF8.GetBytes(plainString);
					result = Convert.ToBase64String(tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
				}
			}
			return result;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002E530 File Offset: 0x0002C730
		private void WriteEncryptHead(FileStream fswrite, string strDescription, int iEnType)
		{
			byte[] bytes = Encoding.UTF8.GetBytes("rjsz");
			byte[] bytes2 = Encoding.UTF8.GetBytes(strDescription);
			byte[] bytes3 = BitConverter.GetBytes(iEnType);
			byte[] bytes4 = BitConverter.GetBytes(bytes2.Length);
			fswrite.Write(bytes, 0, bytes.Length);
			fswrite.Write(bytes3, 0, 1);
			fswrite.Write(bytes4, 0, 4);
			fswrite.Write(bytes2, 0, bytes2.Length);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002E594 File Offset: 0x0002C794
		internal byte[] DecryptCipherData(byte[] cipherData)
		{
			if (cipherData == null || cipherData.Length != 1024)
			{
				return cipherData;
			}
			byte[] result;
			using (RijndaelManaged newAES = this.GetNewAES())
			{
				using (ICryptoTransform cryptoTransform = newAES.CreateDecryptor())
				{
					result = cryptoTransform.TransformFinalBlock(cipherData, 0, cipherData.Length);
				}
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002E600 File Offset: 0x0002C800
		internal string FileDecrypt11(string strSrcFile, string strDestFile)
		{
			string result = string.Empty;
			int num = 9;
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateDecryptor();
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open))
			{
				byte[] array = new byte[1024];
				int num2 = fileStream.Read(array, 0, 9);
				if (num2 != 9)
				{
					return result;
				}
				int num3 = this.ParseEncryptHead(array);
				if (num3 > 0)
				{
					num2 = fileStream.Read(array, 0, num3);
					if (num2 > 0)
					{
						result = Encoding.UTF8.GetString(array, 0, num2);
						num += num2;
					}
				}
				using (FileStream fileStream2 = new FileStream(strDestFile, FileMode.Create, FileAccess.Write))
				{
					num2 = fileStream.Read(array, 0, 1024);
					if (num2 == 1024)
					{
						byte[] array2 = cryptoTransform.TransformFinalBlock(array, 0, num2);
						fileStream2.Write(array2, 0, array2.Length);
					}
					else
					{
						fileStream2.Write(array, 0, num2);
					}
					for (;;)
					{
						num2 = fileStream.Read(array, 0, 1024);
						if (num2 == 0)
						{
							break;
						}
						fileStream2.Write(array, 0, num2);
					}
				}
			}
			return result;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002E72C File Offset: 0x0002C92C
		internal string FileDecrypt12(string strSrcFile, string strDestFile)
		{
			string result = string.Empty;
			int num = 9;
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateDecryptor();
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				byte[] array = new byte[1024];
				int num2 = fileStream.Read(array, 0, 9);
				if (num2 != 9)
				{
					return result;
				}
				int num3 = this.ParseEncryptHead(array);
				if (num3 > 0)
				{
					num2 = fileStream.Read(array, 0, num3);
					if (num2 > 0)
					{
						result = Encoding.UTF8.GetString(array, 0, num2);
						num += num2;
					}
				}
				long length = fileStream.Length;
				string directoryName = Path.GetDirectoryName(strDestFile);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using (FileStream fileStream2 = new FileStream(strDestFile, FileMode.Create, FileAccess.Write))
				{
					for (;;)
					{
						num2 = fileStream.Read(array, 0, 1024);
						if (num2 == 0)
						{
							break;
						}
						if (num2 == 1024)
						{
							byte[] array2 = new byte[1024];
							array2 = cryptoTransform.TransformFinalBlock(array, 0, num2);
							fileStream2.Write(array2, 0, array2.Length);
						}
						else
						{
							fileStream2.Write(array, 0, num2);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002E86C File Offset: 0x0002CA6C
		public string StringDecrypt(string cipherString)
		{
			return this.StringDecrypt(cipherString, "rjsz2012+$&#2017");
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002E87C File Offset: 0x0002CA7C
		internal string StringDecrypt(string cipherString, string encryptKey)
		{
			string text = cipherString.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
			if (text.Length % 4 > 0)
			{
				text = text.PadRight(text.Length + 4 - text.Length % 4, '=');
			}
			string @string;
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(encryptKey));
					tripleDESCryptoServiceProvider.Key = key;
					tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
					byte[] array = Convert.FromBase64String(text);
					@string = Encoding.UTF8.GetString(tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length));
				}
			}
			return @string;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002E96C File Offset: 0x0002CB6C
		private int ParseEncryptHead(byte[] buffer)
		{
			if (Encoding.UTF8.GetString(buffer, 0, 4).CompareTo("rjsz") != 0)
			{
				throw new InvalidDataException("未找到需要解密的内容！");
			}
			if (buffer[4].CompareTo(11) > 9)
			{
				throw new InvalidDataException("解密方法已过期！");
			}
			return BitConverter.ToInt32(buffer, 5);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
		internal byte[] EncryptPlainData(byte[] plainData)
		{
			if (plainData == null || plainData.Length != 1024)
			{
				return plainData;
			}
			byte[] result;
			using (RijndaelManaged newAES = this.GetNewAES())
			{
				using (ICryptoTransform cryptoTransform = newAES.CreateEncryptor())
				{
					result = cryptoTransform.TransformFinalBlock(plainData, 0, plainData.Length);
				}
			}
			return result;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002EA30 File Offset: 0x0002CC30
		internal void FileEncrypt(string strSrcFile, string strDestFile)
		{
			this.FileEncrypt(strSrcFile, strDestFile, Path.GetFileName(strSrcFile));
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002EA40 File Offset: 0x0002CC40
		internal void FileEncrypt(string strSrcFile, string strDestFile, string strDescription)
		{
			this.CheckParas(strSrcFile, strDestFile);
			if (this.CheckSpecialFile(strSrcFile))
			{
				this.FileEncrypt11(strSrcFile, strDestFile, strDescription);
				return;
			}
			this.FileEncrypt12(strSrcFile, strDestFile, strDescription);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002EA66 File Offset: 0x0002CC66
		internal string FileDecrypt(string strSrcFile, string strDestFile)
		{
			this.CheckParas(strSrcFile, strDestFile);
			if (this.CheckSpecialFile(strSrcFile))
			{
				return this.FileDecrypt11(strSrcFile, strDestFile);
			}
			return this.FileDecrypt12(strSrcFile, strDestFile);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002EA8A File Offset: 0x0002CC8A
		internal byte[] FileDecrypt(string strSrcFile)
		{
			this.CheckParamter(strSrcFile);
			if (this.CheckSpecialFile(strSrcFile))
			{
				return this.FileDecrypt11(strSrcFile);
			}
			return this.FileDecrypt12(strSrcFile);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002EAAC File Offset: 0x0002CCAC
		private byte[] FileDecrypt11(string strSrcFile)
		{
			string empty = string.Empty;
			int num = 9;
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateDecryptor();
			byte[] result;
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open))
			{
				byte[] array = new byte[1024];
				int num2 = fileStream.Read(array, 0, 9);
				if (num2 == 9)
				{
					int num3 = this.ParseEncryptHead(array);
					if (num3 > 0)
					{
						num2 = fileStream.Read(array, 0, num3);
						if (num2 > 0)
						{
							Encoding.UTF8.GetString(array, 0, num2);
							num += num2;
						}
					}
					using (MemoryStream memoryStream = new MemoryStream((int)fileStream.Length - num))
					{
						num2 = fileStream.Read(array, 0, 1024);
						if (num2 == 1024)
						{
							byte[] array2 = cryptoTransform.TransformFinalBlock(array, 0, num2);
							memoryStream.Write(array2, 0, array2.Length);
						}
						else
						{
							memoryStream.Write(array, 0, num2);
						}
						for (;;)
						{
							num2 = fileStream.Read(array, 0, 1024);
							if (num2 == 0)
							{
								break;
							}
							memoryStream.Write(array, 0, num2);
						}
						result = memoryStream.GetBuffer();
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		internal byte[] FileDecrypt12(string strSrcFile)
		{
			string empty = string.Empty;
			int num = 9;
			ICryptoTransform cryptoTransform = this.GetNewAES().CreateDecryptor();
			byte[] result;
			using (FileStream fileStream = new FileStream(strSrcFile, FileMode.Open))
			{
				byte[] array = new byte[1024];
				int num2 = fileStream.Read(array, 0, 9);
				if (num2 == 9)
				{
					int num3 = this.ParseEncryptHead(array);
					if (num3 > 0)
					{
						num2 = fileStream.Read(array, 0, num3);
						if (num2 > 0)
						{
							Encoding.UTF8.GetString(array, 0, num2);
							num += num2;
						}
					}
					using (MemoryStream memoryStream = new MemoryStream((int)fileStream.Length - num))
					{
						for (;;)
						{
							num2 = fileStream.Read(array, 0, 1024);
							if (num2 == 0)
							{
								break;
							}
							if (num2 == 1024)
							{
								byte[] array2 = new byte[1024];
								array2 = cryptoTransform.TransformFinalBlock(array, 0, num2);
								memoryStream.Write(array2, 0, array2.Length);
							}
							else
							{
								memoryStream.Write(array, 0, num2);
							}
						}
						result = memoryStream.GetBuffer();
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002ED00 File Offset: 0x0002CF00
		private void CheckParamter(string strSrcFile)
		{
			if (string.IsNullOrEmpty(strSrcFile) || !File.Exists(strSrcFile))
			{
				throw new FileNotFoundException("源文件不存在!");
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002ED20 File Offset: 0x0002CF20
		internal string GetTextBookPwd(string bookId)
		{
			Md5Helper md5Helper = new Md5Helper();
			string strDataIn = "rjsz" + bookId + "rjsz2012+$&#2017";
			return md5Helper.GetStringMd5Value(strDataIn);
		}

		// Token: 0x040004A3 RID: 1187
		private const int CST_INT_ENCRYPTOR_LENGTH = 1024;

		// Token: 0x040004A4 RID: 1188
		private const int CST_INT_ENCRYPTOR_LENGTH_13 = 256;

		// Token: 0x040004A5 RID: 1189
		private const int CST_INT_ENCRYPTOR_TYPE_11 = 11;

		// Token: 0x040004A6 RID: 1190
		private const int CST_INT_ENCRYPTOR_TYPE_12 = 12;

		// Token: 0x040004A7 RID: 1191
		private const int CST_INT_ENCRYPTOR_TYPE_13 = 13;

		// Token: 0x040004A8 RID: 1192
		private const string CST_STR_ENCRYPTOR_KEY = "rjsz2012+$&#2017";

		// Token: 0x040004A9 RID: 1193
		private const string CST_STR_ENCRYPTOR_FLAG = "rjsz";

		// Token: 0x040004AA RID: 1194
		private string mEncryptorKey = "rjsz2012+$&#2017";
	}
}
