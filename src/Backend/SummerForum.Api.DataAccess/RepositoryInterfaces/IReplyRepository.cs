using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IReplyRepository : IRepository<Reply, int>
{
	// Det som är unikt för Reply lägger vi här text GetByEmailAsync(string email);
}