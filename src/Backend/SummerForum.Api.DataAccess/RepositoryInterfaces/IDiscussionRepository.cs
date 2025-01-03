using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IDiscussionRepository : IRepository<DiscussionDto, int>
{
	Task<IEnumerable<DiscussionDto>> GetAllByDepartment(int departmentId);
}