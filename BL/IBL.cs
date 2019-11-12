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
        bool addStore(Store store);
        bool removeStore(Store store);
        bool updateStore(Store store);

    }
}
