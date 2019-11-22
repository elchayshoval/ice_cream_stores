using BE;
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
    class StoresCollectionForAdminVM: ViewModelBase
    {
        private Store selectedStore;
        private ViewModelBase addVM;

        public ICommand AddCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public Store SelectedStore { get => selectedStore; set => Set(ref selectedStore, value); }
        public ObservableCollection<Store> Stores { get; set; }

        public ViewModelBase AddVM { get => addVM; set => Set(ref addVM, value); }

        public StoresCollectionForAdminVM()
        {
            //Update the collection
            AddCommand = new MyCommand(executeAddCommand);
            ShowSelectedCommand = new MyCommand(executeShowSelectedCommand);
            RemoveCommand = new MyCommand(executeRemoveCommand);

        }

        private void executeAddCommand() { AddVM = new AddStoreVM(); }
        private void executeShowSelectedCommand() 
        { 
            // i have to invoke event that change page with storeForAdminVM
        }
        private void executeRemoveCommand() 
        {
            if (SelectedStore!=null)
            {
                //call for ibl remove store
            }
        }

        private void closeAddVM() { AddVM = null; }//I have to generate event that when invoke will close addvm with this function

        

    }
}
