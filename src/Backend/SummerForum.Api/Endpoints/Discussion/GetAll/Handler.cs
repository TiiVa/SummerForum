using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Discussion.GetAll;

public class Handler(IDiscussionRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/discussions");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var discussions = await repo.GetManyAsync();

		if (discussions is null)
		{
			return TypedResults.BadRequest();
		}

		await SendAsync(new Response()
		{
			Discussions = discussions
		}, cancellation: ct);

		return TypedResults.Ok();

	}

	
}