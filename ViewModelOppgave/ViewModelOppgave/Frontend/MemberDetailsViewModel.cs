using System.Collections.Generic;
using ViewModelOppgave.Backend.Write;
using ViewModelOppgave.Infrastructure;
using ViewModelOppgave.Infrastructure.ViewModels;
using ViewModelOppgave.Properties;

namespace ViewModelOppgave.Frontend
{
	public class MemberDetailsViewModel : ViewModelWithRules
	{
		private MemberDetailsDto _dataSource;
		private readonly IWriteApi _writeApi;

		public MemberDetailsViewModel(IWriteApi writeApi)
		{
			_writeApi = writeApi;
		}

		public string CreateNewMember()
		{
			if (IsSupressingErrors)
				DoNotSupressErrors();

			if (!IsValid || !IsNewMember)
				return null;

			var id = _writeApi.AddNewMember(DataSource);
			return id;
		}

		public Backend.Write.MemberDetailsDto DataSource
		{
			get { return _dataSource; }
			set
			{
				if (_dataSource == value)
					return;

				_dataSource = value;

				this.AllPropertiesChanged();
			}
		}

		public override bool CanCreateRules
		{
			get { return DataSource != null; }
		}

		protected override void CreateRules(List<Rule> rules)
		{
			rules.Add(new DelegateRule<MemberDetailsViewModel, string>(vm => !string.IsNullOrEmpty(vm.FirstName), vm => vm.FirstName, Resources.KanIkkeVareTom));
			rules.Add(new DelegateRule<MemberDetailsViewModel, string>(vm => !string.IsNullOrEmpty(vm.LastName), vm => vm.LastName, Resources.KanIkkeVareTom));
			rules.Add(new DelegateRule<MemberDetailsViewModel, int?>(vm => vm.Age < 91, vm => vm.Age, "Alder må være mindre enn 91 år"));
			rules.Add(new DelegateRule<MemberDetailsViewModel, int?>(vm => vm.Age > 0, vm => vm.Age, "Alder må være større enn 0 år"));

			base.CreateRules(rules);
		}

		public string FirstName
		{
			get { return DataSource.DefaultIfNull(m => m.FirstName); }
			set
			{
				if (!DataSource.IsChanged(vm => vm.FirstName == value))
					return;

				DataSource.FirstName = value;
				this.OnPropertyChanged(vm => vm.FirstName);
			}
		}

		public string LastName
		{
			get { return DataSource.DefaultIfNull(m => m.LastName); }
			set
			{
				if (!DataSource.IsChanged(vm => vm.LastName == value))
					return;

				DataSource.LastName = value;
				this.OnPropertyChanged(vm => vm.LastName);
			}
		}

		public int? Age
		{
			get { return DataSource.DefaultIfNull(m => m.Age); }
			set
			{
				if (!DataSource.IsChanged(m => m.Age == value))
					return;

				DataSource.Age = value ?? 0 ;
				this.OnPropertyChanged(vm => vm.Age);
			}
		}

		public bool Sex
		{
			get { return DataSource.DefaultIfNull(m => m.Sex != Backend.Sex.Male); }
			set
			{
				DataSource.Sex = value ? Backend.Sex.Female : Backend.Sex.Male;
				this.OnPropertyChanged(vm => vm.Sex);
			}
		}

		public bool IsNewMember
		{
			get { return DataSource.DefaultIfNull(m => m.IsNew); }
		}
	}
}
