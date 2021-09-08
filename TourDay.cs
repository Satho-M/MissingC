using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    class TourDay
    {
        public int idTD { get; set; }
        public string dateTD { get; set; }
        public string timeTD { get; set; }
        public string cityTD { get; set; }
        public string textBoxNameTD { get; set; }
        public int idTourTD { get; set; } //Foreign Key
        public int idUserTD { get; set; } //Foreign Key
    }
}
