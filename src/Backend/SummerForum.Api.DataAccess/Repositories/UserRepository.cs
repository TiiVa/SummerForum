using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
	public async Task<User> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<User>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(User item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(User item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}