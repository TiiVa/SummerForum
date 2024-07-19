using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Discussion.GetById;

public class Handler(IDiscussionRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/discussions/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var discussion = await repo.GetByIdAsync(req.Id);

		if (discussion is null)
		{
			return TypedResults.BadRequest();
		}

		await SendAsync(new Response()
		{
			Discussion = discussion
		}, cancellation: ct);

		return TypedResults.Ok();
	}
}
