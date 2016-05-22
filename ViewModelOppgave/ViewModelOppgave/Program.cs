using System;
using System.Windows.Forms;
using ViewModelOppgave.Backend;
using ViewModelOppgave.Frontend;

namespace ViewModelOppgave
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var bookClubApi = new BookClubApi();
			var viewModel = new MembersViewModel(bookClubApi, new MemberDetailsViewModel(bookClubApi) { DataSource = new Member(true) });
	
			Application.Run(new MembersView(viewModel));
		}
	}
}
