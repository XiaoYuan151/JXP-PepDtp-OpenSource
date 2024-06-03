using System;

namespace NamedPipeWrapper
{
	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x06000033 RID: 51
	public delegate void ConnectionEventHandler<TRead, TWrite>(NamedPipeConnection<TRead, TWrite> connection) where TRead : class where TWrite : class;
}
