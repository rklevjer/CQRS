using System;
using System.Windows.Forms;

namespace ViewModelOppgave.Infrastructure
{
	public static class CommandExtensions
	{
		public static void BindToCommand<T>(this Control button, ICommand<T> command, bool bindToVisible = false)
		{
			BindToCommand(button, command, default(T), null, bindToVisible);
		}

		public static void BindToCommand<T>(this ToolStripItem button, ICommand<T> command, bool bindToVisible = false)
		{
			BindToCommandCore(button, command, default(T), null, bindToVisible);
		}
	

		public static void BindToCommand<T>(this Control button, ICommand<T> command, Action runOnExecuted, bool bindToVisible = false)
		{
			BindToCommand(button, command, default(T), runOnExecuted, bindToVisible);
		}

		public static void RefreshOn(this IRaiseCanExecuteChanged raise, BindingSource bindingSource)
		{
			bindingSource.CurrentItemChanged += (sender, args) => raise.RaiseCanExecuteChanged();
		}

		public static void CommandOnDoubleClick<T>(this Control control, ICommand<T> command)
		{
			control.DoubleClick += (sender, args) =>
			{
				if (command.CanExecute(default(T)))
				{
					command.Execute(default(T));
				}
			};
		}

		public static void BindToCommand<T>(this Control button, ICommand<T> command, T value, Action runOnExecuted = null, bool bindToVisible = false)
		{
			BindToCommandCore(button, command, value, runOnExecuted, bindToVisible);
		}

		public static void BindToCommand<T>(this ButtonBase button, ICommand<T> command, BindingSource source)
		{
			BindToCommandCore(button, command, default(T));

			source.CurrentItemChanged += (sender, args) => button.Enabled = command.CanExecute(default(T));
		}

		private static void BindToCommandCore<T>(ToolStripItem control, ICommand<T> command, T value, Action runOnExecuted = null, bool bindToVisible = false)
		{
			control.Click += (_, _1) =>
			{
				if (command.CanExecute(value))
				{
					command.Execute(value);
					if (runOnExecuted != null)
					{
						runOnExecuted.Invoke();
					}
				}
			};

			if (bindToVisible)
			{
				control.Visible = command.CanExecute(value);
			}
			else
			{
				control.Enabled = command.CanExecute(value);
			}

			command.CanExecuteChanged += (_1, _2) =>
			{
				bool canExecute = command.CanExecute(value);
				if (bindToVisible)
				{
					control.Visible = canExecute;
				}
				else
				{
					control.Enabled = canExecute;
				}
			};			
		}

		private static void BindToCommandCore<T>(Control control, ICommand<T> command, T value, Action runOnExecuted = null, bool bindToVisible = false)
		{
			control.Click += (_, _1) =>
			{
				if (command.CanExecute(value))
				{
					command.Execute(value);
					if (runOnExecuted != null)
					{
						runOnExecuted.Invoke();
					}
				}
			};

			if (bindToVisible)
			{
				control.Visible = command.CanExecute(value);
			}
			else
			{
				control.Enabled = command.CanExecute(value);
			}

			command.CanExecuteChanged += (_1, _2) =>
			{
				bool canExecute = command.CanExecute(value);
				if (bindToVisible)
				{
					control.Visible = canExecute;
				}
				else
				{
					control.Enabled = canExecute;
				}
			};
		}

		public static void BindToCommand<T>(this ToolStripMenuItem item, ICommand<T> command)
		{
			item.Click += (_, _1) =>
			{
				if (command.CanExecute())
				{
					command.Execute();
				}
			};

			item.Enabled = command.CanExecute();
			command.CanExecuteChanged += (_1, _2) =>
			{
				item.Enabled = command.CanExecute();
			};
		}

		public static void BindToCommand<T>(this DataGridViewColumn col, ICommand<T> command)
		{
			col.DataGridView.CellContentClick += (_, e) =>
			{
				if (e.RowIndex >= 0)
				{
					T t;
					try
					{
						t = (T)col.DataGridView.Rows[e.RowIndex].DataBoundItem;
					}
					catch (InvalidCastException)
					{
						t = default(T);
					}

					if (e.ColumnIndex == col.Index && t != null && command.CanExecute(t))
					{
						command.Execute(t);
					}
				}
			};
		}

		public static void CreateMenuItem<T>(this ToolStripMenuItem item, IMenuCommand<T> command, string text, T context)
		{
			var menuItem = item.DropDownItems.Add(text);
			item.DropDownOpening += delegate
			{
				var state = command.QueryState(context);

				menuItem.Enabled = (state & MenuCommandState.Enabled) != 0;
				menuItem.Visible = (state & MenuCommandState.Visible) != 0;
			};

			menuItem.Click += delegate
			{
				command.Execute(context);
			};
		}

		public static ICommand<T> CreateEventCommand<T>(this IEventAggregator aggregator)
		{
			return new DelegateCommand<T>(aggregator.Publish);
		}

		public static ICommand<object> CreateObjectEventCommand<TEvent>(this IEventAggregator aggregator,
			TEvent eventType)
		{
			return new DelegateCommand<object>(_ => aggregator.Publish(eventType));
		}

		public static void Execute<T>(this ICommand<T> cmd)
		{
			cmd.Execute(default(T));
		}

		public static bool CanExecute<T>(this ICommand<T> cmd)
		{
			return cmd.CanExecute(default(T));
		}

		public static void InvokeOnEnter<T>(this TextBoxBase textBox, ICommand<T> command)
		{
			textBox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode != Keys.Enter || !command.CanExecute(default(T)))
				{
					return;
				}

				command.Execute(default(T));
			};
		}
	}
}
