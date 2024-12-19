using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SummerForum.Api.DataAccess.Repositories;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
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
		services.AddScoped<UnitOfWork>();
		

		return services;
	}
}