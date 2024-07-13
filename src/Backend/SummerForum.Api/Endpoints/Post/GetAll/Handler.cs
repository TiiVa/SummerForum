using FastEndpoints;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Post.GetAll;

public class Handler(IPostRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/posts");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var posts = await repo.GetManyAsync(req.Start, req.Count);

		await SendAsync(new Response()
		{
			Posts = posts
		}, cancellation : ct);
	}
}
