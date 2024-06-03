using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IWshRuntimeLibrary
{
	// Token: 0x020000AF RID: 175
	[CompilerGenerated]
	[DefaultMember("FullName")]
	[Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
	[TypeIdentifier]
	[ComImport]
	public interface IWshShortcut
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009DF RID: 2527
		[DispId(0)]
		[IndexerName("FullName")]
		string FullName { [DispId(0)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x060009E0 RID: 2528
		void _VtblGap1_2();

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009E1 RID: 2529
		// (set) Token: 0x060009E2 RID: 2530
		[DispId(1001)]
		string Description { [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1001)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x060009E3 RID: 2531
		void _VtblGap2_2();

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009E4 RID: 2532
		// (set) Token: 0x060009E5 RID: 2533
		[DispId(1003)]
		string IconLocation { [DispId(1003)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1003)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x060009E6 RID: 2534
		void _VtblGap3_1();

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009E7 RID: 2535
		// (set) Token: 0x060009E8 RID: 2536
		[DispId(1005)]
		string TargetPath { [DispId(1005)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1005)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009E9 RID: 2537
		// (set) Token: 0x060009EA RID: 2538
		[DispId(1006)]
		int WindowStyle { [DispId(1006)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1006)] [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009EB RID: 2539
		// (set) Token: 0x060009EC RID: 2540
		[DispId(1007)]
		string WorkingDirectory { [DispId(1007)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1007)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] [param: In] set; }

		// Token: 0x060009ED RID: 2541
		void _VtblGap4_1();

		// Token: 0x060009EE RID: 2542
		[DispId(2001)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Save();
	}
}
