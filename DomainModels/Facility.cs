using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{


    public class Facility : Common
    {
        public Boolean Pool { get; set; }
        public Boolean Fitness { get; set; }
        public Boolean Restaturant { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = default!;

    }
}
