using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class PostRepository(SummerForumDbContext context) : IPostRepository
{
	public async Task<PostDto> GetByIdAsync(int id)
	{
		var post = await context.Posts
			.Where(p => p.IsActive == true)
			.Include(u => u.StartedBy)
			.Include(r => r.Replies)
			.Include(d => d.Discussion)
			.AsSplitQuery()
			.FirstOrDefaultAsync(u => u.Id == id);

		if (post is null)
		{
			return new PostDto();
		}

		var postToReturn = new PostDto
		{
			Id = post.Id,
			Text = post.Text,
			StartedAt = post.StartedAt,
			StartedBy = post.StartedBy.Id,
			Replies = post.Replies.Select(r => new ReplyDto
			{
				Id = r.Id,
				Text = r.Text,
				RepliedAt = r.RepliedAt,
				IsActive = r.IsActive
			}).ToList(),
			IsActive = post.IsActive,
			Description = post.Description,
			Discussion = post.Discussion.Id
			
		};

		return postToReturn;
	}

	public async Task<IEnumerable<PostDto>> GetManyAsync()
	{
		var posts = await context.Posts
			.Where(p => p.IsActive == true)
			.Include(p => p.StartedBy)
			.Include(p => p.Replies).ThenInclude(r => r.RepliedBy)
			.Include(p => p.Discussion)
			.Where(p => p.IsActive == true).ToListAsync();

		var postsToReturn = posts.Select(p => new PostDto 
		{
			Id = p.Id,
			Description = p.Description,
			IsActive = p.IsActive,
			Text = p.Text,
			StartedAt = p.StartedAt,
			StartedBy = p.StartedBy.Id,
			Discussion = p.Discussion.Id,
			Replies = p.Replies != null ? p.Replies.Select(r => new ReplyDto
			{
				Id = r.Id,
				Text = r.Text,
				RepliedAt = r.RepliedAt,
				IsActive = r.IsActive,
				RepliedBy = r.RepliedBy.Id
			}).ToList() : new List<ReplyDto>()
		}).ToList();

		return postsToReturn;
	}

	public async Task AddOneAsync(PostDto item)
	{
		var startedBy = await context.Users.FindAsync(item.StartedBy);
		var discussion = await context.Discussions.FindAsync(item.Discussion);

		var post = new Post
		{
			Description = item.Description,
			StartedBy = startedBy,
			Text = item.Text,
			StartedAt = item.StartedAt,
			Discussion = discussion,
			IsActive = true,
			Replies = item.Replies.Select(r => new Reply 
			{
				Id = r.Id,
				Text = r.Text,
				RepliedAt = r.RepliedAt
			}).ToList()
		};

		await context.Posts.AddAsync(post);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(PostDto item, int id)
	{
		var oldPost = await context.Posts.FindAsync(id);

		if (oldPost is null)
		{
			return;
		}

		oldPost.Text = item.Text;
		oldPost.Description = item.Description;
		oldPost.StartedAt = item.StartedAt;
		oldPost.StartedBy = await context.Users.FindAsync(item.StartedBy);
		oldPost.IsActive = item.IsActive;
		oldPost.Discussion = await context.Discussions.FindAsync(item.Discussion);
		
		await context.SaveChangesAsync();
	}

	public async Task DeleteOneAsync(int id)
	{
		var postToDelete = await context.Posts.FindAsync(id);

		if (postToDelete is null)
		{
			return;
		}

		var entityEntry = context.Posts.Update(postToDelete);
		entityEntry.Property(p => p.IsActive).CurrentValue = false;

		await context.SaveChangesAsync();
	}
}