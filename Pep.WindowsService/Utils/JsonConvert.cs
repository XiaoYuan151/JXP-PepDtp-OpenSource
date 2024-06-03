using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Pep.WindowsService.Utils
{
	// Token: 0x0200001C RID: 28
	internal class JsonConvert
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000038FA File Offset: 0x00001AFA
		private static MemoryStream GenerateStreamFromString(string value)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""))
			{
				Position = 0L
			};
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003920 File Offset: 0x00001B20
		public static T DeserializeObject<T>(string json) where T : class
		{
			T result;
			try
			{
				using (MemoryStream memoryStream = JsonConvert.GenerateStreamFromString(json))
				{
					result = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream));
				}
			}
			catch (Exception)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003988 File Offset: 0x00001B88
		public static string Serializer<T>(T value)
		{
			string result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new DataContractJsonSerializer(typeof(T)).WriteObject(memoryStream, value);
					memoryStream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
