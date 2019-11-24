using BE;
using BL;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    public class FilterModel: ObservableObject
    {
        public Filter Filter{ get; set; }
        public IceCream IceCream { get; set; }


        private string iceCreanDescription;
        private Enums.Stars maxStars = Enums.Stars.five;
        private Enums.Stars minStars = Enums.Stars.one;
        private int? maxCal;
        private int? maxProtein;
        private int? totalFat;
        private IceCream selectedIceCream;

        public IceCream SelectedIceCream { get => selectedIceCream; set => Set(ref selectedIceCream, value); }
        public string IceCreanDescription { get => iceCreanDescription; set => Set(ref iceCreanDescription, value);}
        public Enums.Stars MaxStars { get => maxStars; set => Set(ref maxStars , value); }
        public Enums.Stars MinStars { get => minStars; set => Set(ref minStars, value); }
        public int? MaxCal { get => maxCal; set => Set(ref maxCal, value); }
        public int? MaxProtein { get => maxProtein; set => Set(ref maxProtein, value); }
        public int? TotalFat { get => totalFat; set => Set(ref totalFat, value); }

        public FilterModel(Filter filter = null)
        {
            Filter = filter;
            if(Filter == null)
            {
                Filter = new Filter();
            }
        }
        
        public void ClearAllFeilds()
        {
            IceCreanDescription = string.Empty;
            MaxCal = null;
            MaxProtein = null;
            TotalFat = null;
            MaxStars = Enums.Stars.five;
            MinStars = Enums.Stars.one;
        }

        public bool IsAllFeildsClear()
        {
            Boolean result = false;
            if(String.IsNullOrWhiteSpace(IceCreanDescription) && MaxCal == null && MaxProtein == null && totalFat == null)
            {
                result = true;
            }
            return result;
        }






    }
}
