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
    class StoreForAdminVM : ViewModelBase
    {
        private IceCream selectedIceCream;
        //currently not in use
        private ViewModelBase addVM;
        private IceCreamLogic IceCreamLogic = new IceCreamLogic();

        public StoreModel StoreModel { get; set; }
        public IceCream SelectedIceCream { get => selectedIceCream; set => Set(ref selectedIceCream, value); }

        public ICommand AddCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        //currently not in use
        public ViewModelBase AddVM { get => addVM; set => Set(ref addVM, value); }

        public StoreForAdminVM(Store store)
        {
            //init storeModel with store
            AddCommand = new MyCommand(ExecuteAddCommand);
            ShowSelectedCommand = new MyCommand(ExecuteShowSelectedCommand);
            RemoveCommand = new MyCommand(ExecuteRemoveCommand);
            StoreModel = new StoreModel(store);

        }

        private void ExecuteRemoveCommand()
        {
            if (SelectedIceCream != null)
            {
                IceCreamLogic.RemoveIceCream(SelectedIceCream);

                UpdateIceCreamsCollection();
            }
        }

        private void ExecuteShowSelectedCommand()
        {
            if (selectedIceCream != null)
            {
                MessengerInstance.Send<ViewModelBase>(new IceCreamForAdminVM(SelectedIceCream));

            }
        }

        private void ExecuteAddCommand()
        {
            
                MessengerInstance.Send<ViewModelBase>(new AddIceCreamVM(StoreModel.Store));

            
        }

        internal void UpdateIceCreamsCollection()
        {
             StoreModel.IceCreams= new ObservableCollection<IceCream>(IceCreamLogic.GetIceCreams());
        }

        //curently not in use
        private void closeAddVM() { AddVM = null; }//I have to generate event that when invoke will close addvm with this function

    }
}
