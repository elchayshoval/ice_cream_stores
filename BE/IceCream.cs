using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public class IceCream
    {
        public Guid IceCreamId { get; set; } = Guid.NewGuid();
        public List<FeedBack> Feedbacks { get; set; } = new List<FeedBack>();

        [NotMapped]
        public ClientsFeedback clientsFeedback { get; set; } = new ClientsFeedback();

        //public ICollection<FeedBack> feedBacks { get; set; }
        public string Name { get ; set ; }
        public string Description { get; set; }
        public int NtritionalId { get; set; }
        public string Image { get; set; }
        public Enums.Stars Score { get; set; }
        public Guid StoreId { get; set; }

  
    }
}
