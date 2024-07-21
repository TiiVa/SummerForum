using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
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

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var departments = await _repo.GetManyAsync(0, 10);

		if(departments.Any(d => d.Description.Equals(req.Description)))
		{
			return TypedResults.BadRequest(); // returnar 200 trots att hamnar här

		}

		var departmentToAdd = new DepartmentDto()
		{
			Description = req.Description,
		};

		await _repo.AddOneAsync(departmentToAdd);

		return TypedResults.Ok();
	}
}