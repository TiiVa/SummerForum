using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Reply.GetAll;

public class Handler(IReplyRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/replies");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var replies = await repo.GetManyAsync(req.Start, req.Count);

		if (replies is null)
		{
			return TypedResults.NotFound();
		}

		await SendAsync(new Response
		{
			Replies = replies
		}, cancellation: ct);

		return TypedResults.Ok();
	}
}