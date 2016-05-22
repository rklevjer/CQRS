﻿using ViewModelOppgave.Infrastructure;
using ViewModelOppgave.Infrastructure.ViewModels;

namespace ViewModelOppgave.Frontend
{

	public class MembersGridViewModel : ViewModelWithRules
	{
		private readonly Backend.Member _dataSource;

		public MembersGridViewModel(Backend.Member dataSource)
		{
			_dataSource = dataSource;
		}

		public Backend.Member DataSource
		{
			get { return _dataSource; }
		}

		public string FirstName
		{
			get { return DataSource.DefaultIfNull(r => r.FirstName); }
			set
			{
				DataSource.DefaultIfNull(r => r.FirstName = value);
				this.OnPropertyChanged(vm => vm.FirstName);
			}
		}

		public string LastName
		{
			get { return DataSource.DefaultIfNull(r => r.LastName); }
			set
			{
				DataSource.DefaultIfNull(r => r.LastName = value);
				this.OnPropertyChanged(vm => vm.LastName);
			}
		}

		public int Age
		{
			get { return DataSource.DefaultIfNull(r => r.Age); }
			set
			{
				DataSource.DefaultIfNull(r => r.Age = value);
				this.OnPropertyChanged(vm => vm.Age);
			}
		}

		public string Sex
		{
			get { return DataSource.DefaultIfNull(r => r.Sex == Backend.Sex.Female ? "Female" : "Male"); }
			set
			{
				DataSource.DefaultIfNull(r => r.Sex = Backend.Sex.Female);
				this.OnPropertyChanged(vm => vm.Sex);
			}
		}
	}
}