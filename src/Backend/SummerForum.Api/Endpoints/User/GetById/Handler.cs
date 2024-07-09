using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.User.GetById;

public class Handler(IUserRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/users/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok,NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var user = await repo.GetByIdAsync(req.Id);

		if (user == null)
		{
			return TypedResults.NotFound();
		}

		await SendAsync(new Response
		{
			User = user
		}, cancellation: ct);

		return TypedResults.Ok();
	}
}