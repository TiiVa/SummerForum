using Microsoft.Extensions.DependencyInjection;
using SummerForum.Api.DataAccess.Repositories;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		services.AddScoped<IDepartmentRepository, DepartmentRepository>();
		services.AddScoped<IDiscussionRepository, DiscussionRepository>();
		services.AddScoped<IPostRepository, PostRepository>();
		services.AddScoped<IReplyRepository, ReplyRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}
}