using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class StoresCollectionForUserVM:ViewModelBase
    {
        private StoreLogic storeLogic = new StoreLogic();
        private ObservableCollection<Store> stores;
        private string search;

        public string Search { get => search; set { Set(ref search, value); UpdateStoresCollection(value); } }

        private IEnumerable<Store> allStores;

        public ObservableCollection<Store> Stores { get => stores; set => Set(ref stores, value); }

        public ICommand ShowSelectedCommand { get; set; }
        
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();


        public StoresCollectionForUserVM()
        {
            UpdateStoresCollection();
            ShowSelectedCommand = new RelayCommand<Store>(ExecuteShowSelectedCommand);
        }


        public async void UpdateStoresCollection(string search = null)
        {
            if (search == null || allStores == null)
            {
                allStores = await storeLogic.GetStores();
                Stores = new ObservableCollection<Store>(allStores);
            }
            else
            {
                Stores = new ObservableCollection<Store>(allStores.Where(s => s.Name.Contains(search)));
            }
        }

        private void ExecuteShowSelectedCommand(Object store)
        {

            if (store is Store)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreForUserVM(store as Store));
            }
        }
        

    }
}
