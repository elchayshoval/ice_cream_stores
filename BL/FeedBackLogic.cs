using BE;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FeedBackLogic
    {
        public FeedBackService feedBackService = new FeedBackService();


        public bool addFeedBack(FeedBack feedback)
        {
            return feedBackService.AddFeedback(feedback);
        }
    }
}
