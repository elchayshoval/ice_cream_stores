using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class IceCreamService
    {
        public async Task<IEnumerable<IceCream>> GetIceCreams()
        {
            using (var db = new StoreContext())
            {
                //I have to add to include path the follow ".Feedbacks"
                return await db.IceCreams.Include("Feedbacks").ToListAsync();

            }

        }

        public void RemoveIceCream(IceCream iceCream)
        {
            using (var db = new StoreContext())
            {
                IceCream ice = db.IceCreams.Where(ic => ic.IceCreamId == iceCream.IceCreamId).FirstOrDefault();
                db.IceCreams.Remove(ice);
                db.SaveChanges();

            }
        }

        public async Task<bool> AddIceCream(IceCream iceCream)
        {
            using (var db = new StoreContext())
            {
                db.IceCreams.Add(iceCream);
                await db.SaveChangesAsync();
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
