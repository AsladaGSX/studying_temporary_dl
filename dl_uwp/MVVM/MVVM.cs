using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM
{
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void RaisePropertyChanged(params String[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (var property in propertyNames)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }

    public abstract class ViewModelBase : NotificationObject
    {
        protected void Set<T>(ref T storage, T value, [CallerMemberName] String propertyName = "")
        {
            if (EqualityComparer<Object>.Default.Equals(storage, value))
            {
                return;
            }

            storage = value;

            RaisePropertyChanged(propertyName);
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<Object> action;
            private readonly Predicate<Object> canExecute;

            public RelayCommand(Action<Object> action, Predicate<Object> canExecute = null)
            {
                this.action = action;
                this.canExecute = canExecute;
            }

            #region ICommand
            public event EventHandler CanExecuteChanged;

            public Boolean CanExecute(Object parameter) => action != null && (canExecute == null || canExecute(parameter));

            public void Execute(Object parameter) => action?.Invoke(parameter);
            #endregion

            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
