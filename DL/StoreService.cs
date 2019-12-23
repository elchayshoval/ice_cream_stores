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
    public class StoreService
    {
        public bool AddStore(Store store)
        {
            try
            {
                using (var db = new StoreContext())
                {
                    db.Stores.Add(store);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Store GetStoreByID(Guid id)
        {//what is with exeption??????????
            using (var db = new StoreContext())
            {
                //I have to add to include path the follow ".Feedbacks"
                return db.Stores.Include("IceCreams").Where(s=>s.StoreId==id).FirstOrDefault();

            }
        }

        public  IEnumerable<Store> GetStores()
        {
            try
            {
                using (var db = new StoreContext())
                {
                    //I have to add to include path the follow ".Feedbacks"
                    return  db.Stores.Include("IceCreams.Feedbacks").ToList(); 
                    //return await db.Stores.Include("IceCreams.Feedbacks").ToListAsync(); 

                }
            }
            catch (Exception)
            {
                return null; ;
            }
        }

        public bool RemoveStore(Store store)
        {
            try
            {
                using (var db = new StoreContext())
                {
                    Store s = db.Stores.Where(st => st.StoreId == store.StoreId).FirstOrDefault();
                    db.Stores.Remove(s);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Update store only
        /// </summary>
        /// <param name="store">the store to update, don't update inner collections</param>
        /// <returns></returns>
        public bool UpdateStore(Store store)
        {

            using (var db = new StoreContext())
            {
                db.Stores.AddOrUpdate(store);
                db.SaveChanges();
                return true;
            }
            
        }
    }
}
