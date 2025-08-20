using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Mapping
{
    internal class RoomMapping
    {
        public static RoomGetDto ToRoomGetDto(Room room)
        {
            return new RoomGetDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Booked = room.Booked,
                HotelId = room.HotelId,
                TypeId = room.TypeId,
                Bookings = room.Bookings,
            };
        }

    }
}



