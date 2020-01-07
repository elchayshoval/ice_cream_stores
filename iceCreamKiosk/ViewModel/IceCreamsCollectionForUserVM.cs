using BE;
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iceCreamKiosk.model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iceCreamKiosk.ViewModel
{
    class IceCreamsCollectionForUserVM:ViewModelBase
    {
        private IceCreamLogic iceCreamLogic = new IceCreamLogic();
        private ObservableCollection<IceCream> iceCreams;
        private string search;
        private IEnumerable<IceCream> allIceCreams;

        public FilterModel FilterModel { get; set; } = new FilterModel();

        public string Search { get => search; set { Set(ref search, value); UpdateIceCreamCollection(value); } }
        public ObservableCollection<IceCream> IceCreams { get => iceCreams; set => Set(ref iceCreams, value); }

        public ICommand ShowSelectedCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public SnackbarMessageQueue SnackbarMessageQueue { get; set; } = new SnackbarMessageQueue();


        public IceCreamsCollectionForUserVM()
        {
            UpdateIceCreamCollection();
            ShowSelectedCommand = new RelayCommand<IceCream>(ExecuteShowSelectedCommand);
            SearchCommand = new RelayCommand<string>(UpdateIceCreamCollection);
        }


        public async void UpdateIceCreamCollection(string search = null)
        {
            if (search == null)
            {
                allIceCreams =await iceCreamLogic.GetIceCreams();
                IceCreams = new ObservableCollection<IceCream>(allIceCreams);
            }
            else
            {
                FilterModel.IceCreanDescription = search;
                var iceCreamList =await iceCreamLogic.getFilteredIceCreams(allIceCreams, FilterModel.getAsFilter());
                IceCreams = new ObservableCollection<IceCream>(iceCreamList);
            }

        }

        private void ExecuteShowSelectedCommand(Object iceCream)
        {

            if (iceCream is IceCream)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream as IceCream));
            }
        }
        
    }
}
