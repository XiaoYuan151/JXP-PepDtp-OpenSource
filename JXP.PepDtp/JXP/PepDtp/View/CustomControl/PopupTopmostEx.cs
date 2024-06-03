using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace JXP.PepDtp.View.CustomControl
{
	// Token: 0x02000057 RID: 87
	internal class PopupTopmostEx : Popup
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001FE64 File Offset: 0x0001E064
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0001FE76 File Offset: 0x0001E076
		public bool IsPositionUpdate
		{
			get
			{
				return (bool)base.GetValue(PopupTopmostEx.IsPositionUpdateProperty);
			}
			set
			{
				base.SetValue(PopupTopmostEx.IsPositionUpdateProperty, value);
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001FE89 File Offset: 0x0001E089
		private static void IsPositionUpdateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as PopupTopmostEx).pup_Loaded(d as PopupTopmostEx, null);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001FE9D File Offset: 0x0001E09D
		public PopupTopmostEx()
		{
			base.Loaded += this.pup_Loaded;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		private void pup_Loaded(object sender, RoutedEventArgs e)
		{
			DependencyObject parent = VisualTreeHelper.GetParent(sender as Popup);
			while (parent != null && !(parent is Window))
			{
				parent = VisualTreeHelper.GetParent(parent);
			}
			if (parent is Window)
			{
				(parent as Window).LocationChanged -= this.PositionChanged;
				if (this.IsPositionUpdate)
				{
					(parent as Window).LocationChanged += this.PositionChanged;
				}
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001FF24 File Offset: 0x0001E124
		private void PositionChanged(object sender, EventArgs e)
		{
			MethodInfo method = typeof(Popup).GetMethod("UpdatePosition", BindingFlags.Instance | BindingFlags.NonPublic);
			if (base.IsOpen)
			{
				method.Invoke(this, null);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001FF59 File Offset: 0x0001E159
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0001FF6B File Offset: 0x0001E16B
		public bool Topmost
		{
			get
			{
				return (bool)base.GetValue(PopupTopmostEx.TopmostProperty);
			}
			set
			{
				base.SetValue(PopupTopmostEx.TopmostProperty, value);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001FF7E File Offset: 0x0001E17E
		private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			(obj as PopupTopmostEx).UpdateWindow();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001FF8B File Offset: 0x0001E18B
		protected override void OnOpened(EventArgs e)
		{
			this.UpdateWindow();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001FF94 File Offset: 0x0001E194
		private void UpdateWindow()
		{
			if (base.Child == null)
			{
				return;
			}
			IntPtr handle = ((HwndSource)PresentationSource.FromVisual(base.Child)).Handle;
			PopupTopmostEx.RECT rect;
			if (PopupTopmostEx.NativeMethods.GetWindowRect(handle, out rect))
			{
				PopupTopmostEx.NativeMethods.SetWindowPos(handle, this.Topmost ? -1 : -2, rect.Left, rect.Top, (int)base.Width, (int)base.Height, 0);
			}
		}

		// Token: 0x0400030F RID: 783
		public static readonly DependencyProperty IsPositionUpdateProperty = DependencyProperty.Register("IsPositionUpdate", typeof(bool), typeof(PopupTopmostEx), new PropertyMetadata(true, new PropertyChangedCallback(PopupTopmostEx.IsPositionUpdateChanged)));

		// Token: 0x04000310 RID: 784
		public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(Popup), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(PopupTopmostEx.OnTopmostChanged)));

		// Token: 0x02000116 RID: 278
		public struct RECT
		{
			// Token: 0x040005DA RID: 1498
			public int Left;

			// Token: 0x040005DB RID: 1499
			public int Top;

			// Token: 0x040005DC RID: 1500
			public int Right;

			// Token: 0x040005DD RID: 1501
			public int Bottom;
		}

		// Token: 0x02000117 RID: 279
		public static class NativeMethods
		{
			// Token: 0x06000B05 RID: 2821
			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool GetWindowRect(IntPtr hWnd, out PopupTopmostEx.RECT lpRect);

			// Token: 0x06000B06 RID: 2822
			[DllImport("user32")]
			internal static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
		}
	}
}
