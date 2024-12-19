using SummerForum.Client.Services.ServiceInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public class ReplyService : IReplyService
{
	private readonly HttpClient _httpClient;

	public ReplyService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("SummerForumApi");
	}

	public async Task<ReplyDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"replies/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new ReplyDto();
		}

		var result = await response.Content.ReadFromJsonAsync<ReplyDto>();
		return result;
	}

	public async Task<IEnumerable<ReplyDto>> GetManyAsync(int start, int count)
	{
		var response = await _httpClient.GetAsync($"/replies?start={start}&count={count}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ReplyDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<ReplyDtoList>();
		return result.Replies ?? Enumerable.Empty<ReplyDto>();
	}

	public async Task AddOneAsync(ReplyDto item)
	{
		var response = await _httpClient.PostAsJsonAsync("/replies", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateOneAsync(ReplyDto item, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/replies/{id}", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task DeleteOneAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/replies/{id}");
		
		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}
}