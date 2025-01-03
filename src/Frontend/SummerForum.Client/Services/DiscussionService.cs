using SummerForum.Client.Services.ServiceInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Client.Services;

public class DiscussionService : IDiscussionService
{
	private readonly HttpClient _httpClient;

	public DiscussionService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("SummerForumApi");
	}

	public async Task<DiscussionDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"discussions/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new DiscussionDto();
		}

		var result = await response.Content.ReadFromJsonAsync<DiscussionDto>();
		return result;
	}

	public async Task<IEnumerable<DiscussionDto>> GetManyAsync(int start, int count)
	{
		var response = await _httpClient.GetAsync($"/discussions?start={start}&count={count}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<DiscussionDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<DiscussionDtoList>();
		return result.Discussions ?? Enumerable.Empty<DiscussionDto>();
	}

	public async Task<IEnumerable<DiscussionDto>> GetAllByDepartment(int departmentId)
	{
		var response = await _httpClient.GetAsync($"/discussions/departments/{departmentId}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<DiscussionDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<DiscussionDtoListByDepartment>();
		return result.DiscussionsByDepartment ?? Enumerable.Empty<DiscussionDto>();
	}

	public async Task AddOneAsync(DiscussionDto item)
	{
		var response = await _httpClient.PostAsJsonAsync("/discussions", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateOneAsync(DiscussionDto item, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/discussions/{id}", item);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task DeleteOneAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/discussions/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}
}