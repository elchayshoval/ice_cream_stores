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
        public IEnumerable<IceCream> GetIceCreams()
        {
            return iceCreamService.GetIceCreams();
        }

        public void RemoveIceCream(IceCream iceCream)
        {
            iceCreamService.RemoveIceCream(iceCream);
        }

        public Status AddIceCream(IceCream iceCream)
        {
            IceCreamValidation iceCreamValidation = new IceCreamValidation();
            Status status = Status.Success;
            iceCream.IceCreamId = Guid.NewGuid();
            try
            {
                if (iceCreamValidation.IsIceCreamValid(iceCream))
                {
                    iceCreamService.AddIceCream(iceCream);

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

        public List<IceCream> GetIceCreamsByStoreId(Guid storeId)
        {
            return GetIceCreams().Where(ice => ice.StoreId == storeId).ToList();
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

        public IEnumerable<IceCream> getFilteredIceCreams(Filter filter)
        {
             return GetIceCreams().Where(ice => filter.IsIceCreamRequested(ice)).ToList();
            
        }
    }
}

