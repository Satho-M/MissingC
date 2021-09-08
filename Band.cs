using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    public class Band
    {
        public int idBand { get; set; }
        public string nameBand { get; set; }
        public int riderBand { get; set; }
        public int artCutBand { get; set; }
        public string lastInviteBand { get; set; }
        public string idChainBand { get; set; } //Foreign Key
        public int idUserBand { get; set; } //Foreign Key
    }
}