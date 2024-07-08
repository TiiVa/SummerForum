using FastEndpoints;
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

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();
