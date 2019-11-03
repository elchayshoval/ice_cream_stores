using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Person
    {
        string id;
        Enums.users type;

        public string Id { get => id; set => id = value; }
        internal Enums.users Type { get => type; set => type = value; }
    }
}
