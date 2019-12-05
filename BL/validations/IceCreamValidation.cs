using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DL;

namespace BL.validations
{
    class IceCreamValidation
    {
        internal bool IsIceCreamValid(IceCream iceCream)
        {
            Boolean answer = true;
            StoreService storeService = new StoreService();
            var result = storeService.GetStoreByID(iceCream.StoreId).IceCreams.
                Where(ice => ice.IceCreamId!=iceCream.IceCreamId && ice.Name == iceCream.Name).
                ToList();
            if (result.Count > 0)
            {
                answer = false;
            }
            return answer;
        }
    }
}
