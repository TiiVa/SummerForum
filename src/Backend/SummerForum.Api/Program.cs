using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SummerForumDb");

builder.Services.AddDbContext<SummerForumDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDataAccess();

var app = builder.Build();

app.MapGet("/users{id}", async (IUserRepository repo, int id) =>
{
	await repo.GetByIdAsync(id);
});

app.MapGet("/users", async (IUserRepository repo, int start, int count) => // hämtar från query stringen
{
	return await repo.GetManyAsync(start, count);
});


app.Run();
