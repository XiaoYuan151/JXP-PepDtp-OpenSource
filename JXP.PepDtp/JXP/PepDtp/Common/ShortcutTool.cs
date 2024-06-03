using System;
using System.IO;
using System.Runtime.CompilerServices;
using IWshRuntimeLibrary;
using JXP.Logs;
using Microsoft.CSharp.RuntimeBinder;

namespace JXP.PepDtp.Common
{
	// Token: 0x020000A8 RID: 168
	public class ShortcutTool
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x0002DDF4 File Offset: 0x0002BFF4
		public static bool Create(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
		{
			try
			{
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
				string pathLink = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
				WshShell wshShell = (WshShell)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
				if (ShortcutTool.<>o__0.<>p__0 == null)
				{
					ShortcutTool.<>o__0.<>p__0 = CallSite<Func<CallSite, object, IWshShortcut>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(IWshShortcut), typeof(ShortcutTool)));
				}
				IWshShortcut wshShortcut = ShortcutTool.<>o__0.<>p__0.Target(ShortcutTool.<>o__0.<>p__0, wshShell.CreateShortcut(pathLink));
				wshShortcut.TargetPath = targetPath;
				wshShortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
				wshShortcut.WindowStyle = 1;
				wshShortcut.Description = description;
				wshShortcut.IconLocation = (string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation);
				wshShortcut.Save();
				return true;
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("创建主程序快捷方式失败：" + ex.Message);
			}
			return false;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002DEF4 File Offset: 0x0002C0F4
		public static bool Delete(string directory, string shortcutName)
		{
			try
			{
				string path = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				return true;
			}
			catch (Exception ex)
			{
				LogHelper.Instance.Error("删除主程序快捷方式失败：" + ex.Message);
			}
			return false;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002DF58 File Offset: 0x0002C158
		public static bool IsExists(string directory, string shortcutName)
		{
			try
			{
				if (File.Exists(Path.Combine(directory, string.Format("{0}.lnk", shortcutName))))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}
	}
}
