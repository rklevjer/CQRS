using System;
using System.Linq.Expressions;

namespace ViewModelOppgave.Infrastructure
{
	public static class ExpressionExtensions
	{
		public static string PropertyName<TObj, TRet>(this Expression<Func<TObj, TRet>> func)
		{
			MemberExpression expr = func.Body as MemberExpression;
			if (expr == null)
			{
				throw new ArgumentException("Expression must be property access");
			}

			return expr.Member.Name;
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
														  Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
			return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
		}

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
													   Expression<Func<T, bool>> expr2)
		{
			var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
			return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
		}

		public static Expression<Func<T, T>> Combine<T>(this Expression<Func<T, T>> expr1,
													   Expression<Func<T, T>> expr2)
		{
			//Func<T, T> fu = e => f(f2(e));
			var parameter = Expression.Parameter(typeof(T), "e");//e
			var invokedExp2 = Expression.Invoke(expr2, parameter);//f2
			var invokedExp = Expression.Invoke(expr1, invokedExp2);//f
			return Expression.Lambda<Func<T, T>>(invokedExp, parameter);// =>
		}

		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
		{
			var candidateExpr = expr.Parameters[0];
			var body = Expression.Not(expr.Body);

			return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
		}

	}
}
