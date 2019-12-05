using BE;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.validations
{
    class StoreValidation
    {
        public Boolean IsStoreValid(Store store)
        {
            StoreService storeService = new StoreService();
            Boolean result = true;

            var list = storeService.GetStores().
                Where(s =>s.StoreId!=store.StoreId&& s.Name == store.Name && s.Address == store.Address);
            if (list.Count() > 0)
            {
                result = false;
            }

            return result; ;
        }
            


    }
    
}
