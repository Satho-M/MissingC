using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    public class TicketPrice
    {
        public int idTicket { get; set; }
        public string popTicket { get; set; }
        public string priceTicket { get; set; }
        public int idUserTicket { get; set; } //Foreign Key
        public int idBandTicket { get; set; } //Foreign Key
    }
}
