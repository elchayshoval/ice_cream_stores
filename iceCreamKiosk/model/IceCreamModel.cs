using BE;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    public class IceCreamModel:ObservableObject
    {
        public IceCream IceCream { get; set; }

        private string name;
        private string description;        
        private Byte[] image;
        private Enums.Stars score;
        private int nutritionalID;
        private int? sumCal;
        private int? sumProtein;
        private int? sumFat;

        public string Name { get=> name; set => Set(ref name, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public Byte[] Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Score { get => score; set => Set(ref score, value); }
        public int NutritionalID { get => nutritionalID; set => Set(ref nutritionalID, value); }
        public ObservableCollection<FeedBack> Feedbacks { get; set; }

        public int? SumCal { get => sumCal; set => Set(ref sumCal, value); }
        public int? SumProtein { get => sumProtein; set => Set(ref sumProtein, value); }
        public int? SumFat { get => sumFat; set => Set(ref sumFat, value); }
        
        public IceCreamModel(IceCream iceCream= null)
        {
            IceCream = iceCream;
            if(IceCream == null)
            {
                IceCream = new IceCream();
            }
            // convert
            this.Name = IceCream.Name;
            this.Description = IceCream.Description;
            this.Image = IceCream.Image;
            this.Score = IceCream.Score;
            this.NutritionalID = IceCream.NtritionalId;
            this.Feedbacks =new ObservableCollection<FeedBack>( iceCream.Feedbacks);
            this.SumCal = IceCream.Nutrient.Energy;
            this.SumProtein = IceCream.Nutrient.Protein;
            this.SumFat = IceCream.Nutrient.Fat;
            
        }

        public bool IsValidate()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        public IceCream GetAsStore()
        {
            IceCream.Name =this.Name;
            IceCream.Description=this.Description;
            IceCream.Image=this.Image;
            IceCream.Score=this.Score;
            IceCream.Nutrient.Energy=this.SumCal;
            IceCream.Nutrient.Protein=this.SumProtein;
            IceCream.Nutrient.Fat=this.SumFat;
            return IceCream;
        }

        internal bool IsAllFeildsClear()
        {
            bool result = false;
            if (string.IsNullOrEmpty(Name)&& string.IsNullOrEmpty(Description)
                && Image==null && Score==Enums.Stars.one
                &&SumCal!=null&&SumProtein!=null&&SumFat!=null)
            {
                result = true;
            }
            return result;
        }

        public void ClearAllFeilds()
        {
            Name = IceCream.Name;
            Description = IceCream.Description;
            Image = IceCream.Image;
            Score = IceCream.Score;
            this.SumCal=IceCream.Nutrient.Energy;
            this.SumProtein=IceCream.Nutrient.Protein ;
            this.SumFat=IceCream.Nutrient.Fat ;

        }
    }
}
