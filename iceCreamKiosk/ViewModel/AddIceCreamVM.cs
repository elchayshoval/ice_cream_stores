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
    class AddIceCreamVM : ViewModelBase
    {
        public ICommand AddIceCreamCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public IceCreamModel IceCreamModel { get; set; }

        private IceCreamLogic iceCreamLogic = new IceCreamLogic();
        public AddIceCreamVM(Store store)
        {

            IceCream iceCream = new IceCream();
            iceCream.StoreId = store.StoreId;
            this.IceCreamModel = new IceCreamModel(iceCream);

            AddIceCreamCommand = new MyCommand(ExecuteAddStore, CanExecuteAddStore);
            DismissCommand = new MyCommand(ExecuteDismiss, CanExecuteDismiss);
   
            CancelCommand = new RelayCommand(ExecuteCancel);


        }

        public Boolean CanExecuteAddStore()
        {
            return IceCreamModel.IsValidate();
        }

        public Boolean CanExecuteDismiss()
        {
              return !IceCreamModel.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            IceCream iceCream = IceCreamModel.GetAsStore();
            iceCream.IceCreamId = Guid.NewGuid();
            iceCreamLogic.AddIceCream(iceCream);
            //i have to send message
        }
        public void ExecuteDismiss()
        {
            IceCreamModel.ClearAllFeilds();
        }
        public void ExecuteCancel()
        {
            MessengerInstance.Send<ViewModelBase>(null);
        }
    }
}
