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
    public class AddStoreVM : ViewModelBase
    {
        public ICommand AddStoreCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public StoreModel Store { get; set; }
        public AddStoreVM()//StoreModel store = null)
        {
            //this.Store = store;
            if (this.Store == null)
            {
                this.Store = new StoreModel();

            }
                AddStoreCommand = new RelayCommand(ExecuteAddStore, CanExecuteAddStore);
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
            return !Store.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            //TDOD I have to implement this moethod
        }
        public void ExecuteDismiss() { Store.ClearAllFeilds(); }
        public void ExecuteCancel()
        {
            //TDOD i have to close the window }
        }
    }
}
