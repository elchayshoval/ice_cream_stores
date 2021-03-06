﻿using BE;
using BL;
using GalaSoft.MvvmLight;
using iceCreamKiosk.placesApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    public class StoreModel : ObservableObject
    {
        private string name;
        private string image;
        private string location;
        private string phone;
        private ObservableCollection<IceCream> iceCreams;
        private byte[] map;


        public Store Store { get; set; }
        public string Name { get => name; set => Set(ref name, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public string Location
        {
            get => location;
            set { Set(ref location, value); UpdateMap(value); }
        }

        private void UpdateMap(string location)
        {
            try
            {
                if (location != Store.Address)
                {
                    Map = new MapService().GetMap(location);
                }
            }
            catch (Exception)
            {

            }
        }

        public string Phone { get => phone; set => Set(ref phone, value); }


        public Byte[] Map { get { return map; } set { Set(ref map, value); } }


        public ObservableCollection<IceCream> IceCreams { get => iceCreams; set => Set(ref iceCreams, value); }

        public StoreModel(Store store = null)
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
            Map = Store.Map;

            IceCreams = new ObservableCollection<IceCream>(Store.IceCreams);

        }

        public Boolean IsValidate()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
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
            Store.Map = Map;
            // what is with icecream collection?
            return Store;
        }

        public void ClearAllFeilds()
        {
            Name = Store.Name;
            Location = Store.Address;
            Phone = Store.Phone;
            Image = Store.Image;
            Map = Store.Map;
        }

        public bool IsAllFeildsClear()
        {
            Boolean result = false;
            if (String.IsNullOrWhiteSpace(Name) && String.IsNullOrWhiteSpace(Location) 
                && String.IsNullOrWhiteSpace(Phone) && string.IsNullOrEmpty(Image))
            {
                result = true;
            }
            return result;//I have to change and improve the code!!! 
        }

        public async void UpdateIceCreamCollection()
        {
            IceCreamLogic iceCreamLogic = new IceCreamLogic();
            IceCreams = new ObservableCollection<IceCream>(await iceCreamLogic.GetIceCreamsByStoreId(Store.StoreId));
        }
    }
}
