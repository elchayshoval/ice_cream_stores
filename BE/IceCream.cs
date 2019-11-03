using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class IceCream
    {
        string name;
        string description;
        int ntritionalId;
        Enums.Stars score;
        string image;
        // clientsFeedBack from BL


        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int NtritionalId { get => ntritionalId; set => ntritionalId = value; }
        public string Image { get => image; set => image = value; }
        internal Enums.Stars Score { get => score; set => score = value; }
    }
}
