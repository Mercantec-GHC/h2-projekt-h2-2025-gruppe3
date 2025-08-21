using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{


    public class Facilities : Common
    {
        public Boolean Pool { get; set; }
        public Boolean Fitness { get; set; }
        public Boolean Restaturant { get; set; }
        public required int HotelId { get; set; }
    }
}
