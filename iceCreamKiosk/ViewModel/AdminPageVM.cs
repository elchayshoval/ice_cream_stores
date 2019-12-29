using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class AdminPageVM : ViewModelBase
    {
        private ViewModelBase currentPage;

        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand GoToSearchStoreCommand { get; set; } 
        public ICommand GoToSearchIceCreamCommand { get; set; }
        public ICommand GoToAddStoreCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public Stack<ViewModelBase> PreviousPages { get; set; } = new Stack<ViewModelBase>();
        public Stack<ViewModelBase> NextPages { get; set; } = new Stack<ViewModelBase>();
        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public AdminPageVM()
        {
            CurrentPage = new StoresCollectionForAdminVM();//temp
            BackCommand = new MyCommand(ExecuteBackCommand, CanExecuteBackCommand);
            NextCommand = new MyCommand(ExecuteNextCommand, CanExecuteNextCommand);
            GoToSearchStoreCommand = new RelayCommand(() => GoNewPage(new StoresCollectionForAdminVM()));
            GoToSearchIceCreamCommand = new RelayCommand(() => GoNewPage(new IceCreamCollectionForAdminVM()));
            GoToAddStoreCommand = new RelayCommand(() => GoNewPage(new AddStoreVM()));
            LogoutCommand = new RelayCommand(logout);

            MessengerInstance.Register<ViewModelBase>(this, GoNewPage);

        }

        private void logout()
        {
            MessengerInstance.Send(1);
        }

        private void GoNewPage(ViewModelBase VM)
        {
            NextPages.Clear();
            if (VM != null)
            {
                PreviousPages.Push(CurrentPage);
                CurrentPage = VM;

            }
            else
            {
                NextPages.Push(CurrentPage);
                if (PreviousPages.Count > 0)
                {
                    CurrentPage = PreviousPages.Pop();
                }
            }
            ExecutePageRequires(CurrentPage);
        }

        private void ExecutePageRequires(ViewModelBase currentPage)
        {
            if (currentPage is StoresCollectionForAdminVM)
            {
                (currentPage as StoresCollectionForAdminVM).UpdateStoresCollection();
            }
            if (currentPage is IceCreamCollectionForAdminVM)
            {
                (currentPage as IceCreamCollectionForAdminVM).UpdateIceCreamCollection();
            }
            if (currentPage is StoreForAdminVM)
            {
                (currentPage as StoreForAdminVM).UpdateIceCreamsCollection();
            }
        }

        private bool CanExecuteNextCommand()
        {
            return NextPages.Count > 0;
        }

        private void ExecuteNextCommand()
        {
            if (NextPages.Count > 0)
            {
                PreviousPages.Push(CurrentPage);
                CurrentPage = NextPages.Pop();
                ExecutePageRequires(CurrentPage);
            }
            
        }

        private bool CanExecuteBackCommand()
        {
            return true;
        }

        private void ExecuteBackCommand()
        {
            if (PreviousPages.Count > 0)
            {
                NextPages.Push(CurrentPage);
                CurrentPage = PreviousPages.Pop();
                ExecutePageRequires(CurrentPage);
            }
            else
            {
                logout();
            }
        }
    }
}
