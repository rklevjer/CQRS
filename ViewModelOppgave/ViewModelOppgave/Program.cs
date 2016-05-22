using System;
using System.Windows.Forms;
using ViewModelOppgave.Backend.Read;
using ViewModelOppgave.Backend.Write;
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

			var readApi = new ReadApi();
			var writeApi = new WriteApi();
			var viewModel = new MembersViewModel(readApi, writeApi, new MemberDetailsViewModel(writeApi) { DataSource = new DetailMember(true) });
	
			Application.Run(new MembersView(viewModel));
		}
	}
}
