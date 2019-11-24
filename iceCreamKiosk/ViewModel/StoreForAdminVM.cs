using BE;
using GalaSoft.MvvmLight;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class StoreForAdminVM: ViewModelBase
    {
        private IceCream selectedIceCream;
        private ViewModelBase addVM;

        public StoreModel Store { get; set; }
        public IceCream SelectedIceCream { get => selectedIceCream; set => Set(ref selectedIceCream, value); }

        public ICommand AddCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ViewModelBase AddVM { get => addVM; set => Set(ref addVM, value); }

        public StoreForAdminVM(Store store)
        {
            //init storeModel with store
            AddCommand = new MyCommand(executeAddCommand);
            ShowSelectedCommand = new MyCommand(executeShowSelectedCommand);
            RemoveCommand = new MyCommand(executeRemoveCommand);

        }

        private void executeRemoveCommand()
        {
            if (SelectedIceCream!=null)
            {
                //remove isecream with IBL
            }
        }

        private void executeShowSelectedCommand()
        {
            // i have to invoke event that change page with IceCreamForAdminVM
        }

        private void executeAddCommand()
        {
            //I have to assign AddVM with a new AddIceCreamVM
        }

        private void closeAddVM() { AddVM = null; }//I have to generate event that when invoke will close addvm with this function

    }
}
