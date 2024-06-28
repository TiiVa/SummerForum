using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess.Repositories;

public class PostRepository : IPostRepository
{
	public async Task<Post> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Post>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(Post item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(Post item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}