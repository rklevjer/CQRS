using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ViewModelOppgave.Infrastructure
{
	public static class ObjectExtensions
	{
		public static TRet DefaultIfNull<T, TRet>(this T thisObj, Func<T, TRet> func) where T : class
		{
			return thisObj != null ? func(thisObj) : default(TRet);
		}

		public static string PropertyName<TObj, TRet>(this TObj thisObj, Expression<Func<TObj, TRet>> expr)
		{
			return expr.PropertyName();
		}

	}
}
