using BE;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class StoreLogic
    {
        private StoreService storeService = new StoreService();
        public bool AddStore(Store store)
        {
            //add some validations
            return storeService.AddStore(store);
        }

        public Store GetStoreByID(Guid id)
        {//what is with exeption??????????
            return storeService.GetStoreByID(id);
        }

        public IEnumerable<Store> GetStores()
        {
            return storeService.GetStores();
        }

        public bool RemoveStore(Store store)
        {
            return storeService.RemoveStore(store);
        }
        /// <summary>
        /// Update store only
        /// </summary>
        /// <param name="store">the store to update, don't update inner collections</param>
        /// <returns></returns>
        public bool UpdateStore(Store store)
        {

            return storeService.UpdateStore(store);
        }
    }
}
