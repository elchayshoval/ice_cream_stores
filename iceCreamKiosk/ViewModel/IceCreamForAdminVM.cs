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
    class IceCreamForAdminVM:ViewModelBase
    {
        
        public IceCreamModel IceCreamModel { get; set; }
        

        public ICommand ShowSelectedCommand { get; set; }
        public ICommand NavigateToStoreCommand { get; set; }
        public ICommand UpdateCommand { get; set; } 
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();


        public IceCreamForAdminVM(IceCream iceCream)
        {
            
            IceCreamModel = new IceCreamModel(iceCream);
            ShowSelectedCommand = new RelayCommand<FeedBack>(ExecuteShowSelectedCommand);
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            NavigateToStoreCommand = new RelayCommand(ExecuteNavigateToStoreCommand);


        }

        private void ExecuteNavigateToStoreCommand()
        {
            MessengerInstance.Send<ViewModelBase>(new StoreForAdminVM(
                new StoreLogic().GetStoreByID(IceCreamModel.IceCream.StoreId)));
        }

        private void ExecuteShowSelectedCommand(FeedBack feedBack)
        {
            if (feedBack != null)
            {
                MessengerInstance.Send<ViewModelBase>(new FeedbackVM(feedBack));
            }
            
        }



        private bool CanExecuteUpdateCommand()
        {
            return true;
        }

        private void ExecuteUpdateCommand()
        {
            IceCream iceCream = IceCreamModel.GetAsStore();
            if (iceCream != null)
            {
                IceCreamLogic iceCreamLogic = new IceCreamLogic();
                var res = iceCreamLogic.UpdateIceCream(iceCream);
                switch (res)
                {

                    case IceCreamLogic.Status.DBError:
                        SnackbarMessageQueue.Enqueue("Error occur, Can not Update Ice Cream now...");
                        break;
                    case IceCreamLogic.Status.Success:
                        SnackbarMessageQueue.Enqueue(string.Format("{0} Updated successful.", iceCream.Name));

                        break;

                }

            }


        }
    }
}
