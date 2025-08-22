using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Facility : Common
    {
        public bool Pool { get; set; }
        public bool Fitness { get; set; }
        public bool Restaturant { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
