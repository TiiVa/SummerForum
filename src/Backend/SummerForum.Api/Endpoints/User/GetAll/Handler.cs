using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.User.GetAll;

public class Handler(IUserRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/users");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var users = await repo.GetManyAsync(req.Start, req.Count);

		await SendAsync(new Response()
		{
			Users = users
		});

	}
}