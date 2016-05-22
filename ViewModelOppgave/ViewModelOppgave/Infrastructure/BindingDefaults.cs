using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using System.Windows.Forms;

namespace ViewModelOppgave.Infrastructure
{
	public class BindingDefaults
	{
		public DataSourceUpdateMode? DataSourceUpdateMode { get; set; }
		public ResourceManager ResourceManager { get; set; }
	}

	public static class FluentBindingSourceExtensions
	{


		public static BSBindStage1<TSource> CreateBindingsFor<TSource>(this BindingSource source)
		{
			return new BSBindStage1<TSource>(source);
		}

		public class BSBindStage1<TSource>
		{
			private readonly BindingSource _source;
			private readonly BindingDefaults _defaults;

			public BSBindStage1(BindingSource source) : this(source, new BindingDefaults())
			{
			}

			public BSBindStage1(BindingSource source, BindingDefaults defaults)
			{
				_defaults = defaults;
				_source = source;
			}


			public BSBindStage2<TSource, TProp> From<TProp>(Expression<Func<TSource, TProp>> expr)
			{
				return new BSBindStage2<TSource, TProp>(_source, expr, _defaults);
			}

			public BSBindStage1<TSource> Defaults(BindingDefaults defaults)
			{
				return new BSBindStage1<TSource>(_source, defaults);
			}
		}


		public class BSBindStage2<TSource, TProp>
		{
			private readonly BindingSource _source;
			private readonly Expression<Func<TSource, TProp>> _propertyExpression;
			private readonly BindingDefaults _defaults;

			public BSBindStage2(BindingSource source, Expression<Func<TSource, TProp>> propertyExpression,
				BindingDefaults defaults)
			{
				_source = source;
				_propertyExpression = propertyExpression;
				_defaults = defaults;
			}

			public BSBindStage3<TControl, TSource, TProp> To<TControl>(TControl control) where TControl : Control
			{
				return new BSBindStage3<TControl, TSource, TProp>(control, _source, _propertyExpression,
					_defaults);
			}

		}

	


		public class BSBindStage3<TControl, TSource, TProp> where TControl : Control
		{
			private readonly TControl _control;
			private readonly BindingSource _source;
			private readonly Expression<Func<TSource, TProp>> _propertyExpression;
			private readonly BindingDefaults _defaults;

			public BSBindStage3(TControl control, BindingSource source,
				Expression<Func<TSource, TProp>> propertyExpression,
				BindingDefaults defaults)
			{
				_control = control;
				_source = source;
				_propertyExpression = propertyExpression;
				_defaults = defaults;
			}

			public IBSBindStage4<TControl, TSource, TProp, TControlProp> On<TControlProp>(Expression<Func<TControl, TControlProp>> controlExpr)
			{
				return new BSBindStage4<TControl, TSource, TProp, TControlProp>(controlExpr, _control, _source, _propertyExpression, _defaults);
			}

		}



