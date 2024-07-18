using FastEndpoints;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Post.GetById;

public class Handler(IPostRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/posts/{id}");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var post = await repo.GetByIdAsync(req.Id);

		await SendAsync(new Response()
		{
			Post = post
		}, cancellation: ct);

	}
}
