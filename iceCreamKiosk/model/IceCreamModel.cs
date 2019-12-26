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

        public string Name { get=> name; set => Set(ref name, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public Byte[] Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Score { get => score; set => Set(ref score, value); }
        public int NutritionalID { get => nutritionalID; set => Set(ref nutritionalID, value); }
        public ObservableCollection<FeedBack> Feedbacks { get; set; }

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
            IceCream.NtritionalId = this.NutritionalID;
            return IceCream;
        }

        internal bool IsAllFeildsClear()
        {
            bool result = false;
            if (string.IsNullOrEmpty(Name)&& string.IsNullOrEmpty(Description)&& Image==null && Score==Enums.Stars.one)
            {
                result = true;
            }
            return result;
        }

        public void ClearAllFeilds()
        {
            Name = string.Empty;
            Description = string.Empty;
            Image = null;
            Score = Enums.Stars.one;
            NutritionalID = 0;
        }
    }
}
