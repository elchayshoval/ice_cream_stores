using BE;
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
    class SearchIceCreamVM:ViewModelBase // insert this class to the Ilocator
    {
        private string description;
        public ICommand SearchCommand { get; set; }
        public ICommand DeepSearchCommand { get; set; }


        public ObservableCollection<IceCreamModel> iceCreamModel { get; set; }

        private List<IceCream> iceCreams;

        //  public IceCreamModel IceCream { get; set; }

        public string Description { get => description; set => Set(ref description,value); }
        public List<IceCream> IceCreams { get => iceCreams; set => Set(ref iceCreams,value); }



        public SearchIceCreamVM()
        {
           // if(this.IceCream == null)
            //{
              //  IceCream = new IceCreamModel();
            //}
            SearchCommand = new MyCommand(ExecuteCancel);
            DeepSearchCommand = new MyCommand(ExecuteCancel);
        }
        public void ExecuteCancel()
        {

        }
        

    }
}
