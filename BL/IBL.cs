using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    interface IBL
    {
        
        IEnumerable<Store> GetStores();
        IEnumerable<IceCream> GetIceCreams();
        IEnumerable<IceCream> GetIceCreams(Filter filter);
        Boolean AddStore(Store store);
        Boolean RemoveStore(Store store);
        Boolean UpdateStore(Store store);
        Store GetStoreByID(int id);

    }
}
