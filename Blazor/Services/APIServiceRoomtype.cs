using System.Net.Http.Json;
using DomainModels;

namespace Blazor.Services;

public partial class APIService
{

    public async Task<RoomtypeGetDto[]> GetRoomtypesAsync(
        int maxItems,
        CancellationToken cancellationToken = default
    )
    {
        List<RoomtypeGetDto>? roomtypes = null;

        await foreach (
            var hotel in _httpClient.GetFromJsonAsAsyncEnumerable<RoomtypeGetDto>(
                "/api/Roomtypes",
                cancellationToken
            )
        )
        {
            if (roomtypes?.Count >= maxItems && maxItems != 0)
            {
                break;
            }
            if (hotel is not null)
            {
                roomtypes ??= [];
                roomtypes.Add(hotel);
            }
        }

        return roomtypes?.ToArray() ?? [];
    }
    public async Task CreateRoomtypeAsync(RoomtypePostDto hotel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/roomtypes", hotel);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"API error: {error}");
        }
    }
    public async Task UpdateRoomtypeAsync(RoomtypePutDto hotel)
    {
        // Example implementation using HttpClient (adjust endpoint and logic as needed)
        var response = await _httpClient.PutAsJsonAsync($"api/roomtypes/{hotel.Id}", hotel);
        response.EnsureSuccessStatusCode();
    }


    public async Task<RoomtypeGetDto[]> GetRoomtypeAsync(
    int hotelId,
    CancellationToken cancellationToken = default
)
    {
        RoomtypeGetDto? hotel = null;

        if (hotelId != 0)
        {
            try
            {
                hotel = await _httpClient.GetFromJsonAsync<RoomtypeGetDto>($"/api/Roomtypes/{hotelId}", cancellationToken);
            }
            catch (HttpRequestException)
            {
                return Array.Empty<RoomtypeGetDto>();
            }
        }

        return hotel is null ? Array.Empty<RoomtypeGetDto>() : new[] { hotel };
    }
}
