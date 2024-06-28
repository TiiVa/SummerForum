using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess.Repositories;

public class ReplyRepository : IReplyRepository
{
	public async Task<Reply> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Reply>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(Reply item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(Reply item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}