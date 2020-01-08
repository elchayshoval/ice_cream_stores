using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class StoreAndIceCreamVM : ViewModelBase
    {
        public StoreLogic StoreLogic { get; set; } = new StoreLogic();

        public Store Store { get; set; }
        public IceCream IceCream { get; private set; }
        public ICommand AddFeesbackCommand { get; set; }
        public ICommand GoToStoreCommand { get; set; }

        private ObservableCollection<FeedBack> feedBacks;
        public ObservableCollection<FeedBack> FeedBacks { get => feedBacks; set => Set(ref feedBacks, value); }

        public StoreAndIceCreamVM(IceCream iceCream, Store store = null)
        {
             Store = store;
            if (Store == null)
            {
                Store = StoreLogic.GetStoreByID(iceCream.StoreId);
            }
            IceCream = iceCream;
            AddFeesbackCommand = new RelayCommand(ShowFeedbacksCommand);
            GoToStoreCommand = new RelayCommand(() => { NavigateTo(new StoreForUserVM(Store)); });


        }


        public void ShowFeedbacksCommand()
        {
            NavigateTo(new FeedbackForUserVM(IceCream,Store));

        }

        private void NavigateTo(ViewModelBase vm)
        {
            MessengerInstance.Send<ViewModelBase>(vm);
        }
    }
}
