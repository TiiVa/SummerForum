using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.Add;

public class Request
{
	//public UserDto User { get; set; }

	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public List<PostDto> Posts { get; set; } = new();
	public bool IsActive { get; set; }

	//app.MapPost("/users", async (IUserRepository repo, UserDto user) =>
	//{
	//	await repo.AddOneAsync(user);
	//});
}