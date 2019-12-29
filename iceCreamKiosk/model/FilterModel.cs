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
    public class FilterModel : ObservableObject
    {
        public Filter filter { get; set; }


        private string iceCreanDescription;
        private Enums.Stars maxStars = Enums.Stars.five;
        private Enums.Stars minStars = Enums.Stars.one;
        private int? maxCal;
        private int? maxProtein;
        private int? maxFat;
        private int? minCal;
        private int? minProtein;
        private int? minFat;

        public string IceCreanDescription { get => iceCreanDescription; set => Set(ref iceCreanDescription, value); }
        public Enums.Stars MaxStars { get => maxStars; set => Set(ref maxStars, value); }
        public Enums.Stars MinStars { get => minStars; set => Set(ref minStars, value); }
        public int? MaxCal { get => maxCal; set => Set(ref maxCal, value); }
        public int? MinCal { get => minCal; set => Set(ref minCal, value); }
        public int? MaxProtein { get => maxProtein; set => Set(ref maxProtein, value); }
        public int? MinProtein { get => minProtein; set => Set(ref minProtein, value); }
        public int? MaxFat { get => maxFat; set => Set(ref maxFat, value); }
        public int? MinFat { get => minFat; set => Set(ref minFat, value); }

        public FilterModel(Filter filter = null)
        {
            this.filter = filter;
            if (this.filter == null)
            {
                this.filter = new Filter();
            }
            ClearAllFeilds();
        }

        public void ClearAllFeilds()
        {
            IceCreanDescription = filter.IceCreamDescription;
            MaxCal = filter.MaxCal;
            MaxProtein = filter.MaxProtein;
            MaxFat = filter.MaxFat;
            MinCal = filter.MinCal;
            MinProtein = filter.MinProtein;
            MinFat = filter.MinFat;
            MaxStars = Enums.Stars.five;
            MinStars = Enums.Stars.one;
        }

        public bool IsAllFeildsClear()
        {
            Boolean result = false;
            if (String.IsNullOrWhiteSpace(IceCreanDescription) && MaxCal == null && MaxProtein == null && maxFat == null)
            {
                result = true;
            }
            return result;
        }

        public Filter getAsFilter()
        {
            filter.IceCreamDescription = this.IceCreanDescription;
            filter.MaxCal = this.MaxCal;
            filter.MaxFat = this.MaxFat;
            filter.MaxProtein = this.MaxProtein;
            filter.MinCal = this.MinCal;
            filter.MinFat = this.MinFat;
            filter.MinProtein = this.MinProtein;

            filter.MaxStars = this.MaxStars;
            filter.MinStars = this.minStars;

            return filter;
        }




    }
}
