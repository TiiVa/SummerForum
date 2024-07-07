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
			StartedBy = new UserDto
			{
				Id = post.StartedBy.Id,
				UserName = post.StartedBy.UserName,
				Email = post.StartedBy.Email,
				Password = post.StartedBy.Password,
				IsActive = post.StartedBy.IsActive
			},
			Replies = post.ListOfReplies.Select(r => new ReplyDto
			{
				Id = r.Id,
				Text = r.Text,
				RepliedAt = r.RepliedAt,
				IsActive = r.IsActive
			}).ToList(),
			IsActive = post.IsActive,
			Description = post.Description,
			Discussion = new DiscussionDto
			{
				Id = post.Discussion.Id,
				Description = post.Discussion.Description,
				IsActive = post.Discussion.IsActive,
				Department = new DepartmentDto
				{
					Id = post.Discussion.Department.Id,
					Description = post.Discussion.Department.Description
				}
			}
			
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
			StartedBy = new UserDto
			{
				Id = p.StartedBy.Id,
				UserName = p.StartedBy.UserName,
				Email = p.StartedBy.Email,
				Password = p.StartedBy.Password,
				IsActive = p.StartedBy.IsActive
			},
			Replies = p.ListOfReplies.Select(r => new ReplyDto
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
		var post = new Post
		{
			Text = item.Text,
			StartedBy = await context.Users.FindAsync(item.StartedBy.Id),
			StartedAt = item.StartedAt,
			ListOfReplies = null
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
		oldPost.StartedBy = await context.Users.FindAsync(item.StartedBy.Id);
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