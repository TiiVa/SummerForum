using System.Runtime.InteropServices.JavaScript;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.Repositories;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.User.Add;

public class Handler(IUserRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Post("/users");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{

		var userToAdd = new UserDto()
		{
			UserName = req.UserName,
			Email = req.Email,
			Password = req.Password,
			IsActive = req.IsActive,
			Posts = req.Posts != null ? req.Posts : new List<PostDto>()
		};

		if (userToAdd is null)
		{
			return TypedResults.BadRequest();
		}

		var allUsers = await repo.GetManyAsync(0, 0);

		if (allUsers.Any(u => u.Id == userToAdd.Id))
		{
			return TypedResults.BadRequest();
		}

		await repo.AddOneAsync(userToAdd);

		return TypedResults.Ok();
	}
}