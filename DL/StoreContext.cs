using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class StoreContext : DbContext
    {

        public DbSet<Store> Stores { get; set; }
        public DbSet<IceCream> IceCreams { get; set; }
        public DbSet<FeedBack> Feedbacks { get; set; }
    }
}
