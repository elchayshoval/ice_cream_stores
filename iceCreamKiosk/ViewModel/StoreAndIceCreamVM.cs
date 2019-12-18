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
        
        public ICommand ShowFeedbacks { get; set; }
        public StoreModel StoreModel { get; set; }
        public IceCreamLogic IceCreamLogic { get; set; } = new IceCreamLogic();
        public StoreLogic StoreLogic { get; set; } = new StoreLogic();
        public IceCreamModel IceCreamModel { get; set; }

        private ObservableCollection<FeedBack> feedBacks;
        public ObservableCollection<FeedBack> FeedBacks { get => feedBacks; set => Set(ref feedBacks, value); }

        public StoreAndIceCreamVM(IceCream iceCream)
        {
            Store store = StoreLogic.GetStoreByID(iceCream.StoreId); // null
           // Store store = IceCreamLogic.GetStoreById(iceCream.StoreId);
            StoreModel = new StoreModel(store);
            IceCreamModel = new IceCreamModel(iceCream);
            ShowFeedbacks = new RelayCommand<IceCream>(ShowFeedbacksCommand);

            

        }
       

        public void ShowFeedbacksCommand(IceCream iceCream)
        {
            MessengerInstance.Send<ViewModelBase>(new FeedbackForUserVM(iceCream));

        }
    }
}
