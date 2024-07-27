using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public class UserService : IService<UserDto, int>
{
	private readonly HttpClient _httpClient;

	public UserService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("SummerForumApi");
	}

	public async Task<UserDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new UserDto();
		}

		var result = await response.Content.ReadFromJsonAsync<UserDto>();
		return result;
	}

	public async Task<IEnumerable<UserDto>> GetManyAsync(int start, int count)
	{
		var response = await _httpClient.GetAsync("/users");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<UserDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<UserDto>>();
		return result ?? Enumerable.Empty<UserDto>();
	}

	public async Task AddOneAsync(UserDto item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(UserDto item, int id)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}