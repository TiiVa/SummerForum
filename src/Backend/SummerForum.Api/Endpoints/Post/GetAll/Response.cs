using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Post.GetAll;

public class Response
{
	public IEnumerable<PostDto> Posts { get; set; }
}