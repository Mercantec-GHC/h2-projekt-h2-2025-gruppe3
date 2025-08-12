using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Rooms
    {
        public required string RoomNumber { get; set; }
        public required string Type { get; set; }
        public required string Booked { get; set; } //jeg ved ikke om vi skal bruge den her?
        public string? Crib { get; set; }
        public string? ExtraBeds { get; set; }
        public required int HotelId { get; set; }
        public int RoomPrice { get; set; }
    }
}