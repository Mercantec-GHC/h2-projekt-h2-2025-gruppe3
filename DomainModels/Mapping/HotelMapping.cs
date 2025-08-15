using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Mapping
{
    public class HotelMapping
    {
        public static HotelGetDto ToHotelGetDto(Hotel hotel)
        {
            return new HotelGetDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Road = hotel.Road,
                Zip = hotel.Zip,
                City = hotel.City,
                Phone = hotel.Phone,
                Email = hotel.Email,
                Facility = hotel.Facility,
    };
        }

        public static List<HotelGetDto> ToHotelGetDtos(List<Hotel> hotels)
        {
            return hotels.Select(h => ToHotelGetDto(h)).ToList();
        }

        public static Hotel ToHotelFromDto(HotelPostDto hotelPostDto)
        {
            return new Hotel
            {
                Id = Guid.NewGuid().ToString(),
                Name = hotelPostDto.Name,
                Road = hotelPostDto.Road,
                Zip = hotelPostDto.Zip,
                City = hotelPostDto.City,
                Phone = hotelPostDto.Phone,
                Email = hotelPostDto.Email,
                Facility = hotelPostDto.Facility,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
