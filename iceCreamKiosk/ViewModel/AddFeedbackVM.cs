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
    class AddFeedbackVM: ViewModelBase
    {
        public FeedbackModel Feedback { get; set; }
        public ICommand AddFeedbackCommand { get; set; }

        public AddFeedbackVM(FeedbackModel feedback = null)
        {
            Feedback = feedback;
            if(Feedback == null)
            {
                Feedback = new FeedbackModel();
            }

            AddFeedbackCommand = new MyCommand(ExecuteFeedback, CanExecuteFeedback);
        }
        public void ExecuteFeedback()
        {
            // i need to implementation this func
        }
        public bool CanExecuteFeedback()
        {
            return !Feedback.IsAllFeildsClear();
        }

    }
}
