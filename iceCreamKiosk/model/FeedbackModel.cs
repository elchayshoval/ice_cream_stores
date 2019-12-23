using BE;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iceCreamKiosk.model
{
    public class FeedbackModel:ObservableObject
    {
        public FeedBack FeedBack { get; set; }

        private Enums.Stars stars;
        private string description;
        private string image;

        public string Description { get => description; set => Set(ref description, value); }
        public string Image { get => image; set => Set(ref image, value); }
        public Enums.Stars Stars { get => stars; set => Set(ref stars, value); }

        public FeedbackModel(FeedBack feedBack = null)
        {
            FeedBack = feedBack;
            if(FeedBack == null)
            {
                FeedBack = new FeedBack();
            }
           
        }

        public bool IsAllFeildsClear()
        {
            bool result = false;
            if (string.IsNullOrEmpty(Description))
            {
                result = true;
            }
            return result;
        }

        public FeedBack getAsFeedBack()
        {
            FeedBack feedBack = new FeedBack();
            feedBack.Description = this.Description;
            feedBack.FeedbackId = this.FeedBack.FeedbackId;
            feedBack.IceCreamID = this.FeedBack.IceCreamID;
            feedBack.Image = this.Image;
            feedBack.Stars = this.Stars;
            return feedBack;
        }
    }
}
