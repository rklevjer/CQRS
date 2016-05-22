//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System;
//using System.ComponentModel;
//using System.Reactive.Disposables;
//using System.Reactive.Subjects;

using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace ViewModelOppgave.Infrastructure.ViewModels
{
	public class ViewModelBase : IRaisePropertyChanged, INotifyPropertyChanged, IDisposable,
		IObservable<PropertyChangedEventArgs>
	{
		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		private readonly Subject<PropertyChangedEventArgs> _propertyChangedSubject = new Subject<PropertyChangedEventArgs>();

		public ViewModelBase()
		{
			_disposables.Add(_propertyChangedSubject);
		}

		public void RaisePropertyChanged(string propName)
		{
			var handler = PropertyChanged;
			var propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
			if (handler != null)
			{
				handler(this, propertyChangedEventArgs);
			}
			_propertyChangedSubject.OnNext(propertyChangedEventArgs);
		}

		protected void AddSubscription(IDisposable disposable)
		{
			_disposables.Add(disposable);
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public virtual void Dispose()
		{
			_disposables.Dispose();
		}

		public IDisposable Subscribe(IObserver<PropertyChangedEventArgs> observer)
		{
			return _propertyChangedSubject.Subscribe(observer);
		}
	}
}

