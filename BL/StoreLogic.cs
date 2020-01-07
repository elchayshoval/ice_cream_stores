using BE;
using BL.validations;
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
        public enum Status { Success, NoInternetConnection, DBError, InvalidNameOrLocation }
        private StoreService storeService = new StoreService();
        public async Task<Status> AddStore(Store store)
        {
            StoreValidation storeValidation = new StoreValidation();
            Status status = Status.Success;
            store.StoreId = Guid.NewGuid();
            try
            {
                if (await storeValidation.IsStoreValid(store))
                {
                    await storeService.AddStore(store);

                }
                else
                {
                    status = Status.InvalidNameOrLocation;
                }

            }
            catch (Exception)
            {

                status = Status.DBError;
            }
            return status;
        }

        public Store GetStoreByID(Guid id)
        {//what is with exeption??????????
            return storeService.GetStoreByID(id);
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            //if (search == null) search = string.Empty;
             
            var result =await storeService.GetStores();
            return result;
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
        public Status UpdateStore(Store store)
        {
            StoreValidation storeValidation = new StoreValidation();
            Status status = Status.Success;
            
            try
            {
               //vlidation for update needed
                    storeService.UpdateStore(store);

                
            }
            catch (Exception)
            {

                status = Status.DBError;
            }
            return status;

        }
    }
}
