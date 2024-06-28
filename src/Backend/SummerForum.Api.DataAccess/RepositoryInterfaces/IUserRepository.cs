using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IUserRepository : IRepository<User, int>
{
	// Det som är unikt för User lägger vi här
}