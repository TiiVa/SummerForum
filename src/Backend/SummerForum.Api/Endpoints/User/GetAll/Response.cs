using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.GetAll;

public class Response
{
	public IEnumerable<UserDto> Users { get; set; }
}