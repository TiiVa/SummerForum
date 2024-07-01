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
			/*RepliedBy = await context.Users,*/ // GetByIdAsync
			RepliedAt = reply.RepliedAt,
			IsActive = reply.IsActive

		};

		return replyToReturn;
		
	}

	public async Task<IEnumerable<ReplyDto>> GetManyAsync(int start, int count)
	{
		throw new NotImplementedException();
	}

	public async Task AddOneAsync(ReplyDto item)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateOneAsync(ReplyDto item)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteOneAsync(int id)
	{
		throw new NotImplementedException();
	}
}