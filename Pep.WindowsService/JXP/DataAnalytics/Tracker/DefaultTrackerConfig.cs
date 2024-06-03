using System;
using System.IO;
using System.Reflection;
using JXP.DataAnalytics.Debugging;

namespace JXP.DataAnalytics.Tracker
{
	// Token: 0x0200002F RID: 47
	public class DefaultTrackerConfig : ITrackerConfig
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005F8E File Offset: 0x0000418E
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005F96 File Offset: 0x00004196
		public string AppKey { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005F9F File Offset: 0x0000419F
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005FA7 File Offset: 0x000041A7
		public string AppFolder { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005FB0 File Offset: 0x000041B0
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005FB8 File Offset: 0x000041B8
		public long MaxDataSize { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005FC1 File Offset: 0x000041C1
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005FC9 File Offset: 0x000041C9
		public TimeSpan ScanPeriod { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005FD2 File Offset: 0x000041D2
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005FDA File Offset: 0x000041DA
		public int TrackerQueueCapacity { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005FE3 File Offset: 0x000041E3
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005FEB File Offset: 0x000041EB
		public bool AlwaysDeleteFile { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005FF4 File Offset: 0x000041F4
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005FFC File Offset: 0x000041FC
		public int SingleFileSize { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006005 File Offset: 0x00004205
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000600D File Offset: 0x0000420D
		public int MaxRecordCountWhenPost { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006016 File Offset: 0x00004216
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000601E File Offset: 0x0000421E
		public int PostTimeout { get; set; }

		// Token: 0x0600013D RID: 317 RVA: 0x00006028 File Offset: 0x00004228
		public DefaultTrackerConfig()
		{
			try
			{
				this.AppFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".pa");
			}
			catch (Exception ex)
			{
				DebugTracer.Write(ex);
			}
			this.MaxDataSize = 104857600L;
			this.ScanPeriod = TimeSpan.FromHours(5.0);
			this.AlwaysDeleteFile = false;
			this.SingleFileSize = 143360;
			this.PostTimeout = 15000;
			this.MaxRecordCountWhenPost = 300;
		}
	}
}
