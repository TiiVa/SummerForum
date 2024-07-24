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

		userToUpdate.UserName = req.UserName;
		userToUpdate.Password = req.Password;
		userToUpdate.Email = req.Email;
		userToUpdate.IsActive = req.IsActive;
		userToUpdate.Posts = req.Posts;
		userToUpdate.Role = req.Role;

		await repo.UpdateOneAsync(userToUpdate, req.Id);

		return TypedResults.Ok();
	}
}