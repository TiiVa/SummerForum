using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.User.GetById;

public class Handler(IUserRepository repo) : Endpoint<Request, Results<Ok<Response>, NotFound>>
{
	public override void Configure()
	{
		Get("/users/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok<Response>, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var user = await repo.GetByIdAsync(req.Id);

		if (user is null)
		{
			return TypedResults.NotFound();
		}

		return TypedResults.Ok(new Response
		{
			User = user
		});
	}
}