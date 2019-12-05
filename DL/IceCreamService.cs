using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class IceCreamService
    {
        public IEnumerable<IceCream> GetIceCreams()
        {
            using (var db = new StoreContext())
            {
                //I have to add to include path the follow ".Feedbacks"
                return db.IceCreams.Include("Feedbacks").ToList();

            }

        }

        public void RemoveIceCream(IceCream iceCream)
        {
            using (var db = new StoreContext())
            {
                IceCream ice = db.IceCreams.Where(ic => ic.StoreId == iceCream.StoreId).FirstOrDefault();
                db.IceCreams.Remove(ice);
                db.SaveChanges();

            }
        }

        public bool AddIceCream(IceCream iceCream)
        {
            using (var db = new StoreContext())
            {
                db.IceCreams.Add(iceCream);
                db.SaveChanges();
                return true;
            }
        }
        public bool UpdateIceCream(IceCream iceCream)
        {
            using (var db = new StoreContext())
            {
                db.IceCreams.AddOrUpdate(iceCream);
                db.SaveChanges();
                return true;
            }
        }
    }
}
