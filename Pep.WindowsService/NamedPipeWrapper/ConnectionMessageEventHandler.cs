using System;

namespace NamedPipeWrapper
{
	// Token: 0x02000008 RID: 8
	// (Invoke) Token: 0x06000037 RID: 55
	public delegate void ConnectionMessageEventHandler<TRead, TWrite>(NamedPipeConnection<TRead, TWrite> connection, TRead message) where TRead : class where TWrite : class;
}
