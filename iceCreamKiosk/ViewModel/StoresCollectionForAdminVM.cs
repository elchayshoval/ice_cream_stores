using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
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
    class StoresCollectionForAdminVM : ViewModelBase
    {
        private StoreLogic storeLogic = new StoreLogic();
        private ObservableCollection<Store> stores;
        private string search;

        public string Search { get=>search; set { Set(ref search, value); UpdateStoresCollection(value); } }
        public ObservableCollection<Store> Stores { get => stores; set => Set(ref stores, value); }
        
        public ICommand AddCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand OpenRemoveDialog { get; set; }

        
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        
        public StoresCollectionForAdminVM()
        {
            Stores = new ObservableCollection<Store>(storeLogic.GetStores());
            AddCommand = new MyCommand(ExecuteAddCommand);
            ShowSelectedCommand = new RelayCommand<Store>(ExecuteShowSelectedCommand);
            

            OpenRemoveDialog = new RelayCommand<Store>((s) =>
            {
                if (s is Store)
                {
                    Store store = (s as Store);
                    SnackbarMessageQueue.Enqueue(string.Format("Are you sure you want to remove: {0} ?", store.Name), "Yes, Delete", () =>
                    {
                        ExecuteRemoveCommand(store);
                    });

                }
            });

        }

        private void ExecuteAddCommand()
        {
            MessengerInstance.Send<ViewModelBase>(new AddStoreVM());
        }

        public void UpdateStoresCollection(string search="")
        {
            Stores = new ObservableCollection<Store>(storeLogic.GetStores(search));
        }

        private void ExecuteShowSelectedCommand(Object store)
        {

            if (store is Store)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreForAdminVM(store as Store));
            }
        }
        private void ExecuteRemoveCommand(Store store)
        {
            if (store != null)
            {

                storeLogic.RemoveStore(store);
                UpdateStoresCollection();


                SnackbarMessageQueue.Enqueue(string.Format("Successful remove {0} .", store.Name), "UNDO", () =>
                {
                    //Notjice!! dont add back the icecreams and feedbacks!!!!
                    var status = storeLogic.AddStore(store);
                    if (status == StoreLogic.Status.Success)
                    {

                        SnackbarMessageQueue.Enqueue(string.Format("Successful Undo removing {0} .", store.Name));
                        UpdateStoresCollection();
                    }
                }
                );


            }

        }

        


    }
}
