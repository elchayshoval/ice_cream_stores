﻿using BE;
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
        private ObservableCollection<IceCream> iceCreams;

        public Store Store { get; set; }
        public string Name { get => name; set => Set(ref name, value); }
        public string Image { get => image; set => Set(ref image, value); }   
        public string Location { get => location; set => Set(ref location, value); }
        public string Phone { get => phone; set => Set(ref phone, value); }
        
        //public List<IceCream> iceCreams { get; set; } = new List<IceCream>();
        public ObservableCollection<IceCream> IceCreams { get => iceCreams; set => Set(ref iceCreams, value); }

        public StoreModel(Store store=null)
        {
            Store = store;
            if (Store == null)
            {
                Store = new Store();
            }
            Name = Store.Name;
            Location = Store.Address;
            Image = Store.Image;
            Phone = Store.Phone;
            IceCreams = new ObservableCollection<IceCream>(Store.IceCreams);

        }

        public Boolean IsValidate()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                //add other validations !!!!!
                return true;
            }
            return false;
        }
        public Store GetAsStore()
        {
            Store.Address = Location;
            Store.Image = Image;
            Store.Name = Name;
            Store.Phone = Phone;
            // what is with icecream collection?
            return Store;
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
