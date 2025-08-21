using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    // Booking.cs
    public class Booking : Common
    {
        public required int UserId { get; set; }
        public User? User { get; set; }

        public required int RoomId { get; set; }
        public double FinalPrice { get; set; }
        public bool Crib { get; set; }
        public bool ExtraBed { get; set; }
        public Room? Rooms { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Pris og prisberegning
    }
}