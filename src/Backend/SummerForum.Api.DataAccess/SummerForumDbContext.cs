using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;

namespace SummerForum.Api.DataAccess;

public class SummerForumDbContext : DbContext
{
	public DbSet<Department> Departments { get; set; }
	public DbSet<Discussion> Discussions { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Reply> Replies { get; set; }
	public DbSet<User> Users { get; set; }

	public SummerForumDbContext(DbContextOptions<SummerForumDbContext> options) : base(options)
	{

	}
}