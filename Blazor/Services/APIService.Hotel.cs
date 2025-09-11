using System.Net.Http.Json;
using DomainModels;

namespace Blazor.Services;

public partial class APIService
{

    public async Task<HotelGetDto[]> GetHotelsAsync(
        int maxItems,
        CancellationToken cancellationToken = default
    )
    {
        List<HotelGetDto>? hotels = null;

        await foreach (
            var hotel in _httpClient.GetFromJsonAsAsyncEnumerable<HotelGetDto>(
                "/api/Hotels",
                cancellationToken
            )
        )
        {
            if (hotels?.Count >= maxItems && maxItems != 0)
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
