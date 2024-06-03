using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;

namespace JXP.DataAnalytics.Utility
{
	// Token: 0x02000027 RID: 39
	internal static class HttpUtility
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003E24 File Offset: 0x00002024
		public static NameValueCollection ParseQueryString(string query)
		{
			return HttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003E34 File Offset: 0x00002034
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (query.Length > 0 && query[0] == '?')
			{
				query = query.Substring(1);
			}
			return new HttpUtility.HttpValueCollection(query, encoding);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003E81 File Offset: 0x00002081
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003E94 File Offset: 0x00002094
		public static string UrlPathEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			int num = str.IndexOf('?');
			if (num >= 0)
			{
				return HttpUtility.UrlPathEncode(str.Substring(0, num)) + str.Substring(num);
			}
			return HttpUtility.UrlEncodeSpaces(HttpUtility.UrlEncodeNonAscii(str, Encoding.UTF8));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003EDD File Offset: 0x000020DD
		public static string UrlEncode(string str, Encoding encoding)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(str, encoding));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003EF5 File Offset: 0x000020F5
		public static string UrlEncodeUnicode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlEncodeUnicodeStringToStringInternal(str, false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003F04 File Offset: 0x00002104
		private static string UrlEncodeUnicodeStringToStringInternal(string s, bool ignoreAscii)
		{
			int length = s.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if ((c & 'ﾀ') == '\0')
				{
					if (ignoreAscii || HttpUtility.IsSafe(c))
					{
						stringBuilder.Append(c);
					}
					else if (c == ' ')
					{
						stringBuilder.Append('+');
					}
					else
					{
						stringBuilder.Append('%');
						stringBuilder.Append(HttpUtility.IntToHex((int)(c >> 4 & '\u000f')));
						stringBuilder.Append(HttpUtility.IntToHex((int)(c & '\u000f')));
					}
				}
				else
				{
					stringBuilder.Append("%u");
					stringBuilder.Append(HttpUtility.IntToHex((int)(c >> 12 & '\u000f')));
					stringBuilder.Append(HttpUtility.IntToHex((int)(c >> 8 & '\u000f')));
					stringBuilder.Append(HttpUtility.IntToHex((int)(c >> 4 & '\u000f')));
					stringBuilder.Append(HttpUtility.IntToHex((int)(c & '\u000f')));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003FF4 File Offset: 0x000021F4
		private static string UrlEncodeNonAscii(string str, Encoding e)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			byte[] array = e.GetBytes(str);
			array = HttpUtility.UrlEncodeBytesToBytesInternalNonAscii(array, 0, array.Length, false);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004034 File Offset: 0x00002234
		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004058 File Offset: 0x00002258
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return HttpUtility.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000407D File Offset: 0x0000227D
		public static string UrlDecode(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return HttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004098 File Offset: 0x00002298
		public static string UrlDecode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeStringFromStringInternal(str, e);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000040A8 File Offset: 0x000022A8
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!HttpUtility.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (HttpUtility.IsSafe(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)HttpUtility.IntToHex(b >> 4 & 15);
					array[num3++] = (byte)HttpUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004173 File Offset: 0x00002373
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004184 File Offset: 0x00002384
		private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (HttpUtility.IsNonAsciiByte(bytes[offset + i]))
				{
					num++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num * 2];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				if (HttpUtility.IsNonAsciiByte(b))
				{
					array[num2++] = 37;
					array[num2++] = (byte)HttpUtility.IntToHex(b >> 4 & 15);
					array[num2++] = (byte)HttpUtility.IntToHex((int)(b & 15));
				}
				else
				{
					array[num2++] = b;
				}
			}
			return array;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004220 File Offset: 0x00002420
		private static string UrlDecodeStringFromStringInternal(string s, Encoding e)
		{
			int length = s.Length;
			HttpUtility.UrlDecoder urlDecoder = new HttpUtility.UrlDecoder(length, e);
			int i = 0;
			while (i < length)
			{
				char c = s[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_106;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_106;
				}
				if (s[i + 1] == 'u' && i < length - 5)
				{
					int num = HttpUtility.HexToInt(s[i + 2]);
					int num2 = HttpUtility.HexToInt(s[i + 3]);
					int num3 = HttpUtility.HexToInt(s[i + 4]);
					int num4 = HttpUtility.HexToInt(s[i + 5]);
					if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
					{
						goto IL_106;
					}
					c = (char)(num << 12 | num2 << 8 | num3 << 4 | num4);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num5 = HttpUtility.HexToInt(s[i + 1]);
					int num6 = HttpUtility.HexToInt(s[i + 2]);
					if (num5 < 0 || num6 < 0)
					{
						goto IL_106;
					}
					byte b = (byte)(num5 << 4 | num6);
					i += 2;
					urlDecoder.AddByte(b);
				}
				IL_120:
				i++;
				continue;
				IL_106:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_120;
				}
				urlDecoder.AddChar(c);
				goto IL_120;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000435E File Offset: 0x0000255E
		private static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004394 File Offset: 0x00002594
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000043AC File Offset: 0x000025AC
		internal static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x02000052 RID: 82
		private class UrlDecoder
		{
			// Token: 0x0600025F RID: 607 RVA: 0x00007CA0 File Offset: 0x00005EA0
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06000260 RID: 608 RVA: 0x00007CEE File Offset: 0x00005EEE
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06000261 RID: 609 RVA: 0x00007D10 File Offset: 0x00005F10
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x06000262 RID: 610 RVA: 0x00007D48 File Offset: 0x00005F48
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x06000263 RID: 611 RVA: 0x00007D87 File Offset: 0x00005F87
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x040000EF RID: 239
			private int _bufferSize;

			// Token: 0x040000F0 RID: 240
			private int _numChars;

			// Token: 0x040000F1 RID: 241
			private char[] _charBuffer;

			// Token: 0x040000F2 RID: 242
			private int _numBytes;

			// Token: 0x040000F3 RID: 243
			private byte[] _byteBuffer;

			// Token: 0x040000F4 RID: 244
			private Encoding _encoding;
		}

		// Token: 0x02000053 RID: 83
		[Serializable]
		private class HttpValueCollection : NameValueCollection
		{
			// Token: 0x06000264 RID: 612 RVA: 0x00007DB9 File Offset: 0x00005FB9
			internal HttpValueCollection(string str, Encoding encoding) : base(StringComparer.OrdinalIgnoreCase)
			{
				if (!string.IsNullOrEmpty(str))
				{
					this.FillFromString(str, true, encoding);
				}
				base.IsReadOnly = false;
			}

			// Token: 0x06000265 RID: 613 RVA: 0x00007DDE File Offset: 0x00005FDE
			protected HttpValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}

			// Token: 0x06000266 RID: 614 RVA: 0x00007DE8 File Offset: 0x00005FE8
			internal void FillFromString(string s, bool urlencoded, Encoding encoding)
			{
				int num = (s != null) ? s.Length : 0;
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						char c = s[i];
						if (c == '=')
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (c == '&')
						{
							break;
						}
						i++;
					}
					string text = null;
					string text2;
					if (num3 >= 0)
					{
						text = s.Substring(num2, num3 - num2);
						text2 = s.Substring(num3 + 1, i - num3 - 1);
					}
					else
					{
						text2 = s.Substring(num2, i - num2);
					}
					if (urlencoded)
					{
						base.Add(HttpUtility.UrlDecode(text, encoding), HttpUtility.UrlDecode(text2, encoding));
					}
					else
					{
						base.Add(text, text2);
					}
					if (i == num - 1 && s[i] == '&')
					{
						base.Add(null, string.Empty);
					}
				}
			}

			// Token: 0x06000267 RID: 615 RVA: 0x00007EB5 File Offset: 0x000060B5
			public override string ToString()
			{
				return this.ToString(true, null);
			}

			// Token: 0x06000268 RID: 616 RVA: 0x00007EC0 File Offset: 0x000060C0
			private string ToString(bool urlencoded, IDictionary excludeKeys)
			{
				int count = this.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string text = this.GetKey(i);
					if (excludeKeys == null || text == null || excludeKeys[text] == null)
					{
						if (urlencoded)
						{
							text = HttpUtility.UrlEncodeUnicode(text);
						}
						string value = (!string.IsNullOrEmpty(text)) ? (text + "=") : string.Empty;
						ArrayList arrayList = (ArrayList)base.BaseGet(i);
						int num = (arrayList != null) ? arrayList.Count : 0;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append('&');
						}
						if (num == 1)
						{
							stringBuilder.Append(value);
							string text2 = (string)arrayList[0];
							if (urlencoded)
							{
								text2 = HttpUtility.UrlEncodeUnicode(text2);
							}
							stringBuilder.Append(text2);
						}
						else if (num == 0)
						{
							stringBuilder.Append(value);
						}
						else
						{
							for (int j = 0; j < num; j++)
							{
								if (j > 0)
								{
									stringBuilder.Append('&');
								}
								stringBuilder.Append(value);
								string text2 = (string)arrayList[j];
								if (urlencoded)
								{
									text2 = HttpUtility.UrlEncodeUnicode(text2);
								}
								stringBuilder.Append(text2);
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}
