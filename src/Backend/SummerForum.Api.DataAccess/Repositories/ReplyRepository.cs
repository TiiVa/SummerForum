using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class ReplyRepository(SummerForumDbContext context) : IReplyRepository
{
	public async Task<ReplyDto> GetByIdAsync(int id)
	{
		var reply = await context.Replies.FindAsync(id);

		if (reply is null)
		{
			return new ReplyDto();
		}

		var replyToReturn = new ReplyDto
		{
			Id = reply.Id,
			Text = reply.Text,
			RepliedBy = new UserDto
			{
				Id	= reply.RepliedBy.Id,
				UserName = reply.RepliedBy.UserName,
				Email = reply.RepliedBy.Email,
				Password = reply.RepliedBy.Password,
				IsActive = reply.RepliedBy.IsActive

			},
			RepliedAt = reply.RepliedAt,
			IsActive = reply.IsActive

		};

		return replyToReturn;
		
	}

	public async Task<IEnumerable<ReplyDto>> GetManyAsync(int start, int count)
	{
		var replies = await context.Replies.Skip(0).Take(0).ToListAsync();
		
		var repliesToReturn = replies.Select(r => new ReplyDto
		{
			Id = r.Id,
			Text = r.Text,
			RepliedBy = new UserDto
			{
				Id = r.RepliedBy.Id,
				UserName = r.RepliedBy.UserName,
				Email = r.RepliedBy.Email,
				Password = r.RepliedBy.Password,
				IsActive = r.RepliedBy.IsActive
			},
			RepliedAt = r.RepliedAt,
			IsActive = r.IsActive
		}).ToList();

		return repliesToReturn;
	}

	public async Task AddOneAsync(ReplyDto item)
	{
		var reply = new Reply
		{
			Text = item.Text,
			RepliedBy = await context.Users.FindAsync(item.RepliedBy.Id),
			RepliedAt = item.RepliedAt,
			IsActive = item.IsActive
		};

		await context.Replies.AddAsync(reply);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(ReplyDto item)
	{
		var oldReply = await context.Replies.FindAsync(item.Id);

		if (oldReply is null)
		{
			return;
		}

		oldReply.Text = item.Text;
		oldReply.RepliedBy = await context.Users.FindAsync(item.RepliedBy.Id);
		oldReply.RepliedAt = item.RepliedAt;
		oldReply.IsActive = item.IsActive;

		await context.SaveChangesAsync();
	}

	public async Task DeleteOneAsync(int id)
	{
		var replyToDelete = await context.Replies.FindAsync(id);

		if (replyToDelete is null)
		{
			return;
		}

		context.Replies.Remove(replyToDelete);
		await context.SaveChangesAsync();
	}
}