using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Post.Delete;

public class Handler(IPostRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Delete("/posts/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var postToDelete = await repo.GetByIdAsync(req.Id);

		if (postToDelete is null)
		{
			return TypedResults.NotFound();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}	