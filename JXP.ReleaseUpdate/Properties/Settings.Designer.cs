using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace JXP.ReleaseUpdate.Properties
{
	// Token: 0x0200000C RID: 12
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003546 File Offset: 0x00001746
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400002C RID: 44
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
