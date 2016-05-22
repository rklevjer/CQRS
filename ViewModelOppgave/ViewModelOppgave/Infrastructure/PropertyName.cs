using System;
using System.Linq.Expressions;

namespace ViewModelOppgave.Infrastructure
{
	public static class PropertyName<T>
	{
		public static string For<TRet>(Expression<Func<T, TRet>> expression)
		{
			return expression.PropertyName();
		}
	}
}