using System;
using System.Reactive;

namespace ViewModelOppgave.Infrastructure
{
	public interface IAction
	{
		string Name { get; }
		EventHandler Do { get; }
	}

	[Flags]
	public enum MenuCommandState
	{
		None = 0,
		Visible = 1,
		Enabled = 2,
		All = Visible | Enabled
	}

	public interface ICommand<T>
	{
		void Execute(T t);
		bool CanExecute(T t);
		event EventHandler CanExecuteChanged;
	}

	public interface ICommand : ICommand<Unit>
	{

	}

	public interface IRaiseCanExecuteChanged
	{
		void RaiseCanExecuteChanged();
	}

	public interface IMenuCommand<T> : ICommand<T>
	{
		event EventHandler BeforeExecute;
		event EventHandler AfterExecute;
		MenuCommandState QueryState(T beh);
	}

	public interface IMenuCommand : ICommand
	{
		event EventHandler BeforeExecute;
		event EventHandler AfterExecute;
		MenuCommandState QueryState(Unit beh);
	}

	public interface IEventAggregator
	{
		IObservable<TEvent> GetEvent<TEvent>();
		void Publish<TEvent>(TEvent arg);
	}

	public interface IBrowser
	{
		void ShowPage(Uri uri);
	}

	public class SearchResult
	{
		public bool Cancel { get; set; }
		public string ResultText { get; set; }
	}

	public interface ISearchCommand<TRow>
	{
		SearchResult Search(TRow line, string text);
	}


	public interface IPropertyValidator
	{
		bool IsValid(object value);
	}

	public interface IDialogView
	{
		void CloseOk();
		void CloseCancel();
	}
}
