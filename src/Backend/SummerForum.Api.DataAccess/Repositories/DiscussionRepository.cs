using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class DiscussionRepository(SummerForumDbContext context) : IDiscussionRepository
{
	public async Task<DiscussionDto> GetByIdAsync(int id)
	{
		var discussion = await context.Discussions
			.Include(d => d.Posts)
			.ThenInclude(p => p.StartedBy)  // Include the StartedBy property
			.Include(d => d.Posts)
			.ThenInclude(p => p.Replies)  // Include Replies
			.Where(d => d.Id == id)
			.FirstOrDefaultAsync();


		if (discussion is null)
		{
			return new DiscussionDto();
		}

		var discussionToReturn = new DiscussionDto
		{
			Id = discussion.Id,
			Description = discussion.Description,
			IsActive = discussion.IsActive,
			Posts = discussion.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt,
				StartedBy = new UserDto
				{
					Id = p.StartedBy.Id,
					UserName = p.StartedBy.UserName,
					Email = p.StartedBy.Email,
					Password = p.StartedBy.Password,
					IsActive = p.StartedBy.IsActive
				},
				Replies = p.Replies.Select(r => new ReplyDto
				{
					Id = r.Id,
					Text = r.Text,
					RepliedAt = r.RepliedAt,
					IsActive = r.IsActive
				}).ToList()
			}).ToList()
			
		};

		return discussionToReturn;
	}

	public async Task<IEnumerable<DiscussionDto>> GetManyAsync(int start, int count)
	{
		var discussions = await context.Discussions
			.Include(p => p.Posts)
			.ThenInclude(r => r.Replies)
			.Skip(0)
			.Take(0)
			.ToListAsync();

		var discussionsToReturn = discussions.Select(d => new DiscussionDto
		{
			Id = d.Id,
			Description = d.Description,
			IsActive = d.IsActive,
			Posts = d.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt,
				StartedBy = new UserDto
				{
					Id = p.StartedBy.Id,
					UserName = p.StartedBy.UserName,
					Email = p.StartedBy.Email,
					Password = p.StartedBy.Password,
					IsActive = p.StartedBy.IsActive
				},
				Replies = p.Replies.Select(r => new ReplyDto
				{
					Id = r.Id,
					Text = r.Text,
					RepliedAt = r.RepliedAt,
					IsActive = r.IsActive
				}).ToList()
			}).ToList()
		}).ToList();

		return discussionsToReturn;
	
	}

	public async Task AddOneAsync(DiscussionDto item)
	{
		var department = await context.Departments.FindAsync(item.Department.Id);

		var newDiscussion = new Discussion
		{
			Description = item.Description,
			IsActive = item.IsActive,
			Department = department,
			Posts = null
		};

		await context.Discussions.AddAsync(newDiscussion);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(DiscussionDto item)
	{
		var oldDiscussion = await context.Discussions.FindAsync(item.Id);

		if (oldDiscussion is null)
		{
			return;
		}

		oldDiscussion.Description = item.Description;
		oldDiscussion.IsActive = item.IsActive;
		oldDiscussion.Department = await context.Departments.FindAsync(item.Department.Id);

		
		await context.SaveChangesAsync();
	}

	public async Task DeleteOneAsync(int id)
	{
		var discussionToDelete = await context.Discussions.FindAsync(id);

		if (discussionToDelete is null)
		{
			return;
		}

		context.Discussions.Remove(discussionToDelete);
		await context.SaveChangesAsync();
	}
}