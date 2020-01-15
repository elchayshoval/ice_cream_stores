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

        public void AddFeedback(FeedBack feedBack)
        {
            using (var db = new StoreContext())
            {
                db.Feedbacks.Add(feedBack);
                db.SaveChanges();
                
            }
        }

        public IEnumerable<FeedBack> GetFeedbackByIceCreamID(Guid iceCreamId)
        {
            using (var db = new StoreContext())
            {
                return db.Feedbacks.Where(f => f.IceCreamID == iceCreamId);
                

            }

        }

        public FeedBack GetFeedbackByID(Guid feedbackId)
        {
            using (var db = new StoreContext())
            {
                return db.Feedbacks.Where(f => f.FeedbackId == feedbackId).FirstOrDefault();
            }
        }
    }
}
