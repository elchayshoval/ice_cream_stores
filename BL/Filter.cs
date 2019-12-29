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
        static int minValue=0;
        static int maxValue = 1000;
        public string IceCreamDescription { get; set; }

        
  
        public Enums.Stars MaxStars { get; set; } = Enums.Stars.five;
        public Enums.Stars MinStars { get; set; } = Enums.Stars.one;

        public int? MaxCal { get; set; } = maxValue;
        public int? MinCal { get; set; } = minValue;
        public int? MaxProtein { get; set; } = maxValue;
        public int? MinProtein { get; set; } = minValue;
        public int? MaxFat { get; set; } = maxValue;
        public int? MinFat { get; set; } = minValue;

        public Boolean IsIceCreamRequested(IceCream iceCream)
        {
            return filerByDescription(iceCream) && FilerByScors(iceCream)&& filerByNutrition(iceCream);
        }

        private Boolean filerByDescription(IceCream iceCream)
        {
            Boolean result = true;
            if (IceCreamDescription != null)
            {
                result = iceCream.Name.Contains(IceCreamDescription) || iceCream.Description.Contains(IceCreamDescription);


            }
            return result;
        }
        private Boolean FilerByScors(IceCream iceCream)
        {
            return (iceCream.Score >= MinStars && iceCream.Score <= MaxStars);
        }
        private Boolean filerByNutrition(IceCream iceCream)
        {
            Nutrition nutrition = iceCream.Nutrient;
            SetDefultValues();
            Boolean result = true;
            if (nutrition == null) return false;
            if(nutrition.Energy<MinCal || nutrition.Energy > MaxCal)
            {
                result= false;
            }
            if(nutrition.Protein<MinProtein || nutrition.Protein > MaxProtein)
            {
                result= false;
            }
            if(nutrition.Fat<MinFat || nutrition.Fat > MaxFat)
            {
                result= false;
            }

            return result;
        }

        private void SetDefultValues()
        {
            if (MaxCal == null) MaxCal = maxValue;
            if (MaxProtein == null) MaxProtein = maxValue;
            if (MaxFat == null) MaxFat = maxValue;
            if (MinCal == null) MinCal = minValue;
            if (MinProtein == null) MinProtein = minValue;
            if (MinFat == null) MinFat = minValue;

        }
    }
}
