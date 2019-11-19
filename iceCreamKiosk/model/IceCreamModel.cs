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
    class IceCreamModel:ObservableObject
    {
        
        private string name;
        private string description;
        private int nutritionalId;
        private string image;
        private Enums.Stars score;

        public string Name { get=> name; set => Set(ref name, value); }
        public string Description { get => description; set => Set(ref description, value); }
        public int NutritionalId { get => nutritionalId; set => Set(ref nutritionalId, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Score { get => score; set => Set(ref score, value); }


        public ClientsFeedback clientsFeedback = new ClientsFeedback();

        public ObservableCollection<FeedBack> FeedBacks { get; set; }
    }
}
