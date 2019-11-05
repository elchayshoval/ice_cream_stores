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
        void addStore(Store store);
        void removeStore(Store store);
        void updateStore(Store store);

    }
}
