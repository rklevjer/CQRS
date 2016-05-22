using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModelOppgave.Infrastructure.ViewModels
{
	public class ViewModelWithRules : ViewModelBase, IDataErrorInfo
	{
		private List<Rule> _rules;

		public virtual string this[string propertyName]
		{
			get
			{
				if (IsSupressingErrors)
					return String.Empty;

				string result = String.Empty;

				foreach (Rule r in GetBrokenRules(propertyName))
				{
					result += r.Description;
					result += Environment.NewLine;
				}

				return result;
			}
		}

		public virtual ReadOnlyCollection<Rule> GetBrokenRules(string property)
		{
			property = MapNullToStringEmpty(property);

			if (_rules == null && CanCreateRules)
			{
				_rules = new List<Rule>();
				CreateRules(_rules);
			}

			IList<Rule> rulesTmp = _rules != null ? _rules : new List<Rule>();

			List<Rule> broken = new List<Rule>();

			foreach (Rule r in rulesTmp)
			{
				if (r.PropertyName == property || property == String.Empty)
				{
					bool isRuleBroken = !r.IsValid(this);

					if (isRuleBroken)
					{
						broken.Add(r);
					}
				}
			}

			return broken.AsReadOnly();
		}

		protected virtual void CreateRules(List<Rule> rules)
		{ }

		/// <summary>
		/// No errors will be shown to the user
		/// </summary>
		public virtual void SupressAllErrors()
		{
			if (IsSupressingErrors)
				return;

			IsSupressingErrors = true;
			RaisePropertyChanged("Error");
		}

		/// <summary>
		/// All errors will be shown to the user
		/// </summary>
		public virtual void DoNotSupressErrors()
		{
			if (!IsSupressingErrors)
				return;

			IsSupressingErrors = false;
			RaisePropertyChanged("Error");
		}

		public bool IsSupressingErrors { get; private set; }

		public string Error
		{
			get { return this[null]; }
		}

		public virtual bool IsValid
		{
			get { return GetBrokenRules(String.Empty).Count == 0; }
		}

		/// <summary>
		/// Override if you want to temporarly hold back creation of rules, note: once this has been set to true it 
		/// will have no effect if you set it back to false. Typical use case: ViewModel has not been initialized yet
		/// </summary>
		public virtual bool CanCreateRules
		{
			get { return true; }
		}

		private static string MapNullToStringEmpty(string property)
		{
			if (property == null)
				return String.Empty;

			return property;
		}

	}
}
