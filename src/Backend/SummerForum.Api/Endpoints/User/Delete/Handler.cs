using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.User.Delete;

public class Handler(IUserRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Delete("/users/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var allUsers = await repo.GetManyAsync(0, 0);

		if (!allUsers.Any(u => u.Id.Equals(req.Id)))
		{
			return TypedResults.BadRequest();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}
