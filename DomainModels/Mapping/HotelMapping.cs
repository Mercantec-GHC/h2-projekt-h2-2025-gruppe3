namespace DomainModels.Mapping;

// HotelMapping.cs
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
            Description = hotel.Description,
            OpenedAt = hotel.OpenedAt,
            ClosedAt = hotel.ClosedAt,
            CheckInFrom = hotel.CheckInFrom,
            CheckInUntil = hotel.CheckInUntil,
            CheckOutUntil = hotel.CheckOutUntil,
            FacilityId = hotel.FacilityId
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
            Name = hotelPostDto.Name,
            Road = hotelPostDto.Road,
            Zip = hotelPostDto.Zip,
            City = hotelPostDto.City,
            Country = hotelPostDto.Country,
            Phone = hotelPostDto.Phone,
            Email = hotelPostDto.Email,
            PercentagePrice = hotelPostDto.PercentagePrice,
            Description = hotelPostDto.Description,
            OpenedAt = hotelPostDto.OpenedAt,
            ClosedAt = hotelPostDto.ClosedAt,
            CheckInFrom = hotelPostDto.CheckInFrom,
            CheckInUntil = hotelPostDto.CheckInUntil,
            CheckOutUntil = hotelPostDto.CheckOutUntil,
            FacilityId = hotelPostDto.FacilityId,
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
            OpenedAt = hotelPutDto.OpenedAt,
            ClosedAt = hotelPutDto.ClosedAt,
            CheckInFrom = hotelPutDto.CheckInFrom,
            CheckInUntil = hotelPutDto.CheckInUntil,
            CheckOutUntil = hotelPutDto.CheckOutUntil,
            FacilityId = hotelPutDto.FacilityId,
            CreatedAt = DateTime.UtcNow.AddHours(2),
            UpdatedAt = DateTime.UtcNow.AddHours(2)
        };
    }
}
