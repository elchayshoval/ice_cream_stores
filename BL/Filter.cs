using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public class Filter 
    {
        public string IceCreamDescription { get; set; }

        
  
        public Enums.Stars MaxStars { get; set; } = Enums.Stars.five;
        public Enums.Stars MinStars { get; set; } = Enums.Stars.one;

        public int? MaxCal { get; set; }
        public int? MaxProtein { get; set; }
        public int? MaxFat { get; set; }

        public Boolean IsIceCreamRequested(IceCream iceCream)
        {
            return filerByDescription(iceCream) && filerByScors(iceCream);//|| filerByNutrition(iceCream);
        }

        public Boolean filerByDescription(IceCream iceCream)
        {
            if (IceCreamDescription != null)
            {
                if (!iceCream.Name.Contains(IceCreamDescription))
                    return false;
            }
            return true;
        }
        public Boolean filerByScors(IceCream iceCream)
        {
            if (!(iceCream.Score >= MinStars && iceCream.Score <= MaxStars))
                return false;

            return true;
        }
        public Boolean filerByNutrition(IceCream iceCream)
        {
            Nutrition Nutrition = (NutritionalValue.getNutritionalValue(iceCream.NtritionalId));

            if (MaxCal != null)
            {
                if (Nutrition.Energy > MaxCal)
                    return false;
            }
            if (MaxProtein != null)
            {
                if (Nutrition.Protein > MaxProtein)
                    return false;
            }
            if (MaxFat != null)
            {
                if (Nutrition.Fat > MaxFat)
                    return false;
            }
            return true;
        }
    }
}
