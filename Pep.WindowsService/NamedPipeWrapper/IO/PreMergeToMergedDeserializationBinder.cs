using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace NamedPipeWrapper.IO
{
	// Token: 0x02000011 RID: 17
	internal sealed class PreMergeToMergedDeserializationBinder : SerializationBinder
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003014 File Offset: 0x00001214
		public override Type BindToType(string assemblyName, string typeName)
		{
			string fullName = Assembly.GetExecutingAssembly().FullName;
			return Type.GetType(string.Format("{0}, {1}", typeName, fullName));
		}
	}
}
