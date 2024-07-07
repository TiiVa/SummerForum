using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SummerForumDb");

builder.Services.AddDbContext<SummerForumDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDataAccess();

var app = builder.Build();

app.MapGet("/users/{id}", async (IUserRepository repo, int id) =>
{
	
	return await repo.GetByIdAsync(id);
	
});

app.MapGet("/users", async (IUserRepository repo, int start, int count) => // hämtar från query stringen users?start=0&count=10
{
	return await repo.GetManyAsync(start, count);
});

app.MapPost("/users", async (IUserRepository repo, UserDto user) =>
{
	await repo.AddOneAsync(user);
});

app.MapPut("/users/{id}", async (IUserRepository repo, UserDto user, int id) =>
{
	var userToUpdate = await repo.GetByIdAsync(id);

	if (userToUpdate is null)
	{
		return Results.NotFound($"User with id {id} could not be found");
	}

	await repo.UpdateOneAsync(user, id);

	return Results.Ok();

});

app.MapDelete("/users/{id}", async (IUserRepository repo, int id) =>
{
	var userToDelete = await repo.GetByIdAsync(id);

	if (userToDelete is null)
	{
		return Results.NotFound($"User with id {id} could not be found");
	}

	await repo.DeleteOneAsync(id);

	return Results.Ok();
});


app.Run();
