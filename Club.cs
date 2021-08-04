using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingC
{
    public class Club
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ChainId { get; set; }
        public string UserId { get; set; }

        public override bool Equals(object obj)
        {
            Club c = obj as Club;
            return c != null && c.Id == this.Id && c.Name == this.Name && c.City == this.City;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Name.GetHashCode() ^ this.City.GetHashCode();
        }
    }


}
