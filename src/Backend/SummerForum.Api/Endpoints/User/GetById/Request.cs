using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.GetById;

public class Request
{
	public int Id { get; set; }


	//app.MapGet("/users/{id}", async (IUserRepository repo, int id) =>
	// {
	// 
	// 	return await repo.GetByIdAsync(id);
	// 
	// });
}