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
			.Where(d => d.IsActive && d.Id == id)
			.Include(d => d.Posts)
			.ThenInclude(p => p.StartedBy).Include(d => d.Posts).ThenInclude(post => post.Replies)
			.Include(d => d.Department)
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
			Department = new DepartmentDto
			{
				Id = discussion.Department.Id,
				Description = discussion.Department.Description
			},
			Posts = discussion.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Description = p.Description,
				Text = p.Text,
				StartedAt = p.StartedAt,
				StartedBy = p.StartedBy.Id,
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
			.Where(d => d.IsActive == true)
			.Include(p => p.Posts).ThenInclude(u => u.StartedBy)
			.Include(d => d.Department)
			.Include(discussion => discussion.Posts).ThenInclude(post => post.Replies).ToListAsync();

		var discussionsToReturn = discussions.Select(d => new DiscussionDto
		{
			Id = d.Id,
			Description = d.Description,
			IsActive = d.IsActive,
			Department = new DepartmentDto
			{
				Id = d.Department.Id,
				Description = d.Department.Description
			},
			Posts = d.Posts.Select(p => new PostDto
			{
				Id = p.Id,
				Text = p.Text,
				StartedAt = p.StartedAt,
				Description = p.Description,
				StartedBy = p.StartedBy.Id,
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
			IsActive = true,
			Department = department,
			Posts = item.Posts != null ? item.Posts.Select(p => new Post
			{
				Text = p.Text,
				StartedAt = p.StartedAt,
				StartedBy = context.Users.Find(p.StartedBy)
			}).ToList() : new List<Post>()
		};

		await context.Discussions.AddAsync(newDiscussion);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(DiscussionDto item, int id)
	{
		var oldDiscussion = await context.Discussions.FindAsync(id);

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

		var entityEntry = context.Discussions.Update(discussionToDelete);
		entityEntry.Property(d => d.IsActive).CurrentValue = false;

		await context.SaveChangesAsync();
	}
}