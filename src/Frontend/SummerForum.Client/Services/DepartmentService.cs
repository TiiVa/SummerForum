using SummerForum.Client.Services.ServiceInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public class DepartmentService : IDepartmentService
{
	private readonly HttpClient _httpClient;

	public DepartmentService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("SummerForumApi");
	}

	public async Task<DepartmentDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"departments/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new DepartmentDto();
		}

		var result = await response.Content.ReadFromJsonAsync<DepartmentDto>();
		return result;
	}

	public async Task<IEnumerable<DepartmentDto>> GetManyAsync(int start, int count)
	{
		var response = await _httpClient.GetAsync($"/departments?start={start}&count={count}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<DepartmentDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<DepartmentDtoList>();
		return result.Departments ?? Enumerable.Empty<DepartmentDto>();
	}

	public async Task AddOneAsync(DepartmentDto item)
	{
		var response = await _httpClient.PostAsJsonAsync("/departments", item);
		
		if (!response.IsSuccessStatusCode)
		{
			return;
		}

	}

	public async Task UpdateOneAsync(DepartmentDto item, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/departments/{id}", item);
		
		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task DeleteOneAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/departments/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}
}