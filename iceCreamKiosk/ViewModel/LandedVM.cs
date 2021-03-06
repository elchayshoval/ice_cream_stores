﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class LandedVM : ViewModelBase
    {
        private LoginVM loinDialog;

        public ICommand GoToSearchStoreCommand { get; set; }
        public ICommand GoToSearchIceCreamCommand { get; set; }
        public ICommand GoToAddFeedbackCommand { get; set; }
        public ICommand GoToAddAdminCommand { get; set; }

        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();
        public LoginVM LoginDialog { get { return loinDialog; } private set { Set(ref loinDialog, value); } }

        public LandedVM()
        {
            GoToSearchStoreCommand = new RelayCommand(() => MessengerInstance.Send<ViewModelBase>(new StoresCollectionForUserVM()));
            GoToSearchIceCreamCommand = new MyCommand(ExecuteGoToSearchIceCreamCommand);
            GoToAddFeedbackCommand = new RelayCommand(ExecuteAddFeedbackCommand);//TODO i have to complete
            GoToAddAdminCommand = new RelayCommand(GoToAdminPage);
            MessengerInstance.Register<bool>(this, CloseLoginDialog);
        }

        private void CloseLoginDialog(bool close)
        {
            if (close && LoginDialog != null)
            {
                LoginDialog = null;
            }
        }

        private async void ExecuteAddFeedbackCommand()
        {
            SnackbarMessageQueue.Enqueue("Please, Select a Store first");
            await Task.Delay(1000);
            MessengerInstance.Send<ViewModelBase>(new StoresCollectionForUserVM());

        }

        private void GoToAdminPage()
        {
            LoginDialog = new LoginVM();
        }

        private void ExecuteGoToSearchIceCreamCommand()
        {
            MessengerInstance.Send<ViewModelBase>(new IceCreamsCollectionForUserVM());
        }
    }
}
