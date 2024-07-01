using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IDiscussionRepository : IRepository<DiscussionDto, int>
{
	// Det som är unikt för Discussion lägger vi här
}