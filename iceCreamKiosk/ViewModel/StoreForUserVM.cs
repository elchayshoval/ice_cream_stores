using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class StoreForUserVM:ViewModelBase
    {
        private IceCreamLogic IceCreamLogic = new IceCreamLogic();

        public StoreModel StoreModel { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public StoreForUserVM(Store store)
        {
            ShowSelectedCommand = new RelayCommand<IceCream>(ExecuteShowSelectedCommand);
            StoreModel = new StoreModel(store);
            
        }

        
        private void ExecuteShowSelectedCommand(IceCream iceCream)
        {
            if (iceCream != null)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream,StoreModel.GetAsStore()));
            }
        }

        internal void UpdateIceCreamsCollection()
        {
            StoreModel.UpdateIceCreamCollection();
        }


    }
}
