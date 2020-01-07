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
        private bool isGetNutrienCommandBusy;
        public MyCommand AddIceCreamCommand { get; set; }
        public ICommand DismissCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public MyCommand OpenFileCommand { get; set; }
        public MyCommand GetNutrientCommand { get; set; }

        public bool IsGetNutrienCommandBusy { get { return isGetNutrienCommandBusy; } set { Set(ref isGetNutrienCommandBusy, value); } }
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
            GetNutrientCommand = new MyCommand(ExecuteGetNutrientValueAsync, CanExecuteGetNutrientValue);

        }

        private async void ExecuteGetNutrientValueAsync()
        {

            IceCreamModel.SumCal = null;
            IceCreamModel.SumProtein = null;
            IceCreamModel.SumFat = null;
            IsGetNutrienCommandBusy = true;
            if (string.IsNullOrWhiteSpace(IceCreamModel.Name))
            {
                SnackbarMessageQueue.Enqueue("Please, Enter an ice cream name and then click.");

            }
            else
            {


                try
                {
                    int nutrienId = await NutritionalValue.getProductId(IceCreamModel.Name);
                    if (nutrienId == -1)
                    {
                        SnackbarMessageQueue.Enqueue("Oops... your Ice Cream name not match any nutritional value. please try change the name");

                    }
                    else
                    {
                        Nutrition nutrition = await NutritionalValue.getNutritionalValue(nutrienId);
                        IceCreamModel.SumCal = nutrition.Energy;
                        IceCreamModel.SumProtein = nutrition.Protein;
                        IceCreamModel.SumFat = nutrition.Fat;

                    }
                }
                catch (Exception)
                {

                    SnackbarMessageQueue.Enqueue("Oops... Can Not Get Nutrient Value Now.", "Ok", () => { }, false);

                }
            }

            IsGetNutrienCommandBusy = false;
            CommandManager.InvalidateRequerySuggested();



        }

        private bool CanExecuteGetNutrientValue()
        {
            return !IsGetNutrienCommandBusy;
        }

        public Boolean CanExecuteAddStore()
        {
            return IceCreamModel.IsValidate() && !IsGetNutrienCommandBusy;
        }

        public Boolean CanExecuteDismiss()
        {
            return !IceCreamModel.IsAllFeildsClear();
        }

        public async void ExecuteAddStore()
        {
            IceCream iceCream =IceCreamModel.GetAsStore();




            var res =await iceCreamLogic.AddIceCream(iceCream);
            switch (res)
            {
                case IceCreamLogic.Status.InvalidName:
                    SnackbarMessageQueue.Enqueue(string.Format("Error,Faild to add. Ice Cream With The Name {0} alredy exist !!!.", iceCream.Name));
                    break;
                case IceCreamLogic.Status.DBError:
                    SnackbarMessageQueue.Enqueue("Error occur, Can not add Ice Cream now...");
                    break;
                case IceCreamLogic.Status.Success:
                    SnackbarMessageQueue.Enqueue(string.Format("{0} added successful.", iceCream.Name), "Go to new Ice Cream", () =>
                    {
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

            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                IceCreamModel.Image = File.ReadAllBytes(path);
            }
        }
    }
}
