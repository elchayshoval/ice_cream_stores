using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DL;

namespace BL
{
    class IBL_Imlementation : IBL
    {
        //how to treat exeptions ???????????????
        private IDL dL = new IDL_Impllementation();
        public bool AddStore(Store store)
        {
            //I have to add someValidation
            return dL.AddStore(store);
        }

        public IEnumerable<IceCream> GetIceCreams()
        {
            return dL.GetIceCreams();
        }

        public IEnumerable<IceCream> GetIceCreams(Filter filter)
        {
            //tdod implement
            throw new NotImplementedException();

        }

        public Store GetStoreByID(int id)
        {
            return dL.GetStoreByID(id);
        }

        public IEnumerable<Store> GetStores()
        {
            return dL.GetStores();
        }

        public bool RemoveStore(Store store)
        {
            //add some validation
            return dL.RemoveStore(store);
        }

        public bool UpdateStore(Store store)
        {
            //add some validations
            return dL.UpdateStore(store);
        }
    }
}
