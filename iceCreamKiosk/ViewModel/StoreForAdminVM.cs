using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class StoreForAdminVM : ViewModelBase
    {
        private IceCreamLogic IceCreamLogic = new IceCreamLogic();

        public StoreModel StoreModel { get; set; }
        public ICommand OpenFileCommand { get;  set; }
        
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ShowSelectedCommand { get; set; }
        public ICommand OpenRemoveDialog { get; set; }
        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public StoreForAdminVM(Store store)
        {
            AddCommand = new MyCommand(ExecuteAddCommand);
            ShowSelectedCommand = new RelayCommand<IceCream>(ExecuteShowSelectedCommand);
            StoreModel = new StoreModel(store);
            OpenFileCommand = new MyCommand(ExcuteBrowseFile);
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            OpenRemoveDialog = new RelayCommand<IceCream>((ice) =>
            {
                if (ice is IceCream)
                {
                    IceCream iceCream = (ice as IceCream);
                    SnackbarMessageQueue.Enqueue(string.Format("Are you sure you want to remove: {0} ?", iceCream.Name), "Yes, Delete", () =>
                    {
                        ExecuteRemoveCommand(iceCream);
                    });

                }
            });
        }

        private bool CanExecuteUpdateCommand()
        {
            return true;
        }

        private void ExecuteUpdateCommand()
        {
            Store store = StoreModel.GetAsStore();
            if (store != null)
            {
                StoreLogic storeLogic = new StoreLogic();
                var res = storeLogic.UpdateStore(store);
                switch (res)
                {
                    
                    case StoreLogic.Status.DBError:
                        SnackbarMessageQueue.Enqueue("Error occur, Can not add store now...");
                        break;
                    case StoreLogic.Status.Success:
                        SnackbarMessageQueue.Enqueue(string.Format("{0} Updated successful.", store.Name));
                        
                        break;

                }

            }

            
        }

        void ExcuteBrowseFile()
        {
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                StoreModel.Image = FileManager.AddStoreImage(path, StoreModel.Name);
            }
        }
        private void ExecuteRemoveCommand(IceCream iceCream)
        {
            if (iceCream != null)
            {
                IceCreamLogic.RemoveIceCream(iceCream);

                UpdateIceCreamsCollection();
                SnackbarMessageQueue.Enqueue(string.Format("Successful remove {0} .", iceCream.Name), "UNDO",async () =>
                {
                //Notjice!! dont add back the icecreams and feedbacks!!!!
                var status =await IceCreamLogic.AddIceCream(iceCream);
                if (status == IceCreamLogic.Status.Success)
                {

                    UpdateIceCreamsCollection();
                    SnackbarMessageQueue.Enqueue(string.Format("Successful Undo removing {0} .", iceCream.Name));
                    }
                }
                );
            }
        }

        private void ExecuteShowSelectedCommand(IceCream iceCream)
        {
            if (iceCream != null)
            {
                MessengerInstance.Send<ViewModelBase>(new IceCreamForAdminVM(iceCream));


            }
        }

        private void ExecuteAddCommand()
        {
            
                MessengerInstance.Send<ViewModelBase>(new AddIceCreamVM(StoreModel.Store));

            
        }

        internal void UpdateIceCreamsCollection()
        {
            StoreModel.UpdateIceCreamCollection();
        }

        
    }
}
