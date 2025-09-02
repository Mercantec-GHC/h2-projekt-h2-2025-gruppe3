using System.Net.Http.Json;
using DomainModels;

namespace Blazor.Services;

public partial class APIService
{
    //public async Task<HotelGetDto[]> GetHotelsAsync(int maxItems = 3)
    //{
    //    var hotels = await _httpClient.GetFromJsonAsync<List<HotelGetDto>>(
    //        "/api/Hotels");

    //    if (hotels == null)
    //        return Array.Empty<HotelGetDto>();

    //    return hotels.Take(maxItems).ToArray();
    //}

    public async Task<HotelGetDto[]> GetHotelsAsync(
    int maxItems = 3,
    CancellationToken cancellationToken = default
    )
    {
        List<HotelGetDto>? hotels = null;

        await foreach (
            var hotel in httpClient.GetFromJsonAsAsyncEnumerable<HotelGetDto>(
                "/api/Hotels",
                cancellationToken
            )
        )
        {
            if (hotels?.Count >= maxItems)
            {
                break;
            }
            if (hotel is not null)
            {
                hotels ??= [];
                hotels.Add(hotel);
            }
        }

        return hotels?.ToArray() ?? [];
    }
}
