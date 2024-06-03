using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace JXP.ReleaseUpdate
{
	// Token: 0x02000003 RID: 3
	public class JsonHelper
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002881 File Offset: 0x00000A81
		public List<T> JSONStringToList<T>(string JsonStr)
		{
			if (string.IsNullOrEmpty(JsonStr) || string.IsNullOrEmpty(JsonStr.Trim()))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<List<T>>(JsonStr);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000028A0 File Offset: 0x00000AA0
		public T JsonsDeserialize<T>(string JsonStr) where T : class
		{
			if (string.IsNullOrEmpty(JsonStr) || string.IsNullOrEmpty(JsonStr.Trim()))
			{
				return default(T);
			}
			return JsonConvert.DeserializeObject<T>(JsonStr);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028D2 File Offset: 0x00000AD2
		public string ListToJson<T>(List<T> t)
		{
			if (t == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(t);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000028D2 File Offset: 0x00000AD2
		public string ObservableCollectionToJson<T>(ObservableCollection<T> t)
		{
			if (t == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(t);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028DF File Offset: 0x00000ADF
		public string ScriptSerialize<T>(T t) where T : class
		{
			if (t == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(t);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028F6 File Offset: 0x00000AF6
		public string SerializeString<T>(T t)
		{
			if (t == null)
			{
				return string.Empty;
			}
			return JsonConvert.SerializeObject(t);
		}
	}
}
