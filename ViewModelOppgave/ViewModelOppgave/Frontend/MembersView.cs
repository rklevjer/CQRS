using System.ComponentModel;
using System.Windows.Forms;
using ViewModelOppgave.Infrastructure;

namespace ViewModelOppgave.Frontend
{
	public partial class MembersView : Form
	{
		private readonly MembersViewModel _viewModel;

		public MembersView(MembersViewModel viewModel)
		{
			InitializeComponent();

			_viewModel = viewModel;
			Disposed += (sender, args) => _viewModel.Dispose();

			_memberDetailsView.DataSource =_viewModel.MemberDetails;

			SetupDataBindings(_viewModel);
		}

		private void SetupDataBindings(MembersViewModel viewModel)
		{
			_bsViewModel.DataSource = viewModel;

			_bsAllMembers.BindDataSourceTo<MembersViewModel>(_bsViewModel, vm => vm.AllMembers);

			viewModel.PropertyChanged += PullCurrentMemberFromViewModel;
			_bsAllMembers.CurrentChanged += PushCurrentMemberToViewModel;

			_newMemberBtn.BindToCommand(viewModel.CreateNewMemberRecordCommand);
			_addBtn.BindToCommand(viewModel.SaveNewMemberCommand);
		}

		private void PullCurrentMemberFromViewModel(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == PropertyName<MembersViewModel>.For(vm => vm.SelectedMemberInGrid))
				_bsAllMembers.Position = _bsAllMembers.List.IndexOf(_viewModel.SelectedMemberInGrid);
		}

		private void PushCurrentMemberToViewModel(object sender, System.EventArgs e)
		{
			_viewModel.SelectedMemberInGrid = (MembersGridViewModel)_bsAllMembers.Current;
		}

		private void Close_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void NewMemberBtn_Click(object sender, System.EventArgs e)
		{
			_memberDetailsView.FocusFirstName();
		}
	}
}
