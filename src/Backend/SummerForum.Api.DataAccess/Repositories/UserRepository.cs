using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Repositories;

public class UserRepository(SummerForumDbContext context) : IUserRepository
{
	public async Task<UserDto> GetByIdAsync(int id)
	{
		var user = await context.Users.Include(u => u.Posts).FirstOrDefaultAsync(c => c.Id == id);		
		
		if (user is null)
		{
			return new UserDto();
		}

		var posts = new List<PostDto>();

		foreach (var post in user.Posts)
		{
			var getPost = await context.Posts.Include(u => u.StartedBy).FirstOrDefaultAsync(p => p.Id == post.Id);

			posts.Add(new PostDto
			{
				Id = getPost.Id,
				Text = getPost.Text,
				StartedAt = getPost.StartedAt,
				StartedBy = getPost.StartedBy.Id,

			});
		}

		var userById = new UserDto
		{
			Id = user.Id,
			UserName = user.UserName,
			Email = user.Email,
			Password = user.Password,
			IsActive = user.IsActive,
			Posts = posts
		};

		return userById;

	}

	public async Task<IEnumerable<UserDto>> GetManyAsync(int start, int count)
	{
		var users = await context.Users.Where(u => u.IsActive == true).Skip(start).Take(count).ToListAsync();
		var usersToReturn = users.Select(u => new UserDto
		{
			Id = u.Id,
			UserName = u.UserName,
			Email = u.Email,
			Password = u.Password,
			IsActive = u.IsActive,
			Posts = (u.Posts != null ? u.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt,
				Replies = (p.Replies != null ? p.Replies.Select(r => new ReplyDto
				{
					Id = r.Id,
					Text = r.Text,
					RepliedAt = r.RepliedAt,
				}).ToList() : new List<ReplyDto>())
			}).ToList() : new List<PostDto>())
		}).ToList();


		return usersToReturn;
	}

	public async Task AddOneAsync(UserDto item)
	{
		var userToAdd = new User
		{
			UserName = item.UserName,
			Email = item.Email,
			Password = item.Password,
			IsActive = item.IsActive

		};

		await context.Users.AddAsync(userToAdd);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(UserDto item, int id)
	{
		var oldUser = await context.Users.FindAsync(id);

		if (oldUser is null)
		{
			return;
		}

		
		oldUser.UserName = item.UserName;
		oldUser.Email = item.Email;
		oldUser.Password = item.Password;
		oldUser.IsActive = item.IsActive;
		
		await context.SaveChangesAsync();
	}

	public async Task DeleteOneAsync(int id)
	{
		var userToDelete = await context.Users.FindAsync(id);

		if (userToDelete is null)
		{
			return;
		}
		
		var entityEntry = context.Users.Update(userToDelete);
		entityEntry.Property(u => u.IsActive).CurrentValue = false;

		await context.SaveChangesAsync();
	}
}