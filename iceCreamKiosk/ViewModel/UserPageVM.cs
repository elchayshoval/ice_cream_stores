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
    class UserPageVM:ViewModelBase
    {
        private ViewModelBase currentPage;

        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public Stack<ViewModelBase> PreviousPages { get; set; } = new Stack<ViewModelBase>();
        public Stack<ViewModelBase> NextPages { get; set; } = new Stack<ViewModelBase>();
        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public UserPageVM()
        {
            CurrentPage = new FilterVM();
            BackCommand = new MyCommand(ExecuteBackCommand, CanExecuteBackCommand);
            NextCommand = new MyCommand(ExecuteNextCommand, CanExecuteNextCommand);

            MessengerInstance.Register<ViewModelBase>(this, GoNewPage);

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
            return PreviousPages.Count > 0;
        }

        private void ExecuteBackCommand()
        {
            if (PreviousPages.Count > 0)
            {
                NextPages.Push(CurrentPage);
                CurrentPage = PreviousPages.Pop();
                ExecutePageRequires(CurrentPage);
            }
        }
    }
}
