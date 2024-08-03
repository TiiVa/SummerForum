using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public class UserService : IUserService
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

	public async Task<UserDto?> GetByNameAsync(string name)
	{
		var response = await _httpClient.GetAsync($"users/userName/{name}");

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
		var response = await _httpClient.PostAsJsonAsync("/users", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateOneAsync(UserDto item, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/users/{id}", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task DeleteOneAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<UserDto> LoginAsync(UserDto user)
    {
        var response = await _httpClient.PostAsJsonAsync("/login", user);

        if (!response.IsSuccessStatusCode)
        {
            return new UserDto();
        }

        var result = await response.Content.ReadFromJsonAsync<UserDto>();
        return result;
    }
}