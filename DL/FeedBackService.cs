using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class FeedBackService
    {
        public bool AddFeedback(FeedBack feedBack)
        {
            using (var db = new StoreContext())
            {
                db.Feedbacks.Add(feedBack);
                db.SaveChanges();
                return true;
            }
        }
    }
}
