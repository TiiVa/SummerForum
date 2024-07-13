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
		var post = await context.Posts.FindAsync(id);

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

	public async Task<IEnumerable<PostDto>> GetManyAsync(int start, int count)
	{
		var posts = await context.Posts.Skip(start).Take(count).ToListAsync();

		var postsToReturn = posts.Select(p => new PostDto
		{
			Id = p.Id,
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
		}).ToList();

		return postsToReturn;
	}

	public async Task AddOneAsync(PostDto item)
	{
		var startedBy = await context.Users.FindAsync(item.StartedBy);

		
		var post = new Post
		{
			Description = item.Description,
			StartedBy = startedBy,
			Text = item.Text,
			Replies = null,
			StartedAt = item.StartedAt,
			Discussion = null,
			IsActive = item.IsActive
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
		oldPost.StartedAt = item.StartedAt;
		oldPost.StartedBy = await context.Users.FindAsync(item.StartedBy);
		oldPost.IsActive = item.IsActive;
		
		await context.SaveChangesAsync();
	}

	public async Task DeleteOneAsync(int id)
	{
		var postToDelete = await context.Posts.FindAsync(id);

		if (postToDelete is null)
		{
			return;
		}

		context.Posts.Remove(postToDelete);
		await context.SaveChangesAsync();
	}
}