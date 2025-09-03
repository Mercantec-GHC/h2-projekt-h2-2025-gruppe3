using System.Net.Http.Json;
using DomainModels;

namespace Blazor.Services
{
	public partial class APIService
	{
		public async Task<RoomGetDto[]> GetRoomsAsync(

   CancellationToken cancellationToken = default
   )
		{
			List<RoomGetDto>? rooms = null;

			await foreach (
				var room in httpClient.GetFromJsonAsAsyncEnumerable<RoomGetDto>(
					"/api/rooms",
					cancellationToken
				)
			)
			{
				if (room is not null)
				{
					rooms ??= [];
					rooms.Add(room);
				}
			}

			return rooms?.ToArray() ?? [];
		}
	}
}
