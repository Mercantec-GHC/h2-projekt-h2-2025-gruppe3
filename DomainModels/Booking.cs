using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Booking
    {
        public required string UserId { get; set; }
		public User? User { get; set; }
		public required string RoomId { get; set; }
		public Room? Room { get; set; }
		public required string StartDate { get; set; }
        public required string EndDate { get; set; }

      //  public required int Price { get; set; } test

	}
}