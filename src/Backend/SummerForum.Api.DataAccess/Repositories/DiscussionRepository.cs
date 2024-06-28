using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess.Repositories;

public class DiscussionRepository : IDiscussionRepository
{
	public async Task<Discussion> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Discussion>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(Discussion item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(Discussion item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}