using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    // Hotel.cs
    public class Hotel : Common
    {
        public required string Name { get; set; }
        public string Road { get; set; } = "";
        public string Zip { get; set; } = "";
        public int City  { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; } = "";
        public string Facility { get; set; } = "";

        public List<Room> Rooms { get; set; } = new(); // 1:n
    }

    // DTO for hotel creation / POST
    public class HotelPostDto
    {
        public required string Name { get; set; }
        public string Road { get; set; } = "";
        public string Zip { get; set; } = "";
        public int City { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; } = "";
        public string Facility { get; set; } = "";
    }

    // DTO for hotel retrieval / GET
    //public class HotelWithRoomsGetDto
    //{
    //    public required string Id { get; set; }
    //    public required string Name { get; set; }
    //    public string Address { get; set; } = "";
    //    public List<Room> Rooms { get; private set; } = new();
    //}

    // DTO for hotel retrieval / GET
    public class HotelGetDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Road { get; set; } = "";
        public int Zip { get; set; }
        public int City { get; set; }
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Facility { get; set; } = "";
        //public List <RoomWithBookingsGetDto> Rooms { get; private set; } = new();
    }

    // DTO for hotel update / PUT
    public class HotelPutDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Road { get; set; } = "";
        public int Zip { get; set; }
        public string City { get; set;} = "";
        public int Phone { get; set; }
        public string Email { get; set; } = "";
        public string Facility { get; set; } = "";
    }
}

