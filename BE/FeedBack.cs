using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class FeedBack
    {

        public Guid FeedbackId { get; set; } = Guid.NewGuid();
        public Guid IceCreamID { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Enums.Stars Stars { get; set; }
    }
}
