using System;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeWrapper.Threading
{
	// Token: 0x0200000E RID: 14
	internal class Worker
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E36 File Offset: 0x00001036
		private static TaskScheduler CurrentTaskScheduler
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					return TaskScheduler.Default;
				}
				return TaskScheduler.FromCurrentSynchronizationContext();
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00002E4C File Offset: 0x0000104C
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00002E84 File Offset: 0x00001084
		public event WorkerSucceededEventHandler Succeeded;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002EBC File Offset: 0x000010BC
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002EF4 File Offset: 0x000010F4
		public event WorkerExceptionEventHandler Error;

		// Token: 0x06000063 RID: 99 RVA: 0x00002F29 File Offset: 0x00001129
		public Worker() : this(Worker.CurrentTaskScheduler)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F36 File Offset: 0x00001136
		public Worker(TaskScheduler callbackThread)
		{
			this._callbackThread = callbackThread;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F45 File Offset: 0x00001145
		public void DoWork(Action action)
		{
			new Task(new Action<object>(this.DoWorkImpl), action, CancellationToken.None, TaskCreationOptions.LongRunning).Start();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F64 File Offset: 0x00001164
		private void DoWorkImpl(object oAction)
		{
			Action action = (Action)oAction;
			try
			{
				action();
				this.Callback(new Action(this.Succeed));
			}
			catch (Exception e)
			{
				Exception e2;
				e = e2;
				this.Callback(delegate
				{
					this.Fail(e);
				});
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FCC File Offset: 0x000011CC
		private void Succeed()
		{
			if (this.Succeeded != null)
			{
				this.Succeeded();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002FE1 File Offset: 0x000011E1
		private void Fail(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(exception);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002FF7 File Offset: 0x000011F7
		private void Callback(Action action)
		{
			Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, this._callbackThread);
		}

		// Token: 0x04000021 RID: 33
		private readonly TaskScheduler _callbackThread;
	}
}
