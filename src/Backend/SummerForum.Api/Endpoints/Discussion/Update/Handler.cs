using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Discussion.Update;

public class Handler(IDiscussionRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Put("/discussions/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var discussionToUpdate = await repo.GetByIdAsync(req.Id);

		if (discussionToUpdate is null)
		{
			return TypedResults.NotFound();
		}

		discussionToUpdate.Description = req.Description;
		discussionToUpdate.IsActive = req.IsActive;
		discussionToUpdate.Department = req.Department;

		await repo.UpdateOneAsync(discussionToUpdate, req.Id);
		
		return TypedResults.Ok();
	}
}
