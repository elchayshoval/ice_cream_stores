using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using iceCreamKiosk.placesApi;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    public class AddStoreVM : ViewModelBase
    {
        private StoreLogic storeLogic = new StoreLogic();
        

        public ICommand AddStoreCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public IAutocompleteSource PredictionList { get; set; } = new PlacesAutoComplete();
        
        public StoreModel StoreModel { get; set; }

        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public AddStoreVM()//StoreModel store = null)
        {
            //this.Store = store;
            if (this.StoreModel == null)
            {
                this.StoreModel = new StoreModel(null);

            }
            
                AddStoreCommand = new MyCommand(ExecuteAddStore, CanExecuteAddStore);
                DismissCommand = new MyCommand(ExecuteDismiss, CanExecuteDismiss);
                CancelCommand = new MyCommand(ExecuteCancel);
                OpenFileCommand = new MyCommand(BrowseFile);
            new DirectionService().GetDirection();
            



        }

        public Boolean CanExecuteAddStore()
        {
            return StoreModel.IsValidate();
        }

        public Boolean CanExecuteDismiss()
        {
            return !StoreModel.IsAllFeildsClear();
        }

        public void ExecuteAddStore()
        {
            Store s = StoreModel.GetAsStore();
            var res= storeLogic.AddStore(s);
            switch (res)
            {
                case StoreLogic.Status.InvalidNameOrLocation :
                    SnackbarMessageQueue.Enqueue("There already exists a store with these name and location.");
                    break;
                case StoreLogic.Status.DBError:
                    SnackbarMessageQueue.Enqueue("Error occur, Can not add store now...");
                    break;
                case StoreLogic.Status.Success:
                    SnackbarMessageQueue.Enqueue(string.Format( "{0} added successful.",s.Name),"Go to new store",()=> {
                        MessengerInstance.Send<ViewModelBase>(new StoreForAdminVM(s));
                    });
                    break;

            }
        }

        void BrowseFile()
        {
            //Add code here
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                StoreModel.Image = File.ReadAllBytes(path); ;
            }
        }
        public void ExecuteDismiss() { StoreModel.ClearAllFeilds(); }
        public void ExecuteCancel()
        {
            
            MessengerInstance.Send<ViewModelBase>(null);
        }


        public override void RaisePropertyChanged<T>([CallerMemberName] string propertyName = null, T oldValue = default, T newValue = default, bool broadcast = false)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
            if (propertyName == nameof(PredictionList))
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }
            if (propertyName == nameof(StoreModel.Location))
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(newValue as string))
                    {
                        StoreModel.Map = new MapService().GetMap(newValue as string);
                    }
                }
                catch (Exception)
                {

                    SnackbarMessageQueue.Enqueue("Sorry, It Immposible to add A location now. plreas try latter");
                }
            }
        }



    }
}
