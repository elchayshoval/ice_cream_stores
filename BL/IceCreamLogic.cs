using BE;
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
        private IceCreamService iceCreamService = new IceCreamService();
        public IEnumerable<IceCream> GetIceCreams()
        {
            return iceCreamService.GetIceCreams();
        }

        public void RemoveIceCream(IceCream iceCream)
        {
            iceCreamService.RemoveIceCream(iceCream);
        }

        public bool AddIceCream(IceCream iceCream)
        {
            //add some validations
            return iceCreamService.AddIceCream(iceCream);
        }
    }
}
