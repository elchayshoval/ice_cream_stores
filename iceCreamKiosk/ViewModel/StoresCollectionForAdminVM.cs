using BE;
using BL;
using GalaSoft.MvvmLight;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class StoresCollectionForAdminVM : ViewModelBase
    {
        private Store selectedStore;
        private ViewModelBase addVM;//currrently not in use
        private StoreLogic storeLogic = new StoreLogic();
        private ObservableCollection<Store> stores;

        public ICommand AddCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public Store SelectedStore { get => selectedStore; set => Set(ref selectedStore, value); }
        public ObservableCollection<Store> Stores { get => stores; set => Set(ref stores, value); }

        //currently not in use
        public ViewModelBase AddVM { get => addVM; set => Set(ref addVM, value); }

        public StoresCollectionForAdminVM()
        {
            Stores = new ObservableCollection<Store>(storeLogic.GetStores());
            AddCommand = new MyCommand(ExecuteAddCommand);
            ShowSelectedCommand = new MyCommand(executeShowSelectedCommand);
            RemoveCommand = new MyCommand(executeRemoveCommand);

        }

        private void ExecuteAddCommand()
        {
            MessengerInstance.Send<ViewModelBase>(new AddStoreVM());
        }

        internal void UpdateStoresCollection()
        {
            Stores = new ObservableCollection<Store>(storeLogic.GetStores());
        }

        private void executeShowSelectedCommand()
        {
            if (SelectedStore != null)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreForAdminVM(SelectedStore));
            }
        }
        private void executeRemoveCommand()
        {
            if (SelectedStore != null)
            {
                storeLogic.RemoveStore(SelectedStore);
                UpdateStoresCollection();
                //I have notify that action successed
            }
        }

        //currently not in use
        private void closeAddVM() { AddVM = null; }//I have to generate event that when invoke will close addvm with this function



    }
}
