using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    public  class FeedbackVM : ViewModelBase
    {
        public FeedBackLogic feedBackLogic = new FeedBackLogic();
        public FeedbackModel FeedbackModel { get; set; }
        public IceCreamModel IceCreamModel { get; set; }
        public IceCreamLogic IceCreamLogic { get; set; } = new IceCreamLogic();


        public ICommand AddFeadback { get; set; }

        public FeedbackVM(FeedBack feedBack)
        {
            // FeedbackModel = new FeedbackModel(feedBack);
            AddFeadback = new MyCommand(ExecuteAddFeadback);
            
          
        }
       
        public void ExecuteAddFeadback()
        {
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());

        }

        private void executeCancelCommand()
        {
            feedBackLogic.addFeedBack(FeedbackModel.getAsFeedBack());
            //MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream));
        }
    }
}
