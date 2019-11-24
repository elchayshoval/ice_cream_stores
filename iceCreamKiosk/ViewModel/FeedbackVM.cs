using BE;
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
    class FeedbackVM:ViewModelBase
    {
        public FeedbackVM FeedbackModel { get; set; }
        public ICommand CancelCommand { get; set; }

        public FeedbackVM(FeedBack feedBack)
        {
            //I have to init FeedbackModel with feedback
            CancelCommand = new MyCommand(executeCancelCommand);
        }

        private void executeCancelCommand()
        {
            //TDOD i have to close the window 
        }
    }
}
