using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DL
{
    public class IDL_Impllementation : IDL
    {
        //whhat is the correct way to handle errors ???????????????
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

        public IEnumerable<IceCream> GetIceCreams()
        {
            throw new NotImplementedException();//TDOD implement
        }

        public Store GetStoreByID(int id)
        {//what is with exeption??????????
            using (var db = new StoreContext())
            {
                return db.Stores.Find(id);

            }
        }

        public IEnumerable<Store> GetStores()
        {
            try
            {
                using (var db = new StoreContext())
                {
                    var query = from s in db.Stores
                                orderby s.Name
                                select s;
                    return query.ToList();

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
                    db.Stores.Remove(store);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateStore(Store store)
        {

            using (var db = new StoreContext())
            {
                var result = db.Stores.SingleOrDefault(s => s.StoreId == store.StoreId);
                if (result != null)
                {
                    result = store;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
