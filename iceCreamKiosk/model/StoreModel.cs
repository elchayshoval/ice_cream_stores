using BE;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    class StoreModel:ObservableObject
    {
        private string name;
        private string image;
        private string address;
        private string phone;
        public List<IceCream> iceCreams { get; set; } = new List<IceCream>();

        public string Name { get => name; set => Set(ref name, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public string Address { get => address; set => Set(ref address, value); }
        public string Phone { get => phone; set => Set(ref phone, value); }
        public ObservableCollection<IceCream> IceCreams { get ; set; }
    }
}
