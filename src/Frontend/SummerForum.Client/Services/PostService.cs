using SummerForum.Client.Services.ServiceInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public class PostService : IPostService
{
	private readonly HttpClient _httpClient;

	public PostService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("SummerForumApi");
	}
	public async Task<PostDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"posts/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new PostDto();
		}

		var result = await response.Content.ReadFromJsonAsync<PostDto>();
		return result;
	}

	public async Task<IEnumerable<PostDto>> GetManyAsync(int start, int count)
	{
		var response = await _httpClient.GetAsync($"posts?start={start}&count={count}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<PostDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<PostDtoList>();
		return result.Posts ?? Enumerable.Empty<PostDto>();
	}

	public async Task AddOneAsync(PostDto item)
	{
		var response = await _httpClient.PostAsJsonAsync("/posts", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}

	}

	public async Task UpdateOneAsync(PostDto item, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/posts/{id}", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}

	}

	public async Task DeleteOneAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/posts/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}
}