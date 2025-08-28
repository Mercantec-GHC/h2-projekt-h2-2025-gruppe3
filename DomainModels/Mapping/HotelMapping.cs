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
                Country = hotel.Country,
                Phone = hotel.Phone,
                Email = hotel.Email,
                PercentagePrice = hotel.PercentagePrice,
                Description = hotel.Description
            };
        }

        public static List<HotelGetDto> ToHotelGetDtos(List<Hotel> hotels)
        {
            return hotels.Select(h => ToHotelGetDto(h)).ToList();
        }

        public static Hotel PostHotelFromDto(HotelPostDto hotelPostDto)
        {
            return new Hotel
            {
                Id = hotelPostDto.Id,
                Name = hotelPostDto.Name,
                Road = hotelPostDto.Road,
                Zip = hotelPostDto.Zip,
                City = hotelPostDto.City,
                Country = hotelPostDto.Country,
                Phone = hotelPostDto.Phone,
                Email = hotelPostDto.Email,
                PercentagePrice = hotelPostDto.PercentagePrice,
                Description = hotelPostDto.Description,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };
        }

        public static Hotel PutHotelFromDto(HotelPutDto hotelPutDto)
        {
            return new Hotel
            {
                Id = hotelPutDto.Id,
                Name = hotelPutDto.Name,
                Road = hotelPutDto.Road,
                Zip = hotelPutDto.Zip,
                City = hotelPutDto.City,
                Country = hotelPutDto.Country,
                Phone = hotelPutDto.Phone,
                Email = hotelPutDto.Email,
                PercentagePrice = hotelPutDto.PercentagePrice,
                Description = hotelPutDto.Description,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
