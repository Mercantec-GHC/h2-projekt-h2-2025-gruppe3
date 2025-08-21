using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    // Room.cs
    public class Room : Common
    {

        public required int RoomNumber { get; set; }

        public bool Booked { get; set; }

        public required int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public required int TypeId { get; set; } 
        public Type? Type { get; set; }
        public List<Booking> Bookings { get; set; } = new();
    }
    public class RoomGetDto
    {
        public required int Id { get; set; }
        public required int RoomNumber { get; set; }

        public bool Booked { get; set; }

        public required int HotelId { get; set; }
        public required int TypeId { get; set; }


        public List<Booking> Bookings { get; set; } = new();


    }
}
