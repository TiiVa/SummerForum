using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.Update;

public class Handler(IUserRepository repo) : Endpoint<Request, Results<Ok, NotFound>>
{
	public override void Configure()
	{
		Put("/users/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var userToUpdate = await repo.GetByIdAsync(req.Id);

		if (userToUpdate is null)
		{
			return TypedResults.NotFound();
		}


		await repo.UpdateOneAsync(req.User, req.Id); // behöver skapa ny Dto för att kunna uppdatera?

		return TypedResults.Ok();
	}
}