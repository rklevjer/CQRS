using System;
using System.ComponentModel;
using System.Windows.Forms;
using ViewModelOppgave.Infrastructure;

namespace ViewModelOppgave.Frontend
{
	public partial class MemberDetailsView : UserControl
	{
		private MemberDetailsViewModel _viewModel;

		public MemberDetailsView()
		{
			InitializeComponent();
		}

		public void FocusFirstName()
		{
			_txtFirstName.Focus();
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MemberDetailsViewModel DataSource
		{
			get { return _memberBindingSource.DataSource as MemberDetailsViewModel; }
			set
			{
				if (_viewModel != null)
					throw new InvalidOperationException();

				_viewModel = value;
				SetupDataBindings();				
			}
		}

		private void SetupDataBindings()
		{
			_memberBindingSource.DataSource = _viewModel;

			_memberBindingSource
				.CreateBindingsFor<MemberDetailsViewModel>()
				.Defaults(new BindingDefaults { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged })

				.From(vm => vm.FirstName).To(_txtFirstName).On(c => c.Text)
				.AndThenFrom(vm => vm.LastName).To(_txtLastName).On(c => c.Text)
				.AndThenFrom(vm => vm.Age).To(_intAge).On(c => c.Text).With.FormattingEnabled()
				.AndThenFrom(vm => vm.Sex).To(_rbMan).On(c => c.Checked).With.BooleanInverter()
				.AndThenFrom(vm => vm.Sex).To(_rbWoman).On(c => c.Checked)

				.AndThenFrom(vm => vm.IsNewMember).To(_txtFirstName).On(c => c.ReadOnly).With.BooleanInverter()
				.AndThenFrom(vm => vm.IsNewMember).To(_txtLastName).On(c => c.ReadOnly).With.BooleanInverter()
				.AndThenFrom(vm => vm.IsNewMember).To(_intAge).On(c => c.ReadOnly).With.BooleanInverter()
				.AndThenFrom(vm => vm.IsNewMember).To(_rbMan).On(c => c.Enabled)
				.AndThenFrom(vm => vm.IsNewMember).To(_rbWoman).On(c => c.Enabled);
		}

	}
}
