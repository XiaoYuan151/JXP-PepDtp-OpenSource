using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using JXP.DataAnalytics.Activity;
using JXP.DataAnalytics.Bootstrap;
using JXP.DataAnalytics.Platform;
using JXP.DataAnalytics.Tracker;
using JXP.Networks;
using NamedPipeWrapper;
using Pep.WindowsService.Data;
using Pep.WindowsService.Extensions;
using Pep.WindowsService.Log;
using Pep.WindowsService.Utils;

namespace Pep.WindowsService
{
	// Token: 0x02000016 RID: 22
	public class DaemonService : ServiceBase, IServiceDebug
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003340 File Offset: 0x00001540
		public DaemonService()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000335C File Offset: 0x0000155C
		private void _pipeServer_ClientMessage(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> connection, PipeMessagePacket message)
		{
			Console.WriteLine(connection.Name);
			AnalyticsConnectData analyticsConnectData = JsonConvert.DeserializeObject<AnalyticsConnectData>(message.JsonData);
			if (analyticsConnectData == null)
			{
				return;
			}
			SmartTeachingClient byProductId = this._clients.GetByProductId(analyticsConnectData.ProductId);
			States states = analyticsConnectData.States;
			if (states != States.Join)
			{
				if (states != States.Heart)
				{
					if (states != States.Leave)
					{
						throw new ArgumentOutOfRangeException();
					}
					if (byProductId != null)
					{
						this._clients.Remove(byProductId);
						return;
					}
				}
			}
			else
			{
				SmartTeachingClient item = new SmartTeachingClient
				{
					Data = analyticsConnectData,
					Client = connection
				};
				if (byProductId == null)
				{
					this._clients.Add(item);
					return;
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033F0 File Offset: 0x000015F0
		private void _pipeServer_ClientDisconnected(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> connection)
		{
			Console.WriteLine(string.Format("{0}", DateTime.Now));
			SmartTeachingClient byProductPipeId = this._clients.GetByProductPipeId(connection.Id);
			if (byProductPipeId == null)
			{
				return;
			}
			this._clients.Remove(byProductPipeId);
			TrackerManager.RtTracker.OnEventWithConfig(new EventActivity
			{
				ActionId = "sys_400006",
				Passive = "",
				FromPos = "_pipeServer_ClientDisconnected"
			}, byProductPipeId.Data);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000346F File Offset: 0x0000166F
		protected override void OnStart(string[] args)
		{
			this.InitDataAnalytics();
			this.InitLog();
			this.InitPipe();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003484 File Offset: 0x00001684
		private void InitTimer()
		{
			double totalMilliseconds = TimeSpan.FromSeconds(20.0).TotalMilliseconds;
			this._timer = new Timer(totalMilliseconds);
			this._timer.Elapsed += this._timer_Elapsed;
			this._timer.Start();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034D6 File Offset: 0x000016D6
		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			TrackerManager.RtTracker.OnEvent(new EventActivity
			{
				ActionId = "sys_100007",
				Passive = "",
				FromPos = "Pep.WindowsService._timer_Elapsed"
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003508 File Offset: 0x00001708
		private void InitLog()
		{
			DebugLog.Log(string.Format("DaemonService {0}启动", DateTime.Now));
			this._log = new SmartEventLog();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003530 File Offset: 0x00001730
		private void InitPipe()
		{
			this._pipeServer = new NamedPipeServer<PipeMessagePacket>("smartTeachingPipe");
			this._pipeServer.ClientDisconnected += this._pipeServer_ClientDisconnected;
			this._pipeServer.ClientMessage += this._pipeServer_ClientMessage;
			this._pipeServer.ClientConnected += this._pipeServer_ClientConnected;
			if (this._pipeServer.IsActive)
			{
				Console.WriteLine("IsActive");
			}
			this._pipeServer.Start();
			this._log.WriteEntry("SmartTeachingProtect In OnStart.", EventLogEntryType.Information);
			Console.WriteLine("开始");
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000035CF File Offset: 0x000017CF
		private void _pipeServer_ClientConnected(NamedPipeConnection<PipeMessagePacket, PipeMessagePacket> connection)
		{
			Console.WriteLine(connection.Id);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000035DC File Offset: 0x000017DC
		private void InitDataAnalytics()
		{
			DefaultProductInfoProvider defaultProductInfoProvider = new DefaultProductInfoProvider();
			defaultProductInfoProvider.ProductId = "111302";
			defaultProductInfoProvider.ActiveUser = "123";
			DefaultEnvironmentProvider envProvider = new DefaultEnvironmentProvider();
			defaultProductInfoProvider.ProductVersion = "1.0.0";
			ITrackerConfig trackerConfig = new DefaultTrackerConfig();
			trackerConfig.AppKey = "110000006";
			TrackerManager.Init(new TrackerFactory().SetConfig(trackerConfig).SetProductInfoProvider(defaultProductInfoProvider).SetEnvProvider(envProvider).Build(), "main", "");
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003654 File Offset: 0x00001854
		protected override void OnStop()
		{
			try
			{
				this._pipeServer.ClientDisconnected -= this._pipeServer_ClientDisconnected;
				this._pipeServer.ClientMessage -= this._pipeServer_ClientMessage;
				this._clients.Clear();
				TrackerManager.OnStop();
				this._pipeServer.Stop();
			}
			finally
			{
				this._log.WriteEntry("SmartTeachingProtect In OnStop.", EventLogEntryType.Information);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036D0 File Offset: 0x000018D0
		public void TestStartupAndStop(string[] args)
		{
			this.OnStart(args);
			Console.WriteLine("任意键退出");
			Console.ReadLine();
			this.OnStop();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036EF File Offset: 0x000018EF
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000370E File Offset: 0x0000190E
		private void InitializeComponent()
		{
			base.ServiceName = "SmartTeachingProtect";
		}

		// Token: 0x0400002C RID: 44
		private SmartEventLog _log;

		// Token: 0x0400002D RID: 45
		private NamedPipeServer<PipeMessagePacket> _pipeServer;

		// Token: 0x0400002E RID: 46
		private Timer _timer;

		// Token: 0x0400002F RID: 47
		private List<SmartTeachingClient> _clients = new List<SmartTeachingClient>();

		// Token: 0x04000030 RID: 48
		private IContainer components;
	}
}
