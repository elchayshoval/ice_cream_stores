using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public class IceCream
    {

        public ClientsFeedback clientsFeedback = new ClientsFeedback();

        public string Name { get ; set ; }
        public string Description { get; set; }
        public int NtritionalId { get; set; }
        public string Image { get; set; }
        public Enums.Stars Score { get; set; }


    }
}
