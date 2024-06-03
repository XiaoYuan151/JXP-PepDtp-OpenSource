using System;

namespace JXP.OsInfo
{
	// Token: 0x0200004D RID: 77
	internal sealed class OsInfo : IComparable, ICloneable, IComparable<OsInfo>, IEquatable<OsInfo>
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00007A96 File Offset: 0x00005C96
		public OsInfo(OsVersion osVersion, PlatformID platformId, int majorVersion, int minorVersion)
		{
			this.OsVersion = osVersion;
			this.PlatformId = platformId;
			this.OperatingSystem = new OperatingSystem(platformId, new Version(majorVersion, minorVersion));
			this.MajorVersion = majorVersion;
			this.MinorVersion = minorVersion;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007ACF File Offset: 0x00005CCF
		private int MajorVersion { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00007AD7 File Offset: 0x00005CD7
		private int MinorVersion { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007ADF File Offset: 0x00005CDF
		public OperatingSystem OperatingSystem { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00007AE7 File Offset: 0x00005CE7
		public OsVersion OsVersion { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007AEF File Offset: 0x00005CEF
		private PlatformID PlatformId { get; }

		// Token: 0x0600024D RID: 589 RVA: 0x00007AF7 File Offset: 0x00005CF7
		public object Clone()
		{
			return new OsInfo(this.OsVersion, this.PlatformId, this.MajorVersion, this.MinorVersion);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007B16 File Offset: 0x00005D16
		public int CompareTo(object o)
		{
			return this.CompareTo(o as OsInfo);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007B24 File Offset: 0x00005D24
		public int CompareTo(OsInfo other)
		{
			return new Version(this.MajorVersion, this.MinorVersion).CompareTo(new Version(other.MajorVersion, other.MinorVersion));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007B4D File Offset: 0x00005D4D
		public bool Equals(OsInfo other)
		{
			return other != null && other.MajorVersion == this.MajorVersion && other.MinorVersion == this.MinorVersion && other.PlatformId == this.PlatformId;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007B84 File Offset: 0x00005D84
		public override bool Equals(object o)
		{
			OsInfo osInfo = o as OsInfo;
			return osInfo != null && this.Equals(osInfo);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007BAA File Offset: 0x00005DAA
		public static bool operator ==(OsInfo o, OsInfo p)
		{
			return object.Equals(o, p);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007BB3 File Offset: 0x00005DB3
		public static bool operator >(OsInfo o, OsInfo p)
		{
			if (o == null)
			{
				return p == null;
			}
			return o.CompareTo(p) > 0;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public static bool operator >=(OsInfo o, OsInfo p)
		{
			if (o == null)
			{
				return p == null;
			}
			return o.CompareTo(p) >= 0;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public static bool operator !=(OsInfo o, OsInfo p)
		{
			return !object.Equals(o, p);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007BFC File Offset: 0x00005DFC
		public static bool operator <(OsInfo o, OsInfo p)
		{
			if (o == null)
			{
				return p == null;
			}
			return o.CompareTo(p) < 0;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007C19 File Offset: 0x00005E19
		public static bool operator <=(OsInfo o, OsInfo p)
		{
			if (o == null)
			{
				return p == null;
			}
			return o.CompareTo(p) <= 0;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007C39 File Offset: 0x00005E39
		public override string ToString()
		{
			return this.OperatingSystem.ToString();
		}
	}
}
