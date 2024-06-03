using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace JXP.PepDtp.Properties
{
	// Token: 0x0200000B RID: 11
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00002770 File Offset: 0x00000970
		internal Resources()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000904A File Offset: 0x0000724A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("JXP.PepDtp.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00009076 File Offset: 0x00007276
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000907D File Offset: 0x0000727D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00009085 File Offset: 0x00007285
		internal static string taskresult1_2_x
		{
			get
			{
				return Resources.ResourceManager.GetString("taskresult1_2_x", Resources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000909B File Offset: 0x0000729B
		internal static string textbooks_state1_2_x
		{
			get
			{
				return Resources.ResourceManager.GetString("textbooks_state1_2_x", Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000090B1 File Offset: 0x000072B1
		internal static string userresources1_2_x
		{
			get
			{
				return Resources.ResourceManager.GetString("userresources1_2_x", Resources.resourceCulture);
			}
		}

		// Token: 0x04000043 RID: 67
		private static ResourceManager resourceMan;

		// Token: 0x04000044 RID: 68
		private static CultureInfo resourceCulture;
	}
}
