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
                Address = hotel.Address
            };
        }
        public static Hotel ToHotelFromDto(HotelPostDto HotelPostDto)
        {
            return new Hotel
            {
                Id = Guid.NewGuid().ToString(),
                Name = HotelPostDto.Name,
                Address = HotelPostDto.Address,
                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2),
            };
        }
    }
}
