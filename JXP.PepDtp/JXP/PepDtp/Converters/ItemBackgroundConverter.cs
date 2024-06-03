using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.CSharp.RuntimeBinder;

namespace JXP.PepDtp.Converters
{
	// Token: 0x02000087 RID: 135
	public class ItemBackgroundConverter : IMultiValueConverter
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Color = Colors.Transparent;
			if (values == null || values.Length < 2)
			{
				return solidColorBrush;
			}
			object obj = values[0];
			object arg = values[1];
			if (ItemBackgroundConverter.<>o__0.<>p__0 == null)
			{
				ItemBackgroundConverter.<>o__0.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			object obj2 = ItemBackgroundConverter.<>o__0.<>p__0.Target(ItemBackgroundConverter.<>o__0.<>p__0, obj, null);
			if (ItemBackgroundConverter.<>o__0.<>p__4 == null)
			{
				ItemBackgroundConverter.<>o__0.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			if (!ItemBackgroundConverter.<>o__0.<>p__4.Target(ItemBackgroundConverter.<>o__0.<>p__4, obj2))
			{
				if (ItemBackgroundConverter.<>o__0.<>p__3 == null)
				{
					ItemBackgroundConverter.<>o__0.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = ItemBackgroundConverter.<>o__0.<>p__3.Target;
				CallSite <>p__ = ItemBackgroundConverter.<>o__0.<>p__3;
				if (ItemBackgroundConverter.<>o__0.<>p__2 == null)
				{
					ItemBackgroundConverter.<>o__0.<>p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object> target2 = ItemBackgroundConverter.<>o__0.<>p__2.Target;
				CallSite <>p__2 = ItemBackgroundConverter.<>o__0.<>p__2;
				object arg2 = obj2;
				if (ItemBackgroundConverter.<>o__0.<>p__1 == null)
				{
					ItemBackgroundConverter.<>o__0.<>p__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (!target(<>p__, target2(<>p__2, arg2, ItemBackgroundConverter.<>o__0.<>p__1.Target(ItemBackgroundConverter.<>o__0.<>p__1, arg, null))))
				{
					if (ItemBackgroundConverter.<>o__0.<>p__6 == null)
					{
						ItemBackgroundConverter.<>o__0.<>p__6 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(ItemBackgroundConverter)));
					}
					Func<CallSite, object, int> target3 = ItemBackgroundConverter.<>o__0.<>p__6.Target;
					CallSite <>p__3 = ItemBackgroundConverter.<>o__0.<>p__6;
					if (ItemBackgroundConverter.<>o__0.<>p__5 == null)
					{
						ItemBackgroundConverter.<>o__0.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IndexOf", null, typeof(ItemBackgroundConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					if (target3(<>p__3, ItemBackgroundConverter.<>o__0.<>p__5.Target(ItemBackgroundConverter.<>o__0.<>p__5, arg, obj)) % 2 == 1)
					{
						solidColorBrush.Color = (Color)ColorConverter.ConvertFromString("#F5F5F5");
					}
					return solidColorBrush;
				}
			}
			return solidColorBrush;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
