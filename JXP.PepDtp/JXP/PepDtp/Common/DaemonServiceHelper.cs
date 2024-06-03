using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using JXP.Logs;
using JXP.Models;
using JXP.Networks;
using JXP.PepDtp.Paramter;
using JXP.PepUtility;
using JXP.Threading;
using Newtonsoft.Json;

namespace JXP.PepDtp.Common
{
	// Token: 0x020000A6 RID: 166
	public class DaemonServiceHelper
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002D8FE File Offset: 0x0002BAFE
		public static void StartPipeNotifyServerAsync()
		{
			ThreadEx.Run(delegate()
			{
				try
				{
					DaemonServiceHelper.Setup(DaemonServiceHelper.mServiceName);
					DaemonServiceHelper.ServiceStart(DaemonServiceHelper.mServiceName);
					if (NetManager.Instance.StartPipeClient("smartTeachingPipe"))
					{
						string jsonData = JsonConvert.SerializeObject(new PipeProcessModel
						{
							ActiveUser = CommonParamter.Instance.LoginUserId,
							AppKey = AppConsts.JXP_APPKEY,
							ProductId = AppConsts.JXP_PRODUCTID,
							ProductVersion = UtilityHelper.GetVersion(),
							ProcessName = Process.GetCurrentProcess().ProcessName,
							WinHandle = Process.GetCurrentProcess().Handle.ToInt64(),
							States = States.Join
						});
						NetManager.Instance.SendPipeDataToServer(new PipeMessagePacket
						{
							JsonData = jsonData
						});
					}
				}
				catch (Exception arg)
				{
					LogHelper.Instance.Error(string.Format("给守护进程发送启动消息失败:[{0}]", arg));
				}
			});
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002D928 File Offset: 0x0002BB28
		public static void ExitPipeNotifyServer()
		{
			try
			{
				if (NetManager.Instance.StartPipeClient("smartTeachingPipe"))
				{
					string jsonData = JsonConvert.SerializeObject(new PipeProcessModel
					{
						ActiveUser = CommonParamter.Instance.LoginUserId,
						AppKey = AppConsts.JXP_APPKEY,
						ProductId = AppConsts.JXP_PRODUCTID,
						ProductVersion = UtilityHelper.GetVersion(),
						ProcessName = Process.GetCurrentProcess().ProcessName,
						WinHandle = Process.GetCurrentProcess().Handle.ToInt64(),
						States = States.Leave
					});
					NetManager.Instance.SendPipeDataToServer(new PipeMessagePacket
					{
						JsonData = jsonData
					});
					NetManager.Instance.StopPipeClient();
				}
			}
			catch (Exception arg)
			{
				LogHelper.Instance.Error(string.Format("客户端退出通知守护进程失败:[{0}]", arg));
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002DA04 File Offset: 0x0002BC04
		private static void ServiceStart(string serviceName)
		{
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				if (serviceController.Status == ServiceControllerStatus.Stopped)
				{
					serviceController.Start();
				}
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002DA44 File Offset: 0x0002BC44
		private static void ServiceStop(string serviceName)
		{
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				if (serviceController.Status == ServiceControllerStatus.Running)
				{
					serviceController.Stop();
				}
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002DA84 File Offset: 0x0002BC84
		private static void Setup(string servicesName)
		{
			if (DaemonServiceHelper.IsServiceExisted(servicesName))
			{
				return;
			}
			if (!File.Exists(DaemonServiceHelper.mInstallServerFilePath) && File.Exists(DaemonServiceHelper._serviceFilePath))
			{
				string directoryName = Path.GetDirectoryName(DaemonServiceHelper.mInstallServerFilePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				File.Copy(DaemonServiceHelper._serviceFilePath, DaemonServiceHelper.mInstallServerFilePath);
			}
			DaemonServiceHelper.InstallService(DaemonServiceHelper.mInstallServerFilePath);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
		private static void InstallService(string serviceFilePath)
		{
			using (AssemblyInstaller assemblyInstaller = new AssemblyInstaller())
			{
				assemblyInstaller.UseNewContext = true;
				assemblyInstaller.Path = serviceFilePath;
				IDictionary dictionary = new Hashtable();
				assemblyInstaller.Install(dictionary);
				assemblyInstaller.Commit(dictionary);
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		private static void UninstallService(string serviceFilePath)
		{
			using (AssemblyInstaller assemblyInstaller = new AssemblyInstaller())
			{
				assemblyInstaller.UseNewContext = true;
				assemblyInstaller.Path = serviceFilePath;
				assemblyInstaller.Uninstall(null);
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002DB80 File Offset: 0x0002BD80
		private static bool IsServiceExisted(string servicesName)
		{
			ServiceController[] services = ServiceController.GetServices();
			for (int i = 0; i < services.Length; i++)
			{
				if (services[i].ServiceName.ToLower() == ((servicesName != null) ? servicesName.ToLower() : null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400049D RID: 1181
		private static string _serviceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pep.WindowsService.exe");

		// Token: 0x0400049E RID: 1182
		private static string mInstallServerFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pep\\service\\Pep.WindowsService.exe");

		// Token: 0x0400049F RID: 1183
		private static string mServiceName = "SmartTeachingProtect";
	}
}
