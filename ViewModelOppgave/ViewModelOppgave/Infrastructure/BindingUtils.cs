using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ViewModelOppgave.Infrastructure
{
	public static class Converters
	{
		public static Func<bool, object> NegateBoolean = v => !v;
		public static Func<object, bool> NegateBooleanDown = v => !((bool)v);
	}

	public static class BindingUtils
	{
		public static bool IsRestructuringListChange(ListChangedType changeType)
		{
			return changeType == ListChangedType.ItemAdded ||
				changeType == ListChangedType.ItemDeleted ||
				changeType == ListChangedType.ItemMoved ||
				changeType == ListChangedType.Reset;
		}

		/// <summary>
		/// Reads any bindings to the property 'controlPropertyName' on the specified controls.
		/// </summary>
		/// <param name="controlPropertyName"></param>
		/// <param name="components"></param>
		public static void PullBinding(string controlPropertyName, params IBindableComponent[] components)
		{
			foreach (Binding binding in EnumerateBindings(controlPropertyName, components))
			{
				binding.ReadValue();
			}
		}

		public static void PullAllBindings(this IBindableComponent bindableComponent)
		{
			foreach (Binding dataBinding in bindableComponent.DataBindings)
			{
				dataBinding.ReadValue();
			}
		}

		public static void PullBinding<TComponent, TProperty>(this TComponent component,
			Expression<Func<TComponent, TProperty>> expression) where TComponent : IBindableComponent
		{
			PullBinding(component.PropertyName(expression), component);
		}

		public static bool HasBinding<TComponent, TProperty>(this TComponent component,
			Expression<Func<TComponent, TProperty>> expression) where TComponent : IBindableComponent
		{
			return component.DataBindings[expression.PropertyName()] != null;
		}

		public static void PushBinding<TComponent, TProperty>(this TComponent component,
			Expression<Func<TComponent, TProperty>> expression) where TComponent : IBindableComponent
		{
			PushBinding(component.PropertyName(expression), component);
		}


		/// <summary>
		/// Pushes the specified control property back to the datasource in the components.
		/// </summary>
		/// <param name="controlPropertyName"></param>
		/// <param name="components"></param>
		public static void PushBinding(string controlPropertyName, params IBindableComponent[] components)
		{
			foreach (Binding binding in EnumerateBindings(controlPropertyName, components))
			{
				binding.WriteValue();
			}
		}

		/// <summary>
		/// Binds a single object to a binding source, or binds to an empty array if the reference is null
		/// </summary>
		public static void Bind<T>(BindingSource bs, T obj)
		{
			if (!Equals(obj, default(T)))
			{
				object previousSource = bs.DataSource;
				bs.DataSource = obj;

				if (ReferenceEquals(previousSource, bs.DataSource))
				{
					bs.ResetCurrentItem();
				}
			}
			else
			{
				bs.DataSource = new T[] { };
			}
		}

		private static IEnumerable<Binding> EnumerateBindings(string controlPropertyName, IEnumerable<IBindableComponent> components)
		{
			foreach (IBindableComponent component in components)
			{
				Binding binding = component.DataBindings[controlPropertyName];
				if (binding != null)
				{
					yield return binding;
				}
			}
		}

		public static void NegateBooleanBinding(Binding binding)
		{
			binding.Format += delegate(object sender, ConvertEventArgs args)
			{
				args.Value = !((bool)args.Value);
			};
		}

		public static void BindDataSourceTo<TSource>(this BindingSource bindingSource, BindingSource source,
			Expression<Func<TSource, object>> expression)
		{
			PropertyDescriptor desc = GetPropertyDescriptor(expression, source);


			Action setSource = () =>
			{
				bindingSource.DataSource = source.Current != null && desc.GetValue(source.Current) != null ? desc.GetValue(source.Current) : desc.PropertyType;
			};

			source.ListChanged += (sender, args) => setSource();

			setSource();
		}

		public static void BindMasterDetail<TSource>(this BindingSource master, BindingSource detail)
		{
			master.CurrentChanged += (sender, args) => detail.DataSource = master.Current;
			detail.DataSource = master.Current;
		}

		private static PropertyDescriptor GetPropertyDescriptor<TSource>(Expression<Func<TSource, object>> expression, BindingSource source)
		{
			string propName = expression.PropertyName();
			PropertyDescriptor desc = TypeDescriptor.GetProperties(source).Find(propName, false);

			if (desc == null)
			{
				var list = source as ITypedList;
				if (list != null)
				{
					desc = list.GetItemProperties(null).Find(propName, false);
				}
			}

			if (desc == null)
			{
				throw new ArgumentException(string.Format("Property {0} not found on source {1}", propName, source));
			}
			return desc;
		}

		public static void DataBindCurrent<TSource>(this BindingSource source, BindingSource bindToSource, Expression<Func<TSource, object>> prop)
		{
			var desc = GetPropertyDescriptor(prop, bindToSource);

			bool binding = false;

			Action updateBindingSourcePosition = () =>
			{
				try
				{
					binding = true;
					source.Position = source.IndexOf(desc.GetValue(bindToSource.Current));
				}
				finally
				{
					binding = false;
				}
			};

			bindToSource.ListChanged += (sender, args) =>
			{
				if (!binding &&
					(args.ListChangedType != ListChangedType.ItemChanged
					|| args.PropertyDescriptor == null
					|| args.PropertyDescriptor.Name == prop.PropertyName()))
				{
					updateBindingSourcePosition();
				}
			};

			source.PositionChanged += (o, eventArgs) =>
			{
				if (!binding)
				{
					try
					{
						binding = true;
						desc.SetValue(bindToSource.Current, source.Current);
					}
					finally
					{
						binding = false;
					}
				}
			};

			updateBindingSourcePosition();
		}


		//public static void Bind<TSearchControl, TProperty, TObject>(this TSearchControl control, TObject obj,
		//	Func<TObject, TProperty> getter, Action<TObject, TProperty> setter)
		//	where TProperty : class
		//	where TSearchControl : SearchControl
		//{
		//	control.SelectedChanged += (_1, _2) => setter(obj, control.SelectedObject as TProperty);
		//	control.VisibleChanged += (_1, _2) => control.SelectedObject = getter(obj);

		//}



		public static void SetDataProperty<TSource>(this DataGridViewColumn col,
			Expression<Func<TSource, object>> expression)
		{
			col.DataPropertyName = expression.PropertyName();
		}

		public static void SetDataProperty<TSource, TProp>(this DataGridViewColumn col,
			Expression<Func<TSource, TProp>> expression)
		{
			col.DataPropertyName = expression.PropertyName();
		}

		public static void SetDisplayProperty<TSource, TProp>(this DataGridViewColumn col,
			Expression<Func<TSource, TProp>> expression)
		{
			col.DataPropertyName = expression.PropertyName();
		}

		public static void BindComboData<TSource, TValue>(this DataGridViewComboBoxColumn col,
			Expression<Func<TSource, string>> displayMember, Expression<Func<TSource, TValue>> valueMember)
		{
			col.DisplayMember = displayMember.PropertyName();
			col.ValueMember = valueMember.PropertyName();
		}
/*

		private static ManuallyDisposableBinding HandlePropertyChanged<T, TProp>(this T inpc, Expression<Func<T, TProp>> filter,
			Action<string> handler) where T : INotifyPropertyChanged
		{
			var propertyChanged = OnPropertyChanged(filter, handler);
			inpc.PropertyChanged += propertyChanged;

			return new ManuallyDisposableBinding(() => inpc.PropertyChanged -= propertyChanged);
		}

		private static PropertyChangedEventHandler OnPropertyChanged<T, TProp>(Expression<Func<T, TProp>> filter, Action<string> handler) where T : INotifyPropertyChanged
		{
			return (sender, args) =>
			{
				if (args.PropertyName == null || args.PropertyName == filter.PropertyName())
				{
					handler(args.PropertyName);
				}
			};
		}

		public static ManuallyDisposableBinding DatabindFocus<TControl, TSource>(this TControl control, TSource source, Expression<Func<TSource, bool>> expression)
			where TControl : Control
			where TSource : INotifyPropertyChanged
		{
			PropertyWrapper<TSource, bool> prop = source.CreatePropertyWrapper(expression);

			var manualBinding = source.HandlePropertyChanged(expression, p =>
			{
				if (prop.GetValue(source))
				{
					control.SelectAndFocus();
				}
			});

			var controlOnEnter = ControlOnEnter<TControl, TSource>(source, prop);
			control.Enter += controlOnEnter;
			var controlOnLeave = ControlOnLeave<TControl, TSource>(source, prop);
			control.Leave += controlOnLeave;

			return new ManuallyDisposableBinding(new[] { manualBinding }, () => control.Enter -= controlOnEnter, () => control.Leave -= controlOnLeave);
		}

		private static EventHandler ControlOnLeave<TControl, TSource>(TSource source, PropertyWrapper<TSource, bool> prop)
			where TControl : Control
			where TSource : INotifyPropertyChanged
		{
			return (sender, e) => prop.SetValue(source, false);
		}

		private static EventHandler ControlOnEnter<TControl, TSource>(TSource source, PropertyWrapper<TSource, bool> prop)
			where TControl : Control
			where TSource : INotifyPropertyChanged
		{
			return (sender, e) => prop.SetValue(source, true);
		}

		public static ManuallyDisposableBinding TriggerFocusOn<TControl, TSource>(this TControl control, TSource source,
			Expression<Func<TSource, bool>> expression)
			where TControl : Control
			where TSource : INotifyPropertyChanged
		{
			PropertyWrapper<TSource, bool> prop = source.CreatePropertyWrapper(expression);

			return source.HandlePropertyChanged(expression, p =>
			{
				if (prop.GetValue(source))
				{
					control.SelectAndFocus();
				}
			});
		}
*/
		public static BindStage1<TControlProp> DataBind<TControl, TControlProp>(this TControl c,
			Expression<Func<TControl, TControlProp>> ce)
			where TControl : Control
		{
			string controlProperty = ce.PropertyName();

			BindInfo info = new BindInfo { ControlProperty = controlProperty, Control = c };

			return new BindStage1<TControlProp>(info);
		}

		internal class BindInfo
		{
			public string SourceProperty;
			public string ControlProperty;
			public object DataSource;
			public Control Control;
			public ConvertEventHandler Format;
			public DataSourceUpdateMode? Mode;
			private object _dataSourceNullValue;
			public Binding Binding;

			public bool DataSourceNullValueSet { get; private set; }

			public object DataSourceNullValue
			{
				get
				{
					return _dataSourceNullValue;
				}
				set
				{
					_dataSourceNullValue = value;
					DataSourceNullValueSet = true;
				}
			}
		}

		public class BindStage1<TControlProp>
		{
			private readonly BindInfo _info;

			internal BindStage1(BindInfo info)
			{
				_info = info;
			}

			public BindStage2<TControlProp, TSource> ToType<TSource>()
			{
				return new BindStage2<TControlProp, TSource>(_info);
			}

			public BindStage2Ex<TControlProp, TSource> To<TSource>(TSource source)
			{
				_info.DataSource = (object)source;
				return new BindStage2Ex<TControlProp, TSource>(_info);
			}

			public BindStage2Ex<TControlProp, TSource> To<TSource>(BindingSource source)
			{
				_info.DataSource = source;
				return new BindStage2Ex<TControlProp, TSource>(_info);
			}
		}

		public class BindStage2<TControlProp, TSource>
		{
			private readonly BindInfo _info;

			internal BindStage2(BindInfo info)
			{
				_info = info;
			}

			public BindStage3<TControlProp, TSource, TSourceProp> Property<TSourceProp>(
				Expression<Func<TSource, TSourceProp>> func)
			{
				_info.SourceProperty = func.PropertyName();
				return new BindStage3<TControlProp, TSource, TSourceProp>(_info);
			}
		}

		public interface IWithOptionalBindStage<TControlProp, TSource, TSourceProp>
		{
			OptionalBindStage<TControlProp, TSource, TSourceProp> With { get; }
		}

		public class BindStage2Ex<TControlProp, TSource>
		{
			private readonly BindInfo _info;

			internal BindStage2Ex(BindInfo info)
			{
				_info = info;
			}

			public IWithOptionalBindStage<TControlProp, TSource, TSourceProp> Property<TSourceProp>
				(Expression<Func<TSource, TSourceProp>> expression)
			{
				_info.SourceProperty = expression.PropertyName();

				_info.Binding = new Binding(_info.ControlProperty, _info.DataSource, _info.SourceProperty);
				_info.Control.DataBindings.Add(_info.Binding);

				return new OptionalBindStage<TControlProp, TSource, TSourceProp>(_info);
			}
		}

		public class OptionalBindStage<TControlProp, TSource, TSourceProp> : IWithOptionalBindStage<TControlProp, TSource, TSourceProp>
		{
			private readonly BindInfo _info;

			internal OptionalBindStage(BindInfo info)
			{
				_info = info;
			}

			OptionalBindStage<TControlProp, TSource, TSourceProp> IWithOptionalBindStage<TControlProp, TSource, TSourceProp>.With
			{
				get { return this; }
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp> DataSourceUpdateMode(DataSourceUpdateMode mode)
			{
				_info.Binding.DataSourceUpdateMode = mode;
				return this;
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp> DataSourceNullValue(object value)
			{
				_info.Binding.DataSourceNullValue = value;
				return this;
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp> ControlUpdateMode(ControlUpdateMode mode)
			{
				_info.Binding.ControlUpdateMode = mode;
				return this;
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp> FormatString(string format)
			{
				_info.Binding.FormatString = format;
				return this;
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp> Formatting(bool enabled)
			{
				_info.Binding.FormattingEnabled = enabled;
				return this;
			}

			public OptionalBindStage<TControlProp, TSource, TSourceProp>
				Formatter(Func<TSourceProp, TControlProp> formatter)
			{
				_info.Binding.Format += (sender, args) =>
				{
					args.Value = formatter((TSourceProp)args.Value);
				};

				return this;
			}
		}

		public class BindStage3<TControlProp, TSource, TSourceProp>
		{
			private readonly BindInfo _info;

			internal BindStage3(BindInfo info)
			{
				_info = info;
			}

			public Binding OnSource(object o)
			{
				var binding = new Binding(_info.ControlProperty, o, _info.SourceProperty, true);
				if (_info.Format != null)
				{
					binding.Format += _info.Format;
				}

				binding.DataSourceUpdateMode = _info.Mode ?? binding.DataSourceUpdateMode;

				if (_info.DataSourceNullValueSet)
				{
					binding.DataSourceNullValue = _info.DataSourceNullValue;
				}

				_info.Control.DataBindings.Add(binding);

				return binding;
			}

			public BindStage3<TControlProp, TSource, TSourceProp> ConvertFromSourceBy(Func<TSourceProp, object> func)
			{
				_info.Format = new ConvertEventHandler((_, args) =>
				{
					args.Value = func((TSourceProp)args.Value);
				});
				return this;
			}

			public BindStage3<TControlProp, TSource, TSourceProp> WithSourceUpdateMode(DataSourceUpdateMode mode)
			{
				_info.Mode = mode;
				return this;
			}

			public BindStage3<TControlProp, TSource, TSourceProp> WithDataSourceNullValue(object value)
			{
				_info.DataSourceNullValue = null;
				return this;
			}
		}

		public static GridBindStage1<TSource> BindTo<TSource>(this DataGridView grid)
		{
			return new GridBindStage1<TSource>(grid);
		}

		public class GridBindStage1<TSource> : IGridBindStage2<TSource>
		{
			private readonly DataGridView _grid;
			private DataGridViewColumn _column;

			public GridBindStage1(DataGridView grid)
			{
				_grid = grid;
			}

			public IGridBindStage2<TSource> Column(DataGridViewColumn column)
			{
				_column = column;
				return this;
			}

			public void OnSource(object o)
			{
				_grid.DataSource = o;
			}

			GridBindStage1<TSource> IGridBindStage2<TSource>.To<TRet>(Expression<Func<TSource, TRet>> expr)
			{
				_column.DataPropertyName = expr.PropertyName();

				return new GridBindStage1<TSource>(_grid);
			}
		}

		public interface IGridBindStage2<TSource>
		{
			GridBindStage1<TSource> To<TRet>(Expression<Func<TSource, TRet>> expr);
		}
	}
}
