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
        public FeedbackModel FeedbackModel { get; set; }
        public IceCreamModel IceCreamModel { get; set; }
        public IceCreamLogic IceCreamLogic { get; set; } = new IceCreamLogic();

        public ICommand PickImage { get; set; }
        public ICommand AddFeadback { get; set; }

        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();


        public FeedbackForUserVM(IceCream iceCream)
        {
            FeedbackModel = new FeedbackModel();
            AddFeadback = new MyCommand(ExecuteAddFeadback);
            PickImage = new MyCommand(ExecutePickImage);
            IceCreamModel = new IceCreamModel(iceCream);

        }

        async void ExecutePickImage()
        {
            try
            {


                OpenFileDialog d = new OpenFileDialog();

                if (d.ShowDialog() == true)
                {
                    var path = d.FileName;
                    string imageUrl = await GoogleDriveService.UploadImage(path);
                    FeedbackModel.Image = imageUrl;
                }
            }
            catch (Exception)
            {

                 SnackbarMessageQueue.Enqueue("Sorry, It is impossible to add image now. pleas try latter");

            }
        }

        public void ExecuteAddFeadback()
        {
            FeedBack feedBack = FeedbackModel.getAsFeedBack();
            feedBack.IceCreamID = IceCreamModel.IceCream.IceCreamId;
            feedBackLogic.addFeedBack(feedBack);

        }

        private void executeCancelCommand()
        {
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());
            //MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream));
        }

    }
}
