using BE;
using BL;
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
        private StoreLogic storeLogic = new StoreLogic();
        public ICommand AddStoreCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public StoreModel StoreModel { get; set; }
        public AddStoreVM()//StoreModel store = null)
        {
            //this.Store = store;
            if (this.StoreModel == null)
            {
                this.StoreModel = new StoreModel(null);

            }
                AddStoreCommand = new MyCommand(ExecuteAddStore, CanExecuteAddStore);
                DismissCommand = new MyCommand(ExecuteDismiss, CanExecuteDismiss);
                CancelCommand = new MyCommand(ExecuteCancel);
            

        }

        public Boolean CanExecuteAddStore()
        {
            return StoreModel.IsValidate();
        }

        public Boolean CanExecuteDismiss()
        {
            return !StoreModel.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            Store s = StoreModel.GetAsStore();
            s.StoreId = Guid.NewGuid();
            storeLogic.AddStore(s);
            //i have to send message
        }
        public void ExecuteDismiss() { StoreModel.ClearAllFeilds(); }
        public void ExecuteCancel()
        {
            //TDOD i have to close the window }
        }
    }
}
