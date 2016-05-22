namespace ViewModelOppgave.Infrastructure.ViewModels
{
	public abstract class Rule
	{
		protected Rule(string propertyName)
		{
			PropertyName = propertyName;
		}

		public abstract bool IsValid(object model);

		public string PropertyName { get; private set; }

		public abstract string Description { get; }

	}
}