using System;

namespace ViewModelOppgave.Infrastructure.ViewModels
{
	static class ViewModelHelper
	{
		public static bool IsChanged<T>(this T source, Func<T, bool> value) where T : class
		{
			if (source == null || value(source))
				return false;

			return true;

		}
	}
}
