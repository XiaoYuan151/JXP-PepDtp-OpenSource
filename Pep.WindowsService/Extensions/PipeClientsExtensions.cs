using System;
using System.Collections.Generic;
using System.Linq;
using Pep.WindowsService.Data;

namespace Pep.WindowsService.Extensions
{
	// Token: 0x0200001E RID: 30
	internal static class PipeClientsExtensions
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal static SmartTeachingClient GetByProductId(this List<SmartTeachingClient> clients, string productId)
		{
			return clients.FirstOrDefault((SmartTeachingClient p) => string.Equals(p.Data.ProductId, productId));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A48 File Offset: 0x00001C48
		internal static SmartTeachingClient GetByProductPipeId(this List<SmartTeachingClient> clients, int pipeId)
		{
			return clients.FirstOrDefault((SmartTeachingClient p) => p.Client.Id == pipeId);
		}
	}
}
