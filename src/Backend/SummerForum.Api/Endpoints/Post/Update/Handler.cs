using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Post.Update;

public class Handler(IPostRepository repo) : Endpoint<Request, Results<Ok, BadRequest>>
{
	public override void Configure()
	{
		Put("/posts/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var postToUpdate = await repo.GetByIdAsync(req.Id);

		if (postToUpdate == null)
		{
			return TypedResults.NotFound();
		}

		postToUpdate.Description = req.Description;
		postToUpdate.StartedBy = req.StartedBy;
		postToUpdate.StartedAt = req.StartedAt;
		postToUpdate.Text = req.Text;
		postToUpdate.Replies = req.Replies;
		postToUpdate.Discussion = req.Discussion;
		postToUpdate.IsActive = req.IsActive;

		await repo.UpdateOneAsync(postToUpdate, req.Id);

		return TypedResults.Ok();
	}
}