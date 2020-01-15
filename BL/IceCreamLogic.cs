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
    public class IceCreamLogic
    {
        public enum Status { Success, NoInternetConnection, DBError, InvalidName }
        private IceCreamService iceCreamService = new IceCreamService();
        public async Task<IEnumerable<IceCream>> GetIceCreams()
        {
            
            return await iceCreamService.GetIceCreams();
        }

        public void RemoveIceCream(IceCream iceCream)
        {
            iceCreamService.RemoveIceCream(iceCream);
        }

        public async Task<Status> AddIceCream(IceCream iceCream)
        {
            IceCreamValidation iceCreamValidation = new IceCreamValidation();
            Status status = Status.Success;
            iceCream.IceCreamId = Guid.NewGuid();
            try
            {
                if (iceCreamValidation.IsIceCreamValid(iceCream))
                {
                    await iceCreamService.AddIceCream(iceCream);

                }
                else
                {
                    status = Status.InvalidName;
                }
                

            }
            catch (Exception)
            {

                status = Status.DBError;
            }
            return status;
        }

        public async Task<List<IceCream>> GetIceCreamsByStoreId(Guid storeId)
        {
            return (await GetIceCreams()).Where(ice => ice.StoreId == storeId).ToList();
        }

        public Status UpdateIceCream(IceCream iceCream)
        {
            IceCreamValidation iceCreamValidation = new IceCreamValidation();
            Status status = Status.Success;
            try
            {
                if (iceCreamValidation.IsIceCreamValid(iceCream))
                {
                    iceCreamService.UpdateIceCream(iceCream);

                }
                else
                {
                    status = Status.InvalidName;
                }


            }
            catch (Exception)
            {

                status = Status.DBError;
            }
            return status;
        }

        public async Task<IEnumerable<IceCream>> getFilteredIceCreams(IEnumerable<IceCream> allIceCreams, Filter filter)
        {
            if (allIceCreams == null)
            {
                allIceCreams =await GetIceCreams();
            }
            if (filter == null)
            {
                return allIceCreams;
            }
             return allIceCreams.Where(ice => filter.IsIceCreamRequested(ice)).ToList();
            
        }
    }
}

