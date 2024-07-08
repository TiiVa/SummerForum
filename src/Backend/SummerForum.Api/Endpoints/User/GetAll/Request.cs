namespace SummerForum.Api.Endpoints.User.GetAll;

public class Request
{
	public int Start { get; set; }
	public int Count { get; set; }

	//app.MapGet("/users", async (IUserRepository repo, int start, int count) => // hämtar från query stringen users?start=0&count=10
	// {
	// 	return await repo.GetManyAsync(start, count);
	// });
}