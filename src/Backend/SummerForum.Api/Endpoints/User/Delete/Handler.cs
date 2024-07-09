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
		var userToDelete = await repo.GetByIdAsync(req.Id);

		if (userToDelete is null)
		{
			return TypedResults.BadRequest();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}
