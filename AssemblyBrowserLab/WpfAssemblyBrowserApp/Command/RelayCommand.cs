using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAssemblyBrowserApp.Command
{
    public class RelayCommand : ICommand
    {

        private Action<object> execute;     
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        //Событие CanExecuteChanged вызывается при изменении условий, указывающий, 
        // может ли команда выполняться.Для этого используется событие CommandManager.RequerySuggested
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        // Определяет, может ли команда выполняться
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        // Собственно выполняет логику команды
        //Для его выполнения в конструкторе команды передается делегат типа Action<object>. 
        //При этом класс команды не знает какое именно действие будет выполняться.
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

    }
}
