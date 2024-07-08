namespace SummerForum.Api.Endpoints.User.Delete;

public class Request
{
	public int Id { get; set; }

	//app.MapDelete("/users/{id}", async (IUserRepository repo, int id) =>
	// {
	// 	var userToDelete = await repo.GetByIdAsync(id);
	// 
	// 	if (userToDelete is null)
	// 	{
	// 		return Results.NotFound($"User with id {id} could not be found");
	// 	}
	// 
	// 	await repo.DeleteOneAsync(id);
	// 
	// 	return Results.Ok();
	// });
}