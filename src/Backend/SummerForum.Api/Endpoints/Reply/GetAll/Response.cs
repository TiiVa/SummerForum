using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Reply.GetAll;

public class Response
{
	public IEnumerable<ReplyDto> Replies { get; set; }
}