using System;
using System.Collections.Generic;
using JXP.Models;

namespace JXP.PepDtp.Comparer
{
	// Token: 0x0200007B RID: 123
	public class UserResourceComparer : IEqualityComparer<UserResourceModel>
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0002AAD3 File Offset: 0x00028CD3
		public bool Equals(UserResourceModel x, UserResourceModel y)
		{
			return x == y || (x != null && y != null && x.ID == y.ID);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		public int GetHashCode(UserResourceModel userResMdl)
		{
			if (userResMdl == null)
			{
				return 0;
			}
			return userResMdl.ID.GetHashCode();
		}
	}
}
