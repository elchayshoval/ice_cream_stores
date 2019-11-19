using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class FeedBack
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        internal Enums.Stars Feedback { get; set; }
    }
}
