using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResizeFrameControl
{
    class BaseBindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class BaseBindableWithDelegateCommand : BaseBindable
    {
        public interface IDelegateCommand : ICommand { void RaiseCanExecuteChanged(); }

        public class DelegateCommand : IDelegateCommand
        {
            Action<object> execute;
            Func<object, bool> canExecute;
            public event EventHandler? CanExecuteChanged;

            public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }
            public DelegateCommand(Action<object> execute)
            {
                this.execute = execute;
                this.canExecute = this.AlwaysCanExecute;
            }
            public void Execute(object? param) { if(param !=null) execute(obj: param); }

            public bool CanExecute(object? param) { if (param == null) return false; return canExecute(param); }
            public void RaiseCanExecuteChanged()
            {
                if (CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);
            }
            private bool AlwaysCanExecute(object param) { return true; }
        }
    }
}
