using System;
using System.ComponentModel;
using System.Linq;
using ViewModelOppgave.Backend.Read;
using ViewModelOppgave.Backend.Write;
using ViewModelOppgave.Infrastructure;
using ViewModelOppgave.Infrastructure.ViewModels;

namespace ViewModelOppgave.Frontend
{
	public class MembersViewModel : ViewModelWithRules
	{
		private readonly IReadApi _readApi;
        private readonly IWriteApi _writeApi;
        private BindingList<MembersGridViewModel> _allMembers;
		private MembersGridViewModel _selectedMemberInGrid;

		public MembersViewModel(IReadApi readApi, IWriteApi writeApi, MemberDetailsViewModel memberDetails)
		{
			_readApi = readApi;
            _writeApi = writeApi;

            MemberDetails = memberDetails;
			MemberDetails.PropertyChanged += MemberDetailsHasChanged;

			CreateNewMemberRecordCommand = new DelegateCommand(() => AddNewMemberRecord(), () => !MemberDetails.DataSource.IsNew);
			SaveNewMemberCommand = new DelegateCommand(() => SaveNewMemberRecord(), () => IsValid && MemberDetails.IsNewMember);
		}

		public DelegateCommand CreateNewMemberRecordCommand { get; private set; }

		public DelegateCommand SaveNewMemberCommand { get; private set; }

		public MemberDetailsViewModel MemberDetails { get; private set; }

		public BindingList<MembersGridViewModel> AllMembers
		{
			get
			{
				if (_allMembers == null)
					ReloadMembers();

				return _allMembers;
			}
		}

		public MembersGridViewModel SelectedMemberInGrid
		{
			get { return _selectedMemberInGrid; }
			set
			{
				if (_selectedMemberInGrid == value)
					return;

				if (value != null && !AllMembers.Contains(value))
					throw new InvalidOperationException("The selected member must be picked from the list of all members");

				_selectedMemberInGrid = value;

				ShowMemberDetailsFor(_selectedMemberInGrid);

				this.OnPropertyChanged(vm => vm.SelectedMemberInGrid);
			}
		}

		public override void DoNotSupressErrors()
		{
			base.DoNotSupressErrors();
			MemberDetails.DoNotSupressErrors();
		}

		public override void SupressAllErrors()
		{
			base.SupressAllErrors();
			MemberDetails.SupressAllErrors();
		}

		public override bool IsValid
		{
			get
			{
				if (!MemberDetails.IsValid)
					return false;

				return base.IsValid;
			}
		}

		private void MemberDetailsHasChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(vm => vm.IsValid);
			CreateNewMemberRecordCommand.RaiseCanExecuteChanged();
			SaveNewMemberCommand.RaiseCanExecuteChanged();
		}

		private void ReloadMembers()
		{
			if (_allMembers == null)
				_allMembers = new BindingList<MembersGridViewModel>();

			_allMembers.RaiseListChangedEvents = false;
			_allMembers.Clear();

			foreach (var member in _readApi.GetAllMembers())
			{
				_allMembers.Add(new MembersGridViewModel(member));
			}

			_allMembers.RaiseListChangedEvents = true;
			_allMembers.ResetBindings();
		}

		private void AddNewMemberRecord()
		{
			if (MemberDetails.DataSource != null && MemberDetails.DataSource.IsNew)
				return;

			MemberDetails.DataSource = new DetailMember(true);
			this.AllPropertiesChanged();
		}

		private void SaveNewMemberRecord()
		{
			var id = MemberDetails.CreateNewMember();

			if (id == null) 
				return;

			ReloadMembers();
			SelectedMemberInGrid = AllMembers.First(m => m.DataSource.Id == id);
		}

		private void ShowMemberDetailsFor(MembersGridViewModel selectedMemberInGrid)
		{
			if (selectedMemberInGrid != null)
				MemberDetails.DataSource = _writeApi.GetSelectedMember(SelectedMemberInGrid.Id);
			else
				MemberDetails.DataSource = null;
		}
	}
}
