using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IDiscussionRepository : IRepository<Discussion, int>
{
	// Det som är unikt för Discussion lägger vi här
}