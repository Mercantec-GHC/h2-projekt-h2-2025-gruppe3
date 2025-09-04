// Services/APIService.Hotels.cs
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DomainModels;

namespace Blazor.Services
{
    public partial class APIService
    {
        public async Task<HotelGetDto[]> GetHotelsAsync(int maxItems = 3)
        {
            var hotels = await httpClient.GetFromJsonAsync<List<HotelGetDto>>(
                "/api/Hotels");

            if (hotels == null)
                return Array.Empty<HotelGetDto>();

            return hotels.Take(maxItems).ToArray();
        }
    }
}
