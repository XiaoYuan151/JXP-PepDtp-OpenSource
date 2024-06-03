using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace JXP.PepDtp.Properties
{
	// Token: 0x0200000C RID: 12
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000090C7 File Offset: 0x000072C7
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000045 RID: 69
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
