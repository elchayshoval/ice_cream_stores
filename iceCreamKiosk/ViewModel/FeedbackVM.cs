using BE;
using BL;
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
    class FeedbackVM : ViewModelBase
    {
        public FeedBackLogic feedBackLogic = new FeedBackLogic();
        public FeedbackModel FeedbackModel { get; set; }
        public ICommand AddFeedBackCommand { get; set; }

        public FeedbackVM(FeedBack feedBack)
        {
            FeedbackModel = new FeedbackModel(feedBack);
            AddFeedBackCommand = new MyCommand(executeCancelCommand);
        }

        private void executeCancelCommand()
        {
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());
            //MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream));
        }
    }
}
