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
		var user = await context.Users.FindAsync(id); 
			
		
		if (user is null)
		{
			return new UserDto();
		}

		return new UserDto
		{
			Id = user.Id,
			UserName = user.UserName,
			Email = user.Email,
			Password = user.Password,
			IsActive = user.IsActive,
			Posts = user.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt
			}).ToList()
		};

		
	}

	public async Task<IEnumerable<UserDto>> GetManyAsync(int start, int count)
	{
		var users = await context.Users.Skip(start).Take(count).ToListAsync();

		var usersToReturn = users.Select(u => new UserDto
		{
			Id = u.Id,
			UserName = u.UserName,
			Email = u.Email,
			Password = u.Password,
			IsActive = u.IsActive,
			Posts = u.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt
			}).ToList()
		}).ToList();

		return usersToReturn;
	}

	public async Task AddOneAsync(UserDto item)
	{
		var userToAdd = new User
		{
			UserName = item.UserName,
			Email = item.Email,
			Password = item.Password
		};

		await context.Users.AddAsync(userToAdd);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(UserDto item)
	{
		var oldUser = await context.Users.FindAsync(item.Id);

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
		
		context.Users.Remove(userToDelete);
		await context.SaveChangesAsync();
	}
}