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
    class LandedVM:ViewModelBase
    {
        public ICommand GoToSearchStoreCommand { get; set; }
        public ICommand GoToSearchIceCreamCommand { get; set; }
        public ICommand GoToAddFeedbackCommand { get; set; }
        public ICommand GoToAddAdminCommand { get; set; }

        public LandedVM()
        { 
           // GoToSearchStoreCommand = new RelayCommand(() => MessengerInstance.Send(""));//TODO when added the vm we gave to update the send parameter
            GoToSearchIceCreamCommand = new MyCommand(ExecuteGoToSearchIceCreamCommand);
            //GoToAddFeedbackCommand = new RelayCommand(() => MessengerInstance.Send(""));//TODO i have to complete
             GoToAddAdminCommand = new RelayCommand(GoToAdminPage);
        }

        private void GoToAdminPage()
        {
            MessengerInstance.Send<ViewModelBase>(new StoresCollectionForAdminVM());
        }

        private void ExecuteGoToSearchIceCreamCommand()
        {
            MessengerInstance.Send<ViewModelBase>(new FilterVM());
        }
    }
}
