using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Pep.WindowsService
{
	// Token: 0x0200001A RID: 26
	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000381C File Offset: 0x00001A1C
		public ProjectInstaller()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000382A File Offset: 0x00001A2A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000384C File Offset: 0x00001A4C
		private void InitializeComponent()
		{
			this.serviceProcessInstaller = new ServiceProcessInstaller();
			this.SmartTeachingProtectServiceInstaller = new ServiceInstaller();
			this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			this.serviceProcessInstaller.Password = null;
			this.serviceProcessInstaller.Username = null;
			this.SmartTeachingProtectServiceInstaller.Description = "智慧平台进程守护";
			this.SmartTeachingProtectServiceInstaller.DisplayName = "SmartTeachingProtect";
			this.SmartTeachingProtectServiceInstaller.ServiceName = "SmartTeachingProtect";
			this.SmartTeachingProtectServiceInstaller.StartType = ServiceStartMode.Automatic;
			base.Installers.AddRange(new Installer[]
			{
				this.serviceProcessInstaller,
				this.SmartTeachingProtectServiceInstaller
			});
		}

		// Token: 0x04000033 RID: 51
		private IContainer components;

		// Token: 0x04000034 RID: 52
		private ServiceProcessInstaller serviceProcessInstaller;

		// Token: 0x04000035 RID: 53
		public ServiceInstaller SmartTeachingProtectServiceInstaller;
	}
}
