using System.Reflection.Metadata;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

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

		var userToReturn = new UserDto
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
				StartedAt = p.StartedAt,
				IsActive = p.IsActive,
				 

				
				Replies = p.Replies.Select(r => new ReplyDto
				{
					Id = r.Id,
					Text = r.Text,
					RepliedAt = r.RepliedAt,
					IsActive = r.IsActive
				}).ToList()
			}).ToList()
			
		};
		return userToReturn;
	}

	public async Task<IEnumerable<UserDto>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(UserDto item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(UserDto item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}