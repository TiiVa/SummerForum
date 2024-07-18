using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Discussion.GetAll;

public class Response
{
	public IEnumerable<DiscussionDto> Discussions { get; set; }
}