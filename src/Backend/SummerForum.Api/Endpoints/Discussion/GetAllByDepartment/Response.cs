using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Discussion.GetAllByDepartment;

public class Response
{
	public IEnumerable<DiscussionDto> DiscussionsByDepartment { get; set; } = [];
}