using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.Update;

public class Request
{

	public int Id { get; set; }
	public UserDto User { get; set; }


	// app.MapPut("/users/{id}", async (IUserRepository repo, UserDto user, int id) =>
	// {
	// 	var userToUpdate = await repo.GetByIdAsync(id);
	// 
	// 	if (userToUpdate is null)
	// 	{
	// 		return Results.NotFound($"User with id {id} could not be found");
	// 	}
	// 
	// 	await repo.UpdateOneAsync(user, id);
	// 
	// 	return Results.Ok();
	// 
	// });
}