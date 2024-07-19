using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Reply.Add;

public class Handler(IReplyRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Post("/replies");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{

		var newReply = new ReplyDto
		{
			Text = req.Text,
			RepliedAt = req.RepliedAt,
			RepliedBy = req.RepliedBy,
			Post = req.Post,
			IsActive = req.IsActive
		};

		if (newReply is null)
		{
			return TypedResults.BadRequest();
		}

		await repo.AddOneAsync(newReply);

		return TypedResults.Ok();
	}

}