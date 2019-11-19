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
    public class StoreModel:ObservableObject
    {
        private string name;
        private string image;
        private string location;
        private string phone;


        public Store Store { get; set; }
        public string Name { get => name; set => Set(ref name, value); }
        public string Image { get => image; set => Set(ref image, value); }   
        public string Location { get => location; set => Set(ref location, value); }
        public string Phone { get => phone; set => Set(ref phone, value); }
        
        public List<IceCream> iceCreams { get; set; } = new List<IceCream>();
        public ObservableCollection<IceCream> IceCreams { get ; set; }

        public StoreModel(Store store=null)
        {
            Store = store;
            if (Store == null)
            {
                Store = new Store();
            }
        }

        public void ClearAllFeilds()
        {
            Name = string.Empty;
            Location = string.Empty;
            Phone = string.Empty;
            //TDOD I Have to implement the memthod with iteratinung on all propertys;
        }

        public bool IsAllFeildsClear()
        {
            Boolean result = false;
            if(String.IsNullOrWhiteSpace(Name)&& String.IsNullOrWhiteSpace(Location) && String.IsNullOrWhiteSpace(Phone))
            {
                result = true;
            }
            return result;//I have to change and improve the code!!! 
        }
    }
}
