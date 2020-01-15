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
    class HomePageVM:ViewModelBase
    {
        private ViewModelBase currentPage;
        private bool isNavigationEnabled;

        public bool IsNavigationEnabled { get { return isNavigationEnabled; } set { Set(ref isNavigationEnabled, value); } }
        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public Stack<ViewModelBase> PreviousPages { get; set; } = new Stack<ViewModelBase>();
        public Stack<ViewModelBase> NextPages { get; set; } = new Stack<ViewModelBase>();



        public ViewModelBase LandedPage { get; set; } = new LandedVM();
        public ViewModelBase AdminPage { get; private set; } = new AdminPageVM();
        public ViewModelBase UserPage { get; private set; } = new UserPageVM();

        public HomePageVM()
        {

            BackCommand = new MyCommand(ExecuteBackCommand, CanExecuteBackCommand);
            NextCommand = new MyCommand(ExecuteNextCommand, CanExecuteNextCommand);
            CurrentPage = LandedPage;
            IsNavigationEnabled = false;
            MessengerInstance.Register<ViewModelBase>(this, GoNewPage);

        }

        private void GoToLandedPage()
        {
            CurrentPage = LandedPage;
            IsNavigationEnabled = false;
            PreviousPages.Clear();
            NextPages.Clear();
        }
        




        private void GoNewPage(ViewModelBase VM)
        {
            
            NextPages.Clear();
            if (VM != null)
            {
                PreviousPages.Push(GetPresentedPage());
                AssignPage(VM);
                
                
            }
            else
            {
                NextPages.Push(GetPresentedPage());
                if (PreviousPages.Count > 0)
                {
                    AssignPage( PreviousPages.Pop());
                }
            }
            ExecutePageRequires(GetPresentedPage());
        }

        private void AssignPage(ViewModelBase vm)
        {
            if (vm is LandedVM)
            {
                GoToLandedPage();
                return;
            }
            if (CurrentPage == LandedPage)
            {
                IsNavigationEnabled = true;
                DeterminePageType(vm);
            }
            if (CurrentPage is AdminPageVM)
            {
                (CurrentPage as AdminPageVM).CurrentPage = vm;
            }
            if (CurrentPage is UserPageVM)
            {
                (CurrentPage as UserPageVM).CurrentPage = vm;
            }
        }

        private void DeterminePageType(ViewModelBase vm)
        {
            if(vm is StoresCollectionForAdminVM)
            {
                CurrentPage = AdminPage;
            }
            else
            {
                CurrentPage = UserPage;
            }
        }

        private async void ExecutePageRequires(ViewModelBase currentPage)
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
            if (currentPage is StoresCollectionForUserVM)
            {
                 (currentPage as StoresCollectionForUserVM).UpdateStoresCollection();
            }
            if (currentPage is IceCreamsCollectionForUserVM)
            {
                await (currentPage as IceCreamsCollectionForUserVM).UpdateIceCreamCollection();
            }
            if (currentPage is StoreForUserVM)
            {
                (currentPage as StoreForUserVM).UpdateIceCreamsCollection();
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
                PreviousPages.Push(GetPresentedPage());
                AssignPage( NextPages.Pop());
                ExecutePageRequires(GetPresentedPage());
            }

        }

        private ViewModelBase GetPresentedPage()
        {
            ViewModelBase presentedPage=null;
            if (CurrentPage == LandedPage)
            {
                presentedPage = CurrentPage;
            }
            
            if (CurrentPage is AdminPageVM)
            {
                presentedPage= (CurrentPage as AdminPageVM).CurrentPage ;
            }
            if (CurrentPage is UserPageVM)
            {
                presentedPage= (CurrentPage as UserPageVM).CurrentPage;
            }
            return presentedPage;
        }

        private bool CanExecuteBackCommand()
        {
            return IsNavigationEnabled;
        }

        private void ExecuteBackCommand()
        {
            if (PreviousPages.Count > 0)
            {
                NextPages.Push(GetPresentedPage());
                AssignPage(PreviousPages.Pop());
                ExecutePageRequires(GetPresentedPage());
            }
            else
            {
                GoToLandedPage();
            }
        }
    }
}
 