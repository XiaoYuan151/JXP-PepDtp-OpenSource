using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JXP.Threading;

namespace JXP.SpeechEvaluator.Utility
{
	// Token: 0x02000037 RID: 55
	internal class LimitedPriorityQueue
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public Task<LimitedPriorityQueue.QueueItem> Add(string id, Action action, CancellationTokenSource cts)
		{
			LimitedPriorityQueue.<Add>d__2 <Add>d__;
			<Add>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<LimitedPriorityQueue.QueueItem>.Create();
			<Add>d__.<>4__this = this;
			<Add>d__.id = id;
			<Add>d__.action = action;
			<Add>d__.cts = cts;
			<Add>d__.<>1__state = -1;
			<Add>d__.<>t__builder.Start<LimitedPriorityQueue.<Add>d__2>(ref <Add>d__);
			return <Add>d__.<>t__builder.Task;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008B3C File Offset: 0x00006D3C
		public Task<Task> Get(string id)
		{
			LimitedPriorityQueue.<Get>d__3 <Get>d__;
			<Get>d__.<>t__builder = System.Runtime.CompilerServices.AsyncTaskMethodBuilder<Task>.Create();
			<Get>d__.<>4__this = this;
			<Get>d__.id = id;
			<Get>d__.<>1__state = -1;
			<Get>d__.<>t__builder.Start<LimitedPriorityQueue.<Get>d__3>(ref <Get>d__);
			return <Get>d__.<>t__builder.Task;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008B88 File Offset: 0x00006D88
		public void Remove(string id)
		{
			LimitedPriorityQueue.<Remove>d__4 <Remove>d__;
			<Remove>d__.<>t__builder = System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Create();
			<Remove>d__.<>4__this = this;
			<Remove>d__.id = id;
			<Remove>d__.<>1__state = -1;
			<Remove>d__.<>t__builder.Start<LimitedPriorityQueue.<Remove>d__4>(ref <Remove>d__);
		}

		// Token: 0x040000D9 RID: 217
		private readonly List<LimitedPriorityQueue.QueueItem> mListTask = new List<LimitedPriorityQueue.QueueItem>();

		// Token: 0x040000DA RID: 218
		private static readonly AsyncSemaphoreSlim mSemaphoreSlim = new AsyncSemaphoreSlim(1);

		// Token: 0x0200008D RID: 141
		internal class QueueItem : IDisposable
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D693 File Offset: 0x0000B893
			// (set) Token: 0x060003DD RID: 989 RVA: 0x0000D69B File Offset: 0x0000B89B
			public string Id { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
			// (set) Token: 0x060003DF RID: 991 RVA: 0x0000D6AC File Offset: 0x0000B8AC
			public Task Task { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D6B5 File Offset: 0x0000B8B5
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000D6BD File Offset: 0x0000B8BD
			public CancellationTokenSource Cts { get; set; }

			// Token: 0x060003E2 RID: 994 RVA: 0x0000D6C6 File Offset: 0x0000B8C6
			public Task StopTask()
			{
				if (this.Cts != null)
				{
					this.Cts.Cancel();
					this.Cts.Dispose();
					this.Cts = null;
				}
				return this.Task;
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
			public void Dispose()
			{
				if (this.Cts != null)
				{
					this.Cts.Cancel();
					this.Cts.Dispose();
					this.Cts = null;
				}
				if (this.Task != null)
				{
					this.Task.Dispose();
					this.Task = null;
				}
			}
		}
	}
}
