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
    class IceCreamForAdminVM:ViewModelBase
    {
        private FeedBack selectedFeedback;
        private ViewModelBase showFeedbackVM;

        public IceCreamModel IceCream { get; set; }
        public FeedBack SelectedFeedback { get => selectedFeedback; set => Set(ref selectedFeedback, value); }


        public ICommand ShowSelectedCommand { get; set; }
        public ViewModelBase ShowFeedbackVM { get => showFeedbackVM; set => Set(ref showFeedbackVM, value); }

        public IceCreamForAdminVM(IceCream iceCream)
        {
            //init IceCreamModel with iceCream
            
            ShowSelectedCommand = new MyCommand(executeShowSelectedCommand);
            

        }

        

        private void executeShowSelectedCommand()
        {
            if (selectedFeedback != null)
            {
                //I have to assign ShowFeedbackVM with a new ShowFeedbackVM and selected feedback
            }
            // i have to invoke event that change page with IceCreamForAdminVM
        }

        
        private void closeAddVM() { ShowFeedbackVM = null; }//I have to generate event that when invoke will close addvm with this function

    }
}
