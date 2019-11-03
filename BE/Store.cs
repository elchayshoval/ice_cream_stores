using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Store
    {
        string name;
        string image;
        string address;
        string phone;
        List<IceCream> iceCreams = new List<IceCream>();

        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        internal List<IceCream> IceCreams { get => iceCreams; set => iceCreams = value; } 
    }
}
