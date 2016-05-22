using System;
using System.Linq.Expressions;

namespace ViewModelOppgave.Infrastructure
{
	public static class RaisePropertyChangedExtensions
	{
		public static void OnPropertyChanged<T, TRet>(this T item, Expression<Func<T, TRet>> expr) where T : IRaisePropertyChanged
		{
			string name = expr.PropertyName();
			item.RaisePropertyChanged(name);
		}

		public static void AllPropertiesChanged(this IRaisePropertyChanged c)
		{
			c.RaisePropertyChanged(null);
		}

		public static bool RaiseAndSetIfChanged<TVm, T>(this TVm viewModel, Expression<Func<TVm, T>> expression, ref T field, T newValue) where TVm : IRaisePropertyChanged
		{
			if (object.Equals(newValue, field)) return false;
			field = newValue;
			string name = expression.PropertyName();
			viewModel.RaisePropertyChanged(name);
			return true;
		}
	}
}
