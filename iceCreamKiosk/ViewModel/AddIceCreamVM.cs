using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class AddIceCreamVM:ViewModelBase
    {
        public ICommand AddIceCreamCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public IceCreamModel IceCream { get; set; }
        public AddIceCreamVM()//StoreModel store = null)
        {
            //this.Store = store;
            if (this.IceCream == null)
            {
                this.IceCream = new IceCreamModel();

            }
            AddIceCreamCommand = new RelayCommand(ExecuteAddStore, CanExecuteAddStore);
            DismissCommand = new MyCommand(ExecuteDismiss, CanExecuteDismiss);
            //DismissCommand.CanExecuteChanged+=
            ////public event EventHandler CanExecuteChanged
            ////{
            ////    add { CommandManager.RequerySuggested += value; }
            ////    remove { CommandManager.RequerySuggested -= value; }
            ////}
            CancelCommand = new RelayCommand(ExecuteCancel);


        }

        public Boolean CanExecuteAddStore()
        {
            return false;//TDOD I have to omplement this method
        }

        public Boolean CanExecuteDismiss()
        {
            return false;
            //i have to add this:  return !IceCream.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            //TDOD I have to implement this moethod
        }
        public void ExecuteDismiss() {
            // i have to add this:  IceCream.ClearAllFeilds(); 
        }
        public void ExecuteCancel()
        {
            //TDOD i have to close the window }
        }
    }
}
