﻿using GalaSoft.MvvmLight;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class HomePageVM:ViewModelBase
    {
        private ViewModelBase mainPage;

        public ICommand NavigateToAdmin { get; set; }
        public ICommand NavigateToUser { get; set; }
        public ViewModelBase MainPage { get => mainPage; set => Set(ref mainPage, value); }


        public HomePageVM()
        {
            NavigateToAdmin = new MyCommand(ExecuteToAdminCommand);
            NavigateToUser = new MyCommand(ExecuteToUserCommand);

        }

        public void ExecuteToAdminCommand()
        {
            MainPage = new AdminPageVM();
            
        }
        public void ExecuteToUserCommand()
        {
            MainPage = new UserPageVM();

        }
    }
}
 