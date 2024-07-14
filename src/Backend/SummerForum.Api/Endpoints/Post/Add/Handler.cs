using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Post.Add;

public class Handler(IPostRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Post("/posts");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var newPost = new PostDto()
		{
			Description = req.Description,
			StartedBy = req.StartedBy,
			StartedAt = req.StartedAt,
			Text = req.Text,
			Discussion = req.Discussion,
			IsActive = req.IsActive,
			Replies = req.Replies
		};

		if (newPost == null)
		{
			return TypedResults.BadRequest();
		}

		await repo.AddOneAsync(newPost);

		return TypedResults.Ok();

	}
}