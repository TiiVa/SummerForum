using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SummerForumDb");

builder.Services.AddDbContext<SummerForumDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDataAccess();

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();
