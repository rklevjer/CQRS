using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelOppgave.Infrastructure
{
	public class DelegateCommand<T> : ICommand<T>, IRaiseCanExecuteChanged
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;


		public DelegateCommand(Action<T> execute)
			: this(execute, item => true)
		{

		}

		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public void Execute(T item)
		{
			_execute(item);
		}

		public bool CanExecute(T item)
		{
			return _canExecute(item);
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}



	public class DelegateCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;


		public DelegateCommand(Action execute)
			: this(execute, () => true)
		{

		}

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public void Execute(Unit item)
		{
			_execute();
		}

		public bool CanExecute(Unit item)
		{
			return _canExecute();
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}
