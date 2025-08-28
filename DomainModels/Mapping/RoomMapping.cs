using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Mapping
{
    public class RoomMapping
    {
        public static RoomGetDto ToRoomGetDto(Room room)
        {
            return new RoomGetDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Booked = room.Booked,
                HotelId = room.HotelId,
                RoomtypeId = room.RoomtypeId,
            };
        }

        public static List<RoomGetDto> ToRoomGetDtos(List<Room> rooms)
        {
            return rooms.Select(r => ToRoomGetDto(r)).ToList();
        }

        public static Room PostRoomFromDto(RoomPostDto roomPostDto)
        {
            return new Room
            {
                Id = roomPostDto.Id,
                RoomNumber = roomPostDto.RoomNumber,
                Booked = roomPostDto.Booked,
                HotelId = roomPostDto.HotelId,
                RoomtypeId = roomPostDto.RoomtypeId,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };
        }

        public static Room PutRoomFromDto(RoomPostDto roomPostDto)
        {
            return new Room
            {
                Id = roomPostDto.Id,
                RoomNumber = roomPostDto.RoomNumber,
                Booked = roomPostDto.Booked,
                HotelId = roomPostDto.HotelId,
                RoomtypeId = roomPostDto.RoomtypeId,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
