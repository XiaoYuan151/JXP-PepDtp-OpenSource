using System;

namespace NamedPipeWrapper
{
	// Token: 0x02000009 RID: 9
	// (Invoke) Token: 0x0600003B RID: 59
	public delegate void ConnectionExceptionEventHandler<TRead, TWrite>(NamedPipeConnection<TRead, TWrite> connection, Exception exception) where TRead : class where TWrite : class;
}
