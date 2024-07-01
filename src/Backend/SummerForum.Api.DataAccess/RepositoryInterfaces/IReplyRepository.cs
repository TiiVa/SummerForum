using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IReplyRepository : IRepository<ReplyDto, int>
{
	// Det som är unikt för Reply lägger vi här text GetByEmailAsync(string email);
}