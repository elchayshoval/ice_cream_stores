using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class FeedBack
    {
        Enums.Stars feedback;
        string description;
        string image;

        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }
        internal Enums.Stars Feedback { get => feedback; set => feedback = value; }
    }
}
