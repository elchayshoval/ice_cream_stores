using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class AddIceCreamVM : ViewModelBase
    {
        public ICommand AddIceCreamCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public MyCommand OpenFileCommand { get;  set; }
        public IceCreamModel IceCreamModel { get; set; }

        private IceCreamLogic iceCreamLogic = new IceCreamLogic();
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();
        public AddIceCreamVM(Store store)
        {

            IceCream iceCream = new IceCream();
            iceCream.StoreId = store.StoreId;
            this.IceCreamModel = new IceCreamModel(iceCream);

            AddIceCreamCommand = new MyCommand(ExecuteAddStore, CanExecuteAddStore);
            DismissCommand = new MyCommand(ExecuteDismiss, CanExecuteDismiss);
            CancelCommand = new RelayCommand(ExecuteCancel);
            OpenFileCommand = new MyCommand(BrowseFile);

        }

        public Boolean CanExecuteAddStore()
        {
            return IceCreamModel.IsValidate();
        }

        public Boolean CanExecuteDismiss()
        {
              return !IceCreamModel.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            IceCream iceCream = IceCreamModel.GetAsStore();
            
            

            
            var res = iceCreamLogic.AddIceCream(iceCream);
            switch (res)
            {
                case IceCreamLogic.Status.InvalidName:
                    SnackbarMessageQueue.Enqueue(string.Format("Error,Faild to add. Ice Cream With The Name {0} alredy exist !!!.", iceCream.Name));
                    break;
                case IceCreamLogic.Status.DBError:
                    SnackbarMessageQueue.Enqueue("Error occur, Can not add Ice Cream now...");
                    break;
                case IceCreamLogic.Status.Success:
                    SnackbarMessageQueue.Enqueue(string.Format("{0} added successful.", iceCream.Name), "Go to new Ice Cream", () => {
                        MessengerInstance.Send<ViewModelBase>(new IceCreamForAdminVM(iceCream));
                    });
                    break;

            }
        }
        public void ExecuteDismiss()
        {
            IceCreamModel.ClearAllFeilds();
        }
        public void ExecuteCancel()
        {
            MessengerInstance.Send<ViewModelBase>(null);
        }

        void BrowseFile()
        {
            //Add code here
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                IceCreamModel.Image = File.ReadAllBytes(path);
            }
        }
    }
}
