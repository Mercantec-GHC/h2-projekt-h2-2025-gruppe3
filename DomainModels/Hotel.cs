using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Hotel : Common
    {
		public List<Room> Rooms { get; set; } = new(); // 1:n
		public required string Name { get; set; }
        public required string Road { get; set; }
        public required string Zip { get; set; }
        public required string City  { get; set; }
        public required string PhoneNo  { get; set; }
        public required string ContactMail { get; set; }
        public required string Facilities { get; set; }
    }
}