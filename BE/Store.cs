using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Store
    {

        public Guid StoreId { get; set; } = Guid.NewGuid();

        public string Name { get; set ; }
        public string Image { get ; set ; }
        public string Address { get ; set; }
        public string Phone { get ; set ; }
        public List<IceCream> IceCreams { get; set; } = new List<IceCream>();
    }
}
