using GalaSoft.MvvmLight;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    public class IceCAndStoreVM: ViewModelBase
    {
        public ICommand AddFeedback { get; set; }
        public IceCreamModel IceCream { get; set; }
        public StoreModel Store { get; set; }
        
        public IceCAndStoreVM(IceCreamModel iceCream = null, StoreModel store = null)
        {
            IceCream = iceCream;
            Store = store;
            if(IceCream == null)
            {
                IceCream = new IceCreamModel();
            }
            if(Store == null)
            {
                Store = new StoreModel();
            }
            AddFeedback = new MyCommand(ExecuteAddFeedbackCommand);
        }
        public void ExecuteAddFeedbackCommand()
        {

        }

    }
}
