using GalaSoft.MvvmLight;
using iceCreamKiosk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using BE;
using BL;
namespace iceCreamKiosk.ViewModel
{
    public class FilterVM : ViewModelBase
    {
        public IceCreamLogic iceCreamLogic = new IceCreamLogic();
        public ICommand SearchCommand { get; set; }
        public ICommand ShowIceCreams { get; set; }

        private ObservableCollection<IceCream> icecreams;

        public ObservableCollection<IceCream> iceCreams { get => icecreams; set => Set(ref icecreams, value); }
        public FilterModel Filter { get; set; }
        public FilterVM()
        {

            Filter = new FilterModel();



            SearchCommand = new MyCommand(ExecuteSearch);
            ShowIceCreams = new RelayCommand<IceCream>(ExecuteShowICream); //MyCommand(ExecuteShowICream);
        }

        public async void ExecuteSearch()
        {
            iceCreams = new ObservableCollection<IceCream>(await iceCreamLogic.getFilteredIceCreams(null,Filter.getAsFilter()));
        }
        public void ExecuteShowICream(IceCream iceCream)
        {
            if (iceCream != null)
            {
                MessengerInstance.Send<ViewModelBase>(new StoreAndIceCreamVM(iceCream));
            }
        }
    }
}
