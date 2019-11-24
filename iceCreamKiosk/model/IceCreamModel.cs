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
        private string image;
        private Enums.Stars score;


        public string Name { get=> name; set => Set(ref name, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Score { get => score; set => Set(ref score, value); }

        public ClientsFeedback clientsFeedback = new ClientsFeedback();
        public ObservableCollection<FeedBack> FeedBacks { get; set; }

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
            this.clientsFeedback = IceCream.clientsFeedback;
        }

        public void ClearAllFeilds()
        {
            Name = string.Empty;
            Description = string.Empty;

        }
    }
}
