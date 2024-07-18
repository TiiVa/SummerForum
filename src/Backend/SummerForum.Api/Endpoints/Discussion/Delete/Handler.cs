using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Discussion.Delete;

public class Handler(IDiscussionRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Delete("/discussions/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var discussionToDelete = await repo.GetByIdAsync(req.Id);

		if (discussionToDelete == null)
		{
			return TypedResults.NotFound();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}