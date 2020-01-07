using BE;
using BL;
using DL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    public class FeedbackForUserVM : ViewModelBase
    {
        public FeedBackLogic feedBackLogic = new FeedBackLogic();
        private bool thereWasADialog;
        private bool isUploadImageExecuted;

        public bool IsUploadImageExecuted { get { return isUploadImageExecuted; } set { Set(ref isUploadImageExecuted, value); } }
        public FeedbackModel FeedbackModel { get; set; }
        public ICommand CancelCommand { get; set; }
        public bool ThereWasADialog { get { return thereWasADialog; } set { Set(ref thereWasADialog, value); } }

        public ICommand OpenPictureDialog { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand PickImage { get; set; }
        public ICommand AddFeadback { get; set; }
        public ICommand GoToStore { get; set; }
        public ICommand GoToIceCream { get; set; }


        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public Store store { get; set; }
        public IceCream iceCream { get; set; }

        public FeedbackForUserVM(IceCream iceCream, Store store = null)
        {
            this.iceCream = iceCream;
            this.store = store;
            if (store == null)
            {
                this.store = new StoreLogic().GetStoreByID(iceCream.StoreId);
            }
            FeedbackModel = new FeedbackModel();
            AddFeadback = new MyCommand(ExecuteAddFeadback, () => !IsUploadImageExecuted);
            TakePictureCommand = new MyCommand(ExecuteTakePictureCommand, () => !IsUploadImageExecuted);
            PickImage = new MyCommand(ExecutePickImage, () => !IsUploadImageExecuted);
            OpenPictureDialog = new MyCommand(() => { ThereWasADialog = true; }, () => { return !IsUploadImageExecuted; });
            CancelCommand = new RelayCommand(() => { MessengerInstance.Send<ViewModelBase>(null); });
            GoToStore = new RelayCommand(() => { MessengerInstance.Send<ViewModelBase>(new StoreForUserVM(this.store)); });
            GoToIceCream = new RelayCommand(() => { MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(this.iceCream, this.store)); });
        }

        private async void ExecuteTakePictureCommand()
        {
            ThereWasADialog = false;
            string path;
            IsUploadImageExecuted = true; try
            {

                path = TakePicture.TakePictureProcess();
                await ProcessImage(path);
            }
            catch (Exception)
            {

                SnackbarMessageQueue.Enqueue("Oops... some error occure, please try again");

            }
            IsUploadImageExecuted = false;
            CommandManager.InvalidateRequerySuggested();
        }

        async void ExecutePickImage()
        {
            ThereWasADialog = false;
            IsUploadImageExecuted = true;

            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                await ProcessImage(path);
            }

            IsUploadImageExecuted = false;
            CommandManager.InvalidateRequerySuggested();
        }

        public void ExecuteAddFeadback()
        {



            FeedBack feedBack = FeedbackModel.getAsFeedBack();
            feedBack.IceCreamID = iceCream.IceCreamId;
            var status = feedBackLogic.addFeedBack(feedBack);
            switch (status)
            {
                case FeedBackLogic.Status.Success:
                    SnackbarMessageQueue.Enqueue("Succesful added feedback, Thank you very match.");
                    break;
                case FeedBackLogic.Status.ExistError:
                    SnackbarMessageQueue.Enqueue("Oops, You just add feedback now...");
                    break;
                default:
                    SnackbarMessageQueue.Enqueue("Error, Can not add feedback now. Please try latter.");
                    break;
            }

        }

        private async Task ProcessImage(String path)
        {
            try
            {
                string imageUrl = await GoogleDriveService.UploadImage(path);
                FeedbackModel.Image = imageUrl;
                SnackbarMessageQueue.Enqueue("Please Wait, We now verify your image");
                bool isConfident = await ImageAnlyzer.CheckIceCreamConfidentByPath(path);
                if (isConfident)
                {
                    SnackbarMessageQueue.Enqueue("Your image was verifyed successfully.");

                }
                else
                {
                    SnackbarMessageQueue.Enqueue("Sorry, Your image was not verifyed! please replace the image");
                    FeedbackModel.Image = "";
                }

            }
            catch
            {
                SnackbarMessageQueue.Enqueue("Sorry, it is impossible to add image now");
                FeedbackModel.Image = "";
            }
        }



    }
}
