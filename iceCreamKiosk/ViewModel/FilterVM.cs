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

namespace iceCreamKiosk.ViewModel
{
    public class FilterVM: ViewModelBase
    {
        public ICommand SearchCommand { get; set; }
        public ICommand ShowIceCream { get; set; }

        public ObservableCollection<IceCreamModel> iceCreamModel { get; set; }



        public FilterModel Filter { get; set; }
        public FilterVM(FilterModel filter)
        {
            Filter = filter;
            if(Filter == null)
            {
                Filter = new FilterModel();

            }

            SearchCommand = new MyCommand(ExecuteSearch);
            ShowIceCream = new MyCommand(ExecuteShowICream);
        }

        public void ExecuteSearch()
        {

        }
        public void ExecuteShowICream()
        {

        }
    }
}
