using System;
using System.Diagnostics;

namespace Pep.WindowsService
{
	// Token: 0x02000018 RID: 24
	public class SmartEventLog
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003731 File Offset: 0x00001931
		public string LogName
		{
			get
			{
				return this._logName;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003739 File Offset: 0x00001939
		public SmartEventLog()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000374C File Offset: 0x0000194C
		public SmartEventLog(string logName)
		{
			this._logName = logName;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003768 File Offset: 0x00001968
		public void WriteEntry(string error, EventLogEntryType type = EventLogEntryType.Information)
		{
			string friendlyName = AppDomain.CurrentDomain.FriendlyName;
			if (!EventLog.SourceExists(friendlyName))
			{
				EventLog.CreateEventSource(friendlyName, this._logName);
			}
			using (EventLog eventLog = new EventLog(this._logName))
			{
				eventLog.Source = friendlyName;
				string message = AppDomain.CurrentDomain.BaseDirectory + Environment.NewLine + error;
				eventLog.WriteEntry(message, type);
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly string _logName = "SmartTeachingService";
	}
}
