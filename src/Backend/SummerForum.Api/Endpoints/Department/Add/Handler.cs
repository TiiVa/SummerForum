using FastEndpoints;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Department.Add;

public class Handler : Endpoint<Request, EmptyResponse>
{
	private readonly IDepartmentRepository _repo;
	public Handler(IDepartmentRepository repo)
	{
		_repo = repo;
	}

	public override void Configure()
	{
		Post("/departments");
		AllowAnonymous();
	}

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var departmentToAdd = new DepartmentDto()
		{
			Description = req.Description,
			Discussions = req.Discussions != null ? req.Discussions : new List<DiscussionDto>()
		};

		await _repo.AddOneAsync(departmentToAdd);
	}
}