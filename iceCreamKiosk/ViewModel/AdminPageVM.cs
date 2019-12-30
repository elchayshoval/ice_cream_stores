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
    class AdminPageVM : ViewModelBase
    {
        private ViewModelBase currentPage;

        
        public ICommand GoToSearchStoreCommand { get; set; } 
        public ICommand GoToSearchIceCreamCommand { get; set; }
        public ICommand GoToAddStoreCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public AdminPageVM()
        {
            GoToSearchStoreCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new StoresCollectionForAdminVM()));
            GoToSearchIceCreamCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new IceCreamCollectionForAdminVM()));
            GoToAddStoreCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new AddStoreVM()));
            LogoutCommand = new RelayCommand(logout);

             
        }

        private void logout()
        {
            MessengerInstance.Send<ViewModelBase>(new LandedVM());
        }

        
    }
}
