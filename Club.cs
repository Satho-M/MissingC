using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    public class Club
    {
        public string idClub { get; set; }
        public string nameClub { get; set; }
        public string cityClub { get; set; }
        public string idChainClub { get; set; } //Foreign Key
        public int idUserClub { get; set; } //Foreign Key

    }


}
