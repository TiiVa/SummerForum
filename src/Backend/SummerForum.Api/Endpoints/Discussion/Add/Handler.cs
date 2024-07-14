using FastEndpoints;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Discussion.Add;

public class Handler : Endpoint<Request, EmptyResponse>
{
	private readonly IDiscussionRepository _repo;

	public Handler(IDiscussionRepository repo)
	{
		_repo = repo;
	}

	public override void Configure()
	{
		Post("/discussions");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		
		var newDiscussion = new DiscussionDto
		{
			Description = req.Description,
			IsActive = req.IsActive,
			Posts = req.Posts,
			Department = req.Department

		};

		await _repo.AddOneAsync(newDiscussion);
	}
}