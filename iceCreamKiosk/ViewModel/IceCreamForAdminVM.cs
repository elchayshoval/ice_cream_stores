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
        //currently not in use
        private ViewModelBase showFeedbackVM;

        public IceCreamModel IceCreamModel { get; set; }
        public FeedBack SelectedFeedback { get => selectedFeedback; set => Set(ref selectedFeedback, value); }


        public ICommand ShowSelectedCommand { get; set; }
        
        //currently not in use
        public ViewModelBase ShowFeedbackVM { get => showFeedbackVM; set => Set(ref showFeedbackVM, value); }

        public IceCreamForAdminVM(IceCream iceCream)
        {
            //init IceCreamModel with iceCream
            IceCreamModel = new IceCreamModel(iceCream);
            ShowSelectedCommand = new MyCommand(ExecuteShowSelectedCommand);
            

        }

        

        private void ExecuteShowSelectedCommand()
        {
            if (selectedFeedback != null)
            {
                MessengerInstance.Send<ViewModelBase>(new FeedbackVM(SelectedFeedback));
            }
            
        }

        //currently not in use
        private void closeAddVM() { ShowFeedbackVM = null; }//I have to generate event that when invoke will close addvm with this function

    }
}
