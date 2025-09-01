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


        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public required int RoomtypeId { get; set; }
        public Roomtype? Roomtype { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
    public class RoomGetDto
    {
        public required int Id { get; set; }
        public required int RoomNumber { get; set; }


        public required int HotelId { get; set; }
        public required int RoomtypeId { get; set; }




    }


    // DTO for room creation / POST
    public class RoomPostDto
    {
        public required int RoomNumber { get; set; }


        public required int HotelId { get; set; }
        public required int RoomtypeId { get; set; }
    }


    // DTO for room update / PUT
    public class RoomPutDto
    {
        public required int Id { get; set; }
        public required int RoomNumber { get; set; }


        public required int HotelId { get; set; }
        public required int RoomtypeId { get; set; }


    }
}


