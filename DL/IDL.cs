using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DL
{
    public  interface IDL
    {
        IEnumerable<Store> GetStores();
        IEnumerable<IceCream> GetIceCreams();
        Boolean AddStore(Store store);
        Boolean RemoveStore(Store store);
        Boolean UpdateStore(Store store);
        Store GetStoreByID(int id);
    }
}
