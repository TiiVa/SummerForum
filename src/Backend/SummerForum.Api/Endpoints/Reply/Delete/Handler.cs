using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Reply.Delete;

public class Handler(IReplyRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Delete("/replies/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var reply = await repo.GetByIdAsync(req.Id);

		if (reply is null)
		{
			return TypedResults.BadRequest();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}
