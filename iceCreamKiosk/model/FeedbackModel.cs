using BE;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    class FeedbackModel:ObservableObject
    {
        private Enums.Stars stars;
        private string description;
        private string image;

        public string Description { get => description; set => Set(ref description, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Stars { get => stars; set => Set(ref stars, value); }
    }
}
