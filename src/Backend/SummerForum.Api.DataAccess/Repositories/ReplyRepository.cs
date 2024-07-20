using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class ReplyRepository(SummerForumDbContext context) : IReplyRepository
{
	public async Task<ReplyDto> GetByIdAsync(int id) 
	{
		var reply = await context.Replies.Include(r => r.RepliedBy)
			.Include(r => r.Post)
			.FirstOrDefaultAsync(r => r.Id == id);

		if (reply is null)
		{
			return new ReplyDto();
		}

		var replyToReturn = new ReplyDto
		{
			Id = reply.Id,
			Text = reply.Text,
			RepliedBy = reply.RepliedBy.Id,
			RepliedAt = reply.RepliedAt,
			IsActive = reply.IsActive,
			Post = reply.Post.Id

		};

		return replyToReturn;
		
	}

	public async Task<IEnumerable<ReplyDto>> GetManyAsync(int start, int count)
	{
		var replies = await context.Replies
			.Include(u => u.RepliedBy)
			.Include(r => r.Post).Skip(start).Take(count).ToListAsync();
		
		var repliesToReturn = replies.Select(r => new ReplyDto
		{
			Id = r.Id,
			Text = r.Text,
			RepliedBy = r.RepliedBy.Id,
			RepliedAt = r.RepliedAt,
			IsActive = r.IsActive,
			Post = r.Post.Id
		}).ToList();

		return repliesToReturn;
	}

	public async Task AddOneAsync(ReplyDto item)
	{
		var repliedBy = await context.Users.FindAsync(item.RepliedBy);
		var post = await context.Posts.FindAsync(item.Post);

		var reply = new Reply
		{
			Text = item.Text,
			RepliedBy = repliedBy,
			RepliedAt = item.RepliedAt,
			IsActive = item.IsActive,
			Post = post
		};

		await context.Replies.AddAsync(reply);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(ReplyDto item, int id)
	{
		var oldReply = await context.Replies.FindAsync(id);

		if (oldReply is null)
		{
			return;
		}

		oldReply.Text = item.Text;
		oldReply.RepliedBy = await context.Users.FindAsync(item.RepliedBy);
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

		var entityEntry = context.Replies.Remove(replyToDelete);
		entityEntry.Property(r => r.IsActive).CurrentValue = false;

		await context.SaveChangesAsync();
	}
}