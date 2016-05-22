using System;
using System.Linq.Expressions;

namespace ViewModelOppgave.Infrastructure.ViewModels
{
	public class DelegateRule<T, TRet> : Rule
	{
		private readonly Func<T, bool> _validationRule;

		private readonly Func<string> _descGenerator;

		public DelegateRule(Func<T, bool> validationRule, Expression<Func<T, TRet>> propertyName, string description) : this(validationRule, propertyName, () => description)
		{ }

		public DelegateRule(Func<T, bool> validationRule, Expression<Func<T, TRet>> propertyName, Func<string> description)
			: base(PropertyName<T>.For(propertyName))
		{
			_validationRule = validationRule;
			_descGenerator = description;
		}

		public override bool IsValid(object model)
		{
			return _validationRule((T)model);
		}

		public override string Description
		{
			get { return _descGenerator(); }
		}
	}
}