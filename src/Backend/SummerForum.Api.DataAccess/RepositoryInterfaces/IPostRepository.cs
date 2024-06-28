using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IPostRepository : IRepository<Post, int>
{
	// Det som är unikt för Post lägger vi här
}