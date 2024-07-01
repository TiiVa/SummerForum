using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class DiscussionRepository(SummerForumDbContext context) : IDiscussionRepository
{
	public async Task<DiscussionDto> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<DiscussionDto>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(DiscussionDto item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(DiscussionDto item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}