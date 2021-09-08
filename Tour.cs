using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    class Tour
    {
        public int idTour { get; set; }
        public int yearTour { get; set; }
        public string typeTour { get; set; }
        public int idBandTour { get; set; } //Foreign Key
        public int idUserTour { get; set; } //Foreign Key
    }
}
