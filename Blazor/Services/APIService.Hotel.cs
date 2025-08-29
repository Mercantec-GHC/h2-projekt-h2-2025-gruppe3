using DomainModels;
using System.Net.Http.Json;

namespace Blazor.Services
{
	public partial class APIService
	{

		public async Task<HotelGetDto?> GetHotelAsync(int id)
		{
			return await httpClient.GetFromJsonAsync<HotelGetDto>($"api/Hotels/{id}");
		}

	}
}