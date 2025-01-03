using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services.ServiceInterfaces;

public interface IDiscussionService : IService<DiscussionDto, int>
{
	Task<IEnumerable<DiscussionDto>> GetAllByDepartment(int departmentId);
}