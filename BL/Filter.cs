using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    class Filter
    {
        public string IceCreamDescription { get; set; }
        public Enums.Stars MaxStars { get; set; } = Enums.Stars.five;
        public Enums.Stars MinStars { get; set; } = Enums.Stars.one;

        public int? MaxKcal { get; set; }
        public int? MaxProtein { get; set; }
        public int? MaxFat { get; set; }

        public Boolean IsIceCreamRequested(IceCream iceCream)
        {
            return false;//TDOD i have to implement this
        }
    }
}
