using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Reply.GetById;

public class Handler(IReplyRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{	
		Get("/replies/{id}");
		AllowAnonymous();
	}	

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var replyToReturn = await repo.GetByIdAsync(req.Id);

		if(replyToReturn is null)
		{
			return TypedResults.NotFound();
		}

		await SendAsync(new Response()
		{
			Reply = replyToReturn
		});

		return TypedResults.Ok();
	}
}