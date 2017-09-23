using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CodeChallenge.ViewModels
{
    public class BaseNotifyableViewModel : INotifyPropertyChanged  // DependencyObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String property = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(property);
            return true;
        }

        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class BaseNotifyableViewModel<T> : BaseNotifyableViewModel where T : class, new()
    {
        protected T This;

        public static implicit operator T(BaseNotifyableViewModel<T> thing) { return thing.This; }

        public BaseNotifyableViewModel(T thing = null)
        {
            This = (thing == null) ? new T() : thing;
        }
    }
}
