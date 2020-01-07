using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.model
{
    public class MyCommand :ICommand 
    {
        private Action execute;
        private Func<bool> canExecute;
        public MyCommand(Action execute, bool keepTargetAlive = false) 
        {
            
            this.execute=execute;
            this.canExecute = () => true;
        }

        public MyCommand(Action execute, Func<bool> canExecute, bool keepTargetAlive = false)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        

        public  event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
