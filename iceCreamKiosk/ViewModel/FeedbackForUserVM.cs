using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
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

        public FeedbackForUserVM(IceCream iceCream)
        {
             FeedbackModel = new FeedbackModel();
            AddFeadback = new MyCommand(ExecuteAddFeadback);
            PickImage = new MyCommand(ExecutePickImage);
            IceCreamModel = new IceCreamModel(iceCream);

        }

        void ExecutePickImage()
        {
            OpenFileDialog d = new OpenFileDialog();

            if (d.ShowDialog() == true)
            {
                var path = d.FileName;
                IceCreamModel.Image = path;
            }
        }

        public void ExecuteAddFeadback()
        {
            FeedbackModel feedb = FeedbackModel;
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());

        }

        private void executeCancelCommand()
        {
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());
            //MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream));
        }

    }
}
