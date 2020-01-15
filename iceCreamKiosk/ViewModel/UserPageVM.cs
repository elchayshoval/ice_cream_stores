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
    class UserPageVM:ViewModelBase
    {
        private ViewModelBase currentPage;


        public ICommand GoToSearchStoreCommand { get; set; }
        public ICommand GoToSearchIceCreamCommand { get; set; }
        public ICommand GoToAddFeedbackCommand { get; set; }
        public ICommand GoHomePageCommand { get; set; }

        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public UserPageVM()
        {
            GoToSearchStoreCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new StoresCollectionForUserVM()));
            GoToSearchIceCreamCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new IceCreamsCollectionForUserVM()));
            GoToAddFeedbackCommand = new RelayCommand(ExecuteAddFeedbackCommand);
            GoHomePageCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new LandedVM()));
        }

        private void ExecuteAddFeedbackCommand()
        {
            SnackbarMessageQueue.Enqueue("Please, Select a Store first");
            MessengerInstance.Send<ViewModelBase>(new StoresCollectionForUserVM());
        }
    }
}
