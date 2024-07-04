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
	await repo.GetByIdAsync(id);
});

app.MapGet("/users", async (IUserRepository repo, int start, int count) => // hämtar från query stringen users?start=0&count=10
{
	return await repo.GetManyAsync(start, count);
});

app.MapPost("/users", async (IUserRepository repo, UserDto user) =>
{
	await repo.AddOneAsync(user);
});


app.Run();