		public class BSBindStage4<TControl, TSource, TProp, TControlProp>
			: IBSBindStage4<TControl, TSource, TProp, TControlProp>, IBSOptions<TControl, TSource, TProp, TControlProp>
			where TControl : Control
		{
			private readonly TControl _control;
			private readonly BindingSource _source;
			private readonly Expression<Func<TSource, TProp>> _propertyExpression;
			private readonly BindingDefaults _defaults;
			private readonly Binding _binding;
			private readonly Expression<Func<TControl, TControlProp>> _controlExpr;

			public BSBindStage4(Expression<Func<TControl, TControlProp>> controlExpr, TControl control, BindingSource source, Expression<Func<TSource, TProp>> propertyExpression, BindingDefaults defaults)
			{
				_controlExpr = controlExpr;
				_control = control;
				_source = source;
				_propertyExpression = propertyExpression;
				_defaults = defaults;
				_binding = _control.DataBindings.Add(controlExpr.PropertyName(), _source, propertyExpression.PropertyName(), controlExpr.PropertyName() != PropertyName<Control>.For(c => c.Enabled));
				ApplyDefaults(_binding, _defaults, propertyExpression);
			}

			private static void ApplyDefaults(Binding binding, BindingDefaults defaults, Expression<Func<TSource, TProp>> propertyExpression)
			{
				binding.DataSourceUpdateMode = defaults.DataSourceUpdateMode ?? binding.DataSourceUpdateMode;
			}

			public BSBindStage2<TSource, TNewProp> From<TNewProp>(Expression<Func<TSource, TNewProp>> expr)
			{
				return new BSBindStage2<TSource, TNewProp>(_source, expr, _defaults);
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.DataSourceUpdateMode(DataSourceUpdateMode mode)
			{
				_binding.DataSourceUpdateMode = mode;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.DataSourceNullValue(object value)
			{
				_binding.DataSourceNullValue = value;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.ControlUpdateMode(ControlUpdateMode mode)
			{
				_binding.ControlUpdateMode = mode;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.FormatString(string format)
			{
				_binding.FormatString = format;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.Formatting(bool enabled)
			{
				_binding.FormattingEnabled = enabled;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.Formatter(Func<TProp, object> formatter)
			{
				_binding.FormattingEnabled = true;
				_binding.Format += (sender, args) =>
				{
					args.Value = formatter((TProp)args.Value);
				};
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.EnumFormatter(ResourceManager resourceManager)
			{
				_binding.FormattingEnabled = true;
				_binding.Format += (sender, args) =>
				{
					var typeOf = typeof(TProp);
					typeOf = Nullable.GetUnderlyingType(typeOf) ?? typeOf;

					if (args.Value == null || args.Value.GetType() != typeOf)
					{
						return;
					}
					var val = (TProp)args.Value;
					args.Value = EnumBinding<TProp>.CreateEnumBinding(resourceManager, val).Name;
				};
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.Parser(Func<object, TProp> parser)
			{
				_binding.FormattingEnabled = true;
				_binding.Parse += (sender, args) =>
				{
					args.Value = parser(args.Value);
				};
				return this;
			}

			//IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.EnumBinding()
			//{
				//	_binding.FormatWith<TProp>(_defaults.ResourceManager);
				//	return this;
			//}

			//IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.Validator(
			//	Expression<Func<TSource, IPropertyValidator>> expr)
			//{
			//	_binding.Parse += (sender, args) =>
			//	{
			//		if (_source.Current == null)
			//		{
			//			return;
			//		}
			//		var validator = _source.GetPropertyDescriptor(expr.PropertyName())
			//			.GetValue(_source.Current) as IPropertyValidator;
			//		if (validator == null)
			//		{
			//			return;
			//		}
			//		if (!validator.IsValid(args.Value))
			//		{
			//			// set to the original value on the vm (no change)
			//			args.Value = _source.GetPropertyDescriptor(_propertyExpression.PropertyName())
			//				.GetValue(_source.Current);

			//			// control will assume the value got set properly and won't update its displayed value
			//			// need to set the value on the control manually
			//			// fortunately this doesn't seem to trigger a cascade
			//			var controlDescriptor = TypeDescriptor.GetProperties(_control)
			//				.Cast<PropertyDescriptor>()
			//				.Single(pd => pd.Name == _controlExpr.PropertyName());
			//			controlDescriptor.SetValue(_control, args.Value);
			//		}
			//	};
			//	return this;
			//}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.FormattingEnabled()
			{
				_binding.FormattingEnabled = true;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.FormattingDisabled()
			{
				_binding.FormattingEnabled = false;
				return this;
			}

			IBSBindStage4<TControl, TSource, TProp, TControlProp> IBSOptions<TControl, TSource, TProp, TControlProp>.Validating(Func<TControlProp, bool> validating)
			{
				_control.Validating += (o, e) => e.Cancel = !validating(this.ValueGetter.Invoke(_control));
				return this;
			}

			public IBSBindStage4<TControl, TSource, TProp, TControlProp> EnumBinding()
			{
				throw new NotImplementedException();
			}

			public IBSBindStage4<TControl, TSource, TProp, TControlProp> BooleanInverter()
			{
				if (typeof(TProp) != typeof(bool))
				{
					throw new InvalidOperationException("Only valid on booleans");
				}
				return (IBSBindStage4<TControl, TSource, TProp, TControlProp>)
					((IBSBindStage4<TControl, TSource, bool, TControlProp>)this).With
					.Formatter(Converters.NegateBoolean).With
					.Parser(Converters.NegateBooleanDown);
			}

			private Func<TControl, TControlProp> _valueGetter;
			private Func<TControl, TControlProp> ValueGetter
			{
				get { return _valueGetter ?? (_valueGetter = _controlExpr.Compile()); }
			}

			BSBindStage2<TSource, TNewProp> IBSBindStage4<TControl, TSource, TProp, TControlProp>.AndThenFrom<TNewProp>(Expression<Func<TSource, TNewProp>> expr)
			{
				return new BSBindStage2<TSource, TNewProp>(_source, expr, _defaults);
			}

			IBSOptions<TControl, TSource, TProp, TControlProp> IBSBindStage4<TControl, TSource, TProp, TControlProp>.With
			{
				get { return this; }
			}
		}



		public interface IBSBindStage4<TControl, TSource, TProp, TControlProp>
		{
			BSBindStage2<TSource, TNewProp> AndThenFrom<TNewProp>(Expression<Func<TSource, TNewProp>> expr);
			IBSOptions<TControl, TSource, TProp, TControlProp> With { get; }
		}

		public interface IBSOptions<TControl, TSource, TProp, TControlProp>
		{
			IBSBindStage4<TControl, TSource, TProp, TControlProp> DataSourceUpdateMode(DataSourceUpdateMode mode);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> DataSourceNullValue(object value);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> ControlUpdateMode(ControlUpdateMode mode);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> FormatString(string format);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> Formatting(bool enabled);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> FormattingEnabled();
			IBSBindStage4<TControl, TSource, TProp, TControlProp> FormattingDisabled();
			IBSBindStage4<TControl, TSource, TProp, TControlProp> Formatter(Func<TProp, object> formatter);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> EnumFormatter(ResourceManager resourceManager);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> Parser(Func<object, TProp> formatter);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> Validating(Func<TControlProp, bool> validating);
			IBSBindStage4<TControl, TSource, TProp, TControlProp> BooleanInverter();
			IBSBindStage4<TControl, TSource, TProp, TControlProp> EnumBinding();
		}
	}



	public interface IComboBindStage<T>
	{
		IComboBindStage<T> WithDisplayMember<TProp>(Expression<Func<T, TProp>> expression);
		IComboBindStage<T> WithValueMember<TProp>(Expression<Func<T, TProp>> expression);
		IComboBindStage<T> WithDataSource(IList<T> source);
		IComboBindStage<T> WithDataSourceOn<TUcp>(BindingSource bs, Expression<Func<TUcp, object>> expression);
	}
}
