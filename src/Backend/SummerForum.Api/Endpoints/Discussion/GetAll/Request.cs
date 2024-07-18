using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Discussion.GetAll;

public class Request
{
	public int Start { get; set; }
	public int Count { get; set; }
}